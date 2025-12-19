using System.Net.Http.Headers;
using System.Net.Http.Json;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

internal class StravaApiImpl : IActivitiesApi, IAthletesApi
{
    private readonly string STRAVA_API_BASE_URL = "https://www.strava.com/api/v3";
    private readonly StravaSession _session;
    private readonly HttpClient _client;
    private readonly SemaphoreSlim _authSemaphore = new(1, 1);

    public StravaApiImpl(StravaSession session, HttpClient httpClient)
    {
        _session = session;
        _client = httpClient;
    }

    public async Task<Stream> GetStreamAsync(string uriString, CancellationToken cancellationToken)
        => await GetStreamAsync(new Uri(uriString), cancellationToken).ConfigureAwait(false);

    public async Task<Stream> GetStreamAsync(Uri requestUri, CancellationToken cancellationToken)
    {
        var token = await GetAccessTokenAsync(cancellationToken).ConfigureAwait(false);

        using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        try
        {
            response.EnsureSuccessStatusCode();
            var memoryStream = new MemoryStream();
            // Provide a memory stream to the caller to manage disposal
            await response.Content.CopyToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
            response.Dispose();
            memoryStream.Position = 0;
            return memoryStream;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            response.Dispose();
            throw new StravaException("Rate limit exceeded", ex.StatusCode, innerException: ex);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            response.Dispose();
            throw new StravaException("Invalid login", ex.StatusCode, innerException: ex);
        }
        catch (HttpRequestException ex)
        {
            var message = $"Failed to fetch url '{requestUri}': {ex.Message}";
            response.Dispose();
            throw new StravaException(message, ex.StatusCode, innerException: ex);
        }
    }

    public async Task<ApiResult<T>> GetApiResultAsync<T>(string uriStringOrPath, CancellationToken cancellationToken = default)
        => await GetApiResultAsync<T>(GetUri(uriStringOrPath), cancellationToken).ConfigureAwait(false);

    public async Task<ApiResult<T>> GetApiResultAsync<T>(Uri requestUri, CancellationToken cancellationToken)
    {
        try
        {
            using var stream = await GetStreamAsync(requestUri, cancellationToken).ConfigureAwait(false);
            T? result = await StravaSerializer.DeserializeAsync<T>(stream, cancellationToken).ConfigureAwait(false);
            return result == null
                ? new ApiResult<T>(error: new ApiError("Unable to parse result"))
                : new ApiResult<T>(data: result);
        }
        catch (Exception ex)
        {
            return new ApiResult<T>(error: new ApiError(ex.Message, ex));
        }
    }

    public async Task<ApiResult<TResult>> PutApiResultAsync<TBody, TResult>(string uriStringOrPath, TBody? body, CancellationToken cancellationToken = default)
        => await PutApiResultAsync<TBody, TResult>(GetUri(uriStringOrPath), body, cancellationToken).ConfigureAwait(false);

    public async Task<ApiResult<TResult>> PutApiResultAsync<TBody, TResult>(Uri requestUri, TBody? body, CancellationToken cancellationToken = default)
    {
        ApiError? error = null;
        TResult? data = default;
        HttpResponseMessage? response = null;
        try
        {
            var token = await GetAccessTokenAsync(cancellationToken).ConfigureAwait(false);

            using var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = JsonContent.Create(body, options: StravaSerializer.Options);

            response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            data = await response.Content.ReadFromJsonAsync<TResult>(StravaSerializer.Options, cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            error = new ApiError("Rate limit exceeded", ex);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            error = new ApiError("Invalid login", ex);
        }
        catch (HttpRequestException ex)
        {
            error = new ApiError(ex);
        }
        catch (Exception ex)
        {
            error = new ApiError(new StravaException("Unexpected error posting Json data.", ex));
        }
        response?.Dispose();
        return new ApiResult<TResult>(data, error);
    }

    async Task<ApiResult<Athlete>> IStravaApi.GetAthleteAsync(long? athleteId, CancellationToken cancellationToken)
    {
        // Athlete must use custom serialization
        try
        {
            using var result = await GetStreamAsync(new Uri("https://www.strava.com/api/v3/athlete"), cancellationToken).ConfigureAwait(false);
            using var reader = new StreamReader(result);
            var json = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
            if (json != null)
            {
                var athlete = Athlete.FromJson(json);
                if (athlete != null)
                {
                    return new ApiResult<Athlete>(athlete);
                }
            }
            return new ApiResult<Athlete>(error: new ApiError("Unable to parse athlete data"));
        }
        catch (StravaException ex)
        {
            return new ApiResult<Athlete>(error: new ApiError(ex.Message, ex));
        }
    }

    private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        // Fast path: if authenticated, return token immediately without locking
        if (_session.IsAuthenticated)
        {
            return _session.Authorization.AccessToken;
        }

        // Slow path: need to refresh, use semaphore to ensure only one refresh happens
        await _authSemaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            // Double-check after acquiring semaphore
            if (_session.IsAuthenticated)
            {
                return _session.Authorization.AccessToken;
            }

            var result = await _session.RefreshAsync(cancellationToken).ConfigureAwait(false);
            if (!result.Success)
            {
                throw new StravaException(result.Error!.Message, result.Error.Exception);
            }
            return _session.Authorization.AccessToken;
        }
        finally
        {
            _authSemaphore.Release();
        }
    }

    private Uri GetUri(string uriStringOrPath)
    {
        return Uri.IsWellFormedUriString(uriStringOrPath, UriKind.Absolute)
            ? new Uri(uriStringOrPath)
            : new Uri(STRAVA_API_BASE_URL + uriStringOrPath);
    }
}

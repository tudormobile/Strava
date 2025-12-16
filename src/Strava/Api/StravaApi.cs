using System.Net.Http.Json;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

internal class StravaApiImpl : IActivitiesApi, IAthletesApi
{
    private readonly StravaSession _session;
    private readonly HttpClient _client;
    private readonly Lock _locker = new();

    public StravaApiImpl(StravaSession session, HttpClient httpClient)
    {
        _session = session;
        _client = UpdateAuthHeader(httpClient);
    }

    public async Task<Stream> GetStreamAsync(string uriString, CancellationToken cancellationToken)
        => await GetStreamAsync(new Uri(uriString), cancellationToken).ConfigureAwait(false);

    public async Task<Stream> GetStreamAsync(Uri requestUri, CancellationToken cancellationToken)
    {
        var client = await GetAuthenticatedClientAsync(cancellationToken).ConfigureAwait(false);
        var response = await client.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        try
        {
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            response.Dispose();
            throw new StravaException("Rate limit exceeded", ex);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            response.Dispose();
            throw new StravaException("Invalid login", ex);
        }
        catch (HttpRequestException ex)
        {
            var message = $"Failed to fetch url '{requestUri}': {ex.Message}";
            response.Dispose();
            throw new StravaException(message, ex);
        }
    }

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

    public async Task<ApiResult<TResult>> PutApiResultAsync<TBody, TResult>(Uri requestUri, TBody? body, CancellationToken cancellationToken = default)
    {
        ApiError? error = null;
        TResult? data = default;
        HttpResponseMessage? response = null;
        try
        {
            var client = await GetAuthenticatedClientAsync(cancellationToken).ConfigureAwait(false);
            response = await client.PutAsJsonAsync(requestUri, body, StravaSerializer.Options, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            data = await response.Content.ReadFromJsonAsync<TResult>(cancellationToken).ConfigureAwait(false);
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
        => await GetApiResultAsync<Athlete>(new Uri("https://www.strava.com/api/v3/athlete"), cancellationToken).ConfigureAwait(false);

    private async Task<ApiResult<HttpClient>> GetAuthenticatedClient()
    {
        if (!_session.IsAuthenticated)
        {
            var result = await _session.RefreshAsync();
            if (!result.Success)
            {
                return new ApiResult<HttpClient>(error: result.Error);
            }
            UpdateAuthHeader(_client);
        }
        return new ApiResult<HttpClient>(data: _client);
    }

    private HttpClient UpdateAuthHeader(HttpClient client)
    {
        lock (_locker)
        {
            if (client.DefaultRequestHeaders.Contains("Authorization"))
            {
                client.DefaultRequestHeaders.Remove("Authorization");
            }
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_session.Authorization.AccessToken}");
        }
        return client;
    }

    private async Task<HttpClient> GetAuthenticatedClientAsync(CancellationToken cancellationToken = default)
    {
        if (_session.IsAuthenticated) return _client;

        // Attempt to update the authentication and refresh the headers
        var result = await _session.RefreshAsync(cancellationToken).ConfigureAwait(false);
        if (!result.Success)
        {
            throw new StravaException(result.Error!.Message, result.Error.Exception);
        }
        return UpdateAuthHeader(_client);
    }

}

using System.Text.Json;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

internal class StravaApiImpl : IActivitiesApi, IAthletesApi
{
    private readonly StravaSession _session;
    private readonly Lazy<HttpClient> _client;
    public StravaApiImpl(StravaSession session)
    {
        _session = session;
        _client = new Lazy<HttpClient>(() => updateAuthHeader(new HttpClient()));
    }

    async Task<ApiResult<Athlete>> IStravaApi.GetAthlete(long? athleteId)
    {
        var uri = new Uri($"https://www.strava.com/api/v3/athlete");
        return await getRequest(await getAuthenticatedClient(), uri, json =>
            {
                var athlete = Athlete.FromJson(new StreamReader(json).ReadToEnd());
                _session.Authorization.Id = athlete?.Id ?? 0;
                return new ApiResult<Athlete>(data: athlete);
            });
    }

    async Task<ApiResult<DetailedActivity>> IActivitiesApi.GetActivity(long id, bool? includeAllEfforts)
    {
        var result = await getAuthenticatedClient();
        if (!result.Success)
        {
            return new ApiResult<DetailedActivity>(error: result.Error);
        }
        return new ApiResult<DetailedActivity>(error: new ApiError(new NotImplementedException()));
    }

    async Task<ApiResult<DetailedActivity>> IActivitiesApi.UpdateActivity(long id, UpdatableActivity activity)
    {
        var result = await getAuthenticatedClient();
        if (!result.Success)
        {
            return new(error: result.Error);
        }
        return new ApiResult<DetailedActivity>(error: new ApiError(new NotImplementedException()));
    }

    // http get "https://www.strava.com/api/v3/athlete/activities?before=&after=&page=&per_page=" "Authorization: Bearer [[token]]"
    async Task<ApiResult<List<SummaryActivity>>> IActivitiesApi.GetActivities(DateTime? before, DateTime? after, int? page, int? perPage)
    {
        var beforeDate = before ?? DateTime.Now;                       // default is 'now'
        var afterDate = after ?? DateTimeOffset.UnixEpoch.DateTime;    // default is 'epoch'
        var beforeOffset = new DateTimeOffset(beforeDate).ToUnixTimeSeconds();
        var afterOffset = new DateTimeOffset(afterDate).ToUnixTimeSeconds();
        var uri = new Uri($"https://www.strava.com/api/v3/athlete/activities?before={beforeOffset}&after={afterOffset}&page={page}&per_page={perPage}");
        return await getRequest<List<SummaryActivity>>(await getAuthenticatedClient(), uri, json =>
        {
            if (StravaSerializer.TryDeserialize(json, out SummaryActivity[]? activities, out var exception))
            {
                return new([.. activities!]);
            }
            throw exception ?? new JsonException("Failed to deserialize activities.");
        });
    }

    private async Task<ApiResult<HttpClient>> getAuthenticatedClient()
    {
        if (!_session.IsAuthenticated)
        {
            var result = await _session.RefreshAsync();
            if (!result.Success)
            {
                return new ApiResult<HttpClient>(error: result.Error);
            }
            updateAuthHeader(_client.Value);
        }
        return new ApiResult<HttpClient>(data: _client.Value);
    }

    private HttpClient updateAuthHeader(HttpClient client)
    {
        if (client.DefaultRequestHeaders.Contains("Authorization"))
        {
            client.DefaultRequestHeaders.Remove("Authorization");
        }
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_session.Authorization.AccessToken}");
        return client;
    }

    private async Task<ApiResult<T>> getRequest<T>(ApiResult<HttpClient> clientResult, Uri uri, Func<Stream, ApiResult<T>> builder)
    {
        if (!clientResult.Success || clientResult.Data == null)
        {
            return new ApiResult<T>(error: clientResult.Error);
        }
        var data = await clientResult.Data.GetAsync(uri);
        if (data.IsSuccessStatusCode)
        {
            try
            {
                var jsonStream = await data.Content.ReadAsStreamAsync();
                return builder(jsonStream);
            }
            catch (Exception ex)
            {
                return new ApiResult<T>(error: new ApiError(exception: ex));
            }
        }
        return new ApiResult<T>(error: new ApiError(data.ReasonPhrase));
    }

}

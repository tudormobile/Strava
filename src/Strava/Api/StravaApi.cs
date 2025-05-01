using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

internal class StravaApiImpl : IActivitiesApi, IAthletesApi
{
    private StravaSession _session;
    public StravaApiImpl(StravaSession session)
    {
        _session = session;
    }

    async Task<ApiResult<Athlete>> IStravaApi.GetAthlete(long? athleteId)
    {
        // try and authenticate first
        if (!_session.IsAuthenticated)
        {
            var result = await _session.RefreshAsync();
            if (!result.Success)
            {
                return new ApiResult<Athlete>(error: result.Error);
            }
        }
        var client = new HttpClient();
        var uri = new Uri($"https://www.strava.com/api/v3/athlete");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_session.Authorization.AccessToken}");
        var data = await client.GetAsync(uri);
        if (data.IsSuccessStatusCode)
        {
            try
            {
                var json = await data.Content.ReadAsStringAsync();
                var athlete = Athlete.FromJson(json);
                _session.Authorization.Id = athlete?.Id ?? 0;
                return new ApiResult<Athlete>(data: athlete);
            }
            catch (Exception ex)
            {
                return new ApiResult<Athlete>(error: new ApiError(exception: ex));
            }
        }
        return new ApiResult<Athlete>(error: new ApiError(data.ReasonPhrase));
    }

    async Task<ApiResult<DetailedActivity>> IActivitiesApi.GetActivity(long id, bool? includeAllEfforts)
    {
        // try and authenticate first
        if (!_session.IsAuthenticated)
        {
            var result = await _session.RefreshAsync();
            if (!result.Success)
            {
                return new ApiResult<DetailedActivity>(error: result.Error);
            }
        }
        return new ApiResult<DetailedActivity>(error: new ApiError(new NotImplementedException()));
    }

    async Task<ApiResult<DetailedActivity>> IActivitiesApi.UpdateActivity(long id, UpdatableActivity activity)
    {
        // try and authenticate first
        if (!_session.IsAuthenticated)
        {
            var result = await _session.RefreshAsync();
            if (!result.Success)
            {
                return new(error: result.Error);
            }
        }
        return new ApiResult<DetailedActivity>(error: new ApiError(new NotImplementedException()));

    }

    // http get "https://www.strava.com/api/v3/athlete/activities?before=&after=&page=&per_page=" "Authorization: Bearer [[token]]"
    async Task<ApiResult<List<SummaryActivity>>> IActivitiesApi.GetActivities(DateTime? before, DateTime? after, int? page, int? perPage)
    {
        var beforeDate = before ?? DateTime.Now;                       // default is 'now'
        var afterDate = after ?? DateTimeOffset.UnixEpoch.DateTime;    // default is 'epoch'
        if (!_session.IsAuthenticated)
        {
            var result = await _session.RefreshAsync();
            if (!result.Success)
            {
                return new(error: result.Error);
            }
        }
        var client = new HttpClient();
        var beforeOffset = new DateTimeOffset(beforeDate).ToUnixTimeSeconds();
        var afterOffset = new DateTimeOffset(afterDate).ToUnixTimeSeconds();
        var uri = new Uri($"https://www.strava.com/api/v3/athlete/activities?before={beforeOffset}&after={afterOffset}&page={page}&per_page={perPage}");

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_session.Authorization.AccessToken}");

        var data = await client.GetAsync(uri);

        if (data.IsSuccessStatusCode)
        {

            if (StravaSerializer.TryDeserialize(data.Content.ReadAsStream(), out SummaryActivity[]? activities, out var exception))
            {
                return new([.. activities!]);
            }
            return new ApiResult<List<SummaryActivity>>(null, new ApiError(exception: exception!));
        }
        return new ApiResult<List<SummaryActivity>>(null, new ApiError(data.ReasonPhrase));
    }

}

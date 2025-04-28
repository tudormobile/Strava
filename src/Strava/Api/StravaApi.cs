using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 API Interface.
/// </summary>
public interface IStravaApi
{
    /// <summary>
    /// Retrieve Athlete record by Id for logged in user.
    /// </summary>
    /// <param name="athleteId">Athlete Id of the logged in user.</param>
    /// <returns>Athlete record associated with the Id.</returns>
    /// <remarks>
    /// Returns the currently authenticated athlete. Tokens with profile:read_all scope will 
    /// receive a detailed athlete representation; all others will receive a summary representation.
    /// </remarks>
    Task<ApiResult<Athlete>> GetAthlete(long athleteId);

    /// <summary>
    /// List Athlete Activities
    /// </summary>
    /// <param name="before">Filtering activities that have taken place before a certain time.</param>
    /// <param name="after">Filtering activities that have taken place after a certain time.</param>
    /// <param name="page">Page number. Defaults to 1.</param>
    /// <param name="perPage">Number of items per page. Defaults to 30.</param>
    /// <returns>An array of SummaryActivity objects.</returns>
    /// <remarks>
    /// Returns the activities of an athlete for a specific identifier. Requires activity:read. 
    /// Only Me activities will be filtered out unless requested by a token with activity:read_all.
    /// </remarks>
    Task<ApiResult<List<SummaryActivity>>> GetActivities(DateTime before, DateTime after, int? page = 1, int? perPage = 30);

    /// <summary>
    /// Get Activity
    /// </summary>
    /// <param name="id">The identifier of the activity.</param>
    /// <param name="includeAllEfforts">True to include all segments efforts.</param>
    /// <returns>The activity's detailed representation. An instance of DetailedActivity.</returns>
    Task<ApiResult<DetailedActivity>> GetActivity(long id, bool? includeAllEfforts = false);

    /// <summary>
    /// Update Activity
    /// </summary>
    /// <param name="id">The identifier of the activity.</param>
    /// <param name="activity">An instance of UpdatableActivity.</param>
    /// <returns>The activity's detailed representation. An instance of DetailedActivity.</returns>
    /// <remarks>
    /// Updates the given activity that is owned by the authenticated athlete. Requires activity:write. 
    /// Also requires activity:read_all in order to update Only Me activities
    /// </remarks>
    Task<ApiResult<DetailedActivity>> UpdateActivity(long id, UpdatableActivity activity);
}

internal class StravaApiImpl : IStravaApi
{
    private StravaSession _session;
    public StravaApiImpl(StravaSession session)
    {
        _session = session;
    }

    async Task<ApiResult<Athlete>> IStravaApi.GetAthlete(long athleteId)
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
                return new ApiResult<Athlete>(Athlete.FromJson(json));
            }
            catch (Exception ex)
            {
                return new ApiResult<Athlete>(null, new ApiError(error: ex));
            }
        }
        return new ApiResult<Athlete>(null, new ApiError(data.ReasonPhrase));
    }

    async Task<ApiResult<DetailedActivity>> IStravaApi.GetActivity(long id, bool? includeAllEfforts)
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

    async Task<ApiResult<DetailedActivity>> IStravaApi.UpdateActivity(long id, UpdatableActivity activity)
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
    async Task<ApiResult<List<SummaryActivity>>> IStravaApi.GetActivities(DateTime before, DateTime after, int? page, int? perPage)
    {
        if (!_session.IsAuthenticated)
        {
            var result = await _session.RefreshAsync();
            if (!result.Success)
            {
                return new(error: result.Error);
            }
        }
        var client = new HttpClient();
        var beforeOffset = new DateTimeOffset(before).ToUnixTimeSeconds();
        var afterOffset = new DateTimeOffset(after).ToUnixTimeSeconds();
        var uri = new Uri($"https://www.strava.com/api/v3/athlete/activities?before={beforeOffset}&after={afterOffset}&page={page}&per_page={perPage}");

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_session.Authorization.AccessToken}");

        var data = await client.GetAsync(uri);

        if (data.IsSuccessStatusCode)
        {

            if (StravaSerializer.TryDeserialize(data.Content.ReadAsStream(), out SummaryActivity[]? activities, out var exception))
            {
                return new([.. activities!]);
            }
            return new ApiResult<List<SummaryActivity>>(null, new ApiError(error: exception!));
        }
        return new ApiResult<List<SummaryActivity>>(null, new ApiError(data.ReasonPhrase));
    }

}

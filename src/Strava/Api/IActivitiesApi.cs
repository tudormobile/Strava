using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Activities API Interface.
/// </summary>
public interface IActivitiesApi : IStravaApi
{
    /// <summary>
    /// Creates a manual activity for an athlete, requires activity:write scope.
    /// </summary>
    /// <param name="name">The name of the activity.</param>
    /// <param name="type">Type of activity. For example - Run, Ride etc.</param>
    /// <param name="sportType">Sport type of activity. For example - Run, MountainBikeRide, Ride, etc.</param>
    /// <param name="startDateLocal">Start date and time.</param>
    /// <param name="elapsedTime">Elapsed time, in seconds.</param>
    /// <param name="description">Description of the activity.</param>
    /// <param name="distance">Distance, in meters.</param>
    /// <param name="trainer">True if this is a trainer activity.</param>
    /// <param name="commute">True if this is a commute activity.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    Task<ApiResult<DetailedActivity>> CreateActivityAsync(
        string name,
        string type,
        string sportType,
        DateTime startDateLocal,
        double elapsedTime,
        string description,
        double distance,
        bool trainer,
        bool commute
        ) => throw new NotImplementedException();

    /// <summary>
    /// List Athlete Activities
    /// </summary>
    /// <param name="before">Filtering activities that have taken place before a certain time.</param>
    /// <param name="after">Filtering activities that have taken place after a certain time.</param>
    /// <param name="page">Page number. (Defaults to 1).</param>
    /// <param name="perPage">Number of items per page. (Defaults to 30).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An array of SummaryActivity objects.</returns>
    /// <remarks>
    /// Returns the activities of an athlete for a specific identifier. Requires activity:read. 
    /// Only Me activities will be filtered out unless requested by a token with activity:read_all.
    /// <para>
    /// If the before parameter is not provided, the current date and time is assumed. If the after parameter
    /// is not provided, all available activities are returned. The page and per_page parameters are used to
    /// limit the number of activities returned.
    /// </para>
    /// </remarks>
    async Task<ApiResult<List<SummaryActivity>>> GetActivitiesAsync(DateTime? before = null, DateTime? after = null, int? page = 1, int? perPage = 30, CancellationToken cancellationToken = default)
    {
        var beforeDate = before ?? DateTime.Now;                       // default is 'now'
        var afterDate = after ?? DateTimeOffset.UnixEpoch.DateTime;    // default is 'epoch'
        var beforeOffset = new DateTimeOffset(beforeDate).ToUnixTimeSeconds();
        var afterOffset = new DateTimeOffset(afterDate).ToUnixTimeSeconds();
        var requestUri = new Uri($"https://www.strava.com/api/v3/athlete/activities?before={beforeOffset}&after={afterOffset}&page={page}&per_page={perPage}");

        return await GetApiResultAsync<List<SummaryActivity>>(requestUri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get Activity
    /// </summary>
    /// <param name="id">The identifier of the activity.</param>
    /// <param name="includeAllEfforts">True to include all segments efforts.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The activity's detailed representation. An instance of DetailedActivity.</returns>
    async Task<ApiResult<DetailedActivity>> GetActivityAsync(long id, bool? includeAllEfforts = false, CancellationToken cancellationToken = default)
    {
        var requestUri = new Uri($"https://www.strava.com/api/v3/activities/{id}");
        return await GetApiResultAsync<DetailedActivity>(requestUri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns the comments on the given activity. 
    /// Requires activity:read for Everyone and Followers activities. 
    /// Requires activity:read_all for Only Me activities.
    /// </summary>
    /// <param name="id">The identifier of the activity.</param>
    /// <param name="afterCursor">Cursor of the last item in the previous page of results, used to request the subsequent page of results. When omitted, the first page of results is fetched.</param>
    /// <returns>A list of Comments.</returns>
    /// <exception cref="NotImplementedException"></exception>
    Task<ApiResult<List<Comment>>> ListActivityCommentsAsync(long id, string? afterCursor = null)
        => throw new NotImplementedException();

    /// <summary>
    /// Returns the athletes who kudoed an activity identified by an identifier. 
    /// Requires activity:read for Everyone and Followers activities. 
    /// Requires activity:read_all for Only Me activities.
    /// </summary>
    /// <param name="id">The identifier of the activity.</param>
    /// <param name="page">Page number. Defaults to 1.</param>
    /// <param name="perPage">Number of items per page. Defaults to 30.
    ///</param>
    /// <returns>A list of SummaryAthlete objects.</returns>
    /// <exception cref="NotImplementedException"></exception>
    Task<ApiResult<List<Athlete>>> ListActivityKudoersAsync(long id, int? page = 1, int? perPage = 30)
        => throw new NotImplementedException();

    /// <summary>
    /// Returns the laps of an activity identified by an identifier. 
    /// Requires activity:read for Everyone and Followers activities. 
    /// Requires activity:read_all for Only Me activities.
    /// </summary>
    /// <param name="id">The identifier of the activity.</param>
    /// <returns>A collection of Lap objects.</returns>
    /// <exception cref="NotImplementedException"></exception>
    Task<ApiResult<List<Lap>>> ListActivityLaps(long id)
        => throw new NotImplementedException();

    /// <summary>
    /// Returns the zones of a given activity. 
    /// Requires activity:read for Everyone and Followers activities. 
    /// Requires activity:read_all for Only Me activities.
    /// </summary>
    /// <param name="id">The identifier of the activity.</param>
    /// <returns>An collection of ActivityZone objects.</returns>
    /// <exception cref="NotImplementedException"></exception>
    Task<ApiResult<List<ActivityZone>>> GetActivityZones(long id)
        => throw new NotImplementedException();

    /// <summary>
    /// Update Activity
    /// </summary>
    /// <param name="id">The identifier of the activity.</param>
    /// <param name="activity">An instance of UpdatableActivity.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The activity's detailed representation. An instance of DetailedActivity.</returns>
    /// <remarks>
    /// Updates the given activity that is owned by the authenticated athlete. Requires activity:write. 
    /// Also requires activity:read_all in order to update Only Me activities
    /// </remarks>
    async Task<ApiResult<DetailedActivity>> UpdateActivityAsync(long id, UpdatableActivity activity, CancellationToken cancellationToken = default)
    {
        var requestUri = new Uri($"https://www.strava.com/api/v3/activities/{id}");
        return await PutApiResultAsync<UpdatableActivity, DetailedActivity>(requestUri, activity, cancellationToken).ConfigureAwait(false);
    }
}


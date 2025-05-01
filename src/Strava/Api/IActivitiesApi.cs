using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Activities API Interface.
/// </summary>
public interface IActivitiesApi : IStravaApi
{
    /// <summary>
    /// List Athlete Activities
    /// </summary>
    /// <param name="before">Filtering activities that have taken place before a certain time.</param>
    /// <param name="after">Filtering activities that have taken place after a certain time.</param>
    /// <param name="page">Page number. (Defaults to 1).</param>
    /// <param name="perPage">Number of items per page. (Defaults to 30).</param>
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
    Task<ApiResult<List<SummaryActivity>>> GetActivities(DateTime? before = null, DateTime? after = null, int? page = 1, int? perPage = 30);

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


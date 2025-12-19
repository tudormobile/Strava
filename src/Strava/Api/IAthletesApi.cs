using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Athletes API Interface.
/// </summary>
public interface IAthletesApi : IStravaApi
{
    /// <summary>
    /// Returns the authenticated athlete's heart rate and power zones. Requires profile:read_all.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>Collection of the athlete's heart rate and power zones.</returns>
    Task<ApiResult<List<PowerZoneRanges>>> GetLoggedInAthleteZones(CancellationToken cancellationToken = default) => GetApiResultAsync<List<PowerZoneRanges>>("/athlete/zones", cancellationToken);

    /// <summary>
    /// Returns the activity stats of an athlete. Only includes data from activities set to Everyone visibility.
    /// </summary>
    /// <param name="athleteId">The identifier of the athlete. Must match the authenticated athlete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The athlete's statistics.</returns>
    Task<ApiResult<ActivityStats>> GetStats(long athleteId, CancellationToken cancellationToken = default) => GetApiResultAsync<ActivityStats>($"/athletes/{athleteId}/stats", cancellationToken);

    /// <summary>
    /// Update the weight of the currently authenticated athlete. Requires profile:write scope.
    /// </summary>
    /// <param name="weight">The weight of the athlete in kilograms.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The updated athlete details.</returns>
    Task<ApiResult<DetailedAthlete>> UpdateLoggedInAthlete(double weight, CancellationToken cancellationToken = default) => PutApiResultAsync<double, DetailedAthlete>("/athlete", weight, cancellationToken);

}
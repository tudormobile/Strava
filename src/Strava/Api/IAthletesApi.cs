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
    Task<ApiResult<List<PowerZoneRanges>>> GetLoggedInAthleteZonesAsync(CancellationToken cancellationToken = default)
        => GetApiResultAsync<List<PowerZoneRanges>>("/athlete/zones", cancellationToken);

    /// <summary>
    /// Returns the activity stats of an athlete. Only includes data from activities set to Everyone visibility.
    /// </summary>
    /// <param name="athleteId">The identifier of the athlete. Must match the authenticated athlete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The athlete's statistics.</returns>
    Task<ApiResult<ActivityStats>> GetStatsAsync(long athleteId, CancellationToken cancellationToken = default)
        => GetApiResultAsync<ActivityStats>($"/athletes/{athleteId}/stats", cancellationToken);

    /// <summary>
    /// Update the weight of the currently authenticated athlete. Requires profile:write scope.
    /// </summary>
    /// <param name="weight">The weight of the athlete in kilograms.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The updated athlete details.</returns>
    Task<ApiResult<DetailedAthlete>> UpdateLoggedInAthleteAsync(double weight, CancellationToken cancellationToken = default)
        => PutApiResultAsync<double, DetailedAthlete>("/athlete", weight, cancellationToken);

    /// <summary>
    /// Returns a list of clubs the authenticated athlete is a member of.
    /// </summary>
    /// <param name="page">Page number. Defaults to null.</param>
    /// <param name="perPage">Number of items per page. Defaults to null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A collection of <see cref="SummaryClub"/> objects representing the athlete's clubs.</returns>
    Task<ApiResult<List<SummaryClub>>> ListAthleteClubsAsync(int? page = null, int? perPage = null, CancellationToken cancellationToken = default)
        => GetApiResultAsync<List<SummaryClub>>(ApiExtensions.AddQueryToUriString($"/athlete/clubs", [("page", page), ("per_page", perPage)]), cancellationToken);
}
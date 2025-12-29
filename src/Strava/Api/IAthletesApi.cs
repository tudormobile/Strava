using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Athletes API Interface.
/// </summary>
public interface IAthletesApi
{
    /// <summary>
    /// Retrieve Athlete record by Id for logged in user.
    /// </summary>
    /// <param name="athleteId">Optional; Athlete Id (default = logged in user).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>Athlete record associated with the Id.</returns>
    /// <remarks>
    /// Returns the currently authenticated athlete. Tokens with profile:read_all scope will 
    /// receive a detailed athlete representation; all others will receive a summary representation.
    /// </remarks>
    Task<ApiResult<Athlete>> GetAthleteAsync(long? athleteId = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the authenticated athlete's heart rate and power zones. Requires profile:read_all.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>Collection of the athlete's heart rate and power zones.</returns>
    Task<ApiResult<List<PowerZoneRanges>>> GetLoggedInAthleteZonesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the activity stats of an athlete. Only includes data from activities set to Everyone visibility.
    /// </summary>
    /// <param name="athleteId">The identifier of the athlete. Must match the authenticated athlete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The athlete's statistics.</returns>
    Task<ApiResult<ActivityStats>> GetStatsAsync(long athleteId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update the weight of the currently authenticated athlete. Requires profile:write scope.
    /// </summary>
    /// <param name="weight">The weight of the athlete in kilograms.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The updated athlete details.</returns>
    Task<ApiResult<DetailedAthlete>> UpdateLoggedInAthleteAsync(double weight, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a list of clubs the authenticated athlete is a member of.
    /// </summary>
    /// <param name="page">Page number. Defaults to null.</param>
    /// <param name="perPage">Number of items per page. Defaults to null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A collection of <see cref="SummaryClub"/> objects representing the athlete's clubs.</returns>
    Task<ApiResult<List<SummaryClub>>> ListAthleteClubsAsync(int? page = null, int? perPage = null, CancellationToken cancellationToken = default);
}

internal partial class StravaApiImpl
{
    /// <inheritdoc/>
    Task<ApiResult<Athlete>> IAthletesApi.GetAthleteAsync(long? athleteId, CancellationToken cancellationToken)
        => ((IStravaApi)this).GetAthleteAsync(athleteId, cancellationToken);

    /// <inheritdoc/>
    Task<ApiResult<List<PowerZoneRanges>>> IAthletesApi.GetLoggedInAthleteZonesAsync(CancellationToken cancellationToken)
        => GetApiResultAsync<List<PowerZoneRanges>>("/athlete/zones", cancellationToken);

    /// <inheritdoc/>
    Task<ApiResult<ActivityStats>> IAthletesApi.GetStatsAsync(long athleteId, CancellationToken cancellationToken)
        => GetApiResultAsync<ActivityStats>($"/athletes/{athleteId}/stats", cancellationToken);

    /// <inheritdoc/>
    Task<ApiResult<DetailedAthlete>> IAthletesApi.UpdateLoggedInAthleteAsync(double weight, CancellationToken cancellationToken)
        => PutApiResultAsync<double, DetailedAthlete>("/athlete", weight, cancellationToken);

    /// <inheritdoc/>
    Task<ApiResult<List<SummaryClub>>> IAthletesApi.ListAthleteClubsAsync(int? page, int? perPage, CancellationToken cancellationToken)
        => GetApiResultAsync<List<SummaryClub>>(ApiExtensions.AddQueryToUriString($"/athlete/clubs", [("page", page), ("per_page", perPage)]), cancellationToken);
}
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Clubs API Interface.
/// </summary>
public interface IClubsApi
{
    /// <summary>
    /// Returns a given a club using its identifier.
    /// </summary>
    /// <param name="id">The identifier of the club.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The club's detailed representation. An instance of DetailedClub.</returns>
    Task<ApiResult<DetailedClub>> GetClubAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the athletes of a given club using its identifier.
    /// </summary>
    /// <param name="id">The identifier of the club.</param>
    /// <param name="page">Page number (optional).</param>
    /// <param name="perPage">Number of items per page (optional).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of club athletes. An instance of <see cref="ApiResult{T}"/> where T is <see cref="List{ClubAthlete}"/>.</returns>
    Task<ApiResult<List<ClubAthlete>>> ListClubMembersAsync(long id, int? page = null, int? perPage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the administrators of a given club using its identifier.
    /// </summary>
    /// <param name="id">The identifier of the club.</param>
    /// <param name="page">Page number (optional).</param>
    /// <param name="perPage">Number of items per page (optional).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of club administrators. An instance of <see cref="ApiResult{T}"/> where T is <see cref="List{SummaryAthlete}"/>.</returns>
    Task<ApiResult<List<SummaryAthlete>>> ListClubAdministratorsAsync(long id, int? page = null, int? perPage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the activities of a given club using its identifier.
    /// </summary>
    /// <param name="id">The identifier of the club.</param>
    /// <param name="page">Page number (optional).</param>
    /// <param name="perPage">Number of items per page (optional).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of club activities. An instance of <see cref="ApiResult{T}"/> where T is <see cref="List{ClubActivity}"/>.</returns>
    Task<ApiResult<List<ClubActivity>>> ListClubActivitiesAsync(long id, int? page = null, int? perPage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the clubs that the currently authenticated athlete is a member of.
    /// </summary>
    /// <param name="page">Page number (optional).</param>
    /// <param name="perPage">Number of items per page (optional).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of clubs. An instance of <see cref="ApiResult{T}"/> where T is <see cref="List{SummaryClub}"/>.</returns>
    Task<ApiResult<List<SummaryClub>>> ListAthleteClubsAsync(int? page = null, int? perPage = null, CancellationToken cancellationToken = default);
}

internal partial class StravaApiImpl
{
    /// <inheritdoc/>
    Task<ApiResult<DetailedClub>> IClubsApi.GetClubAsync(long id, CancellationToken cancellationToken)
        => GetApiResultAsync<DetailedClub>($"/clubs/{id}", cancellationToken);

    /// <inheritdoc/>
    Task<ApiResult<List<ClubAthlete>>> IClubsApi.ListClubMembersAsync(long id, int? page, int? perPage, CancellationToken cancellationToken)
        => GetApiResultAsync<List<ClubAthlete>>(ApiExtensions.AddQueryToUriString($"/clubs/{id}/members", [("page", page), ("per_page", perPage)]), cancellationToken);

    /// <inheritdoc/>
    Task<ApiResult<List<SummaryAthlete>>> IClubsApi.ListClubAdministratorsAsync(long id, int? page, int? perPage, CancellationToken cancellationToken)
        => GetApiResultAsync<List<SummaryAthlete>>(ApiExtensions.AddQueryToUriString($"/clubs/{id}/admins", [("page", page), ("per_page", perPage)]), cancellationToken);

    /// <inheritdoc/>
    Task<ApiResult<List<ClubActivity>>> IClubsApi.ListClubActivitiesAsync(long id, int? page, int? perPage, CancellationToken cancellationToken)
        => GetApiResultAsync<List<ClubActivity>>(ApiExtensions.AddQueryToUriString($"/clubs/{id}/activities", [("page", page), ("per_page", perPage)]), cancellationToken);

    /// <inheritdoc/>
    Task<ApiResult<List<SummaryClub>>> IClubsApi.ListAthleteClubsAsync(int? page, int? perPage, CancellationToken cancellationToken)
        => GetApiResultAsync<List<SummaryClub>>(ApiExtensions.AddQueryToUriString($"/athlete/clubs", [("page", page), ("per_page", perPage)]), cancellationToken);
}
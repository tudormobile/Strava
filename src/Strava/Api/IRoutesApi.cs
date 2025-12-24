using Tudormobile.Strava.Documents;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Routes API Interface.
/// </summary>
public interface IRoutesApi
{
    /// <summary>
    /// Retrieves detailed information about a specific route by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the route.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="ApiResult{Route}"/> containing the detailed route information.</returns>
    Task<ApiResult<Route>> GetRouteAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a paginated list of routes created by the specified athlete.
    /// </summary>
    /// <param name="athleteId">The unique identifier of the athlete whose routes are to be retrieved.</param>
    /// <param name="perPage">The maximum number of routes to return per page. If null, the default page size is used.</param>
    /// <param name="page">The page number of results to retrieve. If null, the first page is returned.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ApiResult with a list of Route
    /// objects created by the athlete. The list will be empty if the athlete has no routes.</returns>
    Task<ApiResult<List<Route>>> ListAthleteRoutesAsync(long athleteId, int? page = null, int? perPage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously exports the specified route as a TCX (Training Center XML) document.
    /// </summary>
    /// <param name="id">The unique identifier of the route to export.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the export operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ApiResult{TcxDocument}"/> with the exported TCX document for the specified route.</returns>
    Task<ApiResult<TcxDocument>> ExportRouteTCXAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously exports the specified route as a GPX document.
    /// </summary>
    /// <param name="id">The unique identifier of the route to export.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the export operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ApiResult{GpxDocument}"/> with the exported GPX document if the operation succeeds.</returns>
    Task<ApiResult<GpxDocument>> ExportRouteGPXAsync(long id, CancellationToken cancellationToken = default);
}

internal partial class StravaApiImpl : IRoutesApi
{
    /// <inheritdoc/>
    public Task<ApiResult<Route>> GetRouteAsync(long id, CancellationToken cancellationToken = default)
        => GetApiResultAsync<Route>($"/routes/{id}", cancellationToken);

    /// <inheritdoc/>
    public Task<ApiResult<List<Route>>> ListAthleteRoutesAsync(long id, int? page = null, int? perPage = null, CancellationToken cancellationToken = default)
        => GetApiResultAsync<List<Route>>(ApiExtensions.AddQueryToUriString($"/athletes/{id}/routes", [("page", page), ("per_page", perPage)]), cancellationToken);

    /// <inheritdoc/>
    public Task<ApiResult<TcxDocument>> ExportRouteTCXAsync(long id, CancellationToken cancellationToken = default)
        => GetApiResultAsync<TcxDocument>($"/routes/{id}/export_tcx", cancellationToken);

    /// <inheritdoc/>
    public Task<ApiResult<GpxDocument>> ExportRouteGPXAsync(long id, CancellationToken cancellationToken = default)
        => GetApiResultAsync<GpxDocument>($"/routes/{id}/export_gpx", cancellationToken);

}
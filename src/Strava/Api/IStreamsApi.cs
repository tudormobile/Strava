using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Streams API Interface.
/// </summary>
public interface IStreamsApi
{
    /// <summary>
    /// Asynchronously retrieves a list of activity streams for the specified resource and activity types.
    /// </summary>
    /// <param name="id">The unique identifier of the resource for which to retrieve activity streams.</param>
    /// <param name="types">A collection of activity stream types to include in the result. Each type specifies a category of activity data to
    /// retrieve. Cannot be null or empty.</param>
    /// <param name="keysByType">true to group the returned activity streams by type; otherwise, false to return a flat list.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ApiResult with a list of
    /// ActivityStream objects matching the specified types. The list may be empty if no streams are found.</returns>
    Task<ApiResult<List<ActivityStream>>> GetActivityStreamsAsync(long id, IEnumerable<String> types, bool keysByType = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves the requested data streams for a specific segment effort.
    /// </summary>
    /// <remarks>If a requested stream type is not available for the specified segment effort, it will be
    /// omitted from the result. The method supports cancellation via the provided token.</remarks>
    /// <param name="id">The identifier of the segment effort for which to retrieve streams.</param>
    /// <param name="types">A collection of stream types to retrieve. Each type specifies a particular data stream, such as time, distance, or
    /// heart rate.</param>
    /// <param name="keysByType">true to return the result as a dictionary keyed by stream type; otherwise, false to return a list of streams in
    /// the order requested.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ApiResult with a list of
    /// SegmentEffortStream objects corresponding to the requested stream types.</returns>
    Task<ApiResult<List<SegmentEffortStream>>> GetSegmentEffortStreamsAsync(long id, IEnumerable<String> types, bool keysByType = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a list of segment streams for the specified segment and stream types.
    /// </summary>
    /// <param name="id">The unique identifier of the segment for which to retrieve streams.</param>
    /// <param name="types">A collection of stream type names to filter the results. Only streams matching these types are returned.</param>
    /// <param name="keysByType">true to group the returned streams by type; otherwise, false to return a flat list.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ApiResult with a list of
    /// SegmentStream objects matching the specified criteria.</returns>
    Task<ApiResult<SegmentStreamCollection>> GetSegmentStreamsAsync(long id, IEnumerable<String> types, bool keysByType = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves all route streams associated with the specified route identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the route for which to retrieve streams.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="ApiResult{T}"/> with a
    /// list of <see cref="RouteStream"/> objects for the specified route. If no streams are found, the list will be
    /// empty.</returns>
    Task<ApiResult<List<RouteStream>>> GetRouteStreamsAsync(long id, CancellationToken cancellationToken = default);
}

internal partial class StravaApiImpl
{
    /// <inheritdoc/>
    public Task<ApiResult<List<ActivityStream>>> GetActivityStreamsAsync(long id, IEnumerable<string> types, bool keysByType = true, CancellationToken cancellationToken = default)
        => GetApiResultAsync<List<ActivityStream>>(ApiExtensions.AddQueryToUriString($"/activities/{id}/streams", [("keys", types.ToArray()), ("keys_by_type", keysByType.ToString())]), cancellationToken);
    /// <inheritdoc/>
    public Task<ApiResult<List<SegmentEffortStream>>> GetSegmentEffortStreamsAsync(long id, IEnumerable<string> types, bool keysByType = true, CancellationToken cancellationToken = default)
        => GetApiResultAsync<List<SegmentEffortStream>>(ApiExtensions.AddQueryToUriString($"/segment_efforts/{id}/streams", [("keys", types.ToArray()), ("keys_by_type", keysByType.ToString())]), cancellationToken);
    /// <inheritdoc/>
    public Task<ApiResult<SegmentStreamCollection>> GetSegmentStreamsAsync(long id, IEnumerable<string> types, bool keysByType = true, CancellationToken cancellationToken = default)
        => GetApiResultAsync<SegmentStreamCollection>(ApiExtensions.AddQueryToUriString($"/segments/{id}/streams", [("keys", types.ToArray()), ("keys_by_type", keysByType.ToString())]), cancellationToken);
    /// <inheritdoc/>
    public Task<ApiResult<List<RouteStream>>> GetRouteStreamsAsync(long id, CancellationToken cancellationToken = default)
        => GetApiResultAsync<List<RouteStream>>($"/routes/{id}/streams", cancellationToken);
}

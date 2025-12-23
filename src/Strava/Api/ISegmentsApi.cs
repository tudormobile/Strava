using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Segments API Interface.
/// </summary>
public interface ISegmentsApi : ISegmentEffortsApi
{
    /// <summary>
    /// Gets a detailed segment by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the segment.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An <see cref="ApiResult{T}"/> containing the <see cref="DetailedSegment"/> if successful; otherwise, an error.
    /// </returns>
    Task<ApiResult<DetailedSegment>> GetSegmentAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a list of the authenticated athlete's starred segments.
    /// </summary>
    /// <param name="page">Page number (optional).</param>
    /// <param name="perPage">Number of items per page (optional).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An instance of <see cref="ApiResult{T}"/> where T is <see cref="List{Segment}"/>.
    /// </returns>
    Task<ApiResult<List<Segment>>> ListStarredSegmentsAsync(int? page = null, int? perPage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a list of the top 10 segments matching a specified query.
    /// </summary>
    /// <param name="bounds">The geographical bounds within which to search for segments.</param>
    /// <param name="activityType">The type of activity (e.g., "running", "riding") to filter segments.</param>
    /// <param name="minimumCategory">The minimum climb category to filter segments (optional).</param>
    /// <param name="maximumCategory">The maximum climb category to filter segments (optional).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An instance of <see cref="ApiResult{T}"/> where T is <see cref="SegmentList"/>.
    /// </returns>
    Task<ApiResult<SegmentList>> ExploreSegmentsAsync(Bounds bounds, string? activityType = null, int? minimumCategory = null, int? maximumCategory = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stars or unstars the specified segment asynchronously for the authenticated user.
    /// </summary>
    /// <param name="id">The unique identifier of the segment to star or unstar.</param>
    /// <param name="removeStar">If <see langword="true"/>, removes the star from the segment; otherwise, adds a star. The default is <see
    /// langword="false"/>.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ApiResult{Segment}"/> with the updated segment details.</returns>
    Task<ApiResult<Segment>> StarSegmentAsync(long id, bool removeStar = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the authenticated athlete's star from the specified segment asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the segment to unstar.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ApiResult{Segment}"/> with details of the updated segment.</returns>
    Task<ApiResult<Segment>> UnstarSegmentAsync(long id, CancellationToken cancellationToken = default)
        => StarSegmentAsync(id, removeStar: true, cancellationToken);
}

/// <summary>
/// Strava V3 Segment Efforts API Interface.
/// </summary>
public interface ISegmentEffortsApi
{
    /// <summary>
    /// Returns a segment effort from an activity that is owned by the authenticated athlete. Requires subscription.
    /// </summary>
    /// <param name="id">The unique identifier of the segment effort.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An <see cref="ApiResult{T}"/> containing the <see cref="SegmentEffort"/> if successful; otherwise, an error.
    /// </returns>
    Task<ApiResult<SegmentEffort>> GetSegmentEffortAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a list of efforts for a specified segment, optionally filtered by local start and end dates.
    /// </summary>
    /// <param name="segmentId">The unique identifier of the segment for which to retrieve efforts.</param>
    /// <param name="startDateLocal">The earliest local start date and time for efforts to include in the results. Only efforts starting on or after
    /// this date are returned. If null, no lower date filter is applied.</param>
    /// <param name="endDateLocal">The latest local start date and time for efforts to include in the results. Only efforts starting on or before
    /// this date are returned. If null, no upper date filter is applied.</param>
    /// <param name="perPage">The maximum number of efforts to return per page. If null, the default page size is used. Must be positive if
    /// specified.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="ApiResult{T}"/> with a
    /// list of <see cref="SegmentEffort"/> objects matching the specified criteria. The list may be empty if no efforts
    /// are found.</returns>
    Task<ApiResult<List<SegmentEffort>>> ListSegmentEffortsAsync(long segmentId, DateTime? startDateLocal = null, DateTime? endDateLocal = null, int? perPage = null, CancellationToken cancellationToken = default);
}


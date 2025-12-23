namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a collection of segments.
/// </summary>
/// <remarks>Use this class to group multiple <see cref="Segment"/> instances for batch processing or
/// organizational purposes. The <see cref="Segments"/> property provides access to the underlying list of
/// segments.</remarks>
public class SegmentList
{
    /// <summary>
    /// Gets or sets the collection of segments associated with this instance.
    /// </summary>
    /// <remarks>The order of segments in the list may be significant depending on how the segments are
    /// processed or displayed. Modifying the collection directly affects the segments managed by this
    /// instance.</remarks>
    public List<Segment> Segments { get; set; } = [];
}

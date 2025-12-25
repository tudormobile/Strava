namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a stream of numeric data (e.g., heart rate, power, etc.) for a segment effort in Strava.
/// </summary>
/// <remarks>
/// Inherits common stream metadata from <see cref="StreamBase"/>. The <see cref="Data"/> property contains
/// the time-series values for the segment effort.
/// </remarks>
public class SegmentEffortStream : StreamBase
{
    /// <summary>
    /// Gets or sets the list of numeric data points for the segment effort stream.
    /// </summary>
    /// <remarks>
    /// Each value represents a measurement (such as heart rate, cadence, or power) at a specific point in the segment effort.
    /// </remarks>
    public List<double> Data { get; set; } = [];
}

namespace Tudormobile.Strava.Model;

/// <summary>
/// Base class for all Strava stream types, providing common stream metadata properties.
/// </summary>
/// <remarks>
/// Streams represent time-series data associated with an activity, such as GPS coordinates, altitude, heart rate, etc.
/// This base class defines the common metadata for all stream types.
/// </remarks>
public class StreamBase
{
    /// <summary>
    /// Gets or sets the type of stream (e.g., "time", "latlng", "distance", "altitude", "velocity_smooth", "heartrate", "cadence", "watts", "temp", "moving", "grade_smooth").
    /// </summary>
    /// <remarks>
    /// The type determines the kind of data contained in the stream.
    /// </remarks>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the series type for the stream (e.g., "distance", "time").
    /// </summary>
    /// <remarks>
    /// Indicates how the stream data is indexed or sampled.
    /// </remarks>
    public string SeriesType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the original size of the stream data.
    /// </summary>
    /// <remarks>
    /// Represents the number of data points in the original stream as returned by the Strava API.
    /// </remarks>
    public long OriginalSize { get; set; }

    /// <summary>
    /// Gets or sets the resolution of the stream (e.g., "low", "medium", "high").
    /// </summary>
    /// <remarks>
    /// The resolution indicates the level of detail in the stream data.
    /// </remarks>
    public string Resolution { get; set; } = string.Empty;
}

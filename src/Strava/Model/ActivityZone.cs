namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a zone of activity, including its type, score, and related metrics.
/// </summary>
/// <remarks>The <see cref="ActivityZone"/> class encapsulates information about a specific activity zone, such as
/// its classification, scoring, and whether it is custom-defined or based on sensor data. This class is typically used
/// to organize and analyze activity data within an application.</remarks>
public class ActivityZone
{
    /// <summary>
    /// Gets or sets the score value associated with the current instance.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// May take one of the following values: heartrate, power
    /// </summary>
    public string Type { get; set; } = "Unknown";

    /// <summary>
    /// Gets or sets a value indicating whether the operation is based on sensor input.
    /// </summary>
    public bool SensorBased { get; set; }

    /// <summary>
    /// Gets or sets the number of points associated with the object.
    /// </summary>
    public int Points { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a custom zone is used.
    /// </summary>
    public bool CustomZones { get; set; }

    /// <summary>
    /// Maximum.
    /// </summary>
    public int Max { get; set; }

    /// <summary>
    /// Gets or sets the collection of power zone ranges used for distribution analysis.
    /// </summary>
    public List<PowerZoneRange> DistributionBuckets { get; set; } = [];
}

namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a zone that contains a collection of distribution buckets and indicates whether it is sensor-based.
/// </summary>
/// <typeparam name="T">The type of elements contained in the distribution buckets.</typeparam>
public class Zone<T>
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation is based on sensor input.
    /// </summary>
    public bool SensorBased { get; set; }

    /// <summary>
    /// Gets or sets the collection of distribution buckets for this zone.
    /// </summary>
    public List<T> DistributionBuckets { get; set; } = [];
}

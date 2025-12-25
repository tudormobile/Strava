namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a stream of activity data as a collection of double-precision values.
/// </summary>
public class ActivityStream : StreamBase
{
    /// <summary>
    /// Gets or sets the collection of data values.
    /// </summary>
    public List<double> Data { get; set; } = [];
}

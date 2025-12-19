namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a range defined by minimum and maximum integer values.
/// </summary>
public class ZoneRange
{
    /// <summary>
    /// Gets the minimum value allowed or supported by the current instance.
    /// </summary>
    public int Min { get; set; }

    /// <summary>
    /// Gets the maximum value allowed or supported by the current instance.
    /// </summary>
    public int Max { get; set; }
}

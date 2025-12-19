namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a range within a power zone.
/// </summary>
public class PowerZoneRange : ZoneRange
{
    /// <summary>
    /// Gets or sets the time spent in the zone, in seconds.
    /// </summary>
    public int Time { get; set; }
}
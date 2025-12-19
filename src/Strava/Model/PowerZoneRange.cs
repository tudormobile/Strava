namespace Tudormobile.Strava.Model;

/// <summary>
/// Gets or sets the time spent in the zone, in seconds.
/// </summary>
public class PowerZoneRange : ZoneRange
{
    /// <summary>
    /// Gets the time spent in the zone in seconds.
    /// </summary>
    public int Time { get; set; }
}
namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a waypoint on a route, containing location, distance, and descriptive information.
/// </summary>
public class Waypoint
{
    /// <summary>
    /// Gets or sets the latitude and longitude coordinates of the waypoint.
    /// </summary>
    public LatLng Latlng { get; set; }

    /// <summary>
    /// Gets or sets the target latitude and longitude coordinates for the waypoint.
    /// </summary>
    public LatLng TargetLatlng { get; set; }

    /// <summary>
    /// Gets or sets the distance from the start of the route to this waypoint in meters.
    /// </summary>
    public float DistanceIntoRoute { get; set; }

    /// <summary>
    /// Gets or sets the description of the waypoint.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of categories associated with the waypoint (e.g., "water", "summit", "danger").
    /// </summary>
    public List<string> Categories { get; set; } = [];

    /// <summary>
    /// Gets or sets the title or name of the waypoint.
    /// </summary>
    public string Title { get; set; } = string.Empty;
}


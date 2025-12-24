namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a Strava route with its associated properties, waypoints, and segments.
/// </summary>
public class Route
{
    /// <summary>
    /// Gets or sets the unique identifier for the route.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the route.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the route.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total distance of the route in meters.
    /// </summary>
    public float Distance { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the route is private.
    /// </summary>
    public bool Private { get; set; }

    /// <summary>
    /// Gets or sets the athlete who created the route.
    /// </summary>
    // TODO: Custom converter to extract the Athlete from a potentially empty string in the JSON response.
    public object Athlete { get; set; } = new();

    /// <summary>
    /// Gets or sets the date and time when the route was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the total elevation gain of the route in meters.
    /// </summary>
    public float ElevationGain { get; set; }

    /// <summary>
    /// Gets or sets the activity type for the route (e.g., 1 for ride, 2 for run).
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// Gets or sets the estimated moving time to complete the route.
    /// </summary>
    public TimeSpan EstimatedMovingTime { get; set; }

    /// <summary>
    /// Gets or sets the collection of waypoints along the route.
    /// </summary>
    public List<Waypoint> Waypoints { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of Strava segments included in the route.
    /// </summary>
    public List<Segment> Segments { get; set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether the route is starred by the authenticated athlete.
    /// </summary>
    public bool Starred { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the route was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the sub-type identifier for the route.
    /// </summary>
    public int SubType { get; set; }

    /// <summary>
    /// Gets or sets the string representation of the route identifier.
    /// </summary>
    public string IdStr { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the map data containing the route's polyline representation.
    /// </summary>
    public SummaryPolylineMap Map { get; set; } = new();

    /// <summary>
    /// Gets or sets the timestamp associated with the route.
    /// </summary>
    public TimeSpan Timestamp { get; set; }
}


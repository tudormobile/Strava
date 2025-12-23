namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a Strava segment, which is a specific section of a route defined by a start and end point.
/// </summary>
public class Segment
{
    /// <summary>
    /// Gets or sets the unique identifier of the segment.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the resource state indicating the level of detail available for this segment.
    /// </summary>
    public ResourceStates ResourceState { get; set; }

    /// <summary>
    /// Gets or sets the name of the segment.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of activity for which this segment is designed (e.g., "Ride", "Run").
    /// </summary>
    public string ActivityType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the distance of the segment in meters.
    /// </summary>
    public float Distance { get; set; }

    /// <summary>
    /// Gets or sets the average grade (steepness) of the segment as a percentage.
    /// </summary>
    /// <remarks>
    /// Positive values indicate uphill, negative values indicate downhill.
    /// </remarks>
    public float AverageGrade { get; set; }

    /// <summary>
    /// Gets or sets the maximum grade (steepest point) of the segment as a percentage.
    /// </summary>
    public float MaximumGrade { get; set; }

    /// <summary>
    /// Gets or sets the highest elevation point on the segment in meters.
    /// </summary>
    public float ElevationHigh { get; set; }

    /// <summary>
    /// Gets or sets the lowest elevation point on the segment in meters.
    /// </summary>
    public float ElevationLow { get; set; }

    /// <summary>
    /// Gets or sets the geographic coordinates of the segment start point.
    /// </summary>
    public LatLng StartLatlng { get; set; }

    /// <summary>
    /// Gets or sets the geographic coordinates of the segment end point.
    /// </summary>
    public LatLng EndLatLng { get; set; }

    /// <summary>
    /// Gets or sets the climb category of the segment.
    /// </summary>
    /// <remarks>
    /// Categories typically range from 0 (no significant climb) to 5 (hors catégorie - beyond categorization).
    /// Higher numbers indicate more difficult climbs.
    /// </remarks>
    public int ClimbCategory { get; set; }

    /// <summary>
    /// Gets or sets the city where the segment is located.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the state or region where the segment is located.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the country where the segment is located.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this segment is private (visible only to the creator).
    /// </summary>
    public bool Private { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this segment has been flagged as hazardous.
    /// </summary>
    public bool Hazardous { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the authenticated athlete has starred (favorited) this segment.
    /// </summary>
    public bool Starred { get; set; }
}

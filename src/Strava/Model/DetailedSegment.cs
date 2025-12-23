namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents detailed information about a Strava segment.
/// </summary>
/// <remarks>
/// A segment is a specific portion of road or trail that athletes can compete on.
/// This class contains comprehensive information including location, elevation, statistics, and athlete performance data.
/// </remarks>
public class DetailedSegment : Segment
{
    /// <summary>
    /// Gets or sets the date and time when the segment was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the segment was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the total elevation gain over the course of the segment in meters.
    /// </summary>
    public float TotalElevationGain { get; set; }

    /// <summary>
    /// Gets or sets the detailed polyline map representation of the segment route.
    /// </summary>
    public DetailedPolylineMap? Map { get; set; }

    /// <summary>
    /// Gets or sets the total number of efforts (attempts) recorded on this segment.
    /// </summary>
    public int EffortCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of unique athletes who have attempted this segment.
    /// </summary>
    public int AthleteCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of athletes who have starred (favorited) this segment.
    /// </summary>
    public int StarCount { get; set; }

    /// <summary>
    /// Gets or sets the authenticated athlete's personal statistics for this segment.
    /// </summary>
    /// <remarks>
    /// Contains the athlete's personal record (PR), effort count, and related performance data.
    /// </remarks>
    public SummaryPRSegmentEffort? AthleteSegmentStats { get; set; }
}

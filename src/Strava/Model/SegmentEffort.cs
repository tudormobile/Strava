namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents an athlete's attempt at a segment during an activity.
/// </summary>
/// <remarks>
/// A segment effort captures the performance details of an athlete completing a specific segment,
/// including timing, distance, and references to the activity and segment involved.
/// </remarks>
public class SegmentEffort
{
    /// <summary>
    /// Gets or sets the unique identifier of the segment effort.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the resource state indicating the level of detail available for this segment effort.
    /// </summary>
    public ResourceStates ResourceState { get; set; }

    /// <summary>
    /// Gets or sets the name of the segment effort.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the summary of the activity during which this segment effort occurred.
    /// </summary>
    public SummaryActivity? Activity { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the athlete who completed this segment effort.
    /// </summary>
    public AthleteId Athlete { get; set; } = new();

    /// <summary>
    /// Gets or sets the total elapsed time for the segment effort, including any stopped time.
    /// </summary>
    public TimeSpan ElapsedTime { get; set; }

    /// <summary>
    /// Gets or sets the moving time for the segment effort, excluding any stopped time.
    /// </summary>
    public TimeSpan MovingTime { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the segment effort started in UTC.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the segment effort started in local time.
    /// </summary>
    public DateTime StartDateLocal { get; set; }

    /// <summary>
    /// Gets or sets the distance of the segment effort in meters.
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Gets or sets the index of the start point in the activity's data stream.
    /// </summary>
    public int StartIndex { get; set; }

    /// <summary>
    /// Gets or sets the index of the end point in the activity's data stream.
    /// </summary>
    public int EndIndex { get; set; }

    /// <summary>
    /// Gets or sets the segment that this effort belongs to.
    /// </summary>
    public Segment? Segment { get; set; }
}


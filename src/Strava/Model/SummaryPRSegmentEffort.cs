namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents an athlete's personal record (PR) statistics for a segment.
/// </summary>
/// <remarks>
/// Contains information about the athlete's best performance on a segment, including
/// the fastest time achieved and the number of attempts.
/// </remarks>
public class SummaryPRSegmentEffort
{
    /// <summary>
    /// Gets or sets the elapsed time of the athlete's personal record (fastest completion) on this segment.
    /// </summary>
    public TimeSpan PrElapsedTime { get; set; }

    /// <summary>
    /// Gets or sets the date when the personal record was achieved.
    /// </summary>
    public DateOnly PrDate { get; set; }

    /// <summary>
    /// Gets or sets the total number of times the athlete has attempted this segment.
    /// </summary>
    public int EffortCount { get; set; }
}

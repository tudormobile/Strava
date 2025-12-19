namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents aggregated totals for a collection of activities of a specific type (e.g., rides, runs, swims).
/// </summary>
/// <remarks>
/// This class is used to summarize multiple activities into key metrics including distance, time, elevation, and achievements.
/// It is typically used in activity statistics to show totals over different time periods (recent, year-to-date, all-time).
/// </remarks>
public class ActivityTotal
{
    /// <summary>
    /// Gets or sets the total distance covered across all activities, in meters.
    /// </summary>
    public float Distance { get; set; }

    /// <summary>
    /// Gets or sets the total number of achievements earned across all activities.
    /// </summary>
    /// <remarks>
    /// Achievements include personal records (PRs), segment achievements, and other milestones.
    /// </remarks>
    public int AchievementCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of activities in this aggregate.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the total elapsed time across all activities, in seconds.
    /// </summary>
    /// <remarks>
    /// Elapsed time includes all pauses and stops during the activity.
    /// </remarks>
    public int ElapsedTime { get; set; }

    /// <summary>
    /// Gets or sets the total elevation gain across all activities, in meters.
    /// </summary>
    public float ElevationGain { get; set; }

    /// <summary>
    /// Gets or sets the total moving time across all activities, in seconds.
    /// </summary>
    /// <remarks>
    /// Moving time excludes pauses and stops, representing only the time spent in motion.
    /// </remarks>
    public int MovingTime { get; set; }
}

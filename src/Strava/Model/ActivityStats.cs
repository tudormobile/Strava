namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents comprehensive activity statistics for an athlete across different sports and time periods.
/// </summary>
/// <remarks>
/// This class aggregates activity totals for rides, runs, and swims across three time periods:
/// recent activities, year-to-date (YTD), and all-time totals.
/// </remarks>
public class ActivityStats
{
    /// <summary>
    /// Gets or sets the distance of the athlete's longest ride, in meters.
    /// </summary>
    public float BiggestRideDistance { get; set; }

    /// <summary>
    /// Gets or sets the elevation gain of the athlete's biggest climb, in meters.
    /// </summary>
    public float BiggestClimbElevationGain { get; set; }

    /// <summary>
    /// Gets or sets the totals for recent ride activities.
    /// </summary>
    /// <remarks>
    /// "Recent" typically refers to the last 4 weeks of activity.
    /// This property is null if the athlete has no recent ride activities.
    /// </remarks>
    public ActivityTotal? RecentRideTotals { get; set; }

    /// <summary>
    /// Gets or sets the totals for recent run activities.
    /// </summary>
    /// <remarks>
    /// "Recent" typically refers to the last 4 weeks of activity.
    /// This property is null if the athlete has no recent run activities.
    /// </remarks>
    public ActivityTotal? RecentRunTotals { get; set; }

    /// <summary>
    /// Gets or sets the totals for recent swim activities.
    /// </summary>
    /// <remarks>
    /// "Recent" typically refers to the last 4 weeks of activity.
    /// This property is null if the athlete has no recent swim activities.
    /// </remarks>
    public ActivityTotal? RecentSwimTotals { get; set; }

    /// <summary>
    /// Gets or sets the totals for year-to-date ride activities.
    /// </summary>
    /// <remarks>
    /// Year-to-date (YTD) refers to activities from January 1st of the current year to now.
    /// This property is null if the athlete has no YTD ride activities.
    /// </remarks>
    public ActivityTotal? YtdRideTotals { get; set; }

    /// <summary>
    /// Gets or sets the totals for year-to-date run activities.
    /// </summary>
    /// <remarks>
    /// Year-to-date (YTD) refers to activities from January 1st of the current year to now.
    /// This property is null if the athlete has no YTD run activities.
    /// </remarks>
    public ActivityTotal? YtdRunTotals { get; set; }

    /// <summary>
    /// Gets or sets the totals for year-to-date swim activities.
    /// </summary>
    /// <remarks>
    /// Year-to-date (YTD) refers to activities from January 1st of the current year to now.
    /// This property is null if the athlete has no YTD swim activities.
    /// </remarks>
    public ActivityTotal? YtdSwimTotals { get; set; }

    /// <summary>
    /// Gets or sets the totals for all-time ride activities.
    /// </summary>
    /// <remarks>
    /// All-time refers to the athlete's entire activity history on Strava.
    /// This property is null if the athlete has no ride activities.
    /// </remarks>
    public ActivityTotal? AllRideTotals { get; set; }

    /// <summary>
    /// Gets or sets the totals for all-time run activities.
    /// </summary>
    /// <remarks>
    /// All-time refers to the athlete's entire activity history on Strava.
    /// This property is null if the athlete has no run activities.
    /// </remarks>
    public ActivityTotal? AllRunTotals { get; set; }

    /// <summary>
    /// Gets or sets the totals for all-time swim activities.
    /// </summary>
    /// <remarks>
    /// All-time refers to the athlete's entire activity history on Strava.
    /// This property is null if the athlete has no swim activities.
    /// </remarks>
    public ActivityTotal? AllSwimTotals { get; set; }
}

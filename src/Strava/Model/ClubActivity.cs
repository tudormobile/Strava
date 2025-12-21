namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a Club Activity in the Strava API.
/// </summary>
public class ClubActivity
{
    /// <summary>
    /// Gets or sets the meta information about the athlete who performed the activity.
    /// </summary>
    public SummaryAthlete Athlete { get; set; } = new();

    /// <summary>
    /// Gets or sets the name of the activity.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the distance of the activity in meters.
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Gets or sets the moving time of the activity in seconds.
    /// </summary>
    public long MovingTime { get; set; }

    /// <summary>
    /// Gets or sets the elapsed time of the activity in seconds.
    /// </summary>
    public long ElapsedTime { get; set; }

    /// <summary>
    /// Gets or sets the total elevation gain of the activity in meters.
    /// </summary>
    public double TotalElevationGain { get; set; }

    /// <summary>
    /// Gets or sets the type of the activity (deprecated, use SportType instead).
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the sport type of the activity.
    /// </summary>
    public SportTypes SportType { get; set; }

    /// <summary>
    /// Gets or sets the workout type. This value is activity-specific and indicates the nature of the workout.
    /// </summary>
    public int? WorkoutType { get; set; }
}

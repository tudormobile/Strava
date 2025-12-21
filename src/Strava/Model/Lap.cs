namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a lap within a Strava activity, containing performance metrics and timing information.
/// </summary>
public class Lap
{
    /// <summary>
    /// Gets or sets the unique identifier for the lap.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the lap.
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets metadata about the activity this lap belongs to.
    /// </summary>
    public MetaActivity Activity { get; set; } = new MetaActivity();

    /// <summary>
    /// Gets or sets metadata about the athlete who performed this lap.
    /// </summary>
    public MetaAthlete Athlete { get; set; } = new MetaAthlete();

    /// <summary>
    /// Gets or sets the average cadence (steps per minute for running, or RPM for cycling) during the lap.
    /// </summary>
    public double AverageCadence { get; set; }

    /// <summary>
    /// Gets or sets the average speed in meters per second during the lap.
    /// </summary>
    public double AverageSpeed { get; set; }

    /// <summary>
    /// Gets or sets the distance covered in meters during the lap.
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Gets or sets the total elapsed time for the lap, including rest periods.
    /// </summary>
    public TimeSpan ElapsedTime { get; set; }

    /// <summary>
    /// Gets or sets the starting index of the lap in the activity's data stream.
    /// </summary>
    public int StartIndex { get; set; }

    /// <summary>
    /// Gets or sets the ending index of the lap in the activity's data stream.
    /// </summary>
    public int EndIndex { get; set; }

    /// <summary>
    /// Gets or sets the sequential index of this lap within the activity.
    /// </summary>
    public int LapIndex { get; set; }

    /// <summary>
    /// Gets or sets the maximum speed in meters per second achieved during the lap.
    /// </summary>
    public double MaxSpeed { get; set; }

    /// <summary>
    /// Gets or sets the time spent actively moving during the lap, excluding rest periods.
    /// </summary>
    public TimeSpan MovingTime { get; set; }

    /// <summary>
    /// Gets or sets the pace zone identifier for the lap.
    /// </summary>
    public int PaceZone { get; set; }

    /// <summary>
    /// Gets or sets the split number for the lap.
    /// </summary>
    public int Split { get; set; }

    /// <summary>
    /// Gets or sets the start date and time of the lap in UTC.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the start date and time of the lap in the local timezone.
    /// </summary>
    public DateTime StartDateLocal { get; set; }

    /// <summary>
    /// Gets or sets the total elevation gain in meters during the lap.
    /// </summary>
    public float TotalElevationGain { get; set; }
}

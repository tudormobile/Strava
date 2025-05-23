namespace Tudormobile.Strava.Model;

/// <summary>
/// A detailed (full) activity record.
/// </summary>
public class DetailedActivity : SummaryActivity
{
    /// <summary>
    /// Gets or sets a value indicating whether this activity is a commute.
    /// </summary>
    public bool Commute { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this activity was recorded on a training machine.
    /// </summary>
    public bool Trainer { get; set; }

    /// <summary>
    /// Gets or sets a description of the activity.
    /// </summary>
    public string Description { get; set; } = String.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="DetailedActivity"/> class using the data from a <see
    /// cref="SummaryActivity"/> instance.
    /// </summary>
    /// <remarks>This constructor copies the values of <see cref="SummaryActivity.TotalElevationGain"/>, <see
    /// cref="SummaryActivity.StartDate"/>,  and <see cref="SummaryActivity.SportType"/> from the provided <paramref
    /// name="summaryActivity"/> to initialize the corresponding properties  of the <see cref="DetailedActivity"/>
    /// instance.</remarks>
    /// <param name="summaryActivity">The <see cref="SummaryActivity"/> instance containing the data to initialize the <see cref="DetailedActivity"/>.</param>
    public DetailedActivity(SummaryActivity summaryActivity)
    {
        this.TotalElevationGain = summaryActivity.TotalElevationGain;
        this.StartDate = summaryActivity.StartDate;
        this.SportType = summaryActivity.SportType;
        this.Athlete = summaryActivity.Athlete;
        this.Name = summaryActivity.Name;
        this.Distance = summaryActivity.Distance;
        this.MovingTime = summaryActivity.MovingTime;
        this.ElapsedTime = summaryActivity.ElapsedTime;
        this.Type = summaryActivity.Type;
        this.WorkoutType = summaryActivity.WorkoutType;
        this.Id = summaryActivity.Id;
        this.AverageSpeed = summaryActivity.AverageSpeed;
        this.MaxSpeed = summaryActivity.MaxSpeed;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DetailedActivity"/> class.
    /// </summary>
    /// <remarks>This constructor creates a default instance of the <see cref="DetailedActivity"/> class. Use
    /// this constructor to create an object with default values.</remarks>
    public DetailedActivity()
    {

    }
}

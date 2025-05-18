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
}

namespace Tudormobile.Strava.Model;

/// <summary>
/// An updatable activity record.
/// </summary>
public class UpdatableActivity
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
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the name associated with the activity.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the type of sport associated with the activity.
    /// </summary>
    public SportTypes SportType { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatableActivity"/> class with the specified name and description.
    /// </summary>
    /// <param name="name">The name of the activity. If null, an empty string is used.</param>
    /// <param name="description">The description of the activity. If null, an empty string is used.</param>
    /// <param name="sportType">The type of sport associated with the activity. If null, the default sport type is used.</param>
    public UpdatableActivity(string? name = null, string? description = null, SportTypes? sportType = null)
    {
        Name = name ?? string.Empty;
        Description = description ?? string.Empty;
        SportType = sportType ?? default;
    }

}

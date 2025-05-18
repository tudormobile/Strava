using System.ComponentModel;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.UI.ViewModels;

/// <summary>
/// Represents a view model for an activity, providing access to activity details and allowing updates to certain
/// properties.
/// </summary>
/// <remarks>This class serves as a bridge between the activity data model and the user interface, exposing
/// activity details as read-only properties and allowing updates to specific fields such as the activity name,
/// description, and type.</remarks>
public class ActivityViewModel : INotifyPropertyChanged
{
    private DetailedActivity _activity;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    /// <remarks>This event is typically used to notify subscribers that a property value has been updated. It
    /// is commonly implemented in classes that support data binding or need to signal changes to property
    /// values.</remarks>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event to notify listeners that a property value has changed.
    /// </summary>
    /// <remarks>This method is typically called by property setters to notify data-binding clients or other
    /// listeners of property changes. Subclasses can override this method to provide additional behavior when a
    /// property changes.</remarks>
    /// <param name="propertyName">The name of the property that changed. This value cannot be <see langword="null"/> or empty.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Read-only properties
    /// <summary>
    /// The unique identifier of the activity
    /// </summary>
    public long Id => _activity.Id;

    /// <summary>
    /// Athlete.
    /// </summary>
    public AthleteId Athlete => _activity.Athlete;

    /// <summary>
    /// The activity's distance, in meters
    /// </summary>
    public double Distance => _activity.Distance;

    /// <summary>
    /// The activity's moving time, in seconds
    /// </summary>
    public double MovingTime => _activity.MovingTime;

    /// <summary>
    /// The activity's elapsed time, in seconds
    /// </summary>
    public double ElapsedTime => _activity.ElapsedTime;

    /// <summary>
    /// The activity's total elevation gain.
    /// </summary>
    public double TotalElevationGain => _activity.TotalElevationGain;

    /// <summary>
    /// The time at which the activity was started.
    /// </summary>
    public DateTime StartDate => _activity.StartDate;

    /// <summary>
    /// The activity's average speed, in meters per second
    /// </summary>
    public double AverageSpeed => _activity.AverageSpeed;

    /// <summary>
    /// The activity's max speed, in meters per second
    /// </summary>
    public double MaxSpeed => _activity.MaxSpeed;

    // Updatable properties

    /// <summary>
    /// The name of the activity
    /// </summary>
    public string Name
    {
        get => _activity.Name;
        set
        {
            _activity.Name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this activity is a commute.
    /// </summary>
    public bool Commute
    {
        get => _activity.Commute;
        set
        {
            _activity.Commute = value;
            OnPropertyChanged(nameof(Commute));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this activity was recorded on a training machine.
    /// </summary>
    public bool Trainer
    {
        get => _activity.Trainer;
        set
        {
            _activity.Trainer = value;
            OnPropertyChanged(nameof(Trainer));
        }
    }

    /// <summary>
    /// Gets or sets a description of the activity.
    /// </summary>
    public string Description
    {
        get => _activity.Description;
        set
        {
            _activity.Description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    /// <summary>
    /// Gets or sets the type of sport associated with the activity.
    /// </summary>
    public SportTypes SportType
    {
        get
        {
            if (Enum.TryParse<SportTypes>(_activity.SportType, out var sportType))
            {
                return sportType;
            }
            return SportTypes.Unknown;
        }
        set
        {
            _activity.SportType = value.ToString();
            OnPropertyChanged(nameof(SportType));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityViewModel"/> class with the specified activity details.
    /// </summary>
    /// <param name="activity">The detailed activity data used to initialize the view model. Cannot be null.</param>
    public ActivityViewModel(DetailedActivity activity)
    {
        _activity = activity;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityViewModel"/> class with a default <see cref="DetailedActivity"/>.
    /// </summary>
    public ActivityViewModel() : this(new DetailedActivity()) { }
}

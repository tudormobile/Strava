using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Client;

/// <summary>
/// Wrapper around a <see cref="SummaryActivity"/> that provides a more convenient
/// .NET-friendly API surface for consumers of the client library.
/// </summary>
public class StravaActivity
{
    private readonly SummaryActivity _summaryActivity;

    /// <summary>
    /// Initializes a new instance of the <see cref="StravaActivity"/> class.
    /// </summary>
    /// <param name="summaryActivity">The underlying <see cref="SummaryActivity"/> instance to wrap. Must not be <c>null</c>.</param>
    public StravaActivity(SummaryActivity summaryActivity)
    {
        _summaryActivity = summaryActivity;
    }

    /// <summary>
    /// Gets the underlying <see cref="SummaryActivity"/> instance.
    /// </summary>
    internal SummaryActivity SummaryActivity => _summaryActivity;

    /// <summary>
    /// Gets the activity identifier.
    /// </summary>
    public long Id => _summaryActivity.Id;

    /// <summary>
    /// Gets the activity name.
    /// </summary>
    public string Name => _summaryActivity.Name;

    /// <summary>
    /// Gets the activity distance in meters.
    /// </summary>
    public double Distance => _summaryActivity.Distance;

    /// <summary>
    /// Gets the total elevation gain for the activity in meters.
    /// </summary>
    public double ElevationGain => _summaryActivity.TotalElevationGain;

    /// <summary>
    /// Gets the activity start date and time (UTC when provided by the API).
    /// </summary>
    public DateTime StartDate => _summaryActivity.StartDate;

    /// <summary>
    /// Gets the activity moving time as a <see cref="TimeSpan"/>. The value is converted
    /// from the seconds reported on the underlying <see cref="SummaryActivity"/>.
    /// </summary>
    public TimeSpan MovingTime => TimeSpan.FromSeconds(_summaryActivity.MovingTime);

    /// <summary>
    /// Gets the activity elapsed time as a <see cref="TimeSpan"/>. The value is converted
    /// from the seconds reported on the underlying <see cref="SummaryActivity"/>.
    /// </summary>
    public TimeSpan ElapsedTime => TimeSpan.FromSeconds(_summaryActivity.ElapsedTime);

    /// <summary>
    /// Gets the activity average speed in meters per second.
    /// </summary>
    public double AverageSpeed => _summaryActivity.AverageSpeed;

    /// <summary>
    /// Gets the activity maximum speed in meters per second.
    /// </summary>
    public double MaximumSpeed => _summaryActivity.MaxSpeed;

    /// <summary>
    /// Gets the sport type for the activity. If the underlying <see cref="SummaryActivity.SportType"/>
    /// is not present or empty the value will be deduced from other available properties.
    /// </summary>
    public string SportType => string.IsNullOrWhiteSpace(_summaryActivity.SportType)
                ? DigForSportType()
                : _summaryActivity.SportType;

    /// <summary>
    /// Attempts to deduce the sport type from alternate properties on the underlying
    /// <see cref="SummaryActivity"/> when an explicit sport type is not provided.
    /// </summary>
    /// <returns>
    /// A string representation of the deduced sport type or "Unknown" when deduction fails.
    /// </returns>
    private string DigForSportType()
    {
        // Attempt to deduce sport type from other properties
        if (!string.IsNullOrWhiteSpace(_summaryActivity.Type))
        {
            return _summaryActivity.Type;
        }
        var sportType = (SportTypes)(_summaryActivity.WorkoutType ?? 0);
        return Enum.IsDefined<SportTypes>(sportType)
            ? sportType.ToString()
            : "Unknown";
    }

    /// <summary>
    /// Returns a human friendly representation of the activity.
    /// </summary>
    /// <returns>A string that represents the current activity.</returns>
    public override string ToString() => this.AsNameValuePairString();
}

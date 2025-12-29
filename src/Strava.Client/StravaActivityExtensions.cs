namespace Tudormobile.Strava.Client;

/// <summary>
/// Extension methods for <see cref="StravaActivity"/> instances.
/// </summary>
public static class StravaActivityExtensions
{
    /// <summary>
    /// Formats the activity as a comma-separated name/value pair string suitable for logging or display.
    /// </summary>
    /// <param name="activity">The <see cref="StravaActivity"/> instance to format. Must not be <c>null</c>.</param>
    /// <returns>
    /// A string containing property name/value pairs for the provided activity.
    /// </returns>
    public static string AsNameValuePairString(this StravaActivity activity)
    {
        return string.Format("Id={0},StartDate={1},Name=\"{2}\",Distance={3:F2}m,ElevationGain={4:F2}m,MovingTime={5},ElapsedTime={6},AverageSpeed={7:F2}m/s,MaximumSpeed={8:F2}m/s,SportType=\"{9}\"",
            activity.Id,
            activity.StartDate.ToLocalTime(),
            activity.Name,
            activity.Distance,
            activity.ElevationGain,
            activity.MovingTime,
            activity.ElapsedTime,
            activity.AverageSpeed,
            activity.MaximumSpeed,
            activity.SportType
            );
    }

    /// <summary>
    /// Converts the specified Strava activity to a comma-separated values (CSV) string representation.
    /// </summary>
    /// <remarks>The returned CSV string includes the activity's ID, start date (in local time), name,
    /// distance, elevation gain, moving time, elapsed time, average speed, maximum speed, and sport type, in that
    /// order. String values are quoted to ensure correct CSV formatting.</remarks>
    /// <param name="activity">The StravaActivity instance to convert to CSV format. Cannot be null.</param>
    /// <returns>A CSV-formatted string containing the activity's properties. String fields are enclosed in double quotes.</returns>
    public static string AsCsvString(this StravaActivity activity)
    {
        return string.Format("{0},{1:s},{2},{3},{4:F2},{5:F2},{6},{7},{8:F2},{9:F2}",
            activity.Id,
            activity.StartDate.ToLocalTime(),
            activity.SportType,
            activity.Name.Replace(",", " "),
            activity.Distance,
            activity.ElevationGain,
            activity.MovingTime,
            activity.ElapsedTime,
            activity.AverageSpeed,
            activity.MaximumSpeed
            );
    }

    /// <summary>
    /// Returns the CSV header line corresponding to the properties of a Strava activity.
    /// </summary>
    /// <param name="activity">The Strava activity instance for which to generate the CSV header line.</param>
    /// <returns>A comma-separated string containing the column headers: Id, StartDate, Name, Distance, ElevationGain,
    /// MovingTime, ElapsedTime, AverageSpeed, MaximumSpeed, and SportType.</returns>
    public static string GetCsvHeaderLine(this StravaActivity activity)
    {
        return "Id,StartDate,SportType,Name,Distance,ElevationGain,MovingTime,ElapsedTime,AverageSpeed,MaximumSpeed";
    }
}

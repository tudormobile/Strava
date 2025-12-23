namespace Tudormobile.Strava.Api;


/// <summary>
/// Extensions methods that enable access to the Strava APIs.
/// </summary>
/// <remarks>
/// You must include the Tudormobile.Strava.Api namespace in order to access the Strava APIs. Extensions
/// are added to the StravaSession class, which is the main entry point for accessing the Strava APIs.
/// Access to the current authenticated athlete is provided by all API methods.
/// </remarks>
public static class ApiExtensions
{
    /// <summary>
    /// Creates or returns the existing ActivitiesApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Activities API.</returns>
    public static IActivitiesApi ActivitiesApi(this StravaSession session) => (IActivitiesApi)session.StravaApi();

    /// <summary>
    /// Creates or returns the existing AthletesApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Athletes API.</returns>
    public static IAthletesApi AthletesApi(this StravaSession session) => (IAthletesApi)session.StravaApi();

    /// <summary>
    /// Creates or returns the existing ClubsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Clubs API.</returns>
    public static IClubsApi ClubsApi(this StravaSession session) => (IClubsApi)session.StravaApi();

    /// <summary>
    /// Creates or returns the existing GearsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Gears API.</returns>
    public static IGearsApi GearsApi(this StravaSession session) => (IGearsApi)session.StravaApi();

    /// <summary>
    /// Creates or returns the existing RoutesApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Routes API.</returns>
    public static IRoutesApi RoutesApi(this StravaSession session) => throw new NotImplementedException("Routes API not implemented yet.");

    /// <summary>
    /// Creates or returns the existing SegmentsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Segments API.</returns>
    public static ISegmentsApi SegmentsApi(this StravaSession session) => (ISegmentsApi)session.StravaApi();

    /// <summary>
    /// Creates or returns the existing StreamApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Streams API.</returns>
    public static IStreamsApi StreamApi(this StravaSession session) => throw new NotImplementedException("Streams API not implemented yet.");

    /// <summary>
    /// Creates or returns the existing UploadsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Uploads API.</returns>
    public static IUploadsApi UploadsApi(this StravaSession session) => throw new NotImplementedException("Uploads API not implemented yet.");

    /// <summary>
    /// Appends query parameters to a URI string.
    /// </summary>
    /// <param name="uriString">The base URI string to which query parameters will be added.</param>
    /// <param name="queryParameters">A collection of key-value pairs representing the query parameters to add. Values that are <c>null</c> are ignored.</param>
    /// <returns>The URI string with the appended query parameters.</returns>
    public static string AddQueryToUriString(string uriString, IEnumerable<(string, object?)> queryParameters)
    {
        var queryParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
        foreach (var (key, value) in queryParameters)
        {
            if (value is DateTime dt)
            {
                // Strava API expects date-time values in ISO 8601 format
                queryParams.Add(key, dt.ToString("o"));
            }
            else if (value is Array arr)
            {
                // arrays are represented as multiple query parameters with the same key
                foreach (var item in arr)
                {
                    if (item != null)
                    {
                        queryParams.Add(key, item.ToString());
                    }
                }
            }
            else
            {
                // just a string representation
                if (!string.IsNullOrWhiteSpace(value?.ToString()))
                {
                    queryParams.Add(key, value.ToString());
                }
            }
        }
        var query = queryParams.ToString()!;
        return uriString + (string.IsNullOrWhiteSpace(query) ? string.Empty : "?" + query);
    }
}

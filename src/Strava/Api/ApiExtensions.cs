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
    public static IActivitiesApi ActivitiesApi(this StravaSession session) => session._api ??= new StravaApiImpl(session);

    /// <summary>
    /// Creates or returns the existing AthletesApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Athletes API.</returns>
    public static IAthletesApi AthletesApi(this StravaSession session) => session._api ??= new StravaApiImpl(session);

    /// <summary>
    /// Creates or returns the existing ClubsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Clubs API.</returns>
    public static IClubsApi ClubsApi(this StravaSession session) => throw new NotImplementedException("Clubs API not implemented yet.");

    /// <summary>
    /// Creates or returns the existing GearsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Gears API.</returns>
    public static IGearsApi GearsApi(this StravaSession session) => throw new NotImplementedException("Gears API not implemented yet.");

    /// <summary>
    /// Creates or returns the existing RoutesApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Routes API.</returns>
    public static IRoutesApi RoutesApi(this StravaSession session) => throw new NotImplementedException("Routes API not implemented yet.");

    /// <summary>
    /// Creates or returns the existing SegmentsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Segments API.</returns>
    public static ISegmentsApi SegmentsApi(this StravaSession session) => throw new NotImplementedException("Segments API not implemented yet.");

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

}

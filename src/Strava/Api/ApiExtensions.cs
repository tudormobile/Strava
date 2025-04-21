namespace Tudormobile.Strava.Api;


/// <summary>
/// Extensions to access Strava APIs.
/// </summary>
public static class ApiExtensions
{
    /// <summary>
    /// Creates or returns existing API interface.
    /// </summary>
    /// <returns></returns>
    public static IStravaApi CreateApi(this StravaSession session) => session._api ??= new StravaApiImpl(session);


}

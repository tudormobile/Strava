namespace Tudormobile.Strava.Service;

/// <summary>
/// Provides services for interacting with Strava using an authenticated session.
/// </summary>
public class StravaService
{
    private readonly StravaSession _session;

    /// <summary>
    /// Initializes a new instance of the <see cref="StravaService"/> class with the specified session.
    /// </summary>
    /// <param name="session">The authenticated Strava session.</param>
    public StravaService(StravaSession session)
    {
        _session = session;
    }
}

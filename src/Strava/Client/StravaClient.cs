namespace Tudormobile.Strava.Client;

/// <summary>
/// Provides methods for interacting with the Strava API using an authenticated session.
/// </summary>
public class StravaClient
{
    private readonly StravaSession _session;

    /// <summary>
    /// Initializes a new instance of the <see cref="StravaClient"/> class with the specified session.
    /// </summary>
    /// <param name="session">The authenticated Strava session.</param>
    public StravaClient(StravaSession session)
    {
        _session = session;
    }
}

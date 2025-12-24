using Tudormobile.Strava.Api;

namespace Tudormobile.Strava.Client;

/// <summary>
/// Represents a Strava API client with authentication state and activity operations.
/// </summary>
public interface IStravaClient
    : IActivitiesApi,
    IAthletesApi,
    IClubsApi,
    IGearsApi,
    //IRoutesApi,
    //ISegmentsApi,
    IStreamsApi,
    IUploadsApi
{
    /// <summary>
    /// Gets a value indicating whether the client is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }
}
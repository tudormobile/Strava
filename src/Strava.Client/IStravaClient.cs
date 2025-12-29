using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Client;

/// <summary>
/// Represents a Strava API client with authentication state and activity operations.
/// </summary>
public interface IStravaClient
{
    /// <summary>
    /// Gets a value indicating whether the client is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Asynchronously refreshes the Strava authorization, obtaining a new access token if necessary.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the refresh operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ApiResult{StravaAuthorization}"/> with the updated authorization information.</returns>
    Task<ApiResult<StravaAuthorization>> RefreshAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the athlete currently logged in context, if any.
    /// </summary>
    Athlete? Athlete { get; }

    /// <summary>
    /// Gets the current Strava API session associated with this instance.
    /// </summary>
    StravaSession Session { get; }

    /// <summary>
    /// Gets an interface for managing and accessing activity-related operations.
    /// </summary>
    /// <returns>An <see cref="IActivitiesApi"/> instance that provides methods to interact with activities.</returns>
    public IActivitiesApi ActivitiesApi();

    /// <summary>
    /// Creates or returns the existing AthletesApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Athletes API.</returns>
    public IAthletesApi AthletesApi();

    /// <summary>
    /// Creates or returns the existing ClubsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Clubs API.</returns>
    public IClubsApi ClubsApi();

    /// <summary>
    /// Creates or returns the existing GearsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Gears API.</returns>
    public IGearsApi GearsApi();

    /// <summary>
    /// Creates or returns the existing RoutesApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Routes API.</returns>
    public IRoutesApi RoutesApi();

    /// <summary>
    /// Creates or returns the existing SegmentsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Segments API.</returns>
    public ISegmentsApi SegmentsApi();

    /// <summary>
    /// Creates or returns the existing StreamApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Streams API.</returns>
    public IStreamsApi StreamsApi();

    /// <summary>
    /// Creates or returns the existing UploadsApi interface.
    /// </summary>
    /// <returns>Interface for accessing the Strava Uploads API.</returns>
    public IUploadsApi UploadsApi();
}
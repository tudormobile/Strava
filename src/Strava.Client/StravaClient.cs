using Microsoft.Extensions.Logging;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Client;

/// <summary>
/// Provides a client for interacting with the Strava API, supporting authentication and various API operations.
/// </summary>
public class StravaClient : IStravaClient
{
    private readonly ILogger _logger;
    private readonly StravaSession _session;

    /// <summary>
    /// Initializes a new instance of the StravaClient class using the specified HTTP client and client credentials.
    /// </summary>
    /// <remarks>This constructor is intended for scenarios where only the basic client credentials are
    /// available. For advanced configuration, use an overload that accepts additional parameters such as a logger or
    /// access tokens.</remarks>
    /// <param name="httpClient">The HttpClient instance to use for sending HTTP requests to the Strava API. Cannot be null.</param>
    /// <param name="clientId">The client identifier assigned by Strava for API access. Cannot be null or empty.</param>
    /// <param name="clientSecret">The client secret associated with the Strava application. Cannot be null or empty.</param>
    public StravaClient(HttpClient httpClient, string clientId, string clientSecret)
        : this(httpClient: httpClient, logger: null, clientId, clientSecret, accessToken: null, refreshToken: null) { }

    /// <summary>
    /// Initializes a new instance of the StravaClient class using an IHttpClientFactory to create the HTTP client,
    /// along with the specified logger and options.
    /// </summary>
    /// <remarks>This constructor is useful when integrating with dependency injection frameworks that provide
    /// IHttpClientFactory for managing HttpClient lifetimes.</remarks>
    /// <param name="httpClientFactory">The factory used to create an HttpClient instance for communicating with the Strava API. Cannot be null.</param>
    /// <param name="logger">The logger used to record diagnostic and operational information. Cannot be null.</param>
    /// <param name="options">The configuration options for the StravaClient. Cannot be null.</param>
    public StravaClient(IHttpClientFactory httpClientFactory, ILogger<StravaClient> logger, StravaOptions options)
        : this(httpClientFactory.CreateClient(nameof(StravaClient)), logger, options) { }

    /// <summary>
    /// Initializes a new instance of the StravaClient class using the specified HTTP client, logger, and Strava API
    /// options.
    /// </summary>
    /// <remarks>This constructor simplifies initialization by accepting a StravaOptions object containing all
    /// required credentials and tokens. The provided HttpClient should be configured for use with the Strava API and is
    /// not disposed by the StravaClient.</remarks>
    /// <param name="httpClient">The HTTP client instance used to send requests to the Strava API. Must not be null.</param>
    /// <param name="logger">The logger used to record diagnostic and operational information for the StravaClient. Must not be null.</param>
    /// <param name="options">The options containing Strava API credentials and tokens. Must not be null.</param>
    public StravaClient(HttpClient httpClient, ILogger<StravaClient> logger, StravaOptions options)
        : this(httpClient, logger, options.ClientId, options.ClientSecret, options.AccessToken, options.RefreshToken) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="StravaClient"/> class with the specified HTTP client, logger, client ID, client secret, access token, and refresh token.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for API requests.</param>
    /// <param name="logger">The logger instance for logging operations. Can be null.</param>
    /// <param name="clientId">The Strava API client ID.</param>
    /// <param name="clientSecret">The Strava API client secret.</param>
    /// <param name="accessToken">The access token for authentication. Optional.</param>
    /// <param name="refreshToken">The refresh token for authentication. Optional.</param>
    public StravaClient(
        HttpClient httpClient,
        ILogger? logger,
        string clientId,
        string clientSecret,
        string? accessToken = null,
        string? refreshToken = null
        ) : this(httpClient, logger, new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken)) { }

    /// <summary>
    /// Initializes a new instance of the StravaClient class with the specified HTTP client, logger, and Strava
    /// authorization credentials.
    /// </summary>
    /// <param name="httpClient">The HTTP client used to send requests to the Strava API. Cannot be null.</param>
    /// <param name="logger">The logger used for diagnostic and error messages. If null, a no-op logger is used.</param>
    /// <param name="stravaAuthorization">The Strava authorization credentials, including client ID and client secret. Cannot be null, and both ClientId
    /// and ClientSecret must be non-empty strings.</param>
    /// <exception cref="ArgumentNullException">Thrown if httpClient is null, or if stravaAuthorization is null, or if stravaAuthorization.ClientId or
    /// stravaAuthorization.ClientSecret is null.</exception>
    /// <exception cref="ArgumentException">Thrown if stravaAuthorization.ClientId or stravaAuthorization.ClientSecret is an empty or whitespace string.</exception>
    public StravaClient(
        HttpClient httpClient,
        ILogger? logger,
        StravaAuthorization stravaAuthorization
        )
    {
        // Validate required parameters
        ArgumentNullException.ThrowIfNull(httpClient);
        if (stravaAuthorization is null
            || stravaAuthorization.ClientId == null
            || stravaAuthorization.ClientSecret == null) throw new ArgumentNullException(nameof(stravaAuthorization));

        if (string.IsNullOrWhiteSpace(stravaAuthorization.ClientId)) throw new ArgumentException(nameof(stravaAuthorization.ClientId));
        if (string.IsNullOrWhiteSpace(stravaAuthorization.ClientSecret)) throw new ArgumentException(nameof(stravaAuthorization.ClientSecret));

        // Construct the session
        _logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        _session = new StravaSession(stravaAuthorization, httpClient);

        _logger.LogDebug("StravaClient initialized (ClientId = {clientId}.", stravaAuthorization.ClientId); // the Id is NOT a secret
    }

    /// <inheritdoc/>
    public bool IsAuthenticated => _session.IsAuthenticated;

    /// <inheritdoc/>
    public Task<ApiResult<T>> GetApiResultAsync<T>(Uri requestUri, CancellationToken cancellationToken = default)
        => _session.StravaApi().GetApiResultAsync<T>(requestUri, cancellationToken);

    /// <inheritdoc/>
    public Task<ApiResult<Athlete>> GetAthleteAsync(long? athleteId = 0, CancellationToken cancellationToken = default)
        => _session.StravaApi().GetAthleteAsync(athleteId, cancellationToken);

    /// <inheritdoc/>
    public Task<Stream> GetStreamAsync(string requestUri, CancellationToken cancellationToken = default)
        => _session.StravaApi().GetStreamAsync(requestUri, cancellationToken);

    /// <inheritdoc/>
    public Task<ApiResult<TResult>> PutApiResultAsync<TBody, TResult>(Uri requestUri, TBody? body, CancellationToken cancellationToken = default)
        => _session.StravaApi().PutApiResultAsync<TBody, TResult>(requestUri, body, cancellationToken);

    /// <inheritdoc/>
    public Task<ApiResult<T>> GetApiResultAsync<T>(string uriStringOrPath, CancellationToken cancellationToken = default)
        => _session.StravaApi().GetApiResultAsync<T>(uriStringOrPath, cancellationToken);

    /// <inheritdoc/>
    public Task<ApiResult<TResult>> PutApiResultAsync<TBody, TResult>(string uriStringOrPath, TBody? body, CancellationToken cancellationToken = default)
        => _session.StravaApi().PutApiResultAsync<TBody, TResult>(uriStringOrPath, body, cancellationToken);
}

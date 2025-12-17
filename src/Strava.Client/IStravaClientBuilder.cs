using Microsoft.Extensions.Logging;

namespace Tudormobile.Strava.Client;

/// <summary>
/// Provides a builder interface for configuring and constructing <see cref="StravaClient"/> instances.
/// </summary>
/// <remarks>
/// This builder follows the fluent API pattern, allowing method chaining for easy configuration.
/// Options can be set individually (WithClientId, WithAccessToken, etc.) or in bulk (WithOptions, UseAuthorization).
/// Note that UseAuthorization() takes precedence over other option-setting methods.
/// </remarks>
public interface IStravaClientBuilder : IBuilder<StravaClient>
{
    /// <summary>
    /// Gets or sets the Strava options used for client configuration.
    /// </summary>
    /// <remarks>
    /// This property is the underlying storage for all OAuth credentials and configuration.
    /// Modifying this directly will affect all credential properties (ClientId, ClientSecret, etc.).
    /// </remarks>
    public StravaOptions Options { get; set; }

    /// <summary>
    /// Gets or sets the Strava application client identifier.
    /// </summary>
    /// <value>The client ID assigned by Strava when you register your application.</value>
    /// <remarks>
    /// Setting this property triggers a rebuild of the Options object to maintain consistency.
    /// This is required for OAuth authentication flows.
    /// </remarks>
    public string ClientId
    {
        get => Options.ClientId;
        set => SetOptions(value, Options.ClientSecret, Options.AccessToken, Options.RefreshToken);
    }

    /// <summary>
    /// Gets or sets the Strava application client secret.
    /// </summary>
    /// <value>The client secret assigned by Strava. Should be kept confidential.</value>
    /// <remarks>
    /// Setting this property triggers a rebuild of the Options object to maintain consistency.
    /// This is required for OAuth token refresh operations.
    /// </remarks>
    public string ClientSecret
    {
        get => Options.ClientSecret;
        set => SetOptions(Options.ClientId, value, Options.AccessToken, Options.RefreshToken);
    }

    /// <summary>
    /// Gets or sets the OAuth access token for API authentication.
    /// </summary>
    /// <value>The current access token. May expire and need to be refreshed.</value>
    /// <remarks>
    /// Setting this property triggers a rebuild of the Options object to maintain consistency.
    /// Access tokens typically expire after 6 hours and must be refreshed using the refresh token.
    /// </remarks>
    public string AccessToken
    {
        get => Options.AccessToken;
        set => SetOptions(Options.ClientId, Options.ClientSecret, value, Options.RefreshToken);
    }

    /// <summary>
    /// Gets or sets the OAuth refresh token for obtaining new access tokens.
    /// </summary>
    /// <value>The refresh token used to obtain new access tokens when they expire.</value>
    /// <remarks>
    /// Setting this property triggers a rebuild of the Options object to maintain consistency.
    /// Refresh tokens are long-lived and should be securely stored.
    /// </remarks>
    public string RefreshToken
    {
        get => Options.RefreshToken;
        set => SetOptions(Options.ClientId, Options.ClientSecret, Options.AccessToken, value);
    }

    /// <summary>
    /// Internal helper method to atomically update all OAuth credentials at once.
    /// </summary>
    /// <param name="clientId">The Strava application client identifier.</param>
    /// <param name="clientSecret">The Strava application client secret.</param>
    /// <param name="accessToken">The current OAuth access token.</param>
    /// <param name="refreshToken">The OAuth refresh token.</param>
    /// <remarks>
    /// This method ensures that all credentials are updated together, maintaining consistency
    /// in the Options object. This is called internally when any credential property is set.
    /// </remarks>
    void SetOptions(string clientId, string clientSecret, string accessToken, string refreshToken)
    {
        // Create a new StravaOptions instance with all credentials
        // This ensures atomic updates and prevents partial state changes
        Options = new StravaOptions()
        {
            ClientId = clientId,
            ClientSecret = clientSecret,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    /// <summary>
    /// Configures the builder to use the specified HTTP client for sending requests to the Strava V3 API.
    /// </summary>
    /// <param name="httpClient">The HTTP client instance to use for all outgoing API requests. Cannot be null. The caller is responsible for
    /// managing the lifetime of the provided client.</param>
    /// <returns>The current builder instance configured to use the specified HTTP client.</returns>
    /// <remarks>
    /// Use this method to provide a custom-configured HttpClient, for example to set custom headers,
    /// proxies, or timeouts. If not set, a default HttpClient may be used by the implementation.
    /// <para>
    /// ⚠️ Important: This method must be called; HttpClient is required for the client to function.
    /// The caller is responsible for managing the HttpClient lifetime (e.g., disposing it when done).
    /// </para>
    /// <para>
    /// Consider using HttpClientFactory in production applications for proper HttpClient management.
    /// </para>
    /// </remarks>
    IStravaClientBuilder WithHttpClient(HttpClient httpClient);

    /// <summary>
    /// Configures the builder with the specified Strava client options.
    /// </summary>
    /// <param name="options">The options to use for configuring the Strava client. Cannot be null.</param>
    /// <returns>The current builder instance configured with the specified options.</returns>
    /// <remarks>
    /// This is an alternative to setting credentials individually using WithClientId(), WithAccessToken(), etc.
    /// <para>
    /// Use this method when you have a pre-configured StravaOptions object, such as from configuration
    /// or dependency injection.
    /// </para>
    /// <para>
    /// Note: Using UseAuthorization() after this method will override these options.
    /// </para>
    /// </remarks>
    IStravaClientBuilder WithOptions(StravaOptions options);

    /// <summary>
    /// Configures the client to use the specified access token for authenticating requests to the Strava API.
    /// </summary>
    /// <param name="accessToken">The OAuth access token to use for API authentication. Cannot be null or empty.</param>
    /// <returns>The current builder instance with the access token configured. This enables method chaining.</returns>
    /// <remarks>
    /// Call this method before building the client to ensure all requests are authenticated. If the
    /// access token is invalid or expired, API requests may fail with an authentication error.
    /// <para>
    /// This is an alternative to setting UseAuthorization() or WithOptions(). You can chain this with
    /// other With* methods to configure all required credentials.
    /// </para>
    /// <para>
    /// Access tokens typically expire after 6 hours. Ensure you also provide a refresh token if you
    /// want automatic token renewal.
    /// </para>
    /// </remarks>
    IStravaClientBuilder WithAccessToken(string accessToken);

    /// <summary>
    /// Sets the client identifier to be used for authenticating requests to the Strava API.
    /// </summary>
    /// <param name="clientId">The client identifier assigned by Strava. Cannot be null or empty.</param>
    /// <returns>The current builder instance with the specified client identifier applied.</returns>
    /// <remarks>
    /// Call this method before building the client to ensure that all requests are authenticated
    /// with the correct client identifier. This method is typically used as part of a fluent configuration
    /// sequence.
    /// <para>
    /// This is an alternative to setting UseAuthorization() or WithOptions(). The client ID is required
    /// for OAuth flows and is obtained when you register your application with Strava.
    /// </para>
    /// <para>
    /// Example:
    /// <code>
    /// var client = builder
    ///     .WithClientId("your-client-id")
    ///     .WithClientSecret("your-secret")
    ///     .WithAccessToken("your-token")
    ///     .Build();
    /// </code>
    /// </para>
    /// </remarks>
    IStravaClientBuilder WithClientId(string clientId);

    /// <summary>
    /// Sets the client secret used for authenticating requests to the Strava API.
    /// </summary>
    /// <param name="clientSecret">The client secret provided by Strava for your application. Cannot be null or empty.</param>
    /// <returns>The current builder instance with the specified client secret configured.</returns>
    /// <remarks>
    /// Call this method before building the client to ensure authentication is properly configured.
    /// The client secret is required for OAuth flows and should be kept confidential.
    /// <para>
    /// ⚠️ Security: Never expose the client secret in client-side code or version control.
    /// Store it securely using environment variables, Azure Key Vault, or similar secure storage.
    /// </para>
    /// <para>
    /// This is an alternative to setting UseAuthorization() or WithOptions().
    /// </para>
    /// </remarks>
    IStravaClientBuilder WithClientSecret(string clientSecret);

    /// <summary>
    /// Configures the client to use the specified refresh token for authentication with the Strava API.
    /// </summary>
    /// <param name="refreshToken">The refresh token to use for obtaining new access tokens. Cannot be null or empty.</param>
    /// <returns>The current builder instance with the refresh token configured. This enables method chaining.</returns>
    /// <remarks>
    /// Use this method when you have a refresh token and want the client to automatically handle
    /// access token renewal. The refresh token must be valid for the associated Strava application.
    /// <para>
    /// This is an alternative to setting UseAuthorization() or WithOptions().
    /// </para>
    /// <para>
    /// Refresh tokens are long-lived and should be securely persisted. They allow the client
    /// to obtain new access tokens without requiring the user to re-authenticate.
    /// </para>
    /// </remarks>
    IStravaClientBuilder WithRefreshToken(string refreshToken);

    /// <summary>
    /// Configures the client to use the specified Strava authorization settings for API requests.
    /// </summary>
    /// <param name="authorization">The authorization information to use when authenticating requests. Cannot be null.</param>
    /// <returns>The current <see cref="IStravaClientBuilder"/> instance for method chaining.</returns>
    /// <remarks>
    /// This method provides the most direct way to configure authentication using an existing
    /// StravaAuthorization object, which contains all required OAuth credentials.
    /// <para>
    /// ⚠️ Important: Using this method overrides any credentials previously set via WithOptions()
    /// or individual methods (WithClientId(), WithAccessToken(), etc.). This should be the last
    /// authentication configuration method called in the builder chain.
    /// </para>
    /// <para>
    /// Prefer this method when you already have a StravaAuthorization object from a session or
    /// when migrating from the core StravaSession API.
    /// </para>
    /// </remarks>
    IStravaClientBuilder UseAuthorization(StravaAuthorization authorization);

    /// <summary>
    /// Adds the specified logger to the client builder for capturing diagnostic and operational messages.
    /// </summary>
    /// <param name="logger">The logger instance to use for logging client activity. Cannot be null.</param>
    /// <returns>The current instance of the client builder for method chaining.</returns>
    /// <remarks>
    /// Use this method to integrate with your application's logging infrastructure.
    /// The logger will capture information about API requests, responses, errors, and token refresh operations.
    /// <para>
    /// Example with Microsoft.Extensions.Logging:
    /// <code>
    /// var logger = loggerFactory.CreateLogger&lt;StravaClient&gt;();
    /// var client = builder
    ///     .AddLogging(logger)
    ///     .WithAccessToken("your-token")
    ///     .Build();
    /// </code>
    /// </para>
    /// </remarks>
    IStravaClientBuilder AddLogging(ILogger logger);
}

/// <summary>
/// Defines a mechanism for constructing an instance of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of object to be constructed by the builder.</typeparam>
/// <remarks>
/// This is a generic builder interface that follows the Builder design pattern.
/// It provides a consistent contract for any builder implementation in the library.
/// </remarks>
public interface IBuilder<T>
{
    /// <summary>
    /// Creates and returns an instance of type <typeparamref name="T"/> based on the current configuration.
    /// </summary>
    /// <returns>An instance of type <typeparamref name="T"/> representing the constructed object.</returns>
    /// <remarks>
    /// This method finalizes the builder configuration and produces the target object.
    /// After calling Build(), the builder should be considered consumed and should not be reused.
    /// <para>
    /// Implementations should validate that all required configuration has been provided
    /// before constructing the object, throwing appropriate exceptions if validation fails.
    /// </para>
    /// </remarks>
    T Build();
}
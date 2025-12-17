namespace Tudormobile.Strava;

/// <summary>
/// Client Authorization Record.
/// </summary>
public class StravaAuthorization : StravaAuthorizationOptions
{
    /// <summary>
    /// Current user identifier (AthleteId), or zero if not authenticated.
    /// </summary>
    public long Id { get; set; } = 0;

    /// <summary>
    /// Access token expiration date and time.
    /// </summary>
    public DateTime Expires { get; init; } = DateTime.MinValue;

    /// <summary>
    /// Create and initialize a new instance.
    /// </summary>
    /// <param name="clientId">Client identifier.</param>
    /// <param name="clientSecret">Client secret.</param>
    /// <param name="accessToken">Access token.</param>
    /// <param name="refreshToken">Refresh token.</param>
    /// <param name="expires">Access token expiration date and time.</param>
    public StravaAuthorization(string? clientId = null, string? clientSecret = null, String? accessToken = null, string? refreshToken = null, DateTime? expires = null)
    {
        ClientId = clientId ?? String.Empty;
        ClientSecret = clientSecret ?? String.Empty;
        AccessToken = accessToken ?? String.Empty;
        RefreshToken = refreshToken ?? String.Empty;
        Expires = expires ?? DateTime.MinValue;
    }

    /// <summary>
    /// Creates a new <see cref="StravaAuthorization"/> instance with updated access and refresh tokens and expiration.
    /// </summary>
    /// <param name="accessToken">The new access token.</param>
    /// <param name="refreshToken">The new refresh token.</param>
    /// <param name="expires">The new expiration date and time for the access token.</param>
    /// <returns>A new <see cref="StravaAuthorization"/> instance with updated token information.</returns>
    public StravaAuthorization WithTokens(string accessToken, string refreshToken, DateTime expires)
    {
        return new StravaAuthorization(ClientId, ClientSecret, accessToken, refreshToken, expires)
        {
            Id = this.Id
        };
    }
}

/// <summary>
/// Represents the configuration options required for authorizing access to the Strava API.
/// </summary>
/// <remarks>This class encapsulates the credentials and tokens needed to authenticate and authorize requests to
/// Strava on behalf of a client application. The values are typically provided by the Strava developer portal and
/// should be kept secure. Use these options when configuring services that interact with the Strava API.</remarks>
public class StravaAuthorizationOptions
{
    /// <summary>
    /// Client Access Token.
    /// </summary>
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// Client Identifier.
    /// </summary>
    public string ClientId { get; init; } = string.Empty;

    /// <summary>
    /// Client Secret.
    /// </summary>
    public string ClientSecret { get; init; } = string.Empty;

    /// <summary>
    /// Client Refresh Token.
    /// </summary>
    public string RefreshToken { get; init; } = string.Empty;
}



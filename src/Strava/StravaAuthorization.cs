namespace Tudormobile.Strava;

/// <summary>
/// Client Authorization Record.
/// </summary>
public class StravaAuthorization
{
    /// <summary>
    /// Current user identifier (AthleteId), or zero if not authenticated.
    /// </summary>
    public long Id { get; set; } = 0;

    /// <summary>
    /// Client Identifier.
    /// </summary>
    public string ClientId { get; init; }

    /// <summary>
    /// Client Secret.
    /// </summary>
    public string ClientSecret { get; init; }

    /// <summary>
    /// Access token expiration date and time.
    /// </summary>
    public DateTime Expires { get; init; } = DateTime.MinValue;

    /// <summary>
    /// Client Access Token.
    /// </summary>
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// Client Refresh Token.
    /// </summary>
    public string RefreshToken { get; init; } = string.Empty;

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


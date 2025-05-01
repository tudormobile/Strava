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
    public DateTime Expires { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Client Access Token.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Client Refresh Token.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

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
}


using Tudormobile.Strava.Model;

namespace Tudormobile.Strava;

/// <summary>
/// Client Authorization Record.
/// </summary>
public class StravaAuthorization
{
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

    /// <summary>
    /// Update the authorization record from a stream.
    /// </summary>
    /// <param name="stream">Strava API V3 response stream.</param>
    /// <exception cref="Exception">throws on any error.</exception>
    /// <returns>An Athlete Id object, if successful; otherwise (null)</returns>
    public AthleteId? UpdateFromResponse(Stream stream)
    {
        if (StravaSerializer.TryDeserialize<AuthorizationResponse>(stream, out var auth))
        {
            if (auth == null) throw new Exception("Failed to deserialize StravaAuthorization");
            if (auth.access_token == null) throw new Exception("Failed to deserialize StravaAuthorization");
            if (auth.refresh_token == null) throw new Exception("Failed to deserialize StravaAuthorization");
            AccessToken = auth.access_token;
            RefreshToken = auth.refresh_token;
            Expires = DateTime.UnixEpoch.AddSeconds(auth.expires_at ?? 0);

            return auth.athlete;
        }
        throw new Exception("Failed to deserialize StravaAuthorization");
    }
}


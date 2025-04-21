using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava;

/// <summary>
/// Strava V3 API
/// </summary>
/// <remarks>
/// Strava V3 API authentication, authorization, and access to Athletes and Activities APIs.
/// </remarks>
public class StravaSession
{
    internal StravaApiImpl? _api;
    private readonly string STRAVA_TOKEN_ENDPOINT = "https://www.strava.com/oauth/token";
    /// <summary>
    /// True if current session is authenticated, i.e., access token is not expired.
    /// </summary>
    public bool IsAuthenticated => DateTime.Now.Subtract(TimeSpan.FromSeconds(30)) < Authorization.Expires;

    /// <summary>
    /// Current client authorization record.
    /// </summary>
    public StravaAuthorization Authorization { get; init; }

    /// <summary>
    /// Create and initialize a new instance.
    /// </summary>
    /// <param name="clientAuthorization">Initial authorization.</param>
    public StravaSession(StravaAuthorization clientAuthorization)
    {
        if (clientAuthorization == null) throw new ArgumentNullException(nameof(clientAuthorization));
        Authorization = clientAuthorization;
    }

    /// <summary>
    /// Refresh the authorization (and refresh) tokens.
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<StravaAuthorization>> RefreshAsync()
    {
        var client = new HttpClient();
        KeyValuePair<string, string>[] data =
            [
                new ("client_id", Authorization.ClientId),
                new ("client_secret", Authorization.ClientSecret),
                new ("grant_type", "refresh_token"),
                new ("refresh_token", Authorization.RefreshToken),
            ];

        var content = new FormUrlEncodedContent(data);
        try
        {
            var response = await client.PostAsync(STRAVA_TOKEN_ENDPOINT, content);
            if (response.IsSuccessStatusCode)
            {
                // refresh the authorization data
                if (StravaSerializer.TryDeserialize<AuthorizationResponse>(response.Content.ReadAsStream(), out var auth))
                {
                    if (auth != null && auth.access_token != null && auth.refresh_token != null)
                    {
                        Authorization.AccessToken = auth.access_token;
                        Authorization.RefreshToken = auth.refresh_token;
                        Authorization.Expires = DateTime.UnixEpoch.AddSeconds(auth.expires_at ?? 0);
                        return new ApiResult<StravaAuthorization>(Authorization);   // Successful result
                    }
                }
                return new ApiResult<StravaAuthorization>(error: new ApiError("Unexpected response received"));
            }

            var reply = await response.Content.ReadAsStringAsync();
            if (StravaSerializer.TryDeserialize<Fault>(reply, out var fault))
            {
                return new ApiResult<StravaAuthorization>(error: new ApiError(fault: fault!));
            }
        }
        catch (HttpRequestException ex)
        {
            return new ApiResult<StravaAuthorization>(error: new ApiError("Unable to authorize", ex));
        }
        return new ApiResult<StravaAuthorization>(error: new ApiError("Unable to authorize"));
    }
}


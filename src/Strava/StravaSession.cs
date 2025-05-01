using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava;

/// <summary>
/// Strava V3 API Session
/// </summary>
/// <remarks>
/// Strava V3 API authentication, authorization, and access to Athletes and Activities APIs.
/// <para>
/// A StravaSession object manages the authentication and authorization of a Strava logged-in Athlete (user).
/// Provide either an 'unauthenticated' StravaAuthorization object (CliendId and ClientSecret only) to create
/// a new session, or you can supply the previous tokens and call one the the Refresh() methods to ensure the
/// user is authenticated. Once authenticated, the session can be used to access the Strava API.
/// </para>
/// </remarks>
public class StravaSession
{
    internal StravaApiImpl? _api;
    private readonly string STRAVA_TOKEN_ENDPOINT = "https://www.strava.com/oauth/token";
    /// <summary>
    /// True if current session is authenticated, i.e., access token is provided and is not expired.
    /// </summary>
    /// <remarks>
    /// If the current access token is within 30 seconds of expiration, the session is considered not authenticated.
    /// </remarks>
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
    /// Get the Strava API object.
    /// </summary>
    /// <returns>Basic Strava V3 API object.</returns>
    public IStravaApi StravaApi() => _api ??= new StravaApiImpl(this);

    /// <summary>
    /// Refresh the authorization tokens.
    /// </summary>
    /// <remarks>
    /// If provided (and not expired), the AccessToken and RefreshToken are updated. This method does
    /// not return errors; check the IsAuthenticated property to determine if the session is authenticated.
    /// This method is provded as a convieniee method to refresh the session with existing credentials.
    /// </remarks>
    /// <returns>Reference to the current session.</returns>
    public async Task<StravaSession> RefreshTokens()
    {
        if (!IsAuthenticated)
        {
            _ = await RefreshAsync();
        }
        return this;
    }

    /// <summary>
    /// Refresh the authorization (access and refresh) tokens.
    /// </summary>
    /// <returns>Strava API result.</returns>
    /// <remarks>
    /// Success or Failure is returned in the ApiResult object. If successful, the session authentication is 
    /// updated with the new authentication and refresh tokens, as well as the currently logged in Athlete (user) Id.
    /// </remarks>
    public async Task<ApiResult<StravaAuthorization>> RefreshAsync()
    {
        using var client = new HttpClient();
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
                        Authorization.Id = auth.athlete?.Id ?? 0;
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


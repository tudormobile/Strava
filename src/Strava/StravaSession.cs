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
/// Provide either an 'unauthenticated' StravaAuthorization object (ClientId and ClientSecret only) to create
/// a new session, or you can supply the previous tokens and call one the the Refresh() methods to ensure the
/// user is authenticated. Once authenticated, the session can be used to access the Strava API.
/// </para>
/// </remarks>
public class StravaSession
{
    private static readonly TimeSpan DEFAULT_TIMEOUT = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Shared HttpClient instance for sessions without a provided client.
    /// Static HttpClient is intentionally not disposed per Microsoft best practices for HttpClient reuse.
    /// </summary>
    /// <remarks>
    /// See: https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
    /// </remarks>
    private static readonly HttpClient _httpClient = new() { Timeout = DEFAULT_TIMEOUT };
    private readonly HttpClient? _providedClient;
    internal StravaApiImpl? _api;
    private readonly string STRAVA_TOKEN_ENDPOINT = "https://www.strava.com/oauth/token";
    private StravaAuthorization _authorization;

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
    public StravaAuthorization Authorization => _authorization;

    /// <summary>
    /// Create and initialize a new instance.
    /// </summary>
    /// <param name="clientAuthorization">Initial authorization.</param>
    /// <param name="httpClient">HttpClient to use for api requests; Optional.</param>
    /// <remarks>
    /// Providing an HttpClient is preferred. If provided, management of the HttpClient is the 
    /// responsibility of the caller.
    /// <para>
    /// If the HttpClient is not provided, a single static HttpClient instance will
    /// be used with a 30-second timeout value. Be aware that this can result in issues
    /// with stale DNS and connection pooling.
    /// </para> 
    /// </remarks>
    public StravaSession(StravaAuthorization clientAuthorization, HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(clientAuthorization);
        _authorization = clientAuthorization;
        _providedClient = httpClient;
    }

    /// <summary>
    /// Get the Strava API object.
    /// </summary>
    /// <returns>Basic Strava V3 API object.</returns>
    public IStravaApi StravaApi() => _api ??= new StravaApiImpl(this, _providedClient ?? _httpClient);

    /// <summary>
    /// Refresh the authorization tokens.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <remarks>
    /// ⚠️ This method silently handles authentication failures. Check <see cref="IsAuthenticated"/>
    /// after calling to verify success. For explicit error handling, use <see cref="RefreshAsync"/> instead.
    /// </remarks>
    /// <returns>Reference to the current session.</returns>
    public async Task<StravaSession> RefreshTokensAsync(CancellationToken cancellationToken = default)
    {
        if (!IsAuthenticated)
        {
            _ = await RefreshAsync(cancellationToken).ConfigureAwait(false);  // ⚠️ Discards result
        }
        return this;  // Always returns 'this', even on failure
    }

    /// <summary>
    /// Refresh the authorization (access and refresh) tokens.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>Strava API result.</returns>
    /// <remarks>
    /// Success or Failure is returned in the ApiResult object. If successful, the session authentication is 
    /// updated with the new authentication and refresh tokens, as well as the currently logged in Athlete (user) Id.
    /// </remarks>
    public async Task<ApiResult<StravaAuthorization>> RefreshAsync(CancellationToken cancellationToken = default)
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
            var response = await client.PostAsync(STRAVA_TOKEN_ENDPOINT, content, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                // refresh the authorization data
                if (StravaSerializer.TryDeserialize<AuthorizationResponse>(await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false), out var auth))
                {
                    if (auth != null && auth.access_token != null && auth.refresh_token != null)
                    {
                        _authorization = Authorization.WithTokens(auth.access_token, auth.refresh_token, DateTime.UnixEpoch.AddSeconds(auth.expires_at ?? 0));
                        _authorization.Id = auth.athlete?.Id ?? 0;
                        return new ApiResult<StravaAuthorization>(Authorization);   // Successful result
                    }
                }
                return new ApiResult<StravaAuthorization>(error: new ApiError("Unexpected response received"));
            }

            var reply = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
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


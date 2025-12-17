using Microsoft.Extensions.Logging;

namespace Tudormobile.Strava.Client;

internal class StravaClientBuilder : IStravaClientBuilder
{
    private StravaOptions _options = new();
    private ILogger? _logger;
    private HttpClient? _httpClient;
    private StravaAuthorization? _authorization;

    public StravaClient Build()
    {
        if (_httpClient == null)
        {
            throw new InvalidOperationException("An HttpClient instance must be provided. Use WithHttpClient() to indicate what client instance to use.");
        }
        return _authorization == null
            ? new StravaClient(_httpClient, _logger, _options.ClientId, _options.ClientSecret, _options.AccessToken, _options.RefreshToken)
            : new StravaClient(_httpClient, _logger, _authorization);
    }

    public StravaOptions Options
    {
        get => _options;
        set { _options = value; }
    }

    public IStravaClientBuilder AddLogging(ILogger logger)
    {
        _logger = logger;
        return this;
    }

    public IStravaClientBuilder UseAuthorization(StravaAuthorization authorization)
    {
        _authorization = authorization;
        return this;
    }

    public IStravaClientBuilder WithAccessToken(string accessToken)
    {
        ((IStravaClientBuilder)this).AccessToken = accessToken;
        return this;
    }

    public IStravaClientBuilder WithClientId(string clientId)
    {
        ((IStravaClientBuilder)this).ClientId = clientId;
        return this;
    }

    public IStravaClientBuilder WithClientSecret(string clientSecret)
    {
        ((IStravaClientBuilder)this).ClientSecret = clientSecret;
        return this;
    }

    public IStravaClientBuilder WithRefreshToken(string refreshToken)
    {
        ((IStravaClientBuilder)this).RefreshToken = refreshToken;
        return this;
    }

    public IStravaClientBuilder WithOptions(StravaOptions options)
    {
        _options = options;
        return this;
    }

    public IStravaClientBuilder WithHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        return this;
    }
}
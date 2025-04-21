using System.Diagnostics.CodeAnalysis;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace Strava.Tests.Api;

[TestClass]
public class StravaApiTests
{
    private static StravaSession? _session;

    [ClassInitialize, ExcludeFromCodeCoverage]
    public static void InitializeActivitiesApi(TestContext context)
    {
        // read the environment from json data
        string client_id = Environment.GetEnvironmentVariable("STRAVA_CLIENT_ID") ?? string.Empty;
        string client_secret = Environment.GetEnvironmentVariable("STRAVA_CLIENT_SECRET") ?? string.Empty;
        string access_token = Environment.GetEnvironmentVariable("STRAVA_ACCESS_TOKEN") ?? string.Empty;
        string refresh_token = Environment.GetEnvironmentVariable("STRAVA_REFRESH_TOKEN") ?? string.Empty;

        var auth = new StravaAuthorization(client_id, client_secret, access_token, refresh_token);
        _session = new StravaSession(auth);
        var result = _session.RefreshAsync();
        result.Wait();
        if (result.IsCompletedSuccessfully && _session.IsAuthenticated)
        {
            // Save the data
            Environment.SetEnvironmentVariable("STRAVA_ACCESS_TOKEN", _session.Authorization.AccessToken, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable("STRAVA_REFRESH_TOKEN", _session.Authorization.RefreshToken, EnvironmentVariableTarget.User);
        }
    }
    // is this needed??
    [ClassCleanup, ExcludeFromCodeCoverage]
    public static void CleanupActivitiesApi()
    {
        if (_session?.Authorization != null && _session.IsAuthenticated)
        {
            // Save the data
            Environment.SetEnvironmentVariable("STRAVA_ACCESS_TOKEN", _session.Authorization.AccessToken, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable("STRAVA_REFRESH_TOKEN", _session.Authorization.RefreshToken, EnvironmentVariableTarget.User);
        }
    }

    [TestMethod]
    public async Task GetActivitiesTest()
    {
        var clientAuthorization = new StravaAuthorization();
        var session = new StravaSession(clientAuthorization);
        var api = session.CreateApi();
        var actual = await api.GetActivities(DateTime.Now, DateTime.MinValue);
        Assert.IsFalse(actual.Success);
        Assert.IsNotNull(actual.Error);
        Assert.IsNull(actual.Data);
    }
}

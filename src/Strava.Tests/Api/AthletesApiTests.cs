using System.Diagnostics.CodeAnalysis;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace Strava.Tests.Api;

[TestClass]
public class AthletesApiTests
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
        _session = new StravaSession(auth).RefreshTokens().Result;
    }

    [TestMethod]
    public void AthletesApiTest()
    {
        var target = _session!.AthletesApi();
        Assert.IsInstanceOfType<IAthletesApi>(target);
    }

    [TestMethod]
    public async Task GetAthleteTest()
    {
        var target = _session!.AthletesApi();
        var id = _session!.Authorization.Id;
        if (_session.IsAuthenticated)
        {
            var actual = await target.GetAthlete();
            Assert.IsTrue(actual.Success);
            Assert.IsNotNull(actual.Data);
            Assert.AreEqual(_session.Authorization.Id, actual.Data.Id);
        }
    }

}

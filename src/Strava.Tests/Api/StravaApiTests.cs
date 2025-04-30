using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

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
        if (_session!.IsAuthenticated)
        {
            var api = _session!.CreateApi();
            var actual = await api.GetActivities(DateTime.Now, DateTime.UnixEpoch);
            Assert.IsTrue(actual.Success);
            Assert.IsNull(actual.Error);
            Assert.IsNotNull(actual.Data);
            Assert.IsTrue(actual.Data.Any());

            foreach (var activity in actual.Data)
            {
                Debug.WriteLine($"{activity.Id}:{activity.StartDate}:{activity.Name}, Duration={activity.MovingTime}, sportType={activity.SportType}, distance={activity.Distance}");
            }
        }
    }

    [TestMethod]
    public async Task GetActivitiesTestBadAuth()
    {
        var clientAuthorization = new StravaAuthorization();
        var session = new StravaSession(clientAuthorization);
        var api = session.CreateApi();
        var actual = await api.GetActivities(DateTime.Now, DateTime.MinValue);
        Assert.IsFalse(actual.Success);
        Assert.IsNotNull(actual.Error);
        Assert.IsNull(actual.Data);
    }

    [TestMethod]
    public async Task GetActivitiesTestBadPage()
    {
        var api = _session!.CreateApi();
        var actual = await api.GetActivities(DateTime.Now, DateTime.MinValue, page: -5);
        Assert.IsFalse(actual.Success);
        Assert.IsNotNull(actual.Error);
        Assert.IsNull(actual.Data);
    }

    [TestMethod]
    public async Task UpdateActivityTest()
    {
        var clientAuthorization = new StravaAuthorization();
        var session = new StravaSession(clientAuthorization);
        var api = session.CreateApi();
        var id = 123;
        var actual = await api.UpdateActivity(id, new UpdatableActivity());
        Assert.IsFalse(actual.Success);
        Assert.IsNotNull(actual.Error);
        Assert.IsNull(actual.Data);
    }

    [TestMethod]
    public async Task GetActivityTest()
    {
        var clientAuthorization = new StravaAuthorization();
        var session = new StravaSession(clientAuthorization);
        var api = session.CreateApi();
        var id = 123;
        var actual = await api.GetActivity(id, includeAllEfforts: false);
        Assert.IsFalse(actual.Success);
        Assert.IsNotNull(actual.Error);
        Assert.IsNull(actual.Data);
    }

}

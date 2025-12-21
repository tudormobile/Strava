using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Api;

[TestClass]
public class AthletesApiTests
{
    private static StravaSession? _session;

    [ClassInitialize]
    public static void InitializeActivitiesApi(TestContext _)
    {
        // read the environment from json data
        string client_id = Environment.GetEnvironmentVariable("STRAVA_CLIENT_ID") ?? string.Empty;
        string client_secret = Environment.GetEnvironmentVariable("STRAVA_CLIENT_SECRET") ?? string.Empty;
        string access_token = Environment.GetEnvironmentVariable("STRAVA_ACCESS_TOKEN") ?? string.Empty;
        string refresh_token = Environment.GetEnvironmentVariable("STRAVA_REFRESH_TOKEN") ?? string.Empty;

        var auth = new StravaAuthorization(client_id, client_secret, access_token, refresh_token);
        _session = new StravaSession(auth).RefreshTokensAsync(_.CancellationToken).Result;
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
        // Note: this test hits the actual API endpoint, but only if IsAuthenticated is true (running locally with valid tokens)
        var target = _session!.AthletesApi();
        if (_session!.IsAuthenticated)
        {
            var actual = await target.GetAthleteAsync(cancellationToken: TestContext.CancellationToken);
            Assert.IsTrue(actual.Success);
            Assert.IsNotNull(actual.Data);
            Assert.AreNotEqual(0, actual.Data!.Id);
            // Should this be checked?
            // Assert.AreEqual(_session.Authorization.Id, actual.Data.Id);
        }
    }

    [TestMethod]
    public async Task GetLoggedInAthleteZonesTest()
    {
        // Arrange
        var handler = new MockHttpMessageHandler
        {
            JsonResponse = @"[ {
  ""distribution_buckets"" : [ {
    ""max"" : 0,
    ""min"" : 0,
    ""time"" : 1498
  }, {
    ""max"" : 50,
    ""min"" : 0,
    ""time"" : 62
  }, {
    ""max"" : 100,
    ""min"" : 50,
    ""time"" : 169
  } ],
  ""type"" : ""power"",
  ""resource_state"" : 3,
  ""sensor_based"" : true
} ]"
        };
        var expected = "https://www.strava.com/api/v3/athlete/zones";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        // Act
        var athletesApi = session.AthletesApi();
        var result = await athletesApi.GetLoggedInAthleteZonesAsync(TestContext.CancellationToken);
        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri!.ToString());
        Assert.HasCount(1, result.Data);
        Assert.IsTrue(result.Data[0].SensorBased);
        Assert.HasCount(3, result.Data[0].DistributionBuckets);
        Assert.AreEqual(50, result.Data[0].DistributionBuckets[1].Max);
        Assert.AreEqual(62, result.Data[0].DistributionBuckets[1].Time);
        Assert.AreEqual(50, result.Data[0].DistributionBuckets[2].Min);
    }

    [TestMethod]
    public async Task GetStatsTest()
    {
        // Arrange
        var handler = new MockHttpMessageHandler
        {
            JsonResponse = @"{
  ""recent_run_totals"" : """",
  ""all_run_totals"" : """",
  ""recent_swim_totals"" : """",
  ""biggest_ride_distance"" : 0.8008281904610115,
  ""ytd_swim_totals"" : """",
  ""all_swim_totals"" : """",
  ""recent_ride_totals"" : {
    ""distance"" : 5.962134,
    ""achievement_count"" : 9,
    ""count"" : 1,
    ""elapsed_time"" : 2,
    ""elevation_gain"" : 7.0614014,
    ""moving_time"" : 5
  },
  ""biggest_climb_elevation_gain"" : 6.027456183070403,
  ""ytd_ride_totals"" : """",
  ""all_ride_totals"" : """",
  ""ytd_run_totals"" : """"
}"
        };
        var athleteId = 123456789;
        var expected = $"https://www.strava.com/api/v3/athletes/{athleteId}/stats";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        // Act
        var athletesApi = session.AthletesApi();
        var result = await athletesApi.GetStatsAsync(athleteId, TestContext.CancellationToken);
        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri!.ToString());

        Assert.AreEqual(0.8008281904610115, result.Data.BiggestRideDistance, 0.0001);
        Assert.AreEqual(5.962134, result.Data.RecentRideTotals!.Distance, 0.0001);
        Assert.IsNotNull(result.Data.RecentRideTotals);
        Assert.AreEqual(9, result.Data.RecentRideTotals.AchievementCount);
        Assert.AreEqual(1, result.Data.RecentRideTotals.Count);
        Assert.AreEqual(2, result.Data.RecentRideTotals.ElapsedTime);
        Assert.AreEqual(7.0614014, result.Data.RecentRideTotals.ElevationGain, 0.0001);
        Assert.AreEqual(5, result.Data.RecentRideTotals.MovingTime);

        Assert.IsNull(result.Data.RecentRunTotals);
        Assert.IsNull(result.Data.AllRunTotals);
        Assert.IsNull(result.Data.RecentSwimTotals);
        Assert.IsNull(result.Data.YtdSwimTotals);
        Assert.IsNull(result.Data.AllSwimTotals);
        Assert.IsNull(result.Data.YtdRideTotals);
        Assert.IsNull(result.Data.AllRideTotals);
        Assert.AreEqual(6.027456183070403, result.Data.BiggestClimbElevationGain, 0.0001);
        Assert.IsNull(result.Data.YtdRideTotals);
        Assert.IsNull(result.Data.AllRideTotals);
        Assert.IsNull(result.Data.YtdRunTotals);
    }

    [TestMethod]
    public async Task UpdateLoggedInAthleteTest()
    {
        // Arrange
        var handler = new MockHttpMessageHandler
        {
            JsonResponse = @"{
  ""id"" : 123456789,
  ""username"" : ""marianne_v"",
  ""resource_state"" : 3,
  ""firstname"" : ""Marianne"",
  ""lastname"" : ""V."",
  ""city"" : ""San Francisco"",
  ""state"" : ""CA"",
  ""country"" : ""US"",
  ""sex"" : ""F"",
  ""premium"" : true,
  ""created_at"" : ""2017-11-14T02:30:05Z"",
  ""updated_at"" : ""2018-02-06T19:32:20Z"",
  ""badge_type_id"" : 4,
  ""profile_medium"" : ""https://xxxxxx.cloudfront.net/pictures/athletes/1234567898765509876/1234567898765509876/2/medium.jpg"",
  ""profile"" : ""https://xxxxx.cloudfront.net/pictures/athletes/1234567898765509876/1234567898765509876/2/large.jpg"",
  ""friend"" : null,
  ""follower"" : null,
  ""follower_count"" : 5,
  ""friend_count"" : 5,
  ""mutual_friend_count"" : 0,
  ""athlete_type"" : 1,
  ""date_preference"" : ""%m/%d/%Y"",
  ""measurement_preference"" : ""feet"",
  ""clubs"" : [ ],
  ""ftp"" : null,
  ""weight"" : 123.456,
  ""bikes"" : [ {
    ""id"" : ""b1234567898765509876"",
    ""primary"" : true,
    ""name"" : ""EMC"",
    ""resource_state"" : 2,
    ""distance"" : 0
  } ],
  ""shoes"" : [ {
    ""id"" : ""g1234567898765509876"",
    ""primary"" : true,
    ""name"" : ""adidas"",
    ""resource_state"" : 2,
    ""distance"" : 4904
  } ]
}"
        };
        var weight = 123.456;
        var expected = $"https://www.strava.com/api/v3/athlete";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        // Act
        var athletesApi = session.AthletesApi();
        var result = await athletesApi.UpdateLoggedInAthleteAsync(weight, TestContext.CancellationToken);
        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri!.ToString());

        Assert.AreEqual(123456789, result.Data.Id);
        Assert.AreEqual("marianne_v", result.Data.Username);
        Assert.AreEqual(ResourceStates.Detail, result.Data.ResourceState);
        Assert.AreEqual("Marianne", result.Data.Firstname);
        Assert.AreEqual("V.", result.Data.Lastname);
        Assert.AreEqual("San Francisco", result.Data.City);
        Assert.AreEqual("CA", result.Data.State);
        Assert.AreEqual("US", result.Data.Country);
        Assert.AreEqual("F", result.Data.Sex);
        Assert.IsTrue(result.Data.Premium);
        //Assert.AreEqual(DateTime.Parse("2017-11-14T02:30:05Z"), result.Data.CreatedAt);
        //Assert.AreEqual(DateTime.Parse("2018-02-06T19:32:20Z"), result.Data.UpdatedAt);
        Assert.AreEqual(4, result.Data.BadgeTypeId);
        Assert.AreEqual("https://xxxxxx.cloudfront.net/pictures/athletes/1234567898765509876/1234567898765509876/2/medium.jpg", result.Data.ProfileMedium);
        Assert.AreEqual("https://xxxxx.cloudfront.net/pictures/athletes/1234567898765509876/1234567898765509876/2/large.jpg", result.Data.Profile);
        Assert.AreEqual(5, result.Data.FollowerCount);
        Assert.AreEqual(5, result.Data.FriendCount);
        Assert.AreEqual(0, result.Data.MutualFriendCount);
        Assert.AreEqual(1, result.Data.AthleteType);
        Assert.AreEqual("%m/%d/%Y", result.Data.DatePreference);
        Assert.AreEqual("feet", result.Data.MeasurementPreference);
        Assert.IsNotNull(result.Data.Clubs);
        Assert.IsEmpty(result.Data.Clubs);
        Assert.IsNull(result.Data.Ftp);
        Assert.AreEqual(123.456, result.Data.Weight, 0.0001);
        Assert.IsNotNull(result.Data.Bikes);
        Assert.HasCount(1, result.Data.Bikes);
        Assert.AreEqual("b1234567898765509876", result.Data.Bikes[0].Id);
        Assert.IsTrue(result.Data.Bikes[0].Primary);
        Assert.AreEqual("EMC", result.Data.Bikes[0].Name);
        Assert.AreEqual(ResourceStates.Summary, result.Data.Bikes[0].ResourceState);
        Assert.AreEqual(0, result.Data.Bikes[0].Distance);
        Assert.IsNotNull(result.Data.Shoes);
        Assert.HasCount(1, result.Data.Shoes);
        Assert.AreEqual("g1234567898765509876", result.Data.Shoes[0].Id);
        Assert.IsTrue(result.Data.Shoes[0].Primary);
        Assert.AreEqual("adidas", result.Data.Shoes[0].Name);
        Assert.AreEqual(ResourceStates.Summary, result.Data.Shoes[0].ResourceState);
        Assert.AreEqual(4904, result.Data.Shoes[0].Distance);
    }

    [TestMethod]
    public async Task ListAthleteClubsAsync_ReturnsListOfClubs()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""id"": 231407,
    ""resource_state"": 2,
    ""name"": ""The Strava Club"",
    ""profile_medium"": ""https://xxxxx.cloudfront.net/pictures/clubs/231407/5319085/1/medium.jpg"",
    ""profile"": ""https://xxxxx.cloudfront.net/pictures/clubs/231407/5319085/1/large.jpg"",
    ""cover_photo"": ""https://xxxxx.cloudfront.net/pictures/clubs/231407/5098428/4/large.jpg"",
    ""cover_photo_small"": ""https://xxxxx.cloudfront.net/pictures/clubs/231407/5098428/4/small.jpg"",
    ""sport_type"": ""other"",
    ""city"": ""San Francisco"",
    ""state"": ""California"",
    ""country"": ""United States"",
    ""private"": false,
    ""member_count"": 93151,
    ""featured"": false,
    ""verified"": true,
    ""url"": ""strava""
  }
]"
        };

        // Arrange
        var expected = $"https://www.strava.com/api/v3/athlete/clubs";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.AthletesApi();

        // Act
        var result = await api.ListAthleteClubsAsync(cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success, result.Error?.Message ?? "Api call failed.");
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

    }

    public TestContext TestContext { get; set; }
}

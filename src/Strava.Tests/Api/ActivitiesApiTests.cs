using System.Net;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Api;

[TestClass]
public class ActivitiesApiTests
{
    [TestMethod]
    public void ActivitiesApi_ReturnsValidInstance()
    {
        // Arrange
        var auth = new StravaAuthorization();
        var session = new StravaSession(auth);

        // Act
        var api = session.ActivitiesApi();

        // Assert
        Assert.IsNotNull(api);
        Assert.IsInstanceOfType<IActivitiesApi>(api);
    }

    [TestMethod]
    public async Task GetActivitiesAsync_WithValidResponse_ReturnsSuccessResult()
    {
        // Arrange
        var jsonResponse = @"[
            {
                ""id"": 123456,
                ""name"": ""Morning Run"",
                ""distance"": 5000.0,
                ""moving_time"": 1800.0,
                ""elapsed_time"": 1900.0,
                ""total_elevation_gain"": 100.0,
                ""type"": ""Run"",
                ""sport_type"": ""Run"",
                ""start_date"": ""2023-01-01T08:00:00Z"",
                ""average_speed"": 2.78,
                ""max_speed"": 4.5,
                ""athlete"": { ""id"": 1, ""resource_state"": 1 }
            }
        ]";
        var handler = new MockHttpMessageHandler { JsonResponse = jsonResponse };
        var httpClient = new HttpClient(handler);
        var auth = new StravaAuthorization("client_id", "client_secret", "access_token", "refresh_token", expires: DateTime.Now.AddMonths(1));
        var session = new StravaSession(auth, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.GetActivitiesAsync(cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(1, result.Data);
        Assert.AreEqual(123456, result.Data[0].Id);
        Assert.AreEqual("Morning Run", result.Data[0].Name);
    }

    [TestMethod]
    public async Task GetActivitiesAsync_WithUnauthorized_ReturnsErrorResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.Unauthorized)
        };
        var httpClient = new HttpClient(handler);
        var auth = new StravaAuthorization("client_id", "client_secret", "access_token", "refresh_token", expires: DateTime.Now.AddMonths(1));
        var session = new StravaSession(auth, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.GetActivitiesAsync(cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.Data);
        Assert.IsNotNull(result.Error);
    }

    [TestMethod]
    public async Task GetActivityAsync_WithValidResponse_ReturnsDetailedActivity()
    {
        // Arrange
        var jsonResponse = @"{
            ""id"": 789012,
            ""name"": ""Evening Ride"",
            ""distance"": 15000.0,
            ""moving_time"": 3600.0,
            ""elapsed_time"": 3700.0,
            ""total_elevation_gain"": 200.0,
            ""type"": ""Ride"",
            ""sport_type"": ""Ride"",
            ""start_date"": ""2023-01-02T18:00:00Z"",
            ""average_speed"": 4.17,
            ""max_speed"": 8.5,
            ""athlete"": { ""id"": 1, ""resource_state"": 1 },
            ""description"": ""Great ride"",
            ""trainer"": false,
            ""commute"": false
        }";
        var handler = new MockHttpMessageHandler { JsonResponse = jsonResponse };
        var httpClient = new HttpClient(handler);
        var auth = new StravaAuthorization("client_id", "client_secret", "access_token", "refresh_token", expires: DateTime.Now.AddMonths(1));
        var session = new StravaSession(auth, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.GetActivityAsync(789012, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(789012, result.Data.Id);
        Assert.AreEqual("Evening Ride", result.Data.Name);
        Assert.AreEqual("Great ride", result.Data.Description);
    }

    [TestMethod]
    public async Task GetActivityAsync_WithNotFound_ReturnsErrorResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.NotFound)
        };
        var httpClient = new HttpClient(handler);
        var auth = new StravaAuthorization("client_id", "client_secret", "access_token", "refresh_token", expires: DateTime.Now.AddMonths(1));
        var session = new StravaSession(auth, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.GetActivityAsync(999999, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.Data);
        Assert.IsNotNull(result.Error);
    }

    [TestMethod]
    public async Task UpdateActivityAsync_WithValidRequest_ReturnsUpdatedActivity()
    {
        // Arrange
        var jsonResponse = @"{
            ""id"": 123456,
            ""name"": ""Updated Activity Name"",
            ""distance"": 5000.0,
            ""moving_time"": 1800.0,
            ""elapsed_time"": 1900.0,
            ""total_elevation_gain"": 100.0,
            ""type"": ""Run"",
            ""sport_type"": ""Run"",
            ""start_date"": ""2023-01-01T08:00:00Z"",
            ""average_speed"": 2.78,
            ""max_speed"": 4.5,
            ""athlete"": { ""id"": 1, ""resource_state"": 1 },
            ""description"": ""Updated description"",
            ""trainer"": false,
            ""commute"": true
        }";
        var handler = new MockHttpMessageHandler { JsonResponse = jsonResponse };
        var httpClient = new HttpClient(handler);
        var auth = new StravaAuthorization("client_id", "client_secret", "access_token", "refresh_token", expires: DateTime.Now.AddMonths(1));
        var session = new StravaSession(auth, httpClient);
        var api = session.ActivitiesApi();
        var updateActivity = new UpdatableActivity { Name = "Updated Activity Name", Commute = true };

        // Act
        var result = await api.UpdateActivityAsync(123456, updateActivity, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual("Updated Activity Name", result.Data.Name);
        Assert.IsTrue(result.Data.Commute);
    }

    [TestMethod]
    public async Task UpdateActivityAsync_WithForbidden_ReturnsErrorResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.Forbidden)
        };
        var httpClient = new HttpClient(handler);
        var auth = new StravaAuthorization("client_id", "client_secret", "access_token", "refresh_token", expires: DateTime.Now.AddMonths(1));
        var session = new StravaSession(auth, httpClient);
        var api = session.ActivitiesApi();
        var updateActivity = new UpdatableActivity { Name = "Updated Activity Name" };

        // Act
        var result = await api.UpdateActivityAsync(123456, updateActivity, TestContext.CancellationToken);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.Data);
        Assert.IsNotNull(result.Error);
    }

    [TestMethod]
    public async Task CreateActivityAsync_WithNoOptionalArgument_ReturnsDetailedAsctivity()
    {
        // Arrange
        var handler = new MockHttpMessageHandler
        {
            JsonResponse = @"{
  ""id"": 123456789,
  ""resource_state"": 3,
  ""external_id"": null,
  ""upload_id"": null,
  ""athlete"": {
    ""id"": 12343545645788,
    ""resource_state"": 1
  },
  ""name"": ""Chill Day"",
  ""distance"": 0,
  ""moving_time"": 18373,
  ""elapsed_time"": 18373,
  ""total_elevation_gain"": 0,
  ""type"": ""Ride"",
  ""sport_type"": ""MountainBikeRide"",
  ""start_date"": ""2018-02-20T18:02:13Z"",
  ""start_date_local"": ""2018-02-20T10:02:13Z"",
  ""timezone"": ""(GMT-08:00) America/Los_Angeles"",
  ""utc_offset"": -28800,
  ""achievement_count"": 0,
  ""kudos_count"": 0,
  ""comment_count"": 0,
  ""athlete_count"": 1,
  ""photo_count"": 0,
  ""map"": {
    ""id"": ""a12345678908766"",
    ""polyline"": null,
    ""resource_state"": 3
  },
  ""trainer"": false,
  ""commute"": false,
  ""manual"": true,
  ""private"": false,
  ""flagged"": false,
  ""gear_id"": ""b453542543"",
  ""from_accepted_tag"": null,
  ""average_speed"": 0,
  ""max_speed"": 0,
  ""device_watts"": false,
  ""has_heartrate"": false,
  ""pr_count"": 0,
  ""total_photo_count"": 0,
  ""has_kudoed"": false,
  ""workout_type"": null,
  ""description"": null,
  ""calories"": 0,
  ""segment_efforts"": []
}"
        };
        var athleteId = 123456789;
        var expected = "https://www.strava.com/api/v3/activities";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.CreateActivityAsync("Chill Day", SportTypes.MountainBikeRide, DateTime.Parse("2018-02-20T18:02:13Z"), 18373, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(athleteId, result.Data.Id);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        Assert.AreEqual("Chill Day", result.Data.Name);
        Assert.AreEqual(SportTypes.MountainBikeRide.ToString(), result.Data.SportType);
        Assert.AreEqual(DateTime.Parse("2018-02-20T18:02:13Z"), result.Data.StartDate.ToLocalTime());
        Assert.AreEqual(18373, result.Data.ElapsedTime);
    }

    [TestMethod]
    public async Task CreateActivityAsync_WithAllOptionalArguments_ReturnsDetailedActivity()
    {
        // Arrange
        var handler = new MockHttpMessageHandler
        {
            JsonResponse = @"{
  ""id"": 123456789,
  ""resource_state"": 3,
  ""external_id"": null,
  ""upload_id"": null,
  ""athlete"": {
    ""id"": 12343545645788,
    ""resource_state"": 1
  },
  ""name"": ""Chill Day"",
  ""distance"": 12.345,
  ""moving_time"": 18373,
  ""elapsed_time"": 18373,
  ""total_elevation_gain"": 0,
  ""type"": ""Ride"",
  ""sport_type"": ""MountainBikeRide"",
  ""start_date"": ""2018-02-20T18:02:13Z"",
  ""start_date_local"": ""2018-02-20T10:02:13Z"",
  ""timezone"": ""(GMT-08:00) America/Los_Angeles"",
  ""utc_offset"": -28800,
  ""achievement_count"": 0,
  ""kudos_count"": 0,
  ""comment_count"": 0,
  ""athlete_count"": 1,
  ""photo_count"": 0,
  ""map"": {
    ""id"": ""a12345678908766"",
    ""polyline"": null,
    ""resource_state"": 3
  },
  ""trainer"": false,
  ""commute"": true,
  ""manual"": true,
  ""private"": false,
  ""flagged"": false,
  ""gear_id"": ""b453542543"",
  ""from_accepted_tag"": null,
  ""average_speed"": 0,
  ""max_speed"": 0,
  ""device_watts"": false,
  ""has_heartrate"": false,
  ""pr_count"": 0,
  ""total_photo_count"": 0,
  ""has_kudoed"": false,
  ""workout_type"": null,
  ""description"": ""Evening ride around the park"",
  ""calories"": 0,
  ""segment_efforts"": []
}"
        };
        var athleteId = 123456789;
        var expected = "https://www.strava.com/api/v3/activities";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.CreateActivityAsync(
            "Chill Day",
            SportTypes.MountainBikeRide,
            DateTime.Parse("2018-02-20T18:02:13Z"),
            18373,
            "Ride",
            "Evening ride around the park",
            12.345,
            false,
            true,
            TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(athleteId, result.Data.Id);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        Assert.AreEqual("Chill Day", result.Data.Name);
        Assert.AreEqual(SportTypes.MountainBikeRide.ToString(), result.Data.SportType);
        Assert.AreEqual(DateTime.Parse("2018-02-20T18:02:13Z"), result.Data.StartDate.ToLocalTime());
        Assert.AreEqual(18373, result.Data.ElapsedTime);

        Assert.IsTrue(result.Data.Commute);
        Assert.AreEqual("Ride", result.Data.Type);
        Assert.AreEqual("Evening ride around the park", result.Data.Description);
        Assert.AreEqual(12.345, result.Data.Distance);
        Assert.IsFalse(result.Data.Trainer);
    }

    [TestMethod]
    public async Task ListActivityCommentsAsync_WithDefaultParameters_ReturnsComments()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""id"": 12345678987654320,
    ""activity_id"": 12345678987654320,
    ""post_id"": null,
    ""resource_state"": 2,
    ""text"": ""Good job and keep the cat pictures coming!"",
    ""mentions_metadata"": null,
    ""created_at"": ""2018-02-08T19:25:39Z"",
    ""athlete"": {
      ""firstname"": ""Peter"",
      ""lastname"": ""S""
    },
    ""cursor"": ""abc123%20""
  }
]"
        };

        var expected = "https://www.strava.com/api/v3/activities/12345678987654320/comments";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.ListActivityCommentsAsync(12345678987654320);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(1, result.Data);

        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());
        Assert.AreEqual(12345678987654320, result.Data[0].Id);
        Assert.AreEqual(12345678987654320, result.Data[0].ActivityId);
        Assert.AreEqual("Good job and keep the cat pictures coming!", result.Data[0].Text);
        Assert.AreEqual(DateTime.Parse("2018-02-08T19:25:39Z"), result.Data[0].CreatedAt.ToLocalTime());
        Assert.AreEqual("Peter", result.Data[0].Athlete?.Firstname);
        Assert.AreEqual("S", result.Data[0].Athlete?.Lastname);
    }

    [TestMethod]
    public async Task ListActivityCommentsAsync_WithParameters_ReturnsComments()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""id"": 12345678987654320,
    ""activity_id"": 12345678987654320,
    ""post_id"": null,
    ""resource_state"": 2,
    ""text"": ""Good job and keep the cat pictures coming!"",
    ""mentions_metadata"": null,
    ""created_at"": ""2018-02-08T19:25:39Z"",
    ""athlete"": {
      ""firstname"": ""Peter"",
      ""lastname"": ""S""
    },
    ""cursor"": ""abc123%20""
  }
]"
        };

        var expected = "https://www.strava.com/api/v3/activities/12345678987654320/comments?page_size=10&after_cursor=abc123%2b";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.ListActivityCommentsAsync(12345678987654320, pageSize: 10, afterCursor: "abc123 ");

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(1, result.Data);

        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());
        Assert.AreEqual(12345678987654320, result.Data[0].Id);
        Assert.AreEqual(12345678987654320, result.Data[0].ActivityId);
        Assert.AreEqual("Good job and keep the cat pictures coming!", result.Data[0].Text);
        Assert.AreEqual(DateTime.Parse("2018-02-08T19:25:39Z"), result.Data[0].CreatedAt.ToLocalTime());
        Assert.AreEqual("Peter", result.Data[0].Athlete?.Firstname);
        Assert.AreEqual("S", result.Data[0].Athlete?.Lastname);
    }

    [TestMethod]
    public async Task ListActivityKudoersAsync_ReturnsKudoers()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""firstname"": ""Peter"",
    ""lastname"": ""S""
  }
]"
        };

        var expected = "https://www.strava.com/api/v3/activities/12345678987654320/kudos";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.ListActivityKudoersAsync(12345678987654320);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(1, result.Data);

        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());
        Assert.HasCount(1, result.Data);
        Assert.AreEqual("Peter", result.Data[0].Firstname);
        Assert.AreEqual("S", result.Data[0].Lastname);
    }

    [TestMethod]
    public async Task ListActivityKudoersAsync_WithParameters_ReturnsKudoers()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""firstname"": ""Peter"",
    ""lastname"": ""S""
  }
]"
        };

        var expected = "https://www.strava.com/api/v3/activities/12345678987654320/kudos?page=5&per_page=200";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.ListActivityKudoersAsync(12345678987654320, 5, 200);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(1, result.Data);

        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());
        Assert.HasCount(1, result.Data);
        Assert.AreEqual("Peter", result.Data[0].Firstname);
        Assert.AreEqual("S", result.Data[0].Lastname);
    }


    [TestMethod]
    public async Task ListActivityLaps_ReturnsLaps()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""id"": 12345678987654320,
    ""resource_state"": 2,
    ""name"": ""Lap 1"",
    ""activity"": {
      ""id"": 12345678987654320,
      ""resource_state"": 1
    },
    ""athlete"": {
      ""id"": 12345678987654321,
      ""resource_state"": 1
    },
    ""elapsed_time"": 1691,
    ""moving_time"": 1587,
    ""start_date"": ""2018-02-08T14:13:37Z"",
    ""start_date_local"": ""2018-02-08T06:13:37Z"",
    ""distance"": 8046.72,
    ""start_index"": 0,
    ""end_index"": 1590,
    ""total_elevation_gain"": 270,
    ""average_speed"": 4.76,
    ""max_speed"": 9.4,
    ""average_cadence"": 79,
    ""device_watts"": true,
    ""average_watts"": 228.2,
    ""lap_index"": 1,
    ""split"": 1
  }
]"
        };

        var expected = "https://www.strava.com/api/v3/activities/12345678987654320/laps";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.ListActivityLaps(12345678987654320);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(1, result.Data);

        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        Assert.HasCount(1, result.Data);
        Assert.AreEqual(12345678987654320, result.Data[0].Id);
        Assert.AreEqual("Lap 1", result.Data[0].Name);
        Assert.AreEqual(12345678987654320, result.Data[0].Activity.Id);
        Assert.AreEqual(12345678987654321, result.Data[0].Athlete.Id);
        Assert.AreEqual(TimeSpan.FromSeconds(1691), result.Data[0].ElapsedTime);
        Assert.AreEqual(TimeSpan.FromSeconds(1587), result.Data[0].MovingTime);
        Assert.AreEqual(DateTime.Parse("2018-02-08T14:13:37Z"), result.Data[0].StartDate.ToLocalTime());
        Assert.AreEqual(DateTime.Parse("2018-02-08T06:13:37Z"), result.Data[0].StartDateLocal.ToLocalTime());
        Assert.AreEqual(8046.72, result.Data[0].Distance);
        Assert.AreEqual(0, result.Data[0].StartIndex);
        Assert.AreEqual(1590, result.Data[0].EndIndex);
        Assert.AreEqual(270, result.Data[0].TotalElevationGain);
        Assert.AreEqual(4.76, result.Data[0].AverageSpeed);
        Assert.AreEqual(9.4, result.Data[0].MaxSpeed);
        Assert.AreEqual(79, result.Data[0].AverageCadence);
        Assert.AreEqual(1, result.Data[0].LapIndex);
        Assert.AreEqual(1, result.Data[0].Split);
    }

    [TestMethod]
    public async Task GetActivityZones_ReturnsActivityZones()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""score"": 1234,
    ""distribution_buckets"": [
      {
        ""min"": 1,
        ""max"": 2,
        ""time"": 123
      }
    ],
    ""type"": ""heartrate"",
    ""sensor_based"": true,
    ""points"": 12,
    ""custom_zones"": true,
    ""max"": 14
  }
]"
        };

        var expected = "https://www.strava.com/api/v3/activities/12345678987654320/zones";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ActivitiesApi();

        // Act
        var result = await api.GetActivityZones(12345678987654320);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(1, result.Data);

        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());
        Assert.HasCount(1, result.Data);
        Assert.AreEqual(1234, result.Data[0].Score);
        Assert.IsTrue(result.Data[0].SensorBased);
        Assert.IsTrue(result.Data[0].CustomZones);
        Assert.AreEqual(12, result.Data[0].Points);
        Assert.AreEqual(14, result.Data[0].Max);
        Assert.AreEqual("heartrate", result.Data[0].Type);

        Assert.HasCount(1, result.Data[0].DistributionBuckets);
        Assert.AreEqual(1, result.Data[0].DistributionBuckets[0].Min);
        Assert.AreEqual(2, result.Data[0].DistributionBuckets[0].Max);
        Assert.AreEqual(123, result.Data[0].DistributionBuckets[0].Time);
    }


    [TestMethod]
    public void AddOptionalConent_WithNullToStringValue_AddsEmptyString()
    {
        // Arrange
        var content = new MultipartFormDataContent();
        var payload = new TestContentClass();

        // Act
        var result = ActivitiesApiExtensions.AddOptionalContent(content, payload, "payload");

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, content.Count());
        Assert.AreEqual("payload", content.First().Headers.ContentDisposition!.Parameters.First().Value!.Trim('"'));
    }

    internal class TestContentClass : Object
    {
        override public string? ToString() => null;
    }

    public TestContext TestContext { get; set; }
}

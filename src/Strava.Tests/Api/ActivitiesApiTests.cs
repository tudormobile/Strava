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
    public void CreateActivityAsync_ThrowsNotImplementedException()
    {
        // Arrange
        var auth = new StravaAuthorization();
        var session = new StravaSession(auth);
        var api = session.ActivitiesApi();

        // Act & Assert
        Assert.ThrowsExactlyAsync<NotImplementedException>(async () =>
            await api.CreateActivityAsync("Test", "Run", "Run", DateTime.Now, 1800, "Description", 5000, false, false));
    }

    [TestMethod]
    public void ListActivityCommentsAsync_ThrowsNotImplementedException()
    {
        // Arrange
        var auth = new StravaAuthorization();
        var session = new StravaSession(auth);
        var api = session.ActivitiesApi();

        // Act & Assert
        Assert.ThrowsExactlyAsync<NotImplementedException>(async () =>
            await api.ListActivityCommentsAsync(123456));
    }

    [TestMethod]
    public void ListActivityKudoersAsync_ThrowsNotImplementedException()
    {
        // Arrange
        var auth = new StravaAuthorization();
        var session = new StravaSession(auth);
        var api = session.ActivitiesApi();

        // Act & Assert
        Assert.ThrowsExactlyAsync<NotImplementedException>(async () =>
            await api.ListActivityKudoersAsync(123456));
    }

    [TestMethod]
    public void ListActivityLaps_ThrowsNotImplementedException()
    {
        // Arrange
        var auth = new StravaAuthorization();
        var session = new StravaSession(auth);
        var api = session.ActivitiesApi();

        // Act & Assert
        Assert.ThrowsExactlyAsync<NotImplementedException>(async () =>
            await api.ListActivityLaps(123456));
    }

    [TestMethod]
    public void GetActivityZones_ThrowsNotImplementedException()
    {
        // Arrange
        var auth = new StravaAuthorization();
        var session = new StravaSession(auth);
        var api = session.ActivitiesApi();

        // Act & Assert
        Assert.ThrowsExactlyAsync<NotImplementedException>(async () =>
            await api.GetActivityZones(123456));
    }

    public TestContext TestContext { get; set; }
}

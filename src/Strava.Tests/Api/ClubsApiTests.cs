using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Api;

[TestClass]
public class ClubsApiTests
{
    [TestMethod]
    public async Task GetClubAsync_ReturnsDetailedClubResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"{
  ""id"": 1234,
  ""resource_state"": 3,
  ""name"": ""Team Strava Cycling"",
  ""profile_medium"": ""https://xxxxx.cloudfront.net/pictures/clubs/1/1582/4/medium.jpg"",
  ""profile"": ""https://xxxxx.cloudfront.net/pictures/clubs/1/1582/4/large.jpg"",
  ""cover_photo"": ""https://xxxxx.cloudfront.net/pictures/clubs/1/4328276/1/large.jpg"",
  ""cover_photo_small"": ""https://xxxxx.cloudfront.net/pictures/clubs/1/4328276/1/small.jpg"",
  ""sport_type"": ""cycling"",
  ""activity_types"": [
    ""Ride"",
    ""VirtualRide"",
    ""EBikeRide"",
    ""Velomobile"",
    ""Handcycle""
  ],
  ""city"": ""San Francisco"",
  ""state"": ""California"",
  ""country"": ""United States"",
  ""private"": true,
  ""member_count"": 116,
  ""featured"": false,
  ""verified"": false,
  ""url"": ""team-strava-bike"",
  ""membership"": ""member"",
  ""admin"": false,
  ""owner"": false,
  ""description"": ""Private club for Cyclists who work at Strava."",
  ""club_type"": ""company"",
  ""post_count"": 29,
  ""owner_id"": 759,
  ""following_count"": 107
}"
        };

        var id = 1234;
        var expected = $"https://www.strava.com/api/v3/clubs/{id}";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ClubsApi();

        // Act
        var result = await api.GetClubAsync(id, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success, result.Error?.Message ?? "Api call failed.");
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var club = result.Data!;
        Assert.AreEqual(1234, club.Id);
        Assert.AreEqual(ResourceStates.Detail, club.ResourceState);
        Assert.AreEqual("Team Strava Cycling", club.Name);
        Assert.AreEqual("https://xxxxx.cloudfront.net/pictures/clubs/1/1582/4/medium.jpg", club.ProfileMedium);
        Assert.AreEqual("https://xxxxx.cloudfront.net/pictures/clubs/1/1582/4/large.jpg", club.Profile);
        Assert.AreEqual("https://xxxxx.cloudfront.net/pictures/clubs/1/4328276/1/large.jpg", club.CoverPhoto);
        Assert.AreEqual("https://xxxxx.cloudfront.net/pictures/clubs/1/4328276/1/small.jpg", club.CoverPhotoSmall);
        Assert.AreEqual(SportTypes.Cycling, club.SportType);
        CollectionAssert.AreEqual(new List<string> { "Ride", "VirtualRide", "EBikeRide", "Velomobile", "Handcycle" }, club.ActivityTypes);
        Assert.AreEqual("San Francisco", club.City);
        Assert.AreEqual("California", club.State);
        Assert.AreEqual("United States", club.Country);
        Assert.IsTrue(club.Private);
        Assert.AreEqual(116, club.MemberCount);
        Assert.IsFalse(club.Featured);
        Assert.IsFalse(club.Verified);
        Assert.AreEqual("team-strava-bike", club.Url);
        Assert.AreEqual("member", club.Membership);
        Assert.IsFalse(club.Admin);
        Assert.IsFalse(club.Owner);
        Assert.AreEqual("Private club for Cyclists who work at Strava.", club.Description);
        Assert.AreEqual("company", club.ClubType);
        Assert.AreEqual(29, club.PostCount);
        Assert.AreEqual(759, club.OwnerId);
        Assert.AreEqual(107, club.FollowingCount);

        Assert.IsTrue(club.ActivityTypes != null && club.ActivityTypes.Count == 5, "ActivityTypes should contain 5 items.");
        Assert.Contains("Ride", club.ActivityTypes, "ActivityTypes should contain 'Ride'.");
        Assert.Contains("VirtualRide", club.ActivityTypes, "ActivityTypes should contain 'VirtualRide'.");
        Assert.Contains("EBikeRide", club.ActivityTypes, "ActivityTypes should contain 'EBikeRide'.");
        Assert.Contains("Velomobile", club.ActivityTypes, "ActivityTypes should contain 'Velomobile'.");
        Assert.Contains("Handcycle", club.ActivityTypes, "ActivityTypes should contain 'Handcycle'.");
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
        var expected = "https://www.strava.com/api/v3/athlete/clubs";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ClubsApi();

        // Act
        var result = await api.ListAthleteClubsAsync(cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success, result.Error?.Message ?? "Api call failed.");
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        Assert.HasCount(1, result.Data);
        Assert.AreEqual(231407, result.Data[0].Id);
        Assert.AreEqual(ResourceStates.Summary, result.Data[0].ResourceState);
        Assert.AreEqual("The Strava Club", result.Data[0].Name);
        Assert.AreEqual("https://xxxxx.cloudfront.net/pictures/clubs/231407/5319085/1/medium.jpg", result.Data[0].ProfileMedium);
        Assert.AreEqual("https://xxxxx.cloudfront.net/pictures/clubs/231407/5319085/1/large.jpg", result.Data[0].Profile);
        Assert.AreEqual("https://xxxxx.cloudfront.net/pictures/clubs/231407/5098428/4/large.jpg", result.Data[0].CoverPhoto);
        Assert.AreEqual("https://xxxxx.cloudfront.net/pictures/clubs/231407/5098428/4/small.jpg", result.Data[0].CoverPhotoSmall);
        Assert.AreEqual(SportTypes.Other, result.Data[0].SportType);
        Assert.AreEqual("San Francisco", result.Data[0].City);
        Assert.AreEqual("California", result.Data[0].State);
        Assert.AreEqual("United States", result.Data[0].Country);
        Assert.IsFalse(result.Data[0].Private);
        Assert.AreEqual(93151, result.Data[0].MemberCount);
        Assert.IsFalse(result.Data[0].Featured);
        Assert.IsTrue(result.Data[0].Verified);
        Assert.AreEqual("strava", result.Data[0].Url);

    }

    [TestMethod]
    public async Task ListClubActivitiesAsync_ReturnsListOfActivities()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""resource_state"": 2,
    ""athlete"": {
      ""resource_state"": 2,
      ""firstname"": ""Peter"",
      ""lastname"": ""S.""
    },
    ""name"": ""World Championship"",
    ""distance"": 2641.7,
    ""moving_time"": 577,
    ""elapsed_time"": 635,
    ""total_elevation_gain"": 8.8,
    ""type"": ""Ride"",
    ""sport_type"": ""MountainBikeRide"",
    ""workout_type"": null
  }
]"
        };

        // Arrange
        long id = 1234;
        var expected = $"https://www.strava.com/api/v3/clubs/{id}/activities";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ClubsApi();

        // Act
        var result = await api.ListClubActivitiesAsync(id, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success, result.Error?.Message ?? "Api call failed.");
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        Assert.HasCount(1, result.Data);
        Assert.AreEqual("World Championship", result.Data[0].Name);
        Assert.AreEqual(2641.7, result.Data[0].Distance);
        Assert.AreEqual(577, result.Data[0].MovingTime);
        Assert.AreEqual(635, result.Data[0].ElapsedTime);
        Assert.AreEqual(8.8, result.Data[0].TotalElevationGain);
        Assert.AreEqual("Ride", result.Data[0].Type);
        Assert.AreEqual(SportTypes.MountainBikeRide, result.Data[0].SportType);

        Assert.IsNotNull(result.Data[0].Athlete);
        Assert.AreEqual("Peter", result.Data[0].Athlete.Firstname);
        Assert.AreEqual("S.", result.Data[0].Athlete.Lastname);
    }

    [TestMethod]
    public async Task ListClubAdministratorsAsync_ReturnsListOfAdministrators()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""resource_state"": 2,
    ""firstname"": ""Peter"",
    ""lastname"": ""S.""
  }
]"
        };

        // Arrange
        long id = 1234;
        var expected = $"https://www.strava.com/api/v3/clubs/{id}/admins";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ClubsApi();

        // Act
        var result = await api.ListClubAdministratorsAsync(id, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success, result.Error?.Message ?? "Api call failed.");
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        Assert.HasCount(1, result.Data);
        Assert.AreEqual("Peter", result.Data[0].Firstname);
        Assert.AreEqual("S.", result.Data[0].Lastname);
    }

    [TestMethod]
    public async Task ListClubMembersAsync_ReturnsListOfMembers()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""resource_state"": 2,
    ""firstname"": ""Peter"",
    ""lastname"": ""S."",
    ""membership"": ""member"",
    ""admin"": false,
    ""owner"": true
  }
]"
        };

        // Arrange
        long id = 1234;
        var expected = $"https://www.strava.com/api/v3/clubs/{id}/members";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.ClubsApi();

        // Act
        var result = await api.ListClubMembersAsync(id, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success, result.Error?.Message ?? "Api call failed.");
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        Assert.HasCount(1, result.Data);
        Assert.AreEqual(ResourceStates.Summary, result.Data[0].ResourceState);
        Assert.AreEqual("Peter", result.Data[0].Firstname);
        Assert.AreEqual("S.", result.Data[0].Lastname);
        Assert.AreEqual("member", result.Data[0].Membership);
        Assert.IsFalse(result.Data[0].Admin);
        Assert.IsTrue(result.Data[0].Owner);
    }

    public TestContext TestContext { get; set; }
}

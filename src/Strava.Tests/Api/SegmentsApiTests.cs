using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Api;

[TestClass]
public class SegmentsApiTests
{
    [TestMethod]
    public async Task GetSegmentAsync_ReturnsDetailedSegmentResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = jsonDetailedSegment
        };

        var id = 229781;
        var expected = $"https://www.strava.com/api/v3/segments/{id}";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.SegmentsApi();

        // Act
        var result = await api.GetSegmentAsync(id, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var segment = result.Data!;

        Assert.AreEqual(229781, segment.Id);
        Assert.AreEqual(ResourceStates.Detail, segment.ResourceState);
        Assert.AreEqual("Hawk Hill", segment.Name);
        Assert.AreEqual("Ride", segment.ActivityType);
        Assert.AreEqual(2684.82f, segment.Distance, 0.01f);
        Assert.AreEqual(5.7f, segment.AverageGrade, 0.01f);
        Assert.AreEqual(14.2f, segment.MaximumGrade, 0.01f);
        Assert.AreEqual(245.3f, segment.ElevationHigh, 0.01f);
        Assert.AreEqual(92.4f, segment.ElevationLow, 0.01f);
        Assert.AreEqual(37.8331119f, segment.StartLatlng.Latitude, 0.0001f);
        Assert.AreEqual(-122.4834356f, segment.StartLatlng.Longitude, 0.0001f);
        Assert.AreEqual(37.8280722f, segment.EndLatLng.Latitude, 0.0001f);
        Assert.AreEqual(-122.4981393f, segment.EndLatLng.Longitude, 0.0001f);
        Assert.AreEqual(1, segment.ClimbCategory);
        Assert.AreEqual("San Francisco", segment.City);
        Assert.AreEqual("CA", segment.State);
        Assert.AreEqual("United States", segment.Country);
        Assert.IsFalse(segment.Private);
        Assert.IsFalse(segment.Hazardous);
        Assert.IsFalse(segment.Starred);
        Assert.AreEqual(new DateTime(2009, 09, 21, 20, 29, 41, DateTimeKind.Utc), segment.CreatedAt);
        Assert.AreEqual(new DateTime(2018, 02, 15, 09, 04, 18, DateTimeKind.Utc), segment.UpdatedAt);
        Assert.AreEqual(155.733f, segment.TotalElevationGain, 0.001f);
        Assert.IsNotNull(segment.Map);
        Assert.AreEqual("s229781", segment.Map.Id);
        Assert.AreEqual(ResourceStates.Detail, segment.Map.ResourceState);
        Assert.AreEqual("}g|eFnpqjVl@En@Md@HbAd@d@^h@Xx@VbARjBDh@OPQf@w@d@k@XKXDFPH\\EbGT`AV`@v@|@NTNb@?XOb@cAxAWLuE@eAFMBoAv@eBt@q@b@}@tAeAt@i@dAC`AFZj@dB?~@[h@MbAVn@b@b@\\d@Eh@Qb@_@d@eB|@c@h@WfBK|AMpA?VF\\\\t@f@t@h@j@|@b@hCb@b@XTd@Bl@GtA?jAL`ALp@Tr@RXd@Rx@Pn@^Zh@Tx@Zf@`@FTCzDy@f@Yx@m@n@Op@VJr@", segment.Map.Polyline);
        Assert.AreEqual(309974, segment.EffortCount);
        Assert.AreEqual(30623, segment.AthleteCount);
        Assert.AreEqual(2428, segment.StarCount);
        Assert.IsNotNull(segment.AthleteSegmentStats);
        Assert.AreEqual(TimeSpan.FromSeconds(553), segment.AthleteSegmentStats.PrElapsedTime);
        Assert.AreEqual(new DateOnly(1993, 04, 03), segment.AthleteSegmentStats.PrDate);
        Assert.AreEqual(2, segment.AthleteSegmentStats.EffortCount);
    }

    [TestMethod]
    public async Task ListStarredSegmentsAsync_ReturnsListOfSegmentsResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = $"[{jsonDetailedSegment}]"
        };

        var id = 229781;
        var expected = $"https://www.strava.com/api/v3/segments/starred";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.SegmentsApi();

        // Act
        var result = await api.ListStarredSegmentsAsync(cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var segments = result.Data!;
        Assert.HasCount(1, segments);
        Assert.AreEqual(id, segments[0].Id);
    }

    [TestMethod]
    public async Task StarSegmentsAsync_ReturnsSegmentResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = jsonDetailedSegment
        };

        var id = 229781;
        var expected = $"https://www.strava.com/api/v3/segments/{id}/starred";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.SegmentsApi();

        // Act
        var result = await api.StarSegmentAsync(id, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var segments = result.Data!;
        Assert.AreEqual(id, segments.Id);
    }

    [TestMethod]
    public async Task UnstarSegmentsAsync_ReturnsSegmentResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = jsonDetailedSegment
        };

        var id = 229781;
        var expected = $"https://www.strava.com/api/v3/segments/{id}/starred";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.SegmentsApi();

        // Act
        var result = await api.UnstarSegmentAsync(id, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var segments = result.Data!;
        Assert.AreEqual(id, segments.Id);
    }

    [TestMethod]
    public async Task ExploreSegmentsAsync_ReturnsSegmentsResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = $"{{\"segments\": [{jsonSegment},{jsonSegment},{jsonSegment}]}}"
        };

        Bounds bounds = (37.821362, -122.505373, 37.842038, -122.465977);
        var expected = "https://www.strava.com/api/v3/segments/explore?bounds={(37.821362%2c-122.505373)%2c(37.842038%2c-122.465977)}";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.SegmentsApi();

        // Act
        var result = await api.ExploreSegmentsAsync(bounds, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var segmentList = result.Data!;
        Assert.HasCount(3, segmentList.Segments);
        Assert.AreEqual(229781, segmentList.Segments[0].Id);
    }

    [TestMethod]
    public async Task ListSegmentEffortsAsync_ReturnsSegmentEffortsResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = $"[{jsonFullSegment},{jsonFullSegment},{jsonFullSegment}]"
        };
        var id = 123;
        var expected = $"https://www.strava.com/api/v3/segment_efforts?segment_id={id}";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.SegmentsApi();

        // Act
        var result = await api.ListSegmentEffortsAsync(id, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var segments = result.Data!;
        Assert.HasCount(3, segments);
        Assert.AreEqual(123456789, segments[0].Id);
    }

    [TestMethod]
    public async Task GetSegmentEffortsAsync_ReturnsSegmentEffortsResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = jsonFullSegment
        };
        var id = 123456789;
        var expected = $"https://www.strava.com/api/v3/segment_efforts/{id}";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.SegmentsApi();

        // Act
        var result = await api.GetSegmentEffortAsync(id, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var segment = result.Data!;
        Assert.AreEqual(id, segment.Id);
    }

    public TestContext TestContext { get; set; }

    private static readonly string jsonDetailedSegment = @"{
  ""id"": 229781,
  ""resource_state"": 3,
  ""name"": ""Hawk Hill"",
  ""activity_type"": ""Ride"",
  ""distance"": 2684.82,
  ""average_grade"": 5.7,
  ""maximum_grade"": 14.2,
  ""elevation_high"": 245.3,
  ""elevation_low"": 92.4,
  ""start_latlng"": [
    37.8331119,
    -122.4834356
  ],
  ""end_latlng"": [
    37.8280722,
    -122.4981393
  ],
  ""climb_category"": 1,
  ""city"": ""San Francisco"",
  ""state"": ""CA"",
  ""country"": ""United States"",
  ""private"": false,
  ""hazardous"": false,
  ""starred"": false,
  ""created_at"": ""2009-09-21T20:29:41Z"",
  ""updated_at"": ""2018-02-15T09:04:18Z"",
  ""total_elevation_gain"": 155.733,
  ""map"": {
    ""id"": ""s229781"",
    ""polyline"": ""}g|eFnpqjVl@En@Md@HbAd@d@^h@Xx@VbARjBDh@OPQf@w@d@k@XKXDFPH\\EbGT`AV`@v@|@NTNb@?XOb@cAxAWLuE@eAFMBoAv@eBt@q@b@}@tAeAt@i@dAC`AFZj@dB?~@[h@MbAVn@b@b@\\d@Eh@Qb@_@d@eB|@c@h@WfBK|AMpA?VF\\\\t@f@t@h@j@|@b@hCb@b@XTd@Bl@GtA?jAL`ALp@Tr@RXd@Rx@Pn@^Zh@Tx@Zf@`@FTCzDy@f@Yx@m@n@Op@VJr@"",
    ""resource_state"": 3
  },
  ""effort_count"": 309974,
  ""athlete_count"": 30623,
  ""star_count"": 2428,
  ""athlete_segment_stats"": {
    ""pr_elapsed_time"": 553,
    ""pr_date"": ""1993-04-03"",
    ""effort_count"": 2
  }
}";
    private static readonly string jsonSegment = @"{
      ""id"": 229781,
      ""resource_state"": 2,
      ""name"": ""Hawk Hill"",
      ""climb_category"": 1,
      ""climb_category_desc"": ""4"",
      ""avg_grade"": 5.7,
      ""start_latlng"": [
        37.8331119,
        -122.4834356
      ],
      ""end_latlng"": [
        37.8280722,
        -122.4981393
      ],
      ""elev_difference"": 152.8,
      ""distance"": 2684.8,
      ""points"": ""}g|eFnpqjVl@En@Md@HbAd@d@^h@Xx@VbARjBDh@OPQf@w@d@k@XKXDFPH\\EbGT`AV`@v@|@NTNb@?XOb@cAxAWLuE@eAFMBoAv@eBt@q@b@}@tAeAt@i@dAC`AFZj@dB?~@[h@MbAVn@b@b@\\d@Eh@Qb@_@d@eB|@c@h@WfBK|AMpA?VF\\\\t@f@t@h@j@|@b@hCb@b@XTd@Bl@GtA?jAL`ALp@Tr@RXd@Rx@Pn@^Zh@Tx@Zf@`@FTCzDy@f@Yx@m@n@Op@VJr@"",
      ""starred"": false
    }";
    private static readonly string jsonFullSegment = @"{
    ""id"": 123456789,
    ""resource_state"": 2,
    ""name"": ""Alpe d'Huez"",
    ""activity"": {
      ""id"": 1234567890,
      ""resource_state"": 1
    },
    ""athlete"": {
      ""id"": 123445678689,
      ""resource_state"": 1
    },
    ""elapsed_time"": 1657,
    ""moving_time"": 1642,
    ""start_date"": ""2007-09-15T08:15:29Z"",
    ""start_date_local"": ""2007-09-15T09:15:29Z"",
    ""distance"": 6148.92,
    ""start_index"": 1102,
    ""end_index"": 1366,
    ""device_watts"": false,
    ""average_watts"": 220.2,
    ""segment"": {
      ""id"": 788127,
      ""resource_state"": 2,
      ""name"": ""Alpe d'Huez"",
      ""activity_type"": ""Ride"",
      ""distance"": 6297.46,
      ""average_grade"": 4.8,
      ""maximum_grade"": 16.3,
      ""elevation_high"": 416,
      ""elevation_low"": 104.6,
      ""start_latlng"": [
        52.98501000581467,
        -3.1869720001197366
      ],
      ""end_latlng"": [
        53.02204074375785,
        -3.2039630001245736
      ],
      ""climb_category"": 2,
      ""city"": ""Le Bourg D'Oisans"",
      ""state"": ""RA"",
      ""country"": ""France"",
      ""private"": false,
      ""hazardous"": false,
      ""starred"": false
    },
    ""kom_rank"": null,
    ""pr_rank"": null,
    ""achievements"": []
  }";
}

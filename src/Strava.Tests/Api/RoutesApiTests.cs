using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace Strava.Tests.Api;

[TestClass]
public class RoutesApiTests
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task GetRouteAsync_ReturnsRouteResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = _json
        };

        var id = 1;
        var expected = $"https://www.strava.com/api/v3/routes/{id}";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.RoutesApi();

        // Act
        var result = await api.GetRouteAsync(id, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var route = result.Data!;

        Assert.AreEqual(1, route.Id);
        Assert.AreEqual("aeiou", route.Name);
        Assert.AreEqual("aeiou", route.Description);
        Assert.AreEqual(0.8008282f, route.Distance);
        Assert.IsTrue(route.Private);
        Assert.AreEqual(DateTime.Parse("2000-01-23T04:56:07.000+00:00").ToUniversalTime(), route.CreatedAt.ToUniversalTime());
        Assert.AreEqual(6.0274563f, route.ElevationGain);
        Assert.AreEqual(5, route.Type);
        Assert.AreEqual(TimeSpan.FromSeconds(7), route.EstimatedMovingTime);
        Assert.HasCount(1, route.Waypoints);
        Assert.HasCount(1, route.Segments);
        Assert.IsTrue(route.Starred);
        Assert.AreEqual(DateTime.Parse("2000-01-23T04:56:07.000+00:00").ToUniversalTime(), route.UpdatedAt.ToUniversalTime());
        Assert.AreEqual(2, route.SubType);
        Assert.AreEqual("aeiou", route.IdStr);
        Assert.AreEqual("aeiou", route.Map.SummaryPolyline);
        Assert.AreEqual(TimeSpan.FromSeconds(5), route.Timestamp);
    }

    [TestMethod]
    public async Task ListAthleteRoutesAsync_ReturnsRoutesResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = _jsonRoutes
        };

        var id = 1;
        var page = 1;
        var perPage = 2;
        var expected = $"https://www.strava.com/api/v3/athletes/{id}/routes?page=1&per_page=2";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.RoutesApi();

        // Act
        var result = await api.ListAthleteRoutesAsync(id, page, perPage, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var routes = result.Data!;

        Assert.HasCount(1, routes);
    }

    [TestMethod]
    public async Task ExportRouteTCXAsync_ReturnsTcxDocumentResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage()
            {
                Content = new StringContent(_tcxDocument),
            }
        };

        var id = 1;
        var expected = $"https://www.strava.com/api/v3/routes/{id}/export_tcx";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.RoutesApi();

        // Act
        var result = await api.ExportRouteTCXAsync(id, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var doc = result.Data!;
        Assert.HasCount(1, doc.Activities);
    }

    [TestMethod]
    public async Task ExportRouteGPXAsync_ReturnsTcxDocumentResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage()
            {
                Content = new StringContent(_gpxDocument),
            }
        };

        var id = 1;
        var expected = $"https://www.strava.com/api/v3/routes/{id}/export_gpx";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.RoutesApi();

        // Act
        var result = await api.ExportRouteGPXAsync(id, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var doc = result.Data!;
        Assert.HasCount(1, doc.Tracks);
    }

    private readonly string _json = @"{
  ""private"" : true,
  ""distance"" : 0.8008282,
  ""athlete"" : """",
  ""description"" : ""aeiou"",
  ""created_at"" : ""2000-01-23T04:56:07.000+00:00"",
  ""elevation_gain"" : 6.0274563,
  ""type"" : 5,
  ""estimated_moving_time"" : 7,
  ""waypoints"" : [ {
    ""distance_into_route"" : 9.36931,
    ""target_latlng"" : """",
    ""description"" : ""aeiou"",
    ""categories"" : [ ""aeiou"" ],
    ""title"" : ""aeiou"",
    ""latlng"" : """"
  } ],
  ""segments"" : [ {
    ""country"" : ""aeiou"",
    ""private"" : true,
    ""distance"" : 3.6160767,
    ""average_grade"" : 2.027123,
    ""maximum_grade"" : 4.145608,
    ""climb_category"" : 1,
    ""city"" : ""aeiou"",
    ""elevation_high"" : 7.386282,
    ""athlete_pr_effort"" : {
      ""pr_elapsed_time"" : 6,
      ""pr_date"" : ""2000-01-23T04:56:07.000+00:00"",
      ""effort_count"" : 7,
      ""pr_activity_id"" : 1
    },
    ""athlete_segment_stats"" : {
      ""distance"" : 9.965781,
      ""start_date_local"" : ""2000-01-23T04:56:07.000+00:00"",
      ""activity_id"" : 4,
      ""elapsed_time"" : 5,
      ""is_kom"" : true,
      ""id"" : 1,
      ""start_date"" : ""2000-01-23T04:56:07.000+00:00""
    },
    ""start_latlng"" : """",
    ""elevation_low"" : 1.2315135,
    ""end_latlng"" : """",
    ""activity_type"" : ""Ride"",
    ""name"" : ""aeiou"",
    ""id"" : 9,
    ""state"" : ""aeiou""
  } ],
  ""starred"" : true,
  ""updated_at"" : ""2000-01-23T04:56:07.000+00:00"",
  ""sub_type"" : 2,
  ""id_str"" : ""aeiou"",
  ""name"" : ""aeiou"",
  ""id"" : 1,
  ""map"" : {
    ""summary_polyline"" : ""aeiou"",
    ""id"" : ""aeiou"",
    ""polyline"" : ""aeiou""
  },
  ""timestamp"" : 5
}";

    private readonly string _jsonRoutes = @"
[
  {
    ""athlete"": {
      ""id"": 0,
      ""resource_state"": 0,
      ""firstname"": ""string"",
      ""lastname"": ""string"",
      ""profile_medium"": ""string"",
      ""profile"": ""string"",
      ""city"": ""string"",
      ""state"": ""string"",
      ""country"": ""string"",
      ""sex"": ""M"",
      ""premium"": true,
      ""summit"": true,
      ""created_at"": ""2025-12-24T11:50:30.492Z"",
      ""updated_at"": ""2025-12-24T11:50:30.492Z""
    },
    ""description"": ""string"",
    ""distance"": 0,
    ""elevation_gain"": 0,
    ""id"": 0,
    ""id_str"": ""string"",
    ""map"": {
      ""id"": ""string"",
      ""polyline"": ""string"",
      ""summary_polyline"": ""string""
    },
    ""name"": ""string"",
    ""private"": true,
    ""starred"": true,
    ""timestamp"": 0,
    ""type"": 0,
    ""sub_type"": 0,
    ""created_at"": ""2025-12-24T11:50:30.492Z"",
    ""updated_at"": ""2025-12-24T11:50:30.492Z"",
    ""estimated_moving_time"": 0,
    ""segments"": [
      {
        ""id"": 0,
        ""name"": ""string"",
        ""activity_type"": ""Ride"",
        ""distance"": 0,
        ""average_grade"": 0,
        ""maximum_grade"": 0,
        ""elevation_high"": 0,
        ""elevation_low"": 0,
        ""start_latlng"": [
          0,
          0
        ],
        ""end_latlng"": [
          0,
          0
        ],
        ""climb_category"": 0,
        ""city"": ""string"",
        ""state"": ""string"",
        ""country"": ""string"",
        ""private"": true,
        ""athlete_pr_effort"": {
          ""pr_activity_id"": 0,
          ""pr_elapsed_time"": 0,
          ""pr_date"": ""2025-12-24T11:50:30.492Z"",
          ""effort_count"": 0
        },
        ""athlete_segment_stats"": {
          ""id"": 0,
          ""activity_id"": 0,
          ""elapsed_time"": 0,
          ""start_date"": ""2025-12-24T11:50:30.492Z"",
          ""start_date_local"": ""2025-12-24T11:50:30.492Z"",
          ""distance"": 0,
          ""is_kom"": true
        }
      }
    ],
    ""waypoints"": [
      {
        ""latlng"": [
          0,
          0
        ],
        ""target_latlng"": [
          0,
          0
        ],
        ""categories"": [
          ""string""
        ],
        ""title"": ""string"",
        ""description"": ""string"",
        ""distance_into_route"": 0
      }
    ]
  }
]
";

    private readonly string _tcxDocument = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no"" ?>
<TrainingCenterDatabase>
  <Activities>
    <Activity Sport=""Biking"">
      <Id>2025-12-24T11:50:30.492Z</Id>
      <Lap StartTime=""2025-12-24T11:50:30.492Z"">
        <TotalTimeSeconds>3600</TotalTimeSeconds>
        <DistanceMeters>10000</DistanceMeters>
        <MaximumSpeed>5.0</MaximumSpeed>
        <Calories>500</Calories>
        <AverageHeartRateBpm>
          <Value>150</Value>
        </AverageHeartRateBpm>
        <Track>
          <Trackpoint>
            <Time>2025-12-24T11:50:30.492Z</Time>
            <Position>
              <LatitudeDegrees>0</LatitudeDegrees>
              <LongitudeDegrees>0</LongitudeDegrees>
            </Position>
            <AltitudeMeters>0</AltitudeMeters>
            <HeartRateBpm>
              <Value>150</Value>
            </HeartRateBpm>
          </Trackpoint>
        </Track>
      </Lap>
    </Activity>
  </Activities>
</TrainingCenterDatabase>
";
    private readonly string _gpxDocument = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no"" ?>
<gpx version=""1.1"" creator=""Strava"">
  <metadata>
    <name>GPX Document</name>
    <desc>Generated GPX Document</desc>
  </metadata>
  <trk>
    <name>Track</name>
    <trkseg>
      <trkpt lat=""0"" lon=""0"">
        <ele>0</ele>
        <time>2025-12-24T11:50:30.492Z</time>
      </trkpt>
    </trkseg>
  </trk>
</gpx>
";
}

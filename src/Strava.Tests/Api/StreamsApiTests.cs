using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace Strava.Tests.Api;

[TestClass]
public class StreamsApiTests
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task GetActivityStreamsAsync_ReturnsListStreamsResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""type"": ""distance"",
    ""data"": [
      2.9,
      5.8,
      8.5,
      11.7,
      15,
      19,
      23.2,
      28,
      32.8,
      38.1,
      43.8,
      49.5
    ],
    ""series_type"": ""distance"",
    ""original_size"": 12,
    ""resolution"": ""high""
  }
]",
        };

        var id = 123;
        var keys = "time,latlng,altitude,velocity_smooth,heartrate,cadence,power,temp,moving,grade_smooth".Split(',');
        var keysByType = true;

        var expected = $"https://www.strava.com/api/v3/activities/{id}/streams?keys=time&keys=latlng&keys=altitude&keys=velocity_smooth&keys=heartrate&keys=cadence&keys=power&keys=temp&keys=moving&keys=grade_smooth&keys_by_type=True";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.StreamsApi();

        // Act
        var result = await api.GetActivityStreamsAsync(id, keys, keysByType, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var streams = result.Data!;
        Assert.HasCount(1, streams);
        Assert.HasCount(12, streams[0].Data);
        Assert.AreEqual("distance", streams[0].Type);
        Assert.AreEqual("distance", streams[0].SeriesType);
        Assert.AreEqual(12, streams[0].OriginalSize);
        Assert.AreEqual("high", streams[0].Resolution);
    }

    [TestMethod]
    public async Task GetSegmentEffortStreamsAsync_ReturnsListStreamsResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[]",
        };

        var id = 123;
        string[] keys = ["heartrate"];
        var keysByType = true;

        var expected = $"https://www.strava.com/api/v3/segment_efforts/{id}/streams?keys=heartrate&keys_by_type=True";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.StreamsApi();

        // Act
        var result = await api.GetSegmentEffortStreamsAsync(id, keys, keysByType, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var streams = result.Data!;
        Assert.HasCount(0, streams);
    }

    [TestMethod]
    public async Task GetSegmentStreamsAsync_ReturnsListStreamsResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"[
  {
    ""type"": ""latlng"",
    ""data"": [
      [
        37.833112,
        -122.483436
      ],
      [
        37.832964,
        -122.483406
      ]
    ],
    ""series_type"": ""distance"",
    ""original_size"": 2,
    ""resolution"": ""high""
  },
  {
    ""type"": ""distance"",
    ""data"": [
      0,
      16.8
    ],
    ""series_type"": ""distance"",
    ""original_size"": 2,
    ""resolution"": ""high""
  },
  {
    ""type"": ""altitude"",
    ""data"": [
      92.4,
      93.4
    ],
    ""series_type"": ""distance"",
    ""original_size"": 2,
    ""resolution"": ""high""
  }
]",
        };

        var id = 123;
        string[] keys = ["heartrate"];
        var keysByType = true;

        var expected = $"https://www.strava.com/api/v3/segments/{id}/streams?keys=heartrate&keys_by_type=True";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.StreamsApi();

        // Act
        var result = await api.GetSegmentStreamsAsync(id, keys, keysByType, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var streams = result.Data!;
        //Assert.HasCount(3, streams);
        //Assert.HasCount(2, streams[0].Data);
        //Assert.AreEqual("latlng", streams[0].Type);
        //Assert.AreEqual("distance", streams[0].SeriesType);
        //Assert.AreEqual(37.833112, streams[0].Points[0].Latitude);
        //Assert.AreEqual(-122.483436, streams[0].Points[0].Longitude);
        //Assert.AreEqual(37.833112, streams[0].Points[0].Latitude);
        //Assert.AreEqual(-122.483436, streams[0].Points[0].Longitude);
    }
}

using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace Strava.Tests.Api;

[TestClass]
public class UploadsApiTests
{
    private readonly string _json = @"{
  ""id"": 123,
  ""id_str"": ""id-string"",
  ""external_id"": ""external-id-string"",
  ""error"": ""error-string"",
  ""status"": ""status-string"",
  ""activity_id"": 456
}
";

    [TestMethod]
    public async Task UploadActivityAsync_ReturnsUploadResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = _json,
        };

        var id = 123;
        var activityId = 456;
        var expected = $"https://www.strava.com/api/v3/uploads";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.UploadsApi();

        var filename = "activity.tcx";
        var name = "Morning Ride";
        var description = "A lovely morning ride.";
        var trainer = "1";
        var commute = "0";
        var dataType = "tcx";
        var externalId = "external-id-string";

        File.WriteAllText(filename, _tcxFileContents);

        // Act
        var result = await api.UploadActivityAsync(filename, name, description, trainer, commute, dataType, externalId, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var upload = result.Data!;
        Assert.AreEqual(id, upload.Id);
        Assert.AreEqual("id-string", upload.IdStr);
        Assert.AreEqual("external-id-string", upload.ExternalId);
        Assert.AreEqual("error-string", upload.Error);
        Assert.AreEqual("status-string", upload.Status);
        Assert.AreEqual(activityId, upload.ActivityId);
    }

    [TestMethod]
    public async Task GetUploadAsync_ReturnsUploadResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = _json,
        };

        var id = 123;
        var activityId = 456;
        var expected = $"https://www.strava.com/api/v3/uploads/{id}";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.UploadsApi();

        // Act
        var result = await api.GetUploadAsync(id, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri?.ToString());

        var upload = result.Data!;
        Assert.AreEqual(id, upload.Id);
        Assert.AreEqual("id-string", upload.IdStr);
        Assert.AreEqual("external-id-string", upload.ExternalId);
        Assert.AreEqual("error-string", upload.Error);
        Assert.AreEqual("status-string", upload.Status);
        Assert.AreEqual(activityId, upload.ActivityId);
    }

    private readonly static string _tcxFileContents =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<TrainingCenterDatabase xmlns=""http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2"">
  <Activities>
    <Activity Sport=""Running"">
      <Id>2024-12-20T10:30:00Z</Id>
      <Lap StartTime=""2024-12-20T10:30:00Z"">
        <TotalTimeSeconds>600.5</TotalTimeSeconds>
        <DistanceMeters>1500.0</DistanceMeters>
        <MaximumSpeed>3.5</MaximumSpeed>
        <Track>
          <Trackpoint>
            <Time>2024-12-20T10:30:00Z</Time>
            <Position>
              <LatitudeDegrees>37.8331119</LatitudeDegrees>
              <LongitudeDegrees>-122.4834356</LongitudeDegrees>
            </Position>
            <AltitudeMeters>100.5</AltitudeMeters>
            <DistanceMeters>0.0</DistanceMeters>
            <HeartRateBpm>
              <Value>150</Value>
            </HeartRateBpm>
          </Trackpoint>
          <Trackpoint>
            <Time>2024-12-20T10:30:30Z</Time>
            <Position>
              <LatitudeDegrees>37.8341119</LatitudeDegrees>
              <LongitudeDegrees>-122.4844356</LongitudeDegrees>
            </Position>
            <AltitudeMeters>105.0</AltitudeMeters>
            <DistanceMeters>50.0</DistanceMeters>
            <HeartRateBpm>
              <Value>155</Value>
            </HeartRateBpm>
          </Trackpoint>
        </Track>
      </Lap>
    </Activity>
  </Activities>
</TrainingCenterDatabase>";

    public TestContext TestContext { get; set; }
}


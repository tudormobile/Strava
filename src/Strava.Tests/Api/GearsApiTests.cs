using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Api;

[TestClass]
public class GearsApiTests
{
    [TestMethod]
    public async Task GetEquipmentAsync_ReturnsDetailedEquipmentResult()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = @"{
  ""id"": ""b1231"",
  ""primary"": false,
  ""resource_state"": 3,
  ""distance"": 388206,
  ""brand_name"": ""BMC"",
  ""model_name"": ""Teammachine"",
  ""frame_type"": 3,
  ""description"": ""My Bike.""
}"
        };

        var id = "b1231";
        var expected = $"https://www.strava.com/api/v3/gear/{id}";
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var api = session.GearsApi();

        // Act
        var result = await api.GetEquipmentAsync(id, TestContext.CancellationToken);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expected, handler.ProvidedRequestUri!.ToString());

        var gear = result.Data!;
        Assert.AreEqual(id, gear.Id);
        Assert.AreEqual(ResourceStates.Detail, gear.ResourceState);
        Assert.AreEqual("BMC-Teammachine", gear.Name);
        Assert.AreEqual("My Bike.", gear.Description);
        Assert.IsFalse(gear.Primary);
        Assert.AreEqual(388206, gear.Distance);
        Assert.AreEqual("BMC", gear.BrandName);
        Assert.AreEqual("Teammachine", gear.ModelName);
        Assert.AreEqual(FrameTypes.Hybrid, gear.FrameType);
    }

    public TestContext TestContext { get; set; }
}

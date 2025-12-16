using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace Strava.Tests.Api;

[TestClass]
public class ApiExtensionsTests
{
    [TestMethod]
    public void ClubsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.ClubsApi());
    }

    [TestMethod]
    public void GearsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.GearsApi());
    }

    [TestMethod]
    public void RoutesApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.RoutesApi());
    }

    [TestMethod]
    public void SegmentsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.SegmentsApi());
    }

    [TestMethod]
    public void StreamApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.StreamApi());
    }

    [TestMethod]
    public void UploadsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.UploadsApi());
    }
}

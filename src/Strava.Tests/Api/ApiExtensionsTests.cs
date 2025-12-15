using System.Diagnostics.CodeAnalysis;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace Strava.Tests.Api;

[TestClass]
public class ApiExtensionsTests
{
    [TestMethod, ExcludeFromCodeCoverage]
    public void ClubsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.ClubsApi());
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void GearsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.GearsApi());
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void RoutesApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.RoutesApi());
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void SegmentsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.SegmentsApi());
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void StreamApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.StreamApi());
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void UploadsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.UploadsApi());
    }
}

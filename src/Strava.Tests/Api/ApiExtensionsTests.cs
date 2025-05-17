using System.Diagnostics.CodeAnalysis;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace Strava.Tests.Api;

[TestClass]
public class ApiExtensionsTests
{
    [TestMethod, ExpectedException(typeof(NotImplementedException)), ExcludeFromCodeCoverage]
    public void ClubsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        var api = session.ClubsApi();
        Assert.IsNotNull(api);
    }

    [TestMethod, ExpectedException(typeof(NotImplementedException)), ExcludeFromCodeCoverage]
    public void GearsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        var api = session.GearsApi();
        Assert.IsNotNull(api);
    }

    [TestMethod, ExpectedException(typeof(NotImplementedException)), ExcludeFromCodeCoverage]
    public void RoutesApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        var api = session.RoutesApi();
        Assert.IsNotNull(api);
    }

    [TestMethod, ExpectedException(typeof(NotImplementedException)), ExcludeFromCodeCoverage]
    public void SegmentsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        var api = session.SegmentsApi();
        Assert.IsNotNull(api);
    }

    [TestMethod, ExpectedException(typeof(NotImplementedException)), ExcludeFromCodeCoverage]
    public void StreamApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        var api = session.StreamApi();
        Assert.IsNotNull(api);
    }

    [TestMethod, ExpectedException(typeof(NotImplementedException)), ExcludeFromCodeCoverage]
    public void UploadsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        var api = session.UploadsApi();
        Assert.IsNotNull(api);
    }
}

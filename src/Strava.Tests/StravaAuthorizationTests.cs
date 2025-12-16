using Tudormobile.Strava;

namespace Strava.Tests;

[TestClass]
public class StravaAuthorizationTests
{
    [TestMethod]
    public void ConstructorTest1()
    {
        var target = new StravaAuthorization();
        Assert.IsTrue(target.Expires < DateTime.Now, "Must already be expired.");
        Assert.AreEqual(0, target.Id, "Id must be zero.");
    }

    [TestMethod]
    public void ConstructorTest2()
    {
        var id = 123456789;
        var expires = new DateTime(2025, 1, 1);
        var clientId = "some id";
        var secret = "some secret";
        var token = "access token";
        var refresh = "refresh token";

        var target = new StravaAuthorization(clientId, secret, token, refresh, expires) { Id = id };

        Assert.AreEqual(id, target.Id);
        Assert.AreEqual(clientId, target.ClientId);
        Assert.AreEqual(secret, target.ClientSecret);
        Assert.AreEqual(expires, target.Expires);
        Assert.AreEqual(token, target.AccessToken);
        Assert.AreEqual(refresh, target.RefreshToken);
    }

    [TestMethod]
    public void WithTokensTest()
    {
        var id = 123456789;
        var expires = new DateTime(2025, 1, 1);
        var clientId = "some id";
        var secret = "some secret";
        var token = "access token";
        var refresh = "refresh token";
        var target = new StravaAuthorization(clientId, secret) { Id = id };
        var actual = target.WithTokens(token, refresh, expires);

        Assert.AreEqual(id, actual.Id);
        Assert.AreEqual(clientId, actual.ClientId);

        Assert.AreEqual(token, actual.AccessToken);
        Assert.AreEqual(refresh, actual.RefreshToken);
        Assert.AreEqual(expires, actual.Expires);

        Assert.AreNotSame(target, actual);
    }

}
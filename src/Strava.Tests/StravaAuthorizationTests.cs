using Tudormobile.Strava;

namespace Strava.Tests
{
    [TestClass]
    public class StravaAuthorizationTests
    {
        [TestMethod]
        public void ConstructorTest1()
        {
            var target = new StravaAuthorization();
            Assert.IsNotNull(target.ClientId, "Client Id cannot be (null).");
            Assert.IsNotNull(target.ClientSecret, "Client secret cannot be (null).");
            Assert.IsTrue(target.Expires < DateTime.Now, "Must already be expired.");
            Assert.IsNotNull(target.AccessToken, "Access token cannot be (null).");
            Assert.IsNotNull(target.RefreshToken, "Refresh secret cannot be (null).");
        }

        [TestMethod]
        public void ConstructorTest2()
        {
            var expires = new DateTime(2025, 1, 1);
            var id = "some id";
            var secret = "some secret";
            var token = "access token";
            var refresh = "refresh token";

            var target = new StravaAuthorization(id, secret, token, refresh, expires);

            Assert.AreEqual(target.ClientId, id);
            Assert.AreEqual(target.ClientSecret, secret);
            Assert.AreEqual(target.Expires, expires);
            Assert.AreEqual(target.AccessToken, token);
            Assert.AreEqual(target.RefreshToken, refresh);
        }

    }
}
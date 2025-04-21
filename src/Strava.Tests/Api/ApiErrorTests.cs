using Tudormobile.Strava.Api;

namespace Strava.Tests.Api
{
    [TestClass]
    public class ApiErrorTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var error = new NotImplementedException("exception message");
            var message = "some message";
            var target = new ApiError(message, error);
            Assert.AreEqual(message, target.Message);
            Assert.AreEqual(error, target.Exception);
            Assert.AreNotEqual(message, target.Exception.Message);
        }

        [TestMethod]
        public void ConstructorTestErrorOnly()
        {
            var message = "some message";
            var error = new NotImplementedException(message);
            var target = new ApiError(error);
            Assert.AreEqual(message, target.Message);
            Assert.AreEqual(error, target.Exception);
            Assert.AreEqual(message, target.Exception.Message);
        }

        [TestMethod]
        public void ConstructorTestMessageOnly()
        {
            var message = "some message";
            var target = new ApiError(message);
            Assert.AreEqual(message, target.Message);
            Assert.IsNotNull(target.Exception);
            Assert.IsInstanceOfType<Exception>(target.Exception);
        }

    }
}

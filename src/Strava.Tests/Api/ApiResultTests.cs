using Tudormobile.Strava.Api;

namespace Strava.Tests.Api
{
    [TestClass]
    public class ApiResultTests
    {
        [TestMethod]
        public void ConstructorTestWithError()
        {
            var error = new ApiError();
            var target = new ApiResult<int>(error: error);
            Assert.IsFalse(target.Success, "Cannot indicate success when error is present.");
            Assert.AreEqual(error, target.Error);
        }

        [TestMethod]
        public void ConstructorTestNoError()
        {
            int x = 123;
            var target = new ApiResult<int>(x);
            Assert.IsTrue(target.Success, "Cannot indicate success when error is present.");
            Assert.AreEqual(x, target.Data);
        }
    }
}

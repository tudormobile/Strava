using Tudormobile.Strava;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ErrorResponseTests
{
    [TestMethod]
    public void FromJsonTest()
    {
        var json = "{\"message\":\"Bad Request\",\"errors\":[{\"resource\":\"Application\",\"field\":\"client_id\",\"code\":\"invalid\"}]}";
        var success = StravaSerializer.TryDeserialize<ErrorResponse>(json, out var actual);

        Assert.IsNotNull(actual);
        Assert.AreEqual("Bad Request", actual.Message);
        Assert.AreEqual("Application", actual.Errors[0].Resource);
        Assert.AreEqual("client_id", actual.Errors[0].Field);
        Assert.AreEqual("invalid", actual.Errors[0].Code);
    }
}

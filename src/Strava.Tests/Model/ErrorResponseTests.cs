using System.Text.Json;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ErrorResponseTests
{
    [TestMethod]
    public void FromJsonTest()
    {
        var json = "{\"message\":\"Bad Request\",\"errors\":[{\"resource\":\"Application\",\"field\":\"client_id\",\"code\":\"invalid\"}]}";
        var options = JsonSerializerOptions.Default;
        var actual = JsonSerializer.Deserialize<ErrorResponse>(json);

        Assert.IsNotNull(actual);
        Assert.AreEqual("Bad Request", actual.message);
        Assert.AreEqual("Application", actual.errors[0].resource);
        Assert.AreEqual("client_id", actual.errors[0].field);
        Assert.AreEqual("invalid", actual.errors[0].code);
    }
}

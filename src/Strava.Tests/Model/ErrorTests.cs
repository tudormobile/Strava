using System.Text.Json;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ErrorTests
{
    [TestMethod]
    public void DefaultConstructorTest()
    {
        var target = new Error();
        Assert.AreEqual(String.Empty, target.Resource);
        Assert.AreEqual(String.Empty, target.Field);
        Assert.AreEqual(String.Empty, target.Code);
    }

    [TestMethod]
    public void SerializationTest()
    {
        var resource = "some resource";
        var @field = "some field";
        var code = "some code";

        var json = @$"{{""resource"":""{resource}"",""field"":""{@field}"",""code"":""{code}""}}";

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var target = JsonSerializer.Deserialize<Error>(json, options);

        Assert.IsNotNull(target);
        Assert.AreEqual(code, target.Code);
        Assert.AreEqual(resource, target.Resource);
        Assert.AreEqual(@field, target.Field);

    }
}

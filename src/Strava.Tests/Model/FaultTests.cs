using System.Text.Json;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class FaultTests
{
    [TestMethod]
    public void DefaultConstructorTest()
    {
        var target = new Fault();
        Assert.AreEqual(String.Empty, target.Message);
        Assert.AreEqual(0, target.Errors.Length);
    }

    [TestMethod]
    public void SerializationTest()
    {
        var message = "some message";
        var resource = "some resource";
        var @field = "some field";
        var code = "some code";

        var json = @$"{{""message"":""{message}"",""errors"":[{{""resource"":""{resource}"",""field"":""{@field}"",""code"":""{code}""}}]}}";

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var target = JsonSerializer.Deserialize<Fault>(json, options);

        Assert.IsNotNull(target);
        Assert.AreEqual(message, target.Message);
        Assert.AreEqual(1, target.Errors.Length);
        Assert.AreEqual(code, target.Errors[0].Code);
        Assert.AreEqual(resource, target.Errors[0].Resource);
        Assert.AreEqual(@field, target.Errors[0].Field);

    }

}

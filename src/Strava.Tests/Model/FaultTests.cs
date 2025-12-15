using System.Text.Json;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class FaultTests
{
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };
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

        var target = JsonSerializer.Deserialize<Fault>(json, _options);

        Assert.IsNotNull(target);
        Assert.AreEqual(message, target.Message);
        Assert.AreEqual(1, target.Errors.Length);
        Assert.AreEqual(code, target.Errors[0].Code);
        Assert.AreEqual(resource, target.Errors[0].Resource);
        Assert.AreEqual(@field, target.Errors[0].Field);

    }

}

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
        Assert.AreEqual(string.Empty, target.Message);
        Assert.IsEmpty(target.Errors);
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
        Assert.HasCount(1, target.Errors);
        Assert.AreEqual(code, target.Errors[0].Code);
        Assert.AreEqual(resource, target.Errors[0].Resource);
        Assert.AreEqual(@field, target.Errors[0].Field);

    }

    [TestMethod]
    public void ConstructorTest()
    {
        var target = new Fault();
        Assert.IsNotNull(target);
        Assert.AreEqual(string.Empty, target.Message);
        Assert.IsEmpty(target.Errors);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var errors = new[]
        {
            new Error { Code = "invalid", Field = "client_id", Resource = "Application" }
        };

        var target = new Fault
        {
            Message = "Bad Request",
            Errors = errors
        };

        Assert.AreEqual("Bad Request", target.Message);
        Assert.HasCount(1, target.Errors);
        Assert.AreEqual("invalid", target.Errors[0].Code);
        Assert.AreEqual("client_id", target.Errors[0].Field);
        Assert.AreEqual("Application", target.Errors[0].Resource);
    }
}

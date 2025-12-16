using System.Text;
using System.Text.Json;
using Tudormobile.Strava;
using Tudormobile.Strava.Model;

namespace Strava.Tests;

[TestClass]
public class StravaSerializerTests
{
    [TestMethod]
    public void BadJsonStringTest()
    {
        var json = "this is not a json object";
        var actual = StravaSerializer.TryDeserialize<SummaryActivity>(json, out var _);
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void BadJsonStreamTest()
    {
        var json = "this is not a json object";
        var s = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var actual = StravaSerializer.TryDeserialize<SummaryActivity>(s, out var _);
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void BadJsonWithExceptionTest()
    {
        var json = "this is not a json object";
        var s = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var actual = StravaSerializer.TryDeserialize(s, out SummaryActivity? activity, out JsonException? ex);
        Assert.IsFalse(actual);
        Assert.IsNull(activity);
        Assert.IsNotNull(ex);
        Assert.IsInstanceOfType<JsonException>(ex);
    }

    [TestMethod]
    public void OptionsTests()
    {
        var options = StravaSerializer.Options;
        Assert.IsNotNull(options.PropertyNamingPolicy);
    }

}

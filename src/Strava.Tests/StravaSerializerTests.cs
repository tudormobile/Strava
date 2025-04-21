using System.Text;
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

}

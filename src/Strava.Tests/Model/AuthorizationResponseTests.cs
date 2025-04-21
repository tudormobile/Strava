using System.Text.Json;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class AuthorizationResponseTests
{
    [TestMethod]
    public void FromJsonTest1()
    {
        var json = @"
{
  ""token_type"": ""Bearer"",
  ""access_token"": ""a9b723..."",
  ""expires_at"":1568775134,
  ""expires_in"":20566,
  ""refresh_token"":""b5c569...""
}
";
        var actual = JsonSerializer.Deserialize<AuthorizationResponse>(json);

        Assert.IsNotNull(actual);

        Assert.AreEqual("a9b723...", actual.access_token);
        Assert.AreEqual("b5c569...", actual.refresh_token);
        Assert.AreEqual(20566, actual.expires_in);
        Assert.AreEqual(1568775134, actual.expires_at);
        Assert.IsNull(actual.athlete);
        Assert.AreEqual("Bearer", actual.token_type);
    }

    [TestMethod]
    public void FromJsonTest2()
    {
        var json = @"
{
  ""token_type"": ""Bearer"",
  ""access_token"": ""a9b723..."",
  ""expires_at"":1568775134,
  ""expires_in"":20566,
  ""refresh_token"":""b5c569..."",
  ""athlete"": {
    ""id"" : 134815,
    ""resource_state"" : 1
  }
}
";
        var actual = JsonSerializer.Deserialize<AuthorizationResponse>(json);

        Assert.IsNotNull(actual);

        Assert.AreEqual("a9b723...", actual.access_token);
        Assert.AreEqual("b5c569...", actual.refresh_token);
        Assert.AreEqual(20566, actual.expires_in);
        Assert.AreEqual(1568775134, actual.expires_at);
        Assert.IsNotNull(actual.athlete);
        Assert.AreEqual(134815, actual.athlete.Id);
        Assert.AreEqual(1, actual.athlete.ResourceState);
        Assert.AreEqual("Bearer", actual.token_type);
    }

}

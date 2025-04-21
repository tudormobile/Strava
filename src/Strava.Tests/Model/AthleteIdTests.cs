using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class AthleteIdTests
{
    [TestMethod]
    public void FromJsonTest()
    {
        var json = "{\"id\" : 134815,\"resource_state\" : 1}";
        var actual = AthleteId.FromJson(json);
        Assert.IsNotNull(actual);
        Assert.AreEqual(134815, actual.Id);
        Assert.AreEqual(1, actual.ResourceState);
    }
}

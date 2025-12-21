using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class DetailedAthleteTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new DetailedAthlete();
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
        Assert.IsNull(target.Username);
        Assert.AreEqual(ResourceStates.Unknown, target.ResourceState);
        Assert.AreEqual(DateTime.MinValue, target.CreatedAt);
        Assert.AreEqual(DateTime.MinValue, target.UpdatedAt);
    }
}

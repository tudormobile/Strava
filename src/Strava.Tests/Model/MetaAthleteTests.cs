using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class MetaAthleteTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new MetaAthlete();
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var target = new MetaAthlete
        {
            Id = 98765
        };

        Assert.AreEqual(98765, target.Id);
    }
}
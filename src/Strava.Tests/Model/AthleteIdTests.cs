using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class AthleteIdTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new AthleteId();
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
        Assert.AreEqual(0, target.ResourceState);
    }

    [TestMethod]
    public void FromJsonTest()
    {
        var json = @"{""id"":123456789,""resource_state"":2}";
        var target = AthleteId.FromJson(json);
        Assert.IsNotNull(target);
        Assert.AreEqual(123456789, target.Id);
        Assert.AreEqual(2, target.ResourceState);
    }

    [TestMethod]
    public void FromJsonWithInvalidDataTest()
    {
        var json = "invalid json";
        var target = AthleteId.FromJson(json);
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
    }

    [TestMethod]
    public void PropertyInitializationTest()
    {
        var target = new AthleteId { Id = 987654321, ResourceState = 3 };
        Assert.AreEqual(987654321, target.Id);
        Assert.AreEqual(3, target.ResourceState);
    }
}

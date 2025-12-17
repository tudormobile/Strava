using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class UpdatableActivityTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new UpdatableActivity();
        Assert.IsNotNull(target);
        Assert.AreEqual(String.Empty, target.Name);
        Assert.AreEqual(String.Empty, target.Description);
        Assert.IsFalse(target.Commute);
        Assert.IsFalse(target.Trainer);
    }

    [TestMethod]
    public void ConstructorWithParametersTest()
    {
        var target = new UpdatableActivity("Test Activity", "Test Description", SportTypes.Run);
        Assert.AreEqual("Test Activity", target.Name);
        Assert.AreEqual("Test Description", target.Description);
        Assert.AreEqual(SportTypes.Run, target.SportType);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var target = new UpdatableActivity
        {
            Commute = true,
            Trainer = true,
            Description = "Evening ride",
            Name = "Training Ride",
            SportType = SportTypes.Ride
        };

        Assert.IsTrue(target.Commute);
        Assert.IsTrue(target.Trainer);
        Assert.AreEqual("Evening ride", target.Description);
        Assert.AreEqual("Training Ride", target.Name);
        Assert.AreEqual(SportTypes.Ride, target.SportType);
    }
}

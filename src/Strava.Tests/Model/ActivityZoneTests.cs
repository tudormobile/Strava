using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ActivityZoneTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new ActivityZone();
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Score);
        Assert.AreEqual("Unknown", target.Type);
        Assert.IsFalse(target.SensorBased);
        Assert.AreEqual(0, target.Points);
        Assert.IsFalse(target.CustomZones);
        Assert.AreEqual(0, target.Max);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var target = new ActivityZone
        {
            Score = 100,
            Type = "heartrate",
            SensorBased = true,
            Points = 50,
            CustomZones = true,
            Max = 180
        };

        Assert.AreEqual(100, target.Score);
        Assert.AreEqual("heartrate", target.Type);
        Assert.IsTrue(target.SensorBased);
        Assert.AreEqual(50, target.Points);
        Assert.IsTrue(target.CustomZones);
        Assert.AreEqual(180, target.Max);
    }

    [TestMethod]
    public void HeartRateZoneTest()
    {
        var target = new ActivityZone
        {
            Type = "heartrate",
            SensorBased = true,
            Max = 175
        };

        Assert.AreEqual("heartrate", target.Type);
        Assert.IsTrue(target.SensorBased);
        Assert.AreEqual(175, target.Max);
    }

    [TestMethod]
    public void PowerZoneTest()
    {
        var target = new ActivityZone
        {
            Type = "power",
            SensorBased = true,
            CustomZones = true,
            Max = 300
        };

        Assert.AreEqual("power", target.Type);
        Assert.IsTrue(target.SensorBased);
        Assert.IsTrue(target.CustomZones);
        Assert.AreEqual(300, target.Max);
    }
}
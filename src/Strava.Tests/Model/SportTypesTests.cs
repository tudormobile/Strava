using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class SportTypesTests
{
    [TestMethod]
    public void EnumValuesTest()
    {
        Assert.IsTrue(Enum.IsDefined(SportTypes.Run));
        Assert.IsTrue(Enum.IsDefined(SportTypes.Ride));
        Assert.IsTrue(Enum.IsDefined(SportTypes.Swim));
    }

    [TestMethod]
    public void EnumNamesTest()
    {
        Assert.AreEqual("Unknown", SportTypes.Unknown.ToString());
        Assert.AreEqual("Run", SportTypes.Run.ToString());
        Assert.AreEqual("Ride", SportTypes.Ride.ToString());
        Assert.AreEqual("Swim", SportTypes.Swim.ToString());
    }

    [TestMethod]
    public void EnumParseTest()
    {
        Assert.IsTrue(Enum.TryParse<SportTypes>("Run", out var runType));
        Assert.AreEqual(SportTypes.Run, runType);

        Assert.IsTrue(Enum.TryParse<SportTypes>("Ride", out var rideType));
        Assert.AreEqual(SportTypes.Ride, rideType);
    }
}
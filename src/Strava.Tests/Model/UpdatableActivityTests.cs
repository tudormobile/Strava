using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;
[TestClass]
public class UpdatableActivityTests
{
    [TestMethod]
    public void UpdatableActivity_Constructor_SetsProperties()
    {
        // Arrange
        var name = "Morning Ride";
        var sportType = SportTypes.Ride;
        var commute = false;
        var trainer = true;
        var description = "Test description";

        // Act
        var activity = new UpdatableActivity(name, description)
        {
            Commute = commute,
            Trainer = trainer,
            SportType = sportType,
        };

        // Assert
        Assert.AreEqual(name, activity.Name);
        Assert.AreEqual(description, activity.Description);
        Assert.AreEqual(commute, activity.Commute);
        Assert.AreEqual(trainer, activity.Trainer);
        Assert.AreEqual(sportType, activity.SportType);
    }

    [TestMethod]
    public void UpdatableActivity_DefaultConstructor_InitializesPropertiesToDefaults()
    {
        // Act
        var activity = new UpdatableActivity();

        // Assert
        Assert.AreEqual(String.Empty, activity.Name);
        Assert.AreEqual(String.Empty, activity.Description);
        Assert.AreEqual(false, activity.Commute);
        Assert.AreEqual(false, activity.Trainer);
        Assert.AreEqual(SportTypes.Unknown, activity.SportType);
    }

    [TestMethod]
    public void UpdatableActivity_Setters_UpdateProperties()
    {
        // Arrange
        var activity = new UpdatableActivity();

        // Act
        activity.Name = "Evening Run";
        activity.SportType = SportTypes.Sail;
        activity.Commute = true;
        activity.Trainer = true;
        activity.Description = "Evening run description";

        // Assert
        Assert.AreEqual("Evening Run", activity.Name);
        Assert.AreEqual("Evening run description", activity.Description);
        Assert.IsTrue(activity.Commute);
        Assert.IsTrue(activity.Trainer);
        Assert.AreEqual(SportTypes.Sail, activity.SportType);
    }
}

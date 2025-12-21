using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ClubActivityTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeDefaultValues()
    {
        // Arrange & Act
        var clubActivity = new ClubActivity();

        // Assert
        Assert.IsNotNull(clubActivity.Athlete);
        Assert.AreEqual(string.Empty, clubActivity.Name);
        Assert.AreEqual(0.0, clubActivity.Distance);
        Assert.AreEqual(0L, clubActivity.MovingTime);
        Assert.AreEqual(0L, clubActivity.ElapsedTime);
        Assert.AreEqual(0.0, clubActivity.TotalElevationGain);
        Assert.IsNull(clubActivity.Type);
        Assert.AreEqual(SportTypes.Unknown, clubActivity.SportType);
        Assert.IsNull(clubActivity.WorkoutType);
    }
}

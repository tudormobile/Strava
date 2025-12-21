using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ClubAthleteTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeDefaultValues()
    {
        // Arrange & Act
        var clubAthlete = new ClubAthlete();

        // Assert
        Assert.AreEqual(ResourceStates.Unknown, clubAthlete.ResourceState);
        Assert.AreEqual(string.Empty, clubAthlete.Firstname);
        Assert.AreEqual(string.Empty, clubAthlete.Lastname);
        Assert.AreEqual("Unknown", clubAthlete.Membership);
        Assert.IsFalse(clubAthlete.Admin);
        Assert.IsFalse(clubAthlete.Owner);
    }
}

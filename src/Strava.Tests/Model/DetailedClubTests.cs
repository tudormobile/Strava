using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class DetailedClubTests
{
    [TestMethod]
    public void Constructor_InitializesWithDefaultValues()
    {
        // Act
        var club = new DetailedClub();

        // Assert
        Assert.AreEqual(0L, club.Id);
        Assert.AreEqual(ResourceStates.Unknown, club.ResourceState);
        Assert.AreEqual(string.Empty, club.Name);
        Assert.IsNull(club.ProfileMedium);
        Assert.IsNull(club.Profile);
        Assert.IsNull(club.CoverPhoto);
        Assert.IsNull(club.CoverPhotoSmall);
        Assert.AreEqual(SportTypes.Unknown, club.SportType);
        Assert.IsNull(club.ActivityTypes);
        Assert.IsNull(club.City);
        Assert.IsNull(club.State);
        Assert.IsNull(club.Country);
        Assert.IsFalse(club.Private);
        Assert.AreEqual(0, club.MemberCount);
        Assert.IsFalse(club.Featured);
        Assert.IsFalse(club.Verified);
        Assert.IsNull(club.Url);
        Assert.AreEqual("Unknown", club.Membership);
        Assert.IsFalse(club.Admin);
        Assert.IsFalse(club.Owner);
        Assert.AreEqual(string.Empty, club.Description);
        Assert.AreEqual("Other", club.ClubType);
        Assert.AreEqual(0, club.PostCount);
        Assert.AreEqual(0, club.OwnerId);
        Assert.AreEqual(0, club.FollowingCount);
    }
}

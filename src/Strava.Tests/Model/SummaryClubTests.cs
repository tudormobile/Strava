using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class SummaryClubTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new SummaryClub();
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
        Assert.AreEqual(string.Empty, target.Name);
        Assert.AreEqual(ResourceStates.Unknown, target.ResourceState);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var target = new SummaryClub
        {
            Id = 123456789,
            Name = "Morning Riders Club",
            ResourceState = ResourceStates.Summary
        };

        Assert.AreEqual(123456789, target.Id);
        Assert.AreEqual("Morning Riders Club", target.Name);
        Assert.AreEqual(ResourceStates.Summary, target.ResourceState);
    }

    [TestMethod]
    public void AllResourceStatesTest()
    {
        var target = new SummaryClub
        {
            Id = 1,
            ResourceState = ResourceStates.Unknown
        };
        Assert.AreEqual(ResourceStates.Unknown, target.ResourceState);

        target.ResourceState = ResourceStates.Meta;
        Assert.AreEqual(ResourceStates.Meta, target.ResourceState);

        target.ResourceState = ResourceStates.Summary;
        Assert.AreEqual(ResourceStates.Summary, target.ResourceState);

        target.ResourceState = ResourceStates.Detail;
        Assert.AreEqual(ResourceStates.Detail, target.ResourceState);
    }

    [TestMethod]
    public void NullNameTest()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var target = new SummaryClub
        {
            Id = 999,
            Name = null,
            ResourceState = ResourceStates.Meta
        };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Assert.AreEqual(999, target.Id);
        Assert.IsNull(target.Name);
        Assert.AreEqual(ResourceStates.Meta, target.ResourceState);
    }

    [TestMethod]
    public void EmptyNameTest()
    {
        var target = new SummaryClub
        {
            Id = 888,
            Name = string.Empty,
            ResourceState = ResourceStates.Summary
        };

        Assert.AreEqual(888, target.Id);
        Assert.AreEqual(string.Empty, target.Name);
    }

    [TestMethod]
    public void LongClubNameTest()
    {
        var longName = new string('X', 500);
        var target = new SummaryClub
        {
            Id = 777,
            Name = longName,
            ResourceState = ResourceStates.Detail
        };

        Assert.AreEqual(longName, target.Name);
        Assert.AreEqual(500, target.Name.Length);
    }

    [TestMethod]
    public void MultipleClubsTest()
    {
        var club1 = new SummaryClub
        {
            Id = 111,
            Name = "Club One",
            ResourceState = ResourceStates.Summary
        };

        var club2 = new SummaryClub
        {
            Id = 222,
            Name = "Club Two",
            ResourceState = ResourceStates.Meta
        };

        Assert.AreNotEqual(club1.Id, club2.Id);
        Assert.AreNotEqual(club1.Name, club2.Name);
        Assert.AreNotEqual(club1.ResourceState, club2.ResourceState);
    }
}

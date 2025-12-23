using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class SegmentTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var segment = new Segment();

        // Assert
        Assert.AreEqual(0L, segment.Id);
        Assert.AreEqual(ResourceStates.Unknown, segment.ResourceState);
        Assert.AreEqual(string.Empty, segment.Name);
        Assert.AreEqual(string.Empty, segment.ActivityType);
        Assert.AreEqual(0f, segment.Distance);
        Assert.AreEqual(0f, segment.AverageGrade);
        Assert.AreEqual(0f, segment.MaximumGrade);
        Assert.AreEqual(0f, segment.ElevationHigh);
        Assert.AreEqual(0f, segment.ElevationLow);
        Assert.AreEqual(0, segment.ClimbCategory);
        Assert.IsNull(segment.City);
        Assert.IsNull(segment.State);
        Assert.IsNull(segment.Country);
        Assert.IsFalse(segment.Private);
        Assert.IsFalse(segment.Hazardous);
        Assert.IsFalse(segment.Starred);
    }
}

using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class PolylineMapTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var polylineMap = new PolylineMap();

        // Assert
        Assert.IsNotNull(polylineMap);
        Assert.AreEqual(string.Empty, polylineMap.Id);
        Assert.AreEqual(ResourceStates.Unknown, polylineMap.ResourceState);
    }

    [TestMethod]
    public void PropertyAssignment_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var polylineMap = new PolylineMap
        {
            Id = "a12345678987654321",
            ResourceState = ResourceStates.Summary
        };

        // Assert
        Assert.AreEqual("a12345678987654321", polylineMap.Id);
        Assert.AreEqual(ResourceStates.Summary, polylineMap.ResourceState);
    }

    [TestMethod]
    public void SummaryConstructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var summaryMap = new SummaryPolylineMap();

        // Assert
        Assert.IsNotNull(summaryMap);
        Assert.AreEqual(string.Empty, summaryMap.Id);
        Assert.AreEqual(ResourceStates.Unknown, summaryMap.ResourceState);
        Assert.AreEqual(string.Empty, summaryMap.SummaryPolyline);
    }

    [TestMethod]
    public void SummaryPropertyAssignment_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var summaryMap = new SummaryPolylineMap
        {
            Id = "a12345678987654321",
            ResourceState = ResourceStates.Summary,
            SummaryPolyline = "u{~vFvyysO@dB~@"
        };

        // Assert
        Assert.AreEqual("a12345678987654321", summaryMap.Id);
        Assert.AreEqual(ResourceStates.Summary, summaryMap.ResourceState);
        Assert.AreEqual("u{~vFvyysO@dB~@", summaryMap.SummaryPolyline);
    }

    [TestMethod]
    public void Inheritance_ShouldInheritFromPolylineMap()
    {
        // Arrange & Act
        var summaryMap = new SummaryPolylineMap();

        // Assert
        Assert.IsInstanceOfType<PolylineMap>(summaryMap);
    }
}

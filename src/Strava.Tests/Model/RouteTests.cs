using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class RouteTests
{
    [TestMethod]
    public void Constructor_WithDefaultValues_ShouldInitializeAllProperties()
    {
        // Arrange & Act
        var route = new Route();

        // Assert
        Assert.IsNotNull(route);
        Assert.AreEqual(0L, route.Id);
        Assert.AreEqual(string.Empty, route.Name);
        Assert.AreEqual(string.Empty, route.Description);
        Assert.AreEqual(0f, route.Distance);
        Assert.IsFalse(route.Private);
        Assert.IsNotNull(route.Athlete);
        Assert.AreEqual(default, route.CreatedAt);
        Assert.AreEqual(0f, route.ElevationGain);
        Assert.AreEqual(0, route.Type);
        Assert.AreEqual(default, route.EstimatedMovingTime);
        Assert.IsNotNull(route.Waypoints);
        Assert.IsEmpty(route.Waypoints);
        Assert.IsNotNull(route.Segments);
        Assert.IsEmpty(route.Segments);
        Assert.IsFalse(route.Starred);
        Assert.AreEqual(default, route.UpdatedAt);
        Assert.AreEqual(0, route.SubType);
        Assert.AreEqual(string.Empty, route.IdStr);
        Assert.IsNotNull(route.Map);
        Assert.AreEqual(default, route.Timestamp);
    }

    [TestMethod]
    public void Id_CanBeSet()
    {
        // Arrange
        var route = new Route
        {
            // Act
            Id = 123456789L
        };

        // Assert
        Assert.AreEqual(123456789L, route.Id);
    }

    [TestMethod]
    public void Name_CanBeSet()
    {
        // Arrange
        var route = new Route
        {
            // Act
            Name = "Morning Ride"
        };

        // Assert
        Assert.AreEqual("Morning Ride", route.Name);
    }

    [TestMethod]
    public void Description_CanBeSet()
    {
        // Arrange & Act
        var route = new Route();

        // Assert
        Assert.IsNotNull(route);
        Assert.AreEqual(0L, route.Id);
        Assert.AreEqual(string.Empty, route.Name);
        Assert.AreEqual(string.Empty, route.Description);
        Assert.AreEqual(0f, route.Distance);
        Assert.IsFalse(route.Private);
        Assert.IsNotNull(route.Athlete);
        Assert.AreEqual(default, route.CreatedAt);
        Assert.AreEqual(0f, route.ElevationGain);
        Assert.AreEqual(0, route.Type);
        Assert.AreEqual(default, route.EstimatedMovingTime);
        Assert.IsNotNull(route.Waypoints);
        Assert.IsEmpty(route.Waypoints);
        Assert.IsNotNull(route.Segments);
        Assert.IsEmpty(route.Segments);
        Assert.IsFalse(route.Starred);
        Assert.AreEqual(default, route.UpdatedAt);
        Assert.AreEqual(0, route.SubType);
        Assert.AreEqual(string.Empty, route.IdStr);
        Assert.IsNotNull(route.Map);
        Assert.AreEqual(default, route.Timestamp);
    }
}

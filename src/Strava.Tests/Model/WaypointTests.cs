using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class WaypointTest
{
    [TestMethod]
    public void Constructor_WithDefaultValues_ShouldInitializeAllProperties()
    {
        // Arrange & Act
        var waypoint = new Waypoint();

        // Assert
        Assert.IsNotNull(waypoint);
        Assert.AreEqual(default, waypoint.Latlng);
        Assert.AreEqual(default, waypoint.TargetLatlng);
        Assert.AreEqual(0f, waypoint.DistanceIntoRoute);
        Assert.AreEqual(string.Empty, waypoint.Description);
        Assert.IsNotNull(waypoint.Categories);
        Assert.IsEmpty(waypoint.Categories);
        Assert.AreEqual(string.Empty, waypoint.Title);
    }

    [TestMethod]
    public void Latlng_CanBeSet()
    {
        // Arrange
        var waypoint = new Waypoint();
        var latlng = new LatLng { Latitude = 37.7749, Longitude = -122.4194 };

        // Act
        waypoint.Latlng = latlng;

        // Assert
        Assert.AreEqual(37.7749, waypoint.Latlng.Latitude);
        Assert.AreEqual(-122.4194, waypoint.Latlng.Longitude);
    }

    [TestMethod]
    public void TargetLatlng_CanBeSet()
    {
        // Arrange
        var waypoint = new Waypoint();
        var targetLatlng = new LatLng { Latitude = 40.7128, Longitude = -74.0060 };

        // Act
        waypoint.TargetLatlng = targetLatlng;

        // Assert
        Assert.AreEqual(40.7128, waypoint.TargetLatlng.Latitude);
        Assert.AreEqual(-74.0060, waypoint.TargetLatlng.Longitude);
    }

    [TestMethod]
    public void DistanceIntoRoute_CanBeSet()
    {
        // Arrange
        var waypoint = new Waypoint
        {
            // Act
            DistanceIntoRoute = 1500.5f
        };

        // Assert
        Assert.AreEqual(1500.5f, waypoint.DistanceIntoRoute);
    }

    [TestMethod]
    public void Description_CanBeSet()
    {
        // Arrange
        var waypoint = new Waypoint
        {
            // Act
            Description = "Water station"
        };

        // Assert
        Assert.AreEqual("Water station", waypoint.Description);
    }

    [TestMethod]
    public void Title_CanBeSet()
    {
        // Arrange
        var waypoint = new Waypoint
        {
            // Act
            Title = "Summit Point"
        };

        // Assert
        Assert.AreEqual("Summit Point", waypoint.Title);
    }

    [TestMethod]
    public void Categories_CanBeAdded()
    {
        // Arrange
        var waypoint = new Waypoint();

        // Act
        waypoint.Categories.Add("water");
        waypoint.Categories.Add("rest");

        // Assert
        Assert.HasCount(2, waypoint.Categories);
        Assert.Contains("water", waypoint.Categories);
        Assert.Contains("rest", waypoint.Categories);
    }

    [TestMethod]
    public void Categories_CanBeSetToNewList()
    {
        // Arrange
        var waypoint = new Waypoint();
        var categories = new List<string> { "summit", "photo", "danger" };

        // Act
        waypoint.Categories = categories;

        // Assert
        Assert.HasCount(3, waypoint.Categories);
        Assert.Contains("summit", waypoint.Categories);
        Assert.Contains("photo", waypoint.Categories);
        Assert.Contains("danger", waypoint.Categories);
    }

    [TestMethod]
    public void PropertyAssignment_ShouldSetAllProperties()
    {
        // Arrange
        var latlng = new LatLng { Latitude = 45.5231, Longitude = -122.6765 };
        var targetLatlng = new LatLng { Latitude = 45.5250, Longitude = -122.6800 };
        var categories = new List<string> { "water", "summit" };

        // Act
        var waypoint = new Waypoint
        {
            Latlng = latlng,
            TargetLatlng = targetLatlng,
            DistanceIntoRoute = 2500.75f,
            Description = "Mountain summit with water available",
            Categories = categories,
            Title = "Eagle Peak"
        };

        // Assert
        Assert.AreEqual(45.5231, waypoint.Latlng.Latitude);
        Assert.AreEqual(-122.6765, waypoint.Latlng.Longitude);
        Assert.AreEqual(45.5250, waypoint.TargetLatlng.Latitude);
        Assert.AreEqual(-122.6800, waypoint.TargetLatlng.Longitude);
        Assert.AreEqual(2500.75f, waypoint.DistanceIntoRoute);
        Assert.AreEqual("Mountain summit with water available", waypoint.Description);
        Assert.HasCount(2, waypoint.Categories);
        Assert.Contains("water", waypoint.Categories);
        Assert.Contains("summit", waypoint.Categories);
        Assert.AreEqual("Eagle Peak", waypoint.Title);
    }

    [TestMethod]
    public void DistanceIntoRoute_WithZeroValue_ShouldBeZero()
    {
        // Arrange & Act
        var waypoint = new Waypoint
        {
            DistanceIntoRoute = 0f
        };

        // Assert
        Assert.AreEqual(0f, waypoint.DistanceIntoRoute);
    }

    [TestMethod]
    public void DistanceIntoRoute_WithLargeValue_ShouldAcceptValue()
    {
        // Arrange & Act
        var waypoint = new Waypoint
        {
            DistanceIntoRoute = 42195.5f // Marathon distance
        };

        // Assert
        Assert.AreEqual(42195.5f, waypoint.DistanceIntoRoute, 0.01f);
    }

    [TestMethod]
    public void Categories_EmptyList_ShouldHaveCountZero()
    {
        // Arrange & Act
        var waypoint = new Waypoint
        {
            Categories = []
        };

        // Assert
        Assert.IsEmpty(waypoint.Categories);
    }

    [TestMethod]
    public void Description_WithEmptyString_ShouldBeEmpty()
    {
        // Arrange & Act
        var waypoint = new Waypoint
        {
            Description = string.Empty
        };

        // Assert
        Assert.AreEqual(string.Empty, waypoint.Description);
    }

    [TestMethod]
    public void Title_WithEmptyString_ShouldBeEmpty()
    {
        // Arrange & Act
        var waypoint = new Waypoint
        {
            Title = string.Empty
        };

        // Assert
        Assert.AreEqual(string.Empty, waypoint.Title);
    }

    [TestMethod]
    public void Latlng_WithNegativeCoordinates_ShouldAcceptValues()
    {
        // Arrange
        var latlng = new LatLng { Latitude = -33.8688, Longitude = 151.2093 }; // Sydney coordinates

        // Act
        var waypoint = new Waypoint
        {
            Latlng = latlng
        };

        // Assert
        Assert.AreEqual(-33.8688, waypoint.Latlng.Latitude);
        Assert.AreEqual(151.2093, waypoint.Latlng.Longitude);
    }

    [TestMethod]
    public void Categories_WithMultipleCategories_ShouldMaintainOrder()
    {
        // Arrange
        var waypoint = new Waypoint();
        var expectedCategories = new[] { "water", "rest", "food", "medical" };

        // Act
        foreach (var category in expectedCategories)
        {
            waypoint.Categories.Add(category);
        }

        // Assert
        Assert.HasCount(4, waypoint.Categories);
        for (int i = 0; i < expectedCategories.Length; i++)
        {
            Assert.AreEqual(expectedCategories[i], waypoint.Categories[i]);
        }
    }
}

using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class BoundsTests
{
    [TestMethod]
    public void ToString_ShouldReturnFormattedBounds()
    {
        // Arrange
        var bounds = new Bounds
        {
            SouthWestCorner = new LatLng { Latitude = 37.7749, Longitude = -122.4194 },
            NorthEastCorner = new LatLng { Latitude = 37.8049, Longitude = -122.3894 }
        };

        // Act
        var result = bounds.ToString();

        // Assert
        Assert.AreEqual("{(37.7749,-122.4194),(37.8049,-122.3894)}", result);
    }

    [TestMethod]
    public void AsArray_ShouldReturnCoordinatesInCorrectOrder()
    {
        // Arrange
        var bounds = new Bounds
        {
            SouthWestCorner = new LatLng { Latitude = 37.7749, Longitude = -122.4194 },
            NorthEastCorner = new LatLng { Latitude = 37.8049, Longitude = -122.3894 }
        };

        // Act
        var result = bounds.AsArray();

        // Assert
        Assert.HasCount(4, result);
        Assert.AreEqual(37.7749, result[0]);
        Assert.AreEqual(-122.4194, result[1]);
        Assert.AreEqual(37.8049, result[2]);
        Assert.AreEqual(-122.3894, result[3]);
    }

    [TestMethod]
    public void AsArray_WithNegativeCoordinates_ShouldReturnCorrectValues()
    {
        // Arrange
        var bounds = Bounds.FromCoordinates(-33.9173, 18.4231, -33.8573, 18.5031);

        // Act
        var result = bounds.AsArray();

        // Assert
        Assert.HasCount(4, result);
        Assert.AreEqual(-33.9173, result[0]);
        Assert.AreEqual(18.4231, result[1]);
        Assert.AreEqual(-33.8573, result[2]);
        Assert.AreEqual(18.5031, result[3]);
    }

    [TestMethod]
    public void AsArray_WithDefaultBounds_ShouldReturnZeros()
    {
        // Arrange
        var bounds = new Bounds();

        // Act
        var result = bounds.AsArray();

        // Assert
        Assert.HasCount(4, result);
        Assert.AreEqual(0.0, result[0]);
        Assert.AreEqual(0.0, result[1]);
        Assert.AreEqual(0.0, result[2]);
        Assert.AreEqual(0.0, result[3]);
    }
}

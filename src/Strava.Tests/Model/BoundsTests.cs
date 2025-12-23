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
}

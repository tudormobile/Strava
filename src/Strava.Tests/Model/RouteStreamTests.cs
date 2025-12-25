using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class RouteStreamTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithEmptyData()
    {
        // Arrange & Act
        var stream = new RouteStream();

        // Assert
        Assert.IsNotNull(stream);
        Assert.IsNotNull(stream.Data);
        Assert.HasCount(0, stream.Data);
    }

    [TestMethod]
    public void Data_ShouldBeInitializedAsEmptyList()
    {
        // Arrange
        var stream = new RouteStream();

        // Act
        var data = stream.Data;

        // Assert
        Assert.IsNotNull(data);
        Assert.HasCount(0, data);
    }

    [TestMethod]
    public void Data_CanBeSetWithLatLngValues()
    {
        // Arrange
        var stream = new RouteStream();
        var testData = new List<LatLng>
        {
            new() { Latitude = 37.7749, Longitude = -122.4194 },
            new() { Latitude = 37.7849, Longitude = -122.4294 },
            new() { Latitude = 37.7949, Longitude = -122.4394 }
        };

        // Act
        stream.Data = testData;

        // Assert
        Assert.AreEqual(testData, stream.Data);
        Assert.HasCount(3, stream.Data);
    }

    [TestMethod]
    public void Data_CanBeModifiedAfterInitialization()
    {
        // Arrange
        var stream = new RouteStream();

        // Act
        stream.Data.Add(new LatLng { Latitude = 37.7749, Longitude = -122.4194 });
        stream.Data.Add(new LatLng { Latitude = 37.7849, Longitude = -122.4294 });

        // Assert
        Assert.HasCount(2, stream.Data);
        Assert.AreEqual(37.7749, stream.Data[0].Latitude, 0.0001);
        Assert.AreEqual(-122.4194, stream.Data[0].Longitude, 0.0001);
        Assert.AreEqual(37.7849, stream.Data[1].Latitude, 0.0001);
        Assert.AreEqual(-122.4294, stream.Data[1].Longitude, 0.0001);
    }

    [TestMethod]
    public void Type_CanBeSetAndRetrieved()
    {
        // Arrange
        var stream = new RouteStream
        {
            Type = "latlng"
        };

        // Act
        var type = stream.Type;

        // Assert
        Assert.AreEqual("latlng", type);
    }

    [TestMethod]
    public void SeriesType_CanBeSetAndRetrieved()
    {
        // Arrange
        var stream = new RouteStream
        {
            SeriesType = "distance"
        };

        // Act
        var seriesType = stream.SeriesType;

        // Assert
        Assert.AreEqual("distance", seriesType);
    }

    [TestMethod]
    public void OriginalSize_CanBeSetAndRetrieved()
    {
        // Arrange
        var stream = new RouteStream
        {
            OriginalSize = 500
        };

        // Act
        var originalSize = stream.OriginalSize;

        // Assert
        Assert.AreEqual(500, originalSize);
    }

    [TestMethod]
    public void Resolution_CanBeSetAndRetrieved()
    {
        // Arrange
        var stream = new RouteStream
        {
            Resolution = "high"
        };

        // Act
        var resolution = stream.Resolution;

        // Assert
        Assert.AreEqual("high", resolution);
    }

    [TestMethod]
    public void RouteStream_WithAllProperties_ShouldRetainValues()
    {
        // Arrange
        var testData = new List<LatLng>
        {
            new() { Latitude = 37.7749, Longitude = -122.4194 },
            new() { Latitude = 37.7849, Longitude = -122.4294 },
            new() { Latitude = 37.7949, Longitude = -122.4394 }
        };
        var stream = new RouteStream
        {
            Type = "latlng",
            SeriesType = "distance",
            OriginalSize = 3000,
            Resolution = "high",
            Data = testData
        };

        // Act & Assert
        Assert.AreEqual("latlng", stream.Type);
        Assert.AreEqual("distance", stream.SeriesType);
        Assert.AreEqual(3000, stream.OriginalSize);
        Assert.AreEqual("high", stream.Resolution);
        Assert.HasCount(3, stream.Data);
        Assert.AreEqual(37.7749, stream.Data[0].Latitude, 0.0001);
        Assert.AreEqual(-122.4194, stream.Data[0].Longitude, 0.0001);
    }

    [TestMethod]
    public void RouteStream_WithRouteCoordinates_ShouldStoreCorrectly()
    {
        // Arrange
        var routePoints = new List<LatLng>
        {
            new() { Latitude = 40.7128, Longitude = -74.0060 },  // New York
            new() { Latitude = 34.0522, Longitude = -118.2437 }, // Los Angeles
            new() { Latitude = 41.8781, Longitude = -87.6298 }   // Chicago
        };
        var stream = new RouteStream
        {
            Type = "latlng",
            SeriesType = "distance",
            Data = routePoints
        };

        // Act & Assert
        Assert.AreEqual("latlng", stream.Type);
        Assert.HasCount(3, stream.Data);
        Assert.AreEqual(40.7128, stream.Data[0].Latitude, 0.0001);
        Assert.AreEqual(-74.0060, stream.Data[0].Longitude, 0.0001);
        Assert.AreEqual(34.0522, stream.Data[1].Latitude, 0.0001);
        Assert.AreEqual(-118.2437, stream.Data[1].Longitude, 0.0001);
    }

    [TestMethod]
    public void RouteStream_WithTupleInitialization_ShouldWork()
    {
        // Arrange
        var stream = new RouteStream();

        // Act
        stream.Data.Add((37.7749, -122.4194));
        stream.Data.Add((37.7849, -122.4294));

        // Assert
        Assert.HasCount(2, stream.Data);
        Assert.AreEqual(37.7749, stream.Data[0].Latitude, 0.0001);
        Assert.AreEqual(-122.4194, stream.Data[0].Longitude, 0.0001);
    }

    [TestMethod]
    public void RouteStream_WithNegativeCoordinates_ShouldStoreCorrectly()
    {
        // Arrange
        var negativeCoords = new List<LatLng>
        {
            new() { Latitude = -33.8688, Longitude = 151.2093 },  // Sydney
            new() { Latitude = -23.5505, Longitude = -46.6333 },  // São Paulo
            new() { Latitude = -34.6037, Longitude = -58.3816 }   // Buenos Aires
        };
        var stream = new RouteStream
        {
            Type = "latlng",
            Data = negativeCoords
        };

        // Act & Assert
        Assert.HasCount(3, stream.Data);
        Assert.AreEqual(-33.8688, stream.Data[0].Latitude, 0.0001);
        Assert.AreEqual(151.2093, stream.Data[0].Longitude, 0.0001);
        Assert.AreEqual(-23.5505, stream.Data[1].Latitude, 0.0001);
        Assert.AreEqual(-46.6333, stream.Data[1].Longitude, 0.0001);
    }

    [TestMethod]
    public void RouteStream_WithEquatorCoordinates_ShouldStoreCorrectly()
    {
        // Arrange
        var equatorCoords = new List<LatLng>
        {
            new() { Latitude = 0.0, Longitude = 0.0 },
            new() { Latitude = 0.0, Longitude = 180.0 },
            new() { Latitude = 0.0, Longitude = -180.0 }
        };
        var stream = new RouteStream
        {
            Type = "latlng",
            Data = equatorCoords
        };

        // Act & Assert
        Assert.HasCount(3, stream.Data);
        Assert.AreEqual(0.0, stream.Data[0].Latitude);
        Assert.AreEqual(0.0, stream.Data[0].Longitude);
        Assert.AreEqual(0.0, stream.Data[1].Latitude);
        Assert.AreEqual(180.0, stream.Data[1].Longitude);
    }

    [TestMethod]
    public void RouteStream_WithPolarCoordinates_ShouldStoreCorrectly()
    {
        // Arrange
        var polarCoords = new List<LatLng>
        {
            new() { Latitude = 90.0, Longitude = 0.0 },   // North Pole
            new() { Latitude = -90.0, Longitude = 0.0 }   // South Pole
        };
        var stream = new RouteStream
        {
            Type = "latlng",
            Data = polarCoords
        };

        // Act & Assert
        Assert.HasCount(2, stream.Data);
        Assert.AreEqual(90.0, stream.Data[0].Latitude);
        Assert.AreEqual(-90.0, stream.Data[1].Latitude);
    }

    [TestMethod]
    public void RouteStream_WithLargeDataSet_ShouldHandleCorrectly()
    {
        // Arrange
        var largeData = Enumerable.Range(0, 5000)
            .Select(i => new LatLng { Latitude = i * 0.001, Longitude = i * -0.001 })
            .ToList();
        var stream = new RouteStream
        {
            Type = "latlng",
            SeriesType = "distance",
            OriginalSize = 5000,
            Data = largeData
        };

        // Act & Assert
        Assert.AreEqual(5000, stream.OriginalSize);
        Assert.HasCount(5000, stream.Data);
        Assert.AreEqual(0.0, stream.Data[0].Latitude);
        Assert.AreEqual(0.0, stream.Data[0].Longitude);
        Assert.AreEqual(4.999, stream.Data[4999].Latitude, 0.0001);
        Assert.AreEqual(-4.999, stream.Data[4999].Longitude, 0.0001);
    }

    [TestMethod]
    public void RouteStream_InheritsFromStreamBase()
    {
        // Arrange
        var stream = new RouteStream();

        // Act & Assert
        Assert.IsInstanceOfType<StreamBase>(stream);
    }

    [TestMethod]
    public void RouteStream_WithEmptyData_ShouldBeValid()
    {
        // Arrange
        var stream = new RouteStream
        {
            Type = "latlng",
            SeriesType = "distance",
            OriginalSize = 0,
            Resolution = "high",
            Data = []
        };

        // Act & Assert
        Assert.AreEqual("latlng", stream.Type);
        Assert.HasCount(0, stream.Data);
    }

    [TestMethod]
    public void RouteStream_DataProperty_CanBeReassigned()
    {
        // Arrange
        var stream = new RouteStream
        {
            Data = [
                new LatLng { Latitude = 1.0, Longitude = 1.0 },
                new LatLng { Latitude = 2.0, Longitude = 2.0 }
            ]
        };

        // Act
        stream.Data = [
            new LatLng { Latitude = 3.0, Longitude = 3.0 },
            new LatLng { Latitude = 4.0, Longitude = 4.0 },
            new LatLng { Latitude = 5.0, Longitude = 5.0 }
        ];

        // Assert
        Assert.HasCount(3, stream.Data);
        Assert.AreEqual(3.0, stream.Data[0].Latitude);
        Assert.AreEqual(5.0, stream.Data[2].Latitude);
    }

    [TestMethod]
    public void RouteStream_WithHighResolution_ShouldStoreCorrectly()
    {
        // Arrange
        var stream = new RouteStream
        {
            Type = "latlng",
            Resolution = "high",
            OriginalSize = 1000,
            Data = [.. Enumerable.Range(0, 1000).Select(i => new LatLng { Latitude = 40.0 + i * 0.0001, Longitude = -74.0 + i * 0.0001 })]
        };

        // Act & Assert
        Assert.AreEqual("high", stream.Resolution);
        Assert.AreEqual(1000, stream.OriginalSize);
        Assert.HasCount(1000, stream.Data);
    }

    [TestMethod]
    public void RouteStream_WithLowResolution_ShouldStoreCorrectly()
    {
        // Arrange
        var stream = new RouteStream
        {
            Type = "latlng",
            Resolution = "low",
            OriginalSize = 5000,
            Data = [.. Enumerable.Range(0, 500).Select(i => new LatLng { Latitude = 40.0 + i * 0.001, Longitude = -74.0 + i * 0.001 })]
        };

        // Act & Assert
        Assert.AreEqual("low", stream.Resolution);
        Assert.AreEqual(5000, stream.OriginalSize);
        Assert.HasCount(500, stream.Data);
    }

    [TestMethod]
    public void RouteStream_WithMediumResolution_ShouldStoreCorrectly()
    {
        // Arrange
        var stream = new RouteStream
        {
            Type = "latlng",
            Resolution = "medium",
            OriginalSize = 3000,
            Data = [.. Enumerable.Range(0, 1000).Select(i => new LatLng { Latitude = 40.0 + i * 0.0005, Longitude = -74.0 + i * 0.0005 })]
        };

        // Act & Assert
        Assert.AreEqual("medium", stream.Resolution);
        Assert.AreEqual(3000, stream.OriginalSize);
        Assert.HasCount(1000, stream.Data);
    }

    [TestMethod]
    public void RouteStream_WithAltitudeType_ShouldStoreCorrectly()
    {
        // Arrange
        var stream = new RouteStream
        {
            Type = "altitude",
            SeriesType = "distance",
            Data = [
                new LatLng { Latitude = 37.7749, Longitude = -122.4194 },
                new LatLng { Latitude = 37.7849, Longitude = -122.4294 }
            ]
        };

        // Act & Assert
        Assert.AreEqual("altitude", stream.Type);
        Assert.HasCount(2, stream.Data);
    }

    [TestMethod]
    public void RouteStream_WithDistanceType_ShouldStoreCorrectly()
    {
        // Arrange
        var stream = new RouteStream
        {
            Type = "distance",
            SeriesType = "distance",
            Data = [
                new LatLng { Latitude = 37.7749, Longitude = -122.4194 },
                new LatLng { Latitude = 37.7849, Longitude = -122.4294 }
            ]
        };

        // Act & Assert
        Assert.AreEqual("distance", stream.Type);
        Assert.HasCount(2, stream.Data);
    }

    [TestMethod]
    public void RouteStream_WithMultipleCoordinatePairs_ShouldAccessByIndex()
    {
        // Arrange
        var stream = new RouteStream
        {
            Data = [
                new LatLng { Latitude = 10.0, Longitude = 20.0 },
                new LatLng { Latitude = 30.0, Longitude = 40.0 },
                new LatLng { Latitude = 50.0, Longitude = 60.0 }
            ]
        };

        // Act
        var first = stream.Data[0];
        var second = stream.Data[1];
        var third = stream.Data[2];

        // Assert
        Assert.AreEqual(10.0, first.Latitude);
        Assert.AreEqual(20.0, first.Longitude);
        Assert.AreEqual(30.0, second.Latitude);
        Assert.AreEqual(40.0, second.Longitude);
        Assert.AreEqual(50.0, third.Latitude);
        Assert.AreEqual(60.0, third.Longitude);
    }

    [TestMethod]
    public void RouteStream_WithPreciseCoordinates_ShouldMaintainPrecision()
    {
        // Arrange
        var preciseCoords = new List<LatLng>
        {
            new() { Latitude = 37.7749295, Longitude = -122.4194155 },
            new() { Latitude = 37.7849295, Longitude = -122.4294155 }
        };
        var stream = new RouteStream
        {
            Type = "latlng",
            Data = preciseCoords
        };

        // Act & Assert
        Assert.AreEqual(37.7749295, stream.Data[0].Latitude, 0.0000001);
        Assert.AreEqual(-122.4194155, stream.Data[0].Longitude, 0.0000001);
        Assert.AreEqual(37.7849295, stream.Data[1].Latitude, 0.0000001);
        Assert.AreEqual(-122.4294155, stream.Data[1].Longitude, 0.0000001);
    }

    [TestMethod]
    public void RouteStream_WithZeroCoordinates_ShouldBeValid()
    {
        // Arrange
        var stream = new RouteStream
        {
            Type = "latlng",
            Data = [
                new LatLng { Latitude = 0.0, Longitude = 0.0 }
            ]
        };

        // Act & Assert
        Assert.HasCount(1, stream.Data);
        Assert.AreEqual(0.0, stream.Data[0].Latitude);
        Assert.AreEqual(0.0, stream.Data[0].Longitude);
    }

    [TestMethod]
    public void RouteStream_DataList_CanBeCleared()
    {
        // Arrange
        var stream = new RouteStream
        {
            Data = [
                new LatLng { Latitude = 1.0, Longitude = 1.0 },
                new LatLng { Latitude = 2.0, Longitude = 2.0 }
            ]
        };

        // Act
        stream.Data.Clear();

        // Assert
        Assert.HasCount(0, stream.Data);
    }

    [TestMethod]
    public void RouteStream_WithInternationalDateLine_ShouldStoreCorrectly()
    {
        // Arrange
        var dateLine = new List<LatLng>
        {
            new() { Latitude = 0.0, Longitude = 179.0 },
            new() { Latitude = 0.0, Longitude = -179.0 }
        };
        var stream = new RouteStream
        {
            Type = "latlng",
            Data = dateLine
        };

        // Act & Assert
        Assert.HasCount(2, stream.Data);
        Assert.AreEqual(179.0, stream.Data[0].Longitude);
        Assert.AreEqual(-179.0, stream.Data[1].Longitude);
    }
}

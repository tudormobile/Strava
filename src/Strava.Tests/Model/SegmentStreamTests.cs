using Tudormobile.Strava;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class SegmentStreamTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithEmptyData()
    {
        // Arrange & Act
        var stream = new SegmentStream();

        // Assert
        Assert.IsNotNull(stream);
        Assert.IsNotNull(stream.Data);
        Assert.HasCount(0, stream.Data);
    }

    [TestMethod]
    public void Data_CanBeSetWithLatLngValues()
    {
        // Arrange
        var stream = new SegmentStream
        {
            // Act
            Data = [[37.7749, -122.4194]]
        };

        // Assert
        Assert.AreEqual(37.7749, stream.Points[0].Latitude, 0.0001);
        Assert.AreEqual(-122.4194, stream.Points[0].Longitude, 0.0001);
        Assert.HasCount(1, stream.Data);
    }

    [TestMethod]
    public void Data_CanBeModifiedAfterInitialization()
    {
        // Arrange
        var stream = new SegmentStream();

        // Act
        stream.Data.Add([37.7749, -122.4294]);

        // Assert
        Assert.HasCount(1, stream.Data);
        Assert.AreEqual(37.7749, stream.Points[0].Latitude, 0.0001);
        Assert.AreEqual(-122.4294, stream.Points[0].Longitude, 0.0001);
    }

    [TestMethod]
    public void SegmentStream_WithAllProperties_ShouldRetainValues()
    {
        // Arrange
        List<double> testData = [37.7749, -122.4194];
        var stream = new SegmentStream
        {
            Type = "latlng",
            SeriesType = "distance",
            OriginalSize = 1000,
            Resolution = "high",
            Data = [testData]
        };

        // Act & Assert
        Assert.AreEqual("latlng", stream.Type);
        Assert.AreEqual("distance", stream.SeriesType);
        Assert.AreEqual(1000, stream.OriginalSize);
        Assert.AreEqual("high", stream.Resolution);
        Assert.HasCount(1, stream.Data);
    }

    [TestMethod]
    public void SegmentStream_InheritsFromStreamBase()
    {
        // Arrange
        var stream = new SegmentStream();

        // Act & Assert
        Assert.IsInstanceOfType<StreamBase>(stream);
    }

    [TestMethod]
    public void SegmentStream_WithEmptyData_ShouldBeValid()
    {
        // Arrange
        var stream = new SegmentStream
        {
            Type = "latlng",
            Data = []
        };

        // Act & Assert
        Assert.AreEqual("latlng", stream.Type);
        Assert.HasCount(0, stream.Data);
    }

    [TestMethod]
    public void SegmentStream_CanDeserializeFromJson()
    {
        // Arrange 
        var json = @"{
            ""type"": ""latlng"",
            ""data"": [
              [
                37.833112,
                -122.483436
              ],
              [
                37.832964,
                -122.483406
              ]
            ],
            ""series_type"": ""distance"",
            ""original_size"": 2,
            ""resolution"": ""high""
        }";

        // Act
        var success = StravaSerializer.TryDeserialize<SegmentStream>(json, out var stream);

        // Assert
        Assert.IsTrue(success);
        Assert.IsNotNull(stream);
        Assert.AreEqual("latlng", stream.Type);
        Assert.AreEqual("distance", stream.SeriesType);
        Assert.AreEqual(2, stream.OriginalSize);
        Assert.AreEqual("high", stream.Resolution);
        Assert.IsNotNull(stream.Data);
        Assert.HasCount(2, stream.Data);
        Assert.HasCount(2, stream.Points);
    }
}

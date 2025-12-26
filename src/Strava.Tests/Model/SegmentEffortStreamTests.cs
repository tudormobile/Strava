using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class SegmentEffortStreamTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithEmptyData()
    {
        // Arrange & Act
        var stream = new SegmentEffortStream();

        // Assert
        Assert.IsNotNull(stream);
        Assert.IsNotNull(stream.Data);
        Assert.HasCount(0, stream.Data);
    }

    [TestMethod]
    public void Data_CanBeSetWithValues()
    {
        // Arrange
        var stream = new SegmentEffortStream();
        var testData = new List<double> { 1.0, 2.0, 3.0 };

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
        var stream = new SegmentEffortStream();

        // Act
        stream.Data.Add(10.5);
        stream.Data.Add(20.5);

        // Assert
        Assert.HasCount(2, stream.Data);
        Assert.AreEqual(10.5, stream.Data[0]);
        Assert.AreEqual(20.5, stream.Data[1]);
    }

    [TestMethod]
    public void Type_CanBeSetAndRetrieved()
    {
        // Arrange & Act
        var stream = new SegmentEffortStream
        {
            Type = "altitude"
        };

        // Assert
        Assert.AreEqual("altitude", stream.Type);
    }

    [TestMethod]
    public void SegmentEffortStream_WithAllProperties_ShouldRetainValues()
    {
        // Arrange
        var testData = new List<double> { 100.0, 105.5, 110.0 };
        var stream = new SegmentEffortStream
        {
            Type = "altitude",
            SeriesType = "distance",
            OriginalSize = 1000,
            Resolution = "high",
            Data = testData
        };

        // Act & Assert
        Assert.AreEqual("altitude", stream.Type);
        Assert.AreEqual("distance", stream.SeriesType);
        Assert.AreEqual(1000, stream.OriginalSize);
        Assert.AreEqual("high", stream.Resolution);
        Assert.HasCount(3, stream.Data);
    }

    [TestMethod]
    public void SegmentEffortStream_InheritsFromStreamBase()
    {
        // Arrange
        var stream = new SegmentEffortStream();

        // Act & Assert
        Assert.IsInstanceOfType<StreamBase>(stream);
    }

    [TestMethod]
    public void SegmentEffortStream_WithHeartRateData_ShouldStoreCorrectly()
    {
        // Arrange
        var heartRateData = new List<double> { 120.0, 145.0, 165.0 };
        var stream = new SegmentEffortStream
        {
            Type = "heartrate",
            Data = heartRateData
        };

        // Act & Assert
        Assert.AreEqual("heartrate", stream.Type);
        Assert.HasCount(3, stream.Data);
        Assert.AreEqual(120.0, stream.Data[0]);
    }

    [TestMethod]
    public void SegmentEffortStream_WithEmptyData_ShouldBeValid()
    {
        // Arrange
        var stream = new SegmentEffortStream
        {
            Type = "watts",
            Data = []
        };

        // Act & Assert
        Assert.AreEqual("watts", stream.Type);
        Assert.HasCount(0, stream.Data);
    }
}

using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ActivityStreamTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithEmptyData()
    {
        // Arrange & Act
        var stream = new ActivityStream();

        // Assert
        Assert.IsNotNull(stream);
        Assert.IsNotNull(stream.Data);
        Assert.HasCount(0, stream.Data);
    }

    [TestMethod]
    public void Data_ShouldBeInitializedAsEmptyList()
    {
        // Arrange
        var stream = new ActivityStream();

        // Act
        var data = stream.Data;

        // Assert
        Assert.IsNotNull(data);
        Assert.HasCount(0, data);
    }

    [TestMethod]
    public void Data_CanBeSetWithValues()
    {
        // Arrange
        var stream = new ActivityStream();
        var testData = new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 };

        // Act
        stream.Data = testData;

        // Assert
        Assert.AreEqual(testData, stream.Data);
        Assert.HasCount(5, stream.Data);
    }

    [TestMethod]
    public void Data_CanBeModifiedAfterInitialization()
    {
        // Arrange
        var stream = new ActivityStream();

        // Act
        stream.Data.Add(10.5);
        stream.Data.Add(20.5);
        stream.Data.Add(30.5);

        // Assert
        Assert.HasCount(3, stream.Data);
        Assert.AreEqual(10.5, stream.Data[0]);
        Assert.AreEqual(20.5, stream.Data[1]);
        Assert.AreEqual(30.5, stream.Data[2]);
    }

    [TestMethod]
    public void Type_CanBeSetAndRetrieved()
    {
        // Arrange
        var stream = new ActivityStream
        {
            Type = "altitude"
        };

        // Act
        var type = stream.Type;

        // Assert
        Assert.AreEqual("altitude", type);
    }

    [TestMethod]
    public void SeriesType_CanBeSetAndRetrieved()
    {
        // Arrange
        var stream = new ActivityStream
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
        var stream = new ActivityStream
        {
            OriginalSize = 1500
        };

        // Act
        var originalSize = stream.OriginalSize;

        // Assert
        Assert.AreEqual(1500, originalSize);
    }

    [TestMethod]
    public void Resolution_CanBeSetAndRetrieved()
    {
        // Arrange
        var stream = new ActivityStream
        {
            Resolution = "high"
        };

        // Act
        var resolution = stream.Resolution;

        // Assert
        Assert.AreEqual("high", resolution);
    }

    [TestMethod]
    public void ActivityStream_WithAllProperties_ShouldRetainValues()
    {
        // Arrange
        var testData = new List<double> { 100.0, 105.5, 110.0, 108.5, 115.0 };
        var stream = new ActivityStream
        {
            Type = "altitude",
            SeriesType = "distance",
            OriginalSize = 5000,
            Resolution = "high",
            Data = testData
        };

        // Act & Assert
        Assert.AreEqual("altitude", stream.Type);
        Assert.AreEqual("distance", stream.SeriesType);
        Assert.AreEqual(5000, stream.OriginalSize);
        Assert.AreEqual("high", stream.Resolution);
        Assert.HasCount(5, stream.Data);
        Assert.AreEqual(100.0, stream.Data[0]);
        Assert.AreEqual(115.0, stream.Data[4]);
    }

    [TestMethod]
    public void ActivityStream_WithTimeData_ShouldStoreCorrectly()
    {
        // Arrange
        var timeData = new List<double> { 0, 1, 2, 3, 4, 5 };
        var stream = new ActivityStream
        {
            Type = "time",
            SeriesType = "distance",
            Data = timeData
        };

        // Act & Assert
        Assert.AreEqual("time", stream.Type);
        Assert.HasCount(6, stream.Data);
        Assert.AreEqual(0, stream.Data[0]);
        Assert.AreEqual(5, stream.Data[5]);
    }

    [TestMethod]
    public void ActivityStream_WithDistanceData_ShouldStoreCorrectly()
    {
        // Arrange
        var distanceData = new List<double> { 0.0, 10.5, 25.3, 42.7, 58.9 };
        var stream = new ActivityStream
        {
            Type = "distance",
            SeriesType = "distance",
            Data = distanceData
        };

        // Act & Assert
        Assert.AreEqual("distance", stream.Type);
        Assert.HasCount(5, stream.Data);
        Assert.AreEqual(0.0, stream.Data[0]);
        Assert.AreEqual(58.9, stream.Data[4]);
    }

    [TestMethod]
    public void ActivityStream_WithHeartRateData_ShouldStoreCorrectly()
    {
        // Arrange
        var heartRateData = new List<double> { 65.0, 120.0, 145.0, 155.0, 165.0, 170.0, 160.0 };
        var stream = new ActivityStream
        {
            Type = "heartrate",
            SeriesType = "distance",
            Data = heartRateData
        };

        // Act & Assert
        Assert.AreEqual("heartrate", stream.Type);
        Assert.HasCount(7, stream.Data);
        Assert.AreEqual(65.0, stream.Data[0]);
        Assert.AreEqual(160.0, stream.Data[6]);
    }

    [TestMethod]
    public void ActivityStream_WithCadenceData_ShouldStoreCorrectly()
    {
        // Arrange
        var cadenceData = new List<double> { 0, 75, 80, 85, 90, 88 };
        var stream = new ActivityStream
        {
            Type = "cadence",
            SeriesType = "distance",
            Data = cadenceData
        };

        // Act & Assert
        Assert.AreEqual("cadence", stream.Type);
        Assert.HasCount(6, stream.Data);
        Assert.AreEqual(0, stream.Data[0]);
        Assert.AreEqual(88, stream.Data[5]);
    }

    [TestMethod]
    public void ActivityStream_WithWattsData_ShouldStoreCorrectly()
    {
        // Arrange
        var wattsData = new List<double> { 0, 150, 200, 250, 300, 280, 260 };
        var stream = new ActivityStream
        {
            Type = "watts",
            SeriesType = "distance",
            Data = wattsData
        };

        // Act & Assert
        Assert.AreEqual("watts", stream.Type);
        Assert.HasCount(7, stream.Data);
        Assert.AreEqual(0, stream.Data[0]);
        Assert.AreEqual(260, stream.Data[6]);
    }

    [TestMethod]
    public void ActivityStream_WithVelocitySmoothData_ShouldStoreCorrectly()
    {
        // Arrange
        var velocityData = new List<double> { 0.0, 2.5, 3.0, 3.5, 4.0, 3.8 };
        var stream = new ActivityStream
        {
            Type = "velocity_smooth",
            SeriesType = "distance",
            Data = velocityData
        };

        // Act & Assert
        Assert.AreEqual("velocity_smooth", stream.Type);
        Assert.HasCount(6, stream.Data);
        Assert.AreEqual(0.0, stream.Data[0]);
        Assert.AreEqual(3.8, stream.Data[5]);
    }

    [TestMethod]
    public void ActivityStream_WithGradeData_ShouldStoreCorrectly()
    {
        // Arrange
        var gradeData = new List<double> { 0.0, 1.5, 2.0, 3.5, -1.0, -2.5 };
        var stream = new ActivityStream
        {
            Type = "grade_smooth",
            SeriesType = "distance",
            Data = gradeData
        };

        // Act & Assert
        Assert.AreEqual("grade_smooth", stream.Type);
        Assert.HasCount(6, stream.Data);
        Assert.AreEqual(0.0, stream.Data[0]);
        Assert.AreEqual(-2.5, stream.Data[5]);
    }

    [TestMethod]
    public void ActivityStream_WithTemperatureData_ShouldStoreCorrectly()
    {
        // Arrange
        var tempData = new List<double> { 20.0, 20.5, 21.0, 21.5, 22.0 };
        var stream = new ActivityStream
        {
            Type = "temp",
            SeriesType = "distance",
            Data = tempData
        };

        // Act & Assert
        Assert.AreEqual("temp", stream.Type);
        Assert.HasCount(5, stream.Data);
        Assert.AreEqual(20.0, stream.Data[0]);
        Assert.AreEqual(22.0, stream.Data[4]);
    }

    [TestMethod]
    public void ActivityStream_WithMovingData_ShouldStoreCorrectly()
    {
        // Arrange
        var movingData = new List<double> { 1, 1, 1, 0, 0, 1, 1 };
        var stream = new ActivityStream
        {
            Type = "moving",
            SeriesType = "distance",
            Data = movingData
        };

        // Act & Assert
        Assert.AreEqual("moving", stream.Type);
        Assert.HasCount(7, stream.Data);
        Assert.AreEqual(1, stream.Data[0]);
        Assert.AreEqual(0, stream.Data[3]);
    }

    [TestMethod]
    public void ActivityStream_WithLargeDataSet_ShouldHandleCorrectly()
    {
        // Arrange
        var largeData = Enumerable.Range(0, 10000).Select(i => (double)i).ToList();
        var stream = new ActivityStream
        {
            Type = "distance",
            SeriesType = "distance",
            OriginalSize = 10000,
            Data = largeData
        };

        // Act & Assert
        Assert.AreEqual(10000, stream.OriginalSize);
        Assert.HasCount(10000, stream.Data);
        Assert.AreEqual(0, stream.Data[0]);
        Assert.AreEqual(9999, stream.Data[9999]);
    }

    [TestMethod]
    public void ActivityStream_InheritsFromStreamBase()
    {
        // Arrange
        var stream = new ActivityStream();

        // Act & Assert
        Assert.IsInstanceOfType<StreamBase>(stream);
    }

    [TestMethod]
    public void ActivityStream_WithNullType_ShouldBeAllowed()
    {
        // Arrange & Act
        var stream = new ActivityStream
        {
            Type = null!
        };

        // Assert
        Assert.IsNull(stream.Type);
    }

    [TestMethod]
    public void ActivityStream_WithEmptyData_ShouldBeValid()
    {
        // Arrange
        var stream = new ActivityStream
        {
            Type = "altitude",
            SeriesType = "distance",
            OriginalSize = 0,
            Resolution = "high",
            Data = []
        };

        // Act & Assert
        Assert.AreEqual("altitude", stream.Type);
        Assert.HasCount(0, stream.Data);
    }

    [TestMethod]
    public void ActivityStream_WithNegativeValues_ShouldStoreCorrectly()
    {
        // Arrange
        var negativeData = new List<double> { -10.5, -20.3, -15.7, -5.2, 0.0 };
        var stream = new ActivityStream
        {
            Type = "altitude",
            Data = negativeData
        };

        // Act & Assert
        Assert.HasCount(5, stream.Data);
        Assert.AreEqual(-10.5, stream.Data[0]);
        Assert.AreEqual(0.0, stream.Data[4]);
    }

    [TestMethod]
    public void ActivityStream_DataProperty_CanBeReassigned()
    {
        // Arrange
        var stream = new ActivityStream
        {
            Data = [1.0, 2.0, 3.0]
        };

        // Act
        stream.Data = [4.0, 5.0, 6.0, 7.0];

        // Assert
        Assert.HasCount(4, stream.Data);
        Assert.AreEqual(4.0, stream.Data[0]);
        Assert.AreEqual(7.0, stream.Data[3]);
    }

    [TestMethod]
    public void ActivityStream_WithZeroOriginalSize_ShouldBeValid()
    {
        // Arrange & Act
        var stream = new ActivityStream
        {
            OriginalSize = 0,
            Data = []
        };

        // Assert
        Assert.AreEqual(0, stream.OriginalSize);
        Assert.HasCount(0, stream.Data);
    }

    [TestMethod]
    public void ActivityStream_WithHighResolution_ShouldStoreCorrectly()
    {
        // Arrange
        var stream = new ActivityStream
        {
            Type = "altitude",
            Resolution = "high",
            OriginalSize = 1000,
            Data = [.. Enumerable.Range(0, 1000).Select(i => (double)i)]
        };

        // Act & Assert
        Assert.AreEqual("high", stream.Resolution);
        Assert.AreEqual(1000, stream.OriginalSize);
        Assert.HasCount(1000, stream.Data);
    }

    [TestMethod]
    public void ActivityStream_WithLowResolution_ShouldStoreCorrectly()
    {
        // Arrange
        var stream = new ActivityStream
        {
            Type = "altitude",
            Resolution = "low",
            OriginalSize = 5000,
            Data = [.. Enumerable.Range(0, 500).Select(i => (double)i)]
        };

        // Act & Assert
        Assert.AreEqual("low", stream.Resolution);
        Assert.AreEqual(5000, stream.OriginalSize);
        Assert.HasCount(500, stream.Data);
    }

    [TestMethod]
    public void ActivityStream_WithMediumResolution_ShouldStoreCorrectly()
    {
        // Arrange
        var stream = new ActivityStream
        {
            Type = "altitude",
            Resolution = "medium",
            OriginalSize = 3000,
            Data = [.. Enumerable.Range(0, 1000).Select(i => (double)i)]
        };

        // Act & Assert
        Assert.AreEqual("medium", stream.Resolution);
        Assert.AreEqual(3000, stream.OriginalSize);
        Assert.HasCount(1000, stream.Data);
    }
}

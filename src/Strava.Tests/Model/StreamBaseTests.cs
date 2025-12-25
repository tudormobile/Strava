using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class StreamBaseTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var stream = new StreamBase();

        // Assert
        Assert.IsNotNull(stream);
    }

    [TestMethod]
    public void Type_CanBeSetAndRetrieved()
    {
        // Arrange
        var stream = new StreamBase
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
        var stream = new StreamBase
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
        var stream = new StreamBase
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
        var stream = new StreamBase
        {
            Resolution = "high"
        };

        // Act
        var resolution = stream.Resolution;

        // Assert
        Assert.AreEqual("high", resolution);
    }

    [TestMethod]
    public void StreamBase_WithAllProperties_ShouldRetainValues()
    {
        // Arrange
        var stream = new StreamBase
        {
            Type = "heartrate",
            SeriesType = "time",
            OriginalSize = 3000,
            Resolution = "medium"
        };

        // Act & Assert
        Assert.AreEqual("heartrate", stream.Type);
        Assert.AreEqual("time", stream.SeriesType);
        Assert.AreEqual(3000, stream.OriginalSize);
        Assert.AreEqual("medium", stream.Resolution);
    }

    [TestMethod]
    public void Type_WithVariousStreamTypes_ShouldStore()
    {
        // Arrange & Act
        var streams = new[]
        {
            new StreamBase { Type = "time" },
            new StreamBase { Type = "latlng" },
            new StreamBase { Type = "distance" },
            new StreamBase { Type = "altitude" },
            new StreamBase { Type = "velocity_smooth" },
            new StreamBase { Type = "heartrate" },
            new StreamBase { Type = "cadence" },
            new StreamBase { Type = "watts" },
            new StreamBase { Type = "temp" },
            new StreamBase { Type = "moving" },
            new StreamBase { Type = "grade_smooth" }
        };

        // Assert
        Assert.AreEqual("time", streams[0].Type);
        Assert.AreEqual("latlng", streams[1].Type);
        Assert.AreEqual("distance", streams[2].Type);
        Assert.AreEqual("altitude", streams[3].Type);
        Assert.AreEqual("velocity_smooth", streams[4].Type);
        Assert.AreEqual("heartrate", streams[5].Type);
        Assert.AreEqual("cadence", streams[6].Type);
        Assert.AreEqual("watts", streams[7].Type);
        Assert.AreEqual("temp", streams[8].Type);
        Assert.AreEqual("moving", streams[9].Type);
        Assert.AreEqual("grade_smooth", streams[10].Type);
    }

    [TestMethod]
    public void OriginalSize_WithZeroValue_ShouldBeValid()
    {
        // Arrange & Act
        var stream = new StreamBase
        {
            OriginalSize = 0
        };

        // Assert
        Assert.AreEqual(0, stream.OriginalSize);
    }

    [TestMethod]
    public void Resolution_WithVariousResolutions_ShouldStore()
    {
        // Arrange & Act
        var lowRes = new StreamBase { Resolution = "low" };
        var mediumRes = new StreamBase { Resolution = "medium" };
        var highRes = new StreamBase { Resolution = "high" };

        // Assert
        Assert.AreEqual("low", lowRes.Resolution);
        Assert.AreEqual("medium", mediumRes.Resolution);
        Assert.AreEqual("high", highRes.Resolution);
    }

    [TestMethod]
    public void StreamBase_WithNullProperties_ShouldBeAllowed()
    {
        // Arrange & Act
        var stream = new StreamBase
        {
            Type = null!,
            SeriesType = null!,
            Resolution = null!
        };

        // Assert
        Assert.IsNull(stream.Type);
        Assert.IsNull(stream.SeriesType);
        Assert.IsNull(stream.Resolution);
        Assert.AreEqual(0, stream.OriginalSize);
    }
}

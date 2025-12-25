using System.Text.Json;
using Tudormobile.Strava.Converters;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Converters;

[TestClass]
public class LatLngConverterTests
{
    private JsonSerializerOptions? _options;

    [TestInitialize]
    public void Setup()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new LatLngConverter());
    }

    [TestMethod]
    public void Read_ValidArray_ShouldReturnLatLng()
    {
        // Arrange
        var json = "[37.8280722, -122.4981393]";

        // Act
        var result = JsonSerializer.Deserialize<LatLng>(json, _options);

        // Assert
        Assert.AreEqual(37.8280722f, result.Latitude, 0.0001f);
        Assert.AreEqual(-122.4981393f, result.Longitude, 0.0001f);
    }

    [TestMethod]
    public void Read_NullValue_ShouldReturnDefaultLatLng()
    {
        // Arrange
        var json = "null";

        // Act
        var result = JsonSerializer.Deserialize<LatLng>(json, _options);

        // Assert
        Assert.AreEqual(0f, result.Latitude);
        Assert.AreEqual(0f, result.Longitude);
    }

    [TestMethod]
    public void Read_PositiveCoordinates_ShouldReturnCorrectValues()
    {
        // Arrange
        var json = "[40.7128, 74.0060]";

        // Act
        var result = JsonSerializer.Deserialize<LatLng>(json, _options);

        // Assert
        Assert.AreEqual(40.7128f, result.Latitude, 0.0001f);
        Assert.AreEqual(74.0060f, result.Longitude, 0.0001f);
    }

    [TestMethod]
    public void Read_NegativeCoordinates_ShouldReturnCorrectValues()
    {
        // Arrange
        var json = "[-33.8688, -151.2093]";

        // Act
        var result = JsonSerializer.Deserialize<LatLng>(json, _options);

        // Assert
        Assert.AreEqual(-33.8688f, result.Latitude, 0.0001f);
        Assert.AreEqual(-151.2093f, result.Longitude, 0.0001f);
    }

    [TestMethod]
    public void Read_ZeroCoordinates_ShouldReturnZeroValues()
    {
        // Arrange
        var json = "[0.0, 0.0]";

        // Act
        var result = JsonSerializer.Deserialize<LatLng>(json, _options);

        // Assert
        Assert.AreEqual(0f, result.Latitude);
        Assert.AreEqual(0f, result.Longitude);
    }

    [TestMethod]
    public void Read_StringType_ShouldReturnDefault()
    {
        // Arrange
        var json = "\"not an array\"";

        // Act & Assert
        Assert.AreEqual(default, JsonSerializer.Deserialize<LatLng>(json, _options));
    }

    [TestMethod]
    public void Read_ArrayWithOnlyOneElement_ShouldThrowJsonException()
    {
        // Arrange
        var json = "[37.8280722]";

        // Act & Assert
        Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<LatLng>(json, _options));
    }

    [TestMethod]
    public void Read_ArrayWithMoreThanTwoElements_ShouldThrowJsonException()
    {
        // Arrange
        var json = "[37.8280722, -122.4981393, 100]";

        // Act & Assert
        Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<LatLng>(json, _options));
    }

    [TestMethod]
    public void Read_ArrayWithNonNumericElements_ShouldThrowJsonException()
    {
        // Arrange
        var json = "[\"37.8280722\", \"-122.4981393\"]";

        // Act & Assert
        Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<LatLng>(json, _options));
    }

    [TestMethod]
    public void Write_ValidLatLng_ShouldWriteArray()
    {
        // Arrange
        var latLng = new LatLng
        {
            Latitude = 37.8280722,
            Longitude = -122.4981393
        };

        // Act
        var json = JsonSerializer.Serialize(latLng, _options);

        // Assert
        Assert.AreEqual("[37.8280722,-122.4981393]", json);
    }

    [TestMethod]
    public void Write_ZeroCoordinates_ShouldWriteZeroArray()
    {
        // Arrange
        var latLng = new LatLng
        {
            Latitude = 0,
            Longitude = 0
        };

        // Act
        var json = JsonSerializer.Serialize(latLng, _options);

        // Assert
        Assert.AreEqual("[0,0]", json);
    }

    [TestMethod]
    public void Write_PositiveCoordinates_ShouldWriteCorrectArray()
    {
        // Arrange
        var latLng = new LatLng
        {
            Latitude = 40.7128,
            Longitude = 74.0060
        };

        // Act
        var json = JsonSerializer.Serialize(latLng, _options);

        // Assert
        Assert.AreEqual("[40.7128,74.006]", json);
    }

    [TestMethod]
    public void Write_NegativeCoordinates_ShouldWriteCorrectArray()
    {
        // Arrange
        var latLng = new LatLng
        {
            Latitude = -33.8688,
            Longitude = -151.2093
        };

        // Act
        var json = JsonSerializer.Serialize(latLng, _options);

        // Assert
        Assert.AreEqual("[-33.8688,-151.2093]", json);
    }

    [TestMethod]
    public void RoundTrip_ValidLatLng_ShouldPreserveValues()
    {
        // Arrange
        var original = new LatLng
        {
            Latitude = 37.8280722f,
            Longitude = -122.4981393f
        };

        // Act
        var json = JsonSerializer.Serialize(original, _options);
        var result = JsonSerializer.Deserialize<LatLng>(json, _options);

        // Assert
        Assert.AreEqual(original.Latitude, result.Latitude, 0.0001f);
        Assert.AreEqual(original.Longitude, result.Longitude, 0.0001f);
    }

    [TestMethod]
    public void RoundTrip_MultipleCoordinates_ShouldPreserveAllValues()
    {
        // Arrange
        var coordinates = new[]
        {
            new LatLng { Latitude = 37.8280722f, Longitude = -122.4981393f },
            new LatLng { Latitude = 40.7128f, Longitude = 74.0060f },
            new LatLng { Latitude = -33.8688f, Longitude = -151.2093f },
            new LatLng { Latitude = 0f, Longitude = 0f }
        };

        foreach (var original in coordinates)
        {
            // Act
            var json = JsonSerializer.Serialize(original, _options);
            var result = JsonSerializer.Deserialize<LatLng>(json, _options);

            // Assert
            Assert.AreEqual(original.Latitude, result.Latitude, 0.0001f);
            Assert.AreEqual(original.Longitude, result.Longitude, 0.0001f);
        }
    }

}

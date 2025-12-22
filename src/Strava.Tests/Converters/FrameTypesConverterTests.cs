using System.Text.Json;
using Tudormobile.Strava.Converters;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Converters;

[TestClass]
public class FrameTypesConverterTests
{
    private JsonSerializerOptions? _options;

    [TestInitialize]
    public void Setup()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new FrameTypesConverter());
    }

    [TestMethod]
    public void Read_NumericZero_ShouldReturnNone()
    {
        // Arrange
        var json = "0";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.None, result);
    }

    [TestMethod]
    public void Read_NumericOne_ShouldReturnRoad()
    {
        // Arrange
        var json = "1";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Road, result);
    }

    [TestMethod]
    public void Read_NumericTwo_ShouldReturnMountain()
    {
        // Arrange
        var json = "2";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Mountain, result);
    }

    [TestMethod]
    public void Read_NumericThree_ShouldReturnHybrid()
    {
        // Arrange
        var json = "3";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Hybrid, result);
    }

    [TestMethod]
    public void Read_NumericFour_ShouldReturnCruiser()
    {
        // Arrange
        var json = "4";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Cruiser, result);
    }

    [TestMethod]
    public void Read_NumericFive_ShouldReturnCity()
    {
        // Arrange
        var json = "5";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.City, result);
    }

    [TestMethod]
    public void Read_NumericSix_ShouldReturnElectric()
    {
        // Arrange
        var json = "6";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Electric, result);
    }

    [TestMethod]
    public void Read_NumericSeven_ShouldReturnFolding()
    {
        // Arrange
        var json = "7";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Folding, result);
    }

    [TestMethod]
    public void Read_NumericEight_ShouldReturnTandem()
    {
        // Arrange
        var json = "8";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Tandem, result);
    }

    [TestMethod]
    public void Read_InvalidNumericValue_ShouldReturnNone()
    {
        // Arrange
        var json = "999";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.None, result);
    }

    [TestMethod]
    public void Read_NegativeNumericValue_ShouldReturnNone()
    {
        // Arrange
        var json = "-1";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.None, result);
    }

    [TestMethod]
    public void Read_StringNone_ShouldReturnNone()
    {
        // Arrange
        var json = "\"None\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.None, result);
    }

    [TestMethod]
    public void Read_StringRoad_ShouldReturnRoad()
    {
        // Arrange
        var json = "\"Road\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Road, result);
    }

    [TestMethod]
    public void Read_StringMountain_ShouldReturnMountain()
    {
        // Arrange
        var json = "\"Mountain\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Mountain, result);
    }

    [TestMethod]
    public void Read_StringHybrid_ShouldReturnHybrid()
    {
        // Arrange
        var json = "\"Hybrid\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Hybrid, result);
    }

    [TestMethod]
    public void Read_StringCruiser_ShouldReturnCruiser()
    {
        // Arrange
        var json = "\"Cruiser\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Cruiser, result);
    }

    [TestMethod]
    public void Read_StringCity_ShouldReturnCity()
    {
        // Arrange
        var json = "\"City\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.City, result);
    }

    [TestMethod]
    public void Read_StringElectric_ShouldReturnElectric()
    {
        // Arrange
        var json = "\"Electric\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Electric, result);
    }

    [TestMethod]
    public void Read_StringFolding_ShouldReturnFolding()
    {
        // Arrange
        var json = "\"Folding\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Folding, result);
    }

    [TestMethod]
    public void Read_StringTandem_ShouldReturnTandem()
    {
        // Arrange
        var json = "\"Tandem\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Tandem, result);
    }

    [TestMethod]
    public void Read_StringLowerCase_ShouldReturnCorrectValue()
    {
        // Arrange
        var json = "\"road\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Road, result);
    }

    [TestMethod]
    public void Read_StringMixedCase_ShouldReturnCorrectValue()
    {
        // Arrange
        var json = "\"mOuNtAiN\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.Mountain, result);
    }

    [TestMethod]
    public void Read_InvalidString_ShouldReturnNone()
    {
        // Arrange
        var json = "\"InvalidType\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.None, result);
    }

    [TestMethod]
    public void Read_EmptyString_ShouldReturnNone()
    {
        // Arrange
        var json = "\"\"";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.None, result);
    }

    [TestMethod]
    public void Read_NullToken_ShouldReturnNone()
    {
        // Arrange
        var json = "null";

        // Act
        var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

        // Assert
        Assert.AreEqual(FrameTypes.None, result);
    }

    [TestMethod]
    public void Write_None_ShouldWriteZero()
    {
        // Arrange
        var frameType = FrameTypes.None;

        // Act
        var json = JsonSerializer.Serialize(frameType, _options);

        // Assert
        Assert.AreEqual("0", json);
    }

    [TestMethod]
    public void Write_Road_ShouldWriteOne()
    {
        // Arrange
        var frameType = FrameTypes.Road;

        // Act
        var json = JsonSerializer.Serialize(frameType, _options);

        // Assert
        Assert.AreEqual("1", json);
    }

    [TestMethod]
    public void Write_Mountain_ShouldWriteTwo()
    {
        // Arrange
        var frameType = FrameTypes.Mountain;

        // Act
        var json = JsonSerializer.Serialize(frameType, _options);

        // Assert
        Assert.AreEqual("2", json);
    }

    [TestMethod]
    public void Write_Hybrid_ShouldWriteThree()
    {
        // Arrange
        var frameType = FrameTypes.Hybrid;

        // Act
        var json = JsonSerializer.Serialize(frameType, _options);

        // Assert
        Assert.AreEqual("3", json);
    }

    [TestMethod]
    public void Write_Cruiser_ShouldWriteFour()
    {
        // Arrange
        var frameType = FrameTypes.Cruiser;

        // Act
        var json = JsonSerializer.Serialize(frameType, _options);

        // Assert
        Assert.AreEqual("4", json);
    }

    [TestMethod]
    public void Write_City_ShouldWriteFive()
    {
        // Arrange
        var frameType = FrameTypes.City;

        // Act
        var json = JsonSerializer.Serialize(frameType, _options);

        // Assert
        Assert.AreEqual("5", json);
    }

    [TestMethod]
    public void Write_Electric_ShouldWriteSix()
    {
        // Arrange
        var frameType = FrameTypes.Electric;

        // Act
        var json = JsonSerializer.Serialize(frameType, _options);

        // Assert
        Assert.AreEqual("6", json);
    }

    [TestMethod]
    public void Write_Folding_ShouldWriteSeven()
    {
        // Arrange
        var frameType = FrameTypes.Folding;

        // Act
        var json = JsonSerializer.Serialize(frameType, _options);

        // Assert
        Assert.AreEqual("7", json);
    }

    [TestMethod]
    public void Write_Tandem_ShouldWriteEight()
    {
        // Arrange
        var frameType = FrameTypes.Tandem;

        // Act
        var json = JsonSerializer.Serialize(frameType, _options);

        // Assert
        Assert.AreEqual("8", json);
    }

    [TestMethod]
    public void RoundTrip_NumericValues_ShouldPreserveValue()
    {
        // Arrange
        var frameTypes = new[] { FrameTypes.None, FrameTypes.Road, FrameTypes.Mountain, FrameTypes.Hybrid,
                                 FrameTypes.Cruiser, FrameTypes.City, FrameTypes.Electric, FrameTypes.Folding, FrameTypes.Tandem };

        foreach (var frameType in frameTypes)
        {
            // Act
            var json = JsonSerializer.Serialize(frameType, _options);
            var result = JsonSerializer.Deserialize<FrameTypes>(json, _options);

            // Assert
            Assert.AreEqual(frameType, result, $"Round-trip failed for {frameType}");
        }
    }

}

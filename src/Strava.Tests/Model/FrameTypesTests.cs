using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class FrameTypesTests
{
    [TestMethod]
    public void FrameTypes_None_ShouldHaveValueZero()
    {
        // Arrange & Act
        var value = (int)FrameTypes.None;

        // Assert
        Assert.AreEqual(0, value);
    }

    [TestMethod]
    public void FrameTypes_Road_ShouldHaveValueOne()
    {
        // Arrange & Act
        var value = (int)FrameTypes.Road;

        // Assert
        Assert.AreEqual(1, value);
    }

    [TestMethod]
    public void FrameTypes_Mountain_ShouldHaveValueTwo()
    {
        // Arrange & Act
        var value = (int)FrameTypes.Mountain;

        // Assert
        Assert.AreEqual(2, value);
    }

    [TestMethod]
    public void FrameTypes_Hybrid_ShouldHaveValueThree()
    {
        // Arrange & Act
        var value = (int)FrameTypes.Hybrid;

        // Assert
        Assert.AreEqual(3, value);
    }

    [TestMethod]
    public void FrameTypes_Cruiser_ShouldHaveValueFour()
    {
        // Arrange & Act
        var value = (int)FrameTypes.Cruiser;

        // Assert
        Assert.AreEqual(4, value);
    }

    [TestMethod]
    public void FrameTypes_City_ShouldHaveValueFive()
    {
        // Arrange & Act
        var value = (int)FrameTypes.City;

        // Assert
        Assert.AreEqual(5, value);
    }

    [TestMethod]
    public void FrameTypes_Electric_ShouldHaveValueSix()
    {
        // Arrange & Act
        var value = (int)FrameTypes.Electric;

        // Assert
        Assert.AreEqual(6, value);
    }

    [TestMethod]
    public void FrameTypes_Folding_ShouldHaveValueSeven()
    {
        // Arrange & Act
        var value = (int)FrameTypes.Folding;

        // Assert
        Assert.AreEqual(7, value);
    }

    [TestMethod]
    public void FrameTypes_Tandem_ShouldHaveValueEight()
    {
        // Arrange & Act
        var value = (int)FrameTypes.Tandem;

        // Assert
        Assert.AreEqual(8, value);
    }

    [TestMethod]
    public void FrameTypes_AllValues_ShouldBeDefined()
    {
        // Arrange
        var values = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        // Act & Assert
        foreach (var value in values)
        {
            Assert.IsTrue(Enum.IsDefined(typeof(FrameTypes), value), $"Value {value} is not defined in FrameTypes enum");
        }
    }

    [TestMethod]
    public void FrameTypes_InvalidValue_ShouldNotBeDefined()
    {
        // Arrange
        var invalidValue = 999;

        // Act
        var isDefined = Enum.IsDefined(typeof(FrameTypes), invalidValue);

        // Assert
        Assert.IsFalse(isDefined);
    }

    [TestMethod]
    public void FrameTypes_ShouldHaveNineValues()
    {
        // Arrange & Act
        var count = Enum.GetValues<FrameTypes>().Length;

        // Assert
        Assert.AreEqual(9, count);
    }

    [TestMethod]
    public void FrameTypes_None_ToStringShouldReturnNone()
    {
        // Arrange
        var frameType = FrameTypes.None;

        // Act
        var result = frameType.ToString();

        // Assert
        Assert.AreEqual("None", result);
    }

    [TestMethod]
    public void FrameTypes_Road_ToStringShouldReturnRoad()
    {
        // Arrange
        var frameType = FrameTypes.Road;

        // Act
        var result = frameType.ToString();

        // Assert
        Assert.AreEqual("Road", result);
    }

    [TestMethod]
    public void FrameTypes_Mountain_ToStringShouldReturnMountain()
    {
        // Arrange
        var frameType = FrameTypes.Mountain;

        // Act
        var result = frameType.ToString();

        // Assert
        Assert.AreEqual("Mountain", result);
    }

    [TestMethod]
    public void FrameTypes_ParseRoad_ShouldReturnRoadEnum()
    {
        // Arrange
        var input = "Road";

        // Act
        var result = Enum.Parse<FrameTypes>(input);

        // Assert
        Assert.AreEqual(FrameTypes.Road, result);
    }

    [TestMethod]
    public void FrameTypes_ParseMountain_ShouldReturnMountainEnum()
    {
        // Arrange
        var input = "Mountain";

        // Act
        var result = Enum.Parse<FrameTypes>(input);

        // Assert
        Assert.AreEqual(FrameTypes.Mountain, result);
    }

    [TestMethod]
    public void FrameTypes_ParseRoadIgnoreCase_ShouldReturnRoadEnum()
    {
        // Arrange
        var input = "road";

        // Act
        var result = Enum.Parse<FrameTypes>(input, ignoreCase: true);

        // Assert
        Assert.AreEqual(FrameTypes.Road, result);
    }

    [TestMethod]
    public void FrameTypes_ParseInvalidValue_ShouldThrowArgumentException()
    {
        // Arrange
        var input = "InvalidFrameType";

        // Act & Assert
        Assert.ThrowsExactly<ArgumentException>(() => Enum.Parse<FrameTypes>(input));
    }

    [TestMethod]
    public void FrameTypes_TryParseRoad_ShouldReturnTrue()
    {
        // Arrange
        var input = "Road";

        // Act
        var success = Enum.TryParse<FrameTypes>(input, out var result);

        // Assert
        Assert.IsTrue(success);
        Assert.AreEqual(FrameTypes.Road, result);
    }

    [TestMethod]
    public void FrameTypes_TryParseElectric_ShouldReturnTrue()
    {
        // Arrange
        var input = "Electric";

        // Act
        var success = Enum.TryParse<FrameTypes>(input, out var result);

        // Assert
        Assert.IsTrue(success);
        Assert.AreEqual(FrameTypes.Electric, result);
    }

    [TestMethod]
    public void FrameTypes_TryParseRoadIgnoreCase_ShouldReturnTrue()
    {
        // Arrange
        var input = "mountain";

        // Act
        var success = Enum.TryParse<FrameTypes>(input, ignoreCase: true, out var result);

        // Assert
        Assert.IsTrue(success);
        Assert.AreEqual(FrameTypes.Mountain, result);
    }

    [TestMethod]
    public void FrameTypes_TryParseInvalidValue_ShouldReturnFalse()
    {
        // Arrange
        var input = "InvalidFrameType";

        // Act
        var success = Enum.TryParse<FrameTypes>(input, out var result);

        // Assert
        Assert.IsFalse(success);
        Assert.AreEqual(default, result);
    }

    [TestMethod]
    public void FrameTypes_TryParseNumericString_ShouldReturnTrue()
    {
        // Arrange
        var input = "2";

        // Act
        var success = Enum.TryParse<FrameTypes>(input, out var result);

        // Assert
        Assert.IsTrue(success);
        Assert.AreEqual(FrameTypes.Mountain, result);
    }

    [TestMethod]
    public void FrameTypes_CastIntegerToEnum_ShouldReturnCorrectValue()
    {
        // Arrange
        var intValue = 3;

        // Act
        var result = (FrameTypes)intValue;

        // Assert
        Assert.AreEqual(FrameTypes.Hybrid, result);
    }

    [TestMethod]
    public void FrameTypes_CastEnumToInteger_ShouldReturnCorrectValue()
    {
        // Arrange
        var frameType = FrameTypes.City;

        // Act
        var result = (int)frameType;

        // Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void FrameTypes_GetValues_ShouldContainAllFrameTypes()
    {
        // Arrange & Act
        var values = Enum.GetValues<FrameTypes>();

        // Assert
        CollectionAssert.Contains(values, FrameTypes.None);
        CollectionAssert.Contains(values, FrameTypes.Road);
        CollectionAssert.Contains(values, FrameTypes.Mountain);
        CollectionAssert.Contains(values, FrameTypes.Hybrid);
        CollectionAssert.Contains(values, FrameTypes.Cruiser);
        CollectionAssert.Contains(values, FrameTypes.City);
        CollectionAssert.Contains(values, FrameTypes.Electric);
        CollectionAssert.Contains(values, FrameTypes.Folding);
        CollectionAssert.Contains(values, FrameTypes.Tandem);
    }

    [TestMethod]
    public void FrameTypes_Equality_ShouldWorkCorrectly()
    {
        // Arrange
        var frameType1 = FrameTypes.Road;
        var frameType2 = FrameTypes.Road;
        var frameType3 = FrameTypes.Mountain;

        // Act & Assert
        Assert.AreEqual(frameType1, frameType2);
        Assert.AreNotEqual(frameType1, frameType3);
    }

    [TestMethod]
    public void FrameTypes_DefaultValue_ShouldBeNone()
    {
        // Arrange & Act
        var defaultValue = default(FrameTypes);

        // Assert
        Assert.AreEqual(FrameTypes.None, defaultValue);
    }
}

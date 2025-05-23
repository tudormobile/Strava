using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Tudormobile.Strava.UI.Converters;

namespace Strava.Tests.Converters;

[TestClass]
public class AddConverterTests
{
    private AddConverter _converter = new AddConverter();

    [TestMethod]
    public void Convert_AddsValueAndParameter_ReturnsSum()
    {
        // Arrange
        double value = 5;
        double parameter = 3;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = _converter.Convert(value, typeof(double), parameter, culture);

        // Assert
        Assert.AreEqual(8.0, result);
    }

    [TestMethod]
    public void Convert_ValueIsNull_ReturnsParameter()
    {
        // Arrange
        double? value = null;
        double parameter = 2;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = _converter.Convert(value, typeof(int), parameter, culture);

        // Assert
        Assert.AreEqual(2.0, result);
    }

    [TestMethod]
    public void Convert_ParameterIsNull_ReturnsValue()
    {
        // Arrange
        double value = 7;
        double? parameter = null;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = _converter.Convert(value, typeof(int), parameter, culture);

        // Assert
        Assert.AreEqual(7.0, result);
    }

    [TestMethod, ExcludeFromCodeCoverage, ExpectedException(typeof(FormatException))]
    public void Convert_InvalidValue_ThrowsException()
    {
        // Arrange
        var value = "abc";
        double parameter = 1;
        var culture = CultureInfo.InvariantCulture;

        // Act & Assert
        _converter.Convert(value, typeof(int), parameter, culture);
    }

    [TestMethod, ExcludeFromCodeCoverage, ExpectedException(typeof(FormatException))]
    public void Convert_InvalidParameter_ThrowsException()
    {
        // Arrange
        double value = 1;
        var parameter = "xyz";
        var culture = CultureInfo.InvariantCulture;

        // Act & Assert
        _converter.Convert(value, typeof(int), parameter, culture);
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void ConvertBack_NotImplemented()
    {
        // Act & Assert
        Assert.ThrowsException<NotImplementedException>(() =>
            _converter.ConvertBack(123, typeof(int), null, null));
    }

}

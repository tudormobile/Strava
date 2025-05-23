using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Tudormobile.Strava.UI.Converters;

namespace Strava.Tests.Converters;

[TestClass]
public class SpeedConverterTests
{
    private SpeedConverter _converter = new SpeedConverter();

    [TestMethod]
    public void ConvertMetersTest()
    {
        // Arrange
        var value = "5";
        var parameter = "meters";
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = _converter.Convert(value, typeof(string), parameter, culture);

        // Assert
        Assert.AreEqual("18.0km/h", result);
    }

    [TestMethod]
    public void ConvertFeetTest()
    {
        // Arrange
        var value = "5";
        var parameter = "feet";
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = _converter.Convert(value, typeof(string), parameter, culture);

        // Assert
        Assert.AreEqual("11.2mph", result);
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void ConvertBackTest()
    {
        Assert.Throws<NotImplementedException>(() => new SpeedConverter().ConvertBack("123", typeof(double), null, null));
    }
}
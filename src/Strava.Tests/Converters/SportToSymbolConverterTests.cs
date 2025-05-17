using System.Windows.Media;
using Tudormobile.Strava.UI.Converters;
namespace Strava.Tests.Converters;

[TestClass]
public class SportToSymbolConverterTests
{
    [TestMethod]
    public void ConvertTest()
    {
        // Arrange
        var sport = "AlpineSki";
        var converter = new SportToSymbolConverter();

        // Act
        var actual = converter.Convert(sport, typeof(StreamGeometry), null, null);

        // Assert
        Assert.IsInstanceOfType<StreamGeometry>(actual);
    }
}
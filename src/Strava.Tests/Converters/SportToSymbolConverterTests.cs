using Tudormobile.Strava.UI.Converters;
namespace Strava.Tests.Converters;

[TestClass]
public class SportToSymbolConverterTests
{
    [TestMethod]
    public void ConvertTest()
    {
        var sport = "AlpineSki";
        var converter = new SportToSymbolConverter();
        var actual = converter.Convert(sport, null, null, null);

        Assert.Fail();
    }
}
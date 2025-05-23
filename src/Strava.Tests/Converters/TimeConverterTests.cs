using System.Diagnostics.CodeAnalysis;
using Tudormobile.Strava.UI.Converters;

namespace Strava.Tests.Converters;

[TestClass]
public class TimeConverterTests
{
    [TestMethod]
    public void ConvertTest()
    {
        // Arrange
        var timeConverter = new TimeConverter();
        var input = new TimeSpan(1, 23, 45); // 1 hour, 23 minutes, 45 seconds
        var expected = "1h23m";

        // Act
        var result = timeConverter.Convert(input, typeof(string), null, null);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void ConvertBackTest()
    {
        Assert.Throws<NotImplementedException>(() =>
        {
            var timeConverter = new TimeConverter();
            timeConverter.ConvertBack(null, typeof(TimeSpan), null, null);
        });
    }
}
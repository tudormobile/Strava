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

    [TestMethod]
    public void ConvertBackTest()
    {
        var timeConverter = new TimeConverter();
        Assert.Throws<NotImplementedException>(() =>
        {
            timeConverter.ConvertBack(null, typeof(TimeSpan), null, null);
        });
    }
}
﻿using System.Diagnostics.CodeAnalysis;
using Tudormobile.Strava.UI.Converters;
namespace Strava.Tests.Converters;

[TestClass]
public class DistanceConverterTests
{
    [TestMethod]
    public void ConvertTest()
    {
        // Arrange
        var converter = new DistanceConverter();

        // Test: meters to kilometers
        double meters = 1523.0;
        var result = converter.Convert(meters, typeof(string), "meters", null);
        Assert.AreEqual("1.5km", result);

        // Test: meters to feet
        result = converter.Convert(meters, typeof(string), "feet", null);
        Assert.AreEqual("4996.7ft", result);

        // Test: meters to meters
        meters = 153.0;
        result = converter.Convert(meters, typeof(string), "meters", null);
        Assert.AreEqual("153.0m", result);

        // Test: meters to miles
        meters = 2500.0;
        result = converter.Convert(meters, typeof(string), "feet", null);
        Assert.AreEqual("1.6mi", result);

    }

    [TestMethod, ExcludeFromCodeCoverage, ExpectedException(typeof(NotImplementedException))]
    public void ConvertBackTest()
    {
        var converter = new DistanceConverter();
        var actual = converter.ConvertBack("1.50 km", typeof(double), null, null);
    }
}
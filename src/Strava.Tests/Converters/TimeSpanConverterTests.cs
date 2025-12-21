using System.Text.Json;
using Tudormobile.Strava.Converters;

namespace Strava.Tests.Converters;

[TestClass]
public class TimeSpanConverterTests
{
    private readonly JsonSerializerOptions _options;

    public TimeSpanConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new TimeSpanConverter());
        _options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }

    [TestMethod]
    public void Read_WithValidPositiveSeconds_ReturnsTimeSpan()
    {
        var json = "3600";
        var result = JsonSerializer.Deserialize<TimeSpan>(json, _options);
        Assert.AreEqual(TimeSpan.FromHours(1), result);
    }

    [TestMethod]
    public void Read_WithZeroSeconds_ReturnsZeroTimeSpan()
    {
        var json = "0";
        var result = JsonSerializer.Deserialize<TimeSpan>(json, _options);
        Assert.AreEqual(TimeSpan.Zero, result);
    }

    [TestMethod]
    public void Read_WithNegativeSeconds_ReturnsNegativeTimeSpan()
    {
        var json = "-300";
        var result = JsonSerializer.Deserialize<TimeSpan>(json, _options);
        Assert.AreEqual(TimeSpan.FromMinutes(-5), result);
    }

    [TestMethod]
    public void Read_WithSmallValue_ReturnsCorrectTimeSpan()
    {
        var json = "10";
        var result = JsonSerializer.Deserialize<TimeSpan>(json, _options);
        Assert.AreEqual(TimeSpan.FromSeconds(10), result);
    }

    [TestMethod]
    public void Read_WithLargeValue_ReturnsCorrectTimeSpan()
    {
        var json = "86400";
        var result = JsonSerializer.Deserialize<TimeSpan>(json, _options);
        Assert.AreEqual(TimeSpan.FromDays(1), result);
    }

    [TestMethod]
    public void Read_WithStringToken_ThrowsJsonException()
    {
        var json = "\"3600\"";
        Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<TimeSpan>(json, _options));
    }

    [TestMethod]
    public void Read_WithBooleanToken_ThrowsJsonException()
    {
        var json = "true";
        Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<TimeSpan>(json, _options));
    }

    [TestMethod]
    public void Read_WithNullToken_ThrowsJsonException()
    {
        var json = "null";
        Assert.ThrowsExactly<JsonException>(() => JsonSerializer.Deserialize<TimeSpan>(json, _options));
    }

    [TestMethod]
    public void Write_WithPositiveTimeSpan_WritesSeconds()
    {
        var timeSpan = TimeSpan.FromMinutes(5);
        var json = JsonSerializer.Serialize(timeSpan, _options);
        Assert.AreEqual("300", json);
    }

    [TestMethod]
    public void Write_WithZeroTimeSpan_WritesZero()
    {
        var timeSpan = TimeSpan.Zero;
        var json = JsonSerializer.Serialize(timeSpan, _options);
        Assert.AreEqual("0", json);
    }

    [TestMethod]
    public void Write_WithNegativeTimeSpan_WritesNegativeSeconds()
    {
        var timeSpan = TimeSpan.FromMinutes(-10);
        var json = JsonSerializer.Serialize(timeSpan, _options);
        Assert.AreEqual("-600", json);
    }

    [TestMethod]
    public void Write_WithOneHour_Writes3600()
    {
        var timeSpan = TimeSpan.FromHours(1);
        var json = JsonSerializer.Serialize(timeSpan, _options);
        Assert.AreEqual("3600", json);
    }

    [TestMethod]
    public void Write_WithOneDay_Writes86400()
    {
        var timeSpan = TimeSpan.FromDays(1);
        var json = JsonSerializer.Serialize(timeSpan, _options);
        Assert.AreEqual("86400", json);
    }

    [TestMethod]
    public void Write_WithFractionalSeconds_TruncatesToInteger()
    {
        var timeSpan = TimeSpan.FromSeconds(10.7);
        var json = JsonSerializer.Serialize(timeSpan, _options);
        Assert.AreEqual("10", json);
    }

    [TestMethod]
    public void RoundTrip_WithVariousValues_MaintainsIntegrity()
    {
        var testValues = new[] { 0, 1, 60, 300, 3600, 86400, -300 };

        foreach (var seconds in testValues)
        {
            var original = TimeSpan.FromSeconds(seconds);
            var json = JsonSerializer.Serialize(original, _options);
            var deserialized = JsonSerializer.Deserialize<TimeSpan>(json, _options);
            Assert.AreEqual(original, deserialized, $"Failed for {seconds} seconds");
        }
    }

    [TestMethod]
    public void DeserializeObject_WithTimeSpanProperty_ConvertsCorrectly()
    {
        var json = "{\"duration\":120}";
        var result = JsonSerializer.Deserialize<TestObject>(json, _options);
        Assert.IsNotNull(result);
        Assert.AreEqual(TimeSpan.FromMinutes(2), result.Duration);
    }

    [TestMethod]
    public void SerializeObject_WithTimeSpanProperty_ConvertsCorrectly()
    {
        var obj = new TestObject { Duration = TimeSpan.FromMinutes(3) };
        var json = JsonSerializer.Serialize(obj, _options);
        Assert.Contains("\"duration\":180", json);
    }

    private class TestObject
    {
        public TimeSpan Duration { get; set; }
    }
}

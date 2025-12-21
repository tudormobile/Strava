using System.Text.Json;
using Tudormobile.Strava.Converters;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Converters;

[TestClass]
public class SportTypesConverterTests
{
    private readonly JsonSerializerOptions _options;

    public SportTypesConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new SportTypesConverter());
        _options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    }

    [TestMethod]
    public void Read_WithNumberToken_ReturnsSportType()
    {
        var json = "28"; // Run is at index 28
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Run, result);
    }

    [TestMethod]
    public void Read_WithZeroNumber_ReturnsUnknown()
    {
        var json = "0";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Unknown, result);
    }

    [TestMethod]
    public void Read_WithValidStringToken_ReturnsSportType()
    {
        var json = "\"Run\"";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Run, result);
    }

    [TestMethod]
    public void Read_WithValidStringTokenLowerCase_ReturnsSportType()
    {
        var json = "\"run\"";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Run, result);
    }

    [TestMethod]
    public void Read_WithValidStringTokenMixedCase_ReturnsSportType()
    {
        var json = "\"mOuNtAiNbIkErIdE\"";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.MountainBikeRide, result);
    }

    [TestMethod]
    public void Read_WithInvalidStringToken_ReturnsUnknown()
    {
        var json = "\"InvalidSportType\"";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Unknown, result);
    }

    [TestMethod]
    public void Read_WithEmptyStringToken_ReturnsUnknown()
    {
        var json = "\"\"";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Unknown, result);
    }

    [TestMethod]
    public void Read_WithInvalidNumberToken_ReturnsUnknown()
    {
        var json = "99999";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Unknown, result);
    }

    [TestMethod]
    public void Read_WithNegativeNumber_ReturnsUnknown()
    {
        var json = "-1";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Unknown, result);
    }

    [TestMethod]
    public void Read_WithBooleanToken_ReturnsUnknown()
    {
        var json = "true";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Unknown, result);
    }

    [TestMethod]
    public void Read_WithNullToken_ReturnsUnknown()
    {
        var json = "null";
        var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
        Assert.AreEqual(SportTypes.Unknown, result);
    }

    [TestMethod]
    public void Write_WithValidSportType_WritesNumber()
    {
        var sportType = SportTypes.Ride;
        var json = JsonSerializer.Serialize(sportType, _options);
        Assert.AreEqual("24", json);
    }

    [TestMethod]
    public void Write_WithUnknown_WritesZero()
    {
        var sportType = SportTypes.Unknown;
        var json = JsonSerializer.Serialize(sportType, _options);
        Assert.AreEqual("0", json);
    }

    [TestMethod]
    public void Write_WithRun_WritesCorrectNumber()
    {
        var sportType = SportTypes.Run;
        var json = JsonSerializer.Serialize(sportType, _options);
        Assert.AreEqual("28", json);
    }

    [TestMethod]
    public void Write_WithSwim_WritesCorrectNumber()
    {
        var sportType = SportTypes.Swim;
        var json = JsonSerializer.Serialize(sportType, _options);
        Assert.AreEqual("38", json);
    }

    [TestMethod]
    public void RoundTrip_WithNumberSerialization_MaintainsValue()
    {
        var testValues = new[]
        {
            SportTypes.Unknown,
            SportTypes.Run,
            SportTypes.Ride,
            SportTypes.Swim,
            SportTypes.MountainBikeRide,
            SportTypes.Yoga,
            SportTypes.Walk
        };

        foreach (var original in testValues)
        {
            var json = JsonSerializer.Serialize(original, _options);
            var deserialized = JsonSerializer.Deserialize<SportTypes>(json, _options);
            Assert.AreEqual(original, deserialized, $"Failed for {original}");
        }
    }

    [TestMethod]
    public void RoundTrip_WithStringDeserialization_Works()
    {
        var testCases = new Dictionary<string, SportTypes>
        {
            { "\"Run\"", SportTypes.Run },
            { "\"Ride\"", SportTypes.Ride },
            { "\"Swim\"", SportTypes.Swim },
            { "\"Walk\"", SportTypes.Walk },
            { "\"MountainBikeRide\"", SportTypes.MountainBikeRide },
            { "\"Yoga\"", SportTypes.Yoga }
        };

        foreach (var testCase in testCases)
        {
            var deserialized = JsonSerializer.Deserialize<SportTypes>(testCase.Key, _options);
            Assert.AreEqual(testCase.Value, deserialized, $"Failed for {testCase.Key}");
        }
    }

    [TestMethod]
    public void Read_WithAllValidSportTypes_ReturnsCorrectValue()
    {
        var testCases = new[]
        {
            ("\"AlpineSki\"", SportTypes.AlpineSki),
            ("\"BackcountrySki\"", SportTypes.BackcountrySki),
            ("\"Badminton\"", SportTypes.Badminton),
            ("\"Canoeing\"", SportTypes.Canoeing),
            ("\"Crossfit\"", SportTypes.Crossfit),
            ("\"Cycling\"", SportTypes.Cycling),
            ("\"EBikeRide\"", SportTypes.EBikeRide),
            ("\"Elliptical\"", SportTypes.Elliptical),
            ("\"Golf\"", SportTypes.Golf),
            ("\"Hike\"", SportTypes.Hike),
            ("\"Kayaking\"", SportTypes.Kayaking),
            ("\"Pilates\"", SportTypes.Pilates),
            ("\"Rowing\"", SportTypes.Rowing),
            ("\"Skateboard\"", SportTypes.Skateboard),
            ("\"Snowboard\"", SportTypes.Snowboard),
            ("\"Soccer\"", SportTypes.Soccer),
            ("\"Surfing\"", SportTypes.Surfing),
            ("\"Tennis\"", SportTypes.Tennis),
            ("\"TrailRun\"", SportTypes.TrailRun),
            ("\"VirtualRide\"", SportTypes.VirtualRide),
            ("\"VirtualRun\"", SportTypes.VirtualRun),
            ("\"WeightTraining\"", SportTypes.WeightTraining),
            ("\"Wheelchair\"", SportTypes.Wheelchair),
            ("\"Windsurf\"", SportTypes.Windsurf),
            ("\"Workout\"", SportTypes.Workout)
        };

        foreach (var (json, expected) in testCases)
        {
            var result = JsonSerializer.Deserialize<SportTypes>(json, _options);
            Assert.AreEqual(expected, result, $"Failed for {json}");
        }
    }

    private class TestObject
    {
        public SportTypes Type { get; set; }
    }
}

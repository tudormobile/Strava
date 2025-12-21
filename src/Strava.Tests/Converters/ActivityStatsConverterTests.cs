using System.Text.Json;
using Tudormobile.Strava;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Converters;

[TestClass]
public class ActivityStatsConverterTests
{
    [TestMethod]
    public void Read_WithValidActivityStats_DeserializesCorrectly()
    {
        // Arrange
        var json = @"{
            ""biggest_ride_distance"": 100000.5,
            ""biggest_climb_elevation_gain"": 1500.25,
            ""recent_ride_totals"": {
                ""distance"": 50000.0,
                ""achievement_count"": 5,
                ""count"": 10,
                ""elapsed_time"": 3600,
                ""elevation_gain"": 500.0,
                ""moving_time"": 3500
            },
            ""recent_run_totals"": {
                ""distance"": 25000.0,
                ""achievement_count"": 3,
                ""count"": 8,
                ""elapsed_time"": 7200,
                ""elevation_gain"": 200.0,
                ""moving_time"": 7000
            }
        }";

        // Act
        var result = JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(100000.5f, result.BiggestRideDistance);
        Assert.AreEqual(1500.25f, result.BiggestClimbElevationGain);
        Assert.IsNotNull(result.RecentRideTotals);
        Assert.AreEqual(50000.0f, result.RecentRideTotals.Distance);
        Assert.AreEqual(5, result.RecentRideTotals.AchievementCount);
        Assert.IsNotNull(result.RecentRunTotals);
        Assert.AreEqual(25000.0f, result.RecentRunTotals.Distance);
    }

    [TestMethod]
    public void Read_WithEmptyStringActivityTotals_DeserializesAsNull()
    {
        // Arrange
        var json = @"{
            ""biggest_ride_distance"": 100000.0,
            ""biggest_climb_elevation_gain"": 1500.0,
            ""recent_ride_totals"": """",
            ""ytd_run_totals"": """",
            ""all_swim_totals"": """"
        }";

        // Act
        var result = JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(100000.0f, result.BiggestRideDistance);
        Assert.IsNull(result.RecentRideTotals);
        Assert.IsNull(result.YtdRunTotals);
        Assert.IsNull(result.AllSwimTotals);
    }

    [TestMethod]
    public void Read_WithNullActivityTotals_DeserializesAsNull()
    {
        // Arrange
        var json = @"{
            ""biggest_ride_distance"": 50000.0,
            ""biggest_climb_elevation_gain"": 800.0,
            ""recent_ride_totals"": null,
            ""ytd_swim_totals"": null
        }";

        // Act
        var result = JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNull(result.RecentRideTotals);
        Assert.IsNull(result.YtdSwimTotals);
    }

    [TestMethod]
    public void Read_WithAllActivityTotalTypes_DeserializesCorrectly()
    {
        // Arrange
        var json = @"{
            ""biggest_ride_distance"": 100000.0,
            ""biggest_climb_elevation_gain"": 1500.0,
            ""recent_ride_totals"": {""distance"": 1000.0, ""count"": 5, ""achievement_count"": 1, ""elapsed_time"": 3600, ""elevation_gain"": 100.0, ""moving_time"": 3500},
            ""recent_run_totals"": {""distance"": 2000.0, ""count"": 10, ""achievement_count"": 2, ""elapsed_time"": 7200, ""elevation_gain"": 200.0, ""moving_time"": 7000},
            ""recent_swim_totals"": {""distance"": 3000.0, ""count"": 15, ""achievement_count"": 3, ""elapsed_time"": 1800, ""elevation_gain"": 0.0, ""moving_time"": 1700},
            ""ytd_ride_totals"": {""distance"": 4000.0, ""count"": 20, ""achievement_count"": 4, ""elapsed_time"": 14400, ""elevation_gain"": 400.0, ""moving_time"": 14000},
            ""ytd_run_totals"": {""distance"": 5000.0, ""count"": 25, ""achievement_count"": 5, ""elapsed_time"": 18000, ""elevation_gain"": 500.0, ""moving_time"": 17500},
            ""ytd_swim_totals"": {""distance"": 6000.0, ""count"": 30, ""achievement_count"": 6, ""elapsed_time"": 3600, ""elevation_gain"": 0.0, ""moving_time"": 3500},
            ""all_ride_totals"": {""distance"": 7000.0, ""count"": 35, ""achievement_count"": 7, ""elapsed_time"": 21600, ""elevation_gain"": 700.0, ""moving_time"": 21000},
            ""all_run_totals"": {""distance"": 8000.0, ""count"": 40, ""achievement_count"": 8, ""elapsed_time"": 25200, ""elevation_gain"": 800.0, ""moving_time"": 24500},
            ""all_swim_totals"": {""distance"": 9000.0, ""count"": 45, ""achievement_count"": 9, ""elapsed_time"": 5400, ""elevation_gain"": 0.0, ""moving_time"": 5200}
        }";

        // Act
        var result = JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.RecentRideTotals);
        Assert.IsNotNull(result.RecentRunTotals);
        Assert.IsNotNull(result.RecentSwimTotals);
        Assert.IsNotNull(result.YtdRideTotals);
        Assert.IsNotNull(result.YtdRunTotals);
        Assert.IsNotNull(result.YtdSwimTotals);
        Assert.IsNotNull(result.AllRideTotals);
        Assert.IsNotNull(result.AllRunTotals);
        Assert.IsNotNull(result.AllSwimTotals);
        Assert.AreEqual(1000.0f, result.RecentRideTotals.Distance);
        Assert.AreEqual(9000.0f, result.AllSwimTotals.Distance);
    }

    [TestMethod]
    public void Read_WithUnknownProperties_IgnoresThem()
    {
        // Arrange
        var json = @"{
            ""biggest_ride_distance"": 100000.0,
            ""biggest_climb_elevation_gain"": 1500.0,
            ""unknown_property"": ""some_value"",
            ""another_unknown"": 12345
        }";

        // Act
        var result = JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(100000.0f, result.BiggestRideDistance);
        Assert.AreEqual(1500.0f, result.BiggestClimbElevationGain);
    }

    [TestMethod]
    public void Read_WithInvalidNonEmptyStringForActivityTotal_ThrowsJsonException()
    {
        // Arrange
        var json = @"{
            ""biggest_ride_distance"": 100000.0,
            ""biggest_climb_elevation_gain"": 1500.0,
            ""recent_ride_totals"": ""invalid_string""
        }";

        // Act & Assert
        var ex = Assert.ThrowsExactly<JsonException>(() =>
            JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options));

        Assert.Contains("Expected object or empty string", ex.Message);
    }

    [TestMethod]
    public void Read_WithArrayForActivityTotal_ThrowsJsonException()
    {
        // Arrange
        var json = @"{
            ""biggest_ride_distance"": 100000.0,
            ""biggest_climb_elevation_gain"": 1500.0,
            ""recent_ride_totals"": [1, 2, 3]
        }";

        // Act & Assert
        var ex = Assert.ThrowsExactly<JsonException>(() =>
            JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options));

        Assert.Contains("Expected object, null, or empty string for ActivityTotal", ex.Message);
    }

    [TestMethod]
    public void Read_WithBooleanForActivityTotal_ThrowsJsonException()
    {
        // Arrange
        var json = @"{
            ""biggest_ride_distance"": 100000.0,
            ""biggest_climb_elevation_gain"": 1500.0,
            ""ytd_run_totals"": true
        }";

        // Act & Assert
        var ex = Assert.ThrowsExactly<JsonException>(() =>
            JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options));

        Assert.Contains("Expected object, null, or empty string for ActivityTotal", ex.Message);
    }

    [TestMethod]
    public void Read_WithNumberForActivityTotal_ThrowsJsonException()
    {
        // Arrange
        var json = @"{
            ""biggest_ride_distance"": 100000.0,
            ""biggest_climb_elevation_gain"": 1500.0,
            ""all_swim_totals"": 12345
        }";

        // Act & Assert
        var ex = Assert.ThrowsExactly<JsonException>(() =>
            JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options));

        Assert.Contains("Expected object, null, or empty string for ActivityTotal", ex.Message);
    }

    [TestMethod]
    public void Read_WithInvalidTokenType_ThrowsJsonException()
    {
        // Arrange
        var json = @"[]";

        // Act & Assert
        var ex = Assert.ThrowsExactly<JsonException>(() =>
            JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options));

        Assert.Contains("Expected StartObject token", ex.Message);
    }

    [TestMethod]
    public void Read_WithStringInsteadOfObject_ThrowsJsonException()
    {
        // Arrange
        var json = @"""not an object""";

        // Act & Assert
        var ex = Assert.ThrowsExactly<JsonException>(() =>
            JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options));

        Assert.Contains("Expected StartObject token", ex.Message);
    }

    [TestMethod]
    public void Read_WithNumberInsteadOfObject_ThrowsJsonException()
    {
        // Arrange
        var json = @"12345";

        // Act & Assert
        var ex = Assert.ThrowsExactly<JsonException>(() =>
            JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options));

        Assert.Contains("Expected StartObject token", ex.Message);
    }

    [TestMethod]
    public void Write_WithNullActivityTotals_WritesEmptyStrings()
    {
        // Arrange
        var stats = new ActivityStats
        {
            BiggestRideDistance = 100000.0f,
            BiggestClimbElevationGain = 1500.0f,
            RecentRideTotals = null,
            YtdRunTotals = null,
            AllSwimTotals = null
        };

        // Act
        var json = JsonSerializer.Serialize(stats, StravaSerializer.Options);

        // Assert
        Assert.Contains("\"recent_ride_totals\":\"\"", json);
        Assert.Contains("\"ytd_run_totals\":\"\"", json);
        Assert.Contains("\"all_swim_totals\":\"\"", json);
        Assert.Contains("\"biggest_ride_distance\":100000", json);
    }

    [TestMethod]
    public void Write_WithValidActivityTotals_SerializesObjects()
    {
        // Arrange
        var stats = new ActivityStats
        {
            BiggestRideDistance = 50000.0f,
            BiggestClimbElevationGain = 800.0f,
            RecentRideTotals = new ActivityTotal
            {
                Distance = 25000.0f,
                Count = 10,
                AchievementCount = 5,
                ElapsedTime = 3600,
                ElevationGain = 500.0f,
                MovingTime = 3500
            }
        };

        // Act
        var json = JsonSerializer.Serialize(stats, StravaSerializer.Options);

        // Assert
        Assert.Contains("\"distance\":25000", json);
        Assert.Contains("\"count\":10", json);
        Assert.Contains("\"achievement_count\":5", json);
    }

    [TestMethod]
    public void RoundTrip_WithMixedNullAndValidTotals_PreservesData()
    {
        // Arrange
        var original = new ActivityStats
        {
            BiggestRideDistance = 100000.5f,
            BiggestClimbElevationGain = 1500.25f,
            RecentRideTotals = new ActivityTotal { Distance = 50000.0f, Count = 10 },
            RecentRunTotals = null,
            YtdRideTotals = new ActivityTotal { Distance = 75000.0f, Count = 25 }
        };

        // Act
        var json = JsonSerializer.Serialize(original, StravaSerializer.Options);
        var result = JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(original.BiggestRideDistance, result.BiggestRideDistance);
        Assert.AreEqual(original.BiggestClimbElevationGain, result.BiggestClimbElevationGain);
        Assert.IsNotNull(result.RecentRideTotals);
        Assert.AreEqual(50000.0f, result.RecentRideTotals.Distance);
        Assert.IsNull(result.RecentRunTotals);
        Assert.IsNotNull(result.YtdRideTotals);
        Assert.AreEqual(75000.0f, result.YtdRideTotals.Distance);
    }

    [TestMethod]
    public void Read_WithIncompleteJson_ThrowsJsonException()
    {
        // Arrange
#pragma warning disable JSON001 // Invalid JSON pattern
        var json = @"{
            ""biggest_ride_distance"": 100000.0,
            ""biggest_climb_elevation_gain"": 1500.0";
#pragma warning restore JSON001 // Invalid JSON pattern
        // Missing closing brace

        // Act & Assert
        var ex = Assert.ThrowsExactly<JsonException>(() =>
            JsonSerializer.Deserialize<ActivityStats>(json, StravaSerializer.Options));

        Assert.Contains("Expected a delimiter", ex.Message);
    }

    [TestMethod]
    public void Write_WithAllNullActivityTotals_WritesAllEmptyStrings()
    {
        // Arrange
        var stats = new ActivityStats
        {
            BiggestRideDistance = 0.0f,
            BiggestClimbElevationGain = 0.0f
        };

        // Act
        var json = JsonSerializer.Serialize(stats, StravaSerializer.Options);

        // Assert
        Assert.Contains("\"recent_ride_totals\":\"\"", json);
        Assert.Contains("\"recent_run_totals\":\"\"", json);
        Assert.Contains("\"recent_swim_totals\":\"\"", json);
        Assert.Contains("\"ytd_ride_totals\":\"\"", json);
        Assert.Contains("\"ytd_run_totals\":\"\"", json);
        Assert.Contains("\"ytd_swim_totals\":\"\"", json);
        Assert.Contains("\"all_ride_totals\":\"\"", json);
        Assert.Contains("\"all_run_totals\":\"\"", json);
        Assert.Contains("\"all_swim_totals\":\"\"", json);
    }

    [TestMethod]
    public void Write_WithAllValidActivityTotals_SerializesAllObjects()
    {
        // Arrange
        var stats = new ActivityStats
        {
            BiggestRideDistance = 100000.0f,
            BiggestClimbElevationGain = 1500.0f,
            RecentRideTotals = new ActivityTotal { Distance = 1000.0f, Count = 5 },
            RecentRunTotals = new ActivityTotal { Distance = 2000.0f, Count = 10 },
            RecentSwimTotals = new ActivityTotal { Distance = 3000.0f, Count = 15 },
            YtdRideTotals = new ActivityTotal { Distance = 4000.0f, Count = 20 },
            YtdRunTotals = new ActivityTotal { Distance = 5000.0f, Count = 25 },
            YtdSwimTotals = new ActivityTotal { Distance = 6000.0f, Count = 30 },
            AllRideTotals = new ActivityTotal { Distance = 7000.0f, Count = 35 },
            AllRunTotals = new ActivityTotal { Distance = 8000.0f, Count = 40 },
            AllSwimTotals = new ActivityTotal { Distance = 9000.0f, Count = 45 }
        };

        // Act
        var json = JsonSerializer.Serialize(stats, StravaSerializer.Options);

        // Assert
        Assert.Contains("\"recent_ride_totals\":{", json);
        Assert.Contains("\"recent_run_totals\":{", json);
        Assert.Contains("\"recent_swim_totals\":{", json);
        Assert.Contains("\"ytd_ride_totals\":{", json);
        Assert.Contains("\"ytd_run_totals\":{", json);
        Assert.Contains("\"ytd_swim_totals\":{", json);
        Assert.Contains("\"all_ride_totals\":{", json);
        Assert.Contains("\"all_run_totals\":{", json);
        Assert.Contains("\"all_swim_totals\":{", json);
    }

}



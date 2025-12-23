using Tudormobile.Strava;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class SegmentEffortTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var segmentEffort = new SegmentEffort();

        // Assert
        Assert.AreEqual(0L, segmentEffort.Id);
        Assert.AreEqual(ResourceStates.Unknown, segmentEffort.ResourceState);
        Assert.AreEqual(string.Empty, segmentEffort.Name);
        Assert.IsNull(segmentEffort.Activity);
        Assert.IsNotNull(segmentEffort.Athlete);
        Assert.AreEqual(TimeSpan.Zero, segmentEffort.ElapsedTime);
        Assert.AreEqual(TimeSpan.Zero, segmentEffort.MovingTime);
        Assert.AreEqual(default, segmentEffort.StartDate);
        Assert.AreEqual(default, segmentEffort.StartDateLocal);
        Assert.AreEqual(0.0, segmentEffort.Distance);
        Assert.AreEqual(0, segmentEffort.StartIndex);
        Assert.AreEqual(0, segmentEffort.EndIndex);
        Assert.IsNull(segmentEffort.Segment);
    }

    [TestMethod]
    public void PropertyAssignment_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var startDate = DateTime.UtcNow;
        var startDateLocal = DateTime.Now;
        var activity = new SummaryActivity { Id = 123456 };
        var athlete = new AthleteId { Id = 789 };
        var segment = new Segment { Id = 111, Name = "Test Segment" };

        var segmentEffort = new SegmentEffort
        {
            Id = 987654321,
            ResourceState = ResourceStates.Summary,
            Name = "Hawk Hill",
            Activity = activity,
            Athlete = athlete,
            ElapsedTime = TimeSpan.FromSeconds(360),
            MovingTime = TimeSpan.FromSeconds(350),
            StartDate = startDate,
            StartDateLocal = startDateLocal,
            Distance = 1254.5,
            StartIndex = 10,
            EndIndex = 150,
            Segment = segment
        };

        // Assert
        Assert.AreEqual(987654321L, segmentEffort.Id);
        Assert.AreEqual(ResourceStates.Summary, segmentEffort.ResourceState);
        Assert.AreEqual("Hawk Hill", segmentEffort.Name);
        Assert.AreEqual(activity, segmentEffort.Activity);
        Assert.AreEqual(athlete, segmentEffort.Athlete);
        Assert.AreEqual(TimeSpan.FromSeconds(360), segmentEffort.ElapsedTime);
        Assert.AreEqual(TimeSpan.FromSeconds(350), segmentEffort.MovingTime);
        Assert.AreEqual(startDate, segmentEffort.StartDate);
        Assert.AreEqual(startDateLocal, segmentEffort.StartDateLocal);
        Assert.AreEqual(1254.5, segmentEffort.Distance);
        Assert.AreEqual(10, segmentEffort.StartIndex);
        Assert.AreEqual(150, segmentEffort.EndIndex);
        Assert.AreEqual(segment, segmentEffort.Segment);
    }

    [TestMethod]
    public void Deserialize_ValidJson_ShouldReturnSegmentEffort()
    {
        // Arrange
        var json = @"{
            ""id"": 12345678987654321,
            ""resource_state"": 2,
            ""name"": ""Hawk Hill"",
            ""elapsed_time"": 360,
            ""moving_time"": 350,
            ""start_date"": ""2018-05-02T12:15:09Z"",
            ""start_date_local"": ""2018-05-02T05:15:09Z"",
            ""distance"": 1254.5,
            ""start_index"": 10,
            ""end_index"": 150
        }";

        // Act
        var result = StravaSerializer.TryDeserialize<SegmentEffort>(json, out var segmentEffort);

        // Assert
        Assert.IsTrue(result);
        Assert.IsNotNull(segmentEffort);
        Assert.AreEqual(12345678987654321L, segmentEffort.Id);
        Assert.AreEqual(ResourceStates.Summary, segmentEffort.ResourceState);
        Assert.AreEqual("Hawk Hill", segmentEffort.Name);
        Assert.AreEqual(TimeSpan.FromSeconds(360), segmentEffort.ElapsedTime);
        Assert.AreEqual(TimeSpan.FromSeconds(350), segmentEffort.MovingTime);
        Assert.AreEqual(new DateTime(2018, 5, 2, 12, 15, 9, DateTimeKind.Utc), segmentEffort.StartDate);
        Assert.AreEqual(1254.5, segmentEffort.Distance);
        Assert.AreEqual(10, segmentEffort.StartIndex);
        Assert.AreEqual(150, segmentEffort.EndIndex);
    }

    [TestMethod]
    public void Deserialize_WithNestedObjects_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = @"{
            ""id"": 12345678987654321,
            ""resource_state"": 2,
            ""name"": ""Hawk Hill"",
            ""activity"": {
                ""id"": 123456789
            },
            ""athlete"": {
                ""id"": 987654
            },
            ""segment"": {
                ""id"": 111222,
                ""name"": ""Test Segment"",
                ""resource_state"": 2
            },
            ""elapsed_time"": 360,
            ""moving_time"": 350,
            ""start_date"": ""2018-05-02T12:15:09Z"",
            ""distance"": 1254.5,
            ""start_index"": 10,
            ""end_index"": 150
        }";

        // Act
        var result = StravaSerializer.TryDeserialize<SegmentEffort>(json, out var segmentEffort);

        // Assert
        Assert.IsTrue(result);
        Assert.IsNotNull(segmentEffort);
        Assert.IsNotNull(segmentEffort.Activity);
        Assert.AreEqual(123456789L, segmentEffort.Activity.Id);
        Assert.IsNotNull(segmentEffort.Athlete);
        Assert.AreEqual(987654L, segmentEffort.Athlete.Id);
        Assert.IsNotNull(segmentEffort.Segment);
        Assert.AreEqual(111222L, segmentEffort.Segment.Id);
        Assert.AreEqual("Test Segment", segmentEffort.Segment.Name);
    }

    [TestMethod]
    public void Deserialize_WithMissingProperties_ShouldUseDefaults()
    {
        // Arrange
        var json = @"{
            ""id"": 12345678987654321
        }";

        // Act
        var result = StravaSerializer.TryDeserialize<SegmentEffort>(json, out var segmentEffort);

        // Assert
        Assert.IsTrue(result);
        Assert.IsNotNull(segmentEffort);
        Assert.AreEqual(12345678987654321L, segmentEffort.Id);
        Assert.AreEqual(ResourceStates.Unknown, segmentEffort.ResourceState);
        Assert.AreEqual(string.Empty, segmentEffort.Name);
        Assert.AreEqual(TimeSpan.Zero, segmentEffort.ElapsedTime);
        Assert.AreEqual(0.0, segmentEffort.Distance);
    }

    [TestMethod]
    public void Serialize_ValidSegmentEffort_ShouldProduceCorrectJson()
    {
        // Arrange
        var segmentEffort = new SegmentEffort
        {
            Id = 12345678987654321,
            ResourceState = ResourceStates.Summary,
            Name = "Hawk Hill",
            ElapsedTime = TimeSpan.FromSeconds(360),
            MovingTime = TimeSpan.FromSeconds(350),
            Distance = 1254.5,
            StartIndex = 10,
            EndIndex = 150
        };

        // Act
        var stream = new MemoryStream();
        StravaSerializer.SerializeAsync(stream, segmentEffort, CancellationToken.None).Wait(TestContext.CancellationToken);
        stream.Position = 0;
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        // Assert
        Assert.Contains("\"id\":12345678987654321", json);
        Assert.Contains("\"resource_state\":2", json);
        Assert.Contains("\"name\":\"Hawk Hill\"", json);
        Assert.Contains("\"elapsed_time\":360", json);
        Assert.Contains("\"moving_time\":350", json);
        Assert.Contains("\"distance\":1254.5", json);
        Assert.Contains("\"start_index\":10", json);
        Assert.Contains("\"end_index\":150", json);
    }

    [TestMethod]
    public void RoundTrip_ValidSegmentEffort_ShouldPreserveValues()
    {
        // Arrange
        var original = new SegmentEffort
        {
            Id = 12345678987654321,
            ResourceState = ResourceStates.Detail,
            Name = "Hawk Hill Climb",
            ElapsedTime = TimeSpan.FromSeconds(420),
            MovingTime = TimeSpan.FromSeconds(410),
            StartDate = new DateTime(2018, 5, 2, 12, 15, 9, DateTimeKind.Utc),
            Distance = 1500.75,
            StartIndex = 15,
            EndIndex = 200
        };

        // Act
        var stream = new MemoryStream();
        StravaSerializer.SerializeAsync(stream, original, CancellationToken.None).Wait(TestContext.CancellationToken);
        stream.Position = 0;
        StravaSerializer.TryDeserialize(stream, out SegmentEffort? result);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(original.Id, result.Id);
        Assert.AreEqual(original.ResourceState, result.ResourceState);
        Assert.AreEqual(original.Name, result.Name);
        Assert.AreEqual(original.ElapsedTime, result.ElapsedTime);
        Assert.AreEqual(original.MovingTime, result.MovingTime);
        Assert.AreEqual(original.StartDate, result.StartDate);
        Assert.AreEqual(original.Distance, result.Distance);
        Assert.AreEqual(original.StartIndex, result.StartIndex);
        Assert.AreEqual(original.EndIndex, result.EndIndex);
    }

    [TestMethod]
    public void Deserialize_InvalidJson_ShouldReturnFalse()
    {
        // Arrange
        var json = "this is not valid json";

        // Act
        var result = StravaSerializer.TryDeserialize<SegmentEffort>(json, out var segmentEffort);

        // Assert
        Assert.IsFalse(result);
        Assert.IsNull(segmentEffort);
    }

    [TestMethod]
    public void Deserialize_EmptyJson_ShouldReturnEmptyObject()
    {
        // Arrange
        var json = @"{}";

        // Act
        var result = StravaSerializer.TryDeserialize<SegmentEffort>(json, out var segmentEffort);

        // Assert
        Assert.IsTrue(result);
        Assert.IsNotNull(segmentEffort);
        Assert.AreEqual(0L, segmentEffort.Id);
    }

    public TestContext TestContext { get; set; }
}

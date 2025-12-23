using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class DetailedSegmentTests
{
    [TestMethod]
    public void Constructor_WithDefaultValues_ShouldInitializeAllProperties()
    {
        // Arrange & Act
        var target = new DetailedSegment();

        // Assert
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
        Assert.AreEqual(ResourceStates.Unknown, target.ResourceState);
        Assert.AreEqual(string.Empty, target.Name);
        Assert.AreEqual(string.Empty, target.ActivityType);
        Assert.AreEqual(0f, target.Distance);
        Assert.AreEqual(0f, target.AverageGrade);
        Assert.AreEqual(0f, target.MaximumGrade);
        Assert.AreEqual(0f, target.ElevationHigh);
        Assert.AreEqual(0f, target.ElevationLow);
        Assert.AreEqual(default, target.StartLatlng);
        Assert.AreEqual(default, target.EndLatLng);
        Assert.AreEqual(0, target.ClimbCategory);
        Assert.IsNull(target.City);
        Assert.IsNull(target.State);
        Assert.IsNull(target.Country);
        Assert.IsFalse(target.Private);
        Assert.IsFalse(target.Hazardous);
        Assert.IsFalse(target.Starred);
        Assert.AreEqual(default, target.CreatedAt);
        Assert.AreEqual(default, target.UpdatedAt);
        Assert.AreEqual(0f, target.TotalElevationGain);
        Assert.IsNull(target.Map);
        Assert.AreEqual(0, target.EffortCount);
        Assert.AreEqual(0, target.AthleteCount);
        Assert.AreEqual(0, target.StarCount);
        Assert.IsNull(target.AthleteSegmentStats);
    }

    [TestMethod]
    public void PropertyAssignment_ShouldSetAllProperties()
    {
        // Arrange
        var createdAt = new DateTime(2009, 09, 21, 20, 29, 41, DateTimeKind.Utc);
        var updatedAt = new DateTime(2018, 02, 15, 09, 04, 18, DateTimeKind.Utc);
        var startLatLng = new LatLng { Latitude = 37.8331119f, Longitude = -122.4834356f };
        var endLatLng = new LatLng { Latitude = 37.8280722f, Longitude = -122.4981393f };
        var map = new DetailedPolylineMap
        {
            Id = "s229781",
            ResourceState = ResourceStates.Detail,
            Polyline = "test_polyline"
        };
        var stats = new SummaryPRSegmentEffort
        {
            PrElapsedTime = TimeSpan.FromSeconds(553),
            PrDate = new DateOnly(1993, 04, 03),
            EffortCount = 2
        };

        // Act
        var target = new DetailedSegment
        {
            Id = 229781,
            ResourceState = ResourceStates.Detail,
            Name = "Hawk Hill",
            ActivityType = "Ride",
            Distance = 2684.82f,
            AverageGrade = 5.7f,
            MaximumGrade = 14.2f,
            ElevationHigh = 245.3f,
            ElevationLow = 92.4f,
            StartLatlng = startLatLng,
            EndLatLng = endLatLng,
            ClimbCategory = 1,
            City = "San Francisco",
            State = "CA",
            Country = "United States",
            Private = false,
            Hazardous = false,
            Starred = false,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
            TotalElevationGain = 155.733f,
            Map = map,
            EffortCount = 309974,
            AthleteCount = 30623,
            StarCount = 2428,
            AthleteSegmentStats = stats
        };

        // Assert
        Assert.AreEqual(229781, target.Id);
        Assert.AreEqual(ResourceStates.Detail, target.ResourceState);
        Assert.AreEqual("Hawk Hill", target.Name);
        Assert.AreEqual("Ride", target.ActivityType);
        Assert.AreEqual(2684.82f, target.Distance, 0.01f);
        Assert.AreEqual(5.7f, target.AverageGrade, 0.01f);
        Assert.AreEqual(14.2f, target.MaximumGrade, 0.01f);
        Assert.AreEqual(245.3f, target.ElevationHigh, 0.01f);
        Assert.AreEqual(92.4f, target.ElevationLow, 0.01f);
        Assert.AreEqual(37.8331119f, target.StartLatlng.Latitude, 0.0001f);
        Assert.AreEqual(-122.4834356f, target.StartLatlng.Longitude, 0.0001f);
        Assert.AreEqual(37.8280722f, target.EndLatLng.Latitude, 0.0001f);
        Assert.AreEqual(-122.4981393f, target.EndLatLng.Longitude, 0.0001f);
        Assert.AreEqual(1, target.ClimbCategory);
        Assert.AreEqual("San Francisco", target.City);
        Assert.AreEqual("CA", target.State);
        Assert.AreEqual("United States", target.Country);
        Assert.IsFalse(target.Private);
        Assert.IsFalse(target.Hazardous);
        Assert.IsFalse(target.Starred);
        Assert.AreEqual(createdAt, target.CreatedAt);
        Assert.AreEqual(updatedAt, target.UpdatedAt);
        Assert.AreEqual(155.733f, target.TotalElevationGain, 0.001f);
        Assert.IsNotNull(target.Map);
        Assert.AreEqual("s229781", target.Map.Id);
        Assert.AreEqual(ResourceStates.Detail, target.Map.ResourceState);
        Assert.AreEqual("test_polyline", target.Map.Polyline);
        Assert.AreEqual(309974, target.EffortCount);
        Assert.AreEqual(30623, target.AthleteCount);
        Assert.AreEqual(2428, target.StarCount);
        Assert.IsNotNull(target.AthleteSegmentStats);
        Assert.AreEqual(TimeSpan.FromSeconds(553), target.AthleteSegmentStats.PrElapsedTime);
        Assert.AreEqual(new DateOnly(1993, 04, 03), target.AthleteSegmentStats.PrDate);
        Assert.AreEqual(2, target.AthleteSegmentStats.EffortCount);
    }

    [TestMethod]
    public void LatLng_DefaultValue_ShouldBeZero()
    {
        // Arrange & Act
        var target = new DetailedSegment();

        // Assert
        Assert.AreEqual(0f, target.StartLatlng.Latitude);
        Assert.AreEqual(0f, target.StartLatlng.Longitude);
        Assert.AreEqual(0f, target.EndLatLng.Latitude);
        Assert.AreEqual(0f, target.EndLatLng.Longitude);
    }

    [TestMethod]
    public void BooleanProperties_DefaultValues_ShouldBeFalse()
    {
        // Arrange & Act
        var target = new DetailedSegment();

        // Assert
        Assert.IsFalse(target.Private);
        Assert.IsFalse(target.Hazardous);
        Assert.IsFalse(target.Starred);
    }

    [TestMethod]
    public void BooleanProperties_SetToTrue_ShouldReturnTrue()
    {
        // Arrange & Act
        var target = new DetailedSegment
        {
            Private = true,
            Hazardous = true,
            Starred = true
        };

        // Assert
        Assert.IsTrue(target.Private);
        Assert.IsTrue(target.Hazardous);
        Assert.IsTrue(target.Starred);
    }

    [TestMethod]
    public void NullableProperties_CanBeNull()
    {
        // Arrange & Act
        var target = new DetailedSegment
        {
            City = null,
            State = null,
            Country = null,
            Map = null,
            AthleteSegmentStats = null
        };

        // Assert
        Assert.IsNull(target.City);
        Assert.IsNull(target.State);
        Assert.IsNull(target.Country);
        Assert.IsNull(target.Map);
        Assert.IsNull(target.AthleteSegmentStats);
    }

    [TestMethod]
    public void GradeProperties_CanBeNegative()
    {
        // Arrange & Act
        var target = new DetailedSegment
        {
            AverageGrade = -5.5f,
            MaximumGrade = -12.3f
        };

        // Assert
        Assert.AreEqual(-5.5f, target.AverageGrade, 0.01f);
        Assert.AreEqual(-12.3f, target.MaximumGrade, 0.01f);
    }

    [TestMethod]
    public void ClimbCategory_CanBeZero()
    {
        // Arrange & Act
        var target = new DetailedSegment
        {
            ClimbCategory = 0
        };

        // Assert
        Assert.AreEqual(0, target.ClimbCategory);
    }

    [TestMethod]
    public void ClimbCategory_CanBeHighValue()
    {
        // Arrange & Act
        var target = new DetailedSegment
        {
            ClimbCategory = 5
        };

        // Assert
        Assert.AreEqual(5, target.ClimbCategory);
    }

    [TestMethod]
    public void CountProperties_DefaultToZero()
    {
        // Arrange & Act
        var target = new DetailedSegment();

        // Assert
        Assert.AreEqual(0, target.EffortCount);
        Assert.AreEqual(0, target.AthleteCount);
        Assert.AreEqual(0, target.StarCount);
    }

    [TestMethod]
    public void CountProperties_CanBeSetToLargeValues()
    {
        // Arrange & Act
        var target = new DetailedSegment
        {
            EffortCount = 309974,
            AthleteCount = 30623,
            StarCount = 2428
        };

        // Assert
        Assert.AreEqual(309974, target.EffortCount);
        Assert.AreEqual(30623, target.AthleteCount);
        Assert.AreEqual(2428, target.StarCount);
    }
}

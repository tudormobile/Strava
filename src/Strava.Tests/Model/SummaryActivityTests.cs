using System.Text;
using Tudormobile.Strava;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class SummaryActivityTests
{
    [TestMethod, DeploymentItem("activities.json")]
    public void FromJsonArrayTest()
    {
        string path = "activities.json";
        var utf8Json = File.OpenRead(path);
        StravaSerializer.TryDeserialize(utf8Json, out SummaryActivity[]? activities);
        Assert.IsNotNull(activities);
        Assert.AreNotEqual(0, activities.Length);
    }

    [TestMethod]
    public void FromJsonTest()
    {
        string json = @"
 [ {
  ""resource_state"" : 2,
  ""athlete"" : {
    ""id"" : 134815,
    ""resource_state"" : 1
  },
  ""name"" : ""Happy Friday"",
  ""distance"" : 24931.4,
  ""moving_time"" : 4500,
  ""elapsed_time"" : 4500,
  ""total_elevation_gain"" : 0,
  ""type"" : ""Ride"",
  ""sport_type"" : ""MountainBikeRide"",
  ""workout_type"" : null,
  ""id"" : 154504250376823,
  ""external_id"" : ""garmin_push_12345678987654321"",
  ""upload_id"" : 987654321234567891234,
  ""start_date"" : ""2018-05-02T12:15:09Z"",
  ""start_date_local"" : ""2018-05-02T05:15:09Z"",
  ""timezone"" : ""(GMT-08:00) America/Los_Angeles"",
  ""utc_offset"" : -25200,
  ""start_latlng"" : null,
  ""end_latlng"" : null,
  ""location_city"" : null,
  ""location_state"" : null,
  ""location_country"" : ""United States"",
  ""achievement_count"" : 0,
  ""kudos_count"" : 3,
  ""comment_count"" : 1,
  ""athlete_count"" : 1,
  ""photo_count"" : 0,
  ""map"" : {
    ""id"" : ""a12345678987654321"",
    ""summary_polyline"" : null,
    ""resource_state"" : 2
  },
  ""trainer"" : true,
  ""commute"" : false,
  ""manual"" : false,
  ""private"" : false,
  ""flagged"" : false,
  ""gear_id"" : ""b12345678987654321"",
  ""from_accepted_tag"" : false,
  ""average_speed"" : 5.54,
  ""max_speed"" : 11,
  ""average_cadence"" : 67.1,
  ""average_watts"" : 175.3,
  ""weighted_average_watts"" : 210,
  ""kilojoules"" : 788.7,
  ""device_watts"" : true,
  ""has_heartrate"" : true,
  ""average_heartrate"" : 140.3,
  ""max_heartrate"" : 178,
  ""max_watts"" : 406,
  ""pr_count"" : 0,
  ""total_photo_count"" : 1,
  ""has_kudoed"" : false,
  ""suffer_score"" : 82
}, {
  ""resource_state"" : 2,
  ""athlete"" : {
    ""id"" : 167560,
    ""resource_state"" : 1
  },
  ""name"" : ""Bondcliff"",
  ""distance"" : 23676.5,
  ""moving_time"" : 5400,
  ""elapsed_time"" : 5400,
  ""total_elevation_gain"" : 0,
  ""type"" : ""Ride"",
  ""sport_type"" : ""MountainBikeRide"",
  ""workout_type"" : null,
  ""id"" : 1234567809,
  ""external_id"" : ""garmin_push_12345678987654321"",
  ""upload_id"" : 1234567819,
  ""start_date"" : ""2018-04-30T12:35:51Z"",
  ""start_date_local"" : ""2018-04-30T05:35:51Z"",
  ""timezone"" : ""(GMT-08:00) America/Los_Angeles"",
  ""utc_offset"" : -25200,
  ""start_latlng"" : null,
  ""end_latlng"" : null,
  ""location_city"" : null,
  ""location_state"" : null,
  ""location_country"" : ""United States"",
  ""achievement_count"" : 0,
  ""kudos_count"" : 4,
  ""comment_count"" : 0,
  ""athlete_count"" : 1,
  ""photo_count"" : 0,
  ""map"" : {
    ""id"" : ""a12345689"",
    ""summary_polyline"" : null,
    ""resource_state"" : 2
  },
  ""trainer"" : true,
  ""commute"" : false,
  ""manual"" : false,
  ""private"" : false,
  ""flagged"" : false,
  ""gear_id"" : ""b12345678912343"",
  ""from_accepted_tag"" : false,
  ""average_speed"" : 4.385,
  ""max_speed"" : 8.8,
  ""average_cadence"" : 69.8,
  ""average_watts"" : 200,
  ""weighted_average_watts"" : 214,
  ""kilojoules"" : 1080,
  ""device_watts"" : true,
  ""has_heartrate"" : true,
  ""average_heartrate"" : 152.4,
  ""max_heartrate"" : 183,
  ""max_watts"" : 403,
  ""pr_count"" : 0,
  ""total_photo_count"" : 1,
  ""has_kudoed"" : false,
  ""suffer_score"" : 162
} ]";
        var s = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var actual = StravaSerializer.TryDeserialize(s, out SummaryActivity[]? activities);
        Assert.IsTrue(actual);
        Assert.IsNotNull(activities);
        Assert.AreEqual(2, activities.Length);
        var item = activities[1];

        // Validate the second SummaryActivity
        Assert.AreEqual(1234567809, item.Id);
        Assert.AreEqual(167560, item.Athlete.Id);
        Assert.AreEqual("Bondcliff", item.Name);
        Assert.AreEqual(23676.5, item.Distance);
        Assert.AreEqual(5400, item.MovingTime);
        Assert.AreEqual(5400, item.ElapsedTime);
        Assert.AreEqual(0, item.TotalElevationGain);
        Assert.AreEqual("Ride", item.Type);
        Assert.AreEqual("MountainBikeRide", item.SportType);
        Assert.IsNull(item.WorkoutType);
        Assert.AreEqual(new DateTime(2018, 04, 30, 12, 35, 51, DateTimeKind.Utc), item.StartDate);
        Assert.AreEqual(4.385, item.AverageSpeed);
        Assert.AreEqual(8.8, item.MaxSpeed);
    }

    [TestMethod]
    public void ConstructorTest()
    {
        var target = new SummaryActivity();
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
        Assert.AreEqual(String.Empty, target.Name);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var startDate = DateTime.Now;
        var target = new SummaryActivity
        {
            Id = 123456789,
            Name = "Morning Run",
            Distance = 5000.0,
            MovingTime = 1800.0,
            ElapsedTime = 1900.0,
            TotalElevationGain = 100.0,
            Type = "Run",
            SportType = "Run",
            WorkoutType = 1,
            StartDate = startDate,
            AverageSpeed = 2.78,
            MaxSpeed = 4.5
        };

        Assert.AreEqual(123456789, target.Id);
        Assert.AreEqual("Morning Run", target.Name);
        Assert.AreEqual(5000.0, target.Distance);
        Assert.AreEqual(1800.0, target.MovingTime);
        Assert.AreEqual(1900.0, target.ElapsedTime);
        Assert.AreEqual(100.0, target.TotalElevationGain);
        Assert.AreEqual("Run", target.Type);
        Assert.AreEqual("Run", target.SportType);
        Assert.AreEqual(1, target.WorkoutType);
        Assert.AreEqual(startDate, target.StartDate);
        Assert.AreEqual(2.78, target.AverageSpeed);
        Assert.AreEqual(4.5, target.MaxSpeed);
    }
}

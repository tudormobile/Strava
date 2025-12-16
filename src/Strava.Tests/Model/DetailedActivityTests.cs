using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class DetailedActivityTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new DetailedActivity();
        Assert.IsNotNull(target);
        Assert.IsFalse(target.Commute);
        Assert.IsFalse(target.Trainer);
        Assert.AreEqual(String.Empty, target.Description);
        Assert.AreEqual(0, target.Id);
        Assert.AreEqual(String.Empty, target.Name);
    }

    [TestMethod]
    public void ConstructorFromSummaryActivityTest()
    {
        var summaryActivity = new SummaryActivity
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
            StartDate = DateTime.Now,
            AverageSpeed = 2.78,
            MaxSpeed = 4.5,
            Athlete = new AthleteId { Id = 111, ResourceState = 2 }
        };

        var target = new DetailedActivity(summaryActivity);

        Assert.AreEqual(summaryActivity.Id, target.Id);
        Assert.AreEqual(summaryActivity.Name, target.Name);
        Assert.AreEqual(summaryActivity.Distance, target.Distance);
        Assert.AreEqual(summaryActivity.MovingTime, target.MovingTime);
        Assert.AreEqual(summaryActivity.ElapsedTime, target.ElapsedTime);
        Assert.AreEqual(summaryActivity.TotalElevationGain, target.TotalElevationGain);
        Assert.AreEqual(summaryActivity.Type, target.Type);
        Assert.AreEqual(summaryActivity.SportType, target.SportType);
        Assert.AreEqual(summaryActivity.WorkoutType, target.WorkoutType);
        Assert.AreEqual(summaryActivity.StartDate, target.StartDate);
        Assert.AreEqual(summaryActivity.AverageSpeed, target.AverageSpeed);
        Assert.AreEqual(summaryActivity.MaxSpeed, target.MaxSpeed);
        Assert.AreSame(summaryActivity.Athlete, target.Athlete);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var target = new DetailedActivity
        {
            Commute = true,
            Trainer = true,
            Description = "Great morning workout with intervals",
            Name = "Interval Training",
            Distance = 10000.0,
            Id = 999888777
        };

        Assert.IsTrue(target.Commute);
        Assert.IsTrue(target.Trainer);
        Assert.AreEqual("Great morning workout with intervals", target.Description);
        Assert.AreEqual("Interval Training", target.Name);
        Assert.AreEqual(10000.0, target.Distance);
        Assert.AreEqual(999888777, target.Id);
    }

    [TestMethod]
    public void InheritanceFromSummaryActivityTest()
    {
        var target = new DetailedActivity();
        Assert.IsInstanceOfType<SummaryActivity>(target);
    }

    [TestMethod]
    public void CommuteActivityTest()
    {
        var target = new DetailedActivity
        {
            Commute = true,
            Name = "Morning Commute",
            Distance = 5000.0
        };

        Assert.IsTrue(target.Commute);
        Assert.IsFalse(target.Trainer);
    }

    [TestMethod]
    public void TrainerActivityTest()
    {
        var target = new DetailedActivity
        {
            Trainer = true,
            Name = "Indoor Cycling",
            Description = "Zwift session"
        };

        Assert.IsTrue(target.Trainer);
        Assert.IsFalse(target.Commute);
        Assert.AreEqual("Zwift session", target.Description);
    }

    [TestMethod]
    public void DetailedActivityWithAllPropertiesTest()
    {
        var startDate = DateTime.Now;
        var target = new DetailedActivity
        {
            Id = 555666777,
            Name = "Long Trail Run",
            Distance = 15000.0,
            MovingTime = 5400.0,
            ElapsedTime = 5700.0,
            TotalElevationGain = 500.0,
            Type = "Run",
            SportType = "TrailRun",
            WorkoutType = 2,
            StartDate = startDate,
            AverageSpeed = 2.78,
            MaxSpeed = 5.0,
            Commute = false,
            Trainer = false,
            Description = "Beautiful trail conditions with amazing views"
        };

        Assert.AreEqual(555666777, target.Id);
        Assert.AreEqual("Long Trail Run", target.Name);
        Assert.AreEqual(15000.0, target.Distance);
        Assert.AreEqual("Beautiful trail conditions with amazing views", target.Description);
        Assert.IsFalse(target.Commute);
        Assert.IsFalse(target.Trainer);
    }
}
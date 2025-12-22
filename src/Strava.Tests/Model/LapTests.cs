using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class LapTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new Lap();
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
        Assert.AreEqual(string.Empty, target.Name);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var startDate = DateTime.Now;
        var target = new Lap
        {
            Id = 123,
            Name = "Test Lap",
            AverageCadence = 85.5,
            AverageSpeed = 5.2,
            Distance = 1000.0,
            ElapsedTime = TimeSpan.FromMinutes(10),
            StartIndex = 0,
            EndIndex = 100,
            LapIndex = 1,
            MaxSpeed = 7.5,
            MovingTime = TimeSpan.FromMinutes(9),
            PaceZone = 2,
            Split = 1,
            StartDate = startDate,
            StartDateLocal = startDate.ToLocalTime(),
            TotalElevationGain = 50.0f,
            Athlete = new MetaAthlete() { Id = 123 },
            Activity = new MetaActivity() { Id = 456 }
        };

        Assert.AreEqual(123, target.Id);
        Assert.AreEqual("Test Lap", target.Name);
        Assert.AreEqual(85.5, target.AverageCadence);
        Assert.AreEqual(5.2, target.AverageSpeed);
        Assert.AreEqual(1000.0, target.Distance);
        Assert.AreEqual(TimeSpan.FromMinutes(10), target.ElapsedTime);
        Assert.AreEqual(0, target.StartIndex);
        Assert.AreEqual(100, target.EndIndex);
        Assert.AreEqual(1, target.LapIndex);
        Assert.AreEqual(7.5, target.MaxSpeed);
        Assert.AreEqual(TimeSpan.FromMinutes(9), target.MovingTime);
        Assert.AreEqual(2, target.PaceZone);
        Assert.AreEqual(1, target.Split);
        Assert.AreEqual(startDate, target.StartDate);
        Assert.AreEqual(startDate.ToLocalTime(), target.StartDateLocal);
        Assert.AreEqual(50.0f, target.TotalElevationGain);
        Assert.AreEqual(123, target.Athlete.Id);
        Assert.AreEqual(456, target.Activity.Id);
    }
}
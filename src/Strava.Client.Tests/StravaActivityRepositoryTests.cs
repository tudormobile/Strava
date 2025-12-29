using Tudormobile.Strava.Client;
using Tudormobile.Strava.Model;

namespace Strava.Client.Tests;

[TestClass]
public class StravaActivityRepositoryTests
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task StravaActivityRepository_ConstrctorSetsPropertiesTest()
    {
        // Arrange & Act
        var lastSavedTime = new DateTime(2024, 1, 3);
        var lastActivityTime = new DateTime(2024, 1, 2);
        var repo = new StravaActivityRepository(new StravaActivityRepositoryTestingContext(lastSavedTime, lastActivityTime, []));

        // Assert
        Assert.AreEqual(lastSavedTime, repo.LastSavedTime);
        Assert.AreEqual(lastActivityTime, repo.LastModifiedTime);
        Assert.IsFalse(repo.HasUnsavedChanges());
        Assert.HasCount(0, await repo.GetActivitiesAsync(TestContext.CancellationToken));
        Assert.IsFalse(repo.ActivityExists(1));
    }

    [TestMethod]
    public async Task StravaActivityRepository_AddActivityTest()
    {
        // Arrange
        var lastSavedTime = new DateTime(2024, 1, 3);
        var lastActivityTime = new DateTime(2024, 1, 2);
        var repo = new StravaActivityRepository(new StravaActivityRepositoryTestingContext(lastSavedTime, lastActivityTime, []));

        var today = DateTime.UtcNow;
        var activityName = "Morning Ride";
        var activityId = 1;
        var activityStartDate = today.AddDays(-2);

        var summaryActivity = new SummaryActivity
        {
            Id = activityId,
            Name = activityName,
            StartDate = activityStartDate,
        };
        var newActivity = new StravaActivity(summaryActivity);

        // Act
        repo.AddActivity(newActivity);

        // Assert
        Assert.AreEqual(lastSavedTime, repo.LastSavedTime);
        Assert.IsTrue(repo.HasUnsavedChanges());
        Assert.HasCount(1, await repo.GetActivitiesAsync(TestContext.CancellationToken));
        Assert.IsTrue(repo.ActivityExists(activityId));
        Assert.AreEqual(newActivity, repo.GetActivityById(activityId));

        Assert.AreEqual(lastSavedTime, repo.LastSavedTime, "Time stamp does not update until saved.");
        Assert.AreEqual(activityStartDate, repo.LastModifiedTime, "Time stamps should update on add");
    }

    [TestMethod]
    public async Task StravaActivityRepository_DeleteActivityTest()
    {
        // Arrange
        var lastSavedTime = new DateTime(2024, 1, 3);
        var lastActivityTime = new DateTime(2024, 1, 2);
        var repo = new StravaActivityRepository(new StravaActivityRepositoryTestingContext(lastSavedTime, lastActivityTime, []));

        var today = DateTime.UtcNow;
        var activityName = "Morning Ride";
        var activityId = 1;
        var activityStartDate = today.AddDays(-2);

        var summaryActivity = new SummaryActivity
        {
            Id = activityId,
            Name = activityName,
            StartDate = activityStartDate,
        };
        var newActivity = new StravaActivity(summaryActivity);

        // Act
        repo.DeleteActivity(activityId);    // ensure no error if not exists
        repo.AddActivity(newActivity);
        repo.DeleteActivity(activityId);

        // Assert
        Assert.AreEqual(lastSavedTime, repo.LastSavedTime);
        Assert.IsTrue(repo.HasUnsavedChanges());
        Assert.HasCount(0, await repo.GetActivitiesAsync(TestContext.CancellationToken));
        Assert.IsFalse(repo.ActivityExists(activityId));

        Assert.AreEqual(lastSavedTime, repo.LastSavedTime, "Time stamp does not update until saved.");
        Assert.AreEqual(DateTime.MinValue, repo.LastModifiedTime, "Time stamps should update on delete");
    }

    [TestMethod]
    public async Task StravaActivityRepository_SaveActivitiesTest()
    {
        // Arrange
        var lastSavedTime = new DateTime(2024, 1, 3);
        var lastActivityTime = new DateTime(2024, 1, 2);
        var repo = new StravaActivityRepository(new StravaActivityRepositoryTestingContext(lastSavedTime, lastActivityTime, []));

        var today = DateTime.UtcNow;
        var activityName = "Morning Ride";
        var activityId = 1;
        var activityStartDate = today.AddDays(-2);

        var summaryActivity = new SummaryActivity
        {
            Id = activityId,
            Name = activityName,
            StartDate = activityStartDate,
        };
        var newActivity = new StravaActivity(summaryActivity);

        // Act
        repo.AddActivity(newActivity);
        await repo.SaveChangesAsync(TestContext.CancellationToken);

        // Assert
        Assert.AreEqual(DateTime.UtcNow.Date, repo.LastSavedTime.Date);
        Assert.IsFalse(repo.HasUnsavedChanges());
        Assert.HasCount(1, await repo.GetActivitiesAsync(TestContext.CancellationToken));
    }

    [TestMethod]
    public async Task StravaActivityRepository_UpdateActivityTest()
    {
        // Arrange
        var lastSavedTime = new DateTime(2024, 1, 3);
        var lastActivityTime = new DateTime(2024, 1, 2);
        var repo = new StravaActivityRepository(new StravaActivityRepositoryTestingContext(lastSavedTime, lastActivityTime, []));

        var today = DateTime.UtcNow;
        var activityName = "Morning Ride";
        var activityId = 1;
        var activityStartDate = today.AddDays(-2);

        var summaryActivity = new SummaryActivity
        {
            Id = activityId,
            Name = activityName,
            StartDate = activityStartDate,
        };
        var newActivity = new StravaActivity(summaryActivity);

        // Act
        repo.UpdateActivity(newActivity);   // ensure no error if not exists
        repo.AddActivity(newActivity);
        await repo.SaveChangesAsync(TestContext.CancellationToken);
        var updatedActivityName = "Evening Ride";
        repo.UpdateActivity(new StravaActivity(new SummaryActivity
        {
            Id = activityId,
            Name = updatedActivityName,
            StartDate = activityStartDate,
        }));

        // Assert
        Assert.IsTrue(repo.HasUnsavedChanges());
        Assert.HasCount(1, await repo.GetActivitiesAsync(TestContext.CancellationToken));
        var retrievedActivity = repo.GetActivityById(activityId);
        Assert.IsNotNull(retrievedActivity);
        Assert.AreEqual(updatedActivityName, retrievedActivity.Name);
    }

}

public class StravaActivityRepositoryTestingContext : IStravaActivityRepositoryContext
{
    private DateTime _lastSavedTime;
    private DateTime _lastActivityTime;
    private IList<StravaActivity> _activities;

    public StravaActivityRepositoryTestingContext(DateTime lastSavedTime, DateTime lastActivityTime, IEnumerable<StravaActivity> activities)
    {
        _lastSavedTime = lastSavedTime;
        _lastActivityTime = lastActivityTime;
        _activities = [.. activities];
    }

    public Task<IList<StravaActivity>> LoadActivitiesAsync(CancellationToken cancellationToken) => Task.FromResult(_activities);
    public (DateTime LastSavedTime, DateTime LastActivityTime) Metadata() => (_lastSavedTime, _lastActivityTime);
    public Task<bool> SaveActivitiesAsync(IEnumerable<StravaActivity> activities, CancellationToken cancellationToken = default)
    {
        _activities = [.. activities];
        _lastActivityTime = _activities.Count == 0 ? DateTime.MinValue : _activities.Max(a => a.StartDate);
        _lastSavedTime = DateTime.UtcNow;
        return Task.FromResult(true);
    }
}

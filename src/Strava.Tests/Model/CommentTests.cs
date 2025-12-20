using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class CommentTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new Comment();
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
        Assert.AreEqual(0, target.ActivityId);
        Assert.AreEqual(String.Empty, target.Text);
        Assert.IsNull(target.Athlete);
        Assert.AreEqual(default, target.CreatedAt);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var createdAt = DateTime.Now;
        var athlete = new SummaryAthlete { Firstname = "John", Lastname = "Doe" };

        var target = new Comment
        {
            Id = 123456789,
            ActivityId = 987654321,
            Text = "Great workout!",
            Athlete = athlete,
            CreatedAt = createdAt
        };

        Assert.AreEqual(123456789, target.Id);
        Assert.AreEqual(987654321, target.ActivityId);
        Assert.AreEqual("Great workout!", target.Text);
        Assert.AreSame(athlete.Firstname, target.Athlete.Firstname);
        Assert.AreSame(athlete.Lastname, target.Athlete.Lastname);
        Assert.AreEqual(createdAt, target.CreatedAt);
    }

    [TestMethod]
    public void CommentWithAthleteTest()
    {
        var athlete = Athlete.FromJson(@"{
            ""id"": 12345,
            ""username"": ""test_user"",
            ""firstname"": ""Test"",
            ""lastname"": ""User"",
            ""resource_state"": 2
        }");

        var target = new Comment
        {
            Id = 111,
            ActivityId = 222,
            Text = "Nice job!",
            Athlete = new SummaryAthlete { Firstname = "Test", Lastname = "User" },
            CreatedAt = new DateTime(2025, 1, 1)
        };

        Assert.IsNotNull(target.Athlete);
        Assert.AreEqual("Test", target.Athlete.Firstname);
        Assert.AreEqual("User", target.Athlete.Lastname);
    }

    [TestMethod]
    public void EmptyCommentTextTest()
    {
        var target = new Comment
        {
            Id = 1,
            ActivityId = 2,
            Text = String.Empty
        };

        Assert.AreEqual(String.Empty, target.Text);
    }
}
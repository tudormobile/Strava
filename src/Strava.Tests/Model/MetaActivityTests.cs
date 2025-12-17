using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class MetaActivityTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new MetaActivity();
        Assert.IsNotNull(target);
        Assert.AreEqual(0, target.Id);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var target = new MetaActivity
        {
            Id = 12345
        };

        Assert.AreEqual(12345, target.Id);
    }
}
using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ErrorTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var target = new Error();
        Assert.IsNotNull(target);
        Assert.AreEqual(string.Empty, target.Code);
        Assert.AreEqual(string.Empty, target.Field);
        Assert.AreEqual(string.Empty, target.Resource);
    }

    [TestMethod]
    public void PropertyAssignmentTest()
    {
        var target = new Error
        {
            Code = "invalid",
            Field = "client_id",
            Resource = "Application"
        };

        Assert.AreEqual("invalid", target.Code);
        Assert.AreEqual("client_id", target.Field);
        Assert.AreEqual("Application", target.Resource);
    }
}

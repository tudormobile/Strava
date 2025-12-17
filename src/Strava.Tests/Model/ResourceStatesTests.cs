using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ResourceStatesTests
{
    [TestMethod]
    public void EnumNamesTest()
    {
        Assert.AreEqual("Meta", ResourceStates.Meta.ToString());
        Assert.AreEqual("Summary", ResourceStates.Summary.ToString());
        Assert.AreEqual("Detail", ResourceStates.Detail.ToString());
    }
}
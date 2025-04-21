using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ScopeTests
{
    [TestMethod]
    public void ToStringTest()
    {
        var expected = "read_all,write,profile:read_all,profile:write,activity:read_all";
        var target = new Scope()
        {
            PublicScope = Scope.ScopePermission.read_all | Scope.ScopePermission.write,
            ActivityScope = Scope.ScopePermission.read_all,
            ProfileScope = Scope.ScopePermission.write | Scope.ScopePermission.read_all,
        };
        var actual = target.ToString();
        Assert.AreEqual(expected, actual);
    }
}

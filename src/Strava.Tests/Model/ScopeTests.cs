using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class ScopeTests
{
    [TestMethod]
    public void ToStringTest()
    {
        var expected = "read_all,write,profile:read_all,profile:write,activity:read_all";
        var target = new AuthorizationScope()
        {
            PublicScope = AuthorizationScope.ScopePermission.read_all | AuthorizationScope.ScopePermission.write,
            ActivityScope = AuthorizationScope.ScopePermission.read_all,
            ProfileScope = AuthorizationScope.ScopePermission.write | AuthorizationScope.ScopePermission.read_all,
        };
        var actual = target.ToString();
        Assert.AreEqual(expected, actual);
    }
}

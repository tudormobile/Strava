using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class AuthorizationScopeTests
{
    [TestMethod]
    public void ToStringTest()
    {
        var expected = "read_all,profile:read_all,profile:write,activity:read_all";
        var target = new AuthorizationScope()
        {
            PublicScope = AuthorizationScope.PublicScopes.read_all | AuthorizationScope.PublicScopes.read,
            ActivityScope = AuthorizationScope.ActivityScopes.read_all,
            ProfileScope = AuthorizationScope.ProfileScopes.write | AuthorizationScope.ProfileScopes.read_all,
        };
        var actual = target.ToString();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ReadScopeTest()
    {
        var expected = "read,profile:read_all,activity:read";
        var actual = AuthorizationScope.READ.ToString();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToStringWithBadScopeTest()
    {
        var expected = "read";
        var target = new AuthorizationScope();
        target.PublicScope = AuthorizationScope.PublicScopes.read;
        target.ProfileScope = (AuthorizationScope.ProfileScopes)AuthorizationScope.ScopePermission.read;
        var actual = target.ToString();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToStringWithExtraActivityScopeTest()
    {
        var expected = "activity:read_all,activity:write";
        var target = new AuthorizationScope(activityScope: AuthorizationScope.ActivityScopes.read
            | AuthorizationScope.ActivityScopes.read_all
            | AuthorizationScope.ActivityScopes.write);
        var actual = target.ToString();
        Assert.AreEqual(expected, actual);
    }
}

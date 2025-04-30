using System.Diagnostics.CodeAnalysis;
using Tudormobile.Strava;

namespace Strava.Tests;

[TestClass]
public class StravaSessionTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var auth = new StravaAuthorization();
        var target = new StravaSession(auth);
        Assert.AreEqual(auth, target.Authorization);
        Assert.IsFalse(target.IsAuthenticated, "Must be non-authenticated since no data was provided"); // no exceptions thrown
    }

    [TestMethod, ExpectedException(typeof(ArgumentNullException)), ExcludeFromCodeCoverage]
    public void ConstructorWithNullTest()
    {
        var auth = new StravaAuthorization();
        auth = null;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        _ = new StravaSession(null);   // throws
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public async Task RefreshTestWithBadTokenTest()
    {
        var target = new StravaSession(new StravaAuthorization());
        var actual = await target.RefreshAsync();
        Assert.IsFalse(actual.Success, "Should have failed with bad tokens.");
        Assert.IsNotNull(actual.Error);
        Assert.IsNull(actual.Data);
        if (actual.Error.Fault.Errors.Length > 0)
        {
            Assert.AreEqual("Bad Request", actual.Error.Fault.Message);
            Assert.AreEqual("client_id", actual.Error.Fault.Errors[0].Field);
            Assert.AreEqual("invalid", actual.Error.Fault.Errors[0].Code);
            Assert.AreEqual("Application", actual.Error.Fault.Errors[0].Resource);
        }
    }
}
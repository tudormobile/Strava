using System.Net;
using Tudormobile.Strava;

namespace Strava.Tests;

[TestClass]
public class StravaExceptionTests
{
    [TestMethod]
    public void ConstructorSetsPropertiesTest()
    {
        // Arrange
        string[] expected = ["v1", "v2"];
        var headers = new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["X-Test"] = expected
        };

        // Act
        var ex = new StravaException("boom", HttpStatusCode.BadRequest, "error-body", headers);

        // Assert
        Assert.AreEqual("boom", ex.Message);
        Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
        Assert.AreEqual("error-body", ex.Content);
        Assert.IsNotNull(ex.ResponseHeaders);
        Assert.IsTrue(ex.ResponseHeaders!.ContainsKey("X-Test"));
        CollectionAssert.AreEqual(expected, new List<string>(ex.ResponseHeaders["X-Test"]));
    }

    [TestMethod]
    public void ConstructorSetsBasePropertiesTest()
    {
        // Arrange 
        string message = "this is a test";
        Exception innerException = new(message);

        // Act
        var ex = new StravaException(message, innerException);
        Assert.AreEqual(message, ex.Message);
        Assert.AreEqual(innerException, ex.InnerException);
        Assert.AreEqual(innerException.Message, message);
    }

    [TestMethod]
    public void ConstructorSetsMessageTest()
    {
        // Arrange 
        string message = "this is a test";

        // Act
        var ex = new StravaException(message);
        Assert.AreEqual(message, ex.Message);
    }
    private static readonly string[] expected = ["a"];

    [TestMethod]
    public void ResponseHeadersAreCopiedAndImmutableFromOriginalTest()
    {
        // Arrange
        var original = new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["h"] = ["a"]
        };

        var ex = new StravaException("msg", HttpStatusCode.OK, null, original);

        // Mutate original after construction
        original["h"] = ["b", "c"];

        // Assert: exception keeps the snapshot it received
        Assert.IsNotNull(ex.ResponseHeaders);
        CollectionAssert.AreEqual(expected, new List<string>(ex.ResponseHeaders!["h"]));
    }

    [TestMethod]
    public void ToStringTest()
    {
        // Arrange
        var content = "this is a test";
        var ex = new StravaException("m", HttpStatusCode.BadRequest, content);

        // Act
        var s = ex.ToString();

        // Assert
        Assert.IsTrue(s.Contains("HTTP Status: BadRequest"));
        Assert.IsTrue(s.Contains("Content: "));
        Assert.IsTrue(s.Contains(content));
    }

    [TestMethod]
    public void ToStringIncludesStatusAndTruncatedContentPreviewTest()
    {
        // Arrange
        var longContent = new string('x', 2000);
        var ex = new StravaException("m", HttpStatusCode.BadRequest, longContent, null);

        // Act
        var s = ex.ToString();

        // Assert
        Assert.IsTrue(s.Contains("HTTP Status: BadRequest"));
        Assert.IsTrue(s.Contains("Content: "));
        Assert.IsTrue(s.Contains("…(truncated)"), "ToString should include a truncated preview marker or be short.");
        Assert.IsTrue(s.Length < longContent.Length, "Failed to actually truncate");
    }
}
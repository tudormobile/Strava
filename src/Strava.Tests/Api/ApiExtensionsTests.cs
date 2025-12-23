using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace Strava.Tests.Api;

[TestClass]
public class ApiExtensionsTests
{
    [TestMethod]
    public void ClubsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.IsInstanceOfType<IClubsApi>(session.ClubsApi());
    }

    [TestMethod]
    public void GearsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.IsInstanceOfType<IGearsApi>(session.GearsApi());
    }

    [TestMethod]
    public void RoutesApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.RoutesApi());
    }

    [TestMethod]
    public void SegmentsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.IsInstanceOfType<ISegmentsApi>(session.SegmentsApi());
    }

    [TestMethod]
    public void StreamApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.StreamApi());
    }

    [TestMethod]
    public void UploadsApiTest()
    {
        var session = new StravaSession(new StravaAuthorization());
        Assert.ThrowsExactly<NotImplementedException>(() => session.UploadsApi());
    }

    [TestMethod]
    public void AddQueryToUriString_WithNoParameters_ReturnsOriginalUri()
    {
        // Arrange
        var uriString = "https://api.strava.com/v3/activities";
        var queryParameters = new List<(string, object?)>();

        // Act
        var result = ApiExtensions.AddQueryToUriString(uriString, queryParameters);

        // Assert
        Assert.AreEqual("https://api.strava.com/v3/activities", result);
    }

    [TestMethod]
    public void AddQueryToUriString_WithSingleParameter_AppendsQueryString()
    {
        // Arrange
        var uriString = "https://api.strava.com/v3/activities";
        var queryParameters = new List<(string, object?)>
        {
            ("page", 1)
        };

        // Act
        var result = ApiExtensions.AddQueryToUriString(uriString, queryParameters);

        // Assert
        Assert.AreEqual("https://api.strava.com/v3/activities?page=1", result);
    }

    [TestMethod]
    public void AddQueryToUriString_WithMultipleParameters_AppendsAllQueryStrings()
    {
        // Arrange
        var uriString = "https://api.strava.com/v3/activities";
        var queryParameters = new List<(string, object?)>
        {
            ("page", 1),
            ("per_page", 30),
            ("before", 1234567890)
        };

        // Act
        var result = ApiExtensions.AddQueryToUriString(uriString, queryParameters);

        // Assert
        Assert.AreEqual("https://api.strava.com/v3/activities?page=1&per_page=30&before=1234567890", result);
    }

    [TestMethod]
    public void AddQueryToUriString_WithNullValue_IgnoresNullParameter()
    {
        // Arrange
        var uriString = "https://api.strava.com/v3/activities";
        var queryParameters = new List<(string, object?)>
        {
            ("page", 1),
            ("filter", null),
            ("per_page", 30)
        };

        // Act
        var result = ApiExtensions.AddQueryToUriString(uriString, queryParameters);

        // Assert
        Assert.AreEqual("https://api.strava.com/v3/activities?page=1&per_page=30", result);
    }

    [TestMethod]
    public void AddQueryToUriString_WithEmptyStringValue_IgnoresEmptyParameter()
    {
        // Arrange
        var uriString = "https://api.strava.com/v3/activities";
        var queryParameters = new List<(string, object?)>
        {
            ("page", 1),
            ("filter", string.Empty),
            ("per_page", 30)
        };

        // Act
        var result = ApiExtensions.AddQueryToUriString(uriString, queryParameters);

        // Assert
        Assert.AreEqual("https://api.strava.com/v3/activities?page=1&per_page=30", result);
    }

    [TestMethod]
    public void AddQueryToUriString_WithWhitespaceValue_IgnoresWhitespaceParameter()
    {
        // Arrange
        var uriString = "https://api.strava.com/v3/activities";
        var queryParameters = new List<(string, object?)>
        {
            ("page", 1),
            ("filter", "   "),
            ("per_page", 30)
        };

        // Act
        var result = ApiExtensions.AddQueryToUriString(uriString, queryParameters);

        // Assert
        Assert.AreEqual("https://api.strava.com/v3/activities?page=1&per_page=30", result);
    }

    [TestMethod]
    public void AddQueryToUriString_WithAllNullParameters_ReturnsOriginalUri()
    {
        // Arrange
        var uriString = "https://api.strava.com/v3/activities";
        var queryParameters = new List<(string, object?)>
        {
            ("filter", null),
            ("type", null)
        };

        // Act
        var result = ApiExtensions.AddQueryToUriString(uriString, queryParameters);

        // Assert
        Assert.AreEqual("https://api.strava.com/v3/activities", result);
    }

    [TestMethod]
    public void AddQueryToUriString_WithStringParameter_AppendsStringValue()
    {
        // Arrange
        var uriString = "https://api.strava.com/v3/activities";
        var queryParameters = new List<(string, object?)>
        {
            ("name", "Morning Run")
        };

        // Act
        var result = ApiExtensions.AddQueryToUriString(uriString, queryParameters);

        // Assert
        Assert.AreEqual("https://api.strava.com/v3/activities?name=Morning+Run", result);
    }

    [TestMethod]
    public void AddQueryToUriString_WithSpecialCharacters_EncodesCharacters()
    {
        // Arrange
        var uriString = "https://api.strava.com/v3/activities";
        var queryParameters = new List<(string, object?)>
        {
            ("name", "Run & Bike")
        };

        // Act
        var result = ApiExtensions.AddQueryToUriString(uriString, queryParameters);

        // Assert
        Assert.Contains("Run+%26+Bike", result);
    }
}

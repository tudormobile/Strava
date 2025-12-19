using System.Net;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;

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

    [TestMethod]
    public void ConstructorWithNullTest()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.ThrowsExactly<ArgumentNullException>(() => _ = new StravaSession(null));   // throws
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    public async Task RefreshTestWithBadTokenTest()
    {
        var target = new StravaSession(new StravaAuthorization());
        var actual = await target.RefreshAsync(TestContext.CancellationToken);
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

    [TestMethod]
    public async Task RefreshTokensWhenNotAuthenticatedTest()
    {
        var target = new StravaSession(new StravaAuthorization());
        var actual = await target.RefreshTokensAsync(TestContext.CancellationToken);
        Assert.AreSame(target, actual, "Should return the same instance.");
        Assert.IsFalse(target.IsAuthenticated, "Should not be authenticated.");
    }

    [TestMethod]
    public void StravaAPITest()
    {
        var target = new StravaSession(new StravaAuthorization());
        var actual = target.StravaApi();
        Assert.IsInstanceOfType<IStravaApi>(actual);
        Assert.AreSame(target.StravaApi(), actual, "Failed to cache the api result.");
    }

    [TestMethod]
    public async Task RefreshAsync_SuccessfulRefreshTest()
    {
        var authorization = new StravaAuthorization()
        {
            Id = 1234,
            ClientSecret = "client-secret",
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Expires = DateTime.UtcNow.AddHours(-1),
        };

        var handler = new MockHttpMessageHandler() { JsonResponse = jsonRefresh };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsTrue(actual.Success);
        Assert.IsNotNull(actual.Data);
        Assert.IsNull(actual.Error);
        Assert.AreEqual(authorization.ClientSecret, actual.Data.ClientSecret);
        Assert.AreEqual("new-access-token", actual.Data.AccessToken);
        Assert.AreEqual("new-refresh-token", actual.Data.RefreshToken);
        Assert.AreEqual(0, actual.Data.Id, "Should not have set Athlete Id; not present in response from response.");
    }

    [TestMethod]
    public async Task RefreshAsync_SuccessfulRefresh_WithAthleteIdTest()
    {
        var authorization = new StravaAuthorization()
        {
            Id = 1234,
            ClientSecret = "client-secret",
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Expires = DateTime.UtcNow.AddHours(-1),
        };

        var handler = new MockHttpMessageHandler() { JsonResponse = jsonRefreshWithId };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsTrue(actual.Success);
        Assert.IsNotNull(actual.Data);
        Assert.IsNull(actual.Error);
        Assert.AreEqual(authorization.ClientSecret, actual.Data.ClientSecret);
        Assert.AreEqual("new-access-token", actual.Data.AccessToken);
        Assert.AreEqual("new-refresh-token", actual.Data.RefreshToken);
        Assert.AreEqual(1, actual.Data.Id, "Should have set Athlete Id from response.");
        // <12/18/2040 6:43:28 PM>
        var expectedData = new DateTime(2040, 12, 18, 18, 43, 28);
        Assert.AreEqual(expectedData, actual.Data.Expires, "Failed to properly process expiration date in response.");
    }

    // null access token in response
    [TestMethod]
    public async Task RefreshAsync_SuccessfulRefresh_WithNoAccessTokenInResponseTest()
    {
        var authorization = new StravaAuthorization()
        {
            Id = 1234,
            ClientSecret = "client-secret",
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Expires = DateTime.UtcNow.AddHours(-1),
        };

        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = jsonRefreshWithId.Replace("\"access_token\": \"new-access-token\"", "\"access_token\": null")
        };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsFalse(actual.Success);
        Assert.IsNull(actual.Data);
        Assert.IsNotNull(actual.Error);
        Assert.IsInstanceOfType<StravaException>(actual.Error.Exception);
    }

    // null refresh token in response
    [TestMethod]
    public async Task RefreshAsync_SuccessfulRefresh_WithNoRefreshTokenInResponseTest()
    {
        var authorization = new StravaAuthorization()
        {
            Id = 1234,
            ClientSecret = "client-secret",
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Expires = DateTime.UtcNow.AddHours(-1),
        };

        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = jsonRefreshWithId.Replace("\"refresh_token\": \"new-refresh-token\"", "\"refresh_token\": null")
        };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsFalse(actual.Success);
        Assert.IsNull(actual.Data);
        Assert.IsNotNull(actual.Error);
        Assert.IsInstanceOfType<StravaException>(actual.Error.Exception);
    }

    // malformed json response
    [TestMethod]
    public async Task RefreshAsync_SuccessfulRefresh_WithMalformedInJsonResponseTest()
    {
        var authorization = new StravaAuthorization()
        {
            Id = 1234,
            ClientSecret = "client-secret",
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Expires = DateTime.UtcNow.AddHours(-1),
        };

        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = "this-is-malformed-json"
        };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsFalse(actual.Success);
        Assert.IsNull(actual.Data);
        Assert.IsNotNull(actual.Error);
        Assert.IsInstanceOfType<StravaException>(actual.Error.Exception);
    }

    // athlete id missing in response
    [TestMethod]
    public async Task RefreshAsync_SuccessfulRefresh_WithAthleteNullIdInResponseTest()
    {
        var authorization = new StravaAuthorization()
        {
            Id = 1234,
            ClientSecret = "client-secret",
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Expires = DateTime.UtcNow.AddHours(-1),
        };

        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = jsonRefreshWithId.Replace("\"id\": 1,", string.Empty)
        };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsTrue(actual.Success);
        Assert.IsNotNull(actual.Data);
        Assert.IsNull(actual.Error);
        Assert.AreEqual(0, actual.Data.Id, "Should not have set Athlete Id from response.");
    }

    [TestMethod]
    public async Task RefreshAsync_ExceptionTest()
    {
        var authorization = new StravaAuthorization();
        var ex = new Exception();
        var handler = new MockHttpMessageHandler() { AlwaysThrows = ex };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsFalse(actual.Success);
        Assert.AreEqual(ex, actual.Error!.Exception);
    }

    [TestMethod]
    public async Task RefreshAsync_HttpExceptionTest()
    {
        var authorization = new StravaAuthorization();
        var ex = new HttpRequestException();
        var handler = new MockHttpMessageHandler() { AlwaysThrows = ex };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsFalse(actual.Success);
        Assert.AreEqual(ex, actual.Error!.Exception);
    }

    [TestMethod]
    public async Task RefreshAsync_InternalServerErrorTest()
    {
        var authorization = new StravaAuthorization();
        var handler = new MockHttpMessageHandler() { AlwaysResponds = new HttpResponseMessage(HttpStatusCode.InternalServerError) };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsFalse(actual.Success);
    }

    [TestMethod]
    public async Task RefreshAsync_RespondsWithJsonFaultErrorTest()
    {
        var authorization = new StravaAuthorization();
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.Unauthorized),
            JsonResponse = jsonFault,
        };
        var client = new HttpClient(handler);
        var target = new StravaSession(authorization, client);

        var actual = await target.RefreshAsync(client, TestContext.CancellationToken);

        Assert.IsFalse(actual.Success);
        Assert.IsNotNull(actual.Error);
        Assert.AreEqual("This is a fault message", actual.Error.Fault.Message);
        Assert.IsNotNull(actual.Error.Fault);
        Assert.HasCount(2, actual.Error.Fault.Errors);
    }


    public TestContext TestContext { get; set; }

    private static readonly string jsonFault = @"{
  ""message"": ""This is a fault message"",
  ""errors"": [
    {
      ""code"": ""code 1"",
      ""field"": ""field 1"",
      ""resource"": ""resource 1""
    },
    {
      ""code"": ""code 1"",
      ""field"": ""field 1"",
      ""resource"": ""resource 1""
    }
  ]    
}";

    private static readonly string jsonRefresh = @"{
  ""token_type"": ""Bearer"",
  ""access_token"": ""new-access-token"",
  ""expires_at"":1568775134,
  ""expires_in"":20566,
  ""refresh_token"":""new-refresh-token""
}";

    private static readonly string jsonRefreshWithId = @"{
  ""token_type"": ""Bearer"",
  ""access_token"": ""new-access-token"",
  ""expires_at"":2239469008,
  ""expires_in"":20566,
  ""refresh_token"": ""new-refresh-token"",
  ""athlete"": {
    ""id"": 1,
    ""username"": ""sampleuser"",
    ""resource_state"": 2
  }
}";

}
/*
 
{
  "token_type": "Bearer",
  "access_token": "a9b723...",
  "expires_at":1568775134,
  "expires_in":20566,
  "refresh_token":"b5c569..."
}

{
  "token_type": "Bearer",
  "expires_at": 1568775134,
  "expires_in": 21600,
  "refresh_token": "e5n567567...",
  "access_token": "a4b945687g...",
  "athlete": {
    #{summary athlete representation}
  }
}

{
  "message": "This is a fault message",
  "errors": [
    {
      "code": "code 1",
      "field": "field 1",
      "resource": "resource 1",
    },
    {
      "code": "code 1",
      "field": "field 1",
      "resource": "resource 1",
    }
  ]    
}
*/
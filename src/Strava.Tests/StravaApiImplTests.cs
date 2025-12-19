using System.Net;
using System.Net.Http.Headers;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

namespace Strava.Tests;

[TestClass]
public class StravaApiImplTests
{
    [TestMethod]
    public void StravaApiImpl_Constructor_WithHttpClientAndCredentials_ShouldInitialize()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler());
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        // Act
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Assert
        Assert.IsNotNull(stravaApi);
    }

    [TestMethod]
    public void StravaApiImpl_Constructor_WithHttpClientAndCredentials_ShouldReplaceAuthorizationHeader()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler());
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "old_access_token");
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        // Act
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Assert
        Assert.IsNotNull(stravaApi);
    }


    [TestMethod]
    public async Task StravaApiImpl_GetStreamAsyncThrowsTooManyRequestsException_ShouldReturnRateLimitError()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.TooManyRequests)
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var exception = await Assert.ThrowsExactlyAsync<StravaException>(async () => await stravaApi.GetStreamAsync("http://www.example.com", TestContext.CancellationToken));
        // Assert
        Assert.IsNotNull(exception);
        Assert.AreEqual(System.Net.HttpStatusCode.TooManyRequests, exception.StatusCode, "Failed to properly set the status code.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(exception.Message), "Failed to return some exception message.");
        Assert.IsNotNull(exception.InnerException, "Failed to set inner exception.");
    }

    [TestMethod]
    public async Task StravaApiImpl_GetStreamAsyncThrowsUnauthorized_ShouldReturnInvalidLoginError()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.Unauthorized)
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var exception = await Assert.ThrowsExactlyAsync<StravaException>(async () => await stravaApi.GetStreamAsync("http://www.example.com", TestContext.CancellationToken));
        // Assert
        Assert.IsNotNull(exception);
        Assert.AreEqual(System.Net.HttpStatusCode.Unauthorized, exception.StatusCode, "Failed to properly set the status code.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(exception.Message), "Failed to return some exception message.");
        Assert.IsNotNull(exception.InnerException, "Failed to set inner exception.");
    }

    [TestMethod]
    public async Task StravaApiImpl_GetStreamAsyncThrowsGeneralError_ShouldReturnStravaException()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var exception = await Assert.ThrowsExactlyAsync<StravaException>(async () => await stravaApi.GetStreamAsync("http://www.example.com", TestContext.CancellationToken));
        // Assert
        Assert.IsNotNull(exception);
        Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, exception.StatusCode, "Failed to properly set the status code.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(exception.Message), "Failed to return some exception message.");
        Assert.IsNotNull(exception.InnerException, "Failed to set inner exception.");
    }

    [TestMethod]
    public async Task StravaApiImpl_GetApiResultAsync_WithValidResponse_ShouldDeserializeCorrectly()
    {
        // Arrange
        var sampleJson = "{\"id\":12345,\"username\":\"testuser\",\"resource_state\":3}";
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = sampleJson
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var result = await stravaApi.GetApiResultAsync<AthleteId>(new Uri("http://www.example.com"), TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success, "API result indicates failure.");
        Assert.IsNotNull(result.Data, "Deserialized data is null.");
        Assert.IsNull(result.Error, "API result contains an error.");
        Assert.AreEqual(12345, result.Data.Id, "Deserialized Id does not match.");
    }

    [TestMethod]
    public async Task StravaApiImpl_GetApiResultAsync_WithErrorResponse_ShouldReturnErrorInResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.BadRequest)
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var result = await stravaApi.GetApiResultAsync<AthleteId>(new Uri("http://www.example.com"), TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success when it should indicate failure.");
        Assert.IsNull(result.Data, "Data should be null on error response.");
        Assert.IsNotNull(result.Error, "API result should contain an error.");
    }

    [TestMethod]
    public async Task StravaApiImpl_GetApiResultAsync_WithInvalidJson_ShouldReturnErrorInResult()
    {
        // Arrange
        var invalidJson = "this_is_invalid_json";
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = invalidJson
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var result = await stravaApi.GetApiResultAsync<AthleteId>(new Uri("http://www.example.com"), TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success when it should indicate failure.");
        Assert.IsNull(result.Data, "Data should be null on error response.");
        Assert.IsNotNull(result.Error, "API result should contain an error.");
    }

    [TestMethod]
    public async Task StravaApiImpl_GetApiResultAsync_WithNullJson_ShouldReturnErrorInResult()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = "null"
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var result = await stravaApi.GetApiResultAsync<AthleteId>(new Uri("http://www.example.com"), TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success when it should indicate failure.");
        Assert.IsNull(result.Data, "Data should be null on error response.");
        Assert.IsNotNull(result.Error, "API result should contain an error.");
    }

    [TestMethod]
    public async Task StravaApiImpl_PutApiResultAsync_WithValidResponse_ShouldDeserializeCorrectly()
    {
        // Arrange
        var sampleJson = "{\"id\":12345,\"username\":\"testuser\",\"resource_state\":3}";
        var sampleAthleteId = new AthleteId { Id = 12345, ResourceState = 3 };
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = sampleJson
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var result = await stravaApi.PutApiResultAsync<AthleteId, AthleteId>(new Uri("http://www.example.com"), sampleAthleteId, TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success, "API result indicates failure.");
        Assert.IsNotNull(result.Data, "Deserialized data is null.");
        Assert.IsNull(result.Error, "API result contains an error.");
        Assert.AreEqual(12345, result.Data.Id, "Deserialized Id does not match.");
    }

    [TestMethod]
    public async Task StravaApiImpl_PutApiResultAsyncThrowsTooManyRequestsException_ShouldReturnRateLimitError()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.TooManyRequests)
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        var sampleAthleteId = new AthleteId { Id = 12345, ResourceState = 3 };
        // Act
        var result = await stravaApi.PutApiResultAsync<AthleteId, AthleteId>(new Uri("http://www.example.com"), sampleAthleteId, TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success.");
        Assert.IsNull(result.Data, "Deserialized data exists.");
        Assert.IsNotNull(result.Error, "API result fails to contain an error.");
    }

    [TestMethod]
    public async Task StravaApiImpl_PutApiResultAsyncReturnsUnauthorized_ShouldReturnInvalidLoginError()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.Unauthorized)
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        var sampleAthleteId = new AthleteId { Id = 12345, ResourceState = 3 };
        // Act
        var result = await stravaApi.PutApiResultAsync<AthleteId, AthleteId>(new Uri("http://www.example.com"), sampleAthleteId, TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success.");
        Assert.IsNull(result.Data, "Deserialized data exists.");
        Assert.IsNotNull(result.Error, "API result fails to contain an error.");
        Assert.AreEqual("Invalid login", result.Error!.Message, "Error message does not match expected value.");
    }

    [TestMethod]
    public async Task StravaApiImpl_PutApiResultAsyncReturnsInternalServerError_ShouldReturnError()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        var sampleAthleteId = new AthleteId { Id = 12345, ResourceState = 3 };
        // Act
        var result = await stravaApi.PutApiResultAsync<AthleteId, AthleteId>(new Uri("http://www.example.com"), sampleAthleteId, TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success.");
        Assert.IsNull(result.Data, "Deserialized data exists.");
        Assert.IsNotNull(result.Error, "API result fails to contain an error.");
    }

    [TestMethod]
    public async Task StravaApiImpl_PutApiResultAsyncWithInvalidJson_ShouldReturnError()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = "invalid json"
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        var sampleAthleteId = new AthleteId { Id = 12345, ResourceState = 3 };
        // Act
        var result = await stravaApi.PutApiResultAsync<AthleteId, AthleteId>(new Uri("http://www.example.com"), sampleAthleteId, TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success.");
        Assert.IsNull(result.Data, "Deserialized data exists.");
        Assert.IsNotNull(result.Error, "API result fails to contain an error.");
    }

    [TestMethod]
    public async Task StravaApiImpl_PutApiResultAsyncWithNullPayload_ShouldReturnError()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = "invalid json"
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        var stravaApi = new StravaApiImpl(session, httpClient);
        AthleteId sampleAthleteId = null!;
        // Act
        var result = await stravaApi.PutApiResultAsync<AthleteId, AthleteId>(new Uri("http://www.example.com"), sampleAthleteId, TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success.");
        Assert.IsNull(result.Data, "Deserialized data exists.");
        Assert.IsNotNull(result.Error, "API result fails to contain an error.");
    }

    [TestMethod]
    public async Task StravaApiImpl_GetAthleteAsyncWithInternalServerError_ShouldReturnError()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        IStravaApi stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var result = await stravaApi.GetAthleteAsync(null, TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success.");
        Assert.IsNull(result.Data, "Deserialized data exists.");
        Assert.IsNotNull(result.Error, "API result fails to contain an error.");
    }

    [TestMethod]
    public async Task StravaApiImpl_GetAthleteAsyncWithBadJsonResponse_ShouldReturnError()
    {
        var handler = new MockHttpMessageHandler()
        {
            JsonResponse = "invalid json"
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(12));
        var session = new StravaSession(stravaAuthorization, httpClient);
        IStravaApi stravaApi = new StravaApiImpl(session, httpClient);
        // Act
        var result = await stravaApi.GetAthleteAsync(null, TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success.");
        Assert.IsNull(result.Data, "Deserialized data exists.");
        Assert.IsNotNull(result.Error, "API result fails to contain an error.");
    }


    [TestMethod]
    public async Task StravaApiImpl_GetAuthenticatedClientAsync_WithRefreshRequired_ReturnsError()
    {
        // Arrange
        var handler = new MockHttpMessageHandler()
        {
            AlwaysResponds = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        };
        var httpClient = new HttpClient(handler);
        var clientId = "test_client_id";
        var clientSecret = "test_client_secret";
        var accessToken = "test_access_token";
        var refreshToken = "test_refresh_token";
        var stravaAuthorization = new StravaAuthorization(clientId, clientSecret, accessToken, refreshToken, expires: DateTime.Now.AddMonths(1));
        var session = new StravaSession(stravaAuthorization, httpClient);
        IStravaApi stravaApi = new StravaApiImpl(session, httpClient);
        // Simulate token expiry by setting Expires to past date
        typeof(StravaAuthorization)
            .GetProperty("Expires")!
            .SetValue(session.Authorization, DateTime.Now.AddMinutes(-1));
        // Act
        var result = await stravaApi.GetAthleteAsync(null, TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success, "API result indicates success.");
        Assert.IsNull(result.Data, "Deserialized data exists.");
        Assert.IsNotNull(result.Error, "API result fails to contain an error.");
        Assert.IsInstanceOfType<StravaException>(result.Error!.Exception, "Inner exception is not of type StravaException.");
    }

    [TestMethod]
    public async Task StravaApiImpl_GetStreamAsync_WithValidResponse_AndSuccessReauth_ShouldReturnStream()
    {
        // Arrange
        var authorization = new StravaAuthorization()
        {
            Id = 1234,
            ClientSecret = "client-secret",
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Expires = DateTime.UtcNow.AddMonths(-5),
        };

        var handler = new MockHttpMessageHandler() { JsonResponse = jsonRefresh };
        var client = new HttpClient(handler);
        var session = new StravaSession(authorization, client, useProvidedClientForRefresh: true);

        var stravaApi = session.StravaApi() as StravaApiImpl;
        var sampleContent = "Stream content after reauth";
        handler.JsonSecondaryResponse = sampleContent;

        // Act
        using var stream = await stravaApi!.GetStreamAsync(new Uri("http://www.example.com"), TestContext.CancellationToken);
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync(TestContext.CancellationToken);
        // Assert
        Assert.IsNotNull(stream, "Returned stream is null.");
        Assert.AreEqual(sampleContent, content, "Stream content does not match expected value.");
        Assert.IsTrue(session.IsAuthenticated, "Failed to properly refresh authentication.");

        // Second call should NOT require reauthentication
        using var stream2 = await stravaApi.GetStreamAsync(new Uri("http://www.example.com"), TestContext.CancellationToken);
        Assert.IsNotNull(stream2, "Returned stream is null.");
    }

    public TestContext TestContext { get; set; }

    private static readonly string jsonRefresh = @"{
  ""token_type"": ""Bearer"",
  ""access_token"": ""new-access-token"",
  ""expires_at"":2239469008,
  ""expires_in"":20566,
  ""refresh_token"":""new-refresh-token"",
  ""athlete"": {
    ""id"": 1234,
    ""username"": ""sampleuser"",
    ""resource_state"": 2
  }
}";


}

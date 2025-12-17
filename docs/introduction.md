# Introduction

**Tudormobile.Strava.*** provides client, service, and model libraries for developing clients and services that utilize the Strava V3 API.

## Overview
### Base API and object model
Using the basic *Tudormobile.Strava* API library begins by creating a Strava Session object, which is used to authenticate with the Strava API. Once authenticated, the session object is then used to create an instance of one or more *IStravaApi* interfaces. These interfaces provide methods for accessing the various endpoints of the Strava API. All methods are asynchronous.

The base interface (*IStravaApi*) has methods for GET, POST, and PUT requests which return a stream from the Strava V3 api. <u>It is the responsibility of the caller to dispose of the stream in these cases</u>. There are a few higher level calls that return *ApiResult*, such as returning the currently logged in Athlete (user). 

> [!NOTE] 
> The Athlete Id is also present in the Strava Authorization object of the current session. 

The base interface is designed for callers that wish to provide their own object model, modify the API surface, or include revisions to the API prior to support being provided by this library. Most users will use both the object model and one of the category interfaces described below.

- ***IActivitiesApi*** - Activities category
- ***IAthlete*** - Athletes category
- ***IClubsApi*** - Clubs category
- ***IGearsApi*** - Gears category
- ***IRoutesApi*** - Routes category
- ***ISegmentsApi*** Segments category
- ***IStreamsApi*** Streams category
- ***IUploadsApi*** Uploads category

All category API calls return and instance of ***ApiResult*** which encapsulates both the returned data and any errors that may have occurred. Resource managament is done by the library. Again, all method calls are asynchronous. Results data takes the form of one of the object model entities, which are designed to match the Strava V4 API specification. The model may be found in the *Tudormobile.Strava.Model* namespace, which contains objects used to represent the data returned by the API. All objects can be serialized/deserialized to/from json used by the API.

```cs
var client_id = "your_client_id";
var client_secret = "your_client_secret";
var access_token = "your_access_token";
var refresh_token = "your_refresh_token";

// Strava Authorization
var auth = new StravaAuthorization(
    client_id,          // Client Id
    client_secret,      // Client secret
    access_token,       // Current access token
    refresh_token);     // Current refresh token

var session = await new StravaSession(auth).RefreshTokensAsync();

var api = session.ActivitiesApi();
var reply = await api.GetActivitiesAsync(after: DateTime.Now.AddDays(-30));

if (reply.Success) 
{
    List<SummaryActvity> activities = reply.Data;   // List of SummaryActivity objects
    // ...
} 
else 
{
    ApiError error = reply.Error;
    // ...
}
```
The ApiResult object encapsulates error results and success results in the form of a model object (or collection of model objects). It is defined as follows:
```cs
// T is a model object or collection of model objects.
public class ApiResult<T>
{
    public T? Data { get; init; }
    public ApiError? Error { get; init; }
    public bool Success => Error == null;
    // ...
}
public class ApiError
{
    public string Message { get; init; }
    public Exception Exception { get; init; }
    public Fault Fault { get; init; }
    // ...
}
```
### Client API and object model
The *Tudormobile.Strava.Client* package and namespace contains classes for making HTTP requests to the Strava API via a ***StravaClient***. This package includes a dependency on *Tudormobile.Strava* as well as *Microsoft.Extensions.Http* and provides a builder pattern for configuration and construction via dependency injection, logging, and access to the Strava V3 api through a single client object. This allows you to supply your own serializers, Http client, loggers, etc. if desired.
```cs
// sample code will go here...
```
### Serivice API
 The *Tudormobile.Strava.Service* package and namespace contains classes for exposing a custom API that delegates, caches, or modifies the API provided by Strava allowing you to build custom applications or integrate Strava features into your own web services and applications.

 ### UI Elements
Finally, the *Tudormobile.Strava.UI* library provides a set of user interface components that can be used to display data from the Strava API in a user-friendly manner. This library is designed to work with the *Tudormobile.Strava* API library and can be used to create applications that provide a rich user experience for the Windows desktop platform. Additional platforms and frameworks are planned to be included in future releases in several different programming languages and platforms.

## Overview in more Detail
### Authorization with the Strava API
Create a new *StravaAuthorization* object to begin the authorization process. This object will contain your application client id and client secret. If this is a new authorization object, the remaining fields will be initialized to default values. You should persist and re-use the authorization object from previous sessions to avoid the user having to authenticate via the OAuth2 authorization flow every time. If a previous (existing) authorization object is used, the *AccessToken*, *RefreshToken*, and *Expires* properties will be valid. Use this object to create a new *StravaSession* object.
```cs
var auth = new StravaAuthorization()
{
    ClientId = your_client_id,
    ClientSecret = your_client_secret,
    RefreshToken = previous_refresh_token,  // if any
    AccessToken = previous_access_token,    // if any
};
var session = await new StravaSession(auth);
if (!_session.IsAuthenticated)
{
    var result = await _session.RefreshAsync();
}
```
The sessions object contains an updated authorization object with the new access token, refresh token, and expiration dates. Additionally, the *Id* field will be set to contain the current Strava API *athlete id*.
> [!NOTE]
> It is possible that the Id can change from the value provided in an existing authorization object. In this case, the user has chosen to login under a different account.

There is also a fluent-style API flow that can be used, as follows:
```cs
var session = await new StravaSesstion(auth).RefreshTokensAsync();
```
If the session is not successfully authenticated, the result object will contain error information. If the session is authenticated, the session object will contain the unique identifier new access token, refresh token, and expiration dates. The *StravaSession* object can be used to create an instance of the *IStravaApi* interface if desired. This interface provides methods for accessing the various endpoints of the Strava API.
> [!NOTE]
> You can force the user to re-authenticate by clearing the token properties, or setting the Expires property to a time in the past. This will cause the *RefreshAsync()* method (or *RefreshTokensAsync()* method) to force the user to re-authenticate via the OAuth2 authorization flow.

<small>[&#128462; Session and Authorization components](api/Tudormobile.Strava.StravaAuthorization.yml)</small>

### Working with the Strava API
The Strava API is partitioned into a number of components (categories), each of which is represented by an interface in the *Tudormobile.Strava.Api* namespace. These interfaces provide methods for accessing the various endpoints of the Strava API. The *StravaSession* object is the main entry point for accessing the Strava API, and it provides methods for accessing all of the component interfaces. For example, to access the activities api, use the *ActivitiesApi()* method of the *StravaSession* object. This property returns an instance of the *IActivitiesApi* interface, which provides methods for accessing the various endpoints of the activities API.
```cs
var api = session.ActivitiesApi();
var activities = await api.GetActivitiesAsync();
if (activities.Success)
{
    activities.Data?.ForEach(a => 
        {
            //... do something with 'a' ...
        });
}
```
This partitioning follows the design of the Strava API.
- Activities, IActivitiesApi = endpoints for activities
- Athletes, IAthletesApi = endpoints for athlete information
- Clubs, IClubsApi = endpoints for clubs
- Gears, IGearsApi = endpoints for gear
- Routes, IRoutesApi = endpoints for routes
- Segments, ISegmentsApi = endpoints for segment efforts
- Uploads, IUploadsApi = endpoints for uploading activities

The Strava documentation contains a more complete list of the available partitions, endpoints, and their parameters.

<small>[&#128462; API components](api/Tudormobile.Strava.Api.yml)</small>

### Strava API Object Model
The Stava object model classes in the *Tudormobile.Strava.Model* namespace provide an implementation of the Strava API object model following, for the most part, the documentation provided on the Strava website. In some cases, enumerations are introduced to provide a more dotnet-like experience. For example, the *ActivityType* enumeration is used to represent the various activity types supported by the Strava API. The object model classes are designed to be used with the API interfaces described above as well as the *Tudormobile.Strava.Client* and *Tudormobile.Strava.Service* libraries to facilitate the development of clients and services that utilize the Strava API. Date and time use the native DateTime type, rather than unix epoch seconds. In most cases, the provided object model omits some of the less useful properties, such as *resource_state*. Serialization to and from json is also provided for all the object model classes via the public *StravaSerializer* object.

<small>[&#128462; Object Model](api/Tudormobile.Strava.Model.yml)</small>

### Strava API Clients and Services
The *Strava.Client* library and the *StravaClient* class provides a higher level abstraction for working with HTTPS access to the Strava API from client applications.  

Similarly, the *Strava.Service* library and *StravaService* classes provide a higher level abstraction for creating web services that access the Strava API.  

These libraries add support for dependency injection, hosting, configuration and construction via builder patterns, and logging.

<small>[&#128462; Client Components](api/Tudormobile.Strava.Client.yml) | [&#128462; Service Components](api/Tudormobile.Strava.Service.yml)</small>

## UI Component Library
The *Tudormonbile.Strava.UI* library contains a set of user interface components that can be used to display data from the Strava API in a user-friendly manner. This library is designed to work with the *Tudormobile.Strava* API library and can be used to create applications that provide a rich user experience for the Windows desktop (WPF) platform. A collection of basic controls, pre-built views, and view models are provided to facilitate the development of Strava API clients. UI elements are also provided to facilitate login and authorization with the Strava API, including browser-based OAuth flow.

<small>[&#128462; UI Desktop Library](api/ui.md)</small>

[&#8962; Home](index.md) | [&#128640; Getting Started](getting-started.md) | [&#128462; API Documentation](api/tudormobile.md)
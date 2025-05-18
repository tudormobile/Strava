# Introduction

**Tudormobile.Strava.*** provides client, service, and model libraries for developing clients and services that utilize the Strava V3 API.

## Overview
Using the *Tudormobile.Strava* API library begins by creating a Strava Session object, which is used to authenticate with the Strava API. Once authenticated, the session object is then used to create an instance of the *IStravaApi* interface if desired. This interface provides methods for accessing the various endpoints of the Strava API. An object model contained in the *Tudormobile.Strava.Model* namespace is used to represent the data returned by the API.

The *Tudormobile.Strava.Client* namespace contains classes for making HTTP requests to the Strava API. The *Tudormobile.Strava.Service* namespace contains classes for handling the responses from the API and converting them into the appropriate object model. These classes can be used to facilitate the development of clients and services that utilize the Strava API.

Finally, the *Tudormobile.Strava.UI* library provides a set of user interface components that can be used to display data from the Strava API in a user-friendly manner. This library is designed to work with the *Tudormobile.Strava* API library and can be used to create applications that provide a rich user experience for the Windows desktop platform. Additional platorms and frameworks are planned to be included in future releases.

## Authorization with the Strava API
Create a new *StravaAuthorization* object to begin the authorization process. This object will contain your application cliend id and client secret. If this is a new authoriation object, the remaining fields will be initialized to default values. You should presist and re-use the authorization object from previous sessions to avoid the user having to authenticate via the OAuth2 authorization flow every time. If a previous (existing) authorization objet is used, the *AccessToken*, *RefreshToken*, and *Expires* properties will be valid. Use this object to create a new *StravaSession* object.
```
var auth = new StravaAuthorization()
{
    ClientId = your_client_id,
    ClientSecret = your_client_secret,
    RefreshToken = previous_refresh_token,  // if any
    AccessToken = previous_access_token,    // if any
};
var session = new StravaSession(auth);
if (!_session.IsAuthenticated)
{
    var result = await _session.RefreshAsync();
}
```
The sessions object contains an updated authorization object with the new access token, refresh token, and expiration dates. Additionally, the *Id* field will be set to contain the current Strava API *athlete id*.
> [!NOTE]
> It is possible that the Id can change from the value provded in an existing authorization object. In this case, the user has chosen to login under a different account.

There is also a fluent-style API flow that can be used, as follows:
```
var session = await new StravaSesstion(auth).RefreshTokens();
```
If the session is not successfully authenticated, the result object will contain error information. If the session is authenticated, the session object will contain the unique identifier new access token, refresh token, and expiration dates. The *StravaSession* object can be used to create an instance of the *IStravaApi* interface if desired. This interface provides methods for accessing the various endpoints of the Strava API.
> [!NOTE]
> You can force the user to re-authenticate by clearing the token properties, or setting the Expires property to a time in the past. This will cause the *RefreshAsync* method (or *RefreshTokens()* method) to force the user to re-authenticate via the OAuth2 authorization flow.

<small>[&#128462; Session and Authorization components](api/Tudormobile.Strava.StravaAuthorization.yml)</small>

## Working with the Strava API
The Strava API is partitioned into a number of components, each of which is represented by an interface in the *Tudormobile.Strava.Api* namespace. These interfaces provide methods for accessing the various endpoints of the Strava API. The *StravaSession* object is the main entry point for accessing the Strava API, and it provides methods for accessing all of the component interfaces. For example, to access the activities api, use the *ActivitiesApi* property of the *StravaSession* object. This property returns an instance of the *ActivitiesApi* interface, which provides methods for accessing the various endpoints of the activities API.
```
var api = session.ActivitiesApi();
var activities = await api.GetActivities();
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

## Strava API Object Model
The Stava object model classes in the *Tudormobile.Strava.Model* namespace provide an implementation of the Strava API object model following, for the most part, the documentation provided on the Strava website. In some cases, enumerations are introduced to provide a more dotnet-like experience. For example, the *ActivityType* enumeration is used to represent the various activity types supported by the Strava API. The object model classes are designed to be used with the *Tudormobile.Strava.Client* and *Tudormobile.Strava.Service* libraries to facilitate the development of clients and services that utilize the Strava API. Date and time use the native DateTime type, rather than unix epoch seconds. In most cases, the provided object model omits some of the less useful properties, such as *resource_state*. Serialization to and from json is also provided for all the object model classes.

<small>[&#128462; Object Model](api/Tudormobile.Strava.Model.yml)</small>

## Strava API Clients and Services
The *StravaClient* class provides a higher level abstraction for working with HTTPS access to the Strava API from client applications. This client wraps the various API interfaces provided by the library, and include enhanced error processing and serialization of model objects in a more "dotnet-like" manner.

Similarly, the *StravaService* class provides a higher level abstraction for creating web services that access the Strava API.  

<small>[&#128462; Client Components](api/Tudormobile.Strava.Client.yml) | [&#128462; Service Components](api/Tudormobile.Strava.Service.yml)</small>

## UI Component Library
The *Tudormonbile.Strava.UI* library contains a set of user interface components that can be used to display data from the Strava API in a user-friendly manner. This library is designed to work with the *Tudormobile.Strava* API library and can be used to create applications that provide a rich user experience for the Windows desktop (WPF) platform. A collection of basic controls, pre-built views, and view models are provided to facilitate the development of Strava API clients. UI elements are also provided to facilitate login and authorization with the Strava API, including browser-based OAuth flow.

<small>[&#128462; UI Desktop Library](api/ui.md)</small>

[&#8962; Home](index.md) | [&#128640; Getting Started](getting-started.md) | [&#128462; API Documentation](api/tudormobile.md)
# Strava API Library
[Source Code](https://github.com/tudormobile/Strava) | [Documentation](https://tudormobile.github.io/Strava/) | [API documentation](https://tudormobile.github.io/Strava/api/tudormobile.html)
## Getting Started
### Install the package
```
dotnet package add Tudormobile.Strava
-or-
dotnet package add Tudormobile.Strava.Client
dotnet package add Tudormobile.Strava.Service
dotnet package add Tudormobile.Strava.UI
```
### Dependencies
The base model package (*Tudormobile.Strava*) has no dependencies. The Client and Service model
includes dependency injection and logging features from *Microsoft.Extensions.Http*.

### Key Features
- Authentication and Authorization with the Strava V3 API
- Strava object model including serialization/deserialization
- Service component for hosting web applications and services
- Client component for authenticating with Strava and consuming the API
- UI elements for building GUI applications (windows).

### API Documentation
The API documentation can be found on *github pages* here - [API Documentation](https://tudormobile.github.io/Strava/).
### Sample Code
Code samples are located in the *samples* folder of the [github repository](https://github.com/tudormobile/Strava).
### Feedback
**Tudormobile.Strava** is released as open source under the MIT license. Bug reports can be submitted at the [the github repository](https://github.com/tudormobile/Strava).


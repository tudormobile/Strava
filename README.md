# Strava
Strava API libraries

[![Build and Deploy](https://github.com/tudormobile/Strava/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tudormobile/Strava/actions/workflows/dotnet.yml)  
[![Publish Docs](https://github.com/tudormobile/Strava/actions/workflows/docs.yml/badge.svg)](https://github.com/tudormobile/Strava/actions/workflows/docs.yml)  

> [!NOTE]  
> The current state of this library is experimental (v0.x). Public API surface and project structure will change.

### Quick Start
```
using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;

// Strava Authorization
var auth = new StravaAuthorization(
    client_id,          // Client Id
    client_secret,      // Client secret
    access_token,       // Current access token
    refresh_token);     // Current refresh token

var session = await new StravaSession(auth).RefreshTokens();

// Get user activities (last 30 days)
var api = session.ActivitiesApi();
var activities = await api.GetActivitiesAsync(after: DateTime.Now.AddDays(-30));
```

The Strava API, Client, and Service libraries provide support for developing clients and services that utilize the Strava V3 API in dotnet.

[Documentation](https://tudormobile.github.io/Strava/) | [API documentation](https://tudormobile.github.io/Strava/api/tudormobile.html) | [NuGet Package Readme](docs/README.md)
# Strava
Strava API libraries

[![Build and Deploy](https://github.com/tudormobile/Strava/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tudormobile/Strava/actions/workflows/dotnet.yml)  
[![Publish Docs](https://github.com/tudormobile/Strava/actions/workflows/docs.yml/badge.svg)](https://github.com/tudormobile/Strava/actions/workflows/docs.yml)  

> [!NOTE]  
> The current state is experimental (v0.X).

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
var activities = await api.GetActivities(after: DateTime.Now.AddDays(-30));
```

The Strava API, Client, Service, and Model libraries provide support for developing clients and services that utilize the Strava V3 API.

[Documentation](https://tudormobile.github.io/Strava/) | [API documentation](https://tudormobile.github.io/Strava/api/tudormobile.html) | [NuGet Package Readme](docs/README.md)
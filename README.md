# Strava
Strava API libraries and clients

[![Build and Deploy](https://github.com/tudormobile/Strava/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tudormobile/Strava/actions/workflows/dotnet.yml)  
[![Publish Docs](https://github.com/tudormobile/Strava/actions/workflows/docs.yml/badge.svg)](https://github.com/tudormobile/Strava/actions/workflows/docs.yml)  

Copyright&copy;Tudormobile  
> [!NOTE]  
> Th current state is experimental (initial v0.XXX draft state).

### Quick Start - Client
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

var session = new StravaSession(auth);

if (!session.IsAuthenticated)
{
    await session.RefreshAsync();
}

var result = _session.RefreshAsync();

// Get user activities (last 30 days)
var api = _session!.CreateApi();
var activities = await api.GetActivities(DateTime.Now, DateTime.Now.AddDays(-30));

Debug.WriteLine($"{activities.Count():N0} activities found.")'
```
### Quick Start - Client

```
using Tudormobile.Strava;
using Tudormobile.Strava.Client;
using Tudormobile.Strava.Model;

// add code here
```
### Quick Start - Service

```
using Tudormobile.Strava;
using Tudormobile.Strava.Service;
using Tudormobile.Strava.Model;

// add code here
```

The Strava API, Client, Service, and Model libraries provide support for developing clients and services that utilize the Strava V3 API.

[Documentation](docs/introduction.md) | [Source Code README](src/README.md) | [Tudormobile.Strava API Documentation](https://tudormobile.github.io/Strava/)
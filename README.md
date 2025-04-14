# Strava
Strava API libraries and clients

[ Workflow Badges go here ]

Note: This repo/code is experimental, incomplete, in its initial draft state.

### Quick Start - Client
```
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Client;
using Tudormobile.Strava.Model;

Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

var apiInstance = new ActivitiesApi();
var activities = apiInstance.GetActivities();

Debug.WriteLine($"{activities.Count():N0} activities.")'
```
### Quick Start - Service

```
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Service;
using Tudormobile.Strava.Model;

// add code here
```

The Strava API, Client, Service, and Model libraries provide support for developing clients and services that utilize the Strava V3 API.

[Documentation](docs/introduction.md) | [Source Code README](src/README.md) | [Tudormobile.Strava API Documentation](https://tudormobile.github.io/Strava/)
# Getting Started
## Install the package
```
dotnet package add Tudormobile.Strava
```
-or-
```
dotnet package add Tudormobile.Strava.UI
```
## Using the Library

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

var session = await new StravaSession(auth).RefreshTokensAsync();

// Get user activities
var api = session.ActivitiesApi();
var activities = await api.GetActivities();
```
### Key Features
- Authentication and Authorization with the Strava V3 API
- Strava object model including serialization/deserialization
- Service component for hosting web applications and services
- Client component for authenticating with Strava and consuming the API
- UI elements for building GUI applications (windows).
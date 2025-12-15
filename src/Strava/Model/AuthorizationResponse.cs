namespace Tudormobile.Strava.Model;

/*
{
  "token_type": "Bearer",
  "access_token": "a9b723...",
  "expires_at":1568775134,
  "expires_in":20566,
  "refresh_token":"b5c569..."
}
OR
{
  "token_type": "Bearer",
  "access_token": "a9b723...",
  "expires_at":1568775134,
  "expires_in":20566,
  "refresh_token":"b5c569...",
  "athlete": {
    #{summary athlete representation}
  }
}

*/

/// <summary>
/// Strava Authorization Response.
/// </summary>
internal class AuthorizationResponse
{
#pragma warning disable IDE1006 // Naming Styles
    public string? token_type { get; init; }
    public int? expires_at { get; init; }
    public int? expires_in { get; init; }
    public string? access_token { get; init; }
    public string? refresh_token { get; init; }
    public AthleteId? athlete { get; init; }
#pragma warning restore IDE1006 // Naming Styles
}



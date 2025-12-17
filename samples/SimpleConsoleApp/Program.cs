using Tudormobile.Strava;
using Tudormobile.Strava.Api;

namespace SimpleConsoleApp;

internal class Program
{
    static async Task Main()
    {
        Console.WriteLine("Hello, World!");

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
        var activities = await api.GetActivitiesAsync(after: DateTime.Now.AddDays(-30));

        Console.WriteLine("StravaSession:");
        Console.WriteLine($"  IsAuthenticated = {session.IsAuthenticated}");
        Console.WriteLine("GetActivitiesAsync():");
        Console.WriteLine($"  Success = {activities.Success}");
        Console.WriteLine($"  Error = {activities.Error?.Message}");
        Console.WriteLine($"  Data = {activities.Data?.Count ?? 0} activities found.\n");

        var data = activities.Data ?? [];
        foreach (var activity in data)
        {
<<<<<<< HEAD
            Console.WriteLine(activity.ToString());
=======
            Console.WriteLine(activity);
>>>>>>> origin/main
        }

    }
}

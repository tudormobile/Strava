using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Client;

namespace StravaConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var client_id = "your_client_id";
        var client_secret = "your_client_secret";
        var access_token = "your_access_token";
        var refresh_token = "your_refresh_token";

        // Use environment variables (if set) for sensitive tokens (NOT FOR PRODUCTION)
        client_id = Environment.GetEnvironmentVariable("STRAVA_CLIENT_ID") ?? client_id;
        client_secret = Environment.GetEnvironmentVariable("STRAVA_CLIENT_SECRET") ?? client_secret;
        access_token = Environment.GetEnvironmentVariable("STRAVA_ACCESS_TOKEN") ?? access_token;
        refresh_token = Environment.GetEnvironmentVariable("STRAVA_REFRESH_TOKEN") ?? refresh_token;

        // Setup Host with Strava Client
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services
            .AddStravaClient(options =>
            {
                options.ClientId = client_id;           // Client Id
                options.ClientSecret = client_secret;   // Client secret
                options.AccessToken = access_token;     // Current access token
                options.RefreshToken = refresh_token;   // Current refresh token
            })
            .AddStravaActivityRepository() // Optional: Add Strava Activity Repository
            .AddLogging(builder => builder.AddConsole());

        using IHost host = builder.Build();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        var client = host.Services.GetRequiredService<IStravaClient>();

        logger.LogInformation("Host configured and running.");

        var result = await client.RefreshAsync();
        if (result.Success)
        {
            logger.LogInformation("StravaClient authenticated successfully.");
            logger.LogInformation("Logged in Athlete: Username={athlete}, Name={first} {last}, Id={id}.", client.Athlete!.Username, client.Athlete.FirstName, client.Athlete.LastName, client.Athlete.Id);
            // WARNING: In production, should use encrypted storage
            // Environment variables are not secure for sensitive tokens. Use the USER level at a minimum.
            Environment.SetEnvironmentVariable("STRAVA_ACCESS_TOKEN", client.Session.Authorization.AccessToken, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable("STRAVA_REFRESH_TOKEN", client.Session.Authorization.RefreshToken, EnvironmentVariableTarget.User);
        }
        else
        {
            logger.LogError("StravaClient authentication failed.");
            return;
        }

        var athlete = client.Athlete!;
        var activities = await client.Session.ActivitiesApi().GetActivitiesAsync(after: DateTime.Now.AddYears(-1), perPage: 50, page: 1);

        Console.WriteLine("StravaClient:");
        Console.WriteLine($"  IsAuthenticated     = {client.IsAuthenticated}");
        Console.WriteLine($"  Name                = {athlete.FirstName} {athlete.LastName}");
        Console.WriteLine($"  Following/Followers = {athlete.FriendCount} / {athlete.FollowerCount}");
        Console.WriteLine("GetActivitiesAsync():");
        Console.WriteLine($"  Success = {activities.Success}");
        Console.WriteLine($"  Error   = {activities.Error?.Message}");
        Console.WriteLine($"   Data   = {activities.Data?.Count ?? 0} activities found.\n");

        var data = activities.Data ?? [];
        foreach (var line in data.FormatAsCsv())
        {
            Console.WriteLine(line);
        }

        var repo = host.Services.GetService<IStravaActivityRepository>();
        if (repo != null)
        {
            var list = (await repo.GetActivitiesAsync()).ToList();
            var first = list.FirstOrDefault();
            if (first != null)
            {
                Console.WriteLine(first.GetCsvHeaderLine());
                foreach (var item in list) Console.WriteLine(item.AsCsvString());
                if (repo.HasUnsavedChanges())
                {
                    await repo.SaveChangesAsync();
                    logger.LogInformation("StravaActivityRepository: Changes saved.");
                }
            }
        }

        Console.WriteLine("Done.");
    }
}

public static class TestFormatter
{
    public static IEnumerable<string> FormatAsCsvLines<T>(this IEnumerable<T> collection)
    {
        var props = typeof(T).GetProperties().OrderBy(p => p.Name).ToList();
        yield return string.Join(",", props.Select(p => p.Name));
        foreach (var item in collection)
        {
            var propValues = props.Select(p => p.GetValue(item)?.ToString() ?? "");
            yield return string.Join(",", propValues);
        }
    }
    public static string FormatAsCsv(this Tudormobile.Strava.Model.SummaryActivity activity)
        => string.Join(",",
            activity.Athlete.Id,
            activity.Id,
            activity.StartDate,
            activity.ElapsedTime,
            activity.MovingTime,
            activity.Distance,
            activity.TotalElevationGain,
            activity.AverageSpeed,
            activity.MaxSpeed,
            activity.Name,
            activity.Type,
            activity.SportType,
            activity.WorkoutType
        );
    public static IEnumerable<string> FormatAsCsv(this IEnumerable<Tudormobile.Strava.Model.SummaryActivity> activities)
    {
        yield return "AthleteId,Id,StartDate,ElapsedTime,MovingTime,Distance,TotalElevationGain,AverageSpeed,MaxSpeed,Name,Type,SportType,WorkoutType";
        foreach (var activity in activities)
        {
            yield return activity.FormatAsCsv();
        }
    }

}

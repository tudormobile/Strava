using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            .AddLogging(builder => builder.AddConsole());

        using IHost host = builder.Build();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        var client = host.Services.GetRequiredService<IStravaClient>();

        logger.LogInformation("Host configured and running.");

        var activities = await client.GetActivitiesAsync(after: DateTime.Now.AddDays(-30));

        Console.WriteLine("StravaClient:");
        Console.WriteLine($"  IsAuthenticated = {client.IsAuthenticated}");
        Console.WriteLine("GetActivitiesAsync():");
        Console.WriteLine($"  Success = {activities.Success}");
        Console.WriteLine($"  Error = {activities.Error?.Message}");
        Console.WriteLine($"  Data = {activities.Data?.Count ?? 0} activities found.\n");

        var data = activities.Data ?? [];
        foreach (var activity in data)
        {
            Console.WriteLine(activity);
        }
        Console.WriteLine("Done.");
    }
}

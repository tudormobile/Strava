using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tudormobile.Strava.Client;

/// <summary>
/// Provides extension methods for registering Strava client services with an <see cref="IServiceCollection"/>.
/// </summary>
/// <remarks>Use these extension methods to add and configure Strava API clients in ASP.NET Core dependency
/// injection containers. This enables applications to access athletic and fitness data from Strava using strongly-typed
/// services.</remarks>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Strava activity repository and its dependencies to the service collection.
    /// </summary>
    /// <remarks>Registers <see cref="IStravaActivityRepository"/> as a singleton service. Call this method
    /// during application startup to enable dependency injection for Strava activity data access.</remarks>
    /// <param name="services">The service collection to which the Strava activity repository will be added.</param>
    /// <returns>The same instance of <see cref="IServiceCollection"/> that was provided, to support method chaining.</returns>
    public static IServiceCollection AddStravaActivityRepository(
        this IServiceCollection services)
    {
        services.AddSingleton<IStravaActivityRepository, StravaActivityRepository>();
        services.AddSingleton<IStravaActivityRepositoryContext, StravaActivityRepositoryContext>();
        return services;
    }

    /// <summary>
    /// Adds Strava client services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configure">The action used to configure the <see cref="IStravaClientBuilder"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddStravaClient(
        this IServiceCollection services,
        Action<IStravaClientBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        // Create a builder to capture configuration
        var builder = new StravaClientBuilder();
        configure(builder);

        // Register HttpClient for StravaClient
        services.AddHttpClient(nameof(StravaClient));

        // Register IStravaClient with a factory that provides the API key
        services.AddScoped<IStravaClient>(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var logger = sp.GetRequiredService<ILogger<StravaClient>>();

            // Use the captured options from the builder
            return new StravaClient(httpClientFactory,
                logger,
                builder.Options);
        });

        return services;
    }
}

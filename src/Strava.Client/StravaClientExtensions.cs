namespace Tudormobile.Strava.Client;

/// <summary>
/// Provides extension methods for creating and configuring Strava client instances.
/// </summary>
public static class StravaClientExtensions
{
    /// <summary>
    /// Creates a new builder for configuring and constructing an instance of the Strava client.
    /// </summary>
    /// <remarks>Use the returned builder to specify configuration options before creating a Strava client.
    /// This method is the recommended entry point for constructing a new client instance.</remarks>
    /// <returns>An <see cref="IStravaClientBuilder"/> that can be used to configure and build a Strava client instance.</returns>
    public static IStravaClientBuilder GetBuilder() => new StravaClientBuilder();
}

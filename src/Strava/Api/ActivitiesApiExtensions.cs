using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Provides static helpers and extension methods for <see cref="IActivitiesApi"/>.
/// </summary>
public static class ActivitiesApiExtensions
{
    /// <summary>
    /// Creates an <see cref="HttpContent"/> instance for posting a new activity to the Strava API.
    /// </summary>
    /// <param name="name">The name of the activity. Required.</param>
    /// <param name="sportType">The sport type of the activity. Required.</param>
    /// <param name="startDateLocal">The local start date and time of the activity. Required.</param>
    /// <param name="elapsedTime">The elapsed time of the activity in seconds. Required.</param>
    /// <param name="type">The type of the activity (optional).</param>
    /// <param name="description">The description of the activity (optional).</param>
    /// <param name="distance">The distance of the activity in meters (optional).</param>
    /// <param name="trainer">Indicates if the activity was performed on a trainer (optional).</param>
    /// <param name="commute">Indicates if the activity was a commute (optional).</param>
    /// <returns>A <see cref="HttpContent"/> object containing the activity data.</returns>
    public static HttpContent CreateActivityPostContent(
        string name,
        SportTypes sportType,
        DateTime startDateLocal,
        long elapsedTime,
        string? type = null,
        string? description = null,
        double? distance = null,
        bool? trainer = null,
        bool? commute = null
        )
    {
        var content = new MultipartFormDataContent
        {
            { new StringContent(name), nameof(name) },
            { new StringContent(sportType.ToString()), "sport_type" },
            { new StringContent(startDateLocal.ToString("o")), "start_date_local" },
            { new StringContent(elapsedTime.ToString()), "elapsed_time" }
        }
        .AddOptionalContent(type, nameof(type))
        .AddOptionalContent(description, nameof(description))
        .AddOptionalContent(distance, nameof(distance))
        .AddOptionalContent(trainer, nameof(trainer))
        .AddOptionalContent(commute, nameof(commute));

        return content;
    }

    /// <summary>
    /// Adds an optional value as a <see cref="StringContent"/> to the <see cref="MultipartFormDataContent"/> if the value is not null.
    /// </summary>
    /// <typeparam name="T">The type of the value to add. Must be nullable.</typeparam>
    /// <param name="content">The <see cref="MultipartFormDataContent"/> to add the value to.</param>
    /// <param name="value">The value to add if not null.</param>
    /// <param name="name">The name of the form field.</param>
    /// <returns>The <see cref="MultipartFormDataContent"/> with the value added if not null.</returns>
    /// <remarks>
    /// Boolean values are converted to "1" for true and "0" for false.
    /// </remarks>
    public static MultipartFormDataContent AddOptionalContent<T>(this MultipartFormDataContent content, T? value, string name)
    {
        if (value != null)
        {
            if (value is bool boolValue)
            {
                content.Add(new StringContent(boolValue ? "1" : "0"), name);
                return content;
            }
            content.Add(new StringContent(value.ToString() ?? String.Empty), name);
        }
        return content;
    }

    /// <summary>
    /// Appends query parameters to a URI string.
    /// </summary>
    /// <param name="uriString">The base URI string to which query parameters will be added.</param>
    /// <param name="queryParameters">A collection of key-value pairs representing the query parameters to add. Values that are <c>null</c> are ignored.</param>
    /// <returns>The URI string with the appended query parameters.</returns>
    public static string AddQueryToUriString(string uriString, IEnumerable<(string, object?)> queryParameters)
    {
        var queryParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
        foreach (var (key, value) in queryParameters)
        {
            if (!string.IsNullOrWhiteSpace(value?.ToString()))
            {
                queryParams.Add(key, System.Web.HttpUtility.UrlEncode(value.ToString()));
            }
        }
        var query = queryParams.ToString()!;
        return uriString + (string.IsNullOrWhiteSpace(query) ? string.Empty : "?" + query);
    }
}

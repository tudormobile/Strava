using System.Text.Json;

namespace Tudormobile.Strava;

/// <summary>
/// Serializer for Tudormobile.Strava model objects.
/// </summary>
/// <remarks>
/// The serializer uses the <see cref="JsonSerializer"/> class to convert between JSON and model objects.
/// Serialization options include snake case (lower) property names and case insensitivity. Some specific
/// model objects may include custom serialization. Strava API dates and times are converted to DateTime objects.
/// </remarks>
public static class StravaSerializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Converts UTF8 json string to a model object.
    /// </summary>
    /// <typeparam name="T">Type of model object.</typeparam>
    /// <param name="json">UTF8 json string.</param>
    /// <param name="result">Resulting object if successful; otherwise (null).</param>
    /// <returns>True if successful; otherwise false.</returns>
    public static bool TryDeserialize<T>(string json, out T? result)
    {
        try
        {
            result = JsonSerializer.Deserialize<T>(json, _options);
            return true;
        }
        catch (JsonException) { }

        result = default;
        return false;
    }

    /// <summary>
    /// Converts a UTF8 json stream to a model object.
    /// </summary>
    /// <typeparam name="T">Type of model object.</typeparam>
    /// <param name="utf8Json">UTF8 json stream</param>
    /// <param name="result">Resulting object if successful; otherwise (null).</param>
    /// <returns>True if successful; otherwise false.</returns>
    public static bool TryDeserialize<T>(Stream utf8Json, out T? result)
        => TryDeserialize(utf8Json, out result, out _);

    /// <summary>
    /// Converts a UTF8 json stream to a model object.
    /// </summary>
    /// <typeparam name="T">Type of model object.</typeparam>
    /// <param name="utf8Json">UTF8 json stream</param>
    /// <param name="result">Resulting object if successful; otherwise includes exception.</param>
    /// <param name="exception">Resulting exception or (null) if no errors.</param>
    /// <returns>True if successful; otherwise false.</returns>
    public static bool TryDeserialize<T>(Stream utf8Json, out T? result, out JsonException? exception)
    {
        try
        {
            result = JsonSerializer.Deserialize<T>(utf8Json, _options);
            exception = null;
            return true;
        }
        catch (JsonException je)
        {
            result = default;
            exception = je;
        }
        return false;
    }
}


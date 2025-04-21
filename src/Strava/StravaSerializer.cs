using System.Text.Json;

namespace Tudormobile.Strava;

/// <summary>
/// Serializer for Strava model objects.
/// </summary>
public static class StravaSerializer
{
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

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
    {
        try
        {
            result = JsonSerializer.Deserialize<T>(utf8Json, _options);
            return true;
        }
        catch (JsonException) { }

        result = default;
        return false;
    }
}


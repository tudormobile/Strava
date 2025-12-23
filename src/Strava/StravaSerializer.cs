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
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters =
        {
            new Converters.ResourceStatesConverter(),
            new Converters.ActivityStatsConverter(),
            new Converters.TimeSpanConverter(),
            new Converters.FrameTypesConverter(),
            new Converters.SportTypesConverter(),
            new Converters.LatLngConverter(),
        }
    };

    /// <summary>
    /// Gets the default <see cref="JsonSerializerOptions"/> used for JSON serialization and deserialization.
    /// </summary>
    public static JsonSerializerOptions Options => _options;

    /// <summary>
    /// Asynchronously deserializes a UTF8 JSON stream to a model object.
    /// </summary>
    /// <typeparam name="T">Type of model object.</typeparam>
    /// <param name="stream">The input stream containing UTF8 JSON.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> representing the asynchronous operation. The result contains the deserialized object if successful; otherwise, (null).
    /// </returns>
    public static async ValueTask<T?> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken)
        => await JsonSerializer.DeserializeAsync<T>(stream, _options, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously serializes the specified value as UTF-8 encoded JSON and writes it to the provided stream.
    /// </summary>
    /// <remarks>The method does not close or flush the provided stream. The caller is responsible for
    /// managing the stream's lifetime.</remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="utf8Json">The stream to which the JSON data will be written. The stream must be writable and remain open for the duration
    /// of the operation.</param>
    /// <param name="value">The value to serialize. If <paramref name="value"/> is <see langword="null"/>, the JSON output will represent a
    /// null value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous serialization operation.</returns>
    public static async Task SerializeAsync<T>(Stream utf8Json, T? value, CancellationToken cancellationToken)
        => await JsonSerializer.SerializeAsync(utf8Json, value, _options, cancellationToken).ConfigureAwait(false);

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


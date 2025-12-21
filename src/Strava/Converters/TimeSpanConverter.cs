using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tudormobile.Strava.Converters;

/// <summary>
/// Converts a <see cref="TimeSpan"/> to and from its JSON representation as the total number of seconds.
/// </summary>
public class TimeSpanConverter : JsonConverter<TimeSpan>
{
    /// <summary>
    /// Reads a <see cref="TimeSpan"/> value from the JSON input as the total number of seconds.
    /// </summary>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
    /// <param name="typeToConvert">The type to convert (should be <see cref="TimeSpan"/>).</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <returns>The <see cref="TimeSpan"/> value represented by the number of seconds in the JSON.</returns>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number)
        {
            throw new JsonException("Expected Number token");
        }
        var seconds = reader.GetInt64();
        return TimeSpan.FromSeconds(seconds);
    }
    /// <summary>
    /// Writes a <see cref="TimeSpan"/> value as the total number of seconds to the JSON output.
    /// </summary>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to.</param>
    /// <param name="value">The <see cref="TimeSpan"/> value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue((long)value.TotalSeconds);
    }
}

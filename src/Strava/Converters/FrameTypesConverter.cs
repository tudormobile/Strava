using System.Text.Json;
using System.Text.Json.Serialization;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Converters;

/// <summary>
/// Custom JSON converter for the <see cref="Model.FrameTypes"/> enum.
/// </summary>
/// <remarks>
/// This converter handles serialization and deserialization of <see cref="Model.FrameTypes"/> enum values
/// to and from JSON. It supports both numeric (integer) and string representations of the enum values.
/// The Strava API typically returns sport_type as a string (e.g., "Road", "Mountain").
/// </remarks>

public class FrameTypesConverter : JsonConverter<FrameTypes>
{
    /// <summary>
    /// Reads and converts the JSON to a <see cref="FrameTypes"/> value.
    /// Handles both numeric and string representations of the enum.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The deserialized <see cref="FrameTypes"/> value.</returns>
    public override FrameTypes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            var value = reader.GetInt32();
            return Enum.TryParse<FrameTypes>(value.ToString(), out var result) && Enum.IsDefined<FrameTypes>(result) ? result : FrameTypes.None;
        }

        // Handle string representation as fallback
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            return Enum.TryParse<FrameTypes>(stringValue, ignoreCase: true, out var result) ? result : FrameTypes.None;
        }

        return FrameTypes.None;
    }

    /// <summary>
    /// Writes the <see cref="FrameTypes"/> value as a numeric value to JSON.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The <see cref="FrameTypes"/> value to write.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, FrameTypes value, JsonSerializerOptions options)
    {
        // Write only the numeric value, not a property name
        writer.WriteNumberValue((int)value);
    }
}

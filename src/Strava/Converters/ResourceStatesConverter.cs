using System.Text.Json;
using System.Text.Json.Serialization;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Converters;

/// <summary>
/// Custom JSON converter for the <see cref="ResourceStates"/> enum.
/// </summary>
/// <remarks>
/// This converter handles serialization and deserialization of <see cref="ResourceStates"/> enum values
/// to and from JSON. It supports both numeric (integer) and string representations of the enum values.
/// The Strava API typically returns resource_state as an integer (1, 2, or 3).
/// </remarks>
public class ResourceStatesConverter : JsonConverter<ResourceStates>
{
    /// <summary>
    /// Reads and converts the JSON to a <see cref="ResourceStates"/> enum value.
    /// </summary>
    /// <param name="reader">The reader to read JSON from.</param>
    /// <param name="typeToConvert">The type to convert (always <see cref="ResourceStates"/>).</param>
    /// <param name="options">Serializer options to use during conversion.</param>
    /// <returns>
    /// A <see cref="ResourceStates"/> enum value. Returns <see cref="ResourceStates.Unknown"/> 
    /// if the value cannot be parsed or is not recognized.
    /// </returns>
    /// <remarks>
    /// This method handles two JSON token types:
    /// <list type="bullet">
    /// <item><description>Number: Converts integers (0-3) to the corresponding enum value.</description></item>
    /// <item><description>String: Attempts to parse string representations (e.g., "Meta", "Summary", "Detail").</description></item>
    /// </list>
    /// </remarks>
    public override ResourceStates Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            var value = reader.GetInt32();
            return Enum.TryParse<ResourceStates>(value.ToString(), out var result) && Enum.IsDefined<ResourceStates>(result) ? result : ResourceStates.Unknown;
        }

        // Handle string representation as fallback
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            return Enum.TryParse<ResourceStates>(stringValue, ignoreCase: true, out var result) ? result : ResourceStates.Unknown;
        }

        return ResourceStates.Unknown;
    }

    /// <summary>
    /// Writes a <see cref="ResourceStates"/> enum value as JSON.
    /// </summary>
    /// <param name="writer">The writer to write JSON to.</param>
    /// <param name="value">The <see cref="ResourceStates"/> enum value to write.</param>
    /// <param name="options">Serializer options to use during conversion.</param>
    /// <remarks>
    /// This method writes the enum value as its numeric representation (0-3) to match 
    /// the format expected by the Strava API.
    /// </remarks>
    public override void Write(Utf8JsonWriter writer, ResourceStates value, JsonSerializerOptions options)
    {
        // Write only the numeric value, not a property name
        writer.WriteNumberValue((int)value);
    }
}

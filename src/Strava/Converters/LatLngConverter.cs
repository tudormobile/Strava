using System.Text.Json;
using System.Text.Json.Serialization;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Converters;

/// <summary>
/// Custom JSON converter for the <see cref="LatLng"/> struct.
/// </summary>
/// <remarks>
/// This converter handles serialization and deserialization of <see cref="LatLng"/> values
/// to and from JSON arrays of two floating-point numbers representing [latitude, longitude].
/// For example: [37.8280722, -122.4981393]
/// </remarks>
public class LatLngConverter : JsonConverter<LatLng>
{
    /// <summary>
    /// Reads and converts the JSON array to a <see cref="LatLng"/> value.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The deserialized <see cref="LatLng"/> value.</returns>
    public override LatLng Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return default;
        }

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException($"Expected StartArray token, got {reader.TokenType}");
        }

        reader.Read();
        if (reader.TokenType != JsonTokenType.Number)
        {
            throw new JsonException($"Expected Number token for latitude, got {reader.TokenType}");
        }
        var latitude = reader.GetDouble();

        reader.Read();
        if (reader.TokenType != JsonTokenType.Number)
        {
            throw new JsonException($"Expected Number token for longitude, got {reader.TokenType}");
        }
        var longitude = reader.GetDouble();

        reader.Read();
        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException($"Expected EndArray token, got {reader.TokenType}");
        }

        return new LatLng
        {
            Latitude = latitude,
            Longitude = longitude
        };
    }

    /// <summary>
    /// Writes the <see cref="LatLng"/> value as a JSON array.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The <see cref="LatLng"/> value to write.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, LatLng value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Latitude);
        writer.WriteNumberValue(value.Longitude);
        writer.WriteEndArray();
    }
}

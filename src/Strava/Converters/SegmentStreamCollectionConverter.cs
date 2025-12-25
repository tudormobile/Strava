using System.Text.Json;
using System.Text.Json.Serialization;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Converters;

/// <summary>
/// Converts <see cref="SegmentStreamCollection"/> objects to and from JSON using custom serialization logic.
/// </summary>
/// <remarks>
/// This converter enables serialization and deserialization of <see cref="SegmentStreamCollection"/> instances when
/// working with System.Text.Json. It inspects the "type" property of each stream object in the JSON array to determine
/// whether to deserialize as a <see cref="SegmentStream"/> (when "type" is "latlng") or as a <see cref="SegmentEffortStream"/>.
/// Register this converter with a <see cref="JsonSerializerOptions"/> instance to ensure correct handling of
/// <see cref="SegmentStreamCollection"/> types during JSON operations.
/// </remarks>
public class SegmentStreamCollectionConverter : JsonConverter<SegmentStreamCollection>
{
    /// <summary>
    /// Reads and converts the JSON array to a <see cref="SegmentStreamCollection"/> instance.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The deserialized <see cref="SegmentStreamCollection"/>.</returns>
    /// <exception cref="JsonException">Thrown if the JSON is not in the expected format.</exception>
    public override SegmentStreamCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array for SegmentStreamCollection.");
        }

        var collection = new SegmentStreamCollection();

        // Read each element in the array
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                break;

            // Clone the reader to peek at the "type" property
            var elementReader = reader;

            if (elementReader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected start of object in SegmentStreamCollection array.");

            // Read the object to find the "type" property
            string? typeValue = null;
            using (var doc = JsonDocument.ParseValue(ref elementReader))
            {
                if (doc.RootElement.TryGetProperty("type", out var typeProp))
                {
                    typeValue = typeProp.GetString();
                }
            }

            // Deserialize based on the "type" property
            if (string.Equals(typeValue, "latlng", StringComparison.OrdinalIgnoreCase))
            {
                var segmentStream = JsonSerializer.Deserialize<SegmentStream>(ref reader, options);
                if (segmentStream != null)
                    collection.Add(segmentStream);
            }
            else
            {
                var effortStream = JsonSerializer.Deserialize<SegmentEffortStream>(ref reader, options);
                if (effortStream != null)
                    collection.Add(effortStream);
            }
        }

        return collection;
    }

    /// <summary>
    /// Writes a <see cref="SegmentStreamCollection"/> instance to JSON.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The <see cref="SegmentStreamCollection"/> value to write.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, SegmentStreamCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var stream in value)
        {
            if (stream is SegmentStream segmentStream)
            {
                JsonSerializer.Serialize(writer, segmentStream, options);
            }
            else if (stream is SegmentEffortStream effortStream)
            {
                JsonSerializer.Serialize(writer, effortStream, options);
            }
            else
            {
                // Fallback: serialize as base type
                JsonSerializer.Serialize(writer, stream, stream.GetType(), options);
            }
        }
        writer.WriteEndArray();
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Converters;

/// <summary>
/// Custom JSON converter for the <see cref="ActivityStats"/> class.
/// </summary>
/// <remarks>
/// This converter handles special deserialization logic for activity statistics from the Strava API.
/// The Strava API sometimes returns empty strings ("") instead of null for missing activity totals,
/// which requires custom handling to properly deserialize into nullable <see cref="ActivityTotal"/> objects.
/// </remarks>
public class ActivityStatsConverter : JsonConverter<ActivityStats>
{
    /// <summary>
    /// Reads and converts the JSON to an <see cref="ActivityStats"/> object.
    /// </summary>
    /// <param name="reader">The reader to read JSON from.</param>
    /// <param name="typeToConvert">The type to convert (always <see cref="ActivityStats"/>).</param>
    /// <param name="options">Serializer options to use during conversion.</param>
    /// <returns>
    /// An <see cref="ActivityStats"/> object with properly deserialized activity totals.
    /// </returns>
    /// <remarks>
    /// This method handles the Strava API's quirk of returning empty strings ("") for null activity totals.
    /// It converts these empty strings to null values for the corresponding <see cref="ActivityTotal"/> properties.
    /// </remarks>
    public override ActivityStats? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token");
        }

        var stats = new ActivityStats();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return stats;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException("Expected PropertyName token");
            }

            var propertyName = reader.GetString();
            reader.Read();

            switch (propertyName)
            {
                case "biggest_ride_distance":
                    stats.BiggestRideDistance = reader.GetSingle();
                    break;
                case "biggest_climb_elevation_gain":
                    stats.BiggestClimbElevationGain = reader.GetSingle();
                    break;
                case "recent_ride_totals":
                    stats.RecentRideTotals = ReadActivityTotal(ref reader, options);
                    break;
                case "recent_run_totals":
                    stats.RecentRunTotals = ReadActivityTotal(ref reader, options);
                    break;
                case "recent_swim_totals":
                    stats.RecentSwimTotals = ReadActivityTotal(ref reader, options);
                    break;
                case "ytd_ride_totals":
                    stats.YtdRideTotals = ReadActivityTotal(ref reader, options);
                    break;
                case "ytd_run_totals":
                    stats.YtdRunTotals = ReadActivityTotal(ref reader, options);
                    break;
                case "ytd_swim_totals":
                    stats.YtdSwimTotals = ReadActivityTotal(ref reader, options);
                    break;
                case "all_ride_totals":
                    stats.AllRideTotals = ReadActivityTotal(ref reader, options);
                    break;
                case "all_run_totals":
                    stats.AllRunTotals = ReadActivityTotal(ref reader, options);
                    break;
                case "all_swim_totals":
                    stats.AllSwimTotals = ReadActivityTotal(ref reader, options);
                    break;
                default:
                    reader.Skip();
                    break;
            }
        }

        throw new JsonException("Expected EndObject token");
    }

    /// <summary>
    /// Reads an <see cref="ActivityTotal"/> object from JSON, handling empty strings as null.
    /// </summary>
    /// <param name="reader">The UTF8 JSON reader.</param>
    /// <param name="options">Serializer options.</param>
    /// <returns>An <see cref="ActivityTotal"/> object, or null if the value is an empty string.</returns>
    private static ActivityTotal? ReadActivityTotal(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        // Handle empty string as null
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrEmpty(stringValue))
            {
                return null;
            }
            throw new JsonException($"Expected object or empty string, got non-empty string: {stringValue}");
        }

        // Handle null
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        // Handle object
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            return JsonSerializer.Deserialize<ActivityTotal>(ref reader, options);
        }

        throw new JsonException($"Expected object, null, or empty string for ActivityTotal, got {reader.TokenType}");
    }

    /// <summary>
    /// Writes an <see cref="ActivityStats"/> object as JSON.
    /// </summary>
    /// <param name="writer">The writer to write JSON to.</param>
    /// <param name="value">The <see cref="ActivityStats"/> object to write.</param>
    /// <param name="options">Serializer options to use during conversion.</param>
    /// <remarks>
    /// This method serializes an <see cref="ActivityStats"/> object to JSON format compatible with the Strava API.
    /// Null <see cref="ActivityTotal"/> properties are written as empty strings to match Strava API format.
    /// </remarks>
    public override void Write(Utf8JsonWriter writer, ActivityStats value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteNumber("biggest_ride_distance", value.BiggestRideDistance);
        writer.WriteNumber("biggest_climb_elevation_gain", value.BiggestClimbElevationGain);

        WriteActivityTotal(writer, "recent_ride_totals", value.RecentRideTotals, options);
        WriteActivityTotal(writer, "recent_run_totals", value.RecentRunTotals, options);
        WriteActivityTotal(writer, "recent_swim_totals", value.RecentSwimTotals, options);
        WriteActivityTotal(writer, "ytd_ride_totals", value.YtdRideTotals, options);
        WriteActivityTotal(writer, "ytd_run_totals", value.YtdRunTotals, options);
        WriteActivityTotal(writer, "ytd_swim_totals", value.YtdSwimTotals, options);
        WriteActivityTotal(writer, "all_ride_totals", value.AllRideTotals, options);
        WriteActivityTotal(writer, "all_run_totals", value.AllRunTotals, options);
        WriteActivityTotal(writer, "all_swim_totals", value.AllSwimTotals, options);

        writer.WriteEndObject();
    }

    /// <summary>
    /// Writes an <see cref="ActivityTotal"/> property to JSON, writing empty string for null values.
    /// </summary>
    /// <param name="writer">The UTF8 JSON writer.</param>
    /// <param name="propertyName">The property name to write.</param>
    /// <param name="value">The activity total value, or null.</param>
    /// <param name="options">Serializer options.</param>
    private static void WriteActivityTotal(Utf8JsonWriter writer, string propertyName, ActivityTotal? value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(propertyName);

        if (value == null)
        {
            // Write empty string to match Strava API format
            writer.WriteStringValue(string.Empty);
        }
        else
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}

using System.Text.Json.Serialization;

namespace Tudormobile.Strava.Model;

/// <summary>
/// Strava Athlete Identifier.
/// </summary>
public class AthleteId()
{
    /// <summary>
    /// Unique identifer of the Athlete.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary>
    /// Resource state.
    /// </summary>
    [JsonPropertyName("resource_state")]
    public long ResourceState { get; init; }

    /// <summary>
    /// Create athlete identifier from json record.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static AthleteId FromJson(string json)
    {
        var a = Athlete.FromJson(json);
        if (a != null)
        {
            return new AthleteId() { Id = a.Id, ResourceState = a.ResourceState };
        }
        return new AthleteId();
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


// here is the bad authorization response.
/*
{"message":"Bad Request","errors":[{"resource":"Application","field":"client_id","code":"invalid"}]}
*/


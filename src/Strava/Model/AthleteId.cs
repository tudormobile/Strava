namespace Tudormobile.Strava.Model;

/// <summary>
/// Strava Athlete Identifier.
/// </summary>
public class AthleteId()
{
    /// <summary>
    /// Unique identifer of the Athlete.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Resource state.
    /// </summary>
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
            return new AthleteId() { Id = a.Id, ResourceState = (long)a.ResourceState };
        }
        return new AthleteId();
    }
}

/* ref: bad authorization response.
{
    "message":"Bad Request",
    "errors":[
        {"resource":"Application",
        "field":"client_id","code":"invalid"}
    ]
}
*/


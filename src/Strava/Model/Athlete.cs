using System.Text.Json;

namespace Tudormobile.Strava.Model;

/// <summary>
/// A Strava user, a.k.a "Athlete".
/// </summary>
public class Athlete
{
    private _athleteRecord _athlete;

    /// <summary>
    /// The unique identifier of the athlete.
    /// </summary>
    public long Id => _athlete.id;

    /// <summary>
    /// Resource state, indicates level of detail. Possible values: 1 -> "meta", 2 -> "summary", 3 -> "detail"
    /// </summary>
    public ResourceStates ResourceState => (ResourceStates)_athlete.resource_state;

    /// <summary>
    /// The username of the athlete.
    /// </summary>
    public string Username => _athlete.username!;

    /// <summary>
    /// The athlete's first name.
    /// </summary>
    public string FirstName => _athlete.firstname!;

    /// <summary>
    /// The athlete's last name.
    /// </summary>
    public string LastName => _athlete.lastname!;

    /// <summary>
    /// Friend count of the athlete, i.e., number of people the athlete is following.
    /// </summary>
    public int FriendCount => _athlete.friend_count ?? 0;

    /// <summary>
    /// Follower count of the athlete, i.e., number of people following the athlete.
    /// </summary>
    public int FollowerCount => _athlete.follower_count ?? 0;

    private Athlete(_athleteRecord athlete)
    {
        _athlete = athlete;
    }

    /// <summary>
    /// Create and Athlete from Json data.
    /// </summary>
    /// <param name="json">Json text representing the athlete.</param>
    /// <returns>A new Athlete object.</returns>
    public static Athlete? FromJson(string json)
    {
        if (StravaSerializer.TryDeserialize(json, out _athleteRecord? record))
        {
            return new Athlete(record!);
        }
        return null;
    }

    /// <summary>
    /// UTF8 json string representing the athlete.
    /// </summary>
    /// <returns>Json representation of the athlete.</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(_athlete);
    }

    /// <summary>
    /// Create an empty athlete object.
    /// </summary>
    /// <returns>Empty Athlete object.</returns>
    public static Athlete Empty()
    {
        return new Athlete(new _athleteRecord());
    }
}


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
internal class _athleteRecord
{
    public long id { get; set; }
    public int resource_state { get; set; }
    public string? username { get; set; }
    public string? firstname { get; set; }
    public string? lastname { get; set; }
    public string? city { get; set; }
    public string? state { get; set; }
    public string? country { get; set; }
    public string? sex { get; set; }
    public bool? premium { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    public int? badge_type_id { get; set; }
    public string? profile_medium { get; set; }
    public string? profile { get; set; }
    public object? friend { get; set; }
    public object? follower { get; set; }
    public int? follower_count { get; set; }
    public int? friend_count { get; set; }
    public int? mutual_friend_count { get; set; }
    public int? athlete_type { get; set; }
    public string? date_preference { get; set; }
    public string? measurement_preference { get; set; }
    public object[]? clubs { get; set; }
    public object? ftp { get; set; }
    public int? weight { get; set; }
    public _bikeRecord[]? bikes { get; set; }
    public _shoRecord[]? shoes { get; set; }
}

internal class _bikeRecord
{
    public string id { get; set; }
    public bool primary { get; set; }
    public string name { get; set; }
    public int resource_state { get; set; }
    public int distance { get; set; }
}

internal class _shoRecord
{
    public string id { get; set; }
    public bool primary { get; set; }
    public string name { get; set; }
    public int resource_state { get; set; }
    public int distance { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


// here is the bad authorization response.
/*
{"message":"Bad Request","errors":[{"resource":"Application","field":"client_id","code":"invalid"}]}
*/


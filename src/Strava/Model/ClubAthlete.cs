namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents an athlete who is a member of a Strava club.
/// </summary>
/// <remarks>
/// This model contains information about a club member including their name,
/// membership type, and administrative roles within the club. This is typically
/// returned when requesting club member lists from the Clubs API.
/// </remarks>
public class ClubAthlete
{
    /// <summary>
    /// Resource state, indicates level of detail. Possible values: 1 -> "meta", 2 -> "summary", 3 -> "detail".
    /// </summary>
    public ResourceStates ResourceState { get; set; }

    /// <summary>
    /// The athlete's first name.
    /// </summary>
    public string Firstname { get; set; } = string.Empty;

    /// <summary>
    /// The athlete's last name.
    /// </summary>
    public string Lastname { get; set; } = string.Empty;

    /// <summary>
    /// The membership status or type of the athlete in the club.
    /// </summary>
    public string Membership { get; set; } = "Unknown";

    /// <summary>
    /// Indicates whether the athlete is an administrator of the club.
    /// </summary>
    public bool Admin { get; set; }

    /// <summary>
    /// Indicates whether the athlete is the owner of the club.
    /// </summary>
    public bool Owner { get; set; }
}

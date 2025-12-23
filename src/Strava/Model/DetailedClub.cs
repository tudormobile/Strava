namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents detailed information about a Strava club.
/// </summary>
public class DetailedClub
{
    /// <summary>
    /// Gets or sets the unique identifier for the club.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the resource state indicating the level of detail in this representation.
    /// </summary>
    public ResourceStates ResourceState { get; set; }

    /// <summary>
    /// Gets or sets the name of the club.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL to the club's medium-sized profile picture.
    /// </summary>
    public string? ProfileMedium { get; set; }

    /// <summary>
    /// Gets or sets the URL to the club's full-sized profile picture.
    /// </summary>
    public string? Profile { get; set; }

    /// <summary>
    /// Gets or sets the URL to the club's cover photo.
    /// </summary>
    public string? CoverPhoto { get; set; }

    /// <summary>
    /// Gets or sets the URL to the club's small cover photo.
    /// </summary>
    public string? CoverPhotoSmall { get; set; }

    /// <summary>
    /// Gets or sets the primary sport type for the club.
    /// </summary>
    public SportTypes SportType { get; set; }

    /// <summary>
    /// Gets or sets the list of activity types associated with the club.
    /// </summary>
    public List<string>? ActivityTypes { get; set; }

    /// <summary>
    /// Gets or sets the city where the club is located.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the state or region where the club is located.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the country where the club is located.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the club is private.
    /// </summary>
    public bool Private { get; set; }

    /// <summary>
    /// Gets or sets the total number of members in the club.
    /// </summary>
    public int MemberCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the club is featured by Strava.
    /// </summary>
    public bool Featured { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the club is verified by Strava.
    /// </summary>
    public bool Verified { get; set; }

    /// <summary>
    /// Gets or sets the URL to the club's page on Strava.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the membership status of the current user (e.g., "member", "pending").
    /// </summary>
    public string Membership { get; set; } = "Unknown";

    /// <summary>
    /// Gets or sets a value indicating whether the current user is an admin of the club.
    /// </summary>
    public bool Admin { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the current user is the owner of the club.
    /// </summary>
    public bool Owner { get; set; }

    /// <summary>
    /// Gets or sets the description of the club.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of club (e.g., "casual_club", "racing_team", "shop", "company", "other").
    /// </summary>
    public string ClubType { get; set; } = "Other";

    /// <summary>
    /// Gets or sets the total number of posts in the club.
    /// </summary>
    public int PostCount { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the club owner.
    /// </summary>
    public int OwnerId { get; set; }

    /// <summary>
    /// Gets or sets the number of athletes the club is following.
    /// </summary>
    public int FollowingCount { get; set; }
}

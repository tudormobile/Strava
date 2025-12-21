namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a summary of a Strava club.
/// </summary>
public class SummaryClub
{
    /// <summary>
    /// The club's unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The name of the club.
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// Resource state, indicates level of detail.
    /// </summary>
    public ResourceStates ResourceState { get; set; }

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
    /// Gets or sets the URL to the club's small-sized cover photo.
    /// </summary>
    public string? CoverPhotoSmall { get; set; }

    /// <summary>
    /// Gets or sets the primary sport type for the club.
    /// </summary>
    public SportTypes SportType { get; set; }

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
    /// Gets or sets the number of members in the club.
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
}

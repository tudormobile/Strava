namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a detailed athlete profile with complete information about a Strava user.
/// </summary>
/// <remarks>
/// This model contains comprehensive athlete information including profile details, preferences,
/// statistics, and associated gear. This is typically returned when requesting full athlete details
/// with resource_state = 3.
/// </remarks>
public class DetailedAthlete
{
    /// <summary>
    /// The unique identifier of the athlete.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The athlete's username.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Resource state, indicates level of detail. Possible values: 1 -> "meta", 2 -> "summary", 3 -> "detail".
    /// </summary>
    public ResourceStates ResourceState { get; set; }

    /// <summary>
    /// The athlete's first name.
    /// </summary>
    public string? Firstname { get; set; }

    /// <summary>
    /// The athlete's last name.
    /// </summary>
    public string? Lastname { get; set; }

    /// <summary>
    /// The athlete's city.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// The athlete's state or geographical region.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// The athlete's country.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// The athlete's sex. Possible values: 'M' for male, 'F' for female.
    /// </summary>
    public string? Sex { get; set; }

    /// <summary>
    /// Whether the athlete is a Strava premium (subscription) member.
    /// </summary>
    public bool Premium { get; set; }

    /// <summary>
    /// The time at which the athlete was created (joined Strava).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The time at which the athlete profile was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// The athlete's badge type ID, indicating special status or achievements.
    /// </summary>
    public int BadgeTypeId { get; set; }

    /// <summary>
    /// URL to a 62x62 pixel profile picture.
    /// </summary>
    public string? ProfileMedium { get; set; }

    /// <summary>
    /// URL to a 124x124 pixel profile picture.
    /// </summary>
    public string? Profile { get; set; }

    /// <summary>
    /// The athlete's follower count.
    /// </summary>
    public int FollowerCount { get; set; }

    /// <summary>
    /// The athlete's friend count (athletes the user is following).
    /// </summary>
    public int FriendCount { get; set; }

    /// <summary>
    /// The number of athletes both the authenticated user and this athlete are following.
    /// </summary>
    public int MutualFriendCount { get; set; }

    /// <summary>
    /// The athlete type. Possible values: 0 -> cyclist, 1 -> runner.
    /// </summary>
    public int AthleteType { get; set; }

    /// <summary>
    /// The athlete's date preference format (e.g., "%m/%d/%Y").
    /// </summary>
    public string? DatePreference { get; set; }

    /// <summary>
    /// The athlete's measurement preference. Possible values: 'feet' or 'meters'.
    /// </summary>
    public string? MeasurementPreference { get; set; }

    /// <summary>
    /// The athlete's clubs. An array of <see cref="SummaryClub"/> objects.
    /// </summary>
    public SummaryClub[] Clubs { get; set; } = [];

    /// <summary>
    /// The athlete's FTP (Functional Threshold Power) in watts. May be null if not set.
    /// </summary>
    public double? Ftp { get; set; }

    /// <summary>
    /// The athlete's weight in kilograms.
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// The athlete's bikes. An array of <see cref="Bike"/> objects.
    /// </summary>
    public Bike[] Bikes { get; set; } = [];

    /// <summary>
    /// The athlete's shoes. An array of <see cref="Shoe"/> objects.
    /// </summary>
    public Shoe[] Shoes { get; set; } = [];
}

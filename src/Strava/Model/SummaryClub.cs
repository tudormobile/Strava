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
    public string? Name { get; set; }

    /// <summary>
    /// Resource state, indicates level of detail.
    /// </summary>
    public ResourceStates ResourceState { get; set; }
}

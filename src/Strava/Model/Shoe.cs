namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a summary of shoes (running/walking gear) owned by an athlete.
/// </summary>
public class Shoe
{
    /// <summary>
    /// The gear's unique identifier.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Whether this is the athlete's default (primary) shoes.
    /// </summary>
    public bool Primary { get; set; }

    /// <summary>
    /// The name of the shoes.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Resource state, indicates level of detail.
    /// </summary>
    public ResourceStates ResourceState { get; set; }

    /// <summary>
    /// The total distance logged with these shoes, in meters.
    /// </summary>
    public int Distance { get; set; }
}

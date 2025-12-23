namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a polyline map with basic identification information.
/// </summary>
/// <remarks>
/// A polyline is an encoded representation of a geographic path or route.
/// </remarks>
public class PolylineMap
{
    /// <summary>
    /// Gets or sets the unique identifier of the map.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the resource state indicating the level of detail available for this map.
    /// </summary>
    public ResourceStates ResourceState { get; set; }
}

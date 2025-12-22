namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents detailed information about a piece of gear (equipment) used in Strava activities.
/// </summary>
public class DetailedGear
{
    private string? _name;

    /// <summary>
    /// Gets or sets the unique identifier for the gear.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the gear.
    /// If no custom name is set, returns a combination of the brand name and model name when both are available.
    /// </summary>
    public string Name
    {
        get => _name ?? (BrandName is not null && ModelName is not null ? string.Join("-", BrandName, ModelName) : $"{BrandName}{ModelName}");
        set { _name = value.Trim(); }
    }

    /// <summary>
    /// Gets or sets the description of the gear.
    /// </summary>
    public string Description { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the resource state indicating the level of detail available for this gear.
    /// </summary>
    public ResourceStates ResourceState { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is the primary gear of its type.
    /// </summary>
    public bool Primary { get; set; }

    /// <summary>
    /// Gets or sets the total distance traveled with this gear, measured in meters.
    /// </summary>
    public int Distance { get; set; }

    /// <summary>
    /// Gets or sets the brand name of the gear.
    /// </summary>
    public string? BrandName { get; set; }

    /// <summary>
    /// Gets or sets the model name of the gear.
    /// </summary>
    public string? ModelName { get; set; }

    /// <summary>
    /// Gets or sets the frame type of the gear.
    /// </summary>
    public FrameTypes FrameType { get; set; }
}

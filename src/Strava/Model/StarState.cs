namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents the starred state of an entity.
/// </summary>
public record StarState
{
    /// <summary>
    /// Initializes a new instance of the StarState class with the specified starred state.
    /// </summary>
    /// <param name="isStarred">A value indicating whether the item is initially starred. Set to <see langword="true"/> to mark the item as
    /// starred; otherwise, <see langword="false"/>.</param>
    public StarState(bool isStarred) { Starred = isStarred; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is starred.
    /// </summary>
    public bool Starred { get; set; }
}

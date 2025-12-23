namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a summary polyline map with simplified route encoding.
/// </summary>
/// <remarks>
/// Extends <see cref="PolylineMap"/> with a simplified polyline encoding suitable for overview display.
/// </remarks>
public class SummaryPolylineMap : PolylineMap
{
    /// <summary>
    /// Gets or sets the encoded summary polyline string representing a simplified route.
    /// </summary>
    /// <remarks>
    /// The summary polyline is a lower-resolution version of the route, useful for displaying
    /// an overview without the full detail of the complete polyline.
    /// </remarks>
    public string SummaryPolyline { get; set; } = string.Empty;
}

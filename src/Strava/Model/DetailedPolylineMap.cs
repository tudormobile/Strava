namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a detailed polyline map with full route encoding.
/// </summary>
/// <remarks>
/// Extends <see cref="PolylineMap"/> with a detailed polyline encoding of the complete route.
/// </remarks>
public class DetailedPolylineMap : PolylineMap
{
    /// <summary>
    /// Gets or sets the encoded polyline string representing the detailed route.
    /// </summary>
    /// <remarks>
    /// The polyline is encoded using Google's polyline encoding algorithm.
    /// This provides a high-resolution representation of the geographic path.
    /// </remarks>
    public string Polyline { get; set; } = string.Empty;
}

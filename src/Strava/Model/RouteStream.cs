namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a stream of geographic route points as a collection of latitude and longitude coordinates.
/// </summary>
/// <remarks>Use this class to store or manipulate a sequence of route points, typically for mapping or navigation
/// scenarios. Inherits from StreamBase, which may provide additional stream-related functionality.</remarks>
public class RouteStream : StreamBase
{
    /// <summary>
    /// Gets or sets the collection of geographic coordinates represented as latitude and longitude pairs.
    /// </summary>
    public List<LatLng> Data { get; set; } = [];
}
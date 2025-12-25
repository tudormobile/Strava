namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a stream of geographic coordinates (latitude/longitude pairs) for a segment in Strava.
/// </summary>
/// <remarks>
/// Inherits common stream metadata from <see cref="StreamBase"/>. The <see cref="Data"/> property contains
/// the time-series of <see cref="LatLng"/> points that define the segment's path.
/// </remarks>
public class SegmentStream : StreamBase
{
    /// <summary>
    /// Gets or sets the list of latitude/longitude coordinate pairs for the segment stream.
    /// </summary>
    /// <remarks>
    /// Each <see cref="LatLng"/> represents a point along the segment's route.
    /// </remarks>
    public List<List<double>> Data { get; set; } = [];

    /// <summary>
    /// Gets the geographic coordinates represented by this instance.
    /// </summary>
    public List<LatLng> Points => [.. Data.Select(data => new LatLng
    {
        Latitude = data[0],
        Longitude = data[1]
    })];
}

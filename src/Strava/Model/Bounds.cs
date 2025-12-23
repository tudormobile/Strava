

namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a rectangular geographic area defined by its southwest and northeast corner coordinates.
/// </summary>
/// <remarks>The Bounds structure is typically used to specify a bounding box for map operations, such as
/// searching for locations within a specific area or defining the visible region of a map. The southwest and northeast
/// corners must be specified using valid latitude and longitude values. The structure does not enforce that the
/// southwest corner is actually southwest of the northeast corner; it is the caller's responsibility to ensure the
/// coordinates define a valid area.</remarks>
public struct Bounds
{
    /// <summary>
    /// Creates a Bounds instance from four double values representing latitude and longitude coordinates.
    /// </summary>
    /// <param name="southWestLat">The latitude of the southwest corner of the bounds.</param>
    /// <param name="southWestLng">The longitude of the southwest corner of the bounds.</param>
    /// <param name="northEastLat">The latitude of the northeast corner of the bounds.</param>
    /// <param name="northEastLng">The longitude of the northeast corner of the bounds.</param>
    /// <returns>A new Bounds instance.</returns>
    public static Bounds FromCoordinates(double southWestLat, double southWestLng, double northEastLat, double northEastLng)
    {
        return new Bounds
        {
            SouthWestCorner = (southWestLat, southWestLng),
            NorthEastCorner = (northEastLat, northEastLng)
        };
    }

    /// <summary>
    /// Gets or sets the geographic coordinates of the southwest corner of the bounding area.
    /// </summary>
    public LatLng SouthWestCorner { get; set; }

    /// <summary>
    /// Gets or sets the coordinates of the north-east corner of the bounding area.
    /// </summary>
    public LatLng NorthEastCorner { get; set; }

    /// <summary>
    /// Returns a string that represents the bounding box using the coordinates of its southwest and northeast corners.
    /// </summary>
    /// <returns>A string in the format "({SouthWestLatitude},{SouthWestLongitude}),({NorthEastLatitude},{NorthEastLongitude})"
    /// representing the bounding box coordinates.</returns>
    public override readonly string ToString()
    {
        return $"{{({SouthWestCorner.Latitude},{SouthWestCorner.Longitude}),({NorthEastCorner.Latitude},{NorthEastCorner.Longitude})}}";
    }

    /// <summary>
    /// Returns the coordinates of the bounding box as an array of doubles.
    /// </summary>
    /// <returns>A double array containing the latitude and longitude of the south-west and north-east corners, in the order:
    /// SouthWestCorner.Latitude, SouthWestCorner.Longitude, NorthEastCorner.Latitude, NorthEastCorner.Longitude.</returns>
    public readonly double[] AsArray() => [SouthWestCorner.Latitude, SouthWestCorner.Longitude, NorthEastCorner.Latitude, NorthEastCorner.Longitude];

    /// <summary>
    /// Converts a tuple containing four double values to a Bounds instance.
    /// </summary>
    /// <remarks>This operator enables implicit conversion from a tuple of four doubles to a Bounds object,
    /// allowing for concise initialization. The tuple elements must represent valid coordinates for the
    /// bounds.</remarks>
    /// <param name="v">A tuple containing the minimum X, minimum Y, maximum X, and maximum Y coordinates, in that order.</param>
    public static implicit operator Bounds((double, double, double, double) v) => Bounds.FromCoordinates(v.Item1, v.Item2, v.Item3, v.Item4);
}
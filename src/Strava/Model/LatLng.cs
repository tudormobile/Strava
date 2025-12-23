namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a geographic coordinate with latitude and longitude.
/// </summary>
public struct LatLng
{
    /// <summary>
    /// Converts a tuple containing latitude and longitude values to a LatLng instance.
    /// </summary>
    /// <remarks>This operator enables implicit conversion from a tuple to a LatLng, allowing for concise
    /// initialization in assignment and method calls.</remarks>
    /// <param name="coords">A tuple where the first element is the latitude and the second element is the longitude, both specified as
    /// double-precision floating-point values.</param>
    public static implicit operator LatLng((double Latitude, double Longitude) coords)
        => new LatLng
        {
            Latitude = coords.Latitude,
            Longitude = coords.Longitude
        };

    /// <summary>
    /// Gets or sets the latitude in decimal degrees.
    /// </summary>
    /// <remarks>
    /// Valid range is -90 to 90 degrees, where positive values represent north and negative values represent south.
    /// </remarks>
    public double Latitude;

    /// <summary>
    /// Gets or sets the longitude in decimal degrees.
    /// </summary>
    /// <remarks>
    /// Valid range is -180 to 180 degrees, where positive values represent east and negative values represent west.
    /// </remarks>
    public double Longitude;
}

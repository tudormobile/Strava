using System.Xml.Linq;

namespace Tudormobile.Strava.Documents;

/// <summary>
/// Represents a GPX (GPS Exchange Format) document with support for versions 1.0 and 1.1.
/// </summary>
/// <remarks>
/// Currently, only reading/parsing of GPX documents is supported. Writing or modifying GPX documents is not implemented.
/// </remarks>
public class GpxDocument : XmlDocumentBase
{
    private readonly string _version;
    private string? _name;
    private string? _description;
    private string? _author;
    private string? _email;
    private string? _url;
    private string? _urlName;
    private DateTime? _time;
    private string? _keywords;
    private GpxBounds? _bounds;

    /// <summary>
    /// Initializes a new instance of the <see cref="GpxDocument"/> class.
    /// </summary>
    /// <param name="document">The XML document containing GPX data.</param>
    /// <exception cref="ArgumentNullException">Thrown when the document is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the document is invalid or missing required elements.</exception>
    /// <exception cref="NotSupportedException">Thrown when the GPX version is not 1.0 or 1.1.</exception>
    public GpxDocument(XDocument document) : base(document, "gpx")
    {
        if (_root.Attribute("version") == null)
        {
            throw new ArgumentException("The provided GPX document does not have a version attribute.");
        }
        _version = (_root.Attribute("version")?.Value) ?? throw new ArgumentException("The provided GPX document does not have a version attribute value.");
        if (_version != "1.0" && _version != "1.1")
        {
            throw new NotSupportedException($"GPX version {_version} is not supported. Only versions 1.0 and 1.1 are supported.");
        }
    }

    /// <summary>
    /// Gets the GPX version of the document (e.g., "1.0" or "1.1").
    /// </summary>
    public string Version => _version;

    /// <summary>
    /// Gets a value indicating whether the document is GPX version 1.1.
    /// </summary>
    public bool IsVersion11 => _version == "1.1";

    /// <summary>
    /// Gets the name of the GPX file or track.
    /// </summary>
    public string Name => _name ??= GetStringValueAnyVersion("name") ?? string.Empty;

    /// <summary>
    /// Gets the description of the GPX file or track.
    /// </summary>
    public string Description => _description ??= GetStringValueAnyVersion("desc") ?? string.Empty;

    /// <summary>
    /// Gets the author of the GPX file.
    /// </summary>
    public string Author => _author ??= GetStringValueAnyVersion("author") ?? string.Empty;

    /// <summary>
    /// Gets the email address associated with the GPX file.
    /// </summary>
    public string Email => _email ??= GetStringValueAnyVersion("email") ?? string.Empty;

    /// <summary>
    /// Gets the URL associated with the GPX file.
    /// </summary>
    public string Url => _url ??= GetStringValueAnyVersion("url") ?? string.Empty;

    /// <summary>
    /// Gets the name of the URL associated with the GPX file.
    /// </summary>
    public string UrlName => _urlName ??= GetStringValueAnyVersion("urlname") ?? string.Empty;

    /// <summary>
    /// Gets the timestamp of when the GPX file was created.
    /// </summary>
    public DateTime Time => _time ??= GetDateTimeValueAnyVersion("time");

    /// <summary>
    /// Gets the keywords associated with the GPX file.
    /// </summary>
    public string Keywords => _keywords ??= GetStringValueAnyVersion("keywords") ?? string.Empty;

    /// <summary>
    /// Gets the list of waypoints in the GPX document.
    /// </summary>
    public List<GpxWaypoint> Waypoints => [.. _root.Elements(_defaultNamespace + "wpt").Select(x => new GpxWaypoint(x))];

    /// <summary>
    /// Gets the list of routes in the GPX document.
    /// </summary>
    public List<GpxRoute> Routes => [.. _root.Elements(_defaultNamespace + "rte").Select(rte => new GpxRoute(rte))];

    /// <summary>
    /// Gets the list of tracks in the GPX document.
    /// </summary>
    public List<GpxTrack> Tracks => [.. _root.Elements(_defaultNamespace + "trk").Select(trk => new GpxTrack(trk))];

    /// <summary>
    /// Gets the geographic bounds of the GPX data.
    /// </summary>
    public GpxBounds Bounds => _bounds ??= new(IsVersion11
        ? (_root.Element(_defaultNamespace + "metadata")?.Element(_defaultNamespace + "bounds"))
        : _root.Element(_defaultNamespace + "bounds")
    );

    /// <summary>
    /// Represents the geographic bounds of a GPX document.
    /// </summary>
    public class GpxBounds
    {
        /// <summary>
        /// Gets or sets the minimum latitude.
        /// </summary>
        public double MinLat { get; set; }

        /// <summary>
        /// Gets or sets the minimum longitude.
        /// </summary>
        public double MinLon { get; set; }

        /// <summary>
        /// Gets or sets the maximum latitude.
        /// </summary>
        public double MaxLat { get; set; }

        /// <summary>
        /// Gets or sets the maximum longitude.
        /// </summary>
        public double MaxLon { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpxBounds"/> class.
        /// </summary>
        /// <param name="boundsElement">The XML element containing bounds data.</param>
        public GpxBounds(XElement? boundsElement)
        {
            if (boundsElement != null)
            {
                MinLat = double.Parse(boundsElement.Attribute("minlat")?.Value ?? "0");
                MinLon = double.Parse(boundsElement.Attribute("minlon")?.Value ?? "0");
                MaxLat = double.Parse(boundsElement.Attribute("maxlat")?.Value ?? "0");
                MaxLon = double.Parse(boundsElement.Attribute("maxlon")?.Value ?? "0");
            }
        }

        /// <summary>
        /// Returns a string representation of the bounds.
        /// </summary>
        /// <returns>A formatted string containing the minimum and maximum latitude and longitude values.</returns>
        public override string ToString()
        {
            return $"(MinLat: {MinLat}, MinLon: {MinLon}, MaxLat: {MaxLat}, MaxLon: {MaxLon})";
        }
    }

    /// <summary>
    /// Base class for GPX entities that contain common metadata properties.
    /// </summary>
    public class GpxEntity : XmlDocumentElement
    {
        internal GpxEntity(XElement element) : base(element) { }

        /// <summary>
        /// Gets the name of the GPX entity.
        /// </summary>
        public string Name => _element.Element(_element.GetDefaultNamespace() + "name")?.Value ?? string.Empty;

        /// <summary>
        /// Gets the comment associated with the GPX entity.
        /// </summary>
        public string Comment => _element.Element(_element.GetDefaultNamespace() + "cmt")?.Value ?? string.Empty;

        /// <summary>
        /// Gets the description of the GPX entity.
        /// </summary>
        public string Description => _element.Element(_element.GetDefaultNamespace() + "desc")?.Value ?? string.Empty;

        /// <summary>
        /// Gets the source of the GPX data.
        /// </summary>
        public string Source => _element.Element(_element.GetDefaultNamespace() + "src")?.Value ?? string.Empty;

        /// <summary>
        /// Gets the symbol name for the GPX entity.
        /// </summary>
        public string SymbolName => _element.Element(_element.GetDefaultNamespace() + "sym")?.Value ?? string.Empty;

        /// <summary>
        /// Gets the classification type of the GPX entity.
        /// </summary>
        public string ClassificationType => _element.Element(_element.GetDefaultNamespace() + "type")?.Value ?? string.Empty;
    }

    /// <summary>
    /// Represents a waypoint in a GPX document.
    /// </summary>
    public class GpxWaypoint : GpxEntity
    {
        internal GpxWaypoint(XElement element) : base(element) { }

        /// <summary>
        /// Gets the latitude of the waypoint in decimal degrees.
        /// </summary>
        public double Latitude => double.Parse(_element.Attribute("lat")?.Value ?? "0");

        /// <summary>
        /// Gets the longitude of the waypoint in decimal degrees.
        /// </summary>
        public double Longitude => double.Parse(_element.Attribute("lon")?.Value ?? "0");

        /// <summary>
        /// Gets the elevation of the waypoint in meters.
        /// </summary>
        public double Elevation => double.Parse(_element.Element(_element.GetDefaultNamespace() + "ele")?.Value ?? "0");

        /// <summary>
        /// Gets the timestamp of when the waypoint was recorded.
        /// </summary>
        public DateTime Time
        {
            get
            {
                var timeString = _element.Element(_element.GetDefaultNamespace() + "time")?.Value;
                if (timeString != null && DateTime.TryParse(timeString, out DateTime result))
                {
                    return result;
                }
                return DateTime.MinValue;
            }
        }
    }

    /// <summary>
    /// Represents a track in a GPX document.
    /// </summary>
    public class GpxTrack : GpxEntity
    {
        internal GpxTrack(XElement element) : base(element) { }

        /// <summary>
        /// Gets the track number.
        /// </summary>
        public long Number => long.Parse(_element.Element(_element.GetDefaultNamespace() + "number")?.Value ?? "0");

        /// <summary>
        /// Gets the list of track segments in the track.
        /// </summary>
        public List<GpxTrackSegment> TrackSegments => [.. _element.Elements(_element.GetDefaultNamespace() + "trkseg").Select(x => new GpxTrackSegment(x))];
    }

    /// <summary>
    /// Represents a track segment within a track in a GPX document.
    /// </summary>
    public class GpxTrackSegment : XmlDocumentElement
    {
        internal GpxTrackSegment(XElement element) : base(element) { }

        /// <summary>
        /// Gets the list of track points in the track segment.
        /// </summary>
        public List<GpxWaypoint> TrackPoints => [.. _element.Elements(_element.GetDefaultNamespace() + "trkpt").Select(x => new GpxWaypoint(x))];
    }

    /// <summary>
    /// Represents a route in a GPX document.
    /// </summary>
    public class GpxRoute : GpxEntity
    {
        internal GpxRoute(XElement element) : base(element) { }

        /// <summary>
        /// Gets the route number.
        /// </summary>
        public long Number => long.Parse(_element.Element(_element.GetDefaultNamespace() + "number")?.Value ?? "0");

        /// <summary>
        /// Gets the list of route points in the route.
        /// </summary>
        public List<GpxWaypoint> RoutePoints => [.. _element.Elements(_element.GetDefaultNamespace() + "rtept").Select(x => new GpxWaypoint(x))];
    }

    private string? GetStringValueAnyVersion(string elementName)
    {
        var element = IsVersion11
                ? (_root.Element(_defaultNamespace + "metadata")?.Element(_defaultNamespace + elementName))
                : _root.Element(_defaultNamespace + elementName);
        return element?.Value;
    }

    private DateTime GetDateTimeValueAnyVersion(string name)
    {
        var timeString = GetStringValueAnyVersion(name);
        if (timeString != null && DateTime.TryParse(timeString, out DateTime result))
        {
            return result;
        }
        return DateTime.MinValue;
    }
}

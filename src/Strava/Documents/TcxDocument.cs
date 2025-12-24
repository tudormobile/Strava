using System.Xml.Linq;

namespace Tudormobile.Strava.Documents;

/// <summary>
/// Represents a Training Center XML (TCX) document, providing access to workout and fitness data in the TCX format.
/// </summary>
/// <remarks>Use this class to read, manipulate, or generate TCX files, which are commonly used for exchanging
/// fitness activity data between devices and applications. Inherits functionality from the GpxDocument class, enabling
/// support for GPX-related features where applicable.</remarks>
public class TcxDocument : XmlDocumentBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TcxDocument"/> class.
    /// </summary>
    /// <param name="document">The XML document containing TCX data.</param>
    /// <exception cref="ArgumentNullException">Thrown when the document is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the document is invalid or missing required elements.</exception>
    /// <exception cref="NotSupportedException">Thrown when the TCX version is not supported.</exception>
    public TcxDocument(XDocument document) : base(document, "TrainingCenterDatabase") { }

    /// <summary>
    /// Gets the collection of activities contained in the TCX document.
    /// </summary>
    public IEnumerable<TcxActivity> Activities => _root.Elements("Activities").Select(x => new TcxActivity(x));

    /// <summary>
    /// Represents an activity in a TCX document, containing sport type, identifier, and lap data.
    /// </summary>
    public class TcxActivity : XmlDocumentElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TcxActivity"/> class.
        /// </summary>
        /// <param name="activityElement">The XML element representing the activity.</param>
        public TcxActivity(XElement activityElement) : base(activityElement) { }

        /// <summary>
        /// Gets the unique identifier of the activity as a string in ISO 8601 format.
        /// </summary>
        public string Id => _element.Element("Id")?.Value ?? DateTime.MinValue.ToString("O");

        /// <summary>
        /// Gets the activity identifier as a <see cref="DateTime"/>.
        /// </summary>
        public DateTime ActivityId => DateTime.Parse(Id);

        /// <summary>
        /// Gets the sport type of the activity (e.g., "Running", "Biking").
        /// </summary>
        public string Sport => _element.Attribute("Sport")?.Value ?? "Unknown";

        /// <summary>
        /// Gets the collection of laps in the activity.
        /// </summary>
        public IEnumerable<TcxActivityLap> Laps => _element.Elements("Lap").Select(x => new TcxActivityLap(x));
    }

    /// <summary>
    /// Represents a lap within an activity, containing timing, distance, and trackpoint data.
    /// </summary>
    public class TcxActivityLap : XmlDocumentElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TcxActivityLap"/> class.
        /// </summary>
        /// <param name="activityElement">The XML element representing the lap.</param>
        public TcxActivityLap(XElement activityElement) : base(activityElement) { }

        /// <summary>
        /// Gets the start time of the lap.
        /// </summary>
        public DateTime StartTime => (DateTime)_element.Attribute("StartTime")!;

        /// <summary>
        /// Gets the total elapsed time of the lap in seconds.
        /// </summary>
        public double TotalTimeSeconds => (double)_element.Element("TotalTimeSeconds")!;

        /// <summary>
        /// Gets the total distance covered in the lap in meters.
        /// </summary>
        public double DistanceMeters => (double)_element.Element("DistanceMeters")!;

        /// <summary>
        /// Gets the maximum speed achieved during the lap in meters per second.
        /// </summary>
        public double MaximumSpeed => (double)_element.Element("MaximumSpeed")!;

        /// <summary>
        /// Gets the collection of trackpoints (recorded positions) in the lap.
        /// </summary>
        public IEnumerable<TcxTrackpoint> Tracks => _element.Elements("Track").Select(x => new TcxTrackpoint(x));
    }

    /// <summary>
    /// Represents a trackpoint in a lap, containing timestamp, position, altitude, distance, and heart rate data.
    /// </summary>
    public class TcxTrackpoint : XmlDocumentElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TcxTrackpoint"/> class.
        /// </summary>
        /// <param name="element">The XML element representing the trackpoint.</param>
        public TcxTrackpoint(XElement element) : base(element) { }

        /// <summary>
        /// Gets the timestamp when the trackpoint was recorded.
        /// </summary>
        public DateTime Time => (DateTime)_element.Attribute("Time")!;

        /// <summary>
        /// Gets the geographic position of the trackpoint as a tuple of latitude and longitude in decimal degrees.
        /// </summary>
        public (double lat, double lon) Position => ((double)_element.Element("LatitudeDegrees")!, (double)_element.Element("LongitudeDegrees")!);

        /// <summary>
        /// Gets the altitude of the trackpoint in meters above sea level.
        /// </summary>
        public double AltitudeMeters => (double)_element.Element("AltitudeMeters")!;

        /// <summary>
        /// Gets the cumulative distance traveled up to this trackpoint in meters.
        /// </summary>
        public double DistanceMeters => (double)_element.Element("DistanceMeters")!;

        /// <summary>
        /// Gets the heart rate in beats per minute (BPM) at this trackpoint.
        /// </summary>
        public double HeartRateBpm => (double)(_element.Element("HeartRateBpm")!.Element("value")!);
    }
}
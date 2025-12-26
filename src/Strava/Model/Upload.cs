namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents the result of a file upload to Strava, including status and error information.
/// </summary>
/// <remarks>
/// This class is used to track the state and outcome of an upload operation, such as a FIT, TCX, or GPX file.
/// </remarks>
public class Upload
{
    /// <summary>
    /// Gets or sets the unique identifier for the upload.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the activity created by this upload, if available.
    /// </summary>
    public long ActivityId { get; set; }

    /// <summary>
    /// Gets or sets the string representation of the upload identifier.
    /// </summary>
    public string IdStr { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the external identifier for the uploaded file (e.g., filename).
    /// </summary>
    public string ExternalId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error message if the upload failed, or <c>null</c> if no error occurred.
    /// </summary>
    public string Error { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status message describing the current state of the upload.
    /// </summary>
    /// <remarks>
    /// Typical values include "Your activity is ready.", "Your activity is still being processed.", or an error message.
    /// </remarks>
    public string Status { get; set; } = string.Empty;
}

namespace Tudormobile.Strava.Model;

/// <summary>
/// Encapsulates the errors that may be returned from the API.
/// </summary>
public class Fault
{
    /// <summary>
    /// The set of specific errors associated with this fault, if any.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The message of the fault.
    /// </summary>
    public Error[] Errors { get; set; } = [];
}

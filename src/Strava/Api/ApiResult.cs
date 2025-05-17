namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava API result. Encapsulates the result of an API call.
/// </summary>
/// <typeparam name="T">Type of the result data.</typeparam>
/// <remarks>
/// This object encapsulates the data resulting from an asynchronous call to the Strava API. If no errors
/// occur, the Data property will contain the result data. If an error occurs, the Error property will contain
/// and ApiError object that describes the error. The Success property indicates whether the call was successful.
/// </remarks>
public class ApiResult<T>
{
    /// <summary>
    /// Returned data, if successful; otherwise (null).
    /// </summary>
    public T? Data { get; init; }

    /// <summary>
    /// Error information or (null) if no errors.
    /// </summary>
    public ApiError? Error { get; init; }

    /// <summary>
    /// True if data is valid.
    /// </summary>
    public bool Success => Error == null;

    /// <summary>
    /// Create an initialize a new instance.
    /// </summary>
    /// <param name="data">(Optional) The Data to include. If omitted, (null) is used.</param>
    /// <param name="error">(Optional) The Error to include. If omitted, (null) is used.</param>
    public ApiResult(T? data = default, ApiError? error = null)
    {
        Data = data;
        Error = error;
    }
}

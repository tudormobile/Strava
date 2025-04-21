namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava API result.
/// </summary>
/// <typeparam name="T">Type of the result data.</typeparam>
/// <remarks>
/// The result of an asynchronous API call to the Strava API containing
/// data (if success) or error (if error occurs). Note that only one of
/// these properties is valid (non-null).
/// </remarks>
public class ApiResult<T>
{
    /// <summary>
    /// Returned data, if successful; otherwise (null).
    /// </summary>
    public T? Data { get; init; }

    /// <summary>
    /// Error information or (null) if successful.
    /// </summary>
    public ApiError? Error { get; init; }

    /// <summary>
    /// True if data is valid.
    /// </summary>
    public bool Success => Error == null;

    /// <summary>
    /// Create an initialize a new instance.
    /// </summary>
    /// <param name="data">Data to include (optional).</param>
    /// <param name="error">Error to include (optional)</param>
    public ApiResult(T? data = default, ApiError? error = null)
    {
        Data = data;
        Error = error;
    }
}

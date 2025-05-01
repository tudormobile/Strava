namespace Tudormobile.Strava.Model;

/// <summary>
/// Api Error response record.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// A message summarizing the error.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Collection of errors associated with the request.
    /// </summary>
    public Error[] Errors { get; set; } = [];

}

// here is the bad authorization response.
/*
{"message":"Bad Request","errors":[{"resource":"Application","field":"client_id","code":"invalid"}]}
*/

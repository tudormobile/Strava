namespace Tudormobile.Strava.Model;

/// <summary>
/// Api Error Resource Record.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Error message.
    /// </summary>
    public string message { get; set; } = string.Empty;

    /// <summary>
    /// Errors.
    /// </summary>
    public Error[] errors { get; set; } = [];

    /// <summary>
    /// Error response error.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Resource causing the error.
        /// </summary>
        public string? resource { get; set; }

        /// <summary>
        /// API Field associate with the error.
        /// </summary>
        public string? @field { get; set; }

        /// <summary>
        /// Code associated with the error.
        /// </summary>
        public string? code { get; set; }
    }
}

// here is the bad authorization response.
/*
{"message":"Bad Request","errors":[{"resource":"Application","field":"client_id","code":"invalid"}]}
*/

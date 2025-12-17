using System.Net;
using System.Text;

namespace Tudormobile.Strava;

/// <summary>
/// Represents errors returned by the Strava API or encountered while calling it.
/// Carries HTTP metadata (status code, raw content, headers) to aid diagnostics and handling.
/// </summary>
public class StravaException : Exception
{
    /// <summary>
    /// HTTP status code returned by the API, if available.
    /// </summary>
    public HttpStatusCode? StatusCode { get; }

    /// <summary>
    /// Raw response content returned by the API (may be JSON error body) when available.
    /// </summary>
    public string? Content { get; }

    /// <summary>
    /// Response headers returned by the API. Keys are header names, values are header values.
    /// This is a read-only snapshot supplied at construction.
    /// </summary>
    public IReadOnlyDictionary<string, IEnumerable<string>>? ResponseHeaders { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StravaException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public StravaException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StravaException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public StravaException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StravaException"/> with HTTP metadata and an inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="statusCode">HTTP status code returned by the API (optional).</param>
    /// <param name="content">Raw response content (optional).</param>
    /// <param name="responseHeaders">Response headers snapshot (optional).</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public StravaException(
        string message,
        HttpStatusCode? statusCode,
        string? content = null,
        IDictionary<string, IEnumerable<string>>? responseHeaders = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Content = content;
        ResponseHeaders = responseHeaders is null
            ? null
            : new Dictionary<string, IEnumerable<string>>(responseHeaders, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Returns a string representation of the exception including HTTP metadata (status and a trimmed content preview).
    /// </summary>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(base.ToString());

        if (StatusCode.HasValue)
        {
            sb.AppendLine();
            sb.Append("HTTP Status: ").Append(StatusCode.Value);
        }

        if (!string.IsNullOrEmpty(Content))
        {
            const int maxPreview = 1024;
            var preview = Content.Length <= maxPreview ? Content : Content[..maxPreview] + "�(truncated)";
            sb.AppendLine();
            sb.Append("Content: ").Append(preview);
        }

        return sb.ToString();
    }
}
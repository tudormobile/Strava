using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 API Interface.
/// </summary>
public interface IStravaApi
{
    /// <summary>
    /// Retrieves a stream from the Strava API.
    /// </summary>
    /// <param name="requestUri">The URI of the request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Stream"/> containing the API response.</returns>
    /// <remarks>
    /// It is the responsibility of the caller to dispose of the returned stream.
    /// </remarks>
    Task<Stream> GetStreamAsync(string requestUri, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve Athlete record by Id for logged in user.
    /// </summary>
    /// <param name="athleteId">Optional; Athlete Id (default = logged in user).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>Athlete record associated with the Id.</returns>
    /// <remarks>
    /// Returns the currently authenticated athlete. Tokens with profile:read_all scope will 
    /// receive a detailed athlete representation; all others will receive a summary representation.
    /// </remarks>
    Task<ApiResult<Athlete>> GetAthleteAsync(long? athleteId = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an asynchronous HTTP request to the specified URI and returns the result as an <see cref="ApiResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type to which the response content is deserialized.</typeparam>
    /// <param name="requestUri">The URI of the API endpoint to request. Cannot be <see langword="null"/>.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="ApiResult{T}"/>
    /// representing the outcome of the API request, including the deserialized response data or error information.</returns>
    Task<ApiResult<T>> GetApiResultAsync<T>(Uri requestUri, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP PUT request to the specified URI with the provided request body and returns the deserialized API
    /// result.
    /// </summary>
    /// <typeparam name="TBody">The type of the request body to serialize and send.</typeparam>
    /// <typeparam name="TResult">The type to which the response content is deserialized.</typeparam>
    /// <param name="requestUri">The URI to which the PUT request is sent. Cannot be <c>null</c>.</param>
    /// <param name="body">The request body to send with the PUT request. May be <c>null</c> if the API allows an empty body.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="ApiResult{TResult}"/>
    /// representing the deserialized response from the API.</returns>
    Task<ApiResult<TResult>> PutApiResultAsync<TBody, TResult>(Uri requestUri, TBody? body, CancellationToken cancellationToken = default);
}

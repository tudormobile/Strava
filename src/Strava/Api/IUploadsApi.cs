using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Uploads API Interface.
/// </summary>
public interface IUploadsApi
{
    /// <summary>
    /// Gets the status of an upload by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the upload.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="ApiResult{Upload}"/> containing the upload status.</returns>
    Task<ApiResult<Upload>> GetUploadAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads an activity file to Strava.
    /// </summary>
    /// <param name="filename">The path to the activity file to upload.</param>
    /// <param name="name">The name of the activity.</param>
    /// <param name="description">The description of the activity.</param>
    /// <param name="trainer">Set to "1" if the activity was performed on a trainer, otherwise "0".</param>
    /// <param name="commute">Set to "1" if the activity was a commute, otherwise "0".</param>
    /// <param name="dataType">The format of the uploaded file (e.g., "fit", "gpx", "tcx").</param>
    /// <param name="externalId">An identifier to associate with the upload for deduplication.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="ApiResult{Upload}"/> containing the upload status.</returns>
    Task<ApiResult<Upload>> UploadActivityAsync(string filename, string name, string description, string trainer, string commute, string dataType, string externalId, CancellationToken cancellationToken = default);
}

internal partial class StravaApiImpl
{
    /// <inheritdoc/>
    public Task<ApiResult<Upload>> GetUploadAsync(long id, CancellationToken cancellationToken = default)
        => GetApiResultAsync<Upload>($"/uploads/{id}", cancellationToken);

    /// <inheritdoc/>
    public Task<ApiResult<Upload>> UploadActivityAsync(string filename, string name, string description, string trainer, string commute, string dataType, string externalId, CancellationToken cancellationToken = default)
    {
        using var stream = File.OpenRead(filename);
        using var content = new MultipartFormDataContent
        {
            { new StringContent(name), "name" },
            { new StringContent(description), "description" },
            { new StringContent(trainer), "trainer" },
            { new StringContent(commute), "commute" },
            { new StringContent(dataType), "data_type" },
            { new StringContent(externalId), "external_id" },
            { new StreamContent(stream), "file" }
        };
        return PostApiResultAsync<Upload>("/uploads", content, cancellationToken);
    }
}

using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 Gears API Interface.
/// </summary>
public interface IGearsApi
{
    /// <summary>
    /// Retrieves detailed information about a specific gear by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the gear.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="ApiResult{DetailedGear}"/> containing the detailed gear information.</returns>
    Task<ApiResult<DetailedGear>> GetEquipmentAsync(string id, CancellationToken cancellationToken = default);
}

internal partial class StravaApiImpl
{
    /// <inheritdoc/>
    Task<ApiResult<DetailedGear>> IGearsApi.GetEquipmentAsync(string id, CancellationToken cancellationToken)
        => GetApiResultAsync<DetailedGear>($"/gear/{id}", cancellationToken);
}

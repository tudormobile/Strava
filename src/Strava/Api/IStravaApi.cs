using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api;

/// <summary>
/// Strava V3 API Interface.
/// </summary>
public interface IStravaApi
{
    /// <summary>
    /// Retrieve Athlete record by Id for logged in user.
    /// </summary>
    /// <param name="athleteId">Optional; Athlete Id (default = logged in user).</param>
    /// <returns>Athlete record associated with the Id.</returns>
    /// <remarks>
    /// Returns the currently authenticated athlete. Tokens with profile:read_all scope will 
    /// receive a detailed athlete representation; all others will receive a summary representation.
    /// </remarks>
    Task<ApiResult<Athlete>> GetAthlete(long? athleteId = 0);

}

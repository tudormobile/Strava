﻿namespace Tudormobile.Strava.Model;

/// <summary>
/// Strava Resource state, indicates level of detail.
/// </summary>
public enum ResourceStates
{
    /// <summary>
    /// Meta resource state, indicates a minimal amount of data.
    /// </summary>
    Meta = 1,
    /// <summary>
    /// Summary resource state, indicates a summary of the data.
    /// </summary>
    Summary = 2,
    /// <summary>
    /// Detail resource state, indicates a detailed view of the data.
    /// </summary>
    Detail = 3
}

﻿namespace Tudormobile.Strava.Model;

/// <summary>
/// Strava API Error.
/// </summary>
public class Error
{
    /// <summary>
    /// The code associated with this error.
    /// </summary>
    public string Code { get; set; } = String.Empty;

    /// <summary>
    /// The specific field or aspect of the resource associated with this error.
    /// </summary>
    public string Field { get; set; } = String.Empty;

    /// <summary>
    /// The type of resource associated with this error.
    /// </summary>
    public string Resource { get; set; } = String.Empty;
}

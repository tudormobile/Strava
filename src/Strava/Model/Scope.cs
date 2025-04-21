namespace Tudormobile.Strava.Model;

/// <summary>
/// Authorization Scope
/// </summary>
/// <remarks>
/// Requested scopes, as a comma delimited string, e.g. "activity:read_all,activity:write". 
/// Applications should request only the scopes required for the application to function normally. 
/// The scope activity:read is required for activity webhooks.
/// <para>read: read public segments, public routes, public profile data, public posts, public events, club feeds, and leaderboards</para>
/// <para>read_all:read private routes, private segments, and private events for the user</para>
/// <para>profile:read_all: read all profile information even if the user has set their profile visibility to Followers or Only You</para>
/// <para>profile:write: update the user's weight and Functional Threshold Power (FTP), and access to star or unstar segments on their behalf</para>
/// <para>activity:read: read the user's activity data for activities that are visible to Everyone and Followers, excluding privacy zone data</para>
/// <para>activity:read_all: the same access as activity:read, plus privacy zone data and access to read the user's activities with visibility set to Only You</para>
/// <para>activity:write: access to create manual activities and uploads, and access to edit any activities that are visible to the app, based on activity read access level</para>
/// </remarks>
public class Scope
{
    /// <summary>
    /// General scope.
    /// </summary>
    public ScopePermission PublicScope { get; set; }

    /// <summary>
    /// Profile scope.
    /// </summary>
    public ScopePermission ProfileScope { get; set; }

    /// <summary>
    /// Activity scope.
    /// </summary>
    public ScopePermission ActivityScope { get; set; }

    /// <summary>
    /// Scope permission flags.
    /// </summary>
    [Flags]
    public enum ScopePermission
    {
        /// <summary>
        /// Read permissions
        /// </summary>
        read = 1,

        /// <summary>
        /// Read all permissions
        /// </summary>
        read_all = 2,

        /// <summary>
        /// Write permissions
        /// </summary>
        write = 4
    }

    /// <summary>
    /// Create and initialize a new instance.
    /// </summary>
    /// <param name="publicScope">General scope.</param>
    /// <param name="profileScope">Profile scope.</param>
    /// <param name="activityScope">Activity scope.</param>
    public Scope(ScopePermission publicScope = 0, ScopePermission profileScope = 0, ScopePermission activityScope = 0)
    {
        PublicScope = publicScope;
        ProfileScope = profileScope;
        ActivityScope = activityScope;
    }

    /// <summary>
    /// Converts scope instance to a string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Join(",",
            PublicScope.ToString().Split(',', StringSplitOptions.TrimEntries)
            .Concat(ProfileScope.ToString().Split(',', StringSplitOptions.TrimEntries).Select(s => $"profile:{s}"))
            .Concat(ActivityScope.ToString().Split(',', StringSplitOptions.TrimEntries).Select(s => $"activity:{s}"))
            );
    }
}

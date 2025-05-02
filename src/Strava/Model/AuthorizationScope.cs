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
public class AuthorizationScope
{
    /// <summary>
    /// Read public data only.
    /// </summary>
    /// <remarks>These are the minimal meaningful permissions.</remarks>
    public static readonly AuthorizationScope READ = new AuthorizationScope(PublicScopes.read, ProfileScopes.read_all, ActivityScopes.read);

    /// <summary>
    /// General scope.
    /// </summary>
    public PublicScopes PublicScope { get; set; }

    /// <summary>
    /// Profile scope.
    /// </summary>
    public ProfileScopes ProfileScope { get; set; }

    /// <summary>
    /// Activity scope.
    /// </summary>
    public ActivityScopes ActivityScope { get; set; }

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
    /// Valid public scopes
    /// </summary>
    public enum PublicScopes
    {
        /// <summary>
        /// Read permissions
        /// </summary>
        read = ScopePermission.read,
        /// <summary>
        /// Read all permissions
        /// </summary>
        read_all = ScopePermission.read_all,
    }

    /// <summary>
    /// Valid profile scopes
    /// </summary>
    [Flags]
    public enum ProfileScopes
    {
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
    /// Activity scopes
    /// </summary>
    [Flags]
    public enum ActivityScopes
    {
        /// <summary>
        /// Read permissions
        /// </summary>
        read = ScopePermission.read,

        /// <summary>
        /// Read all permissions
        /// </summary>
        read_all = ScopePermission.read_all,

        /// <summary>
        /// Write permissions
        /// </summary>
        write = ScopePermission.write
    }

    /// <summary>
    /// Create and initialize a new instance.
    /// </summary>
    /// <param name="publicScope">General scope.</param>
    /// <param name="profileScope">Profile scope.</param>
    /// <param name="activityScope">Activity scope.</param>
    public AuthorizationScope(PublicScopes publicScope = 0, ProfileScopes profileScope = 0, ActivityScopes activityScope = 0)
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
        var scopes = new List<string>();

        // global scope
        if (PublicScope != 0)
        {
            scopes.Add(PublicScope.HasFlag(PublicScopes.read_all) ? "read_all" : "read");
        }

        // profile scope
        if (ProfileScope.HasFlag(ProfileScopes.read_all))
        {
            scopes.Add("profile:read_all");
        }
        if (ProfileScope.HasFlag(ProfileScopes.write))
        {
            scopes.Add("profile:write");
        }

        // activity scope
        if (ActivityScope.HasFlag(ActivityScopes.read_all))
        {
            scopes.Add("activity:read_all");
        }
        else if (ActivityScope.HasFlag(ActivityScopes.read))
        {
            scopes.Add("activity:read");
        }
        if (ActivityScope.HasFlag(ActivityScopes.write))
        {
            scopes.Add("activity:write");
        }

        return string.Join(",", scopes);
    }
}

namespace Tudormobile.Strava.Client;

/// <summary>
/// Repository interface for managing <see cref="StravaActivity"/> instances.
/// </summary>
public interface IStravaActivityRepository
{
    /// <summary>
    /// Gets all activities currently available in the repository.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
    /// <returns>An enumerable of <see cref="StravaActivity"/> instances.</returns>
    Task<IEnumerable<StravaActivity>> GetActivitiesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an activity by its identifier.
    /// </summary>
    /// <param name="id">The activity identifier.</param>
    /// <returns>The matching <see cref="StravaActivity"/>, or <c>null</c> if not found.</returns>
    StravaActivity? GetActivityById(long id);

    /// <summary>
    /// Adds the specified activity to the repository.
    /// </summary>
    /// <param name="activity">The activity to add.</param>
    void AddActivity(StravaActivity activity);

    /// <summary>
    /// Deletes the activity with the specified identifier from the repository.
    /// </summary>
    /// <param name="id">The identifier of the activity to delete.</param>
    void DeleteActivity(long id);

    /// <summary>
    /// Updates an existing activity in the repository.
    /// </summary>
    /// <param name="activity">The activity containing updated values.</param>
    void UpdateActivity(StravaActivity activity);

    /// <summary>
    /// Persists any pending changes to the underlying store.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a value indicating whether the repository contains an activity with the specified id.
    /// </summary>
    /// <param name="id">Activity identifier to check.</param>
    /// <returns><c>true</c> when the activity exists; otherwise <c>false</c>.</returns>
    bool ActivityExists(long id);

    /// <summary>
    /// Returns a value indicating whether there are unsaved changes in the repository.
    /// </summary>
    /// <returns><c>true</c> if there are unsaved changes; otherwise <c>false</c>.</returns>
    bool HasUnsavedChanges();

    /// <summary>
    /// Gets the last time the repository was saved.
    /// </summary>
    DateTime LastSavedTime { get; }

    /// <summary>
    /// Gets the last modified time of any activity in the repository.
    /// </summary>
    DateTime LastModifiedTime { get; }
}

internal class StravaActivityRepository : IStravaActivityRepository
{
    private bool _hasUnsavedChanges;
    private IList<StravaActivity>? _activities;
    private (DateTime LastSavedTime, DateTime LastActivityTime)? _timestamps;
    private readonly IStravaActivityRepositoryContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="StravaActivityRepository"/> class using the specified context.
    /// </summary>
    /// <param name="context">Repository context used to load and save activities.</param>
    public StravaActivityRepository(IStravaActivityRepositoryContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the time when changes were last saved to the underlying store.
    /// </summary>
    public DateTime LastSavedTime => (_timestamps ??= _context.Metadata()).LastSavedTime;

    /// <summary>
    /// Gets the time of the most recent activity modification.
    /// </summary>
    public DateTime LastModifiedTime => (_timestamps ??= _context.Metadata()).LastActivityTime;

    /// <summary>
    /// Determines whether an activity with the specified id exists in the repository.
    /// </summary>
    /// <param name="id">The activity identifier to search for.</param>
    /// <returns><c>true</c> when an activity with the specified id exists; otherwise <c>false</c>.</returns>
    public bool ActivityExists(long id) => EnsureActivities().Any(a => a.Id == id);

    /// <summary>
    /// Adds an activity to the repository and marks the repository as modified.
    /// </summary>
    /// <param name="activity">The activity to add.</param>
    public void AddActivity(StravaActivity activity)
    {
        EnsureActivities().Add(activity);
        MarkAsModified();
        if (activity.StartDate > LastModifiedTime)
        {
            _timestamps = (LastSavedTime, activity.StartDate);
        }
    }

    /// <summary>
    /// Deletes an activity by id. If the activity is present it is removed and repository state is updated.
    /// </summary>
    /// <param name="id">Identifier of the activity to delete.</param>
    public void DeleteActivity(long id)
    {
        var activityToDelete = EnsureActivities().FirstOrDefault(a => a.Id == id);
        if (activityToDelete != null)
        {
            EnsureActivities().Remove(activityToDelete);
            MarkAsModified();
            _timestamps = ((_timestamps ??= _context.Metadata()).LastSavedTime, EnsureActivities().Any() ? EnsureActivities().Max(a => a.StartDate) : DateTime.MinValue);
        }
    }

    /// <summary>
    /// Returns all activities currently loaded in the repository.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
    /// <returns>An enumerable of <see cref="StravaActivity"/> instances.</returns>
    public async Task<IEnumerable<StravaActivity>> GetActivitiesAsync(CancellationToken cancellationToken) => await EnsureActivitiesAsync(cancellationToken);

    /// <summary>
    /// Gets a single activity by identifier.
    /// </summary>
    /// <param name="id">The activity identifier.</param>
    /// <returns>The activity when found; otherwise <c>null</c>.</returns>
    public StravaActivity? GetActivityById(long id) => EnsureActivities().FirstOrDefault(a => a.Id == id);

    /// <summary>
    /// Returns whether the repository currently has unsaved changes.
    /// </summary>
    /// <returns><c>true</c> when there are unsaved changes; otherwise <c>false</c>.</returns>
    public bool HasUnsavedChanges() => _hasUnsavedChanges;

    /// <summary>
    /// Persists pending changes to the underlying context and resets the modified state.
    /// </summary>
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        if (_hasUnsavedChanges && _activities != null)
        {
            await _context.SaveActivitiesAsync(_activities, cancellationToken);
            _timestamps = null;
            _hasUnsavedChanges = false;
        }
    }

    /// <summary>
    /// Updates an existing activity in the repository when the id matches.
    /// </summary>
    /// <param name="activity">The activity containing updated values.</param>
    public void UpdateActivity(StravaActivity activity)
    {
        var match = EnsureActivities().Index().FirstOrDefault(a => a.Item.Id == activity.Id);
        if (match != default)
        {
            EnsureActivities()[match.Index] = activity;
            MarkAsModified();
        }
    }

    private async Task<IList<StravaActivity>> EnsureActivitiesAsync(CancellationToken cancellationToken)
    {
        if (_activities == null)
        {
            _activities = await _context.LoadActivitiesAsync(cancellationToken);
            MarkAsModified();
        }
        return _activities;
    }

    private IList<StravaActivity> EnsureActivities()
    {
        _activities ??= EnsureActivitiesAsync(CancellationToken.None).GetAwaiter().GetResult();
        return _activities;
    }


    /// <summary>
    /// Marks the repository state as modified (unsaved changes present).
    /// </summary>
    private void MarkAsModified() => _hasUnsavedChanges = true;
}

using Microsoft.Extensions.DependencyInjection;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Client
{
    /// <summary>
    /// Defines the operations required by a repository context that can load and persist
    /// <see cref="StravaActivity"/> instances and provide repository metadata.
    /// </summary>
    public interface IStravaActivityRepositoryContext
    {
        /// <summary>
        /// Loads activities from the underlying store or source.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
        /// <returns>A list of <see cref="StravaActivity"/> instances.</returns>
        Task<IList<StravaActivity>> LoadActivitiesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns metadata about the repository, including the last save time and the most
        /// recent activity time known to the repository.
        /// </summary>
        /// <returns>
        /// A tuple containing <c>LastSavedTime</c> and <c>LastActivityTime</c>.
        /// </returns>
        (DateTime LastSavedTime, DateTime LastActivityTime) Metadata();

        /// <summary>
        /// Persists the provided activities to the underlying store.
        /// </summary>
        /// <param name="activities">The activities to persist.</param>
        /// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
        /// <returns>A task that resolves to <c>true</c> when save succeeds.</returns>
        Task<bool> SaveActivitiesAsync(IEnumerable<StravaActivity> activities, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Default implementation of <see cref="IStravaActivityRepositoryContext"/> that stores activities
    /// in a local JSON file and can fetch new activities via a provided fetch delegate.
    /// </summary>
    internal class StravaActivityRepositoryContext : IStravaActivityRepositoryContext
    {
        private static readonly string FILENAME = "activities.json";
        private DateTime _lastSavedTime;
        private DateTime _lastActivityTime;
        private readonly Func<DateTime, Task<IEnumerable<SummaryActivity>>> _fetchActivities;
        private readonly IStravaClient? _stravaClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="StravaActivityRepositoryContext"/> class.
        /// </summary>
        /// <param name="fetchActivities">A function that retrieves <see cref="SummaryActivity"/> instances that occurred after the specified date.</param>
        public StravaActivityRepositoryContext(Func<DateTime, Task<IEnumerable<SummaryActivity>>> fetchActivities)
        {
            _fetchActivities = fetchActivities;
            _lastSavedTime = File.Exists(FILENAME)
                ? File.GetLastWriteTimeUtc(FILENAME)
                : DateTime.MinValue;
        }

        public StravaActivityRepositoryContext(IServiceProvider services)

        {
            _stravaClient = services.GetRequiredService<IStravaClient>();
            _fetchActivities = FetchActivitiesFromStravaClient;
            _lastSavedTime = File.Exists(FILENAME)
                ? File.GetLastWriteTimeUtc(FILENAME)
                : DateTime.MinValue;
        }

        private async Task<IEnumerable<SummaryActivity>> FetchActivitiesFromStravaClient(DateTime since)
        {
            var client = _stravaClient ?? throw new Exception("Strava client not initialized.");
            var activitiesApi = client.ActivitiesApi();
            DateTime? sinceDate = since == DateTime.MinValue
                ? null
                : since;
            var activities = await activitiesApi.GetActivitiesAsync(after: sinceDate, perPage: 200);
            if (activities.Success && activities.Data != null)
            {
                return activities.Data;
            }
            return [];
        }

        /// <summary>
        /// Loads activities from the JSON file and merges any newly fetched activities from the remote source.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that may be used to cancel the operation.</param>
        /// <returns>A list of <see cref="StravaActivity"/> instances ordered by discovery.</returns>
        public async Task<IList<StravaActivity>> LoadActivitiesAsync(CancellationToken cancellationToken)
        {
            List<StravaActivity> result = [];
            if (File.Exists(FILENAME))
            {
                var json = await File.ReadAllTextAsync(FILENAME, cancellationToken);
                var success = StravaSerializer.TryDeserialize<List<SummaryActivity>>(json, out var activities);
                if (activities != null && activities.Count > 0)
                {
                    _lastActivityTime = activities.Max(a => a.StartDate);
                    result = [.. activities.Select(a => new StravaActivity(a))];
                }
            }
            // Fetch new activities since last saved time
            var newActivities = await _fetchActivities(_lastActivityTime);
            foreach (var activity in newActivities)
            {
                result.Add(new StravaActivity(activity));
                if (activity.StartDate > _lastActivityTime)
                {
                    _lastActivityTime = activity.StartDate;
                }
            }
            return result;
        }

        /// <summary>
        /// Returns repository metadata including the last saved time and the most recent activity time.
        /// </summary>
        /// <returns>A tuple (<c>LastSavedTime</c>, <c>LastActivityTime</c>).</returns>
        public (DateTime LastSavedTime, DateTime LastActivityTime) Metadata()
        {
            if (_lastActivityTime == DateTime.MinValue)
            {
                // Load activities to determine last activity time
                LoadActivitiesAsync(CancellationToken.None).GetAwaiter().GetResult();
            }
            return (_lastSavedTime, _lastActivityTime);
        }

        /// <summary>
        /// Saves the provided activities to the JSON file in chronological order.
        /// </summary>
        /// <param name="activities">Activities to persist.</param>
        /// <param name="cancellationToken">A token to cancel the save operation.</param>
        /// <returns>A task that resolves to <c>true</c> when the save completes successfully.</returns>
        public async Task<bool> SaveActivitiesAsync(IEnumerable<StravaActivity> activities, CancellationToken cancellationToken = default)
        {
            var summaryActivities = activities
                .Select(a => a.SummaryActivity)
                .OrderBy(a => a.StartDate);
            using var stream = File.Create(FILENAME);
            await StravaSerializer.SerializeAsync<IEnumerable<SummaryActivity>>(stream, summaryActivities, cancellationToken);
            _lastSavedTime = DateTime.UtcNow;
            return true;
        }
    }
}
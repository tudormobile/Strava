using System.IO;
using System.Windows;
using System.Windows.Input;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;
using Tudormobile.Strava.UI.Views;

namespace StravaLogin.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ICommand
    {
        private StravaAuthorization? _authorization;
        private StravaSession? _session;
        private readonly string _filename = "athlete.json";
        private readonly MainViewModel _viewModel = new();

        public event EventHandler? CanExecuteChanged;

        protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        protected override void OnStartup(StartupEventArgs e)
        {
            _authorization = new StravaAuthorization()
            {
                ClientId = Environment.GetEnvironmentVariable("STRAVA_CLIENT_ID") ?? "36431",
                ClientSecret = Environment.GetEnvironmentVariable("STRAVA_CLIENT_SECRET") ?? "badsecret",
                RefreshToken = Environment.GetEnvironmentVariable("STRAVA_REFRESH_TOKEN") ?? string.Empty,
                AccessToken = Environment.GetEnvironmentVariable("STRAVA_ACCESS_TOKEN") ?? string.Empty,
            };

            _viewModel.Athlete = File.Exists("athlete.json") ? Athlete.FromJson(File.ReadAllText(_filename)) : Athlete.Empty();
            _viewModel.LoginCommand = this;
            _viewModel.StatusMessage = "Login required.";

            MainWindow = new MainWindow
            {
                DataContext = _viewModel,
                Title = "Strava Login Sample"
            };
            MainWindow.Show();

            _ = startSession(_authorization);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            File.WriteAllText(_filename, _viewModel.Athlete!.ToJson());
            // Save the data
            if (_session != null)
            {
                Environment.SetEnvironmentVariable("STRAVA_ACCESS_TOKEN", _session.Authorization.AccessToken, EnvironmentVariableTarget.User);
                Environment.SetEnvironmentVariable("STRAVA_REFRESH_TOKEN", _session.Authorization.RefreshToken, EnvironmentVariableTarget.User);
            }

            base.OnExit(e);
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            // Login...
            var loginWindow = new LoginWindow()
            {
                DataContext = new
                {
                    Scope = AuthorizationScope.READ,
                    Authorization = _authorization,
                    LoggedInAthlete = new AthleteId(),
                },
            };
            if (loginWindow.ShowDialog() == true)
            {
                _ = startSession(loginWindow.Authorization, loginWindow.LoggedInAthlete?.Id ?? 0L);
                OnCanExecuteChanged();
            }
        }

        private async Task startSession(StravaAuthorization authorization, long? id = 0)
        {
            _session = new StravaSession(authorization);
            if (!_session.IsAuthenticated)
            {
                await _session.RefreshAsync();
            }
            if (_session.IsAuthenticated)
            {
                _viewModel.StatusMessage = "Loading acivities ...";

                var api = _session.CreateApi();
                var result = await api.GetAthlete(id);
                if (result.Success)
                {
                    _viewModel.Athlete = result.Data;
                }
                var activities = await api.GetActivities(DateTime.Now, DateTime.Now.AddDays(-360));
                if (activities.Success)
                {
                    activities.Data?.ForEach(a => _viewModel.Activities.Add(a));
                }
                _viewModel.StatusMessage = $"{_viewModel.Activities.Count:N0} activities found.";
            }
        }
    }

}

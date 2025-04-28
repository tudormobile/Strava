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
        private StravaSession? _session;
        private string _filename = "athlete.json";
        private MainViewModel _viewModel = new MainViewModel();

        public event EventHandler? CanExecuteChanged;

        protected override void OnStartup(StartupEventArgs e)
        {
            _viewModel.Athlete = File.Exists("athlete.json") ? Athlete.FromJson(File.ReadAllText(_filename)) : Athlete.Empty();
            _viewModel.LoginCommand = this;
            MainWindow = new MainWindow() { DataContext = _viewModel };
            MainWindow.Title = "Strava Login Sample";
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            File.WriteAllText(_filename, _viewModel.Athlete!.ToJson());
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
                    Authorization = new StravaAuthorization()
                    {
                        ClientId = Environment.GetEnvironmentVariable("STRAVA_CLIENT_ID") ?? "36431",
                        ClientSecret = Environment.GetEnvironmentVariable("STRAVA_CLIENT_SECRET") ?? "badsecret",
                    },
                    LoggedInAthlete = new AthleteId(),
                },
            };
            if (loginWindow.ShowDialog() == true)
            {
                _ = startSession(loginWindow.Authorization, loginWindow.LoggedInAthlete?.Id ?? 0L);
            }
        }

        private async Task startSession(StravaAuthorization authorization, long id)
        {
            _session = new StravaSession(authorization);
            if (!_session.IsAuthenticated)
            {
                await _session.RefreshAsync();
            }
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
        }
    }

}

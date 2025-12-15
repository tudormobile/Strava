using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tudormobile.Strava;
using Tudormobile.Strava.Api;
using Tudormobile.Strava.Model;
using Tudormobile.Strava.UI.ViewModels;
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

        protected override async void OnStartup(StartupEventArgs e)
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
            _viewModel.EditCommand = this;
            _viewModel.StatusMessage = "Login required.";

            MainWindow = new MainWindow
            {
                DataContext = _viewModel,
                Title = "Strava Login Sample"
            };
            MainWindow.Show();

            try
            {
                await StartSession(_authorization);
            }
            catch (Exception ex)
            {
                _viewModel.StatusMessage = $"Error: {ex.Message}";
            }
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
            if (parameter is SummaryActivity activity)
            {
                var w = new Window() { SizeToContent = SizeToContent.WidthAndHeight };
                var m = new ActivityViewModel(new DetailedActivity(activity))
                {
                    DoneCommand = new DoneCommand(w),
                    UpdateCommand = new UpdateCommand(w, _session),
                };
                w.Content = new ActivityEditView()
                {
                    DataContext = m
                };
                w.ShowDialog();
            }
            else
            {
                // Login...
                var loginWindow = new LoginWindow()
                {
                    DataContext = new
                    {
                        Scope = AuthorizationScope.READ,
                        Authorization = _authorization,
                    },
                };
                if (loginWindow.ShowDialog() == true)
                {
                    _ = StartSession(loginWindow.Authorization);
                    OnCanExecuteChanged();
                }
            }
        }

        private async Task StartSession(StravaAuthorization authorization, long? id = 0)
        {
            _session = new StravaSession(authorization);
            if (!_session.IsAuthenticated)
            {
                await _session.RefreshAsync();
            }
            if (_session.IsAuthenticated)
            {
                _viewModel.StatusMessage = "Loading acivities ...";

                var api = _session.ActivitiesApi();
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

    public class UpdateCommand : ICommand
    {
        private readonly Window _window;
        private readonly StravaSession? _session;

        public UpdateCommand(Window w, StravaSession? session)
        {
            this._window = w;
            this._session = session;
        }

        public event EventHandler? CanExecuteChanged;
        protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (((Control)_window.Content).DataContext is ActivityViewModel activity)
            {
                var updatableActivity = new UpdatableActivity()
                {
                    Commute = activity.Commute,
                    Trainer = activity.Trainer,
                    Description = activity.Description,
                    Name = activity.Name,
                    SportType = activity.SportType,
                };
                var api = _session!.ActivitiesApi();
                _window.Close();
                api.UpdateActivity(activity.Id, updatableActivity).ContinueWith(async t =>
                {
                    ApiResult<DetailedActivity> result = await t;
                    _window.Dispatcher.Invoke(() =>
                    {
                        if (result.Success)
                        {
                            //activity.Athlete = result.Data.Athlete;
                            //activity.Distance = result.Data.Distance;
                            //activity.ElapsedTime = result.Data.ElapsedTime;
                            //activity.MovingTime = result.Data.MovingTime;
                            //activity.StartDate = result.Data.StartDate;
                            //activity.TotalElevationGain = result.Data.TotalElevationGain;
                            //activity.Type = result.Data.Type;
                            //activity.WorkoutType = result.Data.WorkoutType;
                        }
                        else
                        {
                            MessageBox.Show($"Error: {result.Error!.Message}");
                        }
                    });
                });
            }
        }
    }
    public class DoneCommand : ICommand
    {
        private readonly Window _window;

        public DoneCommand(Window w)
        {
            this._window = w;
        }

        public event EventHandler? CanExecuteChanged;
        protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => _window.Close();
    }

}

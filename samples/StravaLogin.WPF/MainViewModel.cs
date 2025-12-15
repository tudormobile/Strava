using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Tudormobile.Strava.Model;

namespace StravaLogin.WPF
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Athlete? _athlete;
        private string? _statusMessage;
        public ICommand? LoginCommand { get; set; }
        public ICommand? EditCommand { get; set; }
        public SummaryActivity Activity { get; set; } = new SummaryActivity()
        {
            SportType = "AlpineSki",
            Name = "Afternoon Ski Session",
            Distance = 10123,
            ElapsedTime = 1239945,
            StartDate = DateTime.Now,
            TotalElevationGain = 456,
            AverageSpeed = 7.15264
        };
        public static string[] Sports
        {
            get => [
        "AlpineSki",
        "BackcountrySki",
        "Badminton",
        "Canoeing",
        "Crossfit",
        "EBikeRide",
        "Elliptical",
        "EMountainBikeRide",
        "Golf",
        "GravelRide",
        "Handcycle",
        "HighIntensityIntervalTraining",
        "Hike",
        "IceSkate",
        "InlineSkate",
        "Kayaking",
        "Kitesurf",
        "MountainBikeRide",
        "NordicSki",
        "Pickleball",
        "Pilates",
        "Racquetball",
        "Ride",
        "RockClimbing",
        "RollerSki",
        "Rowing",
        "Run",
        "Sail",
        "Skateboard",
        "Snowboard",
        "Snowshoe",
        "Soccer",
        "Squash",
        "StairStepper",
        "StandUpPaddling",
        "Surfing",
        "Swim",
        "TableTennis",
        "Tennis",
        "TrailRun",
        "Velomobile",
        "VirtualRide",
        "VirtualRow",
        "VirtualRun",
        "Walk",
        "WeightTraining",
        "Wheelchair",
        "Windsurf",
        "Workout",
        "Yoga"];
        }
        public Athlete? Athlete
        {
            get => _athlete;
            set
            {
                if (_athlete != value)
                {
                    _athlete = value;
                    OnPropertyChanged(nameof(Athlete));
                }
            }
        }
        public string? StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }
        public ObservableCollection<SummaryActivity> Activities { get; private set; } = [];
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

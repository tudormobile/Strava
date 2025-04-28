using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Tudormobile.Strava.Model;

namespace StravaLogin.WPF
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Athlete? _athlete;
        public ICommand LoginCommand { get; set; }
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
        public ObservableCollection<SummaryActivity> Activities { get; private set; } = new();
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

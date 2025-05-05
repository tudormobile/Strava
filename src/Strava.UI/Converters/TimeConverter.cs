using System.Globalization;
using System.Windows.Data;
using Tudormobile.Strava.Model;
using Tudormobile.Strava.UI.Views;

namespace Tudormobile.Strava.UI.Converters;

public class TimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var seconds = value switch
        {
            SummaryActivity activity => activity?.ElapsedTime ?? 0.0,
            ActivityView view => (view.Activity == null ? (view.DataContext as SummaryActivity)?.ElapsedTime : view.Activity?.ElapsedTime) ?? 0.0,
            TimeSpan timeSpan => timeSpan.TotalSeconds,
            long l => (double)value,
            int i => (double)value,
            double d => value,
            string s when double.TryParse(s, out var d) => d,
            _ => 0.0
        };
        var ts = TimeSpan.FromSeconds((double)seconds);
        return seconds switch
        {
            < 60.0 => $"{ts.TotalSeconds:F2}s",
            < 3600.0 => $"{ts.Minutes}m{ts.Seconds}s",
            < 86400.0 => $"{ts.Hours}h{ts.Minutes}m",
            _ => $"{ts.Days}d{ts.Hours}h{ts.Minutes}m"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

using System.Globalization;
using System.Windows.Data;
using Tudormobile.Strava.Model;
using Tudormobile.Strava.UI.Views;

namespace Tudormobile.Strava.UI.Converters;

public class SpeedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var useMeters = parameter?.ToString() == "meters";
        var data = value switch
        {
            SummaryActivity activity => (activity?.AverageSpeed ?? 0.0, false),
            ActivityView view => ((view.Activity == null ? (view.DataContext as SummaryActivity)?.AverageSpeed : view.Activity?.AverageSpeed) ?? 0.0, view.Units == "meters"),
            double d => (d, false),
            string s when double.TryParse(s, out var d) => (d, false),
            _ => (0.0, false)
        };
        var factor = data.Item2 || parameter?.ToString() == "meters" ? 3.6 : 2.23694;
        return factor == 3.6
            ? $"{data.Item1 * factor:F1}km/h"
            : $"{data.Item1 * factor:F1}mph";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

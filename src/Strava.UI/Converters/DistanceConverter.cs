using System.Globalization;
using System.Windows.Data;
using Tudormobile.Strava.Model;
using Tudormobile.Strava.UI.Views;

namespace Tudormobile.Strava.UI.Converters;

public class DistanceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var useMeters = false;
        var distance = 0.0;
        if (value is SummaryActivity activity)
        {
            useMeters = parameter?.ToString() == "meters";
            distance = activity?.Distance ?? 0;
        }
        else if (value is ActivityView view)
        {
            useMeters = parameter?.ToString() == "meters" || view.Units == "meters";
            distance = (view.Activity == null ? (view.DataContext as SummaryActivity)?.Distance : view.Activity?.Distance) ?? 0;
        }
        else if (value is double)
        {
            useMeters = parameter?.ToString() == "meters";
            distance = (double)value;
        }
        else if (value is string)
        {
            useMeters = parameter?.ToString() == "meters";
            distance = (double)(System.Convert.ChangeType(value, typeof(double)) ?? 0.0);
        }
        else
        {
            return "--";
        }
        if (useMeters)
        {
            return distance > 1000 ? $"{distance / 1000:F1}km" : (object)$"{distance}m";
        }
        else
        {
            distance = distance * 3.28084;
            return distance > 5280 ? $"{distance / 5280:F1}mi" : (object)$"{distance:F1}ft";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

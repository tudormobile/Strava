using System.Globalization;
using System.Windows.Data;
using Tudormobile.Strava.Model;
using Tudormobile.Strava.UI.Views;

namespace Tudormobile.Strava.UI.Converters;

/// <summary>  
/// Converts distance values to a formatted string representation based on the specified unit (meters or feet).  
/// </summary>  
public class DistanceConverter : IValueConverter
{
    /// <summary>  
    /// Converts a distance value to a formatted string representation.  
    /// </summary>  
    /// <param name="value">The distance value to convert. Can be of type <see cref="SummaryActivity"/>, <see cref="ActivityView"/>, <see cref="double"/>, or <see cref="string"/>.</param>  
    /// <param name="targetType">The target type of the binding (not used).</param>  
    /// <param name="parameter">An optional parameter specifying the unit ("meters" or "feet").</param>  
    /// <param name="culture">The culture information (optional).</param>  
    /// <returns>A formatted string representing the distance in the specified unit.</returns>  
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo? culture = null)
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
            return distance > 1000 ? $"{distance / 1000:F1}km" : (object)$"{distance:F1}m";
        }
        else
        {
            distance = distance * 3.28084;
            return distance > 5280 ? $"{distance / 5280:F1}mi" : (object)$"{distance:F1}ft";
        }
    }

    /// <summary>  
    /// Converts a formatted string representation of a distance back to its original value.  
    /// </summary>  
    /// <param name="value">The formatted string to convert back.</param>  
    /// <param name="targetType">The target type of the binding (not used).</param>  
    /// <param name="parameter">An optional parameter specifying the unit ("meters" or "feet").</param>  
    /// <param name="culture">The culture information.</param>  
    /// <returns>Throws a <see cref="NotImplementedException"/> as this method is not implemented.</returns>  
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture = null)
    {
        throw new NotImplementedException();
    }
}

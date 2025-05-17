using System.Globalization;
using System.Windows.Data;
using Tudormobile.Strava.Model;
using Tudormobile.Strava.UI.Views;

namespace Tudormobile.Strava.UI.Converters;

/// <summary>
/// Converts speed values to formatted string representations based on the specified unit.
/// </summary>
public class SpeedConverter : IValueConverter
{
    /// <summary>
    /// Converts a speed value to a formatted string representation based on the specified unit.
    /// </summary>
    /// <param name="value">The value to convert. Can be a <see cref="SummaryActivity"/>, <see cref="ActivityView"/>, <see cref="double"/>, or <see cref="string"/>.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use. Specify "meters" to use metric units.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A formatted string representing the speed in either km/h or mph.</returns>
    /// <exception cref="NotSupportedException">Thrown if the value type is not supported.</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo? culture = null)
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

    /// <summary>
    /// Converts a value back to its original type. This method is not implemented.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>
    /// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

using System.Globalization;
using System.Windows.Data;
using Tudormobile.Strava.Model;
using Tudormobile.Strava.UI.Views;

namespace Tudormobile.Strava.UI.Converters;

/// <summary>
/// Converts time-related values into human-readable string representations.
/// </summary>
public class TimeConverter : IValueConverter
{
    /// <summary>
    /// Converts a time-related value into a formatted string representation.
    /// </summary>
    /// <param name="value">The value to convert. Can be a SummaryActivity, ActivityView, TimeSpan, or numeric type.</param>
    /// <param name="targetType">The target type of the binding operation. Not used in this implementation.</param>
    /// <param name="parameter">Optional parameter for the converter. Not used in this implementation.</param>
    /// <param name="culture">The culture to use in the converter. Not used in this implementation.</param>
    /// <returns>A formatted string representation of the time value.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo? culture = null)
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

    /// <summary>
    /// Converts a formatted string representation of time back into its original value.
    /// </summary>
    /// <param name="value">The value to convert back. Not implemented in this converter.</param>
    /// <param name="targetType">The target type of the binding operation. Not used in this implementation.</param>
    /// <param name="parameter">Optional parameter for the converter. Not used in this implementation.</param>
    /// <param name="culture">The culture to use in the converter. Not used in this implementation.</param>
    /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>
    /// <exception cref="NotImplementedException">Thrown because this method is not implemented.</exception>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        throw new NotImplementedException();
    }
}

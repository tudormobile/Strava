using System.Globalization;
using System.Windows.Data;

namespace Tudormobile.Strava.UI.Converters;

/// <summary>  
/// A value converter that adds a specified value (parameter) to the input value.  
/// </summary>  
public class AddConverter : IValueConverter
{
    /// <summary>  
    /// Converts the input value by adding the specified parameter to it.  
    /// </summary>  
    /// <param name="value">The input value to be converted.</param>  
    /// <param name="targetType">The target type of the conversion (not used).</param>  
    /// <param name="parameter">The value to add to the input value.</param>  
    /// <param name="culture">The culture information (not used).</param>  
    /// <returns>The result of adding the parameter to the input value.</returns>  
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return System.Convert.ToDouble(value ?? 0) + double.Parse(parameter?.ToString() ?? "0");
    }

    /// <summary>  
    /// This method is not implemented.  
    /// </summary>  
    /// <param name="value">The value to be converted back (not used).</param>  
    /// <param name="targetType">The target type of the conversion (not used).</param>  
    /// <param name="parameter">The parameter for the conversion (not used).</param>  
    /// <param name="culture">The culture information (not used).</param>  
    /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>  
    /// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>  
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

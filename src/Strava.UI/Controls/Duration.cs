using System.Windows;

namespace Tudormobile.Strava.UI.Controls;

/// <summary>
/// Represents a duration measurement control that inherits from the <see cref="Measurement"/> class.
/// </summary>
public class Duration : Measurement
{
    static Duration()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Duration), new FrameworkPropertyMetadata(typeof(Duration)));
    }
}

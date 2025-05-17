using System.Windows;

namespace Tudormobile.Strava.UI.Controls;

/// <summary>
/// Represents a speed measurement control that inherits from the <see cref="Measurement"/> class.
/// </summary>
public class Speed : Measurement
{
    static Speed()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Speed), new FrameworkPropertyMetadata(typeof(Speed)));
    }
}

using System.Windows;
using System.Windows.Controls;
using Tudormobile.Strava.UI.Converters;

namespace Tudormobile.Strava.UI.Controls;

/// <summary>
/// Specifies the preferred unit of measurement for distance.
/// </summary>
/// <remarks>This enumeration is used to indicate whether distances should be represented in feet or
/// meters.</remarks>
public enum MeasurementPreference
{
    /// <summary>
    /// Represents a measurement in feet and speed in mph.
    /// </summary>
    feet = 0,
    /// <summary>
    /// Represents a measurement in meters and speed in km/hr.
    /// </summary>  
    meters = 1
}

/// <summary>
/// Represents a UI element that displays a measurement value, with support for unit conversion based on user
/// preferences.
/// </summary>
/// <remarks>The <see cref="Measurement"/> class extends <see cref="TextBlock"/> to provide functionality for
/// displaying measurement values in a user-preferred unit system. The measurement value is always stored internally in
/// meters, but it can be displayed in different units (e.g., feet, kilometers) based on the attached <see
/// cref="PreferenceProperty"/>.</remarks>
public class Measurement : TextBlock
{
    private static SpeedConverter _speedConverter = new SpeedConverter();
    private static DistanceConverter _distanceConverter = new DistanceConverter();
    private static TimeConverter _timeConverter = new TimeConverter();
    static Measurement()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Measurement), new FrameworkPropertyMetadata(typeof(Measurement)));
    }

    /// <summary>
    /// Retrieves the <see cref="MeasurementPreference"/> value associated with the specified <see
    /// cref="DependencyObject"/>.
    /// </summary>
    /// <param name="obj">The <see cref="DependencyObject"/> from which to retrieve the preference value.</param>
    /// <returns>The <see cref="MeasurementPreference"/> value currently set on the specified <paramref name="obj"/>.</returns>
    public static MeasurementPreference GetPreference(DependencyObject obj)
        => (MeasurementPreference)obj.GetValue(PreferenceProperty);

    /// <summary>
    /// Sets the measurement preference for the specified dependency object.
    /// </summary>
    /// <param name="obj">The <see cref="DependencyObject"/> for which the measurement preference is being set. Cannot be <see
    /// langword="null"/>.</param>
    /// <param name="value">The <see cref="MeasurementPreference"/> value to set.</param>
    public static void SetPreference(DependencyObject obj, MeasurementPreference value)
        => obj.SetValue(PreferenceProperty, value);

    /// <summary>
    /// Identifies the attached property that specifies the measurement preference for a control.
    /// </summary>
    /// <remarks>This attached property allows controls to specify whether measurements should be displayed in
    /// feet or meters. The default value is <see cref="MeasurementPreference.feet"/>.</remarks>
    public static readonly DependencyProperty PreferenceProperty = DependencyProperty
        .RegisterAttached("Preference",
        typeof(MeasurementPreference),
        typeof(Measurement),
        new FrameworkPropertyMetadata(
            defaultValue: MeasurementPreference.feet,
            flags: FrameworkPropertyMetadataOptions.AffectsRender,
            propertyChangedCallback: OnValueChanged
        ));

    /// <summary>
    /// The dependency property for the measurement value.
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty
        .Register(nameof(Value),
            typeof(double),
            typeof(Measurement),
            new PropertyMetadata(0.0, OnValueChanged));

    /// <summary>
    /// The measurement value, in meters.
    /// </summary>
    /// <remarks>
    /// This value is always in meters, regardless of the preference set.
    /// </remarks>
    public double Value
    {
        get { return (double)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Speed speed)
        {
            var p = Measurement.GetPreference(d);
            speed.Text = (string)_speedConverter.Convert(speed.Value, typeof(string), p, null);
        }
        else if (d is Measurement measurement)
        {
            var p = Measurement.GetPreference(d);
            measurement.Text = (string)_distanceConverter.Convert(measurement.Value, typeof(string), p, null);
        }
    }

}
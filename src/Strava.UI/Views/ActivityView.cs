using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.UI.Views
{
    /// <summary>
    /// UI element to display an activity.
    /// </summary>
    /// <remarks>
    /// The ActivityView is a custom control that is used to display a Strava summary actuvity. The default style
    /// includes a template that provides a simply detailed view of the activity. Additional styles are found in the
    /// Styles dictionary.
    /// </remarks>
    public class ActivityView : Control
    {
        static ActivityView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ActivityView), new FrameworkPropertyMetadata(typeof(ActivityView)));
        }

        /// <summary>
        /// A summary activity representing the data to display.
        /// </summary>
        public SummaryActivity? Activity
        {
            get { return (SummaryActivity?)GetValue(ActivityProperty); }
            set { SetValue(ActivityProperty, value); }
        }

        /// <summary>
        /// The dependency property for the activity.
        /// </summary>
        public static readonly DependencyProperty ActivityProperty = DependencyProperty
            .Register(nameof(Activity),
                typeof(SummaryActivity),
                typeof(ActivityView),
                new PropertyMetadata(null));

        /// <summary>
        /// The secondary color of the activity view.
        /// </summary>
        public Brush? Secondary
        {
            get { return (Brush?)GetValue(SecondaryProperty); }
            set { SetValue(SecondaryProperty, value); }
        }

        /// <summary>
        /// The dependency property for the secondary color.
        /// </summary>
        public static readonly DependencyProperty SecondaryProperty = DependencyProperty
            .Register(nameof(Secondary),
                typeof(Brush),
                typeof(ActivityView),
                new PropertyMetadata(null));


        /// <summary>
        /// Preferred units (meters or feet) for the activity view.
        /// </summary>
        public string? Units
        {
            get { return (string)GetValue(UnitsProperty); }
            set { SetValue(UnitsProperty, value); }
        }

        /// <summary>
        /// The dependency property for the units.
        /// </summary>
        public static readonly DependencyProperty UnitsProperty = DependencyProperty
            .Register(nameof(Units),
                typeof(string),
                typeof(ActivityView),
                new PropertyMetadata(null));

    }
}

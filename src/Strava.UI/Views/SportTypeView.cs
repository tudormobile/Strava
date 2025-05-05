using System.Windows;
using System.Windows.Controls;

namespace Tudormobile.Strava.UI.Views
{
    /// <summary>
    /// Displays the sport type of an activity.
    /// </summary>
    public class SportTypeView : Control
    {
        private TextBlock? _tb;
        static SportTypeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SportTypeView), new FrameworkPropertyMetadata(typeof(SportTypeView)));
        }

        public override void OnApplyTemplate()
        {
            _tb = Template.FindName("tb", this) as TextBlock;
            _tb?.SetValue(VisibilityProperty, ShowText ? Visibility.Visible : Visibility.Hidden);

            base.OnApplyTemplate();
        }

        public bool ShowText
        {
            get { return (bool)GetValue(ShowTextProperty); }
            set { SetValue(ShowTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowTextProperty = DependencyProperty
            .Register("ShowText",
            typeof(bool),
            typeof(SportTypeView),
            new PropertyMetadata(false, (s, e) =>
            {
                var control = (SportTypeView)s;
                control._tb?.SetValue(VisibilityProperty, (bool)e.NewValue ? Visibility.Visible : Visibility.Hidden);
            }));


    }
}

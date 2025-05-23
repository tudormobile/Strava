using System.Windows.Controls;

namespace Tudormobile.Strava.UI.Views
{
    /// <summary>
    /// Interaction logic for ActivityEditView.xaml
    /// </summary>
    public partial class ActivityEditView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityEditView"/> class.
        /// </summary>
        public ActivityEditView()
        {
            InitializeComponent();
            updateButton.Loaded += (s, e) =>
            {
                updateButton.IsEnabled = false;
            };
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateButton.IsEnabled = true;
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            updateButton.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            updateButton.IsEnabled = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateButton.IsEnabled = e.RemovedItems.Count > 0;
        }

        private void CheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            updateButton.IsEnabled = true;
        }
    }
}

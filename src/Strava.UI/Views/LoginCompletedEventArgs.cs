using System.Windows;

namespace Tudormobile.Strava.UI.Views
{
    /// <summary>
    /// Event arguments for the login completed event.
    /// </summary>
    public class LoginCompletedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="e">The routed event associated with this data.</param>
        /// <param name="success">(Optional) True if login was successful.</param>
        public LoginCompletedEventArgs(RoutedEvent e, bool? success = false) : base(e) { IsSuccess = success ?? false; }

        /// <summary>
        /// True if login was successful.
        /// </summary>
        public bool IsSuccess { get; init; } = false;
    }
}
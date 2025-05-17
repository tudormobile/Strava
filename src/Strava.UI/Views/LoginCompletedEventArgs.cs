using System.Windows;

namespace Tudormobile.Strava.UI.Views
{
    /// <summary>
    /// Login completed event arguments.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void LoginCompletedEventHandler(object sender, LoginCompletedEventArgs e);

    /// <summary>
    /// Event arguments for the login completed event used to indicate success or failure of a login attempt.
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
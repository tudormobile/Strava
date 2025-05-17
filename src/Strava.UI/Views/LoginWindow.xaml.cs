using System.Windows;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.UI.Views
{
    /// <summary>
    /// A window for user login.
    /// <para>
    /// This window contains a LoginView control hosted in a window that allows drag, resize, and close. Upon
    /// succesful login, the window will close and return the granted authorization. The DialogResult is set to
    /// indicate success or failure.
    /// </para>
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Granted Authorization.
        /// </summary>
        public StravaAuthorization Authorization
        {
            get => (StravaAuthorization)GetValue(AuthorizationProperty);
            set => SetValue(AuthorizationProperty, value);
        }

        /// <summary>
        /// Authorization Scope to request for login.
        /// </summary>
        public AuthorizationScope Scope
        {
            get => (AuthorizationScope)GetValue(ScopeProperty);
            set => SetValue(ScopeProperty, value);
        }

        /// <summary>
        /// Authorization Scope to request for login.
        /// </summary>
        public static readonly DependencyProperty ScopeProperty
            = DependencyProperty.Register("Scope", typeof(AuthorizationScope), typeof(LoginWindow), new PropertyMetadata(AuthorizationScope.READ));

        /// <summary>
        /// Granted Authorization.
        /// </summary>
        public static readonly DependencyProperty AuthorizationProperty
            = DependencyProperty.Register("Authorization", typeof(StravaAuthorization), typeof(LoginWindow), new PropertyMetadata(new StravaAuthorization()));

        /// <summary>
        /// Creates a new instance of the LoginWindow class.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            loginView.LoginCompleted += (s, e) =>
            {
                DialogResult = e.IsSuccess;
                Close();
            };
            base.OnApplyTemplate();
        }

        private void closeClick(object sender, RoutedEventArgs e)
            => this.Close();

        private void mouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => this.DragMove();

    }
}

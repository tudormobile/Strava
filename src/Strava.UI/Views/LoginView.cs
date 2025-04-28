using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.UI.Views
{
    /// <summary>
    /// Strava Login View
    /// </summary>
    /// <remarks>
    /// Allows the user to Authenticate with Strava.
    /// </remarks>
    public class LoginView : Control
    {
        private readonly string REDIRECT_URI = "http://localhost/exchange_token";
        private readonly string LOGIN_URI = "https://www.strava.com/oauth/authorize";
        private readonly string TOKEN_URI = "https://www.strava.com/oauth/token";

        private WebView2? _webView;
        private TextBlock? _messageView;

        static LoginView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoginView), new FrameworkPropertyMetadata(typeof(LoginView)));
        }

        /// <summary>
        /// Login completed event.
        /// </summary>
        public static readonly RoutedEvent LoginCompletedEvent = EventManager
            .RegisterRoutedEvent("LoginCompleted",
            RoutingStrategy.Bubble,
            typeof(LoginCompletedEventHandler),
            typeof(LoginView));

        /// <summary>
        /// Login completed event handler.
        /// </summary>
        public event LoginCompletedEventHandler LoginCompleted
        {
            add { AddHandler(LoginCompletedEvent, value); }
            remove { RemoveHandler(LoginCompletedEvent, value); }
        }

        /// <summary>
        /// Login completed event arguments.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        public delegate void LoginCompletedEventHandler(object sender, LoginCompletedEventArgs e);

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
        /// Logged-in Athlete Identifier.
        /// </summary>
        public AthleteId? LoggedInAthlete
        {
            get { return (AthleteId)GetValue(LoggedInAthleteProperty); }
            set { SetValue(LoggedInAthleteProperty, value); }
        }

        /// <summary>
        /// Logged-in Athlete Identifier Dependency Property.
        /// </summary>
        public static readonly DependencyProperty LoggedInAthleteProperty
            = DependencyProperty.Register("LoggedInAthlete", typeof(AthleteId), typeof(LoginView), new PropertyMetadata(null));

        /// <summary>
        /// Authorization Scope to request for login.
        /// </summary>
        public static readonly DependencyProperty ScopeProperty
            = DependencyProperty.Register("Scope", typeof(AuthorizationScope), typeof(LoginView), new PropertyMetadata(AuthorizationScope.READ));

        /// <summary>
        /// Granted Authorization.
        /// </summary>
        public static readonly DependencyProperty AuthorizationProperty
            = DependencyProperty.Register("Authorization", typeof(StravaAuthorization), typeof(LoginView), new PropertyMetadata(new StravaAuthorization()));

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            _webView = Template.FindName("webview", this) as WebView2;
            _messageView = Template.FindName("messageview", this) as TextBlock;
            if (_webView != null)
            {
                // Tap-into some important events
                _webView.NavigationStarting += webView_NavigationStarting;
                _webView.Loaded += webView_Loaded;
                _webView.NavigationCompleted += webView_NavigationCompleted;
            }
            base.OnApplyTemplate();
        }

        private void webView_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (!e.IsSuccess)
            {
                _webView!.Stop();
                _webView!.Visibility = Visibility.Collapsed;
                _messageView!.Visibility = Visibility.Visible;
                _messageView!.Text = $"Error: Status code = {e.HttpStatusCode}";

                // Dig error message out of content?

            }
        }

        private void webView_Loaded(object sender, RoutedEventArgs e)
        {
            var uri = new Uri($"{LOGIN_URI}?client_id={Authorization.ClientId}&response_type=code&redirect_uri={REDIRECT_URI}&approval_prompt=force&scope={HttpUtility.UrlEncode(Scope.ToString())}");
            _webView!.Source = uri;
        }

        private void webView_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (e.Uri.StartsWith(REDIRECT_URI))
            {
                e.Cancel = true;
                _webView!.Stop();
                _webView!.Visibility = Visibility.Collapsed;
                _messageView!.Visibility = Visibility.Visible;

                // Possible Replies:
                // http://localhost/exchange_token?state=&code=6bbb6a5630f954e560a3b8ab64c17a24d5a7fede&scope=read
                // http://localhost/exchange_token?state=&error=access_denied

                var query = HttpUtility.ParseQueryString(new Uri(e.Uri).Query);
                var code = query["code"];   // code to exchange for token
                var error = query["error"]; // error message
                var scope = query["scope"]; // scope granted

                // If we got the code and NO error...
                if (!string.IsNullOrEmpty(code) && string.IsNullOrEmpty(error))
                {
                    _ = exchangeCodeForToken(code!, scope);
                }
                else
                {
                    _messageView!.Text = "Login failed: " + error;
                    loginCompleted(success: false);
                }
            }
        }

        private async Task exchangeCodeForToken(string code, string? grantedScope)
        {
            using var client = new HttpClient();
            KeyValuePair<string, string>[] data =
                [
                    new ("client_id", Authorization.ClientId),
                            new ("client_secret", Authorization.ClientSecret),
                            new ("code", code),
                            new ("grant_type", "authorization_code"),
                        ];

            var content = new FormUrlEncodedContent(data);
            try
            {
                var response = await client.PostAsync(TOKEN_URI, content);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    this.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                            LoggedInAthlete = Authorization.UpdateFromResponse(ms);
                            _messageView!.Text = "Login successful!";
                            loginCompleted(success: true);
                        }
                        catch (Exception ex)
                        {
                            _messageView!.Text = "Login failed: " + ex.Message;
                            loginCompleted(success: false);
                        }
                    });
                }
                this.Dispatcher.Invoke(() =>
                {
                    _messageView!.Text = "Login failed: " + response.ReasonPhrase;
                });
            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    _messageView!.Text = "Login failed: " + ex.Message;
                    loginCompleted(success: false);
                });
            }
        }

        private void loginCompleted(bool success)
        {
            var args = new LoginCompletedEventArgs(LoginCompletedEvent, success);
            args.Source = this;
            RaiseEvent(args);
        }
    }
}

using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.UI.Views
{

    /// <summary>
    /// A UI control that allows the user to login to Strava.
    /// Allows the user to Authenticate with Strava. Internally, it uses the WebView2 control to display the Strava login page.
    /// </summary>
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
                _webView.NavigationStarting += WebView_NavigationStarting;
                _webView.Loaded += WebView_Loaded;
                _webView.NavigationCompleted += WebView_NavigationCompleted;
            }
            base.OnApplyTemplate();
        }

        private void WebView_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
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

        private void WebView_Loaded(object sender, RoutedEventArgs e)
        {
            var uri = new Uri($"{LOGIN_URI}?client_id={Authorization.ClientId}&response_type=code&redirect_uri={REDIRECT_URI}&approval_prompt=force&scope={HttpUtility.UrlEncode(Scope.ToString())}");
            _webView!.Source = uri;
        }

        private void WebView_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
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
                    _ = ExchangeCodeForToken(code!, scope);
                }
                else
                {
                    _messageView!.Text = "Login failed: " + error;
                    OnLoginCompleted(success: false);
                }
            }
        }

        private async Task ExchangeCodeForToken(string code, string? _ /* grantedScope */)
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
                            var jo = JsonObject.Parse(json);
                            var access_token = jo!["access_token"]?.GetValue<string>()!;
                            var refresh_token = jo!["refresh_token"]?.GetValue<string>()!;
                            var id = jo!["athlete"]?["id"]?.GetValue<long>() ?? 0;
                            var expires_at = jo!["expires_at"]?.GetValue<long>() ?? 0;

                            Authorization = new StravaAuthorization()
                            {
                                Id = id,
                            }.WithTokens(access_token, refresh_token, DateTime.UnixEpoch.AddSeconds(expires_at));
                            _messageView!.Text = "Login successful!";
                            OnLoginCompleted(success: true);
                        }
                        catch (Exception ex)
                        {
                            _messageView!.Text = "Login failed: " + ex.Message;
                            OnLoginCompleted(success: false);
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
                    OnLoginCompleted(success: false);
                });
            }
        }

        private void OnLoginCompleted(bool success)
        {
            var args = new LoginCompletedEventArgs(LoginCompletedEvent, success)
            {
                Source = this
            };
            OnLoginCompleted(args);
        }

        /// <summary>
        /// Raises the <see cref="LoginCompletedEvent"/> with the specified event arguments.
        /// </summary>
        /// <param name="e">The <see cref="LoginCompletedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnLoginCompleted(LoginCompletedEventArgs e)
        {
            RaiseEvent(e);
        }
    }
}

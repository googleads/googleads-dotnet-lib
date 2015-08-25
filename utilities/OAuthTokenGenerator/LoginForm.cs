// Copyright 2013, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Api.Ads.Common.Lib;

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Google.Api.Ads.Common.Utilities.OAuthTokenGenerator {

  /// <summary>
  /// The form to handle user login.
  /// </summary>
  public partial class LoginForm : Form {

    /// <summary>
    /// Simple placeholder class to use the abstract AppConfigBase class.
    /// </summary>
    private class SimpleAppConfig : AppConfigBase {
    }

    /// <summary>
    /// The Application configuration patch to be displayed to the user.
    /// </summary>
    private const string APP_CONFIG_PATCH = @"
<add key='AuthorizationMethod' value='OAuth2' />
<add key='OAuth2ClientId' value='{0}' />
<add key='OAuth2ClientSecret' value='{1}' />
<add key='OAuth2RefreshToken' value='{2}' />
";

    /// <summary>
    /// The application configuration instance.
    /// </summary>
    private AppConfig appConfig = new SimpleAppConfig();

    /// <summary>
    /// The address of the local server that receives the OAuth2 callback.
    /// </summary>
    private string LOCALHOST_ADDRESS = "http://localhost:8080/";

    /// <summary>
    /// The HTTP listener that runs the local server.
    /// </summary>
    private HttpListener newHttpListener;

    /// <summary>
    /// The OAuth2 provider for doing OAuth2 installed application flow.
    /// </summary>
    private OAuth2ProviderForApplications oAuth2;

    /// <summary>
    /// The server thread to listen to incoming web requests and handle them,
    /// so that the main UI thread isn't blocked.
    /// </summary>
    private Thread serverThread;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginForm"/> class.
    /// </summary>
    /// <param name="clientId">The OAuth2 client identifier.</param>
    /// <param name="clientSecret">The OAuth2 client secret.</param>
    /// <param name="scope">The OAuth2 scope.</param>
    public LoginForm(string clientId, string clientSecret, string scope) {
      InitializeComponent();
      startLocalServer();

      appConfig.OAuth2RedirectUri = LOCALHOST_ADDRESS;
      appConfig.OAuth2ClientId = clientId;
      appConfig.OAuth2ClientSecret = clientSecret;
      appConfig.OAuth2Scope = scope;

      oAuth2 = new OAuth2ProviderForApplications(appConfig);

      // Get the authorization url and open a browser.
      string authorizationUrl = oAuth2.GetAuthorizationUrl();
      loginBrowser.Navigate(authorizationUrl);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Closed" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> that contains
    /// the event data.</param>
    protected override void OnClosed(EventArgs e) {
      base.OnClosed(e);
      newHttpListener.Stop();
      serverThread.Join();
    }

    /// <summary>
    /// Starts the local server.
    /// </summary>
    private void startLocalServer() {
      // Start the server on localhost.
      newHttpListener = new System.Net.HttpListener();
      newHttpListener.Prefixes.Add(LOCALHOST_ADDRESS);
      newHttpListener.Start();

      // Start the listener.
      serverThread = new Thread(delegate() {
        while (true) {
          try {
            HttpListenerContext ctx = newHttpListener.GetContext();
            HandlePageRequest(ctx);
          } catch (HttpListenerException) {
            break;
          }
        }
      });
      serverThread.Start();
    }

    /// <summary>
    /// Handles the page request.
    /// </summary>
    /// <param name="context">The listener context.</param>
    private void HandlePageRequest(HttpListenerContext context) {
      string url = context.Request.Url.OriginalString;
      string authorizationCode = context.Request.QueryString["code"];
      oAuth2.FetchAccessAndRefreshTokens(authorizationCode);

      HttpListenerResponse response = context.Response;
      response.ContentType = "text/plain";

      using (StreamWriter textWriter = new StreamWriter(response.OutputStream)) {
        textWriter.Write("Copy the snippet shown below to your App.config/Web.config.\n\n");
        string responseText = string.Format(APP_CONFIG_PATCH, appConfig.OAuth2ClientId,
            appConfig.OAuth2ClientSecret, appConfig.OAuth2RefreshToken);
        textWriter.Write(responseText);
      }

      response.StatusCode = (int) HttpStatusCode.OK;
      response.StatusDescription = "OK";
      response.OutputStream.Close();
    }
  }
}
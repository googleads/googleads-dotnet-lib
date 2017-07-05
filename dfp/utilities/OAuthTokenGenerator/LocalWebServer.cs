// Copyright 2016, Google Inc. All Rights Reserved.
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

using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using Google.Api.Ads.Common.Lib;

namespace Google.Api.Ads.Common.Utilities.OAuthTokenGenerator {

  /// <summary>
  /// A local web server that listens and handles OAuth2 callback requests.
  /// </summary>
  public class LocalWebServer {

    /// <summary>
    /// Callback to be triggered when a refresh token is retrieved successfully.
    /// </summary>
    /// <param name="clientId">The OAuth2 client ID.</param>
    /// <param name="clientSecret">The OAuth2 client secret.</param>
    /// <param name="refreshToken">The OAuth2 refresh token.</param>
    public delegate void OnSuccessCallback(string clientId, string clientSecret,
        string refreshToken);

    /// <summary>
    /// Callback to be triggered when a refresh token could not be retrieved.
    /// </summary>
    /// <param name="message">The error message from the server.</param>
    public delegate void OnFailedCallback(string message);

    /// <summary>
    /// Callback to be triggered when a refresh token is retrieved successfully.
    /// </summary>
    private OnSuccessCallback onSuccess;

    /// <summary>
    /// Callback to be triggered when a refresh token could not be retrieved.
    /// </summary>
    private OnFailedCallback onFailed;

    /// <summary>
    /// Gets or sets the callback to be triggered when a refresh token is
    /// retrieved successfully.
    /// </summary>
    public OnSuccessCallback OnSuccess {
      get {
        return onSuccess;
      }
      set {
        onSuccess = value;
      }
    }

    /// <summary>
    /// Gets or sets the callback to be triggered when a refresh token could
    /// not be retrieved.
    /// </summary>
    public OnFailedCallback OnFailed {
      get {
        return onFailed;
      }
      set {
        onFailed = value;
      }
    }

    /// <summary>
    /// The application configuration instance.
    /// </summary>
    private AppConfig appConfig = new AppConfigBase();

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
    public LocalWebServer() {
    }

    /// <summary>
    /// Triggers the authentication flow.
    /// </summary>
    /// <param name="clientId">The OAuth2 client identifier.</param>
    /// <param name="clientSecret">The OAuth2 client secret.</param>
    /// <param name="scope">The OAuth2 scope.</param>
    public void TriggerAuthFlow(string clientId, string clientSecret, string scope) {
      appConfig.OAuth2RedirectUri = LOCALHOST_ADDRESS;
      appConfig.OAuth2ClientId = clientId;
      appConfig.OAuth2ClientSecret = clientSecret;
      appConfig.OAuth2Scope = scope;

      oAuth2 = new OAuth2ProviderForApplications(appConfig);

      // Get the authorization url and open a browser.
      string authorizationUrl = oAuth2.GetAuthorizationUrl();
      Process.Start(authorizationUrl);
    }

    /// <summary>
    /// Starts the local server.
    /// </summary>
    public void Start() {
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
    /// Stops the local server.
    /// </summary>
    public void Stop() {
      newHttpListener.Stop();
      serverThread.Join();
    }

    /// <summary>
    /// Handles the page request.
    /// </summary>
    /// <param name="context">The listener context.</param>
    private void HandlePageRequest(HttpListenerContext context) {
      HttpListenerResponse response = context.Response;

      // If the request is for /, then send back a blank text.
      if (context.Request.Url.LocalPath == "/") {
        bool result = HandleOAuth2Callback(context);
        response.ContentType = "text/plain";

        using (StreamWriter textWriter = new StreamWriter(response.OutputStream)) {
          if (result) {
            textWriter.Write("You have authorized successfully. You can close this window " +
                "and return to the main application to complete the process and copy your " +
                "credentials.\r\n");
          } else {
            textWriter.Write("OAuth2 authorization failed. Please close this window " +
                "and return to the main application to see the errors and fix them.\r\n");
          }
        }

        response.StatusCode = (int) HttpStatusCode.OK;
        response.StatusDescription = "OK";
      } else {
        // Send 404 for anything else.
        response.StatusCode = (int) HttpStatusCode.NotFound;
        response.StatusDescription = "NotFound";
      }
      response.OutputStream.Close();
    }

    /// <summary>
    /// Handles the OAuth2 callback.
    /// </summary>
    /// <param name="context">The web request context.</param>
    private bool HandleOAuth2Callback(HttpListenerContext context) {
      string url = context.Request.Url.OriginalString;
      string authorizationCode = context.Request.QueryString["code"];

      try {
        oAuth2.FetchAccessAndRefreshTokens(authorizationCode);

        if (onSuccess != null) {
          onSuccess(appConfig.OAuth2ClientId, appConfig.OAuth2ClientSecret,
              appConfig.OAuth2RefreshToken);
        }
        return true;
      } catch (AdsOAuthException e) {
        if (onFailed != null) {
          onFailed(e.Message);
        }
        return false;
      }
    }
  }
}

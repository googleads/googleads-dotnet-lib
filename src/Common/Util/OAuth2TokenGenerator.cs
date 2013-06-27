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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace Google.Api.Ads.Common.Util {
  /// <summary>
  /// This utility class generates the necessary configuration that allows you
  /// to configure your application to use OAuth2 as authentication mechanism
  /// when working with various Ads* API .NET libraries.
  /// </summary>
  public class OAuth2TokenGenerator {
    /// <summary>
    /// The App.config patch to be displayed to the user.
    /// </summary>
    private const string APP_CONFIG_PATCH = @"
<add key='AuthorizationMethod' value='OAuth2' />
<add key='OAuth2ClientId' value='{0}' />
<add key='OAuth2ClientSecret' value='{1}' />
<add key='OAuth2RefreshToken' value='{2}' />
";

    /// <summary>
    /// The usage string to be displayed to the user.
    /// </summary>
    private const string USAGE = @"
This application generates the necessary configuration that allows you to
configure your application to use OAuth2 as authentication mechanism when
working with various Ads* .NET client libraries. To use this application,

1) Navigate to https://code.google.com/apis/console/ and create a new project.
2) Click API Access link on the left side of the page and Create an OAuth2
   client ID. Select application type as Web Application. Click the link
   'More options' link next to 'Your site or hostname' and enter {0}
   as a redirect URI.
3) Build this project as msbuild Common.csproj /p:OutputType=""Exe""
4) Run this application. When prompted, enter the client ID and secret from the
   project you created above, as well as the OAuth2 scope of the API which you
   need to authenticate for. The standard list of OAuth2 scopes for various
   Ads* APIs are:

   AdWords API: https://adwords.google.com/api/adwords
   Doubleclick for Publishers API: https://www.google.com/apis/ads/publisher
   DoubleClick for Advertisers API: https://www.googleapis.com/auth/dfatrafficking

   You can find OAuth2 scopes for other Google APIs at
   https://developers.google.com/oauthplayground/

5) The application will open a browser window. Login using your AdWords API MCC
   credentials and grant access to your application.
6) The application will print your access and refresh tokens.
7) Copy the output from this application to your App.config/Web.config.
8) In case your refresh token is empty, navigate to
   https://accounts.google.com/b/0/IssuedAuthSubTokens?hl=en, find your
   application, revoke access and run this application again.
";

    /// <summary>
    /// The address for running local web server.
    /// </summary>
    private const string LOCALHOST_ADDRESS = "http://localhost:8080/";

    /// <summary>
    /// A minimal AppConfig class.
    /// </summary>
    class SimpleAppConfig : AppConfigBase {}

    /// <summary>
    /// Run the application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    static void Main(string[] args) {
       // Start a local http listener on localhost:8080
      HttpListener newHttpListener = new System.Net.HttpListener();
      newHttpListener.Prefixes.Add(LOCALHOST_ADDRESS);
      try {
        newHttpListener.Start();
      } catch (HttpListenerException e) {
        Console.WriteLine("Looks like {0} is blocked. Please open a command line prompt as " +
            "Administrator and run the following command: \n" +
            "netsh http add urlacl url={0} user=machine\\username\n" +
            "where machine\\username is your user account.\nThen re-run this application.",
            LOCALHOST_ADDRESS);
        return;
      }

      Console.WriteLine(USAGE, LOCALHOST_ADDRESS);
      // Create an app configuration object.

      SimpleAppConfig appConfig = new SimpleAppConfig();
      appConfig.OAuth2RedirectUri = LOCALHOST_ADDRESS;

      // Read the client ID, secret and scope from the user.
      appConfig.OAuth2Scope = AcceptInputWithRetry("Enter OAuth2 Scope (separate multiple " +
          "scopes with spaces): ");
      appConfig.OAuth2ClientId = AcceptInputWithRetry("Enter OAuth2 client ID: ");
      appConfig.OAuth2ClientSecret = AcceptInputWithRetry("Enter OAuth2 Client Secret: ");

      // Create the OAuth2 protocol handler and set it to the current user.
      OAuth2ProviderForApplications oAuth2 = new OAuth2ProviderForApplications(appConfig);

      // Get the authorization url and open a browser.
      string authorizationUrl = oAuth2.GetAuthorizationUrl();
      Process.Start(authorizationUrl);

      HttpListenerContext context = newHttpListener.GetContext();
      string authorizationCode = context.Request.QueryString["code"];
      newHttpListener.Stop();

      // Fetch the access and refresh tokens.
      oAuth2.FetchAccessAndRefreshTokens(authorizationCode);

      Console.WriteLine(APP_CONFIG_PATCH, appConfig.OAuth2ClientId, appConfig.OAuth2ClientSecret,
          oAuth2.RefreshToken);
      Console.ReadLine();
    }

    /// <summary>
    /// Accepts an input from the user, and retries if the user enters an empty
    /// value.
    /// </summary>
    /// <param name="message">The prompt message.</param>
    /// <returns>The user input</returns>
    static string AcceptInputWithRetry(string message) {
      string retval = "";
      while (true) {
        Console.Write(message);
        retval = Console.ReadLine();
        if (string.IsNullOrEmpty(retval)) {
          Console.WriteLine("Input value cannot be empty.");
        } else {
          return retval;
        }
      }
    }
  }
}

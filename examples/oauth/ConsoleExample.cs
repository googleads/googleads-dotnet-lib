// Copyright 2012, Google Inc. All Rights Reserved.
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

using OAuth.Net.Common;
using OAuth.Net.Consumer;

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201204;
using Google.Api.Ads.Common.OAuth.Lib;

using System;
using System.Data;

namespace Google.Api.Ads.Dfp.Examples.OAuth {
  /// <summary>
  /// This code example shows how to run an DFP API command line application
  /// using OAuth 1.0a/2.0 as authentication mechanism. To run this application,
  ///
  /// 1. You should create a new Console Application project.
  /// 2. Add reference to the following assemblies:
  /// <list type="table">
  /// <item>Google.Ads.Common.dll</item>
  /// <item>Google.Ads.OAuth.dll</item>
  /// <item>Google.Dfp.dll</item>
  /// <item>Microsoft.Practices.ServiceLocation.dll</item>
  /// <item>OAuth.Net.Combined.dll</item>
  /// <item>System.Web</item>
  /// <item>System.Configuration</item>
  /// </list>
  /// 3. Replace the Main() method with this class's method.
  /// 4. Copy App.config from Dfp.Examples project, and configure
  /// it as shown in this project's Web.config.
  /// 5. Compile and run this example.
  ///
  /// This code example depends on Console environment only for reading and
  /// writing values, you may use this code example in other environments like
  /// Windows Form applications with minimial modifications.
  /// </summary>
  public class ConsoleExample {
    /// <summary>
    /// The main method.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static void Main(string[] args) {
      DfpUser user = new DfpUser();

      if ((user.Config as DfpAppConfig).AuthorizationMethod ==
          DfpAuthorizationMethod.OAuth) {
        DoAuth1Authorization(user);
      } else if ((user.Config as DfpAppConfig).AuthorizationMethod ==
          DfpAuthorizationMethod.OAuth2) {
        DoAuth2Authorization(user);
      } else {
        throw new Exception("Authorization mode is not OAuth.");
      }

      // Get the UserService.
      UserService userService = (UserService)user.GetService(DfpService.v201204.UserService);

      // Sets defaults for page and Statement.
      UserPage page = new UserPage();
      Statement statement = new Statement();
      int offset = 0;

      try {
        do {
          // Create a Statement to get all users.
          statement.query = string.Format("LIMIT 500 OFFSET {0}", offset);

          // Get users by Statement.
          page = userService.getUsersByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (User usr in page.results) {
              Console.WriteLine("{0}) User with ID = '{1}', email = '{2}', and role = '{3}'" +
                  " was found.", i, usr.id, usr.email, usr.roleName);
              i++;
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get all users. Exception says \"{0}\"",
            ex.Message);
      }
    }

    /// <summary>
    /// Does the OAuth2 authorization.
    /// </summary>
    /// <param name="user">The Dfp user.</param>
    /// <remarks>If you have saved a user's access and refresh tokens from a
    /// previous session, you can set them directly to the OAuth2 handler
    /// object. Also, make sure you set the redirect uri and scope correctly
    /// if you wish to call RefreshAccessToken method.</remarks>
    private static void DoAuth2Authorization(DfpUser user) {
      // Set the OAuth2 scope.
      user.Config.OAuth2Scope = DfpService.GetOAuthScope(user.Config as DfpAppConfig);

      // Since we are using a console application, set the callback url to null.
      user.Config.OAuth2RedirectUri = null;

      // Create the OAuth2 protocol handler and set it to the current user.
      OAuth2Provider oAuth2 = new OAuth2Provider(user.Config);
      user.OAuthProvider = oAuth2;

      // Get the authorization url.
      string authorizationUrl = oAuth2.GetAuthorizationUrl();
      Console.WriteLine("Open a fresh web browser and navigate to \n\n{0}\n\n. You will be " +
          "prompted to login and then authorize this application to make calls to the " +
          "DFP API. Once approved, you will be presented with an authorization code.",
          authorizationUrl);

      // Accept the OAuth2 authorization code from the user.
      Console.Write("Enter the authorization code :");
      string authorizationCode = Console.ReadLine();

      // Fetch the access and refresh tokens.
      oAuth2.FetchAccessAndRefreshTokens(authorizationCode);
    }

    /// <summary>
    /// Does the OAuth1 authorization.
    /// </summary>
    /// <param name="user">The Dfp user.</param>
    /// <remarks>If you have saved a user's access tokens from a previous
    /// session, you can set them directly to the OAuth1a handler object. Since
    /// Also, make sure you set the redirect uri and scope correctly for signing
    /// purposes.</remarks>
    private static void DoAuth1Authorization(DfpUser user) {
      // Set the OAuth1.0a scope.
      user.Config.OAuthScope = DfpService.GetOAuthScope(user.Config as DfpAppConfig);

      // Since we are using a console application, set the callback url to null.
      user.Config.OAuthCallbackUrl = null;

      // Create the OAuth1.oa protocol handler and set it to the current user.
      OAuth1aProvider oAuth1a = new OAuth1aProvider(user.Config);
      user.OAuthProvider = oAuth1a;

      // Generate the authorization url and display that to the user. Note that
      // this will also generate a request token if not done already.
      string authorizationUrl = oAuth1a.GetAuthorizationUrl();
      Console.WriteLine("Open a fresh web browser and navigate to \n\n{0}\n\n. You will be " +
          "prompted to login and then authorize this application to make calls to the " +
          "DFP API. Once approved, you will be presented with an authorization code.",
          authorizationUrl);

      // Accept the authorization code from the user.
      Console.Write("Enter the authorization code :");
      string authorizationCode = Console.ReadLine();

      // Fetch the access token.
      oAuth1a.FetchAccessAndRefreshTokens(authorizationCode);
      return;
    }
  }
}

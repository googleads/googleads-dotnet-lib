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

using Google.Api.Ads.Dfa.v1_19;
using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Common.OAuth.Lib;

using System;
using System.Data;

namespace Google.Api.Ads.Dfa.Examples.CSharp.OAuth {
  /// <summary>
  /// This code example shows how to run an DFA API command line application
  /// using OAuth 2.0 as authentication mechanism. To run this application,
  ///
  /// 1. You should create a new Console Application project.
  /// 2. Add reference to the following assemblies:
  /// <list type="table">
  /// <item>Google.Ads.Common.dll</item>
  /// <item>Google.Ads.OAuth.dll</item>
  /// <item>Google.Dfa.dll</item>
  /// <item>Microsoft.Practices.ServiceLocation.dll</item>
  /// <item>OAuth.Net.Combined.dll</item>
  /// <item>System.Web</item>
  /// <item>System.Configuration</item>
  /// </list>
  /// 3. Replace the Main() method with this class's method.
  /// 4. Copy App.config from Dfa.Examples.CSharp project, and configure
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
      DfaUser user = new DfaUser();

      if ((user.Config as DfaAppConfig).AuthorizationMethod ==
           DfaAuthorizationMethod.OAuth2) {
        DoAuth2Authorization(user);
      } else {
        throw new Exception("Authorization mode is not OAuth2.");
      }

      // Set the username. This is required for the LoginService to work
      // correctly when authenticating using OAuth2.
      Console.Write("Enter the user name: ");
      string userName = Console.ReadLine();
      (user.Config as DfaAppConfig).UserName = userName;

      // Create AdRemoteService instance.
      AdRemoteService service = (AdRemoteService) user.GetService(
          DfaService.v1_19.AdRemoteService);

      try {
        // Get ad types.
        AdType[] adTypes = service.getAdTypes();

        // Display ad type and its id.
        foreach (AdType result in adTypes) {
          Console.WriteLine("Ad type with name \"{0}\" and id \"{1}\" was found.", result.name,
              result.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve ad types. Exception says \"{0}\"",
            ex.Message);
      }
    }

    /// <summary>
    /// Does the OAuth2 authorization.
    /// </summary>
    /// <param name="user">The Dfa user.</param>
    /// <remarks>If you have saved a user's access and refresh tokens from a
    /// previous session, you can set them directly to the OAuth2 handler
    /// object. Also, make sure you set the redirect uri and scope correctly
    /// if you wish to call RefreshAccessToken method.</remarks>
    private static void DoAuth2Authorization(DfaUser user) {
      // Set the OAuth2 scope.
      user.Config.OAuth2Scope = DfaService.GetOAuthScope(user.Config as DfaAppConfig);

      // Since we are using a console application, set the callback url to null.
      user.Config.OAuth2RedirectUri = null;

      // Create the OAuth2 protocol handler and set it to the current user.
      OAuth2Provider oAuth2 = new OAuth2Provider(user.Config);
      user.OAuthProvider = oAuth2;

      // Get the authorization url.
      string authorizationUrl = oAuth2.GetAuthorizationUrl();
      Console.WriteLine("Open a fresh web browser and navigate to \n\n{0}\n\n. You will be " +
          "prompted to login and then authorize this application to make calls to the " +
          "DFA API. Once approved, you will be presented with an authorization code.",
          authorizationUrl);

      // Accept the OAuth2 authorization code from the user.
      Console.Write("Enter the authorization code :");
      string authorizationCode = Console.ReadLine();

      // Fetch the access and refresh tokens.
      oAuth2.FetchAccessAndRefreshTokens(authorizationCode);
    }
  }
}

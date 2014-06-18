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
using Google.Api.Ads.Common.Lib;

using System;
using System.Data;

namespace Google.Api.Ads.Dfa.Examples.CSharp.OAuth {
  /// <summary>
  /// This code example shows how to run an DFA API command line application
  /// while incorporating the OAuth2 installed application flow into your
  /// application. If your application uses a single MCC login to make calls to
  /// all your accounts, you shouldn't use this code example. Instead, you
  /// should run Common\Util\OAuth2TokenGenerator.cs to generate a refresh token
  /// and set that in user.Config.OAuth2RefreshToken field, or set
  /// OAuth2RefreshToken key in your App.config / Web.config.
  ///
  /// This code example depends on Console environment only for reading and
  /// writing values, you may use this code example in other environments like
  /// Windows Form applications with minimial modifications.
  ///
  /// To run this application,
  ///
  /// 1. You should create a new Console Application project.
  /// 2. Add reference to the following assemblies:
  /// <list type="table">
  /// <item>Google.Ads.Common.dll</item>
  /// <item>Google.Ads.OAuth.dll</item>
  /// <item>Google.Dfa.dll</item>
  /// <item>System.Web</item>
  /// <item>System.Configuration</item>
  /// </list>
  /// 3. Replace the Main() method with this class's method.
  /// 4. Copy App.config from Dfa.Examples.CSharp project, and configure
  /// it as shown in this project's Web.config.
  /// 5. Compile and run this example.
  /// </summary>
  public class ConsoleExample {
    /// <summary>
    /// The main method.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static void Main(string[] args) {
      DfaUser user = new DfaUser();
      DfaAppConfig config = (user.Config as DfaAppConfig);
      if (config.AuthorizationMethod == DfaAuthorizationMethod.OAuth2) {
        if (config.OAuth2Mode == OAuth2Flow.APPLICATION &&
            string.IsNullOrEmpty(config.OAuth2RefreshToken)) {
          DoAuth2Authorization(user);
        }
      } else {
        throw new Exception("Authorization mode is not OAuth.");
      }

      // Set the username. This is required for the LoginService to work
      // correctly when authenticating using OAuth2.
      Console.Write("Enter the user name: ");
      string userName = Console.ReadLine();
      (user.Config as DfaAppConfig).DfaUserName = userName;

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
    /// Does the OAuth2 authorization for installed applications.
    /// </summary>
    /// <param name="user">The Dfa user.</param>
    private static void DoAuth2Authorization(DfaUser user) {
      // Since we are using a console application, set the callback url to null.
      user.Config.OAuth2RedirectUri = null;
      AdsOAuthProviderForApplications oAuth2Provider =
          (user.OAuthProvider as AdsOAuthProviderForApplications);
      // Get the authorization url.
      string authorizationUrl = oAuth2Provider.GetAuthorizationUrl();
      Console.WriteLine("Open a fresh web browser and navigate to \n\n{0}\n\n. You will be " +
          "prompted to login and then authorize this application to make calls to the " +
          "DFA API. Once approved, you will be presented with an authorization code.",
          authorizationUrl);

      // Accept the OAuth2 authorization code from the user.
      Console.Write("Enter the authorization code :");
      string authorizationCode = Console.ReadLine();

      // Fetch the access and refresh tokens.
      oAuth2Provider.FetchAccessAndRefreshTokens(authorizationCode);
    }
  }
}

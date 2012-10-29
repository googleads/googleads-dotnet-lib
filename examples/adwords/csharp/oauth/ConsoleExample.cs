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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201209;
using Google.Api.Ads.Common.OAuth.Lib;

using System;
using System.Data;

namespace Google.Api.Ads.AdWords.Examples.CSharp.OAuth {
  /// <summary>
  /// This code example shows how to run an AdWords API command line application
  /// using OAuth 1.0a/2.0 as authentication mechanism. To run this application,
  ///
  /// 1. You should create a new Console Application project.
  /// 2. Add reference to the following assemblies:
  /// <list type="table">
  /// <item>Google.Ads.Common.dll</item>
  /// <item>Google.Ads.OAuth.dll</item>
  /// <item>Google.AdWords.dll</item>
  /// <item>Microsoft.Practices.ServiceLocation.dll</item>
  /// <item>OAuth.Net.Combined.dll</item>
  /// <item>System.Web</item>
  /// <item>System.Configuration</item>
  /// </list>
  /// 3. Replace the Main() method with this class's method.
  /// 4. Copy App.config from AdWords.Examples.CSharp project, and configure
  /// it as shown in ths project's Web.config.
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
      AdWordsUser user = new AdWordsUser();

      if ((user.Config as AdWordsAppConfig).AuthorizationMethod ==
          AdWordsAuthorizationMethod.OAuth) {
        DoAuth1Authorization(user);
      } else if ((user.Config as AdWordsAppConfig).AuthorizationMethod ==
          AdWordsAuthorizationMethod.OAuth2) {
        DoAuth2Authorization(user);
      } else {
        throw new Exception("Authorization mode is not OAuth.");
      }

      Console.Write("Enter the customer id: ");
      string customerId = Console.ReadLine();
      (user.Config as AdWordsAppConfig).ClientCustomerId = customerId;

      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201209.CampaignService);

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name", "Status"};

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      CampaignPage page = new CampaignPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the campaigns.
          page = campaignService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (Campaign campaign in page.entries) {
              Console.WriteLine("{0}) Campaign with id = '{1}', name = '{2}' and status = '{3}'" +
                " was found.", i + 1, campaign.id, campaign.name, campaign.status);
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of campaigns found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve campaigns", ex);
      }
    }

    /// <summary>
    /// Does the OAuth2 authorization.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <remarks>If you have saved a user's access and refresh tokens from a
    /// previous session, you can set them directly to the OAuth2 handler
    /// object. Also, make sure you set the redirect uri and scope correctly
    /// if you wish to call RefreshAccessToken method.</remarks>
    private static void DoAuth2Authorization(AdWordsUser user) {
      // Set the OAuth2 scope.
      user.Config.OAuth2Scope = AdWordsService.GetOAuthScope(user.Config as AdWordsAppConfig);

      // Since we are using a console application, set the callback url to null.
      user.Config.OAuth2RedirectUri = null;

      // Create the OAuth2 protocol handler and set it to the current user.
      OAuth2Provider oAuth2 = new OAuth2Provider(user.Config);
      user.OAuthProvider = oAuth2;

      // Get the authorization url.
      string authorizationUrl = oAuth2.GetAuthorizationUrl();
      Console.WriteLine("Open a fresh web browser and navigate to \n\n{0}\n\n. You will be " +
          "prompted to login and then authorize this application to make calls to the " +
          "AdWords API. Once approved, you will be presented with an authorization code.",
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
    /// <param name="user">The user.</param>
    /// <remarks>If you have saved a user's access tokens from a previous
    /// session, you can set them directly to the OAuth1a handler object. Since
    /// Also, make sure you set the redirect uri and scope correctly for signing
    /// purposes.</remarks>
    private static void DoAuth1Authorization(AdWordsUser user) {
      // Set the OAuth1.0a scope.
      user.Config.OAuthScope = AdWordsService.GetOAuthScope(user.Config as AdWordsAppConfig);

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
          "AdWords API. Once approved, you will be presented with an authorization code.",
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

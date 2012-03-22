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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109;
using Google.Api.Ads.Common.OAuth.Lib;

using Microsoft.Practices.ServiceLocation;
using OAuth.Net.Consumer;
using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.OAuth {
  /// <summary>
  /// This code example shows how to run an AdWords API command line application
  /// using OAuth 1.0a as authentication mechanism. To run this application,
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
      // Create a service locator, and configure it to use in-memory state
      // store. The service locator provides a session based store for ASP.NET
      // applications.
      AdsServiceLocator injector = new AdsServiceLocator();
      injector.UseMemoryStore = true;
      ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(delegate() {
        return injector;
      }));

      // Create a new AdWords user.
      AdWordsUser user = new AdWordsUser();
      AdWordsAppConfig config = user.Config as AdWordsAppConfig;

      // Since this is not a web application, leave the callback url as null.
      string callbackUrl = null;

      // Provide a unique user id. This is used to distinguish OAuth credentials
      // of individual users in case your application manages multiple users
      // concurrently. If it doesn't, you can provide any value for this string.
      string userId = "user123";

      // Set the OAuth provider.
      AdsOAuthNetProvider provider = new AdsOAuthNetProvider(config.OAuthConsumerKey,
          config.OAuthConsumerSecret, AdWordsService.GetOAuthScope(user.Config as AdWordsAppConfig),
          callbackUrl, "user123");

      // The library provides an AuthorizationHandler and VerificationHandler
      // that works fine with an ASP.NET environment. In case you need to
      // support another environment, or modify the behaviour of the default
      // handler in an ASP.NET application, you need to override these fields.
      provider.AuthorizationHandler = delegate(object sender, AuthorizationEventArgs authArgs) {
        OAuthRequest request = (OAuthRequest) sender;
        Console.WriteLine("Visit {0} in a new browser window. Once the process is complete, " +
            "enter the verification code provided by the web page below.",
            request.Service.BuildAuthorizationUrl(authArgs.RequestToken).AbsoluteUri);
        authArgs.ContinueOnReturn = true;
      };

      provider.VerificationHandler =
          delegate(object sender, AuthorizationVerificationEventArgs authArgs) {
            Console.Write("Please enter the OAuth verifier: ");
            authArgs.Verifier = Console.ReadLine();
            // If the user provided an empty verifier, exit the application,
            // since the OAuth flow cannot continue any longer.
            if (string.IsNullOrEmpty(authArgs.Verifier)) {
              System.Environment.Exit(2);
            }
          };

      user.OAuthProvider = provider;

      // Trigger the OAuth signup process.
      user.OAuthProvider.GenerateAccessToken();

      // Now make your API call.
      // Create a CampaignService instance.
      CampaignService service = (CampaignService) user.GetService(
          AdWordsService.v201109.CampaignService);

      // If you used OAuth to authenticate against an AdWords user, then you
      // needn't provide the clientCustomerId. If you authenticated against an
      // MCC account, you need to provide the clientCustomerId for which you
      // wish to make the API call. If you are downloading a report, then
      // you should provide a customerId even if you used OAuth to authenticate
      // against an AdWords account. If you don't provide a customer id in your
      // code, then the library will read it from App.config.
      //
      //service.RequestHeader.clientCustomerId = "XXX-XXX-XXXX";

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name", "Status"};

      CampaignPage page = service.get(selector);

      int i = 0;
      // Display the results.
      foreach (Campaign campaign in page.entries) {
        Console.WriteLine("{0}) Campaign with id = '{1}', name = '{2}' and status = '{3}'" +
          " was found.", i + 1, campaign.id, campaign.name, campaign.status);
        i++;
      }
    }
  }
}

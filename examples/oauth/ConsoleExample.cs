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

using Google.Api.Ads.Common.OAuth.Lib;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201203;

using Microsoft.Practices.ServiceLocation;
using OAuth.Net.Consumer;

using System;

namespace Google.Api.Ads.Dfp.Examples.OAuth {
  /// <summary>
  /// This code example shows how to run a DFP API command line application
  /// using OAuth 1.0a as authentication mechanism. To run this application,
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
  /// <item>System.Web.Services</item>
  /// <item>System.Configuration</item>
  /// </list>
  /// 3. Replace the Main() method with this class's method.
  /// 4. Copy App.config from Dfp.Examples project, and configure it as shown in
  /// this project's Web.config.
  /// 5. Compile and run this example.
  ///
  /// This code example depends on Console environment only for reading and
  /// writing values, you may use this code example in other environments like
  /// Windows Form applications with minimial modifications.
  /// </summary>
  class ConsoleExample {
    static void Main(string[] args) {
      // Create a service locator, and configure it to use in-memory state
      // store. The service locator provides a session based store for ASP.NET
      // applications.
      AdsServiceLocator injector = new AdsServiceLocator();
      injector.UseMemoryStore = true;
      ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(delegate() {
        return injector;
      }));

      // Create a new Dfp user.
      DfpUser user = new DfpUser();
      DfpAppConfig config = user.Config as DfpAppConfig;

      // Since this is not a web application, leave the callback url as null.
      string callbackUrl = null;

      // Provide a unique user id. This is used to distinguish OAuth credentials
      // of individual users in case your application manages multiple users
      // concurrently. If it doesn't, you can provide any value for this string.
      string userId = "user123";

      // Set the OAuth provider.
      AdsOAuthNetProvider provider = new AdsOAuthNetProvider(config.OAuthConsumerKey,
          config.OAuthConsumerSecret, DfpService.GetOAuthScope(config),
          callbackUrl, userId);

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
      // Get the UserService.
      UserService userService = (UserService) user.GetService(DfpService.v201203.UserService);

      // Sets defaults for page and Statement.
      UserPage page = new UserPage();
      Statement statement = new Statement();
      int offset = 0;

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
    }
  }
}

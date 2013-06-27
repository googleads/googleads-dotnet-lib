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
using Google.Api.Ads.AdWords.v201209;
using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201209 {
  /// <summary>
  /// This code example illustrates how to handle 2 factor authorization errors.
  ///
  /// Tags: CampaignService.get
  /// </summary>
  public class HandleTwoFactorAuthorizationError : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      HandleTwoFactorAuthorizationError codeExample = new HandleTwoFactorAuthorizationError();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to handle 2 factor authorization errors.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Use a test account for which 2 factor authentication has been enabled.
      string loginEmail = "2steptester@gmail.com";
      string password = "testaccount";

      AdWordsAppConfig config = new AdWordsAppConfig();
      config.Email = loginEmail;
      config.Password = password;
      AuthToken authToken = new AuthToken(config, "adwords");

      try {
        // Try to obtain an authToken.
        string token = authToken.GetToken();
        Console.WriteLine("Retrieved an authToken = {0} for user {1}.", token, loginEmail);
      } catch (AuthTokenException ex) {
        // Since the test account has 2 factor authentication enabled, this block
        // of code will be executed.
        if (ex.ErrorCode == AuthTokenErrorCode.BadAuthentication) {
          if (ex.Info == "InvalidSecondFactor") {
            Console.WriteLine("The user has enabled two factor authentication in this " +
                "account. Have the user generate an application-specific password to make " +
                "calls against the AdWords API. See " +
                "http://adwordsapi.blogspot.com/2011/02/authentication-changes-with-2-step.html" +
                " for more details.");
          } else {
            Console.WriteLine("Invalid credentials.");
          }
        } else {
          throw new System.ApplicationException(String.Format("The server raised an {0} error.",
              ex.ErrorCode));
        }
      }
    }
  }
}

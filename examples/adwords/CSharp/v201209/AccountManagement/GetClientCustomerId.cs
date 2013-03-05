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

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201209 {
  /// <summary>
  /// This code example illustrates how to find a client customer ID for a
  /// client email. We recommend to use this script as a one off to convert your
  /// identifiers to IDs and store them for future use. This code example
  /// won't work with Test Accounts. See
  /// https://developers.google.com/adwords/api/docs/test-accounts
  ///
  /// Tags: InfoService.get
  /// </summary>
  public class GetClientCustomerId : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetClientCustomerId codeExample = new GetClientCustomerId();
      Console.WriteLine(codeExample.Description);
      try {
        string clientEmail = "INSERT_CLIENT_EMAIL_HERE";
        codeExample.Run(new AdWordsUser(), clientEmail);
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
        return "This code example illustrates how to find a client customer ID for a client " +
            "email. We recommend to use this script as a one off to convert your identifiers " +
            "to IDs and store them for future use. This code example won't work with test " +
            "accounts. See https://developers.google.com/adwords/api/docs/test-accounts";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="clientEmail">The client email for which customer id
    /// is retrieved.</param>
    public void Run(AdWordsUser user, string clientEmail) {
      // Get the InfoService.
      InfoService infoService = (InfoService) user.GetService(AdWordsService.v201209.InfoService);

      // Ensure the clientCustomerId is not set, so that requests are made to
      // the MCC.
      infoService.RequestHeader.clientCustomerId = null;

      // Create the selector.
      InfoSelector selector = new InfoSelector();
      selector.clientEmails = new string[] {clientEmail};
      selector.includeSubAccounts = true;
      selector.apiUsageType = ApiUsageType.UNIT_COUNT_FOR_CLIENTS;

      // The date used doesn't matter, so use yesterday.
      DateRange dateRange = new DateRange();
      dateRange.max = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
      dateRange.min = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
      selector.dateRange = dateRange;

      try {
        // Get the information for the client email address.
        ApiUsageInfo info = infoService.get(selector);

        if (info != null && info.apiUsageRecords != null) {
          foreach (ApiUsageRecord record in info.apiUsageRecords) {
            Console.WriteLine("Found record with client email '{0}' and customer ID " +
                "'{1:###-###-####}'.", record.clientEmail, record.clientCustomerId);
          }
        } else {
          Console.WriteLine("No client customer ids were found.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get client customer id.", ex);
      }
    }
  }
}

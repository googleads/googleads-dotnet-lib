// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201406;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {
  /// <summary>
  /// This code example gets all alerts for all clients of an MCC account.
  /// The effective user (ClientCustomerId or OAuth2 header) must be an MCC
  /// user to get results. This code example won't work with Test Accounts. See
  /// https://developers.google.com/adwords/api/docs/test-accounts
  ///
  /// Tags: AlertService.get
  /// </summary>
  public class GetAccountAlerts : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetAccountAlerts codeExample = new GetAccountAlerts();
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
        return "This code example gets all alerts for all clients of an MCC account. The " +
            "effective user (ClientCustomerId or OAuth2 header) must be an MCC user to get " +
            "results. This code example won't work with Test Accounts. See " +
            "https://developers.google.com/adwords/api/docs/test-accounts";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the AlertService.
      AlertService alertService = (AlertService) user.GetService(
          AdWordsService.v201406.AlertService);

      // Create the selector.
      AlertSelector selector = new AlertSelector();

      // Create the alert query.
      AlertQuery query = new AlertQuery();
      query.filterSpec = FilterSpec.ALL;
      query.clientSpec = ClientSpec.ALL;
      query.triggerTimeSpec = TriggerTimeSpec.ALL_TIME;
      query.severities = new AlertSeverity[] {AlertSeverity.GREEN, AlertSeverity.YELLOW,
          AlertSeverity.RED};

      // Enter all possible values of AlertType to get all alerts. If you are
      // interested only in specific alert types, then you may also do it as
      // follows:
      // query.types = new AlertType[] {AlertType.CAMPAIGN_ENDING,
      //     AlertType.CAMPAIGN_ENDED};
      query.types = (AlertType[]) Enum.GetValues(typeof(AlertType));
      selector.query = query;

      // Set paging for selector.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      AlertPage page = new AlertPage();

      try {
        do {
          // Get account alerts.
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          page = alertService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (Alert alert in page.entries) {
              Console.WriteLine("{0}) Customer Id is {1:###-###-####}, Alert type is '{2}', " +
                  "Severity is {3}", i + 1, alert.clientCustomerId, alert.alertType,
                  alert.alertSeverity);
              for (int j = 0; j < alert.details.Length; j++) {
                Console.WriteLine("  - Triggered at {0}", alert.details[j].triggerTime);
              }
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of alerts found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve alerts.", ex);
      }
    }
  }
}

// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v201008;

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace com.google.api.adwords.examples.v201008 {
  /// <summary>
  /// This code example illustrates how to retrieve campaign alerts for a user.
  /// The alerts are restricted to a maximum of 10 entries.
  ///
  /// Tags: AlertService.get
  /// </summary>
  class GetCampaignAlerts : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve campaign alerts for a user. " +
            "The alerts are restricted to a maximum of 10 entries.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AlertService.
      AlertService alertService = (AlertService) user.GetService(
          AdWordsService.v201008.AlertService);

      // Create the alert query.
      AlertQuery query = new AlertQuery();
      query.filterSpec = FilterSpec.ALL;
      query.clientSpec = ClientSpec.ALL;
      query.triggerTimeSpec = TriggerTimeSpec.ALL_TIME;
      query.severities = new AlertSeverity[] {AlertSeverity.GREEN, AlertSeverity.YELLOW,
          AlertSeverity.RED};
      query.types = new AlertType[] {AlertType.CAMPAIGN_ENDING, AlertType.CAMPAIGN_ENDED};

      // Create the selector.
      AlertSelector selector = new AlertSelector();
      selector.query = query;
      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 10;

      try {
        AlertPage page = alertService.get(selector);

        if (page != null && page.entries != null && page.entries.Length > 0) {
          Console.WriteLine("Retrieved {0} alerts out of {1}.", page.entries.Length,
              page.totalNumEntries);
          for (int i = 0; i < page.entries.Length; i++) {
            Alert alert = page.entries[i];
            Console.WriteLine("{0}) Customer Id is {1:###-###-####}, Alert type is '{2}', " +
                "Severity is {3}", i + 1, alert.clientCustomerId, alert.alertType,
                alert.alertSeverity);
            for (int j = 0; j < alert.details.Length; j++) {
              Console.WriteLine("  - Triggered at {0}", alert.details[j].triggerTime);
            }
          }
        } else {
          Console.WriteLine("No alerts were found.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve alerts. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

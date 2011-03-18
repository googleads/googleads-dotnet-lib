// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example shows how to check if a campaign is eligible for
  /// conversion optimizer.
  ///
  /// Tags: CampaignService.get
  /// </summary>
  class GetConversionOptimizerEligibility : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to check if a campaign is eligible for conversion " +
            "optimizer.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetConversionOptimizerEligibility();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201101.CampaignService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Eligible", "RejectionReasons"};

      // Create filter conditions.
      Predicate predicate = new Predicate();
      predicate.field = "Id";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] {campaignId.ToString()};
      selector.predicates = new Predicate[] {predicate};

      try {
        CampaignPage page = campaignService.get(selector);
        if (page != null && page.entries != null && page.entries.Length > 0) {
          Campaign campaign = page.entries[0];
          if (campaign.conversionOptimizerEligibility.eligible == true) {
            Console.WriteLine("Campaign with id = '{0}' is eligible to use conversion optimizer.",
                campaign.id);
          } else {
            foreach (ConversionOptimizerEligibilityRejectionReason reason in
                campaign.conversionOptimizerEligibility.rejectionReasons) {
              Console.WriteLine("Campaign with id = '{0}' is not eligible to use conversion" +
                  " optimizer for reason '{1}'.", campaign.id, reason);
            }
          }
        } else {
          Console.WriteLine("No campaigns were found.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get conversion optimizer eligibility for campaign(s). " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

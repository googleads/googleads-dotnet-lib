// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example retrieves all the disapproved ads in a given campaign.
  /// </summary>
  public class GetAllDisapprovedAds : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetAllDisapprovedAds codeExample = new GetAllDisapprovedAds();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves all the disapproved ads in a given campaign.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign for which disapproved ads
    /// are retrieved.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201603.AdGroupAdService);

      // Create the selector.
      Selector selector = new Selector() {
        fields = new string[] {
          Ad.Fields.Id, AdGroupAd.Fields.AdGroupCreativeApprovalStatus,
          AdGroupAd.Fields.AdGroupAdDisapprovalReasons
        },
        predicates = new Predicate[] {
          Predicate.Equals(AdGroup.Fields.CampaignId, campaignId),
          Predicate.Equals(AdGroupAd.Fields.AdGroupCreativeApprovalStatus,
              AdGroupAdApprovalStatus.DISAPPROVED.ToString())
        },
        paging = Paging.Default
      };

      AdGroupAdPage page = new AdGroupAdPage();

      try {
        do {
          // Get the disapproved ads.
          page = service.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;
            foreach (AdGroupAd adGroupAd in page.entries) {
              Console.WriteLine("{0}) Ad id {1} has been disapproved for the following " +
                  "reason(s):", i + 1, adGroupAd.ad.id);
              foreach (string reason in adGroupAd.disapprovalReasons) {
                Console.WriteLine("    {0}", reason);
              }
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of disapproved ads found: {0}", page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to get disapproved ads.", e);
      }
    }
  }
}

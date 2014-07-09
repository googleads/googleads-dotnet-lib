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
  /// This code example retrieves all the disapproved ads in a given campaign.
  ///
  /// Tags: AdGroupAdService.get
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
          (AdGroupAdService) user.GetService(AdWordsService.v201406.AdGroupAdService);

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "AdGroupCreativeApprovalStatus",
          "AdGroupAdDisapprovalReasons"};

      // Create the filter.
      Predicate campaignPredicate = new Predicate();
      campaignPredicate.@operator = PredicateOperator.EQUALS;
      campaignPredicate.field = "CampaignId";
      campaignPredicate.values = new string[] {campaignId.ToString()};

      Predicate approvalPredicate = new Predicate();
      approvalPredicate.@operator = PredicateOperator.EQUALS;
      approvalPredicate.field = "AdGroupCreativeApprovalStatus";
      approvalPredicate.values = new string[] {AdGroupAdApprovalStatus.DISAPPROVED.ToString()};

      selector.predicates = new Predicate[] {campaignPredicate, approvalPredicate};

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      AdGroupAdPage page = new AdGroupAdPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the disapproved ads.
          page = service.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (AdGroupAd adGroupAd in page.entries) {
              Console.WriteLine("{0}) Ad id {1} has been disapproved for the following " +
                  "reason(s):", i, adGroupAd.ad.id);
              foreach (string reason in adGroupAd.disapprovalReasons) {
                Console.WriteLine("    {0}", reason);
              }
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of disapproved ads found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get disapproved ads.", ex);
      }
    }
  }
}

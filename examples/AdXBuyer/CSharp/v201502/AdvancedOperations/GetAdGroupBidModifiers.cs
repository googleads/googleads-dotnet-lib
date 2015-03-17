// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201502;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201502 {
  /// <summary>
  /// This code example illustrates how to retrieve ad group level mobile bid
  /// modifiers for a campaign.
  ///
  /// Tags: AdGroupBidModifierService.get
  /// </summary>
  public class GetAdGroupBidModifiers : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetAdGroupBidModifiers codeExample = new GetAdGroupBidModifiers();
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
        return "This code example illustrates how to retrieve ad group level mobile bid " +
            "modifiers for a campaign.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to which ads are added.
    /// </param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the AdGroupAdService.
      AdGroupBidModifierService adGroupBidModifierService =
          (AdGroupBidModifierService) user.GetService(
              AdWordsService.v201502.AdGroupBidModifierService);

      const int PAGE_SIZE = 500;

      // Get all ad group bid modifiers for the campaign.
      Selector selector = new Selector();
      selector.fields = new String[] {"CampaignId", "AdGroupId", "BidModifier", "BidModifierSource",
        "CriteriaType", "Id"};

      Predicate predicate = new Predicate();
      predicate.field = "CampaignId";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] {campaignId.ToString()};
      selector.predicates = new Predicate[] {predicate};

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = PAGE_SIZE;

      AdGroupBidModifierPage page = new AdGroupBidModifierPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the campaigns.
          page = adGroupBidModifierService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (AdGroupBidModifier adGroupBidModifier in page.entries) {
              string bidModifier = (adGroupBidModifier.bidModifierSpecified)?
                  adGroupBidModifier.bidModifier.ToString() : "UNSET";
              Console.WriteLine("{0}) Campaign ID {1}, AdGroup ID {2}, Criterion ID {3} has " +
                  "ad group level modifier: {4} and source = {5}.",
                  i + 1, adGroupBidModifier.campaignId,
                  adGroupBidModifier.adGroupId, adGroupBidModifier.criterion.id, bidModifier,
                  adGroupBidModifier.bidModifierSource);
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of adgroup bid modifiers found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve adgroup bid modifiers.", ex);
      }
    }
  }
}

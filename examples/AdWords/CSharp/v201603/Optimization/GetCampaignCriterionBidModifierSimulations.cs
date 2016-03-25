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
  /// This code example gets all available campaign mobile bid modifier
  /// landscapes for a given campaign. To get campaigns, run GetCampaigns.cs.
  /// </summary>
  public class GetCampaignCriterionBidModifierSimulations : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetCampaignCriterionBidModifierSimulations codeExample =
          new GetCampaignCriterionBidModifierSimulations();
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
        return "This code example gets all available campaign mobile bid modifier landscapes " +
            "for a given campaign. To get campaigns, run GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Runs the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="campaignId">Id of the campaign for which bid simulations are
    /// retrieved.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the DataService.
      DataService dataService = (DataService) user.GetService(AdWordsService.v201603.DataService);


      // Create selector.
      Selector selector = new Selector() {
        fields = new string[] {
            CriterionBidLandscape.Fields.CampaignId,
            CriterionBidLandscape.Fields.CriterionId,
            CriterionBidLandscape.Fields.StartDate,
            CriterionBidLandscape.Fields.EndDate,
            BidLandscapeLandscapePoint.Fields.LocalClicks,
            BidLandscapeLandscapePoint.Fields.LocalCost,
            BidLandscapeLandscapePoint.Fields.LocalImpressions,
            BidLandscapeLandscapePoint.Fields.TotalLocalClicks,
            BidLandscapeLandscapePoint.Fields.TotalLocalCost,
            BidLandscapeLandscapePoint.Fields.TotalLocalImpressions,
            BidLandscapeLandscapePoint.Fields.RequiredBudget,
            BidLandscapeLandscapePoint.Fields.BidModifier,
        },
        predicates = new Predicate[] {
          Predicate.Equals(CriterionBidLandscape.Fields.CampaignId, campaignId)
        },
        paging = Paging.Default
      };

      int landscapePointsInLastResponse = 0;
      int landscapePointsFound = 0;

      try {
        CriterionBidLandscapePage page = null;

        do {
          // When retrieving bid landscape, page.totalNumEntities cannot be used to determine
          // if there are more entries, since it shows only the total number of bid landscapes
          // and not the number of bid landscape points. So you need to iterate until you no
          // longer get back any bid landscapes.

          // Get bid landscape for campaign.
          page = dataService.getCampaignCriterionBidLandscape(selector);
          landscapePointsInLastResponse = 0;

          if (page != null && page.entries != null) {
            foreach (CriterionBidLandscape bidLandscape in page.entries) {
              Console.WriteLine("Found campaign-level criterion bid modifier landscapes for" +
                  " criterion with ID {0}, start date '{1}', end date '{2}', and" +
                  " landscape points:",
                  bidLandscape.criterionId,
                  bidLandscape.startDate,
                  bidLandscape.endDate
              );

              foreach (BidLandscapeLandscapePoint point in bidLandscape.landscapePoints) {
                Console.WriteLine("- bid modifier: {0:0.00} => clicks: {1}, cost: {2}, " +
                    "impressions: {3}, total clicks: {4}, total cost: {5}, " +
                    "total impressions: {6}, and required budget: {7}",
                    point.bidModifier, point.clicks, point.cost.microAmount,
                    point.impressions, point.totalLocalClicks, point.totalLocalCost.microAmount,
                    point.totalLocalImpressions, point.requiredBudget.microAmount);
                landscapePointsInLastResponse++;
                landscapePointsFound++;
              }
            }
          } 
          // Offset by the number of landscape points, NOT the number
          // of entries (bid landscapes) in the last response.
          selector.paging.IncreaseOffsetBy(landscapePointsInLastResponse);
        } while (landscapePointsInLastResponse > 0);
        Console.WriteLine("Number of bid landscape points found: {0}",
            landscapePointsFound);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to get campaign bid landscapes.", e);
      }
    }
  }
}

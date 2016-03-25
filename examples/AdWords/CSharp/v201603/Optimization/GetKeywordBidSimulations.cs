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
  /// This code example gets a bid landscape for an ad group and a keyword.
  /// To get ad groups, run GetAdGroups.cs. To get keywords, run
  /// GetKeywords.cs.
  /// </summary>
  public class GetKeywordBidSimulations : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetKeywordBidSimulations codeExample = new GetKeywordBidSimulations();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        long keywordId = long.Parse("INSERT_KEYWORD_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId, keywordId);
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
        return "This code example gets a bid landscape for an ad group and a keyword. " +
            "To get ad groups, run GetAdGroups.cs. To get keywords, run GetKeywords.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group for which keyword bid
    /// simulations are retrieved.</param>
    /// <param name="keywordId">Id of the keyword for which bid simulations are
    /// retrieved.</param>
    public void Run(AdWordsUser user, long adGroupId, long keywordId) {
      // Get the DataService.
      DataService dataService = (DataService) user.GetService(AdWordsService.v201603.DataService);

      // Create the selector.
      Selector selector = new Selector() {
        fields = new string[] {
          CriterionBidLandscape.Fields.AdGroupId, CriterionBidLandscape.Fields.CriterionId,
          CriterionBidLandscape.Fields.StartDate, CriterionBidLandscape.Fields.EndDate, 
          BidLandscapeLandscapePoint.Fields.Bid, BidLandscapeLandscapePoint.Fields.LocalClicks,
          BidLandscapeLandscapePoint.Fields.LocalCost,
          BidLandscapeLandscapePoint.Fields.LocalImpressions
        },
        predicates = new Predicate[] {
          Predicate.Equals(CriterionBidLandscape.Fields.AdGroupId, adGroupId),
          Predicate.Equals(CriterionBidLandscape.Fields.CriterionId, keywordId)
        },
        paging = Paging.Default
      };

      CriterionBidLandscapePage page = new CriterionBidLandscapePage();
      int landscapePointsFound = 0;
      int landscapePointsInLastResponse = 0;

      try {
        do {
          // Get bid landscape for keywords.
          page = dataService.getCriterionBidLandscape(selector);
          landscapePointsInLastResponse = 0;

          // Display bid landscapes.
          if (page != null && page.entries != null) {
            foreach (CriterionBidLandscape bidLandscape in page.entries) {
              Console.WriteLine("Found criterion bid landscape with ad group id '{0}', " +
                  "keyword id '{1}', start date '{2}', end date '{3}', and landscape points:",
                  bidLandscape.adGroupId, bidLandscape.criterionId,
                  bidLandscape.startDate, bidLandscape.endDate);
              foreach (BidLandscapeLandscapePoint bidLandscapePoint in
                  bidLandscape.landscapePoints) {
                Console.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, impressions: {3}\n",
                    bidLandscapePoint.bid.microAmount, bidLandscapePoint.clicks,
                    bidLandscapePoint.cost.microAmount, bidLandscapePoint.impressions);
                landscapePointsInLastResponse++;
                landscapePointsFound++;
              }
            }
          }
          // Offset by the number of landscape points, NOT the number
          // of entries (bid landscapes) in the last response.
          selector.paging.IncreaseOffsetBy(landscapePointsInLastResponse);
        } while (landscapePointsInLastResponse > 0);
        Console.WriteLine("Number of keyword bid landscape points found: {0}",
            landscapePointsFound);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve keyword bid landscapes.", e);
      }
    }
  }
}

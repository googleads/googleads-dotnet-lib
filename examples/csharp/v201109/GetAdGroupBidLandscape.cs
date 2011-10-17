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
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example gets bid landscapes for an ad group. To get adgroups,
  /// run GetAllAdGroups.cs.
  ///
  /// Tags: DataService.getAdGroupBidLandscape
  /// </summary>
  class GetAdGroupBidLandscape : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets bid landscapes for an ad group. To get adgroups, " +
            "run GetAllAdGroups.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAdGroupBidLandscape();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the DataService.
      DataService dataService = (DataService)user.GetService(AdWordsService.v201109.DataService);

      // Replace with valid values of your account.
      string adGroupId = "INSERT_AD_GROUP_ID_HERE";

      // Create selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdGroupId", "LandscapeType", "LandscapeCurrent",
          "StartDate", "EndDate", "Bid", "LocalClicks", "LocalCost", "MarginalCpc",
          "LocalImpressions"};

      // Create the filters.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.IN;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      selector.predicates = new Predicate[] {adGroupPredicate};

      try {
        // Get bid landscape for ad group.
        AdGroupBidLandscapePage page = dataService.getAdGroupBidLandscape(selector);

        // Display bid landscapes.
        if (page != null && page.entries != null && page.entries.Length > 0) {
          foreach (AdGroupBidLandscape bidLandscape in page.entries) {
            Console.WriteLine("Found ad group bid landscape with ad group id '{0}', type '{1}'," +
                " current: '{2}', start date '{3}', end date '{4}', and landscape points",
                bidLandscape.adGroupId, bidLandscape.type, bidLandscape.landscapeCurrent,
                bidLandscape.startDate, bidLandscape.endDate);
            foreach (BidLandscapeLandscapePoint point in bidLandscape.landscapePoints) {
              Console.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, marginalCpc: {3}, " +
                  "impressions: {4}", point.bid.microAmount, point.bid.microAmount,
                  point.clicks, point.cost.microAmount, point.marginalCpc.microAmount,
                  point.impressions);
            }
          }
        } else {
          Console.WriteLine("No ad group bid landscapes were found.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get ad group bid landscapes. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

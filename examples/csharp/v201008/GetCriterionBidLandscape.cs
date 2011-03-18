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
using Google.Api.Ads.AdWords.v201008;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
  /// <summary>
  /// This code example gets a bid landscape for an ad group and a criterion.
  /// To get ad groups, run GetAllAdGroups.cs. To get criteria, run
  /// GetAllAdGroupCriteria.cs.
  ///
  /// Tags: BidLandscapeService.getBidLandscape
  /// </summary>
  class GetCriterionBidLandscape : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a bid landscape for an ad group and a criterion. " +
            "To get ad groups, run GetAllAdGroups.cs. To get criteria, run " +
            "GetAllAdGroupCriteria.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCriterionBidLandscape();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the BidLandscapeService.
      BidLandscapeService bidLandscapeService = (BidLandscapeService)user.GetService(
          AdWordsService.v201008.BidLandscapeService);

      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));
      long criterionId = long.Parse(_T("INSERT_CRITERION_ID_HERE"));

      // Create selector.
      CriterionBidLandscapeSelector selector = new CriterionBidLandscapeSelector();

      // Create id filter.
      BidLandscapeIdFilter idFilter = new BidLandscapeIdFilter();
      idFilter.adGroupId = adGroupId;
      idFilter.criterionId = criterionId;
      selector.idFilters = new BidLandscapeIdFilter[] {idFilter};

      try {
        // Get bid landscape for ad group criteria.
        BidLandscape[] bidLandscapes = bidLandscapeService.getBidLandscape(selector);

        // Display bid landscapes.
        if (bidLandscapes != null && bidLandscapes.Length > 0) {
          foreach (BidLandscape bidLandscape in bidLandscapes) {
            if (bidLandscape is CriterionBidLandscape) {
              CriterionBidLandscape criterionBidLandscape = (CriterionBidLandscape) bidLandscape;
              Console.WriteLine("Found criterion bid landscape with ad group id \"{0}\", " +
                  "criterion id \"{1}\", start date \"{2}\", end date \"{3}\", and " +
                  "landscape points:", criterionBidLandscape.adGroupId,
                  criterionBidLandscape.criterionId, criterionBidLandscape.startDate,
                  criterionBidLandscape.endDate);

              foreach (BidLandscapeLandscapePoint bidLandscapePoint in
                  bidLandscape.landscapePoints) {
                Console.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, marginalCpc: {3}, " +
                    "impressions: {4}", bidLandscapePoint.bid.microAmount, bidLandscapePoint.clicks,
                    bidLandscapePoint.cost.microAmount, bidLandscapePoint.marginalCpc.microAmount,
                    bidLandscapePoint.impressions);
              }
            }
          }
        } else {
          Console.WriteLine("No criterion bid landscapes were found.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve criterion bid landscapes. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

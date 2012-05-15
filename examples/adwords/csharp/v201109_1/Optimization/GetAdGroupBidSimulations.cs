// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example gets bid landscapes for an ad group. To get ad groups,
  /// run GetAdGroups.cs.
  ///
  /// Tags: DataService.getAdGroupBidLandscape
  /// </summary>
  public class GetAdGroupBidSimulations : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetAdGroupBidSimulations();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
        return "This code example gets bid landscapes for an ad group. To get ad groups, run " +
            "GetAdGroups.cs";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID"};
    }

    /// <summary>
    /// Runs the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="writer">The writer.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the DataService.
      DataService dataService = (DataService) user.GetService(AdWordsService.v201109_1.DataService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdGroupId", "LandscapeType", "LandscapeCurrent", "StartDate",
          "EndDate", "Bid", "LocalClicks", "LocalCost", "MarginalCpc", "LocalImpressions"};

      // Set the filters.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.IN;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      selector.predicates = new Predicate[] {adGroupPredicate};

      try {
        // Get bid landscape for ad group.
        AdGroupBidLandscapePage page = dataService.getAdGroupBidLandscape(selector);
        if (page != null && page.entries != null && page.entries.Length > 0) {
          foreach (AdGroupBidLandscape bidLandscape in page.entries) {
            writer.WriteLine("Found ad group bid landscape with ad group id '{0}', type '{1}', " +
                "current: '{2}', start date '{3}', end date '{4}', and landscape points",
                bidLandscape.adGroupId, bidLandscape.type, bidLandscape.landscapeCurrent,
                bidLandscape.startDate, bidLandscape.endDate);
            foreach (BidLandscapeLandscapePoint point in bidLandscape.landscapePoints) {
              writer.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, marginalCpc: {3}, " +
                  "impressions: {4}", point.bid.microAmount, point.bid.microAmount,
                  point.clicks, point.cost.microAmount, point.marginalCpc.microAmount,
                  point.impressions);
            }
          }
        } else {
          writer.WriteLine("No ad group bid landscapes were found.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get ad group bid landscapes.", ex);
      }
    }
  }
}

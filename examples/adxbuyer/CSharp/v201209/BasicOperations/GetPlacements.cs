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
using Google.Api.Ads.AdWords.v201209;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201209 {
  /// <summary>
  /// This code example gets all placements in an account. To add placements,
  /// run AddPlacements.cs.
  ///
  /// Tags: AdGroupCriterionService.get
  /// </summary>
  public class GetPlacements : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetPlacements codeExample = new GetPlacements();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
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
        return "This code example gets all placements in an account. To add placements, run " +
            "AddPlacements.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(
              AdWordsService.v201209.AdGroupCriterionService);

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "AdGroupId", "PlacementUrl"};

      // Select only keywords.
      Predicate predicate = new Predicate();
      predicate.field = "CriteriaType";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] {"PLACEMENT"};
      selector.predicates = new Predicate[] {predicate};

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      AdGroupCriterionPage page = new AdGroupCriterionPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the keywords.
          page = adGroupCriterionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;

            foreach (AdGroupCriterion adGroupCriterion in page.entries) {
              bool isNegative = (adGroupCriterion is NegativeAdGroupCriterion);

              // If you are retrieving multiple type of criteria, then you may
              // need to check for
              //
              // if (adGroupCriterion is Placement) { ... }
              //
              // to identify the criterion type.
              Placement placement = (Placement) adGroupCriterion.criterion;
              if (isNegative) {
                Console.WriteLine("{0}) Negative placement with ad group ID = '{1}', placement " +
                    "ID = '{2}', and url = '{3}' was found.", i, adGroupCriterion.adGroupId,
                    placement.id, placement.url);
              } else {
                Console.WriteLine("{0}) Placement with ad group ID = '{1}', placement ID = '{2}' " +
                    "and url = '{3}' was found.", i, adGroupCriterion.adGroupId,
                    placement.id, placement.url);
              }
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of placements found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve placements.");
      }
    }
  }
}

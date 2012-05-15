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
  /// This code example gets all targeting criteria for a campaign. To set
  /// campaign targeting criteria, run AddCampaignTargetingCriteria.cs. To get
  /// campaigns, run GetCampaigns.cs.
  ///
  /// Tags: CampaignCriterionService.get
  /// </summary>
  public class GetCampaignTargetingCriteria : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetCampaignTargetingCriteria();
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
        return "This code example gets all targeting criteria for a campaign. To set campaign " +
            "targeting criteria, run AddCampaignTargetingCriteria.cs. To get campaigns, run " +
            "GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"CAMPAIGN_ID"};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the CampaignCriterionService.
      CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService) user.GetService(
              AdWordsService.v201109_1.CampaignCriterionService);

      long campaignId = long.Parse(parameters["CAMPAIGN_ID"]);

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "CriteriaType", "PlacementUrl"};

      // Set the filters.
      Predicate campaignPredicate = new Predicate();
      campaignPredicate.field = "CampaignId";
      campaignPredicate.@operator = PredicateOperator.EQUALS;
      campaignPredicate.values = new string[] {campaignId.ToString()};

      Predicate placementPredicate = new Predicate();
      placementPredicate.field = "CriteriaType";
      placementPredicate.@operator = PredicateOperator.EQUALS;
      placementPredicate.values = new string[] {"PLACEMENT"};

      selector.predicates = new Predicate[] {campaignPredicate, placementPredicate};

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      CampaignCriterionPage page = new CampaignCriterionPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get all campaign targets.
          page = campaignCriterionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (CampaignCriterion campaignCriterion in page.entries) {
              Placement placement = campaignCriterion.criterion as Placement;

              writer.WriteLine("{0}) Placement with ID {1} and url {2} was found.", i,
                  placement.id, placement.url);
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        writer.WriteLine("Number of placements found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get campaign targeting criteria.", ex);
      }
    }
  }
}

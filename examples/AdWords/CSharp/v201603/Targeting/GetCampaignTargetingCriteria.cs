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
  /// This code example gets all targeting criteria for a campaign. To set
  /// campaign targeting criteria, run AddCampaignTargetingCriteria.cs. To get
  /// campaigns, run GetCampaigns.cs.
  /// </summary>
  public class GetCampaignTargetingCriteria : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetCampaignTargetingCriteria codeExample = new GetCampaignTargetingCriteria();
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
        return "This code example gets all targeting criteria for a campaign. To set campaign " +
            "targeting criteria, run AddCampaignTargetingCriteria.cs. To get campaigns, run " +
            "GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign from which targeting
    /// criteria are retrieved.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the CampaignCriterionService.
      CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService) user.GetService(
              AdWordsService.v201603.CampaignCriterionService);

      // Create the selector.
      Selector selector = new Selector() {
        fields = new string[] {
          Criterion.Fields.Id, Criterion.Fields.CriteriaType, CampaignCriterion.Fields.CampaignId
        },
        predicates = new Predicate[] {
          Predicate.Equals(CampaignCriterion.Fields.CampaignId, campaignId)
        },
        paging = Paging.Default
      };

      CampaignCriterionPage page = new CampaignCriterionPage();

      try {
        do {
          // Get all campaign targets.
          page = campaignCriterionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;
            foreach (CampaignCriterion campaignCriterion in page.entries) {
              string negative = (campaignCriterion is NegativeCampaignCriterion) ? "Negative " : "";
              Console.WriteLine("{0}) {1}Campaign criterion with id = '{2}' and Type = {3} was " +
                  " found for campaign id '{4}'", i + 1, negative, campaignCriterion.criterion.id,
                  campaignCriterion.criterion.type, campaignCriterion.campaignId);
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of campaign targeting criteria found: {0}",
            page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to get campaign targeting criteria.", e);
      }
    }
  }
}

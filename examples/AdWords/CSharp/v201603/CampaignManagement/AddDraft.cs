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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example illustrates how to create a draft and access its
  /// associated draft campaign. See the Campaign Drafts and Experiments guide
  /// for more information:
  /// https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments
  /// </summary>
  public class AddDraft : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddDraft codeExample = new AddDraft();
      Console.WriteLine(codeExample.Description);
      try {
        long baseCampaignId = long.Parse("INSERT_BASE_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), baseCampaignId);
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
        return "This code example illustrates how to create a draft and access its associated " +
            "draft campaign. See the Campaign Drafts and Experiments guide for more " +
            "information: " +
            "https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="baseCampaignId">Id of the campaign to use as base of the
    /// draft.</param>
    public void Run(AdWordsUser user, long baseCampaignId) {
      // Get the DraftService.
      DraftService draftService = (DraftService) user.GetService(
          AdWordsService.v201603.DraftService);
      Draft draft = new Draft() {
        baseCampaignId = baseCampaignId,
        draftName = "Test Draft #" + ExampleUtilities.GetRandomString()
      };

      DraftOperation draftOperation = new DraftOperation() {
        @operator = Operator.ADD,
        operand = draft
      };

      try {
        draft = draftService.mutate(new DraftOperation[] {draftOperation}).value[0];

        Console.WriteLine("Draft with ID {0}, base campaign ID {1} and draft campaign ID " +
            "{2} created.", draft.draftId, draft.baseCampaignId, draft.draftCampaignId);

        // Once the draft is created, you can modify the draft campaign as if it
        // were a real campaign. For example, you may add criteria, adjust bids,
        // or even include additional ads. Adding a criterion is shown here.
        CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService) user.GetService(
              AdWordsService.v201603.CampaignCriterionService);

        Language language = new Language() {
          id = 1003L // Spanish
        };  

        // Make sure to use the draftCampaignId when modifying the virtual draft
        // campaign.
        CampaignCriterion campaignCriterion = new CampaignCriterion() {
          campaignId = draft.draftCampaignId,
          criterion = language
        };

        CampaignCriterionOperation criterionOperation = new CampaignCriterionOperation() {
          @operator = Operator.ADD,
          operand = campaignCriterion
        };

        campaignCriterion = campaignCriterionService.mutate(
            new CampaignCriterionOperation[] {criterionOperation}).value[0];

        Console.WriteLine("Draft updated to include criteria in draft campaign ID {0}.",
            draft.draftCampaignId);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create draft campaign and add " +
            "criteria.", e);
      }
    }
  }
}

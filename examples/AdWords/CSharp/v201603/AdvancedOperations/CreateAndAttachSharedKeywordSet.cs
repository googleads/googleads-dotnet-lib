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
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example creates a shared keyword list, adds keywords to the list
  /// and attaches it to an existing campaign. To get the list of campaigns,
  /// run GetCampaigns.cs.
  /// </summary>
  public class CreateAndAttachSharedKeywordSet : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      CreateAndAttachSharedKeywordSet codeExample = new CreateAndAttachSharedKeywordSet();
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
        return "This code example creates a shared keyword list, adds keywords to the list " +
            "and attaches it to an existing campaign. To get the list of campaigns, run " +
            "GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign to which keywords are added.</param>
    public void Run(AdWordsUser user, long campaignId) {
      try {
        // Create a shared set.
        SharedSet sharedSet = CreateSharedKeywordSet(user);

        Console.WriteLine("Shared set with id = {0}, name = {1}, type = {2}, status = {3} " +
            "was created.", sharedSet.sharedSetId, sharedSet.name, sharedSet.type,
            sharedSet.status);

        // Add new keywords to the shared set.
        string[] keywordTexts = new string[] {"mars cruise", "mars hotels"};
        SharedCriterion[] sharedCriteria = AddKeywordsToSharedSet(user, sharedSet.sharedSetId,
            keywordTexts);
        foreach (SharedCriterion sharedCriterion in sharedCriteria) {
          Keyword keyword = sharedCriterion.criterion as Keyword;
          Console.WriteLine("Added keyword with id = {0}, text = {1}, matchtype = {2} to " +
              "shared set with id = {3}.", keyword.id, keyword.text, keyword.matchType,
              sharedSet.sharedSetId);
        }

        // Attach the shared set to the campaign.
        CampaignSharedSet attachedSharedSet = AttachSharedSetToCampaign(user, campaignId,
            sharedSet.sharedSetId);

        Console.WriteLine("Attached shared set with id = {0} to campaign id {1}.",
            attachedSharedSet.sharedSetId, attachedSharedSet.campaignId);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create shared keyword set and attach " +
            "it to a campaign.", e);
      }
    }

    /// <summary>
    /// Create a shared keyword set.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <returns>The shared set.</returns>
    public SharedSet CreateSharedKeywordSet(AdWordsUser user) {
      // Get the SharedSetService.
      SharedSetService sharedSetService = (SharedSetService)
          user.GetService(AdWordsService.v201603.SharedSetService);

      SharedSetOperation operation = new SharedSetOperation();
      operation.@operator = Operator.ADD;
      SharedSet sharedSet = new SharedSet();
      sharedSet.name = "API Negative keyword list - " + ExampleUtilities.GetRandomString();
      sharedSet.type = SharedSetType.NEGATIVE_KEYWORDS;
      operation.operand = sharedSet;

      SharedSetReturnValue retval = sharedSetService.mutate(new SharedSetOperation[] {operation});
      return retval.value[0];
    }

    /// <summary>
    /// Adds a set of keywords to a shared set.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="sharedSetId">The shared set id.</param>
    /// <param name="keywordTexts">The keywords to be added to the shared set.</param>
    /// <returns>The newly added set of shared criteria.</returns>
    public SharedCriterion[] AddKeywordsToSharedSet(AdWordsUser user, long sharedSetId,
        string[] keywordTexts) {
      // Get the SharedCriterionService.
      SharedCriterionService sharedCriterionService = (SharedCriterionService)
          user.GetService(AdWordsService.v201603.SharedCriterionService);

      List<SharedCriterionOperation> operations = new List<SharedCriterionOperation>();
      foreach (string keywordText in keywordTexts) {
        Keyword keyword = new Keyword();
        keyword.text = keywordText;
        keyword.matchType = KeywordMatchType.BROAD;

        SharedCriterion sharedCriterion = new SharedCriterion();
        sharedCriterion.criterion = keyword;
        sharedCriterion.negative = true;
        sharedCriterion.sharedSetId = sharedSetId;
        SharedCriterionOperation operation = new SharedCriterionOperation();
        operation.@operator = Operator.ADD;
        operation.operand = sharedCriterion;
        operations.Add(operation);
      }

      SharedCriterionReturnValue retval = sharedCriterionService.mutate(operations.ToArray());
      return retval.value;
    }

    /// <summary>
    /// Attaches a shared set to a campaign.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="sharedSetId">The shared set id.</param>
    /// <returns>A CampaignSharedSet object that represents a binding between
    /// the specified campaign and the shared set.</returns>
    public CampaignSharedSet AttachSharedSetToCampaign(AdWordsUser user, long campaignId,
        long sharedSetId) {
      // Get the CampaignSharedSetService.
      CampaignSharedSetService campaignSharedSetService = (CampaignSharedSetService)
          user.GetService(AdWordsService.v201603.CampaignSharedSetService);

      CampaignSharedSet campaignSharedSet = new CampaignSharedSet();
      campaignSharedSet.campaignId = campaignId;
      campaignSharedSet.sharedSetId = sharedSetId;

      CampaignSharedSetOperation operation = new CampaignSharedSetOperation();
      operation.@operator = Operator.ADD;
      operation.operand = campaignSharedSet;

      CampaignSharedSetReturnValue retval = campaignSharedSetService.mutate(
          new CampaignSharedSetOperation[] {operation});
      return retval.value[0];
    }
  }
}

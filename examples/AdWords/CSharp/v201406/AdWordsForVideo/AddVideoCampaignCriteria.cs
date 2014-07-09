// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201406;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {

  /// <summary>
  /// This code example demonstrates how to add campaign-level criteria
  /// targeting.
  ///
  /// Tags: VideoCampaignCriterionService.mutate
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class AddVideoCampaignCriteria : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example demonstrates how to add campaign-level criteria targeting.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddVideoCampaignCriteria codeExample = new AddVideoCampaignCriteria();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign ID.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the VideoCampaignCriterionService.
      VideoCampaignCriterionService service =
          (VideoCampaignCriterionService) user.GetService(
              AdWordsService.v201406.VideoCampaignCriterionService);

      try {
        VideoCampaignCriterion criterion = new VideoCampaignCriterion();
        criterion.campaignId = campaignId;

        // for a list of languages, see
        // https://devsite.googleplex.com/adwords/api/docs/appendix/languagecodes
        LanguageVideoCriterion englishCriterion = new LanguageVideoCriterion();
        englishCriterion.id = 1001L;
        criterion.criterion = englishCriterion;

        VideoCampaignCriterionOperation operation = new VideoCampaignCriterionOperation();
        operation.operand = criterion;
        operation.@operator = Operator.ADD;

        VideoCampaignCriterionReturnValue retval = service.mutate(
            new VideoCampaignCriterionOperation[] { operation });

        if (retval != null && retval.value != null && retval.value.Length > 0) {
          VideoCampaignCriterion newCriterion = retval.value[0];
          Console.WriteLine("Video campaign criterion with id = {0} and type = {1} was added " +
              "to campaign id {2}", newCriterion.criterion.id,
              newCriterion.criterion.BaseCriterionType, newCriterion.campaignId);
        } else {
          Console.WriteLine("No video campaign criteria were added.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add video campaign criteria.", ex);
      }
    }
  }
}
// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v201008;

using System;
using System.IO;
using System.Net;

namespace com.google.api.adwords.examples.v201008 {
  /// <summary>
  /// This code example retrieves all negative campaign criteria in an account.
  /// To add a negative campaign criterion, run AddNegativeCampaignCriterion.cs.
  ///
  /// Tags: CampaignCriterionService.get
  /// </summary>
  class GetAllNegativeCampaignCriteria : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves all negative campaign criteria in an account. To add " +
            "a negative campaign criterion, run AddNegativeCampaignCriterion.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignCriterionService.
      CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService)user.GetService(AdWordsService.v201008.
              CampaignCriterionService);

      CampaignCriterionSelector selector = new CampaignCriterionSelector();

      try {
        CampaignCriterionPage page = campaignCriterionService.get(selector);
        if (page != null && page.entries != null) {
          foreach (CampaignCriterion campaignCriterion in page.entries) {
            if (campaignCriterion.criterion is Keyword) {
              Keyword keyword = (Keyword)campaignCriterion.criterion;
              Console.WriteLine("Negative keyword campaign criterion with campaign ID = '{0}'," +
                  " criterion ID = '{1}', and text = '{2}' was found.",
                  campaignCriterion.campaignId, keyword.id, keyword.text);
            } else if (campaignCriterion.criterion is Placement) {
              Placement placement = (Placement)campaignCriterion.criterion;
              Console.WriteLine("Negative placement campaign criterion with campaign ID = " +
                  "'{0}', criterion ID = '{1}' and url = '{2}' was found.",
                  campaignCriterion.campaignId, placement.id, placement.url);
            } else {
              Console.WriteLine("Negative campaign criterion with campaign ID = " +
                  "'{0}', criterion ID = '{1}' and type = '{2}' was found.",
                  campaignCriterion.campaignId, campaignCriterion.criterion.id,
                  campaignCriterion.criterion.CriterionType);
            }
          }
        } else {
          Console.WriteLine("No negative campaign criteria were found.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve negative campaign criteria. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

// Copyright 2009, Google Inc. All Rights Reserved.
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
using com.google.api.adwords.v200909;

using System;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This code sample creates a new negative campaign placement given an
  /// existing campaign. To create a campaign, you can run AddCampaign.cs.
  /// </summary>
  class AddNegativeCampaignPlacement : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Create negative campaign placements";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      CampaignCriterionService service =
          (CampaignCriterionService) user.GetService(AdWordsService.v200909.CampaignCriterionService);

      NegativeCampaignCriterion criterion = new NegativeCampaignCriterion();

      criterion.campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));
      criterion.campaignIdSpecified = true;

      Placement placement = new Placement();
      placement.url = "http://www.example.com";
      criterion.criterion = placement;

      CampaignCriterionOperation campaignCriterionOperation = new CampaignCriterionOperation();
      campaignCriterionOperation.operatorSpecified = true;
      campaignCriterionOperation.@operator = Operator.ADD;
      campaignCriterionOperation.operand = criterion;

      try {
        CampaignCriterionReturnValue results =
            service.mutate(new CampaignCriterionOperation[] {campaignCriterionOperation});
        if (results != null && results.value != null && results.value.Length > 0) {
          Placement result = results.value[0].criterion as Placement;
          Console.WriteLine("New negative campaign criterion with url = " +
              "\"{0}\" and id = {1} was created.", result.url, result.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create negative campaign criterion. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

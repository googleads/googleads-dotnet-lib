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

using System;

using com.google.api.adwords.lib;
using com.google.api.adwords.v200902.CampaignService;

namespace com.google.api.adwords.samples.v200902 {
  /// <summary>
  /// This code sample creates a new campaign.
  /// </summary>
  class AddCampaign : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {return "Create a new Campaign";}
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      CampaignService service =
          (CampaignService) user.GetService(ApiServices.v200902.CampaignService);

      Campaign campaign = new Campaign();

      // Generate a campaign name.

      string campaignName =
        string.Format("Campaign - {0}", DateTime.Now.ToString("yyyy-M-d H:m:s"));
      campaign.name = string.Format(campaignName);

      // Required: Set the campaign status.

      campaign.status = CampaignStatus.ACTIVE;
      campaign.statusSpecified = true;

      // Required: Specify the currency and budget amount.

      Budget budget = new Budget();
      Money amount = new Money();
      amount.currencyCode = "USD";
      amount.microAmountSpecified = true;
      amount.microAmount = 50000000;

      budget.amount = amount;

      // Required: Specify the bidding strategy.

      campaign.biddingStrategy = new ManualCPC();

      // Optional: Specify the budget period and delivery method.

      budget.periodSpecified = true;
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethodSpecified = true;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      campaign.budget = budget;

      // Define an Add operation to add the campaign.

      CampaignOperation campaignOperation = new CampaignOperation();

      campaignOperation.operatorSpecified = true;
      campaignOperation.@operator = Operator.ADD;
      campaignOperation.operand = campaign;

      try {
        CampaignReturnValue results =
          service.mutate(new CampaignOperation[] {campaignOperation});
        if (results != null && results.value != null && results.value.Length > 0) {
          Console.WriteLine("New campaign with name = \"{0}\" and id = " +
              "\"{1}\" was created.", results.value[0].name, results.value[0].id.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create campaign. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

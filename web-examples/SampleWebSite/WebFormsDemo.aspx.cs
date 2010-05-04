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
using com.google.api.adwords.v200909;

using System;

namespace com.google.api.adwords.websamples {
  /// <summary>
  /// This page shows how to create a sample using webforms and server
  /// side callbacks.
  /// </summary>
  public partial class WebFormsDemoPage : System.Web.UI.Page {

    /// <summary>
    /// The default page load handler.
    /// </summary>
    /// <param name="sender">The object that raised this event.</param>
    /// <param name="e">The optional event arguments.</param>
    protected void Page_Load(object sender, EventArgs e) {

    }

    /// <summary>
    /// Server-side handler for onClick of btnCreateCampaign button.
    /// </summary>
    /// <param name="sender">The object that raised this event.</param>
    /// <param name="e">The optional event arguments.</param>
    protected void btnCreateCampaign_Click(object sender, EventArgs e) {
      this.lblCampaignMessage.Text = string.Format("Created campaign #{0} successfully!",
          AddAdWordsCampaign());
    }

    /// <summary>
    /// Creates an AdWords campaign.
    /// </summary>
    /// <returns>The campaign id.</returns>
    public long AddAdWordsCampaign() {
      AdWordsUser user = new AdWordsUser();
      long retVal = -1;
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v200909.CampaignService);

      // Create campaign.
      Campaign campaign = new Campaign();
      campaign.name = "Interplanetary Cruise #" + GetTimeStamp();
      campaign.statusSpecified = true;
      campaign.status = CampaignStatus.PAUSED;
      campaign.biddingStrategy = new ManualCPC();

      Budget budget = new Budget();
      budget.periodSpecified = true;
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethodSpecified = true;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget.amount = new Money();
      budget.amount.microAmountSpecified = true;
      budget.amount.microAmount = 50000000;

      campaign.budget = budget;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;
      operation.operand = campaign;

      try {
        // Add campaign.
        CampaignReturnValue result = campaignService.mutate((new CampaignOperation[]
            {operation}));

        // Display campaigns.
        if (result != null && result.value != null) {
          foreach (Campaign campaignResult in result.value) {
            retVal = campaignResult.id;
            break;
          }
        }
      } catch (Exception ex) {

      }
      return retVal;
    }

    /// <summary>
    /// Gets the current unix timestamp. This is used to generate a unique
    /// campaign name.
    /// </summary>
    /// <returns>The unix time stamp.</returns>
    protected string GetTimeStamp() {
      return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
    }
  }
}

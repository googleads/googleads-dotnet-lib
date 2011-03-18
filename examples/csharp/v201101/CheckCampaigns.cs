// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.IO;
using System.Net;
using System.Web.Services.Protocols;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example shows how to use the validateOnly header to validate
  /// an API request. No objects will be created, but exceptions will
  /// still be thrown.
  ///
  /// Tags: CampaignService.mutate
  /// </summary>
  class CheckCampaigns : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to use the validateOnly header to validate an API " +
            "request. No objects will be created, but exceptions will still be thrown.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CheckCampaigns();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201101.CampaignService);

      // Turn on the validation mode.
      campaignService.RequestHeader.validateOnly = true;

      // Create the good campaign.
      Campaign goodCampaign = new Campaign();
      goodCampaign.name = "Campaign #" + GetTimeStamp();
      goodCampaign.status = CampaignStatus.PAUSED;
      goodCampaign.biddingStrategy = new ManualCPC();

      Budget budget = new Budget();
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.amount = new Money();
      budget.amount.microAmount = 50000000;

      goodCampaign.budget = budget;

      CampaignOperation operation = new CampaignOperation();
      operation.@operator = Operator.ADD;
      operation.operand = goodCampaign;

      try {
        CampaignReturnValue retVal = campaignService.mutate((new CampaignOperation[] {operation}));
        // Since validation is ON, result will be null.
        Console.WriteLine("Campaign request validated successfully.");
      } catch (AdWordsApiException ex) {
        // This block will be hit if there is a validation error from the server.
        Console.WriteLine("There were validation error(s) while adding campaigns.");

        if (ex.ApiException != null) {
          foreach (ApiError error in ((ApiException) ex.ApiException).errors) {
            Console.WriteLine("  Error type is '{0}' and fieldPath is '{1}'.",
                error.ApiErrorType, error.fieldPath);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to validate campaign. Exception says \"{0}\"",
            ex.Message);
      }

      // Create the bad campaign.
      Campaign badCampaign = new Campaign();
      badCampaign.name = "Campaign #" + GetTimeStamp();
      badCampaign.status = CampaignStatus.PAUSED;
      badCampaign.budget = budget;

      // Provide an invalid bidding strategy that will cause an exception
      // during validation. The error thrown is
      // RequiredError.REQUIRED @ operations[0].operand.biddingStrategy.
      badCampaign.biddingStrategy = null;

      operation = new CampaignOperation();
      operation.@operator = Operator.ADD;
      operation.operand = badCampaign;

      try {
        CampaignReturnValue retVal = campaignService.mutate((new CampaignOperation[] {operation}));
        // Since we have purposefully added a validation error, the next
        // line won't be hit.
        Console.WriteLine("Campaign request validated successfully.");
      } catch (AdWordsApiException ex) {
        // This block will be hit if there is a validation error from the server.
        Console.WriteLine("There were validation error(s) while adding campaigns.");

        if (ex.ApiException != null) {
          foreach (ApiError error in ((ApiException) ex.ApiException).errors) {
            Console.WriteLine("  Error type is '{0}' and fieldPath is '{1}'.",
                error.ApiErrorType, error.fieldPath);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to validate campaign. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

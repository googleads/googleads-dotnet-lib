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
using System.IO;
using System.Net;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This code example shows how to check for conversion optimizer eligibility
  /// by attempting to set the bidding transition with the validate only header
  /// set to true.
  /// </summary>
  class GetConversionOptimizerEligibility : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to check for conversion optimizer eligibility by" +
            " attempting to set the bidding transition with the validate only header set to" +
            " true.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v200909.CampaignService);
      // Turn on validation mode.
      campaignService.RequestHeader.validateOnly = true;

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      // Create campaign.
      Campaign campaign = new Campaign();
      campaign.id = campaignId;
      campaign.idSpecified = true;

      // Create bidding transition.
      ConversionOptimizerBiddingTransition conversionOptimizerBiddingTransition
          = new ConversionOptimizerBiddingTransition();

      // Create conversion optimizer bidding strategy.
      ConversionOptimizer conversionOptimizer = new ConversionOptimizer();
      conversionOptimizer.pricingModel = PricingModel.CONVERSIONS;
      conversionOptimizer.pricingModelSpecified = true;
      conversionOptimizerBiddingTransition.targetBiddingStrategy = conversionOptimizer;

      // Create converstion optimizer ad group bids.
      ConversionOptimizerAdGroupBids conversionOptimizerAdGroupBids
          = new ConversionOptimizerAdGroupBids();
      conversionOptimizerBiddingTransition.explicitAdGroupBids = conversionOptimizerAdGroupBids;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.biddingTransition = conversionOptimizerBiddingTransition;
      operation.operatorSpecified = true;
      operation.@operator = Operator.SET;
      operation.operand = campaign;

      CampaignOperation[] operations = new CampaignOperation[] {operation};

      try {
        // Check that campaign is eligible for conversion optimization.
        CampaignReturnValue result = campaignService.mutate(operations);

        Console.WriteLine("Campaign with id = '{0}' is eligible to use conversion optimizer.",
            campaign.id);
      } catch (AdWordsApiException ex) {
        ApiException apiException = ex.ApiException as ApiException;
        if (apiException != null) {
          foreach (ApiError error in apiException.errors) {
            if (error is BiddingTransitionError) {
              BiddingTransitionError biddingTransitionError = (BiddingTransitionError) error;
              Console.WriteLine("Campaign with id = '{0}' is not eligible to use conversion" +
                  " optimizer for reason '{1}'.", campaign.id, biddingTransitionError.reason);
            }
          }
        } else {
          Console.WriteLine("Campaign with id = '{0}' is not eligible to use conversion" +
            " optimizer. Exception says '{1}'", ex.Message);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get conversion optimizer eligibility for campaign(s). " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

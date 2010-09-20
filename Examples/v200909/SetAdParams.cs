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

namespace com.google.api.adwords.examples.v200909 {
  /// <summary>
  /// This code example illustrates how to create a text ad with ad parameters.
  /// To add an ad group, run AddAdGroup.cs. To add an ad group criterion,
  /// run AddAdGroupCriterion.cs.
  ///
  /// Tags: AdGroupAdService.mutate, AdParamService.mutate
  /// </summary>
  class SetAdParams : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create a text ad with ad parameters." +
            " To add an ad group, run AddAdGroup.cs. To add an ad group criterion," +
            " run AddAdGroupCriterion.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      AdGroupAdService adGroupAdService = (AdGroupAdService) user.GetService(
          AdWordsService.v200909.AdGroupAdService);

      AdParamService adParamService = (AdParamService) user.GetService(
          AdWordsService.v200909.AdParamService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));
      long criterionId = long.Parse(_T("INSERT_CRITERION_ID_HERE"));

      TextAd textAd = new TextAd();
      textAd.url = "http://www.example.com";
      textAd.displayUrl = "example.com";
      textAd.headline = " Mars Cruises";
      textAd.description1 = "Low-gravity fun for {param1:cheap}.";
      textAd.description2 = "Only {param2:a few} seats left!";

      AdGroupAd adOperand = new AdGroupAd();
      adOperand.adGroupIdSpecified = true;
      adOperand.adGroupId = adGroupId;

      adOperand.status = AdGroupAdStatus.ENABLED;
      adOperand.statusSpecified = true;
      adOperand.ad = textAd;

      AdGroupAdOperation adOperation = new AdGroupAdOperation();

      adOperation.operand = adOperand;
      adOperation.@operator = Operator.ADD;
      adOperation.operatorSpecified = true;

      try {
        AdGroupAdReturnValue retVal =
            adGroupAdService.mutate(new AdGroupAdOperation[] {adOperation});

        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          Console.WriteLine("Text ad id {0} was successfully added.", retVal.value[0].ad.id);
        } else {
          Console.WriteLine("No ads were created.");
          return;
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create ad(s). Exception says \"{0}\"", ex.Message);
        return;
      }

      // Prepare for setting ad parameters.
      AdParam priceParam = new AdParam();
      priceParam.adGroupIdSpecified = true;
      priceParam.adGroupId = adGroupId;
      priceParam.criterionIdSpecified = true;
      priceParam.criterionId = criterionId;
      priceParam.paramIndex = 1;
      priceParam.paramIndexSpecified = true;
      priceParam.insertionText = "$100";

      AdParamOperation priceOperation = new AdParamOperation();
      priceOperation.operatorSpecified = true;
      priceOperation.@operator = Operator.SET;
      priceOperation.operand = priceParam;

      AdParam seatParam = new AdParam();
      seatParam.adGroupIdSpecified = true;
      seatParam.adGroupId = adGroupId;
      seatParam.criterionIdSpecified = true;
      seatParam.criterionId = criterionId;
      seatParam.paramIndex = 2;
      seatParam.paramIndexSpecified = true;
      seatParam.insertionText = "50";

      AdParamOperation seatOperation = new AdParamOperation();
      seatOperation.operatorSpecified = true;
      seatOperation.@operator = Operator.SET;
      seatOperation.operand = seatParam;

      try {
        // Set ad parameters.
        AdParam [] newAdParams = adParamService.mutate(new AdParamOperation[]
            {priceOperation, seatOperation});
        if (newAdParams != null) {
          Console.WriteLine("Ad parameters were successfully updated.");
        } else {
          Console.WriteLine("No ad parameters were set.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to set ad ad parameter(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

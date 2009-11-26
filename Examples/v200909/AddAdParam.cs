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
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This sample shows how to add an AdParam. We create a text ad that
  /// uses ad params, add keywords, and then add AdParams to those keywords.
  /// </summary>
  class AddAdParam : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This sample shows how to add an AdParam. We create a text ad that" +
            " uses ad params, add keywords, and then add AdParams to those keywords.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.</param>
    public override void Run(AdWordsUser user) {
      AdParamService adParamService =
          (AdParamService) user.GetService(AdWordsService.v200909.AdParamService);
      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));

      long adId = CreateTextAd(user, adGroupId);
      long criterionId = CreateKeyword(user, adGroupId, "mug");

      // For mug, set $8 as price, 250 as count.
      AdParam param1 = new AdParam();
      param1.adGroupIdSpecified = true;
      param1.adGroupId = adGroupId;
      param1.criterionIdSpecified = true;
      param1.criterionId = criterionId;
      param1.insertionText = "$8";
      param1.paramIndexSpecified = true;
      param1.paramIndex = 1;

      AdParam param2 = new AdParam();
      param2.adGroupIdSpecified = true;
      param2.adGroupId = adGroupId;
      param2.criterionIdSpecified = true;
      param2.criterionId = criterionId;
      param2.insertionText = "250";
      param2.paramIndexSpecified = true;
      param2.paramIndex = 2;

      AdParamOperation operation1 = new AdParamOperation();
      operation1.operatorSpecified = true;
      operation1.@operator = Operator.SET;
      operation1.operand = param1;

      AdParamOperation operation2 = new AdParamOperation();
      operation2.operatorSpecified = true;
      operation2.@operator = Operator.SET;
      operation2.operand = param2;

      try {
        AdParam[] adParams = adParamService.mutate(new AdParamOperation[]
            {operation1, operation2});
        if (adParams != null) {
          foreach (AdParam adParam in adParams) {
            Console.WriteLine("Added an adparam with index={0} and insertionText='{1}' " +
                "to criterion={2}", adParam.paramIndex, adParam.insertionText, adParam.criterionId);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create AdParams at Keyword level. Exception says \"{0}\"",
            ex.Message);
      }
    }

    /// <summary>
    /// Creates a keyword for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The AdGroup id for which the keyword is created.</param>
    /// <param name="keywordText">The keyword text.</param>
    /// <returns>The keyword id.</returns>
    public long CreateKeyword(AdWordsUser user, long adGroupId, string keywordText) {
      long keywordId = 0;
      AdGroupCriterionService adGroupCriterionService =
         (AdGroupCriterionService) user.GetService(AdWordsService.v200909.AdGroupCriterionService);

      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.ADD;
      operation.operatorSpecified = true;
      operation.operand = new BiddableAdGroupCriterion();
      operation.operand.adGroupId = adGroupId;
      operation.operand.adGroupIdSpecified = true;

      Keyword keyword = new Keyword();
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.matchTypeSpecified = true;
      keyword.text = keywordText;

      operation.operand.criterion = keyword;
      AdGroupCriterionReturnValue retVal =
          adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation});
      if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
        keywordId = retVal.value[0].criterion.id;
      }
      return keywordId;
    }

    /// <summary>
    /// Create a text Ad under an AdGroup.
    /// </summary>
    /// <param name="user">The user fow which the text ad is created.</param>
    /// <param name="adGroupId">Id of the AdGroup under which the text Ad
    /// should be created.</param>
    /// <returns>Id of the newly created Ad.</returns>
    private long CreateTextAd(AdWordsUser user, long adGroupId) {
      long adId = 0;

      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v200909.AdGroupAdService);
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.operatorSpecified = true;
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = new AdGroupAd();
      adGroupAdOperation.operand.adGroupIdSpecified = true;
      adGroupAdOperation.operand.adGroupId = adGroupId;

      TextAd textAd = new TextAd();
      textAd.headline = "Elvis {Keyword:Collectible} Sale";
      textAd.description1 = "Get yours today for {param1:cheap}.";
      textAd.description2 = "Only {param2:a few} left in stock.";
      textAd.displayUrl = "www.example.com";
      textAd.url = "http://www.example.com";

      adGroupAdOperation.operand.ad = textAd;

      AdGroupAdReturnValue retVal =
          adGroupAdService.mutate(new AdGroupAdOperation[] {adGroupAdOperation});
      if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
        adId = retVal.value[0].ad.id;
      }
      return adId;
    }
  }
}

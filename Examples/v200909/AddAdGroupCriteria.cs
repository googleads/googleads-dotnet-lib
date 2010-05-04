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
  /// This code example adds a keyword and a placement to an ad group. To
  /// get ad groups, run GetAllAdGroups.cs.
  /// </summary>
  class AddAdGroupCriteria : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a keyword and a placement to an ad group. To get " +
            "ad groups, run GetAllAdGroups.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v200909.AdGroupCriterionService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));

      // Create keyword.
      Keyword keyword = new Keyword();
      keyword.text = "mars cruise";
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.matchTypeSpecified = true;

      // Create biddable ad group criterion.
      AdGroupCriterion keywordCriterion = new BiddableAdGroupCriterion();
      keywordCriterion.adGroupId = adGroupId;
      keywordCriterion.adGroupIdSpecified = true;
      keywordCriterion.criterion = keyword;

      // Create placement.
      Placement placement = new Placement();
      placement.url = "http://mars.google.com";

      // Create biddable ad group criterion.
      AdGroupCriterion placementCriterion = new BiddableAdGroupCriterion();
      placementCriterion.adGroupId = adGroupId;
      placementCriterion.adGroupIdSpecified = true;
      placementCriterion.criterion = placement;

      // Create operations.
      AdGroupCriterionOperation keywordOperation = new AdGroupCriterionOperation();
      keywordOperation.@operator = Operator.ADD;
      keywordOperation.operatorSpecified = true;
      keywordOperation.operand = keywordCriterion;

      AdGroupCriterionOperation placementOperation = new AdGroupCriterionOperation();
      placementOperation.@operator = Operator.ADD;
      placementOperation.operatorSpecified = true;
      placementOperation.operand = placementCriterion;

      try {
        AdGroupCriterionReturnValue result = adGroupCriterionService.mutate(
            new AdGroupCriterionOperation[] {keywordOperation, placementOperation});

        if (result != null && result.value != null) {
          foreach (AdGroupCriterion adGroupCriterion in result.value) {
            Console.WriteLine("Ad group criterion with ad group id = '{0}, criterion id = '{1} " +
                "and type = '{2}' was created.", adGroupCriterion.adGroupId,
                adGroupCriterion.criterion.id, adGroupCriterion.criterion.CriterionType);
          }
        } else {
          Console.WriteLine("No ad group criteria were added.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create ad group criteria. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

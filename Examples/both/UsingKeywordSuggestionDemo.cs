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
using System.Collections.Generic;

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;
using com.google.api.adwords.v200902.AdGroupCriterionService;

using KeywordV200902 =
    com.google.api.adwords.v200902.AdGroupCriterionService.Keyword;

namespace com.google.api.adwords.samples.both {
  /// <summary>
  /// Shows how to use both v13 and v200902 APIs in a single sample.
  /// </summary>
  class UsingKeywordSuggestionDemo : SampleBase{
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Shows how to use both v13 and v200902 APIs in a single sample.";
      }
    }

    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override void Run(AdWordsUser user) {
      KeywordToolService keywordToolService =
          (KeywordToolService) user.GetService(ApiServices.v13.KeywordToolService);

      // Use v13 KeywordToolService to get keyword variations.
      SeedKeyword seed = new SeedKeyword();
      seed.text = "Red Planet";
      seed.type = KeywordType.Broad;

      KeywordVariations variations = keywordToolService.getKeywordVariations(
          new SeedKeyword[] {seed}, true, new string[] {"en"}, new string[] {"US"});

      // Add top 3 variations as keywords using v200902.
      AdGroupCriterionService service =
          (AdGroupCriterionService) user.GetService(ApiServices.v200902.AdGroupCriterionService);

      int count = (variations.moreSpecific.Length > 3) ? 3 : variations.moreSpecific.Length;
      List<AdGroupCriterionOperation> operations =  new List<AdGroupCriterionOperation>();
      long adGroupId = long.Parse("INSERT_AD_GROUP_ID_HERE");

      for (int i = 0; i < count; i++) {
        KeywordV200902 keyword = new KeywordV200902();
        keyword.text = variations.moreSpecific[i].text;
        keyword.matchTypeSpecified = true;
        keyword.matchType = KeywordMatchType.BROAD;

        BiddableAdGroupCriterion criterion = new BiddableAdGroupCriterion();
        criterion.adGroupId = new AdGroupId();
        criterion.adGroupId.idSpecified = true;
        criterion.adGroupId.id = adGroupId;
        criterion.criterion = keyword;

        AdGroupCriterionOperation adGroupCriterionOperation = new AdGroupCriterionOperation();
        adGroupCriterionOperation.@operator = Operator.ADD;
        adGroupCriterionOperation.operatorSpecified = true;
        adGroupCriterionOperation.operand = criterion;

        operations.Add(adGroupCriterionOperation);
      }

      try {
        AdGroupCriterionReturnValue results = service.mutate(operations.ToArray());

        if (results != null && results.value != null && results.value.Length > 0) {
          foreach (AdGroupCriterion criterion in results.value) {
            KeywordV200902 result = criterion.criterion as KeywordV200902;
            Console.WriteLine("New keyword with text = \"{0}\" and id = \"{1}\" was created.",
                result.text, result.id.id);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create keyword at Ad group level. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

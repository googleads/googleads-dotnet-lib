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
using com.google.api.adwords.v13;
using com.google.api.adwords.v200906;

using System;
using System.Collections.Generic;

using KeywordV200906 = com.google.api.adwords.v200906.Keyword;

namespace com.google.api.adwords.samples.both {
  /// <summary>
  /// Shows how to use both v13 and v200906 APIs in a single sample.
  /// </summary>
  class UsingKeywordSuggestionDemo : SampleBase{
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Shows how to use both v13 and v200906 APIs in a single sample.";
      }
    }

    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override void Run(AdWordsUser user) {
      KeywordToolService keywordToolService =
          (KeywordToolService) user.GetService(AdWordsService.v13.KeywordToolService);

      // Use v13 KeywordToolService to get keyword variations.
      SeedKeyword seed = new SeedKeyword();
      seed.text = "Red Planet";
      seed.type = KeywordType.Broad;

      KeywordVariations variations = keywordToolService.getKeywordVariations(
          new SeedKeyword[] {seed}, true, new string[] {"en"}, new string[] {"US"});

      // Add top 3 variations as keywords using v200906.
      AdGroupCriterionService service =
          (AdGroupCriterionService) user.GetService(AdWordsService.v200906.AdGroupCriterionService);

      int count = (variations.moreSpecific.Length > 3) ? 3 : variations.moreSpecific.Length;
      List<AdGroupCriterionOperation> operations =  new List<AdGroupCriterionOperation>();
      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));

      for (int i = 0; i < count; i++) {
        KeywordV200906 keyword = new KeywordV200906();
        keyword.text = variations.moreSpecific[i].text;
        keyword.matchTypeSpecified = true;
        keyword.matchType = KeywordMatchType.BROAD;

        BiddableAdGroupCriterion criterion = new BiddableAdGroupCriterion();
        criterion.adGroupId = adGroupId;
        criterion.adGroupIdSpecified = true;
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
            KeywordV200906 result = criterion.criterion as KeywordV200906;
            Console.WriteLine("New keyword with text = \"{0}\" and id = \"{1}\" was created.",
                result.text, result.id);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create keyword at Ad group level. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

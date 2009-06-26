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
using com.google.api.adwords.v200906.AdGroupCriterionService;

namespace com.google.api.adwords.samples.v200906 {
  /// <summary>
  /// This code sample creates a new keyword given an existing ad group.
  /// To create an ad group, you can run AddAdGroup.cs.
  /// </summary>
  class AddAdGroupKeyword : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {return "Create AdGroup-level Keywords";}
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      AdGroupCriterionService service =
          (AdGroupCriterionService) user.GetService(ApiServices.v200906.AdGroupCriterionService);

      Keyword keyword = new Keyword();
      keyword.text = "mars cruise";
      keyword.matchTypeSpecified = true;
      keyword.matchType = KeywordMatchType.BROAD;

      BiddableAdGroupCriterion criterion = new BiddableAdGroupCriterion();
      criterion.adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));
      criterion.adGroupIdSpecified = true;
      criterion.criterion = keyword;

      AdGroupCriterionOperation adGroupCriterionOperation = new AdGroupCriterionOperation();
      adGroupCriterionOperation.@operator = Operator.ADD;
      adGroupCriterionOperation.operatorSpecified = true;
      adGroupCriterionOperation.operand = criterion;

      try {
        AdGroupCriterionReturnValue results =
            service.mutate(new AdGroupCriterionOperation[] {adGroupCriterionOperation});
        if (results != null && results.value != null && results.value.Length > 0) {
          Keyword result = results.value[0].criterion as Keyword;
          Console.WriteLine("New keyword with text = \"{0}\" and id = \"{1}\" was created.",
              result.text, result.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create keyword at Ad group level. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

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

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This code sample retrieves all active, non-negative criteria across an
  /// entire account. To add keywords to an existing ad group, you can run
  /// AddAdGroupKeyword.cs.
  /// </summary>
  class GetActiveCriteria : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Gets all active non-negative criteria in an account.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      AdGroupCriterionService service =
          (AdGroupCriterionService) user.GetService(AdWordsService.v200909.AdGroupCriterionService);

      AdGroupCriterionSelector selector = new AdGroupCriterionSelector();
      selector.criterionUseSpecified = true;
      selector.criterionUse = CriterionUse.BIDDABLE;
      selector.userStatuses = new UserStatus[] {UserStatus.ACTIVE};

      try {
        AdGroupCriterionPage results = service.get(selector);
        if (results != null && results.entries != null && results.entries.Length > 0) {
          foreach (AdGroupCriterion criterion in results.entries) {
            Console.WriteLine("Ad group id is \"{0}\", criterion id is " +
                "\"{1}\" and type is \"{2}\"", criterion.adGroupId,
                criterion.criterion.id, criterion.criterion.CriterionType);
          }
          Console.WriteLine("Account has {0} non-negative criteria.\n", results.entries.Length);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve active criterion in this Account. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

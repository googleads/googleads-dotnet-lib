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
using com.google.api.adwords.v201003;

using System;
using System.IO;
using System.Net;

namespace com.google.api.adwords.examples.v201003 {
  /// <summary>
  /// This code example gets all ad group criteria in an account. To add ad
  /// group criteria, run AddAdGroupCriteria.cs.
  /// </summary>
  class GetAllAdGroupCriteria : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all ad group criteria in an account. To add ad group " +
            "criteria, run AddAdGroupCriteria.cs.";
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
          (AdGroupCriterionService) user.GetService(AdWordsService.v201003.AdGroupCriterionService);

      try {
        // Get all ad group criteria.
        AdGroupCriterionPage adGroupCriterionPage = adGroupCriterionService.get(
            new AdGroupCriterionSelector());

        if (adGroupCriterionPage != null && adGroupCriterionPage.entries != null) {
          // Display ad group criteria.
          foreach (AdGroupCriterion adGroupCriterion in adGroupCriterionPage.entries) {
            if (adGroupCriterion.criterion is Keyword) {
              Keyword keyword = (Keyword) adGroupCriterion.criterion;
              Console.WriteLine("Keyword ad group criterion with ad group ID = '{0}', criterion" +
                  "ID = '{1}', text = '{2}' and matchType = '{3} was found.",
                  adGroupCriterion.adGroupId, keyword.id, keyword.text, keyword.matchType);
            } else if (adGroupCriterion.criterion is Placement) {
              Placement placement = (Placement) adGroupCriterion.criterion;
              Console.WriteLine("Placement ad group criterion with ad group ID = '{0}', criterion" +
                  " ID = '{1}' and url = '{2}' was found.", adGroupCriterion.adGroupId,
                  placement.id, placement.url);
            }
          }
        } else {
          Console.WriteLine("No ad group criteria found.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve criteria. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

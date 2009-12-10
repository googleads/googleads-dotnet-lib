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
using System.IO;
using System.Net;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This example gets all active ad group criteria in an ad group. To add ad
  /// group criteria, run AddAdGroupCriteria.cs. To get ad groups in an
  /// account, run GetAllAdGroups.cs.
  /// </summary>
  class GetAllActiveAdGroupCriteria : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all active ad group criteria in an ad group. To add ad group " +
            "criteria, run AddAdGroupCriteria.cs. To get ad groups in an account, run" +
            " GetAllAdGroups.cs.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v200909.AdGroupCriterionService);

      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));

      // Create selector.
      AdGroupCriterionSelector selector = new AdGroupCriterionSelector();
      selector.userStatuses = new UserStatus[] {UserStatus.ACTIVE};

      // Create id filter.
      AdGroupCriterionIdFilter idFilter = new AdGroupCriterionIdFilter();
      idFilter.adGroupId = adGroupId;
      idFilter.adGroupIdSpecified = true;

      selector.idFilters = new AdGroupCriterionIdFilter[] {idFilter};

      try {
        // Get all ad group criteria.
        AdGroupCriterionPage adGroupCriterionPage = adGroupCriterionService.get(
            selector);

        if (adGroupCriterionPage != null && adGroupCriterionPage.entries != null) {
          // Display ad group criteria.
          foreach (AdGroupCriterion adGroupCriterion in adGroupCriterionPage.entries) {
            if (adGroupCriterion.criterion is Keyword) {
              Keyword keyword = (Keyword) adGroupCriterion.criterion;
              Console.WriteLine("Keyword ad group criterion with criterion ID = '{0}', text =" +
                  " '{1}' and matchType = '{2} was found.", keyword.id, keyword.text,
                  keyword.matchType);
            } else if (adGroupCriterion.criterion is Placement) {
              Placement placement = (Placement) adGroupCriterion.criterion;
              Console.WriteLine("Placement ad group criterion criterion ID = '{0}' and url =" +
                  " '{1}' was found.", placement.id, placement.url);
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

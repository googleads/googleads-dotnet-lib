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
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v201003 {
  /// <summary>
  /// This code example deletes a campaign by setting the status to 'DELETED'.
  /// To get campaigns, run GetAllCampaigns.cs.
  /// </summary>
  class DeleteAdGroupCriterion : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes a campaign by setting the status to 'DELETED'. " +
            "To get campaigns, run GetAllCampaigns.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
        // Get the AdGroupCriterionService.
        AdGroupCriterionService adGroupCriterionService = (AdGroupCriterionService)user.GetService(
            AdWordsService.v201003.AdGroupCriterionService);

        long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));
        long criterionId = long.Parse(_T("INSERT_CRITERION_ID_HERE"));

        // Create base class criterion to avoid setting keyword and placement specific
        // fields.
        Criterion criterion = new Criterion();
        criterion.id = criterionId;
      criterion.idSpecified = true;

        // Create ad group criterion.
        BiddableAdGroupCriterion adGroupCriterion = new BiddableAdGroupCriterion();
        adGroupCriterion.adGroupId = adGroupId;
      adGroupCriterion.adGroupIdSpecified = true;
        adGroupCriterion.criterion = criterion;

        // Create operations.
        AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
        operation.operand = adGroupCriterion;
        operation.@operator = Operator.REMOVE;
      operation.operatorSpecified = true;

      try {
        // Delete ad group criteria.
        AdGroupCriterionReturnValue result = adGroupCriterionService.mutate(
            new AdGroupCriterionOperation[] {operation});

        // Display ad group criteria.
        if (result != null && result.value != null && result.value.Length > 0) {
         foreach (AdGroupCriterion temp in result.value) {
           Console.WriteLine("Ad group criterion with ad group id = \"{0}\", criterion id = " +
                "\"{1}\" and type = \"{2}\" was deleted.", temp.adGroupId, temp.criterion.id,
                temp.criterion.CriterionType);
         }
        } else {
         Console.WriteLine("No ad group criteria were deleted.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to delete ad group criteria. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

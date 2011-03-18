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
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v200909 {
  /// <summary>
  /// This code example deletes an ad group by setting the status to 'DELETED'.
  /// To get ad groups, run GetAllAdGroups.cs.
  ///
  /// Tags: AdGroupService.mutate
  /// </summary>
  class DeleteAdGroup : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes an ad group by setting the status to 'DELETED'. " +
            "To get ad groups, run GetAllAdGroups.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupService.
      AdGroupService adGroupService = (AdGroupService) user.GetService(
          AdWordsService.v200909.AdGroupService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));

      // Create ad group with DELETED status.
      AdGroup adGroup = new AdGroup();
      adGroup.id = adGroupId;
      adGroup.idSpecified = true;
      adGroup.statusSpecified = true;
      adGroup.status = AdGroupStatus.DELETED;

      // Create operations.
      AdGroupOperation operation = new AdGroupOperation();
      operation.operand = adGroup;
      operation.operatorSpecified = true;
      operation.@operator = Operator.SET;

      try {
        // Update ad group.
        AdGroupReturnValue result = adGroupService.mutate(new AdGroupOperation[] { operation });

        // Display ad groups.
        if (result != null && result.value != null && result.value.Length > 0) {
          foreach (AdGroup temp in result.value) {
            Console.WriteLine("Ad group with name = \"{0}\" and id = \"{1}\" was deleted.",
                temp.name, temp.id);
          }
        } else {
          Console.WriteLine("No ad groups were deleted.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to delete ad groups. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

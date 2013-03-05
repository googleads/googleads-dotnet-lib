// Copyright 2012, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201206;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201206 {
  /// <summary>
  /// This code example deletes an ad group by setting the status to 'DELETED'.
  /// To get ad groups, run GetAdGroups.cs.
  ///
  /// Tags: AdGroupService.mutate
  /// </summary>
  public class DeleteAdGroup : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      DeleteAdGroup codeExample = new DeleteAdGroup();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes an ad group by setting the status to 'DELETED'. " +
            "To get ad groups, run GetAdGroups.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to be deleted.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupService.
      AdGroupService adGroupService = (AdGroupService) user.GetService(
          AdWordsService.v201206.AdGroupService);

      // Create ad group with DELETED status.
      AdGroup adGroup = new AdGroup();
      adGroup.id = adGroupId;

      // When deleting an ad group, rename it to avoid name collisions with new
      // adgroups.
      adGroup.name = "Deleted AdGroup - " + ExampleUtilities.GetRandomString();
      adGroup.status = AdGroupStatus.DELETED;

      // Create the operation.
      AdGroupOperation operation = new AdGroupOperation();
      operation.operand = adGroup;
      operation.@operator = Operator.SET;

      try {
        // Delete the ad group.
        AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroup deletedAdGroup = retVal.value[0];
          Console.WriteLine("Ad group with id = \"{0}\" was renamed to \"{1}\" and deleted.",
              deletedAdGroup.id, deletedAdGroup.name);
        } else {
          Console.WriteLine("No ad groups were deleted.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to delete ad group.", ex);
      }
    }
  }
}

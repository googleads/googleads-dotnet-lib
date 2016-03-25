// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example illustrates how to update an ad group, setting its
  /// status to 'PAUSED'. To create an ad group, run AddAdGroup.cs.
  /// </summary>
  public class UpdateAdGroup : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      UpdateAdGroup codeExample = new UpdateAdGroup();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to update an ad group, setting its status to " +
            "'PAUSED'. To create an ad group, run AddAdGroup.cs";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to be updated.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201603.AdGroupService);

      // Create the ad group.
      AdGroup adGroup = new AdGroup();
      adGroup.status = AdGroupStatus.PAUSED;
      adGroup.id = adGroupId;

      // Create the operation.
      AdGroupOperation operation = new AdGroupOperation();
      operation.@operator = Operator.SET;
      operation.operand = adGroup;

      try {
        // Update the ad group.
        AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroup pausedAdGroup = retVal.value[0];
          Console.WriteLine("Ad group with id = '{0}' was successfully updated.",
              pausedAdGroup.id);
        } else {
          Console.WriteLine("No ad groups were updated.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to update ad group.", e);
      }
    }
  }
}

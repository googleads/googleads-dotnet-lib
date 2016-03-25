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
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example illustrates how to add ad group level mobile bid
  /// modifier override.
  /// </summary>
  public class AddAdGroupBidModifier : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddAdGroupBidModifier codeExample = new AddAdGroupBidModifier();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        double bidModifier = double.Parse("INSERT_ADGROUP_BID_MODIFIER_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId, bidModifier);
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
        return "This code example illustrates how to add ad group level mobile bid modifier " +
            "override.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the adgroup for which bid modifier is
    /// set.</param>
    /// <param name="bidModifier">The mobile bid modifier for adgroup</param>
    public void Run(AdWordsUser user, long adGroupId, double bidModifier) {
      // Get the AdGroupAdService.
      AdGroupBidModifierService adGroupBidModifierService =
          (AdGroupBidModifierService) user.GetService(
              AdWordsService.v201603.AdGroupBidModifierService);

      // Mobile criterion ID.
      long criterionId = 30001;


      // Create the adgroup bid modifier.
      AdGroupBidModifier adGroupBidModifier = new AdGroupBidModifier();
      adGroupBidModifier.bidModifier = bidModifier;
      adGroupBidModifier.adGroupId = adGroupId;

      Platform platform = new Platform();
      platform.id = criterionId;

      adGroupBidModifier.criterion = platform;

      AdGroupBidModifierOperation operation = new AdGroupBidModifierOperation();
      operation.@operator = Operator.ADD;
      operation.operand = adGroupBidModifier;


      try {
        // Add ad group level mobile bid modifier.
        AdGroupBidModifierReturnValue retval  = adGroupBidModifierService.mutate(
            new AdGroupBidModifierOperation[] {operation});

        // Display the results.
        if (retval != null && retval.value != null && retval.value.Length > 0) {
          AdGroupBidModifier newBidModifier = retval.value[0];
          Console.WriteLine("AdGroup ID {0}, Criterion ID {1} was updated with ad group level " +
              "modifier: {2}", newBidModifier.adGroupId, newBidModifier.criterion.id,
              newBidModifier.bidModifier);
        } else {
          Console.WriteLine("No bid modifiers were added to the adgroup.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to add bid modifiers to adgroup.", e);
      }
    }
  }
}

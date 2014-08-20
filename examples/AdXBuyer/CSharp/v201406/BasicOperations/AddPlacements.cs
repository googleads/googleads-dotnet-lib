// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201406;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {
  /// <summary>
  /// This code example adds placements to an ad group. To get ad groups, run
  /// GetAdGroups.cs.
  ///
  /// Tags: AdGroupCriterionService.mutate
  /// </summary>
  public class AddPlacements : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddPlacements codeExample = new AddPlacements();
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
        return "This code example adds placements to an ad group. To get ad groups, run " +
            "GetAdGroups.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to which placements are added.
    /// </param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201406.AdGroupCriterionService);

      // Create the placement.
      Placement placement1 = new Placement();
      placement1.url = "http://mars.google.com";

      // Create biddable ad group criterion.
      AdGroupCriterion placementCriterion1 = new BiddableAdGroupCriterion();
      placementCriterion1.adGroupId = adGroupId;
      placementCriterion1.criterion = placement1;

      // Create the placement.
      Placement placement2 = new Placement();
      placement2.url = "http://venus.google.com";

      // Create biddable ad group criterion.
      AdGroupCriterion placementCriterion2 = new BiddableAdGroupCriterion();
      placementCriterion2.adGroupId = adGroupId;
      placementCriterion2.criterion = placement2;

      // Create the operations.
      AdGroupCriterionOperation placementOperation1 = new AdGroupCriterionOperation();
      placementOperation1.@operator = Operator.ADD;
      placementOperation1.operand = placementCriterion1;

      AdGroupCriterionOperation placementOperation2 = new AdGroupCriterionOperation();
      placementOperation2.@operator = Operator.ADD;
      placementOperation2.operand = placementCriterion2;

      try {
        // Create the placements.
        AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(
            new AdGroupCriterionOperation[] {placementOperation1, placementOperation2});

        // Display the results.
        if (retVal != null && retVal.value != null) {
          foreach (AdGroupCriterion adGroupCriterion in retVal.value) {
            // If you are adding multiple type of criteria, then you may need to
            // check for
            //
            // if (adGroupCriterion is Placement) { ... }
            //
            // to identify the criterion type.
            Console.WriteLine("Placement with ad group id = '{0}, placement id = '{1}, url = " +
                "'{2}' was created.", adGroupCriterion.adGroupId,
                adGroupCriterion.criterion.id, (adGroupCriterion.criterion as Placement).url);
          }
        } else {
          Console.WriteLine("No placements were added.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create placements.", ex);
      }
    }
  }
}

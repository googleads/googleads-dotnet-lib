// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
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
      ExampleBase codeExample = new AddPlacements();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID"};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201109.AdGroupCriterionService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);

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
            writer.WriteLine("Placement with ad group id = '{0}, placement id = '{1}, url = " +
                "'{2}' was created.", adGroupCriterion.adGroupId,
                adGroupCriterion.criterion.id, (adGroupCriterion.criterion as Placement).url);
          }
        } else {
          writer.WriteLine("No placements were added.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to create placements. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

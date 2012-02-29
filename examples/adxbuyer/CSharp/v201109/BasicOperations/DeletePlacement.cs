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
  /// This code example deletes a placement using the 'REMOVE' operator. To get
  /// placements, run GetPlacements.cs.
  ///
  /// Tags: AdGroupCriterionService.mutate
  /// </summary>
  class DeletePlacement : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new DeletePlacement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes a placement using the 'REMOVE' operator. To get " +
            "placements, run GetPlacements.cs.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID", "PLACEMENT_ID"};
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
      AdGroupCriterionService adGroupCriterionService = (AdGroupCriterionService)user.GetService(
          AdWordsService.v201109.AdGroupCriterionService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);
      long keywordId = long.Parse(parameters["PLACEMENT_ID"]);

      // Create base class criterion to avoid setting placement-specific fields.
      Criterion criterion = new Criterion();
      criterion.id = keywordId;

      // Create the ad group criterion.
      BiddableAdGroupCriterion adGroupCriterion = new BiddableAdGroupCriterion();
      adGroupCriterion.adGroupId = adGroupId;
      adGroupCriterion.criterion = criterion;

      // Create the operation.
      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.operand = adGroupCriterion;
      operation.@operator = Operator.REMOVE;

      try {
        // Delete the placement.
        AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(
            new AdGroupCriterionOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroupCriterion deletedPlacement = retVal.value[0];
          writer.WriteLine("Placement with ad group id = \"{0}\" and id = \"{1}\" was deleted.",
              deletedPlacement.adGroupId, deletedPlacement.criterion.id);
        } else {
          writer.WriteLine("No placement was deleted.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to delete placement. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

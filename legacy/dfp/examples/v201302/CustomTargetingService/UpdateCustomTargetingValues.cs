// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201302;
using Google.Api.Ads.Dfp.v201302;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201302 {
  /// <summary>
  /// This code example updates the display name of each custom targeting value
  /// up to the first 500. To determine which custom targeting keys exist, run
  /// GetAllCustomTargetingKeysAndValues.cs.
  ///
  /// Tags: CustomTargetingService.getCustomTargetingValuesByStatement
  /// Tags: CustomTargetingService.updateCustomTargetingValues
  /// </summary>
  class UpdateCustomTargetingValues : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the display name of each custom targeting value up to " +
            "the first 500. To determine which custom targeting keys exist, run " +
            "GetAllCustomTargetingKeysAndValues.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateCustomTargetingValues();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CreativeService.
      CustomTargetingService customTargetingService =
          (CustomTargetingService) user.GetService(DfpService.v201302.CustomTargetingService);

      // Set the ID of the predefined custom targeting key to get custom
      // targeting values for.
      long customTargetingKeyId = long.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE"));

      // Create a statement to only select predefined custom targeting values
      // for a given key.
      Statement filterStatement =
          new StatementBuilder("WHERE customTargetingKeyId = :customTargetingKeyId LIMIT 500")
              .AddValue("customTargetingKeyId", customTargetingKeyId).ToStatement();

      try {
        // Get custom targeting values by statement.
        CustomTargetingValuePage page =
            customTargetingService.getCustomTargetingValuesByStatement(filterStatement);

        if (page.results != null) {
          CustomTargetingValue[] customTargetingValues = page.results;

          // Update each local custom targeting value object by changing its
          // display name.
          foreach (CustomTargetingValue customTargetingValue in customTargetingValues) {
            if (customTargetingValue.displayName == null) {
              customTargetingValue.displayName = customTargetingValue.displayName;
            }
            customTargetingValue.displayName = customTargetingValue.displayName + " (Deprecated)";
          }

          // Update the custom targeting values on the server.
          customTargetingValues =
              customTargetingService.updateCustomTargetingValues(customTargetingValues);

          if (customTargetingValues != null) {
            foreach (CustomTargetingValue customTargetingValue in customTargetingValues) {
              Console.WriteLine("Custom targeting value with ID \"{0}\", name \"{1}\", and " +
                  "display name \"{2}\" was updated.", customTargetingValue.id,
                  customTargetingValue.name, customTargetingValue.displayName);
            }
          } else {
            Console.WriteLine("No custom targeting values updated.");
          }
        } else {
          Console.WriteLine("No custom targeting values found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update display names of custom targeting values. Exception " +
            "says \"{0}\"", ex.Message);
      }
    }
  }
}

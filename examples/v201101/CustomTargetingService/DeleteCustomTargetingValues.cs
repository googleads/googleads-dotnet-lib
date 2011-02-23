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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201101;
using Google.Api.Ads.Dfp.v201101;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201101 {
  /// <summary>
  /// This code example deletes custom targeting values for a given custom
  /// targeting key. To determine which custom targeting keys and values exist,
  /// run GetAllCustomTargetingKeysAndValues.cs.
  /// </summary>
  class DeleteCustomTargetingValues : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes custom targeting values for a given custom targeting " +
          "key. To determine which custom targeting keys and values exist, run " +
          "GetAllCustomTargetingKeysAndValues.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeleteCustomTargetingValues();
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
          (CustomTargetingService) user.GetService(DfpService.v201101.CustomTargetingService);

      // Set ID of the custom targeting key to delete values from.
      long customTargetingKeyId = long.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE"));

      // Create statement to only select custom values by the given custom
      // targeting key ID.
      String statementText = "WHERE customTargetingKeyId = :customTargetingKeyId";
      StatementBuilder statementBuilder = new StatementBuilder("").AddValue(
          "customTargetingKeyId", customTargetingKeyId);

      // Set defaults for page and offset.
      CustomTargetingValuePage page = new CustomTargetingValuePage();
      int offset = 0;
      List<string> customTargetingValueIds = new List<string>();

      try {
        do {
          // Create a statement to page through custom targeting values.
          statementBuilder.Query = statementText + " LIMIT 500 OFFSET " + offset.ToString();

          // Get custom targeting values by statement.
          page = customTargetingService.getCustomTargetingValuesByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            foreach (CustomTargetingValue customTargetingValue in page.results) {
              // We store the ids as strings, so that we can do a string.Join()
              // later in the code.
              customTargetingValueIds.Add(customTargetingValue.id.ToString());
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of custom targeting values to be deleted: {0}",
            customTargetingValueIds.Count);

        if (customTargetingValueIds.Count > 0) {
          // Modify statement for action.

          statementBuilder.Query = string.Format("WHERE customTargetingKeyId = " +
              ":customTargetingKeyId AND id IN ({0})",
              string.Join(",", customTargetingValueIds.ToArray()));

          // Create action.
          DeleteCustomTargetingValueAction action = new DeleteCustomTargetingValueAction();

          // Perform action.
          UpdateResult result = customTargetingService.performCustomTargetingValueAction(
              action, statementBuilder.ToStatement());

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of custom targeting values deleted: {0}",
                result.numChanges);
          } else {
            Console.WriteLine("No custom targeting values were deleted.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to delete custom targeting values for given custom targeting " +
          "key. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

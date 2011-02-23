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
  /// This code example deletes custom targeting key by its name. To determine
  /// which custom targeting keys exist, run
  /// GetAllCustomTargetingKeysAndValues.cs.
  /// </summary>
  class DeleteCustomTargetingKeys : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes custom targeting key by its name. To determine which " +
            "custom targeting keys exist, run GetAllCustomTargetingKeysAndValues.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeleteCustomTargetingKeys();
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

      // Set the name of the custom targeting key to delete.
      string customTargetingKeyName = _T("INSERT_CUSTOM_TARGETING_KEY_NAME_HERE");

      // Create statement to only select custom targeting key by the given name.
      String statementText = "WHERE name = :name";
      StatementBuilder statementBuilder = new StatementBuilder("")
          .AddValue("name", customTargetingKeyName);

      // Set defaults for page and offset.
      CustomTargetingKeyPage page = new CustomTargetingKeyPage();
      int offset = 0;
      List<string> customTargetingKeyIds = new List<string>();

      try {
        do {
          // Create a statement to page through custom targeting keys.
          statementBuilder.Query = statementText + " LIMIT 500 OFFSET " + offset.ToString();

          // Get custom targeting keys by statement.
          page = customTargetingService.getCustomTargetingKeysByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            foreach (CustomTargetingKey customTargetingKey in page.results) {
              // We store the ids as strings, so that we can do a string.Join()
              // later in the code.
              customTargetingKeyIds.Add(customTargetingKey.id.ToString());
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of custom targeting keys to be deleted: {0}",
            customTargetingKeyIds.Count);

        if (customTargetingKeyIds.Count > 0) {
          // Modify statement for action.
          statementBuilder.Query = string.Format("WHERE id IN ({0})", string.Join(",",
              customTargetingKeyIds.ToArray()));

          // Create action.
          DeleteCustomTargetingKeyAction action = new DeleteCustomTargetingKeyAction();

          // Perform action.
          UpdateResult result = customTargetingService.performCustomTargetingKeyAction(
              action, statementBuilder.ToStatement());

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of custom targeting keys deleted: " + result.numChanges);
          } else {
            Console.WriteLine("No custom targeting keys were deleted.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to delete custom targeting keys by name. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

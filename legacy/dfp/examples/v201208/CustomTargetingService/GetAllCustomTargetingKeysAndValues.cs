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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201208;
using Google.Api.Ads.Dfp.v201208;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201208 {
  /// <summary>
  /// This code example gets all custom targeting keys and the values. To create
  /// custom targeting keys and values, run
  /// CreateCustomTargetingKeysAndValues.cs.
  ///
  /// Tags: CustomTargetingService.getCustomTargetingKeysByStatement
  /// Tags: CustomTargetingService.getCustomTargetingValuesByStatement
  /// </summary>
  class GetAllCustomTargetingKeysAndValues : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all custom targeting keys and the values. To create " +
            "custom targeting keys and values, run CreateCustomTargetingKeysAndValues.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllCustomTargetingKeysAndValues();
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
          (CustomTargetingService) user.GetService(DfpService.v201208.CustomTargetingService);

      // Sets defaults for page and filter.
      CustomTargetingKeyPage keyPage = new CustomTargetingKeyPage();
      Statement keyFilterStatement = new Statement();
      int keyOffset = 0;

      try {
        do {
          // Create a statement to get all custom targeting keys.
          keyFilterStatement.query = "LIMIT 500 OFFSET " + keyOffset;

          // Get custom targeting keys by statement.
          keyPage = customTargetingService.getCustomTargetingKeysByStatement(keyFilterStatement);

          if (keyPage.results != null) {
            int i = keyPage.startIndex;
            foreach (CustomTargetingKey key in keyPage.results) {
              Console.WriteLine("{0}) Custom targeting key with ID \"{1}\", name \"{2}\", " +
                  "display name \"{3}\", and type \"{4}\" was found.", i, key.id, key.name,
                  key.displayName, key.type);


              // Sets defaults for page and filter.
              CustomTargetingValuePage valuePage = new CustomTargetingValuePage();
              Statement valueFilterStatement = new Statement();
              int valueOffset = 0;

              do {
                // Create a statement to get all custom targeting values for a
                // custom targeting key (required) by its ID.
                valueFilterStatement.query = string.Format("WHERE customTargetingKeyId = {0} " +
                    "LIMIT 500 OFFSET {1}", key.id, valueOffset);

                // Get custom targeting values by statement.
                valuePage = customTargetingService.getCustomTargetingValuesByStatement(
                    valueFilterStatement);

                if (valuePage.results != null) {
                  int j = valuePage.startIndex;
                  foreach (CustomTargetingValue value in valuePage.results) {
                    Console.WriteLine("\t{0}) Custom targeting value with ID \"{1}\", name " +
                        "\"{2}\", and display name \"{3}\" was found.", j, value.id, value.name,
                        value.displayName);
                    j++;
                  }
                }
                valueOffset += 500;
              } while (valuePage.results != null && valuePage.results.Length == 500);
              i++;
            }
          }
          keyOffset += 500;
        } while (keyPage.results != null && keyPage.results.Length == 500);
        Console.WriteLine("Number of results found: {0}", keyPage.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get custom targeting keys and the values. Exception " +
            "says \"{0}\"", ex.Message);
      }
    }
  }
}

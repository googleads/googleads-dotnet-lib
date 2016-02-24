// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all custom targeting keys and the values. To create
  /// custom targeting keys and values, run
  /// CreateCustomTargetingKeysAndValues.cs.
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
      // Get the CustomTargetingService.
      CustomTargetingService customTargetingService =
          (CustomTargetingService) user.GetService(DfpService.v201602.CustomTargetingService);

      // Create a statement to get all custom targeting keys.
      StatementBuilder keyStatementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Set default for page.
      CustomTargetingKeyPage keyPage = new CustomTargetingKeyPage();

      try {
        do {
          // Get custom targeting keys by statement.
          keyPage = customTargetingService.getCustomTargetingKeysByStatement(
              keyStatementBuilder.ToStatement());

          if (keyPage.results != null) {
            int i = keyPage.startIndex;
            foreach (CustomTargetingKey key in keyPage.results) {
              Console.WriteLine("{0}) Custom targeting key with ID \"{1}\", name \"{2}\", " +
                  "display name \"{3}\", and type \"{4}\" was found.", i, key.id, key.name,
                  key.displayName, key.type);

              // Create a statement to get all custom targeting values for a
              // custom targeting key (required) by its ID.
              StatementBuilder valueStatementBuilder = new StatementBuilder()
                  .Where("customTargetingKeyId = :customTargetingKeyId")
                  .OrderBy("id ASC")
                  .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
                  .AddValue("customTargetingKeyId", key.id);

              // Set default for page.
              CustomTargetingValuePage valuePage = new CustomTargetingValuePage();

              do {
                // Get custom targeting values by statement.
                valuePage = customTargetingService.getCustomTargetingValuesByStatement(
                    valueStatementBuilder.ToStatement());

                if (valuePage.results != null) {
                  int j = valuePage.startIndex;
                  foreach (CustomTargetingValue value in valuePage.results) {
                    Console.WriteLine("\t{0}) Custom targeting value with ID \"{1}\", name " +
                        "\"{2}\", and display name \"{3}\" was found.", j, value.id, value.name,
                        value.displayName);
                    j++;
                  }
                }
                valueStatementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
              } while (valueStatementBuilder.GetOffset() < valuePage.totalResultSetSize);
              i++;
            }
          }
          keyStatementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (keyStatementBuilder.GetOffset() < keyPage.totalResultSetSize);
        Console.WriteLine("Number of results found: {0}", keyPage.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get custom targeting keys and the values. Exception " +
            "says \"{0}\"", e.Message);
      }
    }
  }
}

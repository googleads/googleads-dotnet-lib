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
  /// This code example gets custom targeting values for the given predefined
  /// custom targeting key. To create custom targeting values, run
  /// CreateCustomTargetingKeysAndValues.cs. To determine which custom
  /// targeting keys exist, run GetAllCustomTargetingKeysAndValues.cs.
  /// </summary>
  class GetCustomTargetingValuesByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets custom targeting values for the given predefined custom " +
            "targeting key. To create custom targeting values, run " + 
            "CreateCustomTargetingKeysAndValues.cs. To determine which custom targeting keys " + 
            "exist, run GetAllCustomTargetingKeysAndValues.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCustomTargetingValuesByStatement();
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

      // Set the ID of the custom targeting key to get custom targeting values
      // for.
      long customTargetingKeyId = long.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE"));

      // Create a statement to only select custom targeting values for a given
      // key.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("customTargetingKeyId = :customTargetingKeyId")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
         .AddValue("customTargetingKeyId", customTargetingKeyId);

      // Set default for page.
      CustomTargetingValuePage page = new CustomTargetingValuePage();

      try {
        do {
          // Get custom targeting values by statement.
          page = customTargetingService.getCustomTargetingValuesByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            int i = page.startIndex;
            foreach (CustomTargetingValue customTargetingValue in page.results) {
              Console.WriteLine("{0}) Custom targeting value with ID \"{1}\", name \"{2}\", and " +
                  "display name \"{3}\" was found.", i, customTargetingValue.id,
                  customTargetingValue.name, customTargetingValue.displayName);
              i++;
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);
        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get custom targeting values. Exception " +
            "says \"{0}\"", e.Message);
      }
    }
  }
}

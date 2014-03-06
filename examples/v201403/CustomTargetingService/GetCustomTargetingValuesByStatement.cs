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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201403;
using Google.Api.Ads.Dfp.v201403;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201403 {
  /// <summary>
  /// This code example gets custom targeting values for the given predefined
  /// custom targeting key. The statement retrieves up to the maximum page size
  /// limit of 500. To create custom targeting values, run
  /// CreateCustomTargetingKeysAndValues.cs. To determine which custom
  /// targeting keys exist, run GetAllCustomTargetingKeysAndValues.cs.
  ///
  /// Tags: CustomTargetingService.getCustomTargetingValuesByStatement
  /// </summary>
  class GetCustomTargetingValuesByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets custom targeting values for the given predefined custom " +
            "targeting key. The statement retrieves up to the maximum page size limit of 500. " +
            "To create custom targeting values, run CreateCustomTargetingKeysAndValues.cs. To " +
            "determine which custom targeting keys exist, run " +
            "GetAllCustomTargetingKeysAndValues.cs.";
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
      // Get the CreativeService.
      CustomTargetingService customTargetingService =
          (CustomTargetingService) user.GetService(DfpService.v201403.CustomTargetingService);

      // Set the ID of the custom targeting key to get custom targeting values
      // for.
      long customTargetingKeyId = long.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE"));

      // Create a statement to only select custom targeting values for a given
      // key.
      Statement filterStatement =
          new StatementBuilder("WHERE customTargetingKeyId = :customTargetingKeyId LIMIT 500")
              .AddValue("customTargetingKeyId", customTargetingKeyId).ToStatement();

      try {
        // Get custom targeting values by statement.
        CustomTargetingValuePage page =
            customTargetingService.getCustomTargetingValuesByStatement(filterStatement);

        if (page.results != null) {
          int i = page.startIndex;
          foreach (CustomTargetingValue customTargetingValue in page.results) {
            Console.WriteLine("{0}) Custom targeting value with ID \"{1}\", name \"{2}\", and " +
                "display name \"{3}\" was found.", i, customTargetingValue.id,
                customTargetingValue.name, customTargetingValue.displayName);
            i++;
          }
        }

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get custom targeting values. Exception " +
            "says \"{0}\"", ex.Message);
      }
    }
  }
}

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
using Google.Api.Ads.Dfp.Util.v201107;
using Google.Api.Ads.Dfp.v201107;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201107 {
  /// <summary>
  /// This code example gets all predefined custom targeting keys. The statement
  /// retrieves up to the maximum page size limit of 500. To create custom
  /// targeting keys, run CreateCustomTargetingKeysAndValues.cs.
  ///
  /// Tags: CustomTargetingService.getCustomTargetingKeysByStatement
  /// </summary>
  class GetCustomTargetingKeysByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all predefined custom targeting keys. The statement " +
            "retrieves up to the maximum page size limit of 500. To create custom " +
            "targeting keys, run CreateCustomTargetingKeysAndValues.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCustomTargetingKeysByStatement();
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
          (CustomTargetingService) user.GetService(DfpService.v201107.CustomTargetingService);

      try {
        // Create a statement to only select predefined custom targeting keys.
        Statement filterStatement = new StatementBuilder("WHERE type = :type LIMIT 500").AddValue(
            "type", CustomTargetingKeyType.PREDEFINED.ToString()).ToStatement();

        // Get custom targeting keys by statement.
        CustomTargetingKeyPage page =
            customTargetingService.getCustomTargetingKeysByStatement(filterStatement);

        if (page.results != null) {
          int i = page.startIndex;
          foreach (CustomTargetingKey customTargetingKey in page.results) {
            Console.WriteLine("{0}) Custom targeting key with ID \"{1}\", name \"{2}\", and " +
                "display name \"{3}\" was found.", i, customTargetingKey.id,
                customTargetingKey.name, customTargetingKey.displayName);
            i++;
          }
        }
        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get predefined custom targeting keys. Exception " +
            "says \"{0}\"", ex.Message);
      }
    }
  }
}

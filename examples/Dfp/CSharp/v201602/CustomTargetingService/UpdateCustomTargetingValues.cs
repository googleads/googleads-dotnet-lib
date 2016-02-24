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
  /// This code example updates the display name of custom targeting values.  To determine 
  /// which custom targeting keys exist, run GetAllCustomTargetingKeysAndValues.cs.
  /// </summary>
  class UpdateCustomTargetingValues : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the display name of custom targeting values. To " +
            "determine which custom targeting keys exist, run " +
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
          (CustomTargetingService) user.GetService(DfpService.v201602.CustomTargetingService);

      // Set the ID of the predefined custom targeting value to update.
      long customTargetingValueId = long.Parse(_T("INSERT_CUSTOM_TARGETING_VALUE_ID_HERE"));

      // Create a statement to only select predefined custom targeting values
      // for a given key.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :customTargetingValueId")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("customTargetingValueId", customTargetingValueId);

      try {
        // Get custom targeting values by statement.
        CustomTargetingValuePage page =
            customTargetingService.getCustomTargetingValuesByStatement(
            statementBuilder.ToStatement());

        CustomTargetingValue customTargetingValue = page.results[0];

        // Update the local custom targeting value object by changing its display name.
        if (customTargetingValue.displayName == null) {
          customTargetingValue.displayName = customTargetingValue.displayName;
        }
        customTargetingValue.displayName = customTargetingValue.displayName + " (Deprecated)";

        // Update the custom targeting values on the server.
        CustomTargetingValue[] customTargetingValues =
            customTargetingService.updateCustomTargetingValues(
            new CustomTargetingValue[] {customTargetingValue});

        foreach (CustomTargetingValue updatedCustomTargetingValue in customTargetingValues) {
          Console.WriteLine("Custom targeting value with ID \"{0}\", name \"{1}\", and " +
              "display name \"{2}\" was updated.", updatedCustomTargetingValue.id,
              updatedCustomTargetingValue.name, updatedCustomTargetingValue.displayName);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update display names of custom targeting values. Exception " +
            "says \"{0}\"", e.Message);
      }
    }
  }
}

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
  /// This code example updates the display name of each custom targeting key up
  /// to the first 500. To determine which custom targeting keys exist, run
  /// GetAllCustomTargetingKeysAndValues.cs.
  /// </summary>
  class UpdateCustomTargetingKeys : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the display name of custom targeting keys. To " +
            "determine which custom targeting keys exist, run " +
            "GetAllCustomTargetingKeysAndValues.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateCustomTargetingKeys();
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

      // Set the ID of the custom targeting key to update.
      int customTargetingKeyId = int.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE"));

      // Create a statement to get the custom targeting key.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", customTargetingKeyId);

      try {
        // Get custom targeting keys by statement.
        CustomTargetingKeyPage page =
            customTargetingService.getCustomTargetingKeysByStatement(
            statementBuilder.ToStatement());

        CustomTargetingKey customTargetingKey = page.results[0];

        // Update each local custom targeting key object by changing its display name.
        if (customTargetingKey.displayName == null) {
          customTargetingKey.displayName = customTargetingKey.name;
        }
        customTargetingKey.displayName = customTargetingKey.displayName + " (Deprecated)";

        // Update the custom targeting keys on the server.
        CustomTargetingKey[] customTargetingKeys = customTargetingService.updateCustomTargetingKeys(
            new CustomTargetingKey[] {customTargetingKey});

        foreach (CustomTargetingKey updatedCustomTargetingKey in customTargetingKeys) {
          Console.WriteLine("Custom targeting key with ID \"{0}\", name \"{1}\", and " +
              "display name \"{2}\" was updated.", updatedCustomTargetingKey.id,
              updatedCustomTargetingKey.name, updatedCustomTargetingKey.displayName);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update display name of custom targeting keys. Exception " +
            "says \"{0}\"", e.Message);
      }
    }
  }
}

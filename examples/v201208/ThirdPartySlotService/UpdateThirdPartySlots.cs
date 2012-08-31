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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201208;

using System;
using Google.Api.Ads.Dfp.Util.v201208;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201208 {
  /// <summary>
  /// This code example updates the first third party slot description.
  ///
  /// Tags: ThirdPartySlotsService.getThirdPartySlotsByStatement
  /// Tags:, ThirdPartySlotsService.updateThirdPartySlots
  /// </summary>
  class UpdateThirdPartySlots : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the first third party slot description.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateThirdPartySlots();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ThirdpartySlotService.
      ThirdPartySlotService thirdPartySlotService =
          (ThirdPartySlotService) user.GetService(DfpService.v201208.ThirdPartySlotService);

      // Create a statement to get one active third party slot.
      Statement filterStatement = new StatementBuilder("WHERE status = :status LIMIT 1").AddValue(
          "status", ThirdPartySlotStatus.ACTIVE.ToString()).ToStatement();

      try {
        // Get third party slot by statement.
        ThirdPartySlotPage page =
            thirdPartySlotService.getThirdPartySlotsByStatement(filterStatement);

        if (page.results != null) {
          // Update each third party slot by changing its description.
          foreach (ThirdPartySlot slot in page.results) {
            slot.description = "Updated description";

            // Update the third party slot on the server.
            ThirdPartySlot updatedSlot = thirdPartySlotService.updateThirdPartySlot(slot);

            // Display results.
            if (updatedSlot != null) {
              Console.WriteLine("A third party slot with ID \"{0}\" and description \"{1}\" " +
                  "was updated.", updatedSlot.id, updatedSlot.description);
            } else {
              Console.WriteLine("No third party slot was updated.");
            }
          }
        } else {
          Console.WriteLine("No third party slots found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update third party slots. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

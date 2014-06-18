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
using Google.Api.Ads.Dfp.v201211;

using System;
using Google.Api.Ads.Dfp.Util.v201211;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201211 {
  /// <summary>
  /// This code example creates a new third party slot. To determine which
  /// third party slots exist, run GetAllThirdPartySlots.cs.
  ///
  /// Tags: ThirdPartySlotsService.createThirdPartySlot
  /// </summary>
  class CreateThirdPartySlot : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a new third party slot. To determine which third " +
            "party slots exist, run GetAllThirdPartySlots.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateThirdPartySlot();
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
          (ThirdPartySlotService) user.GetService(DfpService.v201211.ThirdPartySlotService);

      // Set the company ID to associate with this third party slot.
      long companyId = long.Parse(_T("INSERT_COMPANY_ID_HERE"));

      // Set the external unique ID to associate with this third party slot.
      string externalUniqueId = _T("INSERT_EXTERNAL_UNIQUE_ID_HERE");

      // Set the external unique name to associate with this third party slot.
      string externalUniqueName = _T("INSERT_EXTERNAL_UNIQUE_NAME_HERE");

      // Set the creative IDs to associate with this third party slot.
      long[] creativeIds = new long[] {long.Parse(_T("INSERT_CREATIVE_ID_HERE"))};

      // Create the ThirdPartySlot object.
      ThirdPartySlot thirdPartySlot = new ThirdPartySlot();
      thirdPartySlot.companyId = companyId;
      thirdPartySlot.description = "Third party slot description.";
      thirdPartySlot.externalUniqueId = externalUniqueId;
      thirdPartySlot.externalUniqueName = externalUniqueName;

      // Set the creatives that the third party slots will represent.
      thirdPartySlot.creativeIds = creativeIds;

      try {
        // Create the third party slot.
        thirdPartySlot = thirdPartySlotService.createThirdPartySlot(thirdPartySlot);

        if (thirdPartySlot != null) {
          Console.WriteLine("A third party slot with ID \"{0}\" and named \"{1}\" was created.",
              thirdPartySlot.id, thirdPartySlot.externalUniqueName);
        } else {
          Console.WriteLine("No third party slot created.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create third party slots. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

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

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201208 {
  /// <summary>
  /// This code example archives all active third party slots for a company. To
  /// determine which third party slots exist, run GetAllThirdPartySlots.cs.
  ///
  /// Tags: ThirdPartSlotsService.getThirdPartySlotsByStatement
  /// Tags: ThirdPartSlotsService.performThirdPartySlotsAction
  /// </summary>
  class ArchiveThirdPartySlot : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example archives all active third party slots for a company. To " +
            "determine which third party slots exist, run GetAllThirdPartySlots.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ArchiveThirdPartySlot();
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

      //Set the company that the third party slots to archive belong to.
      long companyId = long.Parse(_T("INSERT_COMPANY_ID_HERE"));

      // Create a statement to only select active third party slots.
      String statementText = "WHERE status = :status AND companyId = :companyId LIMIT 500";
      Statement filterStatement = new StatementBuilder("").AddValue("status",
          ThirdPartySlotStatus.ACTIVE.ToString()).AddValue("companyId", companyId).ToStatement();

      // Set defaults for page and offset.
      ThirdPartySlotPage page = new ThirdPartySlotPage();
      int offset = 0;
      List<string> thirdPartySlotIds = new List<string>();

      try {
        do {
          // Create a statement to page through active third party slots.
          filterStatement.query = statementText + " OFFSET " + offset;

          // Get third party slots by statement.
          page = thirdPartySlotService.getThirdPartySlotsByStatement(filterStatement);

          if (page.results != null) {
            foreach (ThirdPartySlot thirdPartySlot in page.results) {
              Console.WriteLine("Third party slot with ID \"{0}\" will be archived.",
                  thirdPartySlot.id);
              thirdPartySlotIds.Add(thirdPartySlot.id.ToString());
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of third party slots to be archived: " + thirdPartySlotIds.Count);

        if (thirdPartySlotIds.Count > 0) {
          // Modify statement for action.
          filterStatement.query = "WHERE id IN (" + string.Join(",", thirdPartySlotIds.ToArray()) +
              ")";

          // Create action.
          ArchiveThirdPartySlots action = new ArchiveThirdPartySlots();

          // Perform action.
          UpdateResult result = thirdPartySlotService.performThirdPartySlotAction(action,
              filterStatement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of third party slots archived: " + result.numChanges);
          } else {
            Console.WriteLine("No third party slots were archived.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to archive slots. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

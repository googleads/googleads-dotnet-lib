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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example updates a proposal line item's notes. To determine which 
  /// proposal line items exist, run GetAllProposalLineItems.cs.
  /// </summary>
  class UpdateProposalLineItems : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a proposal line item's notes. To determine which " +
          "proposal line items exist, run GetAllProposalLineItems.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateProposalLineItems();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ProposalLineItemService.
      ProposalLineItemService proposalLineItemService =
          (ProposalLineItemService) user.GetService(DfpService.v201602.ProposalLineItemService);

      // Set the ID of the proposal line item.
      long proposalLineItemId = long.Parse(_T("INSERT_PROPOSAL_LINE_ITEM_ID_HERE"));

      // Create a statement to get the proposal line item.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :proposalLineItemId")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("proposalLineItemId", proposalLineItemId);

      try {
        // Get proposal line items by statement.
        ProposalLineItemPage page = proposalLineItemService
            .getProposalLineItemsByStatement(statementBuilder.ToStatement());

        ProposalLineItem proposalLineItem = page.results[0];

        // Update proposal line item notes.
        proposalLineItem.notes = "Proposal line item ready for submission";

        // Update the proposal line item on the server.
        ProposalLineItem[] proposalLineItems = proposalLineItemService
            .updateProposalLineItems(new ProposalLineItem[] {proposalLineItem});

        if (proposalLineItems != null) {
          foreach (ProposalLineItem updatedProposalLineItem in proposalLineItems) {
            Console.WriteLine("A proposal line item with ID = '{0}' and name '{1}' was updated.",
                updatedProposalLineItem.id, updatedProposalLineItem.name);
          }
        } else {
          Console.WriteLine("No proposal line items updated.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update proposal line items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}

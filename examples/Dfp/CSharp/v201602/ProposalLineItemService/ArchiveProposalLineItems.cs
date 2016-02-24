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
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example archives a proposal line item. To determine which proposal line items
  /// exist, run GetAllProposalLineItem.cs.
  /// </summary>
  class ArchiveProposalLineItem : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example archives a proposal line item. To determine which proposal " +
            "line items exist, run GetAllProposalLineItem.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ArchiveProposalLineItem();
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

      // Set the ID of the proposal line item to archive.
      long proposalLineItemId = long.Parse(_T("INSERT_PROPOSAL_LINE_ITEM_ID_HERE"));

      // Create statement to select a proposal line item by ID.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("id", proposalLineItemId);

      // Set default for page.
      ProposalLineItemPage page = new ProposalLineItemPage();
      List<string> proposalLineItemIds = new List<string>();

      try {
        do {
          // Get proposal line items by statement.
          page = proposalLineItemService.getProposalLineItemsByStatement(
              statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (ProposalLineItem proposalLineItem in page.results) {
              Console.WriteLine("{0}) Proposal line item with ID ='{1}' will be archived.",
                  i++, proposalLineItem.id);
              proposalLineItemIds.Add(proposalLineItem.id.ToString());
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of proposal line items to be archived: {0}",
            proposalLineItemIds.Count);

        if (proposalLineItemIds.Count > 0) {
          // Modify statement.
          statementBuilder.RemoveLimitAndOffset();

          // Create action.
          Google.Api.Ads.Dfp.v201602.ArchiveProposalLineItems action =
              new Google.Api.Ads.Dfp.v201602.ArchiveProposalLineItems();

          // Perform action.
          UpdateResult result = proposalLineItemService.performProposalLineItemAction(action,
              statementBuilder.ToStatement());

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of proposal line items archived: {0}", result.numChanges);
          } else {
            Console.WriteLine("No proposal line items were archived.");
          }
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to archive proposal line items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}

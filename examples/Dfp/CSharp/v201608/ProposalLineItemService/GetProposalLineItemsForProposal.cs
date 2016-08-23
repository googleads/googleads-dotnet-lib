// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201608;
using Google.Api.Ads.Dfp.v201608;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201608 {
  /// <summary>
  /// This example gets all proposal line items belonging to a specific proposal.
  /// </summary>
  public class GetProposalLineItemsForProposal : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all proposal line items belonging to a specific proposal.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main() {
      GetProposalLineItemsForProposal codeExample = new GetProposalLineItemsForProposal();
      Console.WriteLine(codeExample.Description);

      long proposalId = long.Parse(_T("INSERT_PROPOSAL_ID_HERE"));
      codeExample.Run(new DfpUser(), proposalId);
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public void Run(DfpUser user, long proposalId) {
      ProposalLineItemService proposalLineItemService =
          (ProposalLineItemService) user.GetService(DfpService.v201608.ProposalLineItemService);

      // Create a statement to select proposal line items.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("proposalId = :proposalId")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("proposalId", proposalId);

      // Retrieve a small amount of proposal line items at a time, paging through
      // until all proposal line items have been retrieved.
      ProposalLineItemPage page = new ProposalLineItemPage();
      try {
        do {
          page = proposalLineItemService.getProposalLineItemsByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each proposal line item.
            int i = page.startIndex;
            foreach (ProposalLineItem proposalLineItem in page.results) {
              Console.WriteLine("{0}) Proposal line item with ID \"{1}\" "
                  + "and name \"{2}\" was found.",
                  i++,
                  proposalLineItem.id,
                  proposalLineItem.name);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get proposal line items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}

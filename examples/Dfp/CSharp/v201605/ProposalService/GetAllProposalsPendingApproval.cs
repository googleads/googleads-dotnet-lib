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
using Google.Api.Ads.Dfp.Util.v201605;
using Google.Api.Ads.Dfp.v201605;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201605 {
  /// <summary>
  /// This code example gets all proposals that are pending approval. To create proposals, run
  /// CreateProposals.cs.
  /// </summary>
  public class GetAllProposalsPendingApproval : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all proposals that are pending approval. To create " +
            "proposals, run CreateProposals.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      GetAllProposalsPendingApproval codeExample = new GetAllProposalsPendingApproval();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    public void Run(DfpUser user) {
      // Get the ProposalService.
      ProposalService proposalService =
          (ProposalService) user.GetService(DfpService.v201605.ProposalService);

      // Create a statement to only select proposals that are pending approval.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("status = :status")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("status", ProposalStatus.PENDING_APPROVAL.ToString());

      // Set default for page.
      ProposalPage page = new ProposalPage();

      try {
        do {
          // Get proposals by statement.
          page = proposalService.getProposalsByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            int i = page.startIndex;
            foreach (Proposal proposal in page.results) {
              Console.WriteLine("{0}) Proposal with ID = '{1}', name = '{2}' was found.",
                  i++, proposal.id, proposal.name);
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while(statementBuilder.GetOffset() < page.totalResultSetSize);
        Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get proposals. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}

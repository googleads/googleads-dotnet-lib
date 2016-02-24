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
  /// This code example submits a proposal for approval. To determine which proposals exist,
  /// run GetAllProposals.cs.
  /// </summary>
  class ApproveProposal : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example submits a proposal for approval. To determine which proposals " +
            "exist, run GetAllProposals.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ApproveProposal();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ProposalService.
      ProposalService proposalService =
          (ProposalService) user.GetService(DfpService.v201602.ProposalService);

      // Set the ID of the proposal.
      long proposalId = long.Parse(_T("INSERT_PROPOSAL_ID_HERE"));

      // Create statement to select the proposal.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", proposalId);

      // Set default for page.
      ProposalPage page = new ProposalPage();
      List<string> proposalIds = new List<string>();
      int i = 0;

      try {
        do {
          // Get proposals by statement.
          page = proposalService.getProposalsByStatement(statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            foreach (Proposal proposal in page.results) {
              Console.WriteLine("{0}) Proposal with ID = '{1}', name = '{2}', and status ='{3}' " +
                  "will be approved.", i++, proposal.id, proposal.name, proposal.status);
              proposalIds.Add(proposal.id.ToString());
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of proposals to be approved: {0}", proposalIds.Count);

        if (proposalIds.Count > 0) {
          // Modify statement for action.
          statementBuilder.RemoveLimitAndOffset();

          // Create action.
          SubmitProposalsForApproval action = new SubmitProposalsForApproval();

          // Perform action.
          UpdateResult result = proposalService.performProposalAction(action,
              statementBuilder.ToStatement());

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of proposals approved: {0}", result.numChanges);
          } else {
            Console.WriteLine("No proposals were approved.");
          }
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to approve proposals. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}

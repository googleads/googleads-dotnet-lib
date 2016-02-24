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
  /// This code example updates the note of an proposal. To determine which proposals exist,
  /// run GetAllProposals.cs.
  /// </summary>
  class UpdateProposals : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the note of an proposal. To determine which proposals " +
            "exist, run GetAllProposals.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateProposals();
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

      long proposalId = long.Parse(_T("INSERT_PROPOSAL_ID_HERE"));

      // Create a statement to get the proposal.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", proposalId);

      try {
        // Get proposals by statement.
        ProposalPage page =
            proposalService.getProposalsByStatement(statementBuilder.ToStatement());

        Proposal proposal = page.results[0];

        // Update the proposal object by changing its note.
        proposal.notes = "Proposal needs further review before approval.";

        // Update the proposals on the server.
        Proposal[] proposals = proposalService.updateProposals(new Proposal[] {proposal});

        if (proposals != null) {
          foreach (Proposal updatedProposal in proposals) {
            Console.WriteLine("Proposal with ID = '{0}', name = '{1}', and notes = '{2}' was " +
                "updated.", updatedProposal.id, updatedProposal.name, updatedProposal.notes);
          }
        } else {
          Console.WriteLine("No proposals updated.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update proposals. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}

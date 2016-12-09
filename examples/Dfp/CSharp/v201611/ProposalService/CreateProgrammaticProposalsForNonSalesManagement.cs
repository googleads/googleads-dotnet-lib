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
using Google.Api.Ads.Dfp.v201611;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201611 {
  /// <summary>
  /// This example creates a programmatic proposal for networks not using sales management.
  /// </summary>
  public class CreateProgrammaticProposalsForNonSalesManagement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example creates a programmatic proposal for networks "
            + "not using sales management";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      CreateProgrammaticProposalsForNonSalesManagement codeExample = 
          new CreateProgrammaticProposalsForNonSalesManagement();
      Console.WriteLine(codeExample.Description);

      long primarySalespersonId = long.Parse(_T("INSERT_PRIMARY_SALESPERSON_ID_HERE"));
      long primaryTraffickerId = long.Parse(_T("INSERT_PRIMARY_TRAFFICKER_ID_HERE"));

      // Set the ID of the programmatic buyer. This can be obtained through the
      // Programmatic_Buyer PQL table.
      long programmaticBuyerId = long.Parse(_T("INSERT_PROGRAMMATIC_BUYER_ID_HERE"));

      codeExample.Run(new DfpUser(), primarySalespersonId, primaryTraffickerId,
          programmaticBuyerId);
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    public void Run(DfpUser user, long primarySalespersonId, long primaryTraffickerId,
        long programmaticBuyerId) {
      // Get the ProposalService.
      ProposalService proposalService =
          (ProposalService) user.GetService(DfpService.v201611.ProposalService);

      // Create a proposal.
      Proposal proposal = new Proposal();
      proposal.name = "Programmatic proposal #" + new Random().Next(int.MaxValue);

      // Set required Marketplace information
      proposal.marketplaceInfo = new ProposalMarketplaceInfo() {
        buyerAccountId = programmaticBuyerId
      };
      proposal.isProgrammatic = true;
      proposal.primaryTraffickerId = primaryTraffickerId;

      // Create salesperson splits for the primary salesperson and secondary salespeople.
      SalespersonSplit primarySalesperson = new SalespersonSplit();
      primarySalesperson.userId = primarySalespersonId;
      primarySalesperson.split = 100000;
      proposal.primarySalesperson = primarySalesperson;

      try {
        // Create the proposal on the server.
        Proposal[] proposals = proposalService.createProposals(new Proposal[] {proposal});

        foreach (Proposal createdProposal in proposals) {
          Console.WriteLine("A programmatic proposal with ID \"{0}\" "
              + "and name \"{1}\" was created.",
              createdProposal.id,
              createdProposal.name);
        }
      } catch (Exception e) {
          Console.WriteLine("Failed to create proposals. Exception says \"{0}\"",
                            e.Message);
      }
    }
  }
}

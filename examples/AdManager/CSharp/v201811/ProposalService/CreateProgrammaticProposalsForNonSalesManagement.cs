// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This example creates a programmatic proposal for networks not using sales management.
    /// </summary>
    public class CreateProgrammaticProposalsForNonSalesManagement : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example creates a programmatic proposal for networks " +
                    "not using sales management";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateProgrammaticProposalsForNonSalesManagement codeExample =
                new CreateProgrammaticProposalsForNonSalesManagement();
            Console.WriteLine(codeExample.Description);

            long primarySalespersonId = long.Parse(_T("INSERT_PRIMARY_SALESPERSON_ID_HERE"));
            long primaryTraffickerId = long.Parse(_T("INSERT_PRIMARY_TRAFFICKER_ID_HERE"));

            // Set the ID of the programmatic buyer. This can be obtained through the
            // Programmatic_Buyer PQL table.
            long programmaticBuyerId = long.Parse(_T("INSERT_PROGRAMMATIC_BUYER_ID_HERE"));

            long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));

            codeExample.Run(new AdManagerUser(), primarySalespersonId, primaryTraffickerId,
                programmaticBuyerId, advertiserId);
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long primarySalespersonId, long primaryTraffickerId,
            long programmaticBuyerId, long advertiserId)
        {
            using (ProposalService proposalService = user.GetService<ProposalService>())
            {

                // Create a proposal with the minimum required fields.
                Proposal proposal = new Proposal()
                {
                    name = "Programmatic proposal #" + new Random().Next(int.MaxValue),
                    isProgrammatic = true,
                    // Set required Marketplace information
                    marketplaceInfo = new ProposalMarketplaceInfo()
                    {
                        buyerAccountId = programmaticBuyerId
                    }
                };

                // Set fields that are required before sending the proposal to the buyer.
                proposal.primaryTraffickerId = primaryTraffickerId;
                proposal.sellerContactIds = new long[] { primarySalespersonId };
                proposal.primarySalesperson = new SalespersonSplit()
                {
                    userId = primarySalespersonId,
                    split = 100000
                };
                proposal.advertiser = new ProposalCompanyAssociation()
                {
                    type = ProposalCompanyAssociationType.ADVERTISER,
                    companyId = advertiserId
                };

                try
                {
                    // Create the proposal on the server.
                    Proposal[] proposals =
                        proposalService.createProposals(new Proposal[] { proposal });

                    foreach (Proposal createdProposal in proposals)
                    {
                        Console.WriteLine("A programmatic proposal with ID \"{0}\" " +
                            "and name \"{1}\" was created.",
                            createdProposal.id, createdProposal.name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create proposals. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}

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
    /// This example creates a programmatic proposal. Your network must have sales management
    /// enabled to run this example.
    /// </summary>
    public class CreateProgrammaticProposals : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This example creates a programmatic proposal. Your network must have sales " +
                    "management enabled to run this example.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateProgrammaticProposals codeExample = new CreateProgrammaticProposals();
            Console.WriteLine(codeExample.Description);

            // Set the advertiser, salesperson, and trafficker to assign to each
            // order.
            long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));
            long buyerId = long.Parse(_T("INSERT_BUYER_ID_HERE"));
            long primarySalespersonId = long.Parse(_T("INSERT_PRIMARY_SALESPERSON_ID_HERE"));
            long primaryTraffickerId = long.Parse(_T("INSERT_PRIMARY_TRAFFICKER_ID_HERE"));

            codeExample.Run(new AdManagerUser(), advertiserId, buyerId, primarySalespersonId,
                primaryTraffickerId);
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long advertiserId, long buyerId,
            long primarySalespersonId, long primaryTraffickerId)
        {
            using (ProposalService proposalService = user.GetService<ProposalService>())
            using (NetworkService networkService = user.GetService<NetworkService>())
            {
                // Create a proposal.
                Proposal proposal = new Proposal();
                proposal.name = "Programmatic proposal #" + new Random().Next(int.MaxValue);

                // Set the required Marketplace information.
                proposal.marketplaceInfo = new ProposalMarketplaceInfo()
                {
                    buyerAccountId = buyerId
                };
                proposal.isProgrammatic = true;

                // Create a proposal company association.
                ProposalCompanyAssociation proposalCompanyAssociation =
                    new ProposalCompanyAssociation();
                proposalCompanyAssociation.companyId = advertiserId;
                proposalCompanyAssociation.type = ProposalCompanyAssociationType.ADVERTISER;
                proposal.advertiser = proposalCompanyAssociation;

                // Create salesperson splits for the primary salesperson.
                SalespersonSplit primarySalesperson = new SalespersonSplit();
                primarySalesperson.userId = primarySalespersonId;
                primarySalesperson.split = 100000;
                proposal.primarySalesperson = primarySalesperson;

                // Set the probability to close to 100%.
                proposal.probabilityOfClose = 100000L;

                // Set the primary trafficker on the proposal for when it becomes an order.
                proposal.primaryTraffickerId = primaryTraffickerId;

                // Create a budget for the proposal worth 100 in the network local currency.
                Money budget = new Money();
                budget.microAmount = 100000000L;
                budget.currencyCode = networkService.getCurrentNetwork().currencyCode;
                proposal.budget = budget;

                try
                {
                    // Create the proposal on the server.
                    Proposal[] proposals = proposalService.createProposals(new Proposal[]
                    {
                        proposal
                    });

                    foreach (Proposal createdProposal in proposals)
                    {
                        Console.WriteLine(
                            "A programmatic proposal with ID \"{0}\" and name \"{1}\" " +
                            "was created.", createdProposal.id, createdProposal.name);
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

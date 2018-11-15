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
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example creates a new programmatic proposal line item that 
    /// uses its product's targeting. Your network must have sales management enabled
    /// to run this example.
    /// </summary>
    public class CreateProgrammaticProposalLineItems : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates a new programmatic proposal line item that " +
                    "uses its product's targeting. Your network must have sales management " +
                    "enabled to run this example.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateProgrammaticProposalLineItems codeExample =
                new CreateProgrammaticProposalLineItems();
            Console.WriteLine(codeExample.Description);

            long proposalId = long.Parse(_T("INSERT_PROPOSAL_ID_HERE"));
            long rateCardId = long.Parse(_T("INSERT_RATE_CARD_ID_HERE"));
            long productId = long.Parse(_T("INSERT_PRODUCT_ID_HERE"));

            codeExample.Run(new AdManagerUser(), proposalId, rateCardId, productId);
        }

        /// <summary>
        /// Run the code examples.
        /// </summary>
        public void Run(AdManagerUser user, long proposalId, long rateCardId, long productId)
        {
            using (ProposalLineItemService proposalLineItemService =
                user.GetService<ProposalLineItemService>())
            {
                // Create a proposal line item.
                ProposalLineItem proposalLineItem = new ProposalLineItem();
                proposalLineItem.name = "Programmatic proposal line item #" +
                    new Random().Next(int.MaxValue);
                proposalLineItem.proposalId = proposalId;
                proposalLineItem.rateCardId = rateCardId;
                proposalLineItem.productId = productId;

                // Set the Marketplace information.
                proposalLineItem.marketplaceInfo = new ProposalLineItemMarketplaceInfo()
                {
                    adExchangeEnvironment = AdExchangeEnvironment.DISPLAY
                };

                // Set the length of the proposal line item to run.
                proposalLineItem.startDateTime =
                    DateTimeUtilities.FromDateTime(System.DateTime.Now.AddDays(7),
                        "America/New_York");
                proposalLineItem.endDateTime =
                    DateTimeUtilities.FromDateTime(System.DateTime.Now.AddDays(30),
                        "America/New_York");

                // Set pricing for the proposal line item for 1000 impressions at a CPM of $2
                // for a total value of $2.
                proposalLineItem.goal = new Goal()
                {
                    unitType = UnitType.IMPRESSIONS,
                    units = 1000L
                };
                proposalLineItem.netCost = new Money()
                {
                    currencyCode = "USD",
                    microAmount = 2000000L
                };
                proposalLineItem.netRate = new Money()
                {
                    currencyCode = "USD",
                    microAmount = 2000000L
                };
                proposalLineItem.rateType = RateType.CPM;

                try
                {
                    // Create the proposal line item on the server.
                    ProposalLineItem[] proposalLineItems =
                        proposalLineItemService.createProposalLineItems(new ProposalLineItem[]
                        {
                            proposalLineItem
                        });

                    foreach (ProposalLineItem createdProposalLineItem in proposalLineItems)
                    {
                        Console.WriteLine(
                            "A programmatic proposal line item with ID \"{0}\" " +
                            "and name \"{1}\" was created.", createdProposalLineItem.id,
                            createdProposalLineItem.name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to create proposal line items. Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}

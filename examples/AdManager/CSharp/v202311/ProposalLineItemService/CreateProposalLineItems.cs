// Copyright 2019 Google LLC
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
using Google.Api.Ads.AdManager.Util.v202311;
using Google.Api.Ads.AdManager.v202311;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This example creates a new proposal line item. 
    /// </summary>
    public class CreateProposalLineItems : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example creates a new proposal line item.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateProposalLineItems codeExample = new CreateProposalLineItems();
            Console.WriteLine(codeExample.Description);

            // Set the ID of the proposal that the proposal line item will belong to.
            long proposalId = long.Parse(_T("INSERT_PROPOSAL_ID_HERE"));

            codeExample.Run(new AdManagerUser(), proposalId);
        }

        /// <summary>
        /// Run the code examples.
        /// </summary>
        public void Run(AdManagerUser user, long proposalId)
        {
            using (ProposalLineItemService proposalLineItemService =
                user.GetService<ProposalLineItemService>())

                using (NetworkService networkService = user.GetService<NetworkService>())
                {
                    ProposalLineItem proposalLineItem = new ProposalLineItem();
                    proposalLineItem.name = "Proposal line item #" +
                        new Random().Next(int.MaxValue);
                    proposalLineItem.proposalId = proposalId;
                    proposalLineItem.lineItemType = LineItemType.STANDARD;

                    // Get the root ad unit ID used to target the whole site.
                    String rootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

                    // Create inventory targeting.
                    InventoryTargeting inventoryTargeting = new InventoryTargeting();

                    // Create ad unit targeting for the root ad unit (i.e. the whole network).
                    AdUnitTargeting adUnitTargeting = new AdUnitTargeting();
                    adUnitTargeting.adUnitId = rootAdUnitId;
                    adUnitTargeting.includeDescendants = true;

                    inventoryTargeting.targetedAdUnits = new AdUnitTargeting[]
                    {
                        adUnitTargeting
                    };

                    RequestPlatformTargeting requestPlatformTargeting = new RequestPlatformTargeting()
                    {
                       targetedRequestPlatforms = new RequestPlatform[] { RequestPlatform.BROWSER }
                    };

                    // Set targeting.
                    proposalLineItem.targeting = new Targeting()
                    {
                      inventoryTargeting = inventoryTargeting,
                      requestPlatformTargeting = requestPlatformTargeting
                    };

                    // Create creative placeholder.
                    Size size = new Size()
                    {
                        width = 300,
                        height = 250,
                        isAspectRatio = false
                    };
                    CreativePlaceholder creativePlaceholder = new CreativePlaceholder();
                    creativePlaceholder.size = size;

                    proposalLineItem.creativePlaceholders = new CreativePlaceholder[]
                    {
                        creativePlaceholder
                    };

                    // Set the length of the proposal line item to run.
                    proposalLineItem.startDateTime =
                        DateTimeUtilities.FromDateTime(System.DateTime.Now.AddDays(7),
                            "America/New_York");
                    proposalLineItem.endDateTime =
                        DateTimeUtilities.FromDateTime(System.DateTime.Now.AddDays(30),
                            "America/New_York");

                    // Set delivery specifications for the proposal line item.
                    proposalLineItem.deliveryRateType = DeliveryRateType.EVENLY;

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
                            Console.WriteLine("A proposal line item with ID \"{0}\" " +
                                "and name \"{1}\" was created.", createdProposalLineItem.id,
                                createdProposalLineItem.name);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(
                            "Failed to create proposal line items. " +
                            "Exception says \"{0}\"", e.Message);
                    }
                }
        }
    }
}

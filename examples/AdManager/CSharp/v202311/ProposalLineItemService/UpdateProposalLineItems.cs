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
    /// This code example updates a proposal line item's notes. To determine which 
    /// proposal line items exist, run GetAllProposalLineItems.cs.
    /// </summary>
    public class UpdateProposalLineItems : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example updates a proposal line item's notes. To determine which " +
                    "proposal line items exist, run GetAllProposalLineItems.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateProposalLineItems codeExample = new UpdateProposalLineItems();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ProposalLineItemService proposalLineItemService =
                user.GetService<ProposalLineItemService>())
            {
                // Set the ID of the proposal line item.
                long proposalLineItemId = long.Parse(_T("INSERT_PROPOSAL_LINE_ITEM_ID_HERE"));

                // Create a statement to get the proposal line item.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :proposalLineItemId").OrderBy("id ASC").Limit(1)
                    .AddValue("proposalLineItemId", proposalLineItemId);

                try
                {
                    // Get proposal line items by statement.
                    ProposalLineItemPage page =
                        proposalLineItemService.getProposalLineItemsByStatement(
                            statementBuilder.ToStatement());

                    ProposalLineItem proposalLineItem = page.results[0];

                    // Update proposal line item notes.
                    proposalLineItem.internalNotes = "Proposal line item ready for submission";

                    // Update the proposal line item on the server.
                    ProposalLineItem[] proposalLineItems =
                        proposalLineItemService.updateProposalLineItems(new ProposalLineItem[]
                        {
                            proposalLineItem
                        });

                    if (proposalLineItems != null)
                    {
                        foreach (ProposalLineItem updatedProposalLineItem in proposalLineItems)
                        {
                            Console.WriteLine(
                                "A proposal line item with ID = '{0}' and name '{1}' was updated.",
                                updatedProposalLineItem.id, updatedProposalLineItem.name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No proposal line items updated.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to update proposal line items. Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}

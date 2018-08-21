// Copyright 2017, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdManager.Util.v201808;
using Google.Api.Ads.AdManager.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This example gets all proposal line items belonging to a specific proposal.
    /// </summary>
    public class GetProposalLineItemsForProposal : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This example gets all proposal line items belonging to a specific proposal.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetProposalLineItemsForProposal codeExample = new GetProposalLineItemsForProposal();
            long proposalId = long.Parse("INSERT_PROPOSAL_ID_HERE");
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser(), proposalId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get proposal line items. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long proposalId)
        {
            using (ProposalLineItemService proposalLineItemService =
                user.GetService<ProposalLineItemService>())
            {
                // Create a statement to select proposal line items.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("proposalId = :proposalId").OrderBy("id ASC").Limit(pageSize)
                    .AddValue("proposalId", proposalId);

                // Retrieve a small amount of proposal line items at a time, paging through until
                // all proposal line items have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    ProposalLineItemPage page =
                        proposalLineItemService.getProposalLineItemsByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each proposal line item.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (ProposalLineItem proposalLineItem in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Proposal line item with ID {1} and name \"{2}\" was found.",
                                i++, proposalLineItem.id, proposalLineItem.name);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}

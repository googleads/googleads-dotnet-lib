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
using Google.Api.Ads.AdManager.Util.v201808;
using Google.Api.Ads.AdManager.v201808;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This example sends programmatic proposals to Marketplace to request buyer acceptance.
    /// </summary>
    public class RequestBuyerAcceptance : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This example sends programmatic proposals to Marketplace to request buyer " +
                    "acceptance.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            RequestBuyerAcceptance codeExample = new RequestBuyerAcceptance();
            Console.WriteLine(codeExample.Description);

            // Set the ID of the proposal.
            long proposalId = long.Parse(_T("INSERT_PROPOSAL_ID_HERE"));

            codeExample.Run(new AdManagerUser(), proposalId);
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long proposalId)
        {
            using (ProposalService proposalService = user.GetService<ProposalService>())
            {
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

                try
                {
                    do
                    {
                        // Get proposals by statement.
                        page = proposalService.getProposalsByStatement(
                            statementBuilder.ToStatement());

                        if (page.results != null)
                        {
                            foreach (Proposal proposal in page.results)
                            {
                                Console.WriteLine(
                                    "{0}) Proposal with ID = '{1}', name = '{2}', " +
                                    "and status = '{3}' will be sent to Marketplace for buyer " +
                                    "acceptance.",
                                    i++, proposal.id, proposal.name, proposal.status);
                                proposalIds.Add(proposal.id.ToString());
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of proposals to be sent to Marketplace: {0}",
                        proposalIds.Count);

                    if (proposalIds.Count > 0)
                    {
                        // Modify statement for action.
                        statementBuilder.RemoveLimitAndOffset();

                        // Create action.
                        Google.Api.Ads.AdManager.v201808.RequestBuyerAcceptance action =
                            new Google.Api.Ads.AdManager.v201808.RequestBuyerAcceptance();

                        // Perform action.
                        UpdateResult result =
                            proposalService.performProposalAction(action,
                                statementBuilder.ToStatement());

                        // Display results.
                        if (result != null && result.numChanges > 0)
                        {
                            Console.WriteLine(
                                "Number of proposals that were sent to Marketplace: {0}",
                                result.numChanges);
                        }
                        else
                        {
                            Console.WriteLine("No proposals were sent to Marketplace.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to send proposals to Marketplace. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}

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
using Google.Api.Ads.AdManager.Util.v201902;
using Google.Api.Ads.AdManager.v201902;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This example gets all proposals pending approval.
    /// </summary>
    public class GetProposalsPendingApproval : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets all proposals pending approval."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetProposalsPendingApproval codeExample = new GetProposalsPendingApproval();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get proposals. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ProposalService proposalService = user.GetService<ProposalService>())
            {
                // Create a statement to select proposals.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("status = :status")
                    .OrderBy("id ASC")
                    .Limit(pageSize)
                    .AddValue("status", ProposalStatus.PENDING_APPROVAL.ToString());

                // Retrieve a small amount of proposals at a time, paging through until all
                // proposals have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    ProposalPage page =
                        proposalService.getProposalsByStatement(statementBuilder.ToStatement());

                    // Print out some information for each proposal.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (Proposal proposal in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Proposal with ID {1} and name \"{2}\" was found.", i++,
                                proposal.id, proposal.name);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}

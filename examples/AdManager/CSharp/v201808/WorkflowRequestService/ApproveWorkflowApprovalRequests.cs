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
    /// This code example approves all workflow approval requests belonging to a specific proposal.
    /// To determine which proposals exist, run GetAllProposals.cs.
    /// </summary>
    public class ApproveWorkflowApprovalRequests : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example approves all workflow approval requests belonging to " +
                    "a specific proposal. To determine which proposals exist, " +
                    "run GetAllProposals.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            ApproveWorkflowApprovalRequests codeExample = new ApproveWorkflowApprovalRequests();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (WorkflowRequestService workflowRequestService =
                user.GetService<WorkflowRequestService>())
            {
                // Set the ID of the proposal to approve workflow approval requests for.
                long proposalId = long.Parse(_T("INSERT_PROPOSAL_ID_HERE"));

                // Create a statement to select workflow approval requests for a proposal.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where(
                        "WHERE entityId = :entityId and entityType = :entityType and type = :type")
                    .OrderBy("id ASC")
                    .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
                    .AddValue("entityId", proposalId)
                    .AddValue("entityType", WorkflowEntityType.PROPOSAL.ToString())
                    .AddValue("type", WorkflowRequestType.WORKFLOW_APPROVAL_REQUEST.ToString());

                // Set default for page.
                WorkflowRequestPage page = new WorkflowRequestPage();
                List<long> worflowRequestIds = new List<long>();

                try
                {
                    do
                    {
                        // Get workflow requests by statement.
                        page = workflowRequestService.getWorkflowRequestsByStatement(
                            statementBuilder.ToStatement());

                        if (page.results != null)
                        {
                            int i = page.startIndex;
                            foreach (WorkflowRequest workflowRequest in page.results)
                            {
                                Console.WriteLine(
                                    "{0})  Workflow approval request with ID '{1}' will be " +
                                    "approved.", i++, workflowRequest.id);
                                worflowRequestIds.Add(workflowRequest.id);
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of workflow approval requests to be approved: {0}",
                        worflowRequestIds.Count);

                    if (worflowRequestIds.Count > 0)
                    {
                        // Modify statement.
                        statementBuilder.RemoveLimitAndOffset();

                        // Create action.
                        Google.Api.Ads.AdManager.v201808.ApproveWorkflowApprovalRequests action =
                            new Google.Api.Ads.AdManager.v201808.ApproveWorkflowApprovalRequests();

                        // Add a comment to the approval.
                        action.comment = "The proposal looks good to me. Approved.";

                        // Perform action.
                        UpdateResult result =
                            workflowRequestService.performWorkflowRequestAction(action,
                                statementBuilder.ToStatement());

                        // Display results.
                        if (result != null && result.numChanges > 0)
                        {
                            Console.WriteLine("Number of workflow requests approved: {0}",
                                result.numChanges);
                        }
                        else
                        {
                            Console.WriteLine("No workflow requests were approved.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to archive workflow requests. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}

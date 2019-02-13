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
    /// This example gets workflow external condition requests. Workflow external condition
    /// requests must be triggered or skipped for a workflow to finish.
    /// </summary>
    public class GetWorkflowExternalConditionRequests : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This example gets workflow external condition requests. Workflow external " +
                    "condition requests must be triggered or skipped for a workflow to finish.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetWorkflowExternalConditionRequests codeExample =
                new GetWorkflowExternalConditionRequests();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get workflow requests. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (WorkflowRequestService workflowRequestService =
                user.GetService<WorkflowRequestService>())
            {
                // Create a statement to select workflow requests.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("type = :type")
                    .OrderBy("id ASC")
                    .Limit(pageSize)
                    .AddValue("type",
                        WorkflowRequestType.WORKFLOW_EXTERNAL_CONDITION_REQUEST.ToString());

                // Retrieve a small amount of workflow requests at a time, paging through until all
                // workflow requests have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    WorkflowRequestPage page =
                        workflowRequestService.getWorkflowRequestsByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each workflow request.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (WorkflowRequest workflowRequest in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Workflow request with ID {1}, " + "entity type \"{2}\", " +
                                "and entity ID {3} was found.", i++, workflowRequest.id,
                                workflowRequest.entityType, workflowRequest.entityId);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}

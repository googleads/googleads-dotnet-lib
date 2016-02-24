// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets workflow approval requests. Workflow approval requests must
  /// be approved or rejected for a workflow to finish.
  /// </summary>
  class GetWorkflowApprovalRequests : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets workflow approval requests. Workflow approval requests " +
            "must be approved or rejected for a workflow to finish.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetWorkflowApprovalRequests();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the WorkflowRequestService.
      WorkflowRequestService workflowRequestService =
          (WorkflowRequestService) user.GetService(DfpService.v201602.WorkflowRequestService);

      // Create a statement to select all workflow approval requests.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("type = :type")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("type", WorkflowRequestType.WORKFLOW_APPROVAL_REQUEST.ToString());

      // Set default for page.
      WorkflowRequestPage page = new WorkflowRequestPage();

      try {
        do {
          // Get workflow requests by statement.
          page = workflowRequestService
              .getWorkflowRequestsByStatement(statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (WorkflowRequest workflowRequest in page.results) {
              Console.WriteLine("{0}) Workflow approval request with ID '{1}' for {2} with ID " +
                  "'{3}' was found.", i++, workflowRequest.id,
                  workflowRequest.entityType.ToString(), workflowRequest.entityId);
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);
        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get workflow requests by statement. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}

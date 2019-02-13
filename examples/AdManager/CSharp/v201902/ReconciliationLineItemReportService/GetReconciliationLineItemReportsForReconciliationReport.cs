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
using Google.Api.Ads.AdManager.Util.v201902;
using Google.Api.Ads.AdManager.v201902;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This example gets a reconciliation report's data for line items that served through DFP.
    /// </summary>
    public class GetReconciliationLineItemReportsForReconciliationReport : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This example gets a reconciliation report's data for line items that served " +
                    "through DFP.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetReconciliationLineItemReportsForReconciliationReport codeExample =
                new GetReconciliationLineItemReportsForReconciliationReport();
            long reconciliationReportId = long.Parse("INSERT_RECONCILIATION_REPORT_ID_HERE");
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser(), reconciliationReportId);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Failed to get reconciliation line item reports. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long reconciliationReportId)
        {
            using (ReconciliationLineItemReportService reconciliationLineItemReportService =
                user.GetService<ReconciliationLineItemReportService>())
            {
                // Create a statement to select reconciliation line item reports.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("reconciliationReportId = :reconciliationReportId AND " +
                        "lineItemId != :lineItemId").OrderBy("lineItemId ASC").Limit(pageSize)
                    .AddValue("reconciliationReportId", reconciliationReportId)
                    .AddValue("lineItemId", 0);

                // Retrieve a small amount of reconciliation line item reports at a time, paging
                // through until all reconciliation line item reports have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    ReconciliationLineItemReportPage page =
                        reconciliationLineItemReportService
                            .getReconciliationLineItemReportsByStatement(statementBuilder
                                .ToStatement());

                    // Print out some information for each reconciliation line item report.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (ReconciliationLineItemReport reconciliationLineItemReport in page
                            .results)
                        {
                            Console.WriteLine(
                                "{0}) Reconciliation line item report with ID {1}, " +
                                "line item ID {2}, " + "reconciliation source \"{3}\", " +
                                "and reconciled volume {4} was found.", i++,
                                reconciliationLineItemReport.id,
                                reconciliationLineItemReport.lineItemId,
                                reconciliationLineItemReport.reconciliationSource,
                                reconciliationLineItemReport.reconciledVolume);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}

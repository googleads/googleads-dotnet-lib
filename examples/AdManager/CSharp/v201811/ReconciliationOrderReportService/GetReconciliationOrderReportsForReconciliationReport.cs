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
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This example gets all reconciliation order reports for a given reconciliation report.
    /// </summary>
    public class GetReconciliationOrderReportsForReconciliationReport : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example gets all reconciliation order reports for a given " +
                    "reconciliation report.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetReconciliationOrderReportsForReconciliationReport codeExample =
                new GetReconciliationOrderReportsForReconciliationReport();
            long reconciliationReportId = long.Parse("INSERT_RECONCILIATION_REPORT_ID_HERE");
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser(), reconciliationReportId);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Failed to get reconciliation order reports. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long reconciliationReportId)
        {
            using (ReconciliationOrderReportService reconciliationOrderReportService =
                user.GetService<ReconciliationOrderReportService>())
            {
                // Create a statement to select reconciliation order reports.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("reconciliationReportId = :reconciliationReportId").OrderBy("id ASC")
                    .Limit(pageSize)
                    .AddValue("reconciliationReportId", reconciliationReportId);

                // Retrieve a small amount of reconciliation order reports at a time, paging through
                // until all reconciliation order reports have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    ReconciliationOrderReportPage page =
                        reconciliationOrderReportService.getReconciliationOrderReportsByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each reconciliation order report.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (ReconciliationOrderReport reconciliationOrderReport in page.results
                        )
                        {
                            Console.WriteLine(
                                "{0}) Reconciliation order report with ID {1} and status \"{2}\" " +
                                "was found.",
                                i++, reconciliationOrderReport.id,
                                reconciliationOrderReport.status);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}

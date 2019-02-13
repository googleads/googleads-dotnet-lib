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
    /// This example gets a reconciliation report's rows for line items that served through DFP.
    /// </summary>
    public class GetReconciliationReportRowsForReconciliationReport : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This example gets a reconciliation report's rows for line items that served " +
                    "through DFP.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetReconciliationReportRowsForReconciliationReport codeExample =
                new GetReconciliationReportRowsForReconciliationReport();
            long reconciliationReportId = long.Parse("INSERT_RECONCILIATION_REPORT_ID_HERE");
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser(), reconciliationReportId);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Failed to get reconciliation report rows. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long reconciliationReportId)
        {
            using (ReconciliationReportRowService reconciliationReportRowService =
                user.GetService<ReconciliationReportRowService>())
            {
                // Create a statement to select reconciliation report rows.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("reconciliationReportId = :reconciliationReportId AND " +
                        "lineItemId != :lineItemId").OrderBy("id ASC").Limit(pageSize)
                    .AddValue("reconciliationReportId", reconciliationReportId)
                    .AddValue("lineItemId", 0);

                // Retrieve a small amount of reconciliation report rows at a time, paging through
                // until all reconciliation report rows have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    ReconciliationReportRowPage page =
                        reconciliationReportRowService.getReconciliationReportRowsByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each reconciliation report row.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (ReconciliationReportRow reconciliationReportRow in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Reconciliation report row with ID {1}, " +
                                "reconciliation source \"{2}\", " +
                                "and reconciled volume {3} was found.", i++,
                                reconciliationReportRow.id,
                                reconciliationReportRow.reconciliationSource,
                                reconciliationReportRow.reconciledVolume);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}

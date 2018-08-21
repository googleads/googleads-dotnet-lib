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

using Google.Api.Ads.Common.Util.Reports;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v201808;
using Google.Api.Ads.AdManager.Util.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This code example runs a report from a saved query.
    /// </summary>
    public class RunSavedQuery : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This code example runs a report from a saved query."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            RunSavedQuery codeExample = new RunSavedQuery();
            Console.WriteLine(codeExample.Description);

            // Set the ID of the saved query to run.
            // This ID is part of the URL in the Ad Manager UI.
            long savedQueryId = long.Parse(_T("INSERT_SAVED_QUERY_ID_HERE"));
            codeExample.Run(new AdManagerUser(), savedQueryId);
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long savedQueryId)
        {
            using (ReportService reportService = user.GetService<ReportService>())
            {
                // Set the file path where the report will be saved.
                String filePath = _T("INSERT_FILE_PATH_HERE");

                // Create statement to retrieve the saved query.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", savedQueryId);

                SavedQueryPage page =
                    reportService.getSavedQueriesByStatement(statementBuilder.ToStatement());
                SavedQuery savedQuery = page.results[0];

                if (!savedQuery.isCompatibleWithApiVersion)
                {
                    throw new InvalidOperationException("Saved query is not compatible with this " +
                        "API version");
                }

                // Optionally modify the query.
                ReportQuery reportQuery = savedQuery.reportQuery;
                reportQuery.adUnitView = ReportQueryAdUnitView.HIERARCHICAL;

                // Create a report job using the saved query.
                ReportJob reportJob = new ReportJob();
                reportJob.reportQuery = reportQuery;

                try
                {
                    // Run report.
                    reportJob = reportService.runReportJob(reportJob);

                    ReportUtilities reportUtilities =
                        new ReportUtilities(reportService, reportJob.id);

                    // Set download options.
                    ReportDownloadOptions options = new ReportDownloadOptions();
                    options.exportFormat = ExportFormat.CSV_DUMP;
                    options.useGzipCompression = true;
                    reportUtilities.reportDownloadOptions = options;

                    // Download the report.
                    using (ReportResponse reportResponse = reportUtilities.GetResponse())
                    {
                        reportResponse.Save(filePath);
                    }

                    Console.WriteLine("Report saved to \"{0}\".", filePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to run saved query. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}

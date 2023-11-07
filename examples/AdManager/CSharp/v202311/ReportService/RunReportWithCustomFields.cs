// Copyright 2019 Google LLC
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
using Google.Api.Ads.AdManager.v202311;
using Google.Api.Ads.AdManager.Util.v202311;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example runs a report that includes custom fields found in the
    /// line items of an order. To download the report see DownloadReport.cs.
    ///
    /// Tag: ReportService.runReportJob
    /// Tag: LineItemService.getLineItemsByStatement
    /// </summary>
    public class RunReportWithCustomFields : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example runs a report that includes custom fields found in the " +
                    "line items of an order. The report is saved to the specified file path.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            RunReportWithCustomFields codeExample = new RunReportWithCustomFields();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ReportService reportService = user.GetService<ReportService>())
            {
                try
                {
                    // Set the ID of the custom field to report on.
                    long customFieldId = long.Parse(_T("INSERT_FIELD_ID_HERE"));

                    // Set the key ID of the custom dimension to report on.
                    long customDimensionKeyId =
                        long.Parse(_T("INSERT_CUSTOM_DIMENSION_KEY_ID_HERE"));

                    // Set the file path where the report will be saved.
                    String filePath = _T("INSERT_FILE_PATH_HERE");

                    // Create report job.
                    ReportJob reportJob = new ReportJob();

                    // Create report query.
                    ReportQuery reportQuery = new ReportQuery();
                    reportQuery.dateRangeType = DateRangeType.LAST_MONTH;
                    reportQuery.dimensions = new Dimension[]
                    {
                        Dimension.CUSTOM_DIMENSION,
                        Dimension.LINE_ITEM_ID,
                        Dimension.LINE_ITEM_NAME
                    };
                    reportQuery.customFieldIds = new long[] { customFieldId };
                    reportQuery.customDimensionKeyIds = new long[] { customDimensionKeyId };
                    reportQuery.columns = new Column[]
                    {
                        Column.AD_SERVER_IMPRESSIONS
                    };
                    reportJob.reportQuery = reportQuery;

                    // Run report job.
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
                    Console.WriteLine(
                        "Failed to run custom fields report. Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}

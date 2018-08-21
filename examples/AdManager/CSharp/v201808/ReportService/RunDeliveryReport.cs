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
    /// This code example runs a report similar to the "Orders report" in the Ad Manager
    /// UI with additional attributes and can filter to include just one
    /// order. The report is saved to the specified file path.
    /// </summary>
    public class RunDeliveryReport : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example runs a report similar to the \"Orders report\" in the " +
                    "Ad Manager UI with additional attributes and can filter to include just one " +
                    "order. The report is saved to the specified file path.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            RunDeliveryReport codeExample = new RunDeliveryReport();
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
                // Set the file path where the report will be saved.
                String filePath = _T("INSERT_FILE_PATH_HERE");

                long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));

                // Create report job.
                ReportJob reportJob = new ReportJob();
                reportJob.reportQuery = new ReportQuery();
                reportJob.reportQuery.dimensions = new Dimension[]
                {
                    Dimension.ORDER_ID,
                    Dimension.ORDER_NAME
                };
                reportJob.reportQuery.dimensionAttributes = new DimensionAttribute[]
                {
                    DimensionAttribute.ORDER_TRAFFICKER,
                    DimensionAttribute.ORDER_START_DATE_TIME,
                    DimensionAttribute.ORDER_END_DATE_TIME
                };
                reportJob.reportQuery.columns = new Column[]
                {
                    Column.AD_SERVER_IMPRESSIONS,
                    Column.AD_SERVER_CLICKS,
                    Column.AD_SERVER_CTR,
                    Column.AD_SERVER_CPM_AND_CPC_REVENUE,
                    Column.AD_SERVER_WITHOUT_CPD_AVERAGE_ECPM
                };

                // Set a custom date range for the last 8 days
                reportJob.reportQuery.dateRangeType = DateRangeType.CUSTOM_DATE;
                System.DateTime endDateTime = System.DateTime.Now;
                reportJob.reportQuery.startDate = DateTimeUtilities
                    .FromDateTime(endDateTime.AddDays(-8), "America/New_York").date;
                reportJob.reportQuery.endDate = DateTimeUtilities
                    .FromDateTime(endDateTime, "America/New_York").date;

                // Create statement object to filter for an order.
                StatementBuilder statementBuilder = new StatementBuilder().Where("ORDER_ID = :id")
                    .AddValue("id", orderId);
                reportJob.reportQuery.statement = statementBuilder.ToStatement();

                try
                {
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
                    Console.WriteLine("Failed to run delivery report. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}

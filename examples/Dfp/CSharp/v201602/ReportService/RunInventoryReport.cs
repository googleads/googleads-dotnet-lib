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

using Google.Api.Ads.Common.Util.Reports;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201602;
using Google.Api.Ads.Dfp.Util.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example runs a report equal to the "Whole network report" on the
  /// DFP website. The report is saved to the specified file path.
  /// </summary>
  class RunInventoryReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example runs a report equal to the \"Whole network report\" on the " +
            "DFP website. The report is saved to the specified file path.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new RunInventoryReport();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      ReportService reportService = (ReportService) user.GetService(
          DfpService.v201602.ReportService);

      // Get the NetworkService.
      NetworkService networkService = (NetworkService) user.GetService(
            DfpService.v201602.NetworkService);

      // Set the file path where the report will be saved.
      String filePath = _T("INSERT_FILE_PATH_HERE");

      // Get the root ad unit ID to filter on.
      String rootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

      // Create statement to filter on an ancestor ad unit with the root ad unit ID to include all
      // ad units in the network.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("PARENT_AD_UNIT_ID = :parentAdUnitId")
          .AddValue("parentAdUnitId", long.Parse(rootAdUnitId));

      // Create report query.
      ReportQuery reportQuery = new ReportQuery();
      reportQuery.dimensions =
          new Dimension[] {Dimension.AD_UNIT_ID, Dimension.AD_UNIT_NAME};
      reportQuery.columns = new Column[] {Column.AD_SERVER_IMPRESSIONS,
        Column.AD_SERVER_CLICKS, Column.DYNAMIC_ALLOCATION_INVENTORY_LEVEL_IMPRESSIONS,
        Column.DYNAMIC_ALLOCATION_INVENTORY_LEVEL_CLICKS,
        Column.TOTAL_INVENTORY_LEVEL_IMPRESSIONS,
        Column.TOTAL_INVENTORY_LEVEL_CPM_AND_CPC_REVENUE};

      // Set the filter statement.
      reportQuery.statement = statementBuilder.ToStatement();

      reportQuery.adUnitView = ReportQueryAdUnitView.HIERARCHICAL;
      reportQuery.dateRangeType = DateRangeType.LAST_WEEK;

      // Create report job.
      ReportJob reportJob = new ReportJob();
      reportJob.reportQuery = reportQuery;

      try {
        // Run report.
        reportJob = reportService.runReportJob(reportJob);

        ReportUtilities reportUtilities = new ReportUtilities(reportService, reportJob.id);

        // Set download options.
        ReportDownloadOptions options = new ReportDownloadOptions();
        options.exportFormat = ExportFormat.CSV_DUMP;
        options.useGzipCompression = true;
        reportUtilities.reportDownloadOptions = options;

        // Download the report.
        using (ReportResponse reportResponse = reportUtilities.GetResponse()) {
          reportResponse.Save(filePath);
        }
        Console.WriteLine("Report saved to \"{0}\".", filePath);

      } catch (Exception e) {
        Console.WriteLine("Failed to run inventory report. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}

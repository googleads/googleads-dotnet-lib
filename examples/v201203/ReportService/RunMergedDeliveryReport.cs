// Copyright 2012, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201203;

using System;
using System.Threading;

namespace Google.Api.Ads.Dfp.Examples.v201203 {
  /// <summary>
  /// This code example runs a report that an upgraded publisher would use to
  /// include statistics before the upgrade. To download the report see
  /// DownloadReport.cs.
  ///
  /// Tags: ReportService.runReportJob, ReportService.getReportJob
  /// </summary>
  class RunMergedDeliveryReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example runs a report equal to the \"Orders report\" on the DFP " +
            "website with additional attributes. To download the report run DownloadReport.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new RunMergedDeliveryReport();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      ReportService reportService = (ReportService) user.GetService(
          DfpService.v201203.ReportService);

      // Create report job.
      ReportJob reportJob = new ReportJob();

      // Create report query.
      ReportQuery reportQuery = new ReportQuery();
      reportQuery.dateRangeType = DateRangeType.LAST_MONTH;
      reportQuery.dimensions = new Dimension[] {Dimension.ORDER};
      reportQuery.columns = new Column[] {Column.MERGED_AD_SERVER_IMPRESSIONS,
          Column.MERGED_AD_SERVER_CLICKS, Column.MERGED_AD_SERVER_CTR,
          Column.MERGED_AD_SERVER_REVENUE, Column.MERGED_AD_SERVER_AVERAGE_ECPM};
      reportJob.reportQuery = reportQuery;

      try {
        // Run report job.
        reportJob = reportService.runReportJob(reportJob);

        do {
          Console.WriteLine("Report with ID '{0}' is still running.", reportJob.id);
          Thread.Sleep(30000);
          // Get report job.
          reportJob = reportService.getReportJob(reportJob.id);
        } while (reportJob.reportJobStatus == ReportJobStatus.IN_PROGRESS);

        if (reportJob.reportJobStatus == ReportJobStatus.FAILED) {
          Console.WriteLine("Report job with ID '{0}' failed to finish successfully.",
              reportJob.id);
        } else {
          Console.WriteLine("Report job with ID '{0}' completed successfully.", reportJob.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to run delivery report. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

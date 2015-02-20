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

// Author: Anash P. Oommen

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201502;

using System;
using System.Threading;
using Google.Api.Ads.Dfp.Util.v201502;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201502 {
  /// <summary>
  /// This code example runs a reach report. To download the report see
  /// DownloadReport.cs.
  ///
  /// Tags: ReportService.runReportJob, ReportService.getReportJob
  /// </summary>
  class RunReachReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example runs a reach report. To download the report see " +
            "DownloadReport.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new RunReachReport();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      ReportService reportService = (ReportService) user.GetService(
          DfpService.v201502.ReportService);

      // Create report job.
      ReportJob reportJob = new ReportJob();

      // Create report query.
      ReportQuery reportQuery = new ReportQuery();
      reportQuery.dateRangeType = DateRangeType.REACH_LIFETIME;
      reportQuery.dimensions = new Dimension[] {Dimension.LINE_ITEM_ID, Dimension.LINE_ITEM_NAME};
      reportQuery.columns = new Column[] {Column.REACH_FREQUENCY, Column.REACH_AVERAGE_REVENUE,
          Column.REACH};
      reportJob.reportQuery = reportQuery;

      try {
        // Run report.
        reportJob = reportService.runReportJob(reportJob);
        // Wait for report to complete.
        while (reportJob.reportJobStatus == ReportJobStatus.IN_PROGRESS) {
          Console.WriteLine("Report job with id = '{0}' is still running.", reportJob.id);
          Thread.Sleep(30000);
          // Get report job.
          reportJob = reportService.getReportJob(reportJob.id);
        }

        if (reportJob.reportJobStatus == ReportJobStatus.COMPLETED) {
          Console.WriteLine("Report job with id = '{0}' completed successfully.", reportJob.id);
        } else if (reportJob.reportJobStatus == ReportJobStatus.FAILED) {
          Console.WriteLine("Report job with id = '{0}' failed to complete successfully.",
              reportJob.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to run delivery report. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

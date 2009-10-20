// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

using System;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;

namespace com.google.api.adwords.samples.v13 {
  /// <summary>
  /// Schedules structure report and retrives its destination url.
  /// </summary>
  class ReportServiceStructureDemo : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Schedules structure report and retrives its destination url.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      ReportService service =
          (ReportService) user.GetService(AdWordsService.v13.ReportService);

      // Create the report job.
      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.name = "Structure Report";
      reportJob.selectedReportType = "Structure";
      reportJob.aggregationTypes = new String[] {"Keyword"};
      reportJob.selectedColumns = new String[] {
          "Campaign", "CampaignId", "AdGroup", "AdGroupId", "Keyword", "KeywordId",
          "KeywordStatus", "MaximumCPC"};

      // Validate the report job.
      try {
        service.validateReportJob(reportJob);

        // Submit the request for the report.
        long jobId = service.scheduleReportJob(reportJob);

        // Wait until the report has been generated.
        ReportJobStatus status = service.getReportJobStatus(jobId);

        while (status != ReportJobStatus.Completed && status != ReportJobStatus.Failed) {
          Thread.Sleep(30000);
          status = service.getReportJobStatus(jobId);
          Console.WriteLine("Report job status is " + status);
        }

        if (status == ReportJobStatus.Failed) {
          Console.WriteLine("Job failed!");
        } else {
          // Report is ready.
          Console.WriteLine("The report is ready!");

          // Download the report.
          String url = service.getReportDownloadUrl(jobId);
          Console.WriteLine("Download it at url {0}", url);
        }
      } catch (SoapException e) {
        Console.WriteLine("Report job is invalid. Exception: {0}", e.Message);
      }
    }
  }
}

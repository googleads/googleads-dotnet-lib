// Copyright 2011, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v13;

using System;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v13 {
  /// <summary>
  /// This code example schedules a keyword report and retrives its destination
  /// url.
  /// </summary>
  class ReportServiceKeywordDemo : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example schedules a keyword report and retrives its destination url.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      ReportService service =
          (ReportService) user.GetService(AdWordsService.v13.ReportService);

      // Create the report job.
      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.name = "Keyword Report";
      reportJob.selectedReportType = "Keyword";
      reportJob.aggregationTypes = new string[] {"Daily"};
      reportJob.adWordsType = AdWordsType.SearchOnly;
      reportJob.endDay = DateTime.Today;  // defaults to today
      reportJob.startDay = new DateTime(2009, 1, 1);
      reportJob.selectedColumns = new string[] {
          "Campaign", "AdGroup", "Keyword", "KeywordStatus", "KeywordMinCPC",
          "KeywordDestUrlDisplay", "Impressions", "Clicks", "CTR", "AveragePosition"};

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
          string url = service.getReportDownloadUrl(jobId);
          Console.WriteLine("Download it at url {0}", url);
        }
      } catch (SoapException e) {
        Console.WriteLine("Report job is invalid. Exception: {0}", e.Message);
      }
    }
  }
}

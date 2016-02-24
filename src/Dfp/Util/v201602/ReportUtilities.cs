// Copyright 2014, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Common.Util.Reports;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201602;

using System.Web;
using System.Net;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.Dfp.Util.v201602 {

  /// <summary>
  /// Utility class for DFP API report downloads.
  /// </summary>
  public class ReportUtilities : AdsReportUtilities {

    /// <summary>
    /// The report service object to make calls with
    /// </summary>
    private ReportService reportService;

    /// <summary>
    /// The ID of the report job to check
    /// </summary>
    private long reportJobId;

    /// <summary>
    /// The options to use when downloading the completed report.
    /// </summary>
    public ReportDownloadOptions reportDownloadOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportUtilities"/>
    /// class.
    /// </summary>
    /// <param name="reportService">ReportService to be used</param>
    /// <param name="reportJobId">The ID of the report job</param>
    public ReportUtilities(ReportService reportService, long reportJobId)
        : base(reportService.User) {
      this.reportService = reportService;
      this.reportJobId = reportJobId;
    }

    protected override bool ShouldWaitMore() {
      ReportJobStatus status = reportService.getReportJobStatus(reportJobId);
      if (status == ReportJobStatus.FAILED) {
        throw new AdsReportsException(string.Format("Report job {0} failed.", reportJobId));
      }
      return status != ReportJobStatus.COMPLETED;
    }

    protected override ReportResponse GetReport() {
      PreconditionUtilities.CheckNotNull(reportDownloadOptions,
          "reportDownloadOptions cannot be null");
      WebResponse response = null;
      WebRequest request = BuildRequest(reportService.getReportDownloadUrlWithOptions(
          reportJobId, reportDownloadOptions));
      response = request.GetResponse();
      return new ReportResponse(response);
    }

    /// <summary>
    /// Builds an HTTP request for downloading reports.
    /// </summary>
    /// <param name="downloadUrl">The download url.</param>
    /// <returns></returns>
    private WebRequest BuildRequest(string downloadUrl) {
      return HttpUtilities.BuildRequest(downloadUrl, "GET", this.reportService.User.Config);
    }
  }
}

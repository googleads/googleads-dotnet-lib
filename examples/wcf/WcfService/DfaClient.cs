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


using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_19;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.Dfa.Examples.Wcf {
  /// <summary>
  /// A class that makes calls to DFA API.
  /// </summary>
  public class DfaClient {
    /// <summary>
    /// Time to wait between checks for report status.
    /// </summary>
    private const int TIME_BETWEEN_CHECKS = 10 * 60 * 1000;

    /// <summary>
    /// The dfa user making calls.
    /// </summary>
    DfaUser user = new DfaUser();

    /// <summary>
    /// Gets the ad types.
    /// </summary>
    /// <returns></returns>
    internal AdType[] GetAdTypes() {
       // Create AdRemoteService instance.
      AdRemoteService service = (AdRemoteService) user.GetService(
          DfaService.v1_19.AdRemoteService);

      // Get ad types.
      return service.getAdTypes();
    }

    /// <summary>
    /// Schedules and downloads a report given a query id.
    /// </summary>
    /// <param name="queryId">The query id.</param>
    /// <param name="reportFilePath">The path to which the report should be
    /// downloaded.</param>
    /// <returns>True, if the report was downloaded successfully, false
    /// otherwise.</returns>
    internal bool GetReport(long queryId, string reportFilePath) {
      // Create ReportRemoteService instance.
      ReportRemoteService service = (ReportRemoteService)user.GetService(
          DfaService.v1_19.ReportRemoteService);
      return ScheduleAndDownloadReport(service, queryId, reportFilePath);
    }

    /// <summary>
    /// Schedules and downloads a report.
    /// </summary>
    /// <param name="service">The report service instance.</param>
    /// <param name="queryId">The query id to be used for generating reports.
    /// </param>
    /// <param name="reportFilePath">The file path to which the downloaded report
    /// should be saved.</param>
    private bool ScheduleAndDownloadReport(ReportRemoteService service, long queryId,
        string reportFilePath) {
      // Create report request and submit it to the server.
      ReportRequest reportRequest = new ReportRequest();
      reportRequest.queryId = queryId;

      ReportInfo reportInfo = service.runDeferredReport(reportRequest);
      long reportId = reportInfo.reportId;
      reportRequest.reportId = reportId;

      while (reportInfo.status.name != "COMPLETE") {
        Thread.Sleep(TIME_BETWEEN_CHECKS);
        reportInfo = service.getReport(reportRequest);
        if (reportInfo.status.name == "ERROR") {
          throw new Exception("Deferred report failed with errors. Run in the UI to " +
              "troubleshoot.");
        }
      }

      FileStream fs = null;
      try {
        fs = File.OpenWrite(reportFilePath);
        byte[] data = MediaUtilities.GetAssetDataFromUrl(reportInfo.url);
        fs.Write(data, 0, data.Length);
        return true;
      } catch {
        return false;
      } finally {
        if (fs != null) {
          fs.Close();
        }
      }
    }
  }
}

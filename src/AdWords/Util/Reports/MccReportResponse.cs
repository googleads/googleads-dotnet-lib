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

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Google.Api.Ads.AdWords.Util {
  /// <summary>
  /// Represents a report response from the reports server when downloading an
  /// MCC report.
  /// </summary>
  class MccReportResponse {
    /// <summary>
    /// The HTTP status code from the server.
    /// </summary>
    private HttpStatusCode statusCode;

    /// <summary>
    /// The status of this report.
    /// </summary>
    private MccReportStatus reportStatus;

    /// <summary>
    /// The query token for the next request.
    /// </summary>
    private string queryToken;

    /// <summary>
    /// The report download url, if the report has been generated successfully.
    /// </summary>
    private string followupUrl;

    /// <summary>
    /// Gets or sets the HTTP status code.
    /// </summary>
    public HttpStatusCode StatusCode {
      get {
        return statusCode;
      }
      set {
        statusCode = value;
      }
    }

    /// <summary>
    /// Gets or sets the report status.
    /// </summary>
    public MccReportStatus ReportStatus {
      get {
        return reportStatus;
      }
      set {
        reportStatus = value;
      }
    }

    /// <summary>
    /// Gets or sets the query token.
    /// </summary>
    public string QueryToken {
      get {
        return queryToken;
      }
      set {
        queryToken = value;
      }
    }

    /// <summary>
    /// Gets or sets the followup URL.
    /// </summary>
    public string FollowupUrl {
      get {
        return followupUrl;
      }
      set {
        followupUrl = value;
      }
    }
  }
}

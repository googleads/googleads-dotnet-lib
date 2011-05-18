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
using System.IO;
using System.Text;

namespace Google.Api.Ads.AdWords.Util {
  /// <summary>
  /// Represents a response from the report server that updates the
  /// caller about the status of the asynchronous report being downloaded.
  /// </summary>
  public class MccReportStatus {
    /// <summary>
    /// Represents the state the asynchronous report is in.
    /// </summary>
    public enum State {
      /// <summary>
      /// The request is still being processed. Pause 30 seconds and retry the
      /// request.
      /// </summary>
      RUNNING,

      /// <summary>
      /// The query has completed. You can retrieve the report from the URL
      /// identified in the Location header.
      /// </summary>
      SUCCESS,

      /// <summary>
      /// A user or system error has occurred. Check the failureReason for an
      /// explanation.
      /// </summary>
      FAILURE,

      /// <summary>
      /// The server encountered a problem when parsing the report definition.
      /// </summary>
      VALIDATION_FAILURE,

      /// <summary>
      /// You have tried to query too many different client accounts or created
      /// too many reports in a short time.
      /// </summary>
      RATE_EXCEEDED,

      /// <summary>
      /// The query token that you passed to the server is invalid.
      /// </summary>
      INVALID_TOKEN,

      /// <summary>
      /// The qt parameter was missing from the HTTP request.
      /// </summary>
      MISSING_QUERY_TOKEN
    }

    /// <summary>
    /// Represents an account for which report generation failed.
    /// </summary>
    public class Account {
      /// <summary>
      /// The account id.
      /// </summary>
      string idField;

      /// <summary>
      /// Gets or sets the account id.
      /// </summary>
      public string id {
        get {
          return idField;
        }
        set {
          idField = value;
        }
      }
    }

    /// <summary>
    /// The query token to be used in the next call.
    /// </summary>
    private string queryTokenField;

    /// <summary>
    /// The state the asynchronous report is in.
    /// </summary>
    private State stateField;

    /// <summary>
    /// The total number of accounts touched when generating this report.
    /// </summary>
    private int totalField;

    /// <summary>
    /// The number of accounts for which reports were generated successfully.
    /// </summary>
    private int successField;

    /// <summary>
    /// The number of accounts for which report generation failed.
    /// </summary>
    private int failField;

    /// <summary>
    /// The failure reason for this report.
    /// </summary>
    private string failureReasonField;

    /// <summary>
    /// The list of accounts for which failures were encountered when running
    /// this report.
    /// </summary>
    List<Account> failuresField = new List<Account>();

    /// <summary>
    /// Gets or sets the query token to be used in the next call.
    /// </summary>
    public string queryToken {
      get {
        return queryTokenField;
      }
      set {
        queryTokenField = value;
      }
    }

    /// <summary>
    /// Gets or sets the state the asynchronous report is in.
    /// </summary>
    public State state {
      get {
        return stateField;
      }
      set {
        stateField = value;
      }
    }

    /// <summary>
    /// The total number of accounts touched when generating this report.
    /// </summary>
    public int total {
      get {
        return totalField;
      }
      set {
        totalField = value;
      }
    }

    /// <summary>
    /// The number of accounts for which reports were generated successfully.
    /// </summary>
    public int success {
      get {
        return successField;
      }
      set {
        successField = value;
      }
    }

    /// <summary>
    /// The number of accounts for which report generation failed.
    /// </summary>
    public int fail {
      get {
        return failField;
      }
      set {
        failField = value;
      }
    }

    /// <summary>
    /// Gets or sets the failure reason for this report.
    /// </summary>
    public string failureReason {
      get {
        return failureReasonField;
      }
      set {
        failureReasonField = value;
      }
    }

    /// <summary>
    /// Gets the list of accounts for which failures were encountered when
    /// running this report.
    /// </summary>
    public List<Account> failures {
      get {
        return failuresField;
      }
    }
  }
}

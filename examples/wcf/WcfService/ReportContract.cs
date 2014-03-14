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
using System.Runtime.Serialization;

namespace Google.Api.Ads.Dfa.Examples.Wcf {
  /// <summary>
  /// Defines a data contract for IWcfService.GetReportUsingContract.
  /// </summary>
  [DataContract]
  public class ReportContract {
    /// <summary>
    /// The query id for which report should be generated.
    /// </summary>
    long queryId;

    /// <summary>
    /// The path to which report should be saved.
    /// </summary>
    string reportFilePath;

    /// <summary>
    /// True, if the download was successful, false otherwise.
    /// </summary>
    Boolean isSuccess;

    /// <summary>
    /// Gets or sets if the download was successful or not.
    /// </summary>
    [DataMember]
    public Boolean IsSuccess {
      get {
        return isSuccess;
      }
      set {
        isSuccess = value;
      }
    }

    /// <summary>
    /// Gets or sets the query id for which report should be generated.
    /// </summary>
    [DataMember]
    public long QueryId {
      get {
        return queryId;
      }
      set {
        queryId = value;
      }
    }

    /// <summary>
    /// Gets or sets the path to which report should be saved.
    /// </summary>
    [DataMember]
    public string ReportFilePath {
      get {
        return reportFilePath;
      }
      set {
        reportFilePath = value;
      }
    }
  }
}

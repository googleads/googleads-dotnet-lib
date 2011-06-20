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

using Google.Api.Ads.Dfa.v1_14;

using System;
using System.ServiceModel;

namespace Google.Api.Ads.Dfa.Examples.Wcf {
  /// <summary>
  /// A public interface for calling the methods exposed by this service.
  /// </summary>
  [ServiceContract]
  public interface IWcfService {
    /// <summary>
    /// Schedules and downloads a report given a query id.
    /// </summary>
    /// <param name="queryId">The query id.</param>
    /// <param name="reportFilePath">The path to which the report should be
    /// downloaded.</param>
    /// <returns>True, if the report was downloaded successfully, false
    /// otherwise.</returns>
    [OperationContract]
    bool GetReport(long queryId, string reportFilePath);

    /// <summary>
    /// Schedules and downloads a report given a query id.
    /// </summary>
    /// <param name="contract">The report contract.</param>
    /// <returns>The report contract, with report contents populated.</returns>
    [OperationContract]
    ReportContract GetReportUsingContract(ReportContract contract);

    /// <summary>
    /// Gets the ad types supported by DFA.
    /// </summary>
    /// <returns>A list of ad types.</returns>
    [OperationContract]
    AdType[] GetAdTypes();

    /// <summary>
    /// Gets the ad types supported by DFA.
    /// </summary>
    /// <param name="contract">The adtype contract.</param>
    /// <returns>The adtype contract, with ad types popupated.</returns>
    [OperationContract]
    AdTypeContract GetAdTypesUsingContract(AdTypeContract contract);
  }
}

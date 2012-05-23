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

using Google.Api.Ads.Dfa.v1_17;

namespace Google.Api.Ads.Dfa.Examples.Wcf {
  /// <summary>
  /// Implements the service functionalities.
  /// </summary>
  public class WcfService : IWcfService {
    /// <summary>
    /// The DFA client that makes calls to the server.
    /// </summary>
    DfaClient client = new DfaClient();

    /// <summary>
    /// Schedules and downloads a report given a query id.
    /// </summary>
    /// <param name="queryId">The query id.</param>
    /// <param name="reportFilePath">The path to which the report should be
    /// downloaded.</param>
    /// <returns>True, if the report was downloaded successfully, false
    /// otherwise.</returns>
    public bool GetReport(long queryId, string reportFilePath) {
      return client.GetReport(queryId, reportFilePath);
    }

    /// <summary>
    /// Schedules and downloads a report given a query id.
    /// </summary>
    /// <param name="contract">The report contract.</param>
    /// <returns>
    /// The report contract, with report contents populated.
    /// </returns>
    public ReportContract GetReportUsingContract(ReportContract contract) {
      contract.IsSuccess = GetReport(contract.QueryId, contract.ReportFilePath);
      return contract;
    }

    /// <summary>
    /// Gets the ad types supported by DFA.
    /// </summary>
    /// <returns>
    /// A list of ad types.
    /// </returns>
    public AdType[] GetAdTypes() {
      return client.GetAdTypes();
    }

    /// <summary>
    /// Gets the ad types supported by DFA.
    /// </summary>
    /// <param name="contract">The adtype contract.</param>
    /// <returns>
    /// The adtype contract, with ad types popupated.
    /// </returns>
    public AdTypeContract GetAdTypesUsingContract(AdTypeContract contract) {
      contract.AdTypes = GetAdTypes();
      return contract;
    }
  }
}

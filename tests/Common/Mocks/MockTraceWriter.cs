// Copyright 2017, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Logging;

namespace Google.Api.Ads.Common.Tests.Mocks {

  class MockTraceWriter : ITraceWriter {
    internal string SummaryLog { get; set; }
    internal bool IsSummaryFailure { get; set; }

    internal string DetailedLog { get; set; }
    internal bool IsDetailedLogFailure { get; set; }

    void ITraceWriter.WriteDetailedRequestLogs(string message, bool isFailure) {
      this.DetailedLog = message;
      this.IsDetailedLogFailure = isFailure;
    }

    void ITraceWriter.WriteSummaryRequestLogs(string message, bool isFailure) {
      this.SummaryLog = message;
      this.IsSummaryFailure = isFailure;
    }
  }
}

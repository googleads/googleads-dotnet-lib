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

namespace Google.Api.Ads.Common.Logging
{
    /// <summary>
    /// Interface for SOAP trace log writers.
    /// </summary>
    public interface ITraceWriter
    {
        /// <summary>
        /// Writes detailed logs.
        /// </summary>
        /// <param name="message">The log content to write</param>
        /// <param name="isFailure">If the log is for a failed request.</param>
        void WriteDetailedRequestLogs(string message, bool isFailure);

        /// <summary>
        /// Writes summary logs.
        /// </summary>
        /// <param name="message">The log content to write</param>
        /// <param name="isFailure">If the log is for a failed request.</param>
        void WriteSummaryRequestLogs(string message, bool isFailure);
    }
}

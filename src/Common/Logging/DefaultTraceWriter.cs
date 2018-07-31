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

using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Common.Logging
{
    /// <summary>
    /// Default instance of TraceWriter, which just delegates to TraceUtilities.
    /// </summary>
    public class DefaultTraceWriter : ITraceWriter
    {
        void ITraceWriter.WriteDetailedRequestLogs(string message, bool isFailure)
        {
            TraceUtilities.WriteDetailedRequestLogs(message, isFailure);
        }

        void ITraceWriter.WriteSummaryRequestLogs(string message, bool isFailure)
        {
            TraceUtilities.WriteSummaryRequestLogs(message, isFailure);
        }
    }
}

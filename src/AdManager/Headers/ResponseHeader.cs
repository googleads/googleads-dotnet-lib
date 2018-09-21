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

using System.Runtime.Serialization;

namespace Google.Api.Ads.AdManager.Headers
{
    /// <summary>
    /// SOAP Response header for DFP API services.
    /// </summary>
    [DataContract(Name = "ResponseHeader", Namespace = PLACEHOLDER_NAMESPACE)]
    public class ResponseHeader
    {
        /// <summary>
        /// A placeholder namespace for deserializing response headers from different API versions.
        /// </summary>
        public const string PLACEHOLDER_NAMESPACE =
            "https://www.google.com/ads/api/publisher/version";

        /// <summary>
        /// Gets or sets the request id for this API call.
        /// </summary>
        [DataMember(Order = 0)]
        public string requestId { get; set; }

        /// <summary>
        /// Gets or sets the response time for this API call.
        /// </summary>
        [DataMember(Order = 1)]
        public long responseTime { get; set; }
    }
}

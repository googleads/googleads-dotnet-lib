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

using System.Net;

namespace Google.Api.Ads.Common.Logging
{
    /// <summary>
    /// Stores the details of an HTTP response being logged.
    /// </summary>
    public class ResponseInfo
    {
        /// <summary>
        /// The HTTP response headers.
        /// </summary>
        public WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// The HTTP response body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The HTTP status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The ID of the preceeding request.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// The count of operations included in the preceeding request.
        /// </summary>
        public long OperationCount { get; set; }

        /// <summary>
        /// The response time in milliseconds.
        /// </summary>
        public long ResponseTimeMs { get; set; }

        /// <summary>
        /// The error message associated with this response.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseInfo"/> class.
        /// </summary>
        public ResponseInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseInfo"/> class.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="body">The HTTP response body.</param>
        public ResponseInfo(WebResponse response, string body)
        {
            this.Headers = response != null ? response.Headers : new WebHeaderCollection();
            this.Body = body;
        }
    }
}

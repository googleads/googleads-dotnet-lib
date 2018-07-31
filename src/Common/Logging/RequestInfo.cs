// Copyright 2014, Google Inc. All Rights Reserved.
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

using System;
using System.Net;

namespace Google.Api.Ads.Common.Logging
{
    /// <summary>
    /// Stores the details of an HTTP request being logged.
    /// </summary>
    public class RequestInfo
    {
        /// <summary>
        /// The request URI.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// The HTTP request method.
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// The HTTP request headers.
        /// </summary>
        public WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// The HTTP request body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The service being requested.
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// The method being requested.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// The identifier associated with this request.
        /// </summary>
        public string IdentifierName { get; set; }

        /// <summary>
        /// The value of the identifier associated with this request.
        /// </summary>
        public string IdentifierValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestInfo"/> class.
        /// </summary>
        public RequestInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestInfo"/> class.
        /// </summary>
        /// <param name="request">The HTTP request being logged.</param>
        /// <param name="body">The HTTP request body.</param>
        public RequestInfo(WebRequest request, string body)
        {
            this.Uri = request.RequestUri;
            this.HttpMethod = request.Method;
            this.Headers = request.Headers;
            this.Body = body;
        }
    }
}

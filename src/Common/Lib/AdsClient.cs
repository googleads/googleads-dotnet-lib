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

using System;
using System.Net;

using Google.Api.Ads.Common.Lib;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// This interface defines a client protocol (SOAP, WSE, REST, etc.)
    /// supported by the library.
    /// </summary>
    public interface AdsClient
    {
        /// <summary>
        /// The timeout for the request.
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// The url endpoint for the service.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets whether gzip compression is enabled.
        /// </summary>
        bool EnableDecompression { get; set; }

        /// <summary>
        /// Gets or sets proxy information for making a service request through
        /// a firewall.
        /// </summary>
        IWebProxy Proxy { get; set; }

        /// <summary>
        /// Gets or sets the value for the user agent header that is sent with each
        /// request.
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the AdsUser object that created this
        /// service.
        /// </summary>
        AdsUser User { get; set; }

        /// <summary>
        /// Gets or sets the signature for this service.
        /// </summary>
        ServiceSignature Signature { get; set; }
    }
}

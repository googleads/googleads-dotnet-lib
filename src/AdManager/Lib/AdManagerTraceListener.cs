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

using Google.Api.Ads.Common.Logging;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Lib
{
    /// <summary>
    /// Listens to SOAP messages sent and received by this library.
    /// </summary>
    public class AdManagerTraceListener : TraceListener
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        protected static AdManagerTraceListener instance = new AdManagerTraceListener();

        /// <summary>
        /// Protected constructor.
        /// </summary>
        protected AdManagerTraceListener() : base(new AdManagerAppConfig())
        {
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static SoapListener Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Parses the body of the request and populates fields in the request info.
        /// </summary>
        /// <param name="info">The request info for this SOAP call.</param>
        protected override void PopulateRequestInfo(ref RequestInfo info)
        {
            base.PopulateRequestInfo(ref info);

            // Set the network code.
            info.IdentifierName = "networkCode";
            info.IdentifierValue = ((AdManagerAppConfig) this.Config).NetworkCode;
        }

        /// <summary>
        /// Gets a list of fields to be masked in xml logs.
        /// </summary>
        /// <returns>The list of fields to be masked.</returns>
        protected override ISet<string> GetFieldsToMask()
        {
            return new HashSet<string>(new string[]
            {
                "authToken",
                "token",
                "Authorization"
            }, StringComparer.OrdinalIgnoreCase);
        }
    }
}

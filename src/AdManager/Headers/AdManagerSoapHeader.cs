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

using Google.Api.Ads.Common.Lib;

using System;
using System.ServiceModel.Channels;

namespace Google.Api.Ads.AdManager.Headers
{
    /// <summary>
    /// Base class for Ad Manager API Soap headers.
    /// </summary>
    public abstract class AdManagerSoapHeader : MessageHeader
    {
        /// <summary>
        /// The API version the header should be namespaced to.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public override string Namespace
        {
            get { return "https://www.google.com/apis/ads/publisher/" + Version; }
        }
    }
}

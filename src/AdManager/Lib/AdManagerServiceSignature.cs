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
using System.Globalization;

namespace Google.Api.Ads.AdManager.Lib
{
    /// <summary>
    /// Service creation params for DFP API family of services.
    /// </summary>
    public class AdManagerServiceSignature : ServiceSignature
    {
        /// <summary>
        /// Gets the type of service.
        /// </summary>
        public override Type ServiceType
        {
            get
            {
                return Type.GetType(string.Format(CultureInfo.InvariantCulture,
                    "Google.Api.Ads.AdManager.{0}.{1}", Version, ServiceName));
            }
        }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="version">Service version.</param>
        /// <param name="serviceName">Service name.</param>
        public AdManagerServiceSignature(string version, string serviceName)
            : base(version, serviceName, SupportedProtocols.SOAP)
        {
        }
    }
}

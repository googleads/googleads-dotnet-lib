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
using Google.Api.Ads.Common.Logging;

using System;
using System.Linq;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Lib
{
    /// <summary>
    /// Represents an Ad Manager API user.
    /// </summary>
    public partial class AdManagerUser : AdsUser
    {
        /// <summary>
        /// Public constructor. Use this version if you want the library to
        /// use all settings from App.config.
        /// </summary>
        public AdManagerUser() : base(new AdManagerAppConfig())
        {
        }

        /// <summary>
        /// Parameterized constructor. Use this version if you want to construct
        /// a AdManagerUser with a custom set of headers.
        /// </summary>
        /// <param name="headers">The custom set of headers.</param>
        public AdManagerUser(Dictionary<string, string> headers) : base(new AdManagerAppConfig(),
            headers)
        {
        }

        /// <summary>
        /// Public constructor. Use this version if you want to construct
        /// a AdManagerUser with a custom configuration.
        /// </summary>
        public AdManagerUser(AdManagerAppConfig config) : base(config)
        {
        }

        /// <summary>
        /// Gets all the service types to be registered against this user.
        /// </summary>
        /// <returns>The type of all service classes to be registered.</returns>
        public override Type[] GetServiceTypes()
        {
            return new AdManagerService().GetServiceTypes();
        }

        /// <summary>
        /// Gets the default listeners.
        /// </summary>
        /// <returns>A list of default listeners</returns>
        public override SoapListener[] GetDefaultListeners()
        {
            return new SoapListener[]
            {
                AdManagerTraceListener.Instance
            };
        }

        /// <summary>
        /// Creates a service of the given type.
        /// </summary>
        public T GetService<T>() where T : AdsClient
        {
            Type serviceType = typeof(T);
            string version = serviceType.Namespace.Split('.').Last();
            AdManagerServiceSignature serviceSignature =
                new AdManagerServiceSignature(version, serviceType.Name);
            return (T) GetService(serviceSignature);
        }
    }
}

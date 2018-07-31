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
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// Interface to a factory which can create a particular group of services.
    /// For every new service supported, you need an implementation of this
    /// interface.
    /// </summary>
    public abstract class ServiceFactory : Configurable
    {
        /// <summary>
        /// An App.config reader suitable for this factory.
        /// </summary>
        private AppConfig config;

        /// <summary>
        /// Gets an App.config reader suitable for this factory.
        /// </summary>
        public AppConfig Config
        {
            get { return config; }
            set
            {
                config = value;
                config.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
                {
                    ReadHeadersFromConfig(config);
                };
                ReadHeadersFromConfig(config);
            }
        }

        /// <summary>
        /// Create a service object.
        /// </summary>
        /// <param name="signature">Signature of the service being created.</param>
        /// <param name="user">The user for which the service is being created.</param>
        /// <param name="serverUrl">The server to which the API calls should be
        /// made.</param>
        /// <returns>An object of the desired service type.</returns>
        public abstract AdsClient CreateService(ServiceSignature signature, AdsUser user,
            Uri serverUrl);

        /// <summary>
        /// Reads the headers from App.config.
        /// </summary>
        /// <param name="config">The configuration class.</param>
        protected abstract void ReadHeadersFromConfig(AppConfig config);

        /// <summary>
        /// Checks preconditions of the service signature and throws and exception if the service
        /// cannot be generated.
        /// </summary>
        /// <param name="signature">the service signature for generating the service</param>
        protected abstract void CheckServicePreconditions(ServiceSignature signature);
    }
}

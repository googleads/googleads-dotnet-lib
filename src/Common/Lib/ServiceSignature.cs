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
using System.Text;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// Defines an API signature object. This class is used as a support class
    /// to assist AdsUser in creating a service object.
    /// </summary>
    public abstract class ServiceSignature
    {
        /// <summary>
        /// The various protocols supported by the library.
        /// </summary>
        public enum SupportedProtocols
        {
            /// <summary>
            /// SOAP
            /// </summary>
            SOAP,

            /// <summary>
            /// WSE
            /// </summary>
            WSE
        }

        /// <summary>
        /// The supported protocol.
        /// </summary>
        SupportedProtocols supportedProtocol;

        /// <summary>
        /// The service version.
        /// </summary>
        private string version;

        /// <summary>
        /// The name of the service.
        /// </summary>
        private string serviceName;

        /// <summary>
        /// A unique id to distinguish the service represented by this signature
        /// object.
        /// </summary>
        public virtual string Id
        {
            get { return version + "." + serviceName; }
        }

        /// <summary>
        /// Gets the service version.
        /// </summary>
        public string Version
        {
            get { return version; }
        }

        /// <summary>
        /// Gets the service name.
        /// </summary>
        public string ServiceName
        {
            get { return serviceName; }
        }

        /// <summary>
        /// Gets the supported protocol.
        /// </summary>
        public SupportedProtocols SupportedProtocol
        {
            get { return supportedProtocol; }
        }

        /// <summary>
        /// Gets the type of service.
        /// </summary>
        public abstract Type ServiceType { get; }

        /// <summary>
        /// Protected constructor.
        /// </summary>
        /// <param name="version">Service version.</param>
        /// <param name="serviceName">Service name.</param>
        /// <param name="protocol">The supported protocol.</param>
        protected ServiceSignature(string version, string serviceName, SupportedProtocols protocol)
        {
            this.version = version;
            this.serviceName = serviceName;
            this.supportedProtocol = protocol;
        }
    }
}

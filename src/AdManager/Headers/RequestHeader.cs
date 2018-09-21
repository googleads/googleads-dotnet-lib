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
using System.Xml;
using System.ServiceModel.Channels;


namespace Google.Api.Ads.AdManager.Headers
{
    /// <summary>
    /// Soap Request header for DFP API services.
    /// </summary>
    public class RequestHeader : AdManagerSoapHeader, ICloneable
    {
        /// <summary>
        /// The name of the element to be used when serializing.
        /// </summary>
        public override string Name
        {
            get { return "RequestHeader"; }
        }

        /// <summary>
        /// Gets or sets the network code.
        /// </summary>
        public string networkCode { get; set; }

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        public string applicationName { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public object Clone()
        {
            return new RequestHeader()
            {
                networkCode = this.networkCode,
                applicationName = this.applicationName,
                Version = this.Version
            };
        }

        /// <summary>
        /// Serlalizes the RequestHeader for the SOAP XML request.
        /// </summary>
        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer,
            MessageVersion messageVersion)
        {
            writer.WriteElementString("networkCode", networkCode);
            writer.WriteElementString("applicationName", applicationName);
        }
    }
}

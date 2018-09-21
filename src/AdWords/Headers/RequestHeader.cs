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
using System.ServiceModel.Channels;
using System.Xml;

namespace Google.Api.Ads.AdWords.Headers
{
    /// <summary>
    /// This class represents an AdWords SOAP request header.
    /// </summary>
    public class RequestHeader : MessageHeader, ICloneable
    {
        /// <summary>
        /// The namespace template for various header nodes.
        /// </summary>
        private const string NAMESPACE_TEMPLATE = "https://adwords.google.com/api/adwords/{0}/{1}";

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public string ChildNamespace
        {
            get { return String.Format(NAMESPACE_TEMPLATE, "cm", Version); }
        }

        /// <summary>
        /// Gets or sets the service namespace (e.g. cm, o, info, etc.).
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public override string Namespace
        {
            get { return String.Format(NAMESPACE_TEMPLATE, GroupName, Version); }
        }

        /// <summary>
        /// Gets the name of this header.
        /// </summary>
        public override string Name
        {
            get { return "RequestHeader"; }
        }

        /// <summary>
        /// Gets or sets the client customer id.
        /// </summary>
        public string clientCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the developer token.
        /// </summary>
        public string developerToken { get; set; }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        public string userAgent { get; set; }

        /// <summary>
        /// Gets or sets whether this API call is for validation only.
        /// </summary>
        public bool? validateOnly { get; set; }

        /// <summary>
        /// Gets or sets whether partial failures should be returned.
        /// </summary>
        public bool? partialFailure { get; set; }

        /// <summary>
        /// Called when the header content is serialized using the specified XML writer.
        /// </summary>
        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer,
            MessageVersion messageVersion)
        {
            if (!string.IsNullOrEmpty(clientCustomerId))
            {
                writer.WriteElementString("clientCustomerId", ChildNamespace, clientCustomerId);
            }

            writer.WriteElementString("developerToken", ChildNamespace, developerToken);
            writer.WriteElementString("userAgent", ChildNamespace, userAgent);
            if (validateOnly.HasValue)
            {
                writer.WriteElementString("validateOnly", ChildNamespace,
                    validateOnly.ToString().ToLower());
            }

            if (partialFailure.HasValue)
            {
                writer.WriteElementString("partialFailure", ChildNamespace,
                    partialFailure.ToString().ToLower());
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

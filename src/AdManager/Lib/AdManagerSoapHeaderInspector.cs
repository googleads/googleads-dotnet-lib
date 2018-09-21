// Copyright 2017, Google Inc. All Rights Reserved.
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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using Google.Api.Ads.AdManager.Headers;

using System.Xml;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

using Google.Api.Ads.Common.Logging;

namespace Google.Api.Ads.AdManager.Lib
{
    /// <summary>
    /// Inspector that adds a SOAP request header to outbound requests.
    /// </summary>
    public class AdManagerSoapHeaderInspector : IClientMessageInspector
    {
        /// <summary>
        /// Gets or sets the SOAP request header.
        /// </summary>
        /// <value>The request header.</value>
        public RequestHeader RequestHeader { get; set; }

        /// <summary>
        /// Gets or sets the SOAP response header
        /// </summary>
        public ResponseHeader ResponseHeader { get; set; }

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The config.</value>
        public AdManagerAppConfig Config { get; set; }

        void Validate()
        {
            if (Config == null)
            {
                // TODO (cseeley): Add a a message to AdManagerErrorMessages for this.
                throw new AdManagerApiException(null, "Config cannot be null");
            }

            if (RequestHeader == null)
            {
                // TODO (cseeley): Add a a message to AdManagerErrorMessages for this.
                throw new AdManagerApiException(null, "RequestHeader cannot be null");
            }

            if (string.IsNullOrWhiteSpace(Config.ApplicationName) ||
                Config.ApplicationName.Contains(AdManagerAppConfig.DEFAULT_APPLICATION_NAME))
            {
                throw new AdManagerApiException(null,
                    AdManagerErrorMessages.RequireValidApplicationName);
            }
        }

        /// <summary>
        /// Validates and applies the Header to the SOAP request.
        /// </summary>
        /// <param name="request">The request to add the Header to</param>
        /// <param name="channel">The channel to the SOAP service</param>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Validate();
            // Get the latest user agent.
            RequestHeader.applicationName = Config.GetUserAgent();
            // WCF adds an Action header that is irrelevant to DFP. The only SOAP header needed
            // is the RequestHeader.
            request.Headers.Clear();
            request.Headers.Add(RequestHeader);
            return null;
        }

        /// <summary>
        /// Performs any operations after receiving the SOAP response.
        /// </summary>
        /// <param name="reply">The response Message</param>
        /// <param name="correlationState">The </param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (reply.Headers.Count > 0)
            {
                // DataContract is strict with namespacing. Change the namespace to be the same
                // as the DataContract attribute.
                XmlReader reader = reply.Headers.GetReaderAtHeader(0);
                String ns = reader.NamespaceURI;
                String headerText = reader.ReadOuterXml();
                headerText = headerText.Replace(ns, ResponseHeader.PLACEHOLDER_NAMESPACE);
                XmlObjectSerializer ser = new DataContractSerializer(typeof(ResponseHeader));
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(headerText)))
                {
                    ResponseHeader = (ResponseHeader) ser.ReadObject(stream);
                }
            }

            AdsFeatureUsageRegistry.Instance.Clear();
        }
    }
}

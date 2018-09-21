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

using Google.Api.Ads.AdWords.Headers;
using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace Google.Api.Ads.AdWords.Lib
{
    /// <summary>
    /// Inspector that adds a SOAP request header to outbound requests
    /// and records API operation counts.
    /// </summary>
    public class AdWordsSoapHeaderInspector : IClientMessageInspector
    {
        /// <summary>
        /// Gets or sets the SOAP request header.
        /// </summary>
        /// <value>The request header.</value>
        public RequestHeader RequestHeader { get; set; }

        /// <summary>
        /// Gets or sets the SOAP response header.
        /// </summary>
        public ResponseHeader ResponseHeader { get; set; }

        /// <summary>
        /// Gets or sets the user making the requests.
        /// </summary>
        /// <value>The user.</value>
        public AdWordsUser User { get; set; }

        private void Validate()
        {
            // TODO (cseeley): add real error messages
            if (RequestHeader == null)
            {
                throw new ArgumentNullException("Header is null");
            }

            if (this.User == null)
            {
                throw new ArgumentNullException("User is null");
            }

            if (string.IsNullOrEmpty(RequestHeader.developerToken))
            {
                throw new ArgumentNullException(AdWordsErrorMessages.DeveloperTokenCannotBeEmpty);
            }

            if (string.IsNullOrEmpty(RequestHeader.clientCustomerId))
            {
                TraceUtilities.WriteGeneralWarnings(AdWordsErrorMessages.ClientCustomerIdIsEmpty);
            }
        }

        /// <summary>
        /// Validates and applies the Header to the SOAP request.
        /// </summary>
        /// <param name="request">The message to be sent to the service</param>
        /// <param name="channel">The WCF client object channel</param>
        /// <returns>The object to be used as the correlationState argument of the AfterReceiveReply
        /// method. This is null if no correlation state is used.</returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Validate();
            // Get the latest user agent with latest feature usage registry.
            RequestHeader.userAgent = User.Config.GetUserAgent();
            // WCF adds an Action header that is irrelevant to AdWords. The only header needed
            // is the RequestHeader.
            request.Headers.Clear();
            request.Headers.Add(RequestHeader);
            return null;
        }

        /// <summary>
        /// Performs any operations after receiving the SOAP response.
        /// </summary>
        /// <param name="reply">The message to be transformed into types
        /// and handed back to the client application.</param>
        /// <param name="correlationState">
        /// Correlation state data returned by BeforeSendRequest
        /// </param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (reply.Headers.Count > 0)
            {
                XmlReader reader = reply.Headers.GetReaderAtHeader(0);
                ResponseHeader = ResponseHeader.ReadFrom(reader, RequestHeader.Namespace);

                ApiCallEntry entry = new ApiCallEntry()
                {
                    OperationCount = (int) ResponseHeader.operations,
                    Method = ResponseHeader.methodName,
                    Service = ResponseHeader.serviceName
                };

                this.User.AddCallDetails(entry);
            }

            AdsFeatureUsageRegistry.Instance.Clear();
        }
    }
}

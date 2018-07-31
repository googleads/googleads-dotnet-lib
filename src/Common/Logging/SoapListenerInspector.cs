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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util;

using System;
using System.Text;
using System.Xml;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Google.Api.Ads.Common.Logging
{
    /// <summary>
    /// Define a SOAP Extension that traces the SOAP request and SOAP response
    /// for the XML Web service method the SOAP extension is applied to.
    /// </summary>
    public class SoapListenerInspector : IClientMessageInspector
    {
        private static readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            Encoding = Encoding.UTF8,
            Indent = true,
            OmitXmlDeclaration = true
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SoapListenerInspector"/>
        /// class.
        /// </summary>
        public SoapListenerInspector(AdsUser user, string serviceName)
        {
            this.user = user;
            this.serviceName = serviceName;
        }

        /// <summary>
        /// The RequestInfo to log.
        /// </summary>
        private RequestInfo requestInfo;

        /// <summary>
        /// The ResponseInfo to log.
        /// </summary>
        private ResponseInfo responseInfo;

        /// <summary>
        /// The AdsUser to call listeners for.
        /// </summary>
        private AdsUser user;

        /// <summary>
        /// The name of the service this listener is applied to.
        /// </summary>
        private string serviceName;

        /// <summary>
        /// Gets the message body as a string.
        /// </summary>
        /// <returns>The message body.</returns>
        /// <param name="message">Message.</param>
        private string GetMessageBody(ref Message message)
        {
            using (MessageBuffer buffer = message.CreateBufferedCopy(Int32.MaxValue))
            {
                // Message can only be read once, so replace it with a copy.
                message = buffer.CreateMessage();
                StringBuilder sb = new StringBuilder();
                try
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(sb, xmlWriterSettings))
                    {
                        buffer.CreateMessage().WriteMessage(xmlWriter);
                    }

                    return sb.ToString();
                }
                catch (Exception e)
                {
                    TraceUtilities.WriteGeneralWarnings(
                        string.Format(CommonErrorMessages.MalformedSoap, e));
                    return "<soap />";
                }
            }
        }

        /// <summary>
        /// Logs the request information.
        /// </summary>
        /// <param name="request">The message to be sent to the service</param>
        /// <param name="channel">The WCF client object channel</param>
        /// <returns>The object to be used as the correlationState argument of the AfterReceiveReply
        /// method. This is null if no correlation state is used.</returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            object httpProp;
            if (!request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpProp))
            {
                httpProp = new HttpRequestMessageProperty();
                request.Properties.Add(HttpRequestMessageProperty.Name, httpProp);
            }

            HttpRequestMessageProperty requestProperties = (HttpRequestMessageProperty) httpProp;
            this.requestInfo = new RequestInfo()
            {
                Headers = requestProperties.Headers,
                Body = GetMessageBody(ref request),
                HttpMethod = requestProperties.Method,
                Uri = channel.RemoteAddress.Uri,
                Service = this.serviceName
            };
            return null;
        }

        /// <summary>
        /// Logs the response information.
        /// </summary>
        /// <param name="response">The message to be transformed into types
        /// and handed back to the client application.</param>
        /// <param name="correlationState">
        /// Correlation state data returned by BeforeSendRequest
        /// </param>
        public void AfterReceiveReply(ref Message response, object correlationState)
        {
            object httpProp;
            if (!response.Properties.TryGetValue(HttpResponseMessageProperty.Name, out httpProp))
            {
                httpProp = new HttpResponseMessageProperty();
                response.Properties.Add(HttpResponseMessageProperty.Name, httpProp);
            }

            HttpResponseMessageProperty responseProperties = (HttpResponseMessageProperty) httpProp;
            this.responseInfo = new ResponseInfo()
            {
                Headers = responseProperties.Headers,
                Body = GetMessageBody(ref response),
                StatusCode = responseProperties.StatusCode
            };

            this.user.CallListeners(requestInfo, responseInfo);
        }
    }
}

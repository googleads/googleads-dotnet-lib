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
using Google.Api.Ads.Common.Util;

using System.Collections.Generic;
using System.Xml;

namespace Google.Api.Ads.Common.Logging
{
    /// <summary>
    /// Listens to SOAP messages sent and received by this library.
    /// </summary>
    public abstract class TraceListener : SoapListener
    {
        /// <summary>
        /// The config class to be used with this class.
        /// </summary>
        private AppConfig config;

        /// <summary>
        /// The date and time provider.
        /// </summary>
        private DateTimeProvider dateTimeProvider;

        /// <summary>
        /// The ITraceWriter to use when writing SOAP messages.
        /// </summary>
        internal ITraceWriter TraceWriter { get; set; }

        /// <summary>
        /// Gets or sets the date and time provider.
        /// </summary>
        public DateTimeProvider DateTimeProvider
        {
            get { return dateTimeProvider; }
            set { dateTimeProvider = value; }
        }

        /// <summary>
        /// Gets the config class to be used with this class.
        /// </summary>
        public AppConfig Config
        {
            get { return config; }
        }

        /// <summary>
        /// Protected constructor.
        /// </summary>
        /// <param name="config">The config class.</param>
        protected TraceListener(AppConfig config)
        {
            this.config = config;
            this.dateTimeProvider = new DefaultDateTimeProvider();
            this.TraceWriter = new DefaultTraceWriter();
        }

        /// <summary>
        /// Initializes the listener for handling an API call.
        /// </summary>
        public void InitForCall()
        {
        }

        /// <summary>
        /// Handles the SOAP message.
        /// </summary>
        /// <param name="requestInfo">Request info.</param>
        /// <param name="responseInfo">Response info.</param>
        public virtual void HandleMessage(RequestInfo requestInfo, ResponseInfo responseInfo)
        {
            PerformLogging(requestInfo, responseInfo);
        }

        /// <summary>
        /// Cleans up any resources after an API call.
        /// </summary>
        public void CleanupAfterCall()
        {
            ContextStore.RemoveKey("SoapRequest");
            ContextStore.RemoveKey("SoapResponse");
            ContextStore.RemoveKey("FormattedSoapLog");
            ContextStore.RemoveKey("FormattedRequestLog");
        }

        /// <summary>
        /// Performs the SOAP and HTTP logging.
        /// </summary>
        /// <param name="request">The request information.</param>
        /// <param name="response">The response information.</param>
        private void PerformLogging(RequestInfo request, ResponseInfo response)
        {
            LogEntry logEntry = new LogEntry(config, dateTimeProvider, TraceWriter);

            PopulateRequestInfo(ref request);
            logEntry.LogRequest(request, GetFieldsToMask(), new SoapTraceFormatter());

            PopulateResponseInfo(ref response);
            logEntry.LogResponse(response, GetFieldsToMask(), new SoapTraceFormatter());
            logEntry.Flush();

            ContextStore.AddKey("FormattedSoapLog", logEntry.DetailedLog);
            ContextStore.AddKey("FormattedRequestLog", logEntry.SummaryLog);
        }

        /// <summary>
        /// Gets a list of fields to be masked in xml logs.
        /// </summary>
        /// <returns>The list of fields to be masked.</returns>
        protected abstract ISet<string> GetFieldsToMask();

        /// <summary>
        /// Parses the body of the request and populates fields in the request info.
        /// </summary>
        /// <param name="info">The request info for this SOAP call.</param>
        protected virtual void PopulateRequestInfo(ref RequestInfo info)
        {
            XmlDocument xDoc = XmlUtilities.CreateDocument(info.Body);
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xDoc.NameTable);
            xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

            // Retrieve method.
            XmlNode methodNode = xDoc.SelectSingleNode("soap:Envelope/soap:Body/*", xmlns);
            if (methodNode != null)
            {
                info.Method = methodNode.Name;
            }
        }

        /// <summary>
        /// Parses the body of the response and populates fields in the repsonse info.
        /// </summary>
        /// <param name="info">The response info for this SOAP call.</param>
        protected virtual void PopulateResponseInfo(ref ResponseInfo info)
        {
            XmlDocument xDoc = XmlUtilities.CreateDocument(info.Body);
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xDoc.NameTable);
            xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

            // Retrieve loggable headers.
            XmlNode headerNode = xDoc.SelectSingleNode("soap:Envelope/soap:Header/*", xmlns);
            if (headerNode != null && headerNode.Name == "ResponseHeader")
            {
                info.RequestId = RetrieveLoggableHeader(headerNode, "requestId");

                long operations;
                if (long.TryParse(RetrieveLoggableHeader(headerNode, "operations"), out operations))
                {
                    info.OperationCount = operations;
                }

                long responseTime;
                if (long.TryParse(RetrieveLoggableHeader(headerNode, "responseTime"),
                    out responseTime))
                {
                    info.ResponseTimeMs = responseTime;
                }
            }

            //Retrieve fault string (if one exists).
            XmlNode faultNode =
                xDoc.SelectSingleNode("soap:Envelope/soap:Body/soap:Fault/faultstring", xmlns);
            if (faultNode != null)
            {
                info.ErrorMessage = faultNode.InnerText;
            }
        }

        /// <summary>
        /// Returns a string containing the specified loggable headers, retrieved from the specified
        /// header XML node.
        /// </summary>
        /// <param name="headerNode">An XML node containing loggable headers.</param>
        /// <param name="loggableHeader">The loggable header to retrieve.</param>
        /// <returns>A string containing the specified loggable headers.</returns>
        private string RetrieveLoggableHeader(XmlNode headerNode, string loggableHeader)
        {
            string xPath = string.Format("descendant::*[local-name()='{0}']", loggableHeader);
            XmlNode childHeaderNode = headerNode.SelectSingleNode(xPath);
            if (childHeaderNode != null)
            {
                return childHeaderNode.InnerText;
            }

            return "";
        }
    }
}

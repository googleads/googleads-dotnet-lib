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

using Google.Api.Ads.Common.Util;

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// Inspector that deserializes SOAP faults into AdsExceptions and rethrows them.
    /// </summary>
    /// <typeparam name="TException">The type of AdsException to throw</typeparam>
    public class SoapFaultInspector<TException> : IClientMessageInspector
        where TException : AdsException
    {
        private const string FAULT_ELEMENT_NAME = "ApiExceptionFault";

        private const string FAULT_ELEMENT_XPATH =
            "descendant::*[local-name()='" + FAULT_ELEMENT_NAME + "']";

        private static readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            Encoding = Encoding.UTF8,
            Indent = true
        };

        /// <summary>
        /// Gets or sets the type to deserialize faults into.
        /// </summary>
        public Type ErrorType { get; set; }

        /// <summary>
        /// A no-op for this inspector.
        /// </summary>
        /// <param name="request">The request to perform actions with</param>
        /// <param name="channel">The channel to the SOAP service</param>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }

        /// <summary>
        /// Throws an AdsException if the response was a SOAP fault.
        /// </summary>
        /// <param name="reply">The response Message</param>
        /// <param name="correlationState">
        /// The correlation state returned by BeforeSendRequest
        /// </param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (reply.IsFault)
            {
                StringBuilder xmlStringBuilder = new StringBuilder();
                using (XmlWriter xmlWriter = XmlWriter.Create(xmlStringBuilder, xmlWriterSettings))
                    using (MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue))
                    {
                        // Message can only be read once, so replace it with a copy.
                        reply = buffer.CreateMessage();
                        buffer.CreateMessage().WriteBody(xmlWriter);
                    }

                // Try locating the ApiExceptionFault node and deserializing it. Make sure to ignore
                // the namespace and look only for the local name.
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlStringBuilder.ToString());
                XmlElement faultNode = (XmlElement) xDoc.SelectSingleNode(FAULT_ELEMENT_XPATH);

                if (faultNode != null)
                {
                    // Deserialize the correct exception type and raise it.
                    string faultNodeNamespaceUri = faultNode.NamespaceURI;
                    string faultNodeContents = faultNode.OuterXml;
                    object apiError = SerializationUtilities.DeserializeFromXmlTextCustomRootNs(
                        faultNodeContents, ErrorType, faultNodeNamespaceUri, FAULT_ELEMENT_NAME);
                    throw (TException) Activator.CreateInstance(typeof(TException), new object[]
                    {
                        apiError
                    });
                }
            }
        }
    }
}

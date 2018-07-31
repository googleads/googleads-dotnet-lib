// Copyright 2014, Google Inc. All Rights Reserved.
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

using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Google.Api.Ads.Common.Logging
{
    /// <summary>
    /// Formats a SOAP message.
    /// </summary>
    public class SoapTraceFormatter : TraceFormatter
    {
        private static readonly XmlWriterSettings XmlWriterSettings = new XmlWriterSettings()
        {
            Encoding = Encoding.UTF8,
            Indent = true
        };

        /// <summary>
        /// Masks the contents of the traced message.
        /// </summary>
        /// <param name="body">The message body.</param>
        /// <param name="keysToMask">The keys for which values should be masked
        /// in the message body.</param>
        /// <returns>
        /// The formatted message body.
        /// </returns>
        public override string MaskContents(string body, ISet<string> keysToMask)
        {
            if (keysToMask.Count == 0)
            {
                return body;
            }

            XmlDocument xDoc = XmlUtilities.CreateDocument(body);
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xDoc.NameTable);
            xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

            foreach (string key in keysToMask)
            {
                string xPath =
                    string.Format("soap:Envelope/descendant::*[local-name()='{0}']", key);
                XmlNodeList nodes = xDoc.SelectNodes(xPath, xmlns);
                foreach (XmlElement node in nodes)
                {
                    node.InnerText = MASK_PATTERN;
                }
            }

            // Pretty-print the XML.
            StringBuilder sb = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(sb, XmlWriterSettings))
            {
                xDoc.WriteContentTo(xmlWriter);
            }

            return sb.ToString();
        }
    }
}

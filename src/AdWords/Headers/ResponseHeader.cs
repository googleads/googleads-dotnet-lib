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

using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;

namespace Google.Api.Ads.AdWords.Headers
{
    /// <summary>
    /// This class represents an AdWords SOAP response header.
    /// </summary>
    public class ResponseHeader
    {
        /// <summary>
        /// Gets or sets the request id for this API call.
        /// </summary>
        public string requestId { get; set; }

        /// <summary>
        /// Gets or sets the name of the service that was invoked.
        /// </summary>
        public string serviceName { get; set; }

        /// <summary>
        /// Gets or sets the name of the method that was invoked.
        /// </summary>
        public string methodName { get; set; }

        /// <summary>
        /// Gets or sets the number of operations for this API call.
        /// </summary>
        public long? operations { get; set; }

        /// <summary>
        /// Gets or sets the response time for this API call.
        /// </summary>
        public long? responseTime { get; set; }

        /// <summary>
        /// Reads a response header from an xml reader.
        /// </summary>
        /// <param name="reader">The xml reader.</param>
        /// <param name="rootNamespace"></param>
        /// <returns>A deserialized response header.</returns>
        public static ResponseHeader ReadFrom(XmlReader reader, string rootNamespace)
        {
            ResponseHeader retval = new ResponseHeader();
            XmlReader childReader = reader.ReadSubtree();
            XPathNavigator root = new XPathDocument(childReader).CreateNavigator();

            var ns = new XmlNamespaceManager(root.NameTable);
            ns.AddNamespace("root", rootNamespace);

            foreach (XPathNavigator childNode in root.Select("root:ResponseHeader/child::*", ns))
            {
                string content = childNode.Value;
                string name = childNode.LocalName;
                PropertyInfo pi = typeof(ResponseHeader).GetProperty(name);
                TypeConverter typeConverter = TypeDescriptor.GetConverter(pi.PropertyType);
                pi.SetValue(retval, typeConverter.ConvertFrom(content));
            }

            return retval;
        }
    }
}

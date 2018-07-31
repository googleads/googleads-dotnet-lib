// Copyright 2016, Google Inc. All Rights Reserved.
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


using System.IO;
using System.Text;
using System.Xml;

namespace Google.Api.Ads.Common.Util
{
    /// <summary>
    /// Utilities for working with XML.
    /// </summary>
    public class XmlUtilities
    {
        /// <summary>
        /// Loads a string into an XML document that that has XXE disabled.
        /// </summary>
        /// <param name="contents">The XML document contents as a text.</param>
        /// <returns>An XML Document object, with the contents loaded into the
        /// DOM.</returns>
        public static XmlDocument CreateDocument(string contents)
        {
            return CreateDocument(Encoding.UTF8.GetBytes(contents));
        }

        /// <summary>
        /// Loads the contents of a byte array into an XML document that
        /// has XXE disabled.
        /// </summary>
        /// <param name="contents">The XML document contents as a byte array.</param>
        /// <returns>An XML Document object, with the contents loaded into the
        /// DOM.</returns>
        public static XmlDocument CreateDocument(byte[] contents)
        {
            return CreateDocument(new MemoryStream(contents));
        }

        /// <summary>
        /// Loads the contents of a stream into an XML document that has XXE
        /// disabled.
        /// </summary>
        /// <param name="stream">The content stream.</param>
        /// <returns>An XML Document object, with the contents loaded into the
        /// DOM.</returns>
        public static XmlDocument CreateDocument(Stream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings()
            {
                DtdProcessing = DtdProcessing.Prohibit
            };
            XmlReader reader = XmlReader.Create(stream, settings);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            return doc;
        }
    }
}

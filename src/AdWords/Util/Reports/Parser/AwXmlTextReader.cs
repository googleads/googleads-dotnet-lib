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

using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// An implementation of <see ref="InputTextReader" /> using XML.
    /// </summary>
    public class AwXmlTextReader : InputTextReader
    {
        /// <summary>
        /// The instance of an XML text reader this reader uses.
        /// </summary>
        private readonly XmlTextReader reader;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">An input Stream of the XML text.</param>
        public AwXmlTextReader(Stream input)
        {
            reader = new XmlTextReader(input);
        }

        /// <summary>
        /// Advances this reader to the next row in the XML.
        /// </summary>
        /// <returns>True if reader was moved to a row element, and false if further
        /// reading is impossible.</returns>
        public bool Read()
        {
            if (!reader.Read())
            {
                return false;
            }
            else if (reader.NodeType == XmlNodeType.Element && reader.Name == "row")
            {
                return true;
            }

            return Read();
        }

        /// <summary>
        /// Disposes the underlying XML reader and makes this reader useless.
        /// </summary>
        public void Dispose()
        {
            reader.Dispose();
        }

        /// <summary>
        /// Gets a list of the attributes and their values for the current row of the report.
        /// </summary>
        /// <returns>A list of pairs of attribute names and values</returns>
        public IEnumerable<ColumnValuePair> GetAttributes()
        {
            while (reader.MoveToNextAttribute())
            {
                yield return new ColumnValuePair(reader.Name, reader.Value);
            }
        }
    }
}

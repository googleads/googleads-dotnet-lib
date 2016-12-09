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


using System.Xml;

namespace Google.Api.Ads.Common.Util {
  /// <summary>
  /// Utilities for working with XML.
  /// </summary>
  public class XmlUtilities {
    /// <summary>
    /// Creates an empty XML document that that has XXE disabled.
    /// </summary>
    /// <returns>An XML Document object.</returns>
    public static XmlDocument CreateDocument() {
      return new XmlDocument() { XmlResolver = null };
    }

    /// <summary>
    /// Loads the contents into an XML document that that has XXE disabled.
    /// </summary>
    /// <param name="contents">The XML document contents as a text.</param>
    /// <returns>An XML Document object, with the contents loaded into the
    /// DOM.</returns>
    public static XmlDocument CreateDocument(string contents) {
      XmlDocument xDoc = CreateDocument();
      xDoc.LoadXml(contents);
      return xDoc;
    }
  }
}

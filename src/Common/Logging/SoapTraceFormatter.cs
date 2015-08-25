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
using System.Xml;

namespace Google.Api.Ads.Common.Logging {

  /// <summary>
  /// Formats a SOAP message.
  /// </summary>
  public class SoapTraceFormatter : TraceFormatter {

    /// <summary>
    /// Masks the contents of the traced message.
    /// </summary>
    /// <param name="body">The message body.</param>
    /// <param name="keysToMask">The keys for which values should be masked
    /// in the message body.</param>
    /// <returns>
    /// The formatted message body.
    /// </returns>
    public override string MaskContents(string body, ISet<string> keysToMask) {
      XmlDocument xDoc = SerializationUtilities.LoadXml(body);
      XmlNamespaceManager xmlns = new XmlNamespaceManager(xDoc.NameTable);
      xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
      XmlNodeList nodes =
          xDoc.SelectNodes("soap:Envelope/soap:Header/descendant::*", xmlns);

      foreach (XmlElement node in nodes) {
        if (keysToMask.Contains(node.LocalName)) {
          node.InnerText = MASK_PATTERN;
        }
      }
      return xDoc.OuterXml;
    }
  }
}

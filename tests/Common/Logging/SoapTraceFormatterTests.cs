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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System.Collections.Generic;
using System.Xml;

namespace Google.Api.Ads.Common.Tests.Util {

  /// <summary>
  /// UnitTests for <see cref="SoapTraceFormatter"/> class.
  /// </summary>
  [TestFixture]
  public class SoapTraceFormatterTests {
    /// <summary>
    /// The keys to be masked in the request.
    /// </summary>
    private ISet<string> KEYS = new HashSet<string>() { "KEY1", "KEY2" };

    /// <summary>
    /// Test for SoapTraceFormatter.MaskContents method.
    /// </summary>
    [Test]
    public void TestMaskContents() {
      string maskedBody = new SoapTraceFormatter().MaskContents(Resources.SoapRequest, KEYS);
      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(maskedBody);
      XmlNamespaceManager xmlns = new XmlNamespaceManager(xDoc.NameTable);
      xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
      xmlns.AddNamespace("cm", "https://adwords.google.com/api/adwords/cm/v201409");

      XmlNodeList childNodes = xDoc.SelectNodes(
          "soap:Envelope/soap:Header/cm:RequestHeader/child::*", xmlns);

      foreach (XmlElement childNode in childNodes) {
        if (KEYS.Contains(childNode.LocalName)) {
          Assert.AreEqual(childNode.InnerText, SoapTraceFormatter.MASK_PATTERN);
        }
      }
    }
  }
}
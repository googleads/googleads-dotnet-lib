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

using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System.Collections.Generic;
using System.Xml;

namespace Google.Api.Ads.Common.Tests.Util {
  /// <summary>
  /// UnitTests for <see cref="XmlUtilitiesTest"/> class.
  /// </summary>
  [TestFixture]
  public class XmlUtilitiesTest {

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
    }

    [Test]
    [Category("Small")]
    public void TestNoXxeTranslation() {
      XmlDocument xDoc = XmlUtilities.CreateDocument(Resources.XxeExample);
      string temp = xDoc.OuterXml;
      Assert.That(temp.Contains("file:///c:/boot.ini"));
    }
  }
}


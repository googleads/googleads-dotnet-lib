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
using System.IO;
using System.Text;
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

    /// <summary>
    /// Tests that XmlDocument created with XmlUtililites doesn't resolve
    /// External Xml Entities.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestNoXxeTranslation() {
      Assert.Throws<XmlException>(delegate() {
        XmlDocument xDoc = XmlUtilities.CreateDocument(Resources.XxeExample);
      });
    }

    /// <summary>
    /// Tests that XmlDocument created with XmlUtililites can load an XML with
    /// UTF-8 BOM mark from a byte array or string.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestCanLoadXmlFromDiskWithUtf8BomInMemory() {
      Assert.DoesNotThrow(delegate() {
        XmlUtilities.CreateDocument(Resources.Utf8Bom);
        XmlUtilities.CreateDocument(Encoding.UTF8.GetBytes(Resources.Utf8Bom));
      });
    }

    /// <summary>
    /// Tests that XmlDocument created with XmlUtililites can load an XML with
    /// UTF-8 BOM mark from a file.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestCanLoadXmlFromDiskWithUtf8Bom() {
      string path = Path.GetTempFileName();
      using (FileStream fs = File.Create(path)) {
        byte[] bytes = Encoding.UTF8.GetBytes(Resources.Utf8Bom);
        fs.Write(bytes, 0, bytes.Length);
      }
      using (FileStream fs = File.OpenRead(path)) {
        Assert.DoesNotThrow(delegate() {
          XmlUtilities.CreateDocument(fs);
        });
      }
    }
  }
}


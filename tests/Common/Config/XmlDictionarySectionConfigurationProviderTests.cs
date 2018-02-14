// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Config;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Google.Api.Ads.Common.Tests.Config {
  public class XmlDictionarySectionConfigurationProviderTests {
    private const string SECTION_NAME = "TestConfig";
    Dictionary<string, string> TEST_VALUES = new Dictionary<string, string>() {
      { "TestSetting1", "Value1" },
      { "TestSetting2", "Value2" },
      { "TestSetting3", "42" },
      { "TestSetting4", "Value1" },
      { "TestSetting5", "ENUM_VALUE2" },
    };

    readonly string TEST_XML;

    public XmlDictionarySectionConfigurationProviderTests() {
      TEST_XML = MakeXml();
    }
    private string MakeXml() {
      StringBuilder builder = new StringBuilder();
      builder.Append("<?xml version = '1.0' encoding='utf-8'?>");
      builder.Append("<configuration>");
      builder.AppendFormat("<{0}>", SECTION_NAME);

      foreach (string key in TEST_VALUES.Keys) {
        builder.AppendFormat("<add key='{0}' value='{1}' />", key, TEST_VALUES[key]);
      }

      builder.AppendFormat("</{0}>", SECTION_NAME);
      builder.Append("</configuration>");
      return builder.ToString();
    }

    /// <summary>
    /// Tests the Load() method.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestLoad() {
      var source = new XmlDictionarySectionConfigurationSource(SECTION_NAME);
      var provider = new XmlDictionarySectionConfigurationProvider(source);
      var memStream = new MemoryStream(Encoding.UTF8.GetBytes(TEST_XML));

      provider.Load(memStream);
      foreach (string key in TEST_VALUES.Keys) {
        string value = null;
        Assert.IsTrue(provider.TryGet(key, out value));
        Assert.AreEqual(TEST_VALUES[key], value, string.Format(
            "Setting '{0}' value has mismatch.", key));
      }
    }
  }
}

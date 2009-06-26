// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;

using NUnit.Framework;

namespace com.google.api.adwords.tests {
  /// <summary>
  /// UnitTests for <see cref="ApplicationConfiguration"/> class.
  /// </summary>
  [TestFixture]
  public class ApplicationConfigurationTests {
    /// <summary>
    /// Test if <see cref="ApplicationConfiguration"/> class read the values properly
    /// from app.config.
    /// </summary>
    [Test]
    public void TestConfigCreation() {
      // Load the config xml.
      string configXmlPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
      ConfigXmlDocument xmlDoc = new ConfigXmlDocument();
      xmlDoc.Load(configXmlPath);

      // Read AdWordsApi section.
      XmlNode section = xmlDoc.SelectSingleNode("/configuration/AdWordsApi");
      NameValueSectionHandler handler = new NameValueSectionHandler();
      NameValueCollection collection = (NameValueCollection) handler.Create(null, null, section);

      // TODO: Fix this code if you change one of the field names in ApplicationConfiguration
      // class.
      FieldInfo[] fields = typeof(ApplicationConfiguration).GetFields();

      foreach (FieldInfo field in fields) {
        StringBuilder builder = new StringBuilder();
        if (field.Name == "urlV13") {
          builder.Append("v13.Url");
        } else if (field.Name == "urlV200906") {
          builder.Append("v200906.Url");
        } else {
          builder.Append(field.Name);
          builder[0] = Char.ToUpper(builder[0]);
        }
        string keyName = builder.ToString();
        if (!string.IsNullOrEmpty((string) collection[keyName]) && field.GetValue(null) != null) {
          string failMessage =
              string.Format("{0} was not read correctly from app.config.", keyName);
          Assert.AreEqual(field.GetValue(null).ToString().ToLower(),
              (string) (collection[keyName]).ToLower(), failMessage);
        }
      }

      // Test if ApplicationConfiguration.proxy was created properly.
      if (!string.IsNullOrEmpty(collection["ProxyServer"])) {
        Assert.NotNull(ApplicationConfiguration.proxy);
        Assert.AreEqual(collection["ProxyServer"].ToLower(),
            ApplicationConfiguration.proxy.Address.AbsoluteUri.ToLower(),
            "Proxy should not be null if ProxyServer is available.");
        if (!string.IsNullOrEmpty(collection["ProxyUser"])) {
          Assert.AreEqual(collection["ProxyUser"].ToLower(),
              ((NetworkCredential) ApplicationConfiguration.proxy.Credentials).UserName.ToLower(),
              "ProxyUser was not read correctly from app.config.");
        }
        if (!string.IsNullOrEmpty(collection["ProxyPassword"])) {
          Assert.AreEqual(collection["ProxyPassword"].ToLower(),
              ((NetworkCredential) ApplicationConfiguration.proxy.Credentials).Password.ToLower(),
              "ProxyPassword was not read correctly from app.config.");
        }
        if (!string.IsNullOrEmpty(collection["ProxyDomain"])) {
          Assert.AreEqual(collection["ProxyDomain"].ToLower(),
              ((NetworkCredential) ApplicationConfiguration.proxy.Credentials).Domain.ToLower(),
              "ProxyDomain was not read correctly from app.config.");
        }
      }
    }
  }
}

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

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Google.Api.Ads.Common.Config {

  /// <summary>
  /// A provider for <see cref="XmlDictionarySectionConfigurationSource"/>.
  /// </summary>
  /// <seealso cref="Microsoft.Extensions.Configuration.FileConfigurationProvider" />
  public class XmlDictionarySectionConfigurationProvider : FileConfigurationProvider {

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlDictionarySectionConfigurationProvider"/>
    /// class.
    /// </summary>
    /// <param name="source">The source.</param>
    public XmlDictionarySectionConfigurationProvider(
        XmlDictionarySectionConfigurationSource source) : base(source) {
    }

    /// <summary>
    /// Loads this provider's data from a stream.
    /// </summary>
    /// <param name="stream">The stream to read.</param>
    public override void Load(Stream stream) {
      string sectionName = (Source as XmlDictionarySectionConfigurationSource).SectionName;
      Dictionary<string, string> data = new Dictionary<string, string>();

      XmlDocument xDoc = new XmlDocument();
      xDoc.Load(stream);

      // A typical configuration XML looks like follows:

      // <configuration>
      //   <configSections>
      //     <section name = "AdWordsApi" type = "System.Configuration.DictionarySectionHandler" />
      //   </configSections>
      //   <AdWordsApi>
      //     <add key = "MaskCredentials" value= "false" />
      //     <add key= "EnableSoapExtension" value= "true" />
      //   ...
      //   </AdWordsApi>
      // </configuration>

      // We ignore the <configSections> node and only care about the node pointed to
      // by Source.SectionName.
      string query = string.Format("//configuration/{0}/add", sectionName);
      XmlNodeList configNodes = xDoc.SelectNodes(query);

      foreach (XmlElement configNode in configNodes) {
        string configName = configNode.GetAttribute("key");
        string configValue = configNode.GetAttribute("value");
        data[configName] = configValue;
      }
      // Once the parsing is done, the dictionary we created has to be assigned to the Data
      // property of the provider.

      this.Data = data;
    }
  }
}

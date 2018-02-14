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
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;

namespace Google.Api.Ads.Common.Config {

  /// <summary>
  /// A configuration source that acts as a replacement for
  /// System.Configuration.DictionarySectionHandler.
  /// </summary>
  public class XmlDictionarySectionConfigurationSource : FileConfigurationSource {

    /// <summary>
    /// Gets or sets the name of the section.
    /// </summary>
    internal string SectionName { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlDictionarySectionConfigurationSource"/>
    /// class.
    /// </summary>
    /// <param name="sectionName">Name of the section to be parsed.</param>
    public XmlDictionarySectionConfigurationSource(string sectionName) {
      this.SectionName = sectionName;
    }

    /// <summary>
    /// Builds the configuration provider for this source.
    /// </summary>
    /// <param name="builder">The configuration builder.</param>
    /// <returns>
    /// A configuration provider.
    /// </returns>
    public override IConfigurationProvider Build(IConfigurationBuilder builder) {
      FileProvider = FileProvider ?? builder.GetFileProvider();

      List<string> potentialConfigFiles = new List<string>();

      string configFile = null;

      foreach (IFileInfo fileInfo in this.FileProvider.GetDirectoryContents("")) {
        string lowerCaseFileName = fileInfo.Name.ToLower();
        if (lowerCaseFileName.EndsWith(".exe.config")) {
          // If an .exe.config is found, pick that file and break the loop.
          configFile = fileInfo.Name;
          break;
        } else if (lowerCaseFileName == "web.config") {
          // If a web.config is found, pick it, but continue with the loop to see if there is
          // a better match.
          configFile = fileInfo.Name;
        } else if (lowerCaseFileName.EndsWith(".config")) {
          // If a .config is found, pick it if no matches were found so far. Continue with the
          // loop to see if there is a better match.
          configFile = configFile ?? fileInfo.Name;
        }
      }

      // If no config files were found, we should skip loading the file.
      if (string.IsNullOrEmpty(configFile)) {
        this.Optional = true;
        this.Path = null;
      } else {
        this.Optional = false;
        this.Path = configFile;
      }

      return new XmlDictionarySectionConfigurationProvider(this);
    }
  }
}

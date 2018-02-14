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

namespace Google.Api.Ads.Common.Config {

  /// <summary>
  /// Static class to add a builder method for <see cref="XmlDictionarySectionConfigurationSource"/>
  /// to <see cref="IConfigurationBuilder"/>.
  /// </summary>
  internal static class XmlDictionarySectionConfigurationExtensions {

    /// <summary>
    /// Adds configuration from an XML file section.
    /// </summary>
    /// <param name="builder">The configuration builder.</param>
    /// <param name="sectionName">Name of the section.</param>
    /// <returns>A configuration builder.</returns>
    internal static IConfigurationBuilder AddXmlFileSection(this IConfigurationBuilder builder,
        string sectionName) {
      builder.Add(new XmlDictionarySectionConfigurationSource(sectionName));
      return builder;
    }
  }
}

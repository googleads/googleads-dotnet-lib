// Copyright 2011, Google Inc. All Rights Reserved.
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

using System;
using System.Xml.Serialization;

namespace Google.Api.Ads.AdWords.Headers {
  /// <summary>
  /// A SOAP stub class that represents the response headers for an AdWords
  /// API call.
  /// </summary>
  [XmlRootAttribute(ElementName = "ResponseHeader", Namespace =
      "https://adwords.google.com/api/adwords{gp}{version}")]
  [SerializableAttribute()]
  public class ResponseHeaderStub {
    /// <summary>
    /// Request ID for this API call.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm{version}")]
    public string requestId;

    /// <summary>
    /// Number of operations for this API call.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm{version}")]
    public long operations;

    /// <summary>
    /// Specifies whether <seealso cref="operations"/> is provided.
    /// </summary>
    [XmlIgnoreAttribute]
    public bool operationsSpecified;

    /// <summary>
    /// Response time for this API call.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm{version}")]
    public long responseTime;

    /// <summary>
    /// Specifies whether <seealso cref="responseTime"/> is provided.
    /// </summary>
    [XmlIgnoreAttribute]
    public bool responseTimeSpecified;

    /// <summary>
    /// Units consumed for this API call.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm{version}")]
    public long units;

    /// <summary>
    /// Specifies whether <seealso cref="units"/> is provided.
    /// </summary>
    [XmlIgnoreAttribute]
    public bool unitsSpecified;
  }
}

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
  /// A SOAP stub class that represents the request headers for an AdWords
  /// API call.
  /// </summary>
  [SerializableAttribute()]
  [XmlRootAttribute(ElementName = "RequestHeader", Namespace =
      "https://adwords.google.com/api/adwords{gp}{version}")]
  public class RequestHeaderStub {

    /// <summary>
    /// Developer token.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm{version}")]
    public string developerToken;

    /// <summary>
    /// Client customer id for which API calls are made.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm{version}")]
    public string clientCustomerId;

    /// <summary>
    /// User agent to identify the requesting application.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm{version}")]
    public string userAgent;

    /// <summary>
    /// Specifies whether this request is for validation only.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm{version}")]
    public bool validateOnly;

    /// <summary>
    /// Specifies whether <seealso cref="validateOnly"/> is provided.
    /// </summary>
    [XmlIgnoreAttribute]
    public bool validateOnlySpecified;

    /// <summary>
    /// Specifies whether partial failures should be returned.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm{version}")]
    public bool partialFailure;

    /// <summary>
    /// Specifies whether <seealso cref="partialFailure"/> is provided.
    /// </summary>
    [XmlIgnoreAttribute]
    public bool partialFailureSpecified;

    /// <summary>
    /// Specifies the AdWords Express business ID. This field applies only
    /// for AdWords Express API services.
    /// </summary>
    [XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/express{version}")]
    public long expressBusinessId;

    /// <summary>
    /// Specifies whether <seealso cref="expressBusinessId"/> is provided. This
    /// field applies only for AdWords Express API services.
    /// </summary>
    [XmlIgnoreAttribute]
    public bool expressBusinessIdSpecified;
  }
}

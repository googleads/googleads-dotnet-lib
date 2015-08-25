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

using System;
using System.Xml.Serialization;

namespace Google.Api.Ads.Dfp.Headers {
  /// <summary>
  /// The authentication header.
  /// </summary>
  [XmlIncludeAttribute(typeof(OAuth))]
  [XmlRootAttribute(Namespace = "https://www.google.com/apis/ads/publisher{version}")]
  public abstract class Authentication {
    /// <summary>
    /// The authentication type.
    /// </summary>
    private string authenticationTypeField;

    /// <summary>
    /// Gets or sets the type of the authentication.
    /// </summary>
    [XmlElementAttribute("Authentication.Type")]
    public string AuthenticationType {
      get {
        return this.authenticationTypeField;
      }
      set {
        this.authenticationTypeField = value;
      }
    }
  }
}

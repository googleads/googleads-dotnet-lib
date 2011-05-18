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

namespace Google.Api.Ads.Dfp.Headers {
  /// <summary>
  /// The oauth authentication header.
  /// </summary>
  [XmlRootAttribute(Namespace = "https://www.google.com/apis/ads/publisher{version}")]
  public class OAuth : Authentication {
    /// <summary>
    /// The oauth parameters.
    /// </summary>
    private string parametersField;

    /// <summary>
    /// Gets or sets the oauth parameters.
    /// </summary>
    public string parameters {
      get {
        return this.parametersField;
      }
      set {
        this.parametersField = value;
      }
    }
  }
}

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
using System.ServiceModel.Channels;

namespace Google.Api.Ads.AdWords.Headers {

  /// <summary>
  /// Base class for AdWords API SOAP headers.
  /// </summary>
  public abstract class AdWordsSoapHeader : MessageHeader {

    /// <summary>
    /// Gets or sets the API version.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Gets the namespace.
    /// </summary>
    /// <value>The namespace.</value>
    public override string Namespace {
      get {
        return String.Format("https://adwords.google.com/api/adwords/{0}/{1}", GroupName, Version);
      }
    }

    /// <summary>
    /// Gets or sets the service namespace (e.g. cm, o, info, etc.).
    /// </summary>
    public string GroupName { get; set; }
  }
}

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

using Google.Api.Ads.Common.Lib;

using System;

namespace Google.Api.Ads.AdWords.Headers {
  /// <summary>
  /// Base class for AdWords API SOAP headers.
  /// </summary>
  public abstract class AdWordsSoapHeader : SoapHeaderBase {
    /// <summary>
    /// Gets or sets the API version.
    /// </summary>
    public string Version {
      get {
        return placeHolders["{version}"].TrimStart('/');
      }
      set {
        placeHolders["{version}"] = "/" + value;
      }
    }

    /// <summary>
    /// Gets or sets the service namespace (e.g. cm, o, info, etc.).
    /// </summary>
    public string GroupName {
      get {
        return placeHolders["{gp}"].TrimStart('/');
      }
      set {
        placeHolders["{gp}"] = "/" + value;
      }
    }
  }
}

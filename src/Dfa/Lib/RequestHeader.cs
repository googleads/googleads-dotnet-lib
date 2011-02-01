// Copyright 2010, Google Inc. All Rights Reserved.
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
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// This class represents an optional Request Soap Header be used with
  /// DFA services.
  /// </summary>
  public class RequestHeader {
    /// <summary>
    /// The application name.
    /// </summary>
    string applicationName = "";

    /// <summary>
    /// The target namespace to which this object is serialized.
    /// </summary>
    string targetNamespace;

    /// <summary>
    /// Gets or sets the target namespace.
    /// </summary>
    public string TargetNamespace {
      get {
        return targetNamespace;
      } set {
        targetNamespace = value;
      }
    }

    /// <summary>
    /// Gets or sets the applicationName.
    /// </summary>
    public string ApplicationName {
      get {
        return applicationName;
      }
      set {
        applicationName = value;
      }
    }
  }
}

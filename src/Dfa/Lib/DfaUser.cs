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
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// Represents a DFA API user.
  /// </summary>
  public partial class DfaUser : AdsUser {
    /// <summary>
    /// Public constructor. Use this version if you want the library to
    /// use all settings from App.config.
    /// </summary>
    public DfaUser() : base(new DfaAppConfig()) {
    }

    /// <summary>
    /// Parameterized constructor. Use this version if you want to construct
    /// a DfaUser with a custom set of headers.
    /// </summary>
    /// <param name="headers">The custom set of headers.</param>
    public DfaUser(Dictionary<string, string> headers) : base(new DfaAppConfig(), headers) {
    }

    /// <summary>
    /// Gets all the service types to be registered against this user.
    /// </summary>
    /// <returns>The type of all service classes to be registered.</returns>
    public override Type[] GetServiceTypes() {
      return new DfaService().GetServiceTypes();
    }

    /// <summary>
    /// Gets the default listeners.
    /// </summary>
    /// <returns>A list of default listeners</returns>
    public override SoapListener[] GetDefaultListeners() {
      // Order is important, WseSecurityListener should get the first shot, so
      // DfaTraceListener writes log with header.
      return new SoapListener[] {SoapHeaderListener.Instance, DfaTraceListener.Instance};
    }
  }
}

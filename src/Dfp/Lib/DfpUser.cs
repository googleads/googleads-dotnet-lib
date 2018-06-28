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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Logging;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Lib {

  /// <summary>
  /// Represents an DFP API user.
  /// </summary>
  public partial class DfpUser : AdsUser {

    /// <summary>
    /// Public constructor. Use this version if you want the library to
    /// use all settings from App.config.
    /// </summary>
    public DfpUser()
      : base(new DfpAppConfig()) {
    }

    /// <summary>
    /// Parameterized constructor. Use this version if you want to construct
    /// a DfpUser with a custom set of headers.
    /// </summary>
    /// <param name="headers">The custom set of headers.</param>
    public DfpUser(Dictionary<string, string> headers)
      : base(new DfpAppConfig(), headers) {
    }

    /// <summary>
    /// Public constructor. Use this version if you want to construct
    /// a DfpUser with a custom configuration.
    /// </summary>
    public DfpUser(DfpAppConfig config)
        : base(config)
    {
    }

        /// <summary>
        /// Gets all the service types to be registered against this user.
        /// </summary>
        /// <returns>The type of all service classes to be registered.</returns>
        public override Type[] GetServiceTypes() {
      return new DfpService().GetServiceTypes();
    }

    /// <summary>
    /// Gets the default listeners.
    /// </summary>
    /// <returns>A list of default listeners</returns>
    public override SoapListener[] GetDefaultListeners() {
      return new SoapListener[] { DfpTraceListener.Instance };
    }
  }
}

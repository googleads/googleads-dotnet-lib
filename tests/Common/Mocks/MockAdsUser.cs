// Copyright 2012, Google Inc. All Rights Reserved.
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

namespace Google.Api.Ads.Common.Tests.Mocks {
  /// <summary>
  /// Implments a mock version of AdsUser class for testing purposes.
  /// </summary>
  class MockAdsUser : AdsUser {
    /// <summary>
    /// Overloaded constructor.
    /// </summary>
    /// <param name="config">The configuration class.</param>
    public MockAdsUser(MockAppConfig config)
      : base(config) {
    }

    /// <summary>
    /// Gets all the service types to be registered against this user.
    /// </summary>
    /// <returns>
    /// The type of all service classes to be registered.
    /// </returns>
    public override Type[] GetServiceTypes() {
      return null;
    }

    /// <summary>
    /// Gets the default listeners.
    /// </summary>
    /// <returns>
    /// A list of default listeners
    /// </returns>
    public override SoapListener[] GetDefaultListeners() {
      return new SoapListener[] {new MockTraceListener(this.Config)};
    }
  }
}

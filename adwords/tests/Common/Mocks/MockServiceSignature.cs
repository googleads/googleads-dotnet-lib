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

using System;

namespace Google.Api.Ads.Common.Tests.Mocks {
  /// <summary>
  /// Implements a mock version of the ServiceSignature class for testing
  /// purposes.
  /// </summary>
  public class MockServiceSignature : ServiceSignature {
    /// <summary>
    /// Initializes a new instance of the <see cref="MockServiceSignature"/>
    /// class.
    /// </summary>
    /// <param name="version">The version.</param>
    /// <param name="serviceName">Name of the service.</param>
    /// <param name="protocols">The protocols.</param>
    public MockServiceSignature(string version, string serviceName, SupportedProtocols protocols)
      : base(version, serviceName, protocols) {
    }

    /// <summary>
    /// Gets the type of service.
    /// </summary>
    public override Type ServiceType {
      get {
        return null;
      }
    }
  }
}

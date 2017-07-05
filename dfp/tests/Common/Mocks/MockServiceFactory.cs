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
  /// Implements a mock version of the ServiceFactory interface for testing
  /// purposes.
  /// </summary>
  public class MockServiceFactory : ServiceFactory {
    /// <summary>
    /// Create a service object.
    /// </summary>
    /// <param name="signature">Signature of the service being created.</param>
    /// <param name="user">The user for which the service is being created.
    /// <param name="serverUrl">The server to which the API calls should be
    /// made.</param></param>
    /// <param name="serverUrl"></param>
    /// <returns>
    /// An object of the desired service type.
    /// </returns>
    public override AdsClient CreateService(ServiceSignature signature, AdsUser user,
        Uri serverUrl) {
      return null;
    }

    /// <summary>
    /// Reads the headers from App.config.
    /// </summary>
    /// <param name="config">The configuration class.</param>
    protected override void ReadHeadersFromConfig(AppConfig config) {
      return;
    }

    /// <summary>
    /// Checks preconditions of the service signature and throws and exception if the service
    /// cannot be generated.
    /// </summary>
    /// <param name="signature">the service signature for generating the service</param>
    protected override void CheckServicePreconditions(ServiceSignature signature) {
      return;
    }
  }
}

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
using Google.Api.Ads.Common.Tests.Mocks;

using NUnit.Framework;

using System;

namespace Google.Api.Ads.Common.Tests.Lib {
  /// <summary>
  /// Tests the ServiceSignature class.
  /// </summary>
  public class ServiceSignatureTests {
    /// <summary>
    /// The test version string.
    /// </summary>
    const string VERSION = "TEST_VERSION";

    /// <summary>
    /// The test service name string.
    /// </summary>
    const string SERVICE_NAME = "SERVICE_NAME";

    /// <summary>
    /// The test protocol.
    /// </summary>
    const ServiceSignature.SupportedProtocols PROTOCOLS = ServiceSignature.SupportedProtocols.SOAP;

    /// <summary>
    /// Tests the property setters and getters.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestProperties() {
      MockServiceSignature signature = new MockServiceSignature(VERSION, SERVICE_NAME, PROTOCOLS);
      Assert.AreEqual(VERSION, signature.Version);
      Assert.AreEqual(SERVICE_NAME, signature.ServiceName);
      Assert.AreEqual(PROTOCOLS, signature.SupportedProtocol);
      Assert.NotNull(signature.Id);
      Assert.Null(signature.ServiceType);
    }
  }
}

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
  /// Tests the ServiceFactory class.
  /// </summary>
  public class ServiceFactoryTests {
    /// <summary>
    /// The test configuration class.
    /// </summary>
    MockAppConfig config = new MockAppConfig();

    /// <summary>
    /// Tests the property setters and getters.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestProperties() {
      ServiceFactory serviceFactory = new MockServiceFactory();
      serviceFactory.Config = config;
      Assert.AreEqual(config, serviceFactory.Config);
    }
  }
}

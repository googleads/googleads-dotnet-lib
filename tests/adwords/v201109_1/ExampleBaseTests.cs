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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109_1;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Google.Api.Ads.AdWords.Tests.v201109_1 {
  /// <summary>
  /// UnitTests for mocking a SOAP service.
  /// </summary>
  class ExampleBaseTests : BaseTests {
    /// <summary>
    /// Test utilities instance for support functionality when running tests.
    /// </summary>
    protected TestUtils utils = new TestUtils();

    /// <summary>
    /// Map of parameters to run the code example.
    /// </summary>
    protected Dictionary<String, String> parameters = null;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ExampleBaseTests() : base() {
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="codeExample">The code example.</param>
    /// <param name="parameters">The code example parameters.</param>
    protected void RunExample(Object codeExample) {
      Thread.Sleep(10000);
      StringWriter writer = new StringWriter();
      Assert.DoesNotThrow(delegate() {
        codeExample.GetType().GetMethod("Run").Invoke(codeExample,
            new object[] {user, parameters, writer});
        Console.WriteLine(writer.ToString());
      });
    }
  }
}

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

using Google.Api.Ads.Dfa.Lib;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.Dfa.Tests {
  /// <summary>
  /// Base class for all test suites.
  /// </summary>
  public class BaseTests {
    /// <summary>
    /// The Dfa user to be used for tests.
    /// </summary>
    protected DfaUser user = new DfaUser();

    /// <summary>
    /// Default public constructor.
    /// </summary>
    /// <remarks>The constructor adds a 2000 ms delay between running individual
    /// tests so that we don't hit the ClientLogin server frequently and cause
    /// it throw captcha errors. You shouldn't do this in your code, instead
    /// you should generate an authtoken, set it in your App.config and reuse
    /// it to avoid performance issues.
    /// </remarks>
    public BaseTests() {
      // Make sure we don't hit the authtoken endpoint really bad.
      Thread.Sleep(2000);
    }
  }
}

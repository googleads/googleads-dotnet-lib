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
using System.Collections.Generic;

namespace Google.Api.Ads.Common.Tests.Mocks {
  /// <summary>
  /// Implements a mock version of the TraceListener class for testing purposes.
  /// </summary>
  class MockTraceListener : TraceListener {
    /// <summary>
    /// Initializes a new instance of the <see cref="MockTraceListener"/> class.
    /// </summary>
    /// <param name="config">The config class.</param>
    public MockTraceListener(AppConfig config) : base(config) {
      this.DateTimeProvider = new MockDateTimeProvider();
    }

    /// <summary>
    /// Gets a list of fields to be masked in xml logs.
    /// </summary>
    /// <returns>
    /// The list of fields to be masked.
    /// </returns>
    protected override ISet<string> GetFieldsToMask() {
      return new HashSet<string>(new string[] { "authToken", "developerToken" },
          StringComparer.OrdinalIgnoreCase);
    }
  }
}

// Copyright 2013, Google Inc. All Rights Reserved.
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
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// Handles DFA API Errors.
  /// </summary>
  public class DfaErrorHandler : ErrorHandler {
    /// <summary>
    /// Initializes a new instance of the <see cref="DfaErrorHandler"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    public DfaErrorHandler(AdsUser user)
      : base(user) {
    }

    /// <summary>
    /// Checks if an API call should be retried when an exception occurs.
    /// </summary>
    /// <param name="ex">The exception.</param>
    /// <returns>
    /// True, if the call should be retried, false otherwise.
    /// </returns>
    public override bool ShouldRetry(Exception ex) {
      return false;
    }

    /// <summary>
    /// Prepares the system for retrying the last failed call.
    /// </summary>
    /// <param name="ex">The exception.</param>
    public override void PrepareForRetry(Exception ex) {
    }
  }
}

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

// Author: api.anash@gmail.com (Anash P. Oommen)

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Timers;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Base class for all exceptions thrown by the library related
  /// to an Ads API call.
  /// </summary>
  public class InMemoryRateLimiter : RateLimiter {
    private List<long> bucket = new List<long>();

    /// <summary>
    /// Public constructor.
    /// </summary>
    public InMemoryRateLimiter() : base() {
    }

    protected override void ResetBucket() {
      bucket.Clear();
    }

    protected override void AddToken(long token) {
      bucket.Add(token);
      Console.WriteLine("Generated token = #{0}", bucket.Count);
    }

    protected override long GetActiveTokenCount() {
      return bucket.Count;
    }

    protected override long RemoveToken() {
      long retVal = bucket[0];
      bucket.RemoveAt(0);
      Console.WriteLine("Removed token = #{0}", bucket.Count);
      return retVal;
    }
  }
}

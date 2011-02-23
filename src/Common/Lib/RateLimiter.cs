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
  public abstract class RateLimiter {
    private long callsPerSecond;

    private long bucketSize;

    private System.Timers.Timer timer = new System.Timers.Timer();

    public long CallsPerSecond {
      get {
        return callsPerSecond;
      }
      set {
        callsPerSecond = value;
        ResetTimer();
      }
    }

    protected abstract void AddToken(long token);

    protected abstract void ResetBucket();

    protected abstract long GetActiveTokenCount();

    protected abstract long RemoveToken();

    /// <summary>
    /// Public constructor.
    /// </summary>
    protected RateLimiter() {
      callsPerSecond = 3;
      timer.Elapsed += new ElapsedEventHandler(OnTimerEllapsed);
      ResetTimer();
    }

    private void OnTimerEllapsed(object sender, ElapsedEventArgs e) {
      lock (this) {
        if (GetActiveTokenCount() < bucketSize) {
          AddToken(DateTime.Now.Ticks);
        }
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void ResetTimer() {
      timer.Stop();
      if (callsPerSecond > 0) {
        bucketSize = callsPerSecond * 10;
        ResetBucket();
        timer.Interval = 1000 / callsPerSecond;
        timer.Start();
      }
    }

    public long GetToken() {
      while (true) {
        while (GetActiveTokenCount() == 0) {
          Thread.Sleep((int)timer.Interval + 1);
        }
        lock (this) {
          if (GetActiveTokenCount() > 0) {
            return RemoveToken();
          }
        }
      }
    }
  }
}

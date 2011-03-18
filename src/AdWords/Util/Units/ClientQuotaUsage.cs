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

namespace Google.Api.Ads.AdWords.Util.Units {
  /// <summary>
  /// Represents the API usage of a user.
  /// </summary>
  public class ClientQuotaUsage {
    /// <summary>
    /// Map between the child account emails and API usage.
    /// </summary>
    private SortedList<string, long> usageMap = new SortedList<string, long>();

    /// <summary>
    /// Total API usage for the user.
    /// </summary>
    private long totalUnits;

    /// <summary>
    /// Difference in units between <seealso cref="totalUnits"/> and
    /// sum of units consumed by all child accounts.
    /// </summary>
    private long diffUnits;

    /// <summary>
    /// Gets or sets a map between the child account emails and API usage.
    /// </summary>
    public SortedList<string, long> UsageMap {
      get {
        return usageMap;
      }
    }

    /// <summary>
    /// Gets or sets total API usage for the user.
    /// </summary>
    public long TotalUnits {
      get {
        return totalUnits;
      }
      set {
        totalUnits = value;
      }
    }

    /// <summary>
    /// Gets or sets difference in units between <seealso cref="TotalUnits"/>
    /// and sum of units consumed by all child accounts.
    /// </summary>
    public long DiffUnits {
      get {
        return diffUnits;
      }
      set {
        diffUnits = value;
      }
    }

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ClientQuotaUsage() {
    }
  }
}

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

namespace Google.Api.Ads.AdWords.Util {
  /// <summary>
  /// Represents the quota usage of a method in AdWords API, in units.
  /// </summary>
  public class MethodQuotaUsage {
    /// <summary>
    /// Name of the service.
    /// </summary>
    private string serviceName;

    /// <summary>
    /// Name of the method.
    /// </summary>
    private string methodName;

    /// <summary>
    /// Units consumed.
    /// </summary>
    private long units;

    /// <summary>
    /// Gets or sets the name of the service.
    /// </summary>
    public string ServiceName {
      get {
        return serviceName;
      }
      set {
        serviceName = value;
      }
    }

    /// <summary>
    /// Gets or sets the name of the method.
    /// </summary>
    public string MethodName {
      get {
        return methodName;
      }
      set {
        methodName = value;
      }
    }

    /// <summary>
    /// Gets or sets the units consumed.
    /// </summary>
    public long Units {
      get {
        return units;
      }
      set {
        units = value;
      }
    }

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public MethodQuotaUsage() {
    }

    /// <summary>
    /// Parameterized constructor.
    /// </summary>
    /// <param name="serviceName">Name of the service.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="units">Units consumed.</param>
    public MethodQuotaUsage(string serviceName, string methodName, long units) {
      this.serviceName = serviceName;
      this.methodName = methodName;
      this.units = units;
    }
  }
}

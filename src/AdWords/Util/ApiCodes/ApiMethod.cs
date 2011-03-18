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

namespace Google.Api.Ads.AdWords.Util.ApiCodes {
  /// <summary>
  /// Represents a method in AdWords API.
  /// </summary>
  public class ApiMethod {
    /// <summary>
    /// Name of the service.
    /// </summary>
    private string serviceName;

    /// <summary>
    /// Name of the method.
    /// </summary>
    private string methodName;

    /// <summary>
    /// Version of the API to which the method belongs.
    /// </summary>
    private string version;

    /// <summary>
    /// Gets or sets the name or the service.
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
    /// Gets or sets the version of the API to which the method belongs.
    /// </summary>
    public string Version {
      get {
        return version;
      }
      set {
        version = value;
      }
    }

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ApiMethod() {
      serviceName = "";
      methodName = "";
      version = "";
    }

    /// <summary>
    /// Parameterized constructor.
    /// </summary>
    /// <param name="serviceName">Name of the service to which the method
    /// belongs.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="version">Version of the API to which the method
    /// belongs.</param>
    public ApiMethod(string serviceName, string methodName, string version) {
      this.serviceName = serviceName;
      this.methodName = methodName;
      this.version = version;
    }
  }
}

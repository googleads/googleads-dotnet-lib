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

using Google.Api.Ads.Common.Lib;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// Represents an API call that was made to the AdWords API server.
  /// </summary>
  public partial class ApiCallEntry {
    /// <summary>
    /// The service that was called.
    /// </summary>
    private AdsClient service;

    /// <summary>
    /// Name of the method that was called.
    /// </summary>
    private string method;

    /// <summary>
    /// The number of API operations for this call.
    /// </summary>
    private int operationCount;

    /// <summary>
    /// Gets or sets the service that was called.
    /// </summary>
    public AdsClient Service {
      get {
        return service;
      }
      set {
        service = value;
      }
    }

    /// <summary>
    /// Gets or sets the name of the method that was called.
    /// </summary>
    public string Method {
      get {
        return method;
      }
      set {
        method = value;
      }
    }

    /// <summary>
    /// Gets or sets the number of API operations for this call.
    /// </summary>
    public int OperationCount {
      get {
        return operationCount;
      }
      set {
        operationCount = value;
      }
    }
  }
}

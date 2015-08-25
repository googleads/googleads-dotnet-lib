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

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// This class represents the Response Soap Header from the DFA server.
  /// </summary>
  public class ResponseHeader {
    /// <summary>
    /// The request id.
    /// </summary>
    string requestId = "";

    /// <summary>
    /// The response time.
    /// </summary>
    long responseTime;

    /// <summary>
    /// Gets or sets the request id.
    /// </summary>
    public string RequestId {
      get {
        return requestId;
      } set {
        requestId = value;
      }
    }

    /// <summary>
    /// Gets or sets the response time.
    /// </summary>
    public long ResponseTime {
      get {
        return responseTime;
      } set {
        responseTime = value;
      }
    }
  }
}

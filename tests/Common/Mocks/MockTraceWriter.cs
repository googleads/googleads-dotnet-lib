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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Common.Lib;

using System;

namespace Google.Api.Ads.Common.Tests.Mocks {
  /// <summary>
  /// Implements a mock version of the TraceWriter interface.
  /// </summary>
  class MockTraceWriter : TraceWriter {
    /// <summary>
    /// The last soap log written to this writer.
    /// </summary>
    string soapLog;

    /// <summary>
    /// The last request log written to this writer.
    /// </summary>
    string requestLog;

    /// <summary>
    /// Gets or sets the SOAP log.
    /// </summary>
    /// <value>
    /// The SOAP log.
    /// </value>
    public string SoapLog {
      get {
        return soapLog;
      }
      set {
        soapLog = value;
      }
    }

    /// <summary>
    /// Gets or sets the request log.
    /// </summary>
    /// <value>
    /// The request log.
    /// </value>
    public string RequestLog {
      get {
        return requestLog;
      }
      set {
        requestLog = value;
      }
    }

    /// <summary>
    /// Write the SOAP and HTTP trace logs.
    /// </summary>
    /// <param name="soapLog">The SOAP log.</param>
    /// <param name="requestLog">The HTTP request log.</param>
    public void Write(string soapLog, string requestLog) {
      this.soapLog = soapLog;
      this.requestLog = requestLog;
    }

    /// <summary>
    /// Resets this instance.
    /// </summary>
    public void Reset() {
      this.soapLog = this.requestLog = null;
    }
  }
}

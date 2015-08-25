// Copyright 2014, Google Inc. All Rights Reserved.
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

using System.Net;

namespace Google.Api.Ads.Common.Logging {

  /// <summary>
  /// Stores the details of an HTTP response being logged.
  /// </summary>
  public class ResponseInfo {
    /// <summary>
    /// The HTTP response headers.
    /// </summary>
    private WebHeaderCollection headers;

    /// <summary>
    /// The HTTP response body.
    /// </summary>
    private string body;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseInfo"/> class.
    /// </summary>
    public ResponseInfo() {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseInfo"/> class.
    /// </summary>
    /// <param name="headers">The HTTP response headers.</param>
    /// <param name="body">The HTTP response body.</param>
    public ResponseInfo(WebHeaderCollection headers, string body) {
      this.Headers = headers;
      this.Body = body;
    }

    /// <summary>
    /// Gets or sets the HTTP response headers.
    /// </summary>
    public WebHeaderCollection Headers {
      get {
        return headers;
      }
      set {
        headers = value;
      }
    }

    /// <summary>
    /// Gets or sets the HTTP response body.
    /// </summary>
    public string Body {
      get {
        return body;
      }
      set {
        body = value;
      }
    }
  }
}

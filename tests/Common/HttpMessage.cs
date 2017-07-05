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

namespace Google.Api.Ads.Common.Tests {
  /// <summary>
  /// Represents an HTTP request / response pair to be mocked.
  /// </summary>
  public class HttpMessage {
    /// <summary>
    /// Request body.
    /// </summary>
    string request;

    /// <summary>
    /// Response body.
    /// </summary>
    string response;

    /// <summary>
    /// Response type.
    /// </summary>
    string responseType;

    /// <summary>
    /// Overloaded constructor.
    /// </summary>
    /// <param name="request">The request body.</param>
    /// <param name="response">The response body.</param>
    /// <param name="responseType">The response type.</param>
    public HttpMessage(string request, string response, string responseType) {
      this.request = request;
      this.response = response;
      this.responseType = responseType;
    }

    /// <summary>
    /// Gets the request.
    /// </summary>
    public string Request {
      get {
        return request;
      }
    }

    /// <summary>
    /// Gets the response.
    /// </summary>
    public string Response {
      get {
        return response;
      }
    }

    /// <summary>
    /// Gets the response type.
    /// </summary>
    public string ResponseType {
      get {
        return responseType;
      }
    }
  }
}

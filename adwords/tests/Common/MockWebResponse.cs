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

using System.IO;
using System.Net;
using System.Text;

namespace Google.Api.Ads.Common.Tests {
  /// <summary>
  /// Represents a mock web response object used by WebRequestInterceptor.
  /// </summary>
  public class MockWebResponse : WebResponse {
    /// <summary>
    /// The response stream.
    /// </summary>
    MemoryStream responseStream;

    /// <summary>
    /// The response content type.
    /// </summary>
    string contentType;

    /// <summary>
    /// The HTTP response headers.
    /// </summary>
    WebHeaderCollection headers = new WebHeaderCollection();

    /// <summary>
    /// Initializes a new instance of the <see cref="MockWebResponse"/> class.
    /// </summary>
    /// <param name="mockResponse">The mock response.</param>
    /// <param name="contentType">Type of the content.</param>
    public MockWebResponse(string mockResponse, string contentType) {
      this.contentType = contentType;
      this.responseStream = new MemoryStream();

      if (mockResponse != null) {
        byte[] data = Encoding.UTF8.GetBytes(mockResponse);
        this.responseStream.Write(data, 0, data.Length);
      }
    }

    /// <summary>
    /// Returns the data stream from the Internet resource.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:System.IO.Stream"/> class for reading
    /// data from the Internet resource.
    /// </returns>
    public override Stream GetResponseStream() {
      this.responseStream.Seek(0, SeekOrigin.Begin);
      return responseStream;
    }

    /// <summary>
    /// Closes the response stream.
    /// </summary>
    public override void Close() {
      responseStream.Close();
    }

    /// <summary>
    /// Gets or sets the content type of the data being received.
    /// </summary>
    /// <returns>
    /// A string that contains the content type of the response.
    /// </returns>
    public override string ContentType {
      get {
        return contentType;
      }
      set {
        contentType = value;
      }
    }

    /// <summary>
    /// Gets or sets the content length of data being received.
    /// </summary>
    /// <returns>
    /// The number of bytes returned from the Internet resource.
    /// </returns>
    public override long ContentLength {
      get {
        return base.ContentLength;
      }
      set {
        base.ContentLength = value;
      }
    }

    /// <summary>
    /// Gets a collection of header name-value pairs associated with this
    /// request.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:System.Net.WebHeaderCollection"/> class
    /// that contains header values associated with this response.
    /// </returns>
    public override WebHeaderCollection Headers {
      get {
        return headers;
      }
    }
  }
}

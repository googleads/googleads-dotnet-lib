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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Google.Api.Ads.Common.Tests {
  /// <summary>
  /// Represents a mock web request object used by WebRequestInterceptor.
  /// </summary>
  public class MockWebRequest : WebRequest {
    /// <summary>
    /// Timeout in milliseconds.
    /// </summary>
    int timeout;

    /// <summary>
    /// The connection group name.
    /// </summary>
    string connectionGroupName;

    /// <summary>
    /// The web credentials.
    /// </summary>
    ICredentials credentials;

    /// <summary>
    /// Flag to decide whether or not to preauthenticate.
    /// </summary>
    bool preAuthenticate;

    /// <summary>
    /// HTTP request content type.
    /// </summary>
    string contentType;

    /// <summary>
    /// HTTP method name.
    /// </summary>
    string method;

    /// <summary>
    /// HTTP request headers.
    /// </summary>
    WebHeaderCollection headers = new WebHeaderCollection();

    /// <summary>
    /// Request stream.
    /// </summary>
    MemoryStream requestStream = new MemoryStream();

    /// <summary>
    /// Response for this request.
    /// </summary>
    WebResponse webResponse;

    /// <summary>
    /// Request url.
    /// </summary>
    Uri requestUri;

    /// <summary>
    /// Web proxy.
    /// </summary>
    IWebProxy proxy;

    /// <summary>
    /// Request content length in bytes.
    /// </summary>
    long contentLength;

    /// <summary>
    /// Callback delegate before sending response.
    /// </summary>
    WebRequestInterceptor.OnBeforeSendResponse onBeforeSendResponse;

    /// <summary>
    /// If set to true, then Get will return an error code rather
    /// than a successful response.
    /// </summary>
    private bool raiseException;

    /// <summary>
    /// Initializes a new instance of the <see cref="MockWebRequest"/> class.
    /// </summary>
    /// <param name="webResponse">The web response.</param>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="onBeforeSendResponse">Callback to be called before sending
    /// response.</param>
    public MockWebRequest(MockWebResponse webResponse, Uri requestUri,
        WebRequestInterceptor.OnBeforeSendResponse onBeforeSendResponse, bool raiseException) {
      this.webResponse = webResponse;
      this.requestUri = requestUri;
      this.onBeforeSendResponse = onBeforeSendResponse;
      this.raiseException = raiseException;
    }

    /// <summary>
    /// Gets or sets the content length of the request data being sent.
    /// </summary>
    /// <returns>
    /// The number of bytes of request data being sent.
    /// </returns>
    public override long ContentLength {
      get {
        return contentLength;
      }
      set {
        contentLength = value;
      }
    }

    /// <summary>
    /// Gets or sets the network proxy to use to access this Internet resource.
    /// </summary>
    /// <returns>
    /// The <see cref="T:System.Net.IWebProxy"/> to use to access the Internet
    /// resource.
    /// </returns>
    public override IWebProxy Proxy {
      get {
        return proxy;
      }
      set {
        proxy = value;
      }
    }

    /// <summary>
    /// Gets or sets the length of time, in milliseconds, before the request
    /// times out.
    /// </summary>
    /// <returns>
    /// The length of time, in milliseconds, until the request times out, or
    /// the value <see cref="F:System.Threading.Timeout.Infinite"/> to indicate
    /// that the request does not time out. The default value is defined by the
    /// descendant class.
    /// </returns>
    public override int Timeout {
      get {
        return timeout;
      }
      set {
        timeout = value;
      }
    }

    /// <summary>
    /// Gets the URI of the Internet resource associated with the request.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Uri"/> representing the resource associated with
    /// the request
    /// </returns>
    public override Uri RequestUri {
      get {
        return requestUri;
      }
    }

    /// <summary>
    /// Gets or sets the name of the connection group for the request.
    /// </summary>
    /// <returns>
    /// The name of the connection group for the request.
    /// </returns>
    public override string ConnectionGroupName {
      get {
        return connectionGroupName;
      }
      set {
        connectionGroupName = value;
      }
    }

    /// <summary>
    /// Gets or sets the network credentials used for authenticating the request
    /// with the Internet resource.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Net.ICredentials"/> containing the authentication
    /// credentials associated with the request. The default is null.
    /// </returns>
    public override ICredentials Credentials {
      get {
        return credentials;
      }
      set {
        credentials = value;
      }
    }

    /// <summary>
    /// Indicates whether to pre-authenticate the request.
    /// </summary>
    /// <returns>true to pre-authenticate; otherwise, false.
    /// </returns>
    public override bool PreAuthenticate {
      get {
        return preAuthenticate;
      }
      set {
        preAuthenticate = value;
      }
    }

    /// <summary>
    /// Gets or sets the content type of the request data being sent.
    /// </summary>
    /// <returns>
    /// The content type of the request data.
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
    /// Gets or sets the protocol method to use in this request.
    /// </summary>
    /// <returns>
    /// The protocol method to use in this request.
    /// </returns>
    public override string Method {
      get {
        return method;
      }
      set {
        method = value;
      }
    }

    /// <summary>
    /// Gets or sets the collection of header name/value pairs associated with
    /// the request.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Net.WebHeaderCollection"/> containing the header
    /// name/value pairs associated with this request.
    /// </returns>
    public override WebHeaderCollection Headers {
      get {
        return headers;
      }
      set {
        headers = value;
      }
    }

    /// <summary>
    /// Returns a <see cref="T:System.IO.Stream"/> for writing data to the
    /// Internet resource.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.IO.Stream"/> for writing data to the Internet
    /// resource.
    /// </returns>
    public override Stream GetRequestStream() {
      return requestStream;
    }

    /// <summary>
    /// Returns a response to an Internet request.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Net.WebResponse"/> containing the response to the
    /// Internet request.
    /// </returns>
    public override WebResponse GetResponse() {
      onBeforeSendResponse(this.requestUri, headers, Encoding.UTF8.GetString(
          requestStream.ToArray()));
      if (raiseException) {
        throw new WebException("", null, WebExceptionStatus.ProtocolError, webResponse);
      } else {
        return webResponse;
      }
    }
  }
}

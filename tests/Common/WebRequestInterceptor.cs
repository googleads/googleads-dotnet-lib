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
using System.Net;
using System.Reflection;
using System.Text;

namespace Google.Api.Ads.Common.Tests {
  /// <summary>
  /// Intercepts a web request for testing purposes.
  /// </summary>
  public abstract class WebRequestInterceptor : IWebRequestCreate {
    /// <summary>
    /// Callback delegate for listening to outgoing requests.
    /// </summary>
    /// <param name="uri">The URI to which calls are being made.</param>
    /// <param name="headers">The HTTP request headers.</param>
    /// <param name="body">The HTTP request body.</param>
    public delegate void OnBeforeSendResponse(Uri uri, WebHeaderCollection headers, String body);

    /// <summary>
    /// Callback for listening to outgoing request.
    /// </summary>
    private OnBeforeSendResponse beforeSendResponse = OnBeforeSendResponseCallback;

    /// <summary>
    /// True, to intercept a request, false to bypass the request.
    /// </summary>
    bool intercept;

    /// <summary>
    /// If set to true, then the interceptor will return an error code rather
    /// than a successful response.
    /// </summary>
    private bool raiseException;

    /// <summary>
    /// Gets or sets a value indicating whether this handler should intercept
    /// a request or not.
    /// </summary>
    public bool Intercept {
      get {
        return intercept;
      }
      set {
        intercept = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to return an error code rather
    /// than a successful response.
    /// </summary>
    public bool RaiseException {
      get {
        return raiseException;
      }
      set {
        raiseException = value;
      }
    }

    /// <summary>
    /// Gets or sets the beforeSendResponse callback.
    /// </summary>
    public OnBeforeSendResponse BeforeSendResponse {
      get {
        return beforeSendResponse;
      }
      set {
        beforeSendResponse = value;
      }
    }

    /// <summary>
    /// Default handler for OnBeforeSendResponse.
    /// </summary>
    /// <param name="uri">The URI to which calls are being made.</param>
    /// <param name="headers">The HTTP request headers.</param>
    /// <param name="body">The HTTP request body.</param>
    static void OnBeforeSendResponseCallback(Uri uri, WebHeaderCollection headers, string body) {
    }

    /// <summary>
    /// Gets the next message to be mocked.
    /// </summary>
    /// <returns>An HttpMessage object that represents the next message.</returns>
    public abstract HttpMessage GetNextMessage();

    /// <summary>
    /// Gets the default HTTP handler.
    /// </summary>
    /// <param name="uri">The uri to handler.</param>
    /// <returns>The web request handler.</returns>
    /// <remarks>This is a slightly hacky method to get the default handler.
    /// We are using reflection to call the internal constructor of
    /// HttpWebRequest, since WebRequest.RegisterPrefix neither allows us to
    /// unregister the handler for a prefix, nor gives us back the old prefix.
    /// </remarks>
    protected static WebRequest GetDefaultHttpHandler(Uri uri) {
      ConstructorInfo ci = typeof(HttpWebRequest).GetConstructor(BindingFlags.NonPublic |
          BindingFlags.Instance, null, new Type[] {typeof(Uri), typeof(ServicePoint)}, null);
      return (HttpWebRequest) ci.Invoke(new object[] {uri, null});
    }

    /// <summary>
    /// Creates a <see cref="T:System.Net.WebRequest"/> instance.
    /// </summary>
    /// <param name="uri">The uniform resource identifier (URI) of the Web
    /// resource.</param>
    /// <returns>
    /// A <see cref="T:System.Net.WebRequest"/> instance.
    /// </returns>
    public WebRequest Create(Uri uri) {
      if (Intercept) {
        HttpMessage message = GetNextMessage();
        MockWebResponse response = new MockWebResponse(message.Response, message.ResponseType);
        return new MockWebRequest(response, uri, beforeSendResponse, RaiseException);
      } else {
        return GetDefaultHttpHandler(uri);
      }
    }
  }
}

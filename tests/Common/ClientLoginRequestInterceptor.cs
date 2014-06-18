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
using System.Collections.Generic;
using System.Net;
using System.Text;

// Disable deprecation warnings for AuthToken class.
#pragma warning disable 612, 618

namespace Google.Api.Ads.Common.Tests {
  /// <summary>
  /// Intercepts calls to ClientLogin API for testing purposes.
  /// </summary>
  public class ClientLoginRequestInterceptor : WebRequestInterceptor {
    /// <summary>
    /// AuthToken returned by interceptor.
    /// </summary>
    public const string AUTH_TOKEN = "AUTH_TOKEN";

    /// <summary>
    /// Standard response for ClientLogin calls.
    /// </summary>
    private const string CLIENTLOGIN_RESPONSE = "SID=DQAAA\r\nLSID=DQAAA\r\nAuth=" + AUTH_TOKEN;

    /// <summary>
    /// Error response for ClientLogin calls.
    /// </summary>
    private const string CLIENTLOGIN_ERROR_RESPONSE =
        "Error=BadAuthentication\r\nUrl=http://localhost";

    /// <summary>
    /// Singleton instance.
    /// </summary>
    private static WebRequestInterceptor instance;


    /// <summary>
    /// Content type for ClientLogin API calls.
    /// </summary>
    private const string CLIENTLOGIN_RESPONSE_TYPE = "text/plain";

    /// <summary>
    /// Gets the only instance.
    /// </summary>
    public static WebRequestInterceptor Instance {
      get {
        return instance;
      }
    }

    /// <summary>
    /// Initializes the <see cref="AdWordsRequestInterceptor"/> class.
    /// </summary>
    static ClientLoginRequestInterceptor() {
      instance = new ClientLoginRequestInterceptor();
      WebRequest.RegisterPrefix(AuthToken.Url.AbsoluteUri, instance);
    }

    /// <summary>
    /// Gets the next message to be mocked.
    /// </summary>
    /// <returns>
    /// An HttpMessage object that represents the next message.
    /// </returns>
    public override HttpMessage GetNextMessage() {
      if (RaiseException) {
        return new HttpMessage(null, CLIENTLOGIN_ERROR_RESPONSE, CLIENTLOGIN_RESPONSE_TYPE);
      } else {
        return new HttpMessage(null, CLIENTLOGIN_RESPONSE, CLIENTLOGIN_RESPONSE_TYPE);
      }
    }
  }
}

#pragma warning restore 612, 618

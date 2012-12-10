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
using Google.Api.Ads.Common.Tests;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Google.Api.Ads.Common.OAuth.Tests {
  /// <summary>
  /// Intercepts calls to ClientLogin API for testing purposes.
  /// </summary>
  public class OAuth2RequestInterceptor : WebRequestInterceptor {
    /// <summary>
    /// Type of OAuth2 requests that can be mocked by this interceptor.
    /// </summary>
    public enum OAuth2RequestType {
      FetchAccessAndRefreshToken,
      FetchAccessTokenForServiceAccount,
      RefreshAccessToken,
      RevokeRefresshToken
    }

    /// <summary>
    /// Type of OAuth2 request being mocked by this interceptor.
    /// </summary>
    private OAuth2RequestType requestType;

    /// <summary>
    /// Url prefix for OAuth2 requests.
    /// </summary>
    public const string OAUTH2_URL_PREFIX = "https://accounts.google.com/o/oauth2";

    /// <summary>
    /// Access token returned by interceptor.
    /// </summary>
    public const string ACCESS_TOKEN = "ACCESS_TOKEN";

    /// <summary>
    /// Refresh token returned by interceptor.
    /// </summary>
    public const string REFRESH_TOKEN = "REFRESH_TOKEN";

    /// <summary>
    /// Access token type returned by interceptor.
    /// </summary>
    public const string ACCESS_TOKEN_TYPE = "Bearer";

    /// <summary>
    /// Access token expiration returned by interceptor.
    /// </summary>
    public const string EXPIRES_IN = "3600";

    /// <summary>
    /// Mock response when requesting access token for serviced accounts.
    /// </summary>
    private const string SERVICED_ACCOUNT_RESPONSE = "{\r\n\"access_token\" : \"" +
        ACCESS_TOKEN + "\",\r\n\"token_type\" : \"" + ACCESS_TOKEN_TYPE +
        "\",\r\n \"expires_in\" : " + EXPIRES_IN + "\r\n}";

    /// <summary>
    /// Mock response when reqquesting access token in offline mode for
    /// installed clients.
    /// </summary>
    private const string ACCESS_REFRESH_TOKEN_RESPONSE = "{\r\n\"access_token\" " +
        ": \"" + ACCESS_TOKEN + "\",\r\n" +
        "\"token_type\" : \"" + ACCESS_TOKEN_TYPE + "\",\r\n\"expires_in\" : " +
        EXPIRES_IN + ",\"refresh_token\"" + " : \"" + REFRESH_TOKEN + "\"\r\n}";

    /// <summary>
    /// Mock response when refreshing access token for installed clients.
    /// </summary>
    private const string REFRESH_TOKEN_RESPONSE = "{\r\n\"access_token\" : \"" +
        ACCESS_TOKEN + "\",\r\n\"token_type\" : \"" + ACCESS_TOKEN_TYPE +
        "\", \"expires_in\" : " + EXPIRES_IN + "}";

    /// <summary>
    /// Content type for OAuth2 API calls.
    /// </summary>
    private const string OAUTH2_RESPONSE_TYPE = "application/json";

    /// <summary>
    /// Gets the only instance.
    /// </summary>
    public static WebRequestInterceptor Instance {
      get {
        return instance;
      }
    }

    /// <summary>
    /// Gets or sets the type of the request being handled by this interceptor.
    /// </summary>
    public OAuth2RequestType RequestType {
      get {
        return requestType;
      }
      set {
        requestType = value;
      }
    }

    /// <summary>
    /// Initializes the <see cref="OAuth2RequestInterceptor"/> class.
    /// </summary>
    static OAuth2RequestInterceptor() {
      instance = new OAuth2RequestInterceptor();
      WebRequest.RegisterPrefix(OAUTH2_URL_PREFIX, instance);
    }

    /// <summary>
    /// Gets the next message to be mocked.
    /// </summary>
    /// <returns>
    /// An HttpMessage object that represents the next message.
    /// </returns>
    public override HttpMessage GetNextMessage() {
      string message = null;
      switch (RequestType) {
        case OAuth2RequestType.FetchAccessTokenForServiceAccount:
          message = SERVICED_ACCOUNT_RESPONSE;
          break;

        case OAuth2RequestType.FetchAccessAndRefreshToken:
          message = ACCESS_REFRESH_TOKEN_RESPONSE;
          break;

        case OAuth2RequestType.RefreshAccessToken:
          message = REFRESH_TOKEN_RESPONSE;
          break;

        case OAuth2RequestType.RevokeRefresshToken:
          message = "";
          break;
      }
      return new HttpMessage(null, message, OAUTH2_RESPONSE_TYPE);
    }
  }
}

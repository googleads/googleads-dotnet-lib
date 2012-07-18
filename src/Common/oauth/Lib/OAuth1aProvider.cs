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

using Microsoft.Practices.ServiceLocation;
using OAuth.Net.Components;
using OAuth.Net.Common;
using OAuth.Net.Consumer;

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace Google.Api.Ads.Common.OAuth.Lib {
  /// <summary>
  /// Provides a default implementation of OAuth1a protocol using OAuth.Net
  /// library.
  /// </summary>
  public class OAuth1aProvider : OAuthRequest, OAuth1a {
    /// <summary>
    /// The OAuth endpoint for obtaining request token.
    /// </summary>
    const string RequestTokenUrl = "https://www.google.com/accounts/OAuthGetRequestToken";

    /// <summary>
    /// The OAuth endpoint for authorizing request token.
    /// </summary>
    const string AuthorizationUrl = "https://www.google.com/accounts/OAuthAuthorizeToken";

    /// <summary>
    /// The OAuth endpoint for obtaining access token.
    /// </summary>
    const string AccessTokenUrl = "https://www.google.com/accounts/OAuthGetAccessToken";

    /// <summary>
    /// Default signature method supported by this class.
    /// </summary>
    const string SignatureMethod = "HMAC-SHA1";

    /// <summary>
    /// OAuth version supported by this class.
    /// </summary>
    const string OAuthVersion = "1.0";

    /// <summary>
    /// The configuration class for getting application settings.
    /// </summary>
    private AppConfigBase config;

    /// <summary>
    /// The ads service locator.
    /// </summary>
    static AdsServiceLocator injector = GetInjector();

    /// <summary>
    /// Gets or sets the configuration class for getting application settings.
    /// </summary>
    public AppConfigBase Config {
      get {
        return config;
      }
      set {
        config = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth1aProvider"/> class.
    /// </summary>
    /// <param name="config">The configuration object to be used to configure
    /// this class.</param>
    public OAuth1aProvider(AppConfigBase config)
      : this(OAuthService.Create(new EndPoint(
            RequestTokenUrl + "?scope=" + config.OAuthScope, "POST"), new Uri(AuthorizationUrl),
            new EndPoint(AccessTokenUrl, "POST"), true, "", SignatureMethod, OAuthVersion,
            new OAuthConsumer(config.OAuthConsumerKey, config.OAuthConsumerSecret)),
            config.OAuthCallbackUrl) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth1aProvider"/> class.
    /// </summary>
    /// <param name="service">The OAuthService class.</param>
    /// <param name="callbackUrl">The callback URL.</param>
    protected OAuth1aProvider(OAuthService service, string callbackUrl)
      : base(new EndPoint("http://localhost", "POST"), service, null,
          service.ComponentLocator.GetInstance<IRequestStateStore>(),
          new RequestStateKey(service, String.Empty)) {
      this.CallbackUrl = (callbackUrl == null) ? null : new Uri(callbackUrl);
    }

    /// <summary>
    /// Intitializes and returns an ads service locator.
    /// </summary>
    /// <returns>The ads service locator.</returns>
    /// <remarks>This is required for OAuth.NET library, since it uses
    /// Microsoft.Practices.ServiceLocation to create various types using
    /// dependency injection.</remarks>
    private static AdsServiceLocator GetInjector() {
      AdsServiceLocator injector = new AdsServiceLocator();
      injector.UseMemoryStore = true;
      ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(delegate() {
        return injector;
      }));
      return injector;
    }

    /// <summary>
    /// Configures the HTTP connection.
    /// </summary>
    /// <param name="request">The web request object.</param>
    /// <returns>
    /// The configured web request object.
    /// </returns>
    protected override System.Net.HttpWebRequest ConfigureConnection(
        System.Net.HttpWebRequest request) {
      if (config != null) {
        request.Proxy = config.Proxy;
        request.Timeout = config.Timeout;
      }
      return request;
    }

    #region IOAuth1a Members

    /// <summary>
    /// Fetches the request token.
    /// </summary>
    public void FetchRequestToken() {
      DoGetRequestToken();
    }

    /// <summary>
    /// Gets the authorization URL.
    /// </summary>
    /// <returns>
    /// The authorization url.
    /// </returns>
    public string GetAuthorizationUrl() {
      if (this.RequestToken == null) {
        FetchRequestToken();
      }
      return this.Service.BuildAuthorizationUrl(this.RequestToken).AbsoluteUri;
    }

    /// <summary>
    /// Fetches the access token.
    /// </summary>
    /// <param name="authorizationCode">The verifier token returned by the OAuth
    /// server on callback.</param>
    /// <returns>
    /// True if the access token was fetched correctly, false otherwise.
    /// </returns>
    public bool FetchAccessAndRefreshTokens(string authorizationCode) {
      this.VerificationHandler =
          delegate(object sender, AuthorizationVerificationEventArgs authArgs) {
            authArgs.Verifier = authorizationCode;
          };
      DoCollectVerifier();

      bool retval = DoGetAccessToken();

      if (this.OnOAuthTokensObtained != null) {
        this.OnOAuthTokensObtained(this);
      }

      return retval;
    }

    /// <summary>
    /// Gets the AuthorizationHeader value to be set on outgoing HTTP calls.
    /// </summary>
    /// <param name="apiCallUrl">The URL to which API call is being made.
    /// </param>
    /// <returns>The AuthorizationHeader value to be set on outgoing HTTP calls.
    /// </returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public string GetAuthHeader(string apiCallUrl) {
      this.ResourceEndPoint = new EndPoint(apiCallUrl, "POST");
      System.Net.HttpWebRequest webRequest = DoPrepareProtectedResourceRequest(null,
          Constants.HttpPostUrlEncodedContentType, null);
      return webRequest.Headers["Authorization"];
    }

    #endregion

    #region AdsOAuthProvider

    /// <summary>
    /// Callback triggered when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    OAuthTokensObtainedCallback oAuthTokensObtained =
        new OAuthTokensObtainedCallback(TokensUpdatedCallback);

    /// <summary>
    /// Callback triggered when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    public OAuthTokensObtainedCallback OnOAuthTokensObtained {
      get {
        return oAuthTokensObtained;
      }
    }

    /// <summary>
    /// Default callback when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    /// <param name="provider">The provider.</param>
    private static void TokensUpdatedCallback(AdsOAuthProvider provider) {
    }

    #endregion
  }
}

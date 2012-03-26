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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Common.Lib;

using OAuth.Net.Common;
using OAuth.Net.Components;
using OAuth.Net.Consumer;

using System;
using System.Runtime.CompilerServices;
using System.Web;

namespace Google.Api.Ads.Common.OAuth.Lib {
  /// <summary>
  /// Provides OAuth authorization mechanism for Ads services using OAuth.Net
  /// library.
  /// </summary>
  public class AdsOAuthNetProvider : OAuthRequest, AdsOAuthProvider {
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
    /// A default OAuth consumer key for testing purposes.
    /// </summary>
    const string DefaultOAuthConsumerKey = "anonymous";

    /// <summary>
    /// A default OAuth consumer secret for testing purposes.
    /// </summary>
    const string DefaultOAuthConsumerSecret = "anonymous";

    /// <summary>
    /// Default signature method supported by this class.
    /// </summary>
    const string SignatureMethod = "HMAC-SHA1";

    /// <summary>
    /// OAuth version supported by this class.
    /// </summary>
    const string OAuthVersion = "1.0";

    /// <summary>
    /// Initializes a new instance of the <see cref="AdsOAuthNetProvider"/>
    /// class.
    /// </summary>
    /// <param name="scope">The OAuth scope.</param>
    /// <param name="callbackUrl">The OAuth callback URL.</param>
    /// <param name="userId">A unique string to identify a user session. If
    /// this is a web application, this value could be
    /// <code>HttpContext.Current.Session.SessionID</code>.</param>
    public AdsOAuthNetProvider(string scope, string callbackUrl, string userId)
      : this(DefaultOAuthConsumerKey, DefaultOAuthConsumerSecret, scope, callbackUrl, userId) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdsOAuthNetProvider"/>
    /// class.
    /// </summary>
    /// <param name="consumerKey">The OAuth consumer key.</param>
    /// <param name="consumerSecret">The OAuth consumer secret.</param>
    /// <param name="scope">The OAuth scope.</param>
    /// <param name="callbackUrl">The callback URL.</param>
    /// <param name="userId">A unique string to identify a user session. If
    /// this is a web application, this value could be
    /// <code>HttpContext.Current.Session.SessionID</code>.</param>
    public AdsOAuthNetProvider(string consumerKey, string consumerSecret, string scope,
        string callbackUrl, string userId) : this(OAuthService.Create(new EndPoint(
            RequestTokenUrl + "?scope=" + scope, "POST"), new Uri(AuthorizationUrl),
            new EndPoint(AccessTokenUrl, "POST"), true, "", SignatureMethod, OAuthVersion,
            new OAuthConsumer(consumerKey, consumerSecret)), callbackUrl, userId) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdsOAuthNetProvider"/>
    /// class.
    /// </summary>
    /// <param name="service">The OAuth service settings.</param>
    /// <param name="callbackUrl">The callback URL.</param>
    /// <param name="userId">A unique string to identify a user session. If
    /// this is a web application, this value could be
    /// <code>HttpContext.Current.Session.SessionID</code>.</param>
    protected AdsOAuthNetProvider(OAuthService service, string callbackUrl, string userId)
      : base(new EndPoint("http://localhost", "POST"), service, null,
          service.ComponentLocator.GetInstance<IRequestStateStore>(),
          new RequestStateKey(service, userId)) {
      this.CallbackUrl = (callbackUrl == null)? null : new Uri(callbackUrl);
      this.AuthorizationHandler = AspNetOAuthRequest.HandleAuthorization;
      this.VerificationHandler = AspNetOAuthRequest.HandleVerification;
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

    /// <summary>
    /// Generates the OAuth access token.
    /// </summary>
    public void GenerateAccessToken() {
      try {
        if (this.AccessToken == null) {
          if (this.RequestToken == null) {
            // Get a request token
            DoGetRequestToken();

            if (this.RequestToken == null) {
              throw new InvalidOperationException("Request token was not received.");
            }

            if (!DoAuthorizeRequestToken()) {
              return;
            }
          }

          if (string.IsNullOrEmpty(this.RequestTokenVerifier)) {
            DoCollectVerifier();
          }

          if (string.IsNullOrEmpty(this.RequestTokenVerifier)) {
            return;
          }

          // Get the access token - this will return false if the verifier is not provided
          // the implementation needs to get the user to re-authenticate.
          if (!DoGetAccessToken()) {
            return;
          }
        }
        return;
      } catch (OAuthRequestException ex) {
        throw new AdsOAuthException("OAuth server threw an exception." + ex.Problem);
      }
    }
  }
}

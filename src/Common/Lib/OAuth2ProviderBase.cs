// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace Google.Api.Ads.Common.Lib {

  public abstract class OAuth2ProviderBase : AdsOAuthProvider {

    /// <summary>
    /// The registry for saving feature usage information..
    /// </summary>
    protected readonly AdsFeatureUsageRegistry featureUsageRegistry =
        AdsFeatureUsageRegistry.Instance;

    /// <summary>
    /// Gets the OAuth2 server URL.
    /// </summary>
    protected string OAUTH_SERVER {
      get {
        return Config.OAuth2ServerUrl;
      }
    }

    /// <summary>
    /// The OAuth2 endpoint for obtaining an authorization token.
    /// </summary>
    protected string AUTH_ENDPOINT {
      get {
        return OAUTH_SERVER + "/o/oauth2/auth";
      }
    }

    /// <summary>
    /// The OAuth2 endpoint for obtaining or refreshing an access token.
    /// </summary>
    protected string TOKEN_ENDPOINT {
      get {
        return OAUTH_SERVER + "/o/oauth2/token";
      }
    }

    /// <summary>
    /// The OAuth2 tokens will be refreshed automatically if the time left for
    /// access token expiry is less than this value in seconds.
    /// </summary>
    private int oAuth2RefreshCutoffLimit = 60;

    /// <summary>
    /// A parameter that your application can use for keeping state. The
    /// OAuth Authorization Server roundtrips this parameter.
    /// </summary>
    private string state;

    /// <summary>
    /// Indicates the type of token returned by the server. This field will
    /// always have the value Bearer for now.
    /// </summary>
    private string tokenType;

    /// <summary>
    /// The time at which access token was updated.
    /// </summary>
    private DateTime updatedOn = DateTime.MinValue;

    /// <summary>
    /// The remaining lifetime on the access token.
    /// </summary>
    private int expiresIn;

    /// <summary>
    /// The configuration object to be used with this client.
    /// </summary>
    protected AppConfig config = null;

    /// <summary>
    /// The prefix text used by Google for the private key property in the JSON
    /// secrets file.
    /// </summary>
    private const string PRIVATE_KEY_PREFIX = "-----BEGIN PRIVATE KEY-----";

    /// <summary>
    /// The suffix text used by Google for the private key property in the JSON
    /// secrets file.
    /// </summary>
    private const string PRIVATE_KEY_SUFFIX = "-----END PRIVATE KEY-----";

    /// <summary>
    /// The list of headers to mask in the logs.
    /// </summary>
    private readonly ISet<string> REQUEST_HEADERS_TO_MASK = new HashSet<string>() {
        "client_secret",
        "refresh_token"
    };

    /// <summary>
    /// The list of fields to mask in the response logs.
    /// </summary>
    private readonly ISet<string> RESPONSE_FIELDS_TO_MASK = new HashSet<string>() {
        "access_token"
    };

    /// <summary>
    /// Callback triggered when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    private OAuthTokensObtainedCallback oAuthTokensObtained =
        new OAuthTokensObtainedCallback(TokensUpdatedCallback);

    /// <summary>
    /// Gets the config.
    /// </summary>
    public AppConfig Config {
      get {
        return config;
      }
    }

    /// <summary>
    /// Gets or sets the client that is making the request. This value is
    /// obtained from the <a href="https://code.google.com/apis/console">
    /// API console</a> during application registration.
    /// </summary>
    public string ClientId {
      get {
        return config.OAuth2ClientId;
      }
      set {
        config.OAuth2ClientId = value;
      }
    }

    /// <summary>
    /// Gets or sets the client secret obtained from the
    /// <a href="https://code.google.com/apis/console">API console</a>
    /// during application registration.during application registration.
    /// </summary>
    public string ClientSecret {
      get {
        return config.OAuth2ClientSecret;
      }
      set {
        config.OAuth2ClientSecret = value;
      }
    }

    /// <summary>
    /// Gets or sets the API access your application is requesting. This is
    /// space delimited.
    /// </summary>
    public string Scope {
      get {
        return config.OAuth2Scope;
      }
      set {
        config.OAuth2Scope = value;
      }
    }

    /// <summary>
    /// Gets or sets a parameter that your application can use for keeping
    /// state. The OAuth Authorization Server roundtrips this parameter.
    /// </summary>
    public string State {
      get {
        return state;
      }
      set {
        state = value;
      }
    }

    /// <summary>
    /// Gets the type of token returned by the server. This field will
    /// always have the value Bearer for now.
    /// </summary>
    public string TokenType {
      get {
        return tokenType;
      }
    }

    /// <summary>
    /// Gets or sets the token that can be sent to a Google API for
    /// authentication.
    /// </summary>
    public string AccessToken {
      get {
        return config.OAuth2AccessToken;
      }
      set {
        config.OAuth2AccessToken = value;
      }
    }

    /// <summary>
    /// Gets or sets a token that may be used to obtain a new access token.
    /// Refresh tokens are valid until the user revokes access.
    /// </summary>
    public string RefreshToken {
      get {
        return config.OAuth2RefreshToken;
      }
      set {
        config.OAuth2RefreshToken = value;
      }
    }

    /// <summary>
    /// Gets or sets the time at which access token was retrieved.
    /// </summary>
    public DateTime UpdatedOn {
      get {
        return updatedOn;
      }
      set {
        updatedOn = value;
      }
    }

    /// <summary>
    /// Gets the remaining lifetime on the access token.
    /// </summary>
    public int ExpiresIn {
      get {
        return expiresIn;
      }
      set {
        expiresIn = value;
      }
    }

    /// <summary>
    /// Callback triggered when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    public OAuthTokensObtainedCallback OnOAuthTokensObtained {
      get {
        return oAuthTokensObtained;
      }
      set {
        oAuthTokensObtained = value;
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 refresh cutoff limit.
    /// </summary>
    public int OAuth2RefreshCutoffLimit {
      get {
        return oAuth2RefreshCutoffLimit;
      }
      set {
        oAuth2RefreshCutoffLimit = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2"/> class.
    /// </summary>
    /// <param name="config">The config.</param>
    public OAuth2ProviderBase(AppConfig config) {
      this.config = config;
    }

    /// <summary>
    /// Gets the auth header.
    /// </summary>
    /// <returns>The auth header.</returns>
    public virtual string GetAuthHeader() {
      RefreshAccessTokenIfExpiring();
      return "Bearer " + this.AccessToken;
    }

    /// <summary>
    /// Refreshes the access token if expiring.
    /// </summary>
    public virtual void RefreshAccessTokenIfExpiring() {
      if (IsAccessTokenExpiring()) {
        RefreshAccessToken();
      }
    }

    /// <summary>
    /// Refreshes the access token if expiring.
    /// </summary>
    public abstract void RefreshAccessToken();

    /// <summary>
    /// Determines whether the access token for a provider is expiring.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <returns>True if the token is expiring, false otherwise.</returns>
    private bool IsAccessTokenExpiring() {
      if (this.UpdatedOn == DateTime.MinValue) {
        return true;
      }
      return this.UpdatedOn +
          new TimeSpan(0, 0, this.ExpiresIn - oAuth2RefreshCutoffLimit) < DateTime.UtcNow;
    }

    /// <summary>
    /// Calls the token endpoint to obtain an access token.
    /// </summary>
    /// <param name="body">The request body.</param>
    /// <param name="errorMessage">The error message.</param>
    protected void CallTokenEndpoint(string body) {
      WebRequest request = HttpUtilities.BuildRequest(TOKEN_ENDPOINT, "POST", config);
      request.ContentType = "application/x-www-form-urlencoded";

      LogEntry logEntry = new LogEntry(config, new DefaultDateTimeProvider());
      WebResponse response = null;

      try {
        HttpUtilities.WritePostBodyAndLog(request, body, logEntry, REQUEST_HEADERS_TO_MASK);
        response = request.GetResponse();

        string contents = MediaUtilities.GetStreamContentsAsString(response.GetResponseStream());
        logEntry.LogResponse(response, false, contents, RESPONSE_FIELDS_TO_MASK,
            new JsonBodyFormatter());
        logEntry.Flush();

        Dictionary<string, string> values = ParseJsonObjectResponse(contents);
        this.AccessToken =
            CollectionUtilities.TryGetValue(values, "access_token", this.AccessToken);
        this.RefreshToken =
            CollectionUtilities.TryGetValue(values, "refresh_token", this.RefreshToken);
        this.tokenType =
            CollectionUtilities.TryGetValue(values, "token_type", this.tokenType);
        this.expiresIn = int.Parse(
            CollectionUtilities.TryGetValue(values, "expires_in", this.expiresIn.ToString()));
        this.updatedOn = DateTime.UtcNow;

        if (this.OnOAuthTokensObtained != null) {
          this.OnOAuthTokensObtained(this);
        }
      } catch (WebException e) {
        string contents = HttpUtilities.GetErrorResponseBody(e);
        logEntry.LogResponse(response, true, contents, RESPONSE_FIELDS_TO_MASK,
            new JsonBodyFormatter());
        logEntry.Flush();

        throw new ApplicationException(contents, e);
      } finally {
        if (response != null) {
          response.Close();
        }
      }
    }

    /// <summary>
    /// Converts the PKCS8 private key to RSA parameters.
    /// </summary>
    /// <param name="privateKey">The private key read from the JSON secrets file.</param>
    /// <returns>The RSA parameters for generating signature.</returns>
    protected static RSAParameters ConvertPKCS8ToRsaParameters(string privateKey) {
      var base64PrivateKey = privateKey.Replace(PRIVATE_KEY_PREFIX, "").Replace("\n", "")
                .Replace(PRIVATE_KEY_SUFFIX, "");
      var privateKeyBytes = Convert.FromBase64String(base64PrivateKey);
      AsymmetricKeyParameter key = PrivateKeyFactory.CreateKey(privateKeyBytes);
      return DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters) key);
    }

    /// <summary>
    /// Converts the provided P12 file to RSA parameters.
    /// </summary>
    /// <param name="pathToP12File">The file system path to the P12 file.</param>
    /// <param name="password">The P12 file password.</param>
    /// <returns>The RSA parameters for generating signature.</returns>
    protected static RSAParameters ConvertP12ToRsaParameters(String pathToP12File,
      String password) {
      using (FileStream stream = File.OpenRead(pathToP12File)) {
        Pkcs12Store store = new Pkcs12Store(stream, password.ToCharArray());

        foreach (string alias in store.Aliases) {
          if (store.IsKeyEntry(alias)) {
            AsymmetricKeyParameter key = store.GetKey(alias).Key;
            if (key.IsPrivate) {
              return DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters) key);
            }
          }
        }
      }

      // If we get here then no private key could be found, which warrants an error.
      throw new ArgumentException(CommonErrorMessages.OAuth2JwtCertificateInvalid);
    }

    /// <summary>
    /// Gets the RSA sha256 signature for data.
    /// </summary>
    /// <param name="rsa">The RSA parameters to use for signing.</param>
    /// <param name="data">The data for which signature should be calculated.
    /// </param>
    /// <returns>The signature.</returns>
    protected static byte[] GetRsaSha256Signature(RSAParameters rsaParameters, byte[] data) {
      RSACryptoServiceProvider rsaCsp = new RSACryptoServiceProvider();
      rsaCsp.ImportParameters(rsaParameters);

      byte[] signature = rsaCsp.SignData(data, "SHA256");
      bool results = rsaCsp.VerifyData(data, "SHA256", signature);
      return signature;
    }

    /// <summary>
    /// Generates a url-safe base64 encoded string.
    /// </summary>
    /// <param name="data">The data to be base64-encoded.</param>
    /// <returns>The base64 encoded string.</returns>
    protected static string Base64UrlEncode(byte[] data) {
      return Convert.ToBase64String(data).Replace("=", String.Empty).Replace('+', '-').
          Replace('/', '_');
    }

    /// <summary>
    /// Parses a JSON object response.
    /// </summary>
    /// <param name="contents">The JSON contents.</param>
    /// <returns>A dictionary of key-value pairs.</returns>
    protected Dictionary<string, string> ParseJsonObjectResponse(string contents) {
      JavaScriptSerializer serializer = new JavaScriptSerializer();
      return serializer.Deserialize<Dictionary<string, string>>(contents);
    }

    /// <summary>
    /// Validates if a required OAuth2 parameter is null or empty.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="propertyValue">The property value.</param>
    protected static void ValidateOAuth2Parameter(string propertyName, string propertyValue) {
      if (string.IsNullOrEmpty(propertyValue)) {
        throw new ArgumentNullException(
            string.Format(CommonErrorMessages.OAuth2ParameterErrorMessage, propertyName));
      }
    }

    /// <summary>
    /// Default callback when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    /// <param name="provider">The provider.</param>
    protected static void TokensUpdatedCallback(AdsOAuthProvider provider) {
    }
  }
}

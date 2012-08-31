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
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Headers;

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Google.Api.Ads.Dfp.Lib {
  /// <summary>
  /// Base class for DFP API services.
  /// </summary>
  public class DfpSoapClient : AdsSoapClient {
    /// <summary>
    /// The error thrown when an auth token expires.
    /// </summary>
    private const string COOKIE_INVALID_ERROR = "AuthenticationError.GOOGLE_ACCOUNT_COOKIE_INVALID";

    /// <summary>
    /// The error thrown when an oauth token expires.
    /// </summary>
    private const string OAUTH_TOKEN_EXPIRED_ERROR = "AuthenticationError.AUTHENTICATION_FAILED";

    /// <summary>
    /// The service name for use with ClientLogin server.
    /// </summary>
    private const string SERVICE_NAME = "gam";

    /// <summary>
    /// Gets a custom exception that wraps the SOAP exception thrown
    /// by the server.
    /// </summary>
    /// <param name="ex">SOAPException that was thrown by the server.</param>
    /// <returns>A custom exception object that wraps the SOAP exception.
    /// </returns>
    protected override Exception GetCustomException(SoapException ex) {
      string defaultNs = GetDefaultNamespace();
      if (!string.IsNullOrEmpty(defaultNs)) {
        // Extract the ApiExceptionFault node.
        XmlElement faultNode = GetFaultNode(ex, defaultNs, "ApiExceptionFault");

        if (faultNode != null) {
          try {
            return new DfpApiException(SerializationUtilities.DeserializeFromXmlTextCustomRootNs(
                faultNode.OuterXml, Assembly.GetExecutingAssembly().GetType(
                    this.GetType().Namespace + ".ApiException"), defaultNs, "ApiExceptionFault"),
                ex.Message, ex);
          } catch (Exception) {
            // deserialization failed, but we can safely ignore it.
          }
        }
      }
      return new DfpApiException(null, ex.Message, ex);
    }

    /// <summary>
    /// Initializes the service before MakeApiCall.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">The method parameters.</param>
    protected override void InitForCall(string methodName, object[] parameters) {
      DfpAppConfig config = this.User.Config as DfpAppConfig;
      RequestHeader header = (RequestHeader) this.GetType().GetProperty("RequestHeader").
          GetValue(this, null);

      if (header == null) {
        throw new DfpApiException(null, DfpErrorMessages.FailedToSetAuthorizationHeader);
      }

      if (!(this.GetType().Name == "NetworkService" && (methodName == "getAllNetworks"
          || methodName == "makeTestNetwork"))) {
        if (string.Compare(header.Version, "v201203") >= 0 && string.IsNullOrEmpty(
            header.networkCode)) {
          throw new SoapHeaderException("networkCode header is required in all API versions >= " +
              "v201203. The only exceptions are NetworkService.getAllNetworks and " +
              "NetworkService.makeTestNetwork.", XmlQualifiedName.Empty);
        }
      }

      if (config.AuthorizationMethod == DfpAuthorizationMethod.OAuth ||
          config.AuthorizationMethod == DfpAuthorizationMethod.OAuth2) {
        if (this.User.OAuthProvider != null) {
          OAuth oAuth = (header.authentication as OAuth) ?? new OAuth();
          oAuth.parameters = this.User.OAuthProvider.GetAuthHeader(this.Url);
          header.authentication = oAuth;
        } else {
          throw new DfpApiException(null, DfpErrorMessages.OAuthProviderCannotBeNull);
        }
      } else if (config.AuthorizationMethod == DfpAuthorizationMethod.ClientLogin) {
        string authToken = (!string.IsNullOrEmpty(config.AuthToken)) ? config.AuthToken :
            new AuthToken(config, SERVICE_NAME, config.Email, config.Password).GetToken();
        if (string.Compare(header.Version, "v201103") < 0) {
          header.authToken = authToken;
        } else {
          ClientLogin clientLogin = (header.authentication as ClientLogin) ?? new ClientLogin();
          clientLogin.token = authToken;
          header.authentication = clientLogin;
        }
      }

      base.InitForCall(methodName, parameters);
    }

    /// <summary>
    /// Whether the current API call should be retried or not.
    /// </summary>
    /// <param name="ex">The exception thrown from the previous call.</param>
    /// <returns>
    /// True, if the current API call should be retried.
    /// </returns>
    protected override bool ShouldRetry(Exception ex) {
      DfpApiException dfpApiException = ex as DfpApiException;
      return dfpApiException != null && (IsCookieInvalidError(dfpApiException) ||
          IsOAuthTokenExpiredError(dfpApiException));
    }

    /// <summary>
    /// Prepares for retrying the API call.
    /// </summary>
    /// <param name="ex">The exception thrown from the previous call.</param>
    protected override void PrepareForRetry(Exception ex) {
      DfpApiException dfpApiException = ex as DfpApiException;
      if (dfpApiException != null) {
        InvalidateAuthTokenCacheIfNecessary(dfpApiException);
        RefreshOAuthTokenIfNecessary(dfpApiException);
      }
    }

    /// <summary>
    /// Determines whether the exception thrown by the server is an AuthToken
    /// Invalid Error.
    /// </summary>
    /// <param name="dfpApiException">The DFP Api exception.</param>
    /// <returns>True, if the server exception is a AuthToken invalid error,
    /// false otherwise.</returns>
    private bool IsCookieInvalidError(DfpApiException dfpApiException) {
      return MatchesError(dfpApiException, COOKIE_INVALID_ERROR);
    }

    /// <summary>
    /// Determines whether the exception thrown by the server is an OAuth token
    /// expired error.
    /// </summary>
    /// <param name="dfpApiException">The DFP Api exception.</param>
    /// <returns>True, if the server exception is a OAuth token expired error,
    /// false otherwise.</returns>
    private bool IsOAuthTokenExpiredError(DfpApiException dfpApiException) {
      return MatchesError(dfpApiException, OAUTH_TOKEN_EXPIRED_ERROR);
    }

    /// <summary>
    /// Determines whether the exception thrown by the server matches a known
    /// error.
    /// </summary>
    /// <param name="dfpApiException">The DFP Api exception.</param>
    /// <param name="errorMessage">The known error message.</param>
    /// <returns>True, if the server exception matches the known error, false
    /// otherwise.</returns>
    private bool MatchesError(DfpApiException dfpApiException, string errorMessage) {
      object[] errors = (object[])dfpApiException.ApiException.GetType().
          GetProperty("errors").GetValue(dfpApiException.ApiException, null);
      if (errors != null) {
        for (int i = 0; i < errors.Length; i++) {
          string errorString = (string)errors[i].GetType().GetProperty("errorString").
              GetValue(errors[i], null);
          if (errorString == errorMessage) {
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>
    /// Refreshes the OAuth access token if necessary.
    /// </summary>
    /// <param name="dfpApiException">The DFP Api exception.</param>
    private void RefreshOAuthTokenIfNecessary(DfpApiException dfpApiException) {
      DfpAppConfig config = this.User.Config as DfpAppConfig;
      if (config.AuthorizationMethod == DfpAuthorizationMethod.OAuth2 &&
          IsOAuthTokenExpiredError(dfpApiException)) {
        (this.User.OAuthProvider as OAuth2).RefreshAccessToken();
      }
    }

    /// <summary>
    /// Invalidates the auth token cache if necessary.
    /// </summary>
    /// <param name="dfpApiException">The DFP Api exception.</param>
    private void InvalidateAuthTokenCacheIfNecessary(DfpApiException dfpApiException) {
      RequestHeader header = (RequestHeader)this.GetType().GetProperty("RequestHeader").
          GetValue(this, null);
      if (IsCookieInvalidError(dfpApiException)) {
        AuthToken.Cache.InvalidateToken(header.authToken);
      }
    }
  }
}

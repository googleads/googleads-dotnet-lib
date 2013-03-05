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

using Google.Api.Ads.AdWords.Headers;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util;

using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// Base class for AdWords API services.
  /// </summary>
  public class AdWordsSoapClient : AdsSoapClient {
    /// <summary>
    /// The error thrown when an auth token expires.
    /// </summary>
    private const string COOKIE_INVALID_ERROR = "AuthenticationError.GOOGLE_ACCOUNT_COOKIE_INVALID";

    /// <summary>
    /// The error thrown when an oauth token expires.
    /// </summary>
    private const string OAUTH_TOKEN_EXPIRED_ERROR = "AuthenticationError.OAUTH_TOKEN_INVALID";

    /// <summary>
    /// Service name to be used when getting auth token for AdWords.
    /// </summary>
    public const string SERVICE_NAME = "adwords";

    /// <summary>
    /// Initializes the service before MakeApiCall.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">The method parameters.</param>
    protected override void InitForCall(string methodName, object[] parameters) {
      AdWordsAppConfig config = this.User.Config as AdWordsAppConfig;
      string oAuthHeader = null;
      RequestHeader header = (RequestHeader) this.GetType().GetProperty("RequestHeader").
          GetValue(this, null);

      if (string.Compare(header.Version, "v201109") >= 0 && !string.IsNullOrEmpty(
          header.clientEmail)) {
        throw new SoapHeaderException("ClientEmail header is not supported in " + header.Version +
            ".", XmlQualifiedName.Empty);
      }

      if (config.AuthorizationMethod == AdWordsAuthorizationMethod.OAuth2) {
        if (this.User.OAuthProvider != null) {
          oAuthHeader = this.User.OAuthProvider.GetAuthHeader(this.Url);
        } else {
          throw new AdWordsApiException(null, AdWordsErrorMessages.OAuthProviderCannotBeNull);
        }
      } else if (config.AuthorizationMethod == AdWordsAuthorizationMethod.ClientLogin) {
        if (header != null) {
          header.authToken = (!string.IsNullOrEmpty(config.AuthToken)) ? config.AuthToken :
              new AuthToken(config, SERVICE_NAME, config.Email, config.Password).GetToken();
        } else {
          throw new AdWordsApiException(null, AdWordsErrorMessages.FailedToSetAuthorizationHeader);
        }
      }
      ContextStore.AddKey("OAuthHeader", oAuthHeader);
      base.InitForCall(methodName, parameters);
    }

    /// <summary>
    /// Cleans up the service after MakeApiCall.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">The method parameters.</param>
    protected override void CleanupAfterCall(string methodName, object[] parameters) {
      ContextStore.RemoveKey("OAuthHeader");
      base.CleanupAfterCall(methodName, parameters);
    }

    /// <summary>
    /// Creates a WebRequest instance for the specified url.
    /// </summary>
    /// <param name="uri">The Uri to use when creating the WebRequest.</param>
    /// <returns>
    /// The WebRequest instance.
    /// </returns>
    protected override WebRequest GetWebRequest(Uri uri) {
      WebRequest request = base.GetWebRequest(uri);
      string oAuthHeader = (string) ContextStore.GetValue("OAuthHeader");
      if (!string.IsNullOrEmpty(oAuthHeader)) {
        request.Headers["Authorization"] = oAuthHeader;
      }
      return request;
    }

    /// <summary>
    /// Whether the current API call should be retried or not.
    /// </summary>
    /// <param name="ex">The exception thrown from the previous call.</param>
    /// <returns>
    /// True, if the current API call should be retried.
    /// </returns>
    protected override bool ShouldRetry(Exception ex) {
      AdWordsApiException awapiException = ex as AdWordsApiException;
      return awapiException != null && (IsCookieInvalidError(awapiException) ||
          IsOAuthTokenExpiredError(awapiException));
    }

    /// <summary>
    /// Prepares for retrying the API call.
    /// </summary>
    /// <param name="ex">The exception thrown from the previous call.</param>
    protected override void PrepareForRetry(Exception ex) {
      AdWordsApiException awapiException = ex as AdWordsApiException;
      if (awapiException != null) {
        InvalidateAuthTokenCacheIfNecessary(awapiException);
        RefreshOAuthTokenIfNecessary(awapiException);
      }
    }

    /// <summary>
    /// Gets a custom exception that wraps the SOAP exception thrown
    /// by the server.
    /// </summary>
    /// <param name="ex">SOAPException that was thrown by the server.</param>
    /// <returns>A custom exception object that wraps the SOAP exception.
    /// </returns>
    protected override Exception GetCustomException(SoapException ex) {
      string defaultNs = GetDefaultNamespace();

      if (!string.IsNullOrEmpty(defaultNs) && ex.Detail != null) {
        // Extract the ApiExceptionFault node.
        XmlElement faultNode = GetFaultNode(ex, defaultNs, "ApiExceptionFault");

        if (faultNode != null) {
          try {
            AdWordsApiException awapiException = new AdWordsApiException(
                SerializationUtilities.DeserializeFromXmlTextCustomRootNs(
                    faultNode.OuterXml, Assembly.GetExecutingAssembly().GetType(
                    this.GetType().Namespace + ".ApiException"), defaultNs, "ApiExceptionFault"),
                    AdWordsErrorMessages.AnApiExceptionOccurred, ex);

            return awapiException;
          } catch (Exception) {
            // deserialization failed, but we can safely ignore it.
          }
        }
      }
      return new AdWordsApiException(null, AdWordsErrorMessages.AnApiExceptionOccurred, ex);
    }

    /// <summary>
    /// Determines whether the exception thrown by the server is an AuthToken
    /// Invalid Error.
    /// </summary>
    /// <param name="awapiException">The awapi exception.</param>
    /// <returns>True, if the server exception is a AuthToken invalid error,
    /// false otherwise.</returns>
    private bool IsCookieInvalidError(AdWordsApiException awapiException) {
      return MatchesError(awapiException, COOKIE_INVALID_ERROR);
    }

    /// <summary>
    /// Determines whether the exception thrown by the server is an OAuth token
    /// expired error.
    /// </summary>
    /// <param name="awapiException">The awapi exception.</param>
    /// <returns>True, if the server exception is a OAuth token expired error,
    /// false otherwise.</returns>
    private bool IsOAuthTokenExpiredError(AdWordsApiException awapiException) {
      return MatchesError(awapiException, OAUTH_TOKEN_EXPIRED_ERROR);
    }

    /// <summary>
    /// Determines whether the exception thrown by the server matches a known
    /// error.
    /// </summary>
    /// <param name="awapiException">The awapi exception.</param>
    /// <param name="errorMessage">The known error message.</param>
    /// <returns>True, if the server exception matches the known error, false
    /// otherwise.</returns>
    private bool MatchesError(AdWordsApiException awapiException, string errorMessage) {
      object[] errors = (object[]) awapiException.ApiException.GetType().
          GetProperty("errors").GetValue(awapiException.ApiException, null);
      if (errors != null) {
        for (int i = 0; i < errors.Length; i++) {
          string errorString = (string) errors[i].GetType().GetProperty("errorString").
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
    /// <param name="awapiException">The awapi exception.</param>
    private void RefreshOAuthTokenIfNecessary(AdWordsApiException awapiException) {
      AdWordsAppConfig config = this.User.Config as AdWordsAppConfig;
      if (config.AuthorizationMethod == AdWordsAuthorizationMethod.OAuth2 &&
          IsOAuthTokenExpiredError(awapiException)) {
        if (!string.IsNullOrEmpty(config.OAuth2ServiceAccountEmail)) {
          (this.User.OAuthProvider as OAuth2).GenerateAccessTokenForServiceAccount();
        } else {
          (this.User.OAuthProvider as OAuth2).RefreshAccessToken();
        }
      }
    }

    /// <summary>
    /// Invalidates the auth token cache if necessary.
    /// </summary>
    /// <param name="awapiException">The AdWords API exception from the server.
    /// </param>
    private void InvalidateAuthTokenCacheIfNecessary(AdWordsApiException awapiException) {
      RequestHeader header = (RequestHeader) this.GetType().GetProperty("RequestHeader").
          GetValue(this, null);
      if (IsCookieInvalidError(awapiException)) {
        AuthToken.Cache.InvalidateToken(header.authToken);
      }
    }
  }
}

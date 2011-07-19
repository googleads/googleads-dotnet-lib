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
    private const string COOKIE_INVALID_ERROR = "AuthenticationError.GOOGLE_ACCOUNT_COOKIE_INVALID";
    private const string SERVICE_NAME = "adwords";

    /// <summary>
    /// This method makes the actual SOAP API call. It is a thin wrapper
    /// over SOAPHttpClientProtocol:Invoke, and provide things like
    /// protection from race condition.
    /// </summary>
    /// <param name="methodName">The name of the SOAP API method.</param>
    /// <param name="parameters">The list of parameters for the SOAP API
    /// method.</param>
    /// <returns>
    /// The results from calling the SOAP API method.
    /// </returns>
    protected override object[] MakeApiCall(string methodName, object[] parameters) {
      AdWordsAppConfig config = this.User.Config as AdWordsAppConfig;
      string oAuthHeader = null;

      if (config.AuthorizationMethod == AdWordsAuthorizationMethod.OAuth) {
        if (this.User.OAuthProvider != null) {
          AdsOAuthProvider provider = this.User.OAuthProvider;
          provider.GenerateAccessToken();
          oAuthHeader = provider.GetAuthHeader(this.Url);
        } else {
          throw new AdWordsApiException(null, AdWordsErrorMessages.OAuthProviderCannotBeNull);
        }
      } else if (config.AuthorizationMethod == AdWordsAuthorizationMethod.ClientLogin) {
        RequestHeader header = (RequestHeader) this.GetType().GetProperty("RequestHeader").
            GetValue(this, null);
        if (header != null) {
          header.authToken = (!string.IsNullOrEmpty(config.AuthToken)) ? config.AuthToken :
              new AuthToken(config, SERVICE_NAME, config.Email, config.Password).GetToken();
        } else {
          throw new AdWordsApiException(null, AdWordsErrorMessages.FailedToSetAuthorizationHeader);
        }
      }

      try {
        ContextStore.AddKey("OAuthHeader", oAuthHeader);
        return base.MakeApiCall(methodName, parameters);
      } finally {
        ContextStore.RemoveKey("OAuthHeader");
      }
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
      return (ex is AdWordsApiException && IsCookieInvalidError(ex as AdWordsApiException));
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
                SerializationUtilities.DeserializeFromXmlText(
                    faultNode.OuterXml, Assembly.GetExecutingAssembly().GetType(
                    this.GetType().Namespace + ".ApiException"), defaultNs, "ApiExceptionFault"),
                    AdWordsErrorMessages.AnApiExceptionOccurred, ex);

            InvalidateAuthTokenCacheIfNecessary(awapiException);
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
      object[] errors = (object[]) awapiException.ApiException.GetType().
          GetProperty("errors").GetValue(awapiException.ApiException, null);
      if (errors != null) {
        for (int i = 0; i < errors.Length; i++) {
          string errorString = (string) errors[i].GetType().GetProperty("errorString").
              GetValue(errors[i], null);
          if (errorString == COOKIE_INVALID_ERROR) {
            return true;
          }
        }
      }
      return false;
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

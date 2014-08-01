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
      RequestHeader header = GetRequestHeader();

      if (string.IsNullOrEmpty(header.developerToken)) {
        throw new ArgumentNullException(AdWordsErrorMessages.DeveloperTokenCannotBeEmpty);
      }

      if (string.IsNullOrEmpty(header.clientCustomerId)) {
        TraceUtilities.WriteGeneralWarnings(AdWordsErrorMessages.ClientCustomerIdIsEmpty);
      }

      string oAuthHeader = null;
      if (this.User.OAuthProvider != null) {
        oAuthHeader = this.User.OAuthProvider.GetAuthHeader();
      } else {
        throw new AdWordsApiException(null, AdWordsErrorMessages.OAuthProviderCannotBeNull);
      }
      ContextStore.AddKey("OAuthHeader", oAuthHeader);
      base.InitForCall(methodName, parameters);
    }

    /// <summary>
    /// Gets the request header.
    /// </summary>
    /// <returns>The request header.</returns>
    private RequestHeader GetRequestHeader() {
      return (RequestHeader) this.GetType().GetProperty("RequestHeader").GetValue(this, null);
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
    /// Creates the error handler.
    /// </summary>
    /// <returns>
    /// The error handler instance.
    /// </returns>
    protected override ErrorHandler CreateErrorHandler() {
      return new AdWordsErrorHandler(this.User as AdWordsUser);
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
            if (AdWordsErrorHandler.IsOAuthTokenExpiredError(awapiException)) {
              return new AdWordsCredentialsExpiredException(
                  (string) ContextStore.GetValue("OAuthHeader"));
            } else {
              return awapiException;
            }
          } catch (Exception) {
            // deserialization failed, but we can safely ignore it.
          }
        }
      }
      return new AdWordsApiException(null, AdWordsErrorMessages.AnApiExceptionOccurred, ex);
    }
  }
}

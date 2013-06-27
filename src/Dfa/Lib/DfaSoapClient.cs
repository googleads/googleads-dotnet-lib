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
using Google.Api.Ads.Dfa.Util;

using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Services.Configuration;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// Base class for DFA API services.
  /// </summary>
  public class DfaSoapClient : AdsSoapClient {
    /// <summary>
    /// The user token for authentication purposes.
    /// </summary>
    UserToken token = null;

    /// <summary>
    /// The optional request header for sending additional details.
    /// </summary>
    RequestHeader requestHeader = null;

    /// <summary>
    /// The response header from DFA server.
    /// </summary>
    ResponseHeader responseHeader = null;

    /// <summary>
    /// Gets or sets the request header.
    /// </summary>
    public RequestHeader RequestHeader {
      get {
        return requestHeader;
      } set {
        requestHeader = value;
      }
    }

    /// <summary>
    /// Gets or sets the response header.
    /// </summary>
    public ResponseHeader ResponseHeader {
      get {
        return responseHeader;
      }
      set {
        responseHeader = value;
      }
    }

    /// <summary>
    /// Gets or sets the token for authentication purposes.
    /// </summary>
    public UserToken Token {
      get {
        return token;
      } set {
        token = value;
      }
    }

    /// <summary>
    /// Initializes the service before MakeApiCall.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">The method parameters.</param>
    protected override void InitForCall(string methodName, object[] parameters) {
      DfaAppConfig config = this.User.Config as DfaAppConfig;
      string oAuthHeader = null;

      if (this.GetType().Name == "LoginRemoteService") {
        // The choice of OAuth comes only when calling LoginRemoteService.
        // All other services will still use the login token.
        if (config.AuthorizationMethod == DfaAuthorizationMethod.OAuth2) {
          if (this.User.OAuthProvider != null) {
            oAuthHeader = this.User.OAuthProvider.GetAuthHeader();
          } else {
            throw new DfaApiException(null, DfaErrorMessages.OAuthProviderCannotBeNull);
          }
        }
      } else {
        if (this.Token == null) {
          this.Token = LoginUtil.GetAuthenticationToken(config, this.Signature, this.User,
              new Uri(this.Url));
        }
      }

      ContextStore.AddKey("OAuthHeader", oAuthHeader);
      ContextStore.AddKey("RequestHeader", requestHeader);
      ContextStore.AddKey("Token", Token);

      base.InitForCall(methodName, parameters);
    }

    /// <summary>
    /// Cleans up the service after MakeApiCall.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">The method parameters.</param>
    protected override void CleanupAfterCall(string methodName, object[] parameters) {
      this.ResponseHeader = (ResponseHeader) ContextStore.GetValue("ResponseHeader");
      ContextStore.RemoveKey("OAuthHeader");
      ContextStore.RemoveKey("RequestHeader");
      ContextStore.RemoveKey("ResponseHeader");
      ContextStore.RemoveKey("Token");

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
    /// Creates the error handler.
    /// </summary>
    /// <returns>
    /// The error handler instance.
    /// </returns>
    protected override ErrorHandler CreateErrorHandler() {
      return new DfaErrorHandler(this.User);
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

      string nodeName = "com.doubleclick.dart.appserver.dfa.dto.api.ApiException";
      object apiException = Activator.CreateInstance(Type.GetType(
          this.GetType().Namespace + ".ApiException"));
      XmlNode faultNode = ex.Detail.SelectSingleNode(nodeName);
      ErrorCode errorCode = null;

      if (faultNode != null) {
        errorCode = new ErrorCode();
        foreach (XmlElement xNode in faultNode.SelectNodes("*")) {
          switch (xNode.Name) {
            case "errorCode":
              errorCode.Code = int.Parse(xNode.InnerText);
              break;

            case "errorMessage":
              errorCode.Description = xNode.InnerText;
              break;
          }
        }
      }
      return new DfaApiException(errorCode, ex.Message, ex);
    }
  }
}

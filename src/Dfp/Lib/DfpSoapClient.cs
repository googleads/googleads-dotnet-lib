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
            return new DfpApiException(SerializationUtilities.DeserializeFromXmlText(
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
      DfpAppConfig config = this.User.Config as DfpAppConfig;
      RequestHeader header = (RequestHeader) this.GetType().GetProperty("RequestHeader").
          GetValue(this, null);

      if (header == null) {
        throw new DfpApiException(null, DfpErrorMessages.FailedToSetAuthorizationHeader);
      }
      if (config.AuthorizationMethod == DfpAuthorizationMethod.OAuth) {
        if (this.User.OAuthProvider != null) {
          AdsOAuthProvider provider = this.User.OAuthProvider;
          provider.GenerateAccessToken();
          string oAuthHeader = provider.GetAuthHeader(this.Url);

          if (string.Compare(header.Version, "v201103") < 0) {
            header.oAuthToken = oAuthHeader;
          } else {
            OAuth oAuth = (header.authentication as OAuth) ?? new OAuth();
            oAuth.parameters = oAuthHeader;
            header.authentication = oAuth;
          }
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

      return base.MakeApiCall(methodName, parameters);
    }
  }
}

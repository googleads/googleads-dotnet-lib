// Copyright 2010, Google Inc. All Rights Reserved.
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
    /// Gets or sets the token for authentication purposes.
    /// </summary>
    public UserToken Token {
      get {
        return token;
      }
      set {
        token = value;
      }
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
      try {
        ContextStore.AddKey("Token", token);
        return base.MakeApiCall(methodName, parameters);
      } finally {
        ContextStore.AddKey("Token", token);
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

      string nodeName = "com.doubleclick.dart.appserver.dfa.dto.api.ApiException";
      object apiException = Activator.CreateInstance(Type.GetType(
          this.GetType().Namespace + ".ApiException"));
      XmlNode faultNode = ex.Detail.SelectSingleNode(nodeName);
      ErrorCode errorCode = new ErrorCode();
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
      return new DfaApiException(errorCode, ex.Message, ex);
    }
  }
}

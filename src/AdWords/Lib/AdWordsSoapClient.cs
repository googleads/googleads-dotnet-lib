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

using System;
using System.IO;
using System.Reflection;
using System.Text;
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
            return new AdWordsApiException(SerializationUtilities.DeserializeFromXmlText(
                faultNode.OuterXml, Assembly.GetExecutingAssembly().GetType(
                    this.GetType().Namespace + ".ApiException"), defaultNs, "ApiExceptionFault"),
                    AdWordsErrorMessages.AnApiExceptionOccurred, ex);
          } catch (Exception) {
            // deserialization failed, but we can safely ignore it.
          }
        }
      }
      return new AdWordsApiException(null, AdWordsErrorMessages.AnApiExceptionOccurred, ex);
    }
  }
}

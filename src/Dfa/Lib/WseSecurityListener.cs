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
using Google.Api.Ads.Dfa.Lib;

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// This listener adds WSE security headers to outgoing SOAP messages.
  /// </summary>
  public class WseSecurityListener : SoapListener {
    /// <summary>
    /// The singleton instance.
    /// </summary>
    protected static WseSecurityListener instance = new WseSecurityListener();

    /// <summary>
    /// Protected constructor.
    /// </summary>
    protected WseSecurityListener() : base(new DfaAppConfig()) {
    }

    /// <summary>
    /// Gets the singleton instance.
    /// </summary>
    public static SoapListener Instance {
      get {
        return instance;
      }
    }

    /// <summary>
    /// Handles the message.
    /// </summary>
    /// <param name="soapMessage">The SOAP message.</param>
    /// <param name="service">The service.</param>
    /// <param name="direction">The direction.</param>
    public override void HandleMessage(XmlDocument soapMessage, AdsClient service,
        SoapListener.Direction direction) {
      if (direction == Direction.OUT) {
        string soapPrefix = "soap";
        string soapNs = "http://schemas.xmlsoap.org/soap/envelope/";

        XmlNamespaceManager xmlnt = new XmlNamespaceManager(soapMessage.NameTable);
        xmlnt.AddNamespace(soapPrefix, soapNs);

        UserToken token = (UserToken) ContextStore.GetValue("Token");
        if (token != null) {
          string securityXml = string.Join("\n", new string[] {
              "<wsse:Security xmlns:" + soapPrefix + "='" + soapNs + "'",
                  "xmlns:wsse='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd'",
                  "soap:mustUnderstand='1'>",
                "<wsse:UsernameToken>",
                  "<wsse:Username>" + token.Username + "</wsse:Username>",
                  "<wsse:Password>" + token.Password + "</wsse:Password>",
                "</wsse:UsernameToken>",
              "</wsse:Security>"});
          XmlDocumentFragment securityHeader = soapMessage.CreateDocumentFragment();
          securityHeader.InnerXml = securityXml;
          XmlElement soapHeader = (XmlElement) soapMessage.SelectSingleNode(
              "soap:Envelope/soap:Header", xmlnt);
          if (soapHeader == null) {
            soapHeader = soapMessage.CreateElement(soapPrefix, "Header", soapNs);
            soapHeader.AppendChild(securityHeader);

            XmlElement soapBody = (XmlElement) soapMessage.SelectSingleNode(
                "soap:Envelope/soap:Body", xmlnt);
            if (soapBody != null) {
              soapMessage.DocumentElement.InsertBefore(soapHeader, soapBody);
            }
          } else {
            soapHeader.AppendChild(securityHeader);
          }
        }
      }
    }
  }
}

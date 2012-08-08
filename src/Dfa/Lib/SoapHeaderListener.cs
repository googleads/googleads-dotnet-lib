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
using Google.Api.Ads.Dfa.Lib;

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// This listener adds or retrieves necessary Soap Headers to or from the
  /// Soap messages.
  /// </summary>
  public class SoapHeaderListener : SoapListener {
    /// <summary>
    /// The config class to be used with this class.
    /// </summary>
    private AppConfig config;

    /// <summary>
    /// Gets the config class to be used with this class.
    /// </summary>
    public AppConfig Config {
      get {
        return config;
      }
    }
    /// <summary>
    /// The singleton instance.
    /// </summary>
    protected static SoapHeaderListener instance = new SoapHeaderListener();

    /// <summary>
    /// The soap namespace prefix.
    /// </summary>
    private const string SOAP_PREFIX = "soap";

    /// <summary>
    /// The soap namespace url.
    /// </summary>
    private const string SOAP_NAMESPACE = "http://schemas.xmlsoap.org/soap/envelope/";

    /// <summary>
    /// The wsse namespace prefix.
    /// </summary>
    private const string WSSE_PREFIX = "wsse";

    /// <summary>
    /// The wsse namespace url.
    /// </summary>
    private const string WSSE_NAMESPACE =
        "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

    /// <summary>
    /// Protected constructor.
    /// </summary>
    protected SoapHeaderListener() {
      this.config = new DfaAppConfig();
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
    /// Gets the soap header node.
    /// </summary>
    /// <param name="soapMessage">The soap message.</param>
    /// <param name="xmlnt">The xml nametable.</param>
    /// <param name="createIfMissing">True, if the header node should be created
    /// if missing.</param>
    /// <returns>The soap header node.</returns>
    XmlElement GetSoapHeaderNode(XmlDocument soapMessage, XmlNamespaceManager xmlnt,
        bool createIfMissing) {
      string headerXpath = string.Format("{0}:Envelope/{0}:Header", SOAP_PREFIX);
      string bodyXpath = string.Format("{0}:Envelope/{0}:Body", SOAP_PREFIX);
      XmlElement soapHeader = (XmlElement) soapMessage.SelectSingleNode(headerXpath, xmlnt);
      if (soapHeader == null && createIfMissing) {
        soapHeader = soapMessage.CreateElement(SOAP_PREFIX, "Header", SOAP_NAMESPACE);
        XmlElement soapBody = (XmlElement) soapMessage.SelectSingleNode(bodyXpath, xmlnt);
        if (soapBody != null) {
          soapMessage.DocumentElement.InsertBefore(soapHeader, soapBody);
        }
      }
      return soapHeader;
    }

    /// <summary>
    /// Adds the security header.
    /// </summary>
    /// <param name="soapHeader">The SOAP header.</param>
    /// <param name="token">The user token.</param>
    private void AddSecurityHeader(XmlElement soapHeader, UserToken token) {
      string securityXml = string.Join("\n", new string[] {
          "<" + WSSE_PREFIX + ":Security xmlns:" + SOAP_PREFIX + "='" + SOAP_NAMESPACE + "'",
              "xmlns:" + WSSE_PREFIX + "='" + WSSE_NAMESPACE + "'",
              SOAP_PREFIX + ":mustUnderstand='1'>",
            "<" + WSSE_PREFIX + ":UsernameToken>",
              "<" + WSSE_PREFIX + ":Username>" + token.Username + "</" + WSSE_PREFIX + ":Username>",
              "<" + WSSE_PREFIX + ":Password>" + token.Password + "</" + WSSE_PREFIX + ":Password>",
            "</" + WSSE_PREFIX + ":UsernameToken>",
          "</" + WSSE_PREFIX + ":Security>"});
      XmlDocumentFragment securityHeader = soapHeader.OwnerDocument.CreateDocumentFragment();
      securityHeader.InnerXml = securityXml;
      soapHeader.AppendChild(securityHeader);
    }

    /// <summary>
    /// Adds the request header.
    /// </summary>
    /// <param name="soapHeader">The SOAP header.</param>
    /// <param name="requestHeader">The request header.</param>
    private void AddRequestHeader(XmlElement soapHeader, RequestHeader requestHeader) {
      const string TNS_PREFIX = "tns";
      string TNS_NAMESPACE = requestHeader.TargetNamespace;

      string requestHeaderXml = string.Join("\n", new string[] {
          "<" + TNS_PREFIX + ":RequestHeader xmlns:" + TNS_PREFIX + "='" + TNS_NAMESPACE + "'>",
            "<" + TNS_PREFIX + ":applicationName>" + requestHeader.ApplicationName + "</" +
                TNS_PREFIX + ":applicationName>",
          "</" + TNS_PREFIX + ":RequestHeader>"
      });
      XmlDocumentFragment requestHeaderNode = soapHeader.OwnerDocument.CreateDocumentFragment();
      requestHeaderNode.InnerXml = requestHeaderXml;
      soapHeader.AppendChild(requestHeaderNode);
    }

    /// <summary>
    /// Parses the response header.
    /// </summary>
    /// <param name="soapMessage">The SOAP message.</param>
    /// <param name="xmlnt">The xml name table.</param>
    /// <returns>The response header.</returns>
    private ResponseHeader ParseResponseHeader(XmlDocument soapMessage, XmlNamespaceManager xmlnt) {
      ResponseHeader responseHeader = null;
      XmlElement soapHeader = GetSoapHeaderNode(soapMessage, xmlnt, false);
      if (soapHeader != null) {
        responseHeader = new ResponseHeader();
        XmlNodeList childNodes = soapHeader.SelectNodes("child::*");
        foreach (XmlElement childNode in childNodes) {
          if (childNode.LocalName == "ResponseHeader") {
            XmlNodeList grandChildNodes = childNode.SelectNodes("child::*");
            foreach (XmlElement grandChildNode in grandChildNodes) {
              switch (grandChildNode.LocalName) {
                case "requestId":
                  responseHeader.RequestId = grandChildNode.InnerText;
                  break;

                case "responseTime":
                  responseHeader.ResponseTime = long.Parse(grandChildNode.InnerText);
                  break;
              }
            }

          }
        }
      }
      return responseHeader;
    }

    /// <summary>
    /// Initializes the listener for handling an API call.
    /// </summary>
    public void InitForCall() {
    }

    /// <summary>
    /// Handles the message.
    /// </summary>
    /// <param name="soapMessage">The SOAP message.</param>
    /// <param name="service">The service.</param>
    /// <param name="direction">The direction.</param>
    public void HandleMessage(XmlDocument soapMessage, AdsClient service,
        SoapMessageDirection direction) {
      XmlNamespaceManager xmlnt = new XmlNamespaceManager(soapMessage.NameTable);
      xmlnt.AddNamespace(SOAP_PREFIX, SOAP_NAMESPACE);
      if (direction == SoapMessageDirection.OUT) {
        UserToken token = (UserToken) ContextStore.GetValue("Token");
        RequestHeader requestHeader = (RequestHeader) ContextStore.GetValue("RequestHeader");

        if (token != null || requestHeader != null) {
          XmlElement soapHeader = GetSoapHeaderNode(soapMessage, xmlnt, true);

          if (token != null) {
            AddSecurityHeader(soapHeader, token);
          }
          if (requestHeader != null) {
            AddRequestHeader(soapHeader, requestHeader);
          }
        }
      } else {
        ContextStore.AddKey("ResponseHeader", ParseResponseHeader(soapMessage, xmlnt));
      }
    }

    /// <summary>
    /// Cleans up any resources after an API call.
    /// </summary>
    public void CleanupAfterCall() {
    }
  }
}

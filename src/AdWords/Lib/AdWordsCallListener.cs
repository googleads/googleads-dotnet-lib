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

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// Listens to SOAP messages and logs API unit costs.
  /// </summary>
  public class AdWordsCallListener : SoapListener {
    /// <summary>
    /// The config class to be used with this class.
    /// </summary>
    private AppConfig config;

    /// <summary>
    /// Gets or sets the config class to be used with this class.
    /// </summary>
    public AppConfig Config {
      get {
        return config;
      }
      set {
        config = value;
      }
    }

    /// <summary>
    /// The singleton instance.
    /// </summary>
    protected static AdWordsCallListener instance = new AdWordsCallListener();

    /// <summary>
    /// Protected constructor.
    /// </summary>
    protected AdWordsCallListener() {
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
    /// Records API operation count for a user.
    /// </summary>
    /// <param name="user">The AdWords user who made the call.</param>
    /// <param name="service">The AdWords API service that was called.</param>
    /// <param name="methodName">The name of the method that was called.</param>
    /// <param name="operationCount">The number of API operations for the
    /// current call.</param>
    private static void RecordApiOperationCount(AdWordsUser user, AdsClient service,
        string methodName, int operationCount) {
      if (user != null) {
        ApiCallEntry entry = new ApiCallEntry();
        entry.Service = service;
        entry.Method = methodName;
        entry.OperationCount = operationCount;
        user.AddCallDetails(entry);
      }
    }

    /// <summary>
    /// Gets the API units for the method call from the SOAP message.
    /// The derived classes should override this method to analyze
    /// the message and return the API units.
    /// </summary>
    /// <param name="soapMessage">The SOAP message.</param>
    /// <returns>The API units for call.</returns>
    protected static int GetApiUnitsForCall(XmlDocument soapMessage) {
      XmlNamespaceManager xmlns = new XmlNamespaceManager(soapMessage.NameTable);
      xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
      XmlNodeList xmlNodeList = soapMessage.SelectNodes("soap:Envelope/soap:Header/descendant::*",
          xmlns);
      foreach (XmlElement xmlNode in xmlNodeList) {
        if (xmlNode.LocalName == "operations") {
          return int.Parse(xmlNode.InnerText);
        }
      }
      return 0;
    }

    /// <summary>
    /// Handles the SOAP message.
    /// </summary>
    /// <param name="soapMessage">The SOAP message.</param>
    /// <param name="service">The SOAP service.</param>
    /// <param name="direction">The direction of message.</param>
    public void HandleMessage(XmlDocument soapMessage, AdsClient service,
        SoapMessageDirection direction) {
      if (direction == SoapMessageDirection.IN) {
        AdWordsUser user = service.User as AdWordsUser;
        string methodName = (string) ContextStore.GetValue("SoapMethod");
        RecordApiOperationCount(user, service, methodName, GetApiUnitsForCall(soapMessage));
      }
    }

    /// <summary>
    /// Cleanups the after call.
    /// </summary>
    public void CleanupAfterCall() {
    }

    /// <summary>
    /// Inits for call.
    /// </summary>
    public void InitForCall() {
    }
  }
}


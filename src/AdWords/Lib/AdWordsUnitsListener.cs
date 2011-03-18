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
  public class AdWordsUnitsListener : SoapListener {
    /// <summary>
    /// The singleton instance.
    /// </summary>
    protected static AdWordsUnitsListener instance = new AdWordsUnitsListener();

    /// <summary>
    /// Protected constructor.
    /// </summary>
    protected AdWordsUnitsListener() : base(new AdWordsAppConfig()) {
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
    /// Records API call cost in AdWordsUser instance.
    /// </summary>
    /// <param name="units">The API cost for the current call.</param>
    /// <param name="service">The Ads API service that was called.</param>
    private static void RecordApiUnitCost(int units, AdsClient service) {
      AdWordsUser user = service.User as AdWordsUser;
      string methodName = (string) ContextStore.GetValue("SoapMethod");
      if (user != null) {
        ApiUnitsEntry entry = new ApiUnitsEntry();
        entry.Service = service;
        entry.Method = methodName;
        entry.Units = units;
        user.AddUnits(entry);
      }
    }

    /// <summary>
    /// Gets the API units for the method call from the SOAP message.
    /// The derived classes should override this method to analyze
    /// the message and return the API units.
    /// </summary>
    /// <param name="message">The SOAP message.</param>
    /// <returns>The API units for call.</returns>
    protected static int GetApiUnitsForCall(XmlDocument soapMessage) {
      XmlNamespaceManager xmlns = new XmlNamespaceManager(soapMessage.NameTable);
      xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
      XmlNodeList xmlNodeList = soapMessage.SelectNodes("soap:Envelope/soap:Header/descendant::*",
          xmlns);
      foreach (XmlElement xmlNode in xmlNodeList) {
        if (xmlNode.Name == "units") {
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
    public override void HandleMessage(XmlDocument soapMessage, AdsClient service,
        SoapListener.Direction direction) {
      if (direction == Direction.IN) {
        RecordApiUnitCost(GetApiUnitsForCall(soapMessage), service);
      }
    }
  }
}


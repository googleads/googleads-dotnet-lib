//
// Copyright (C) 2008 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

namespace com.google.api.adwords.lib
{
  using System;
  using System.IO;
  using System.Net;
  using System.Web.Services;
  using System.Web.Services.Protocols;

  // Define a SOAP Extension that traces the SOAP request and SOAP response for
  // the XML Web service method the SOAP extension is applied to.
  public class QuotaExtension : SoapExtension
  {
    String currentToken;

    // When the SOAP extension is accessed for the first time, the XML Web
    // service method it is applied to is accessed to store the file
    // name passed in, using the corresponding SoapExtensionAttribute.
    public override object GetInitializer(
        LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
    {
      return null;
    }

    // The SOAP extension was configured to run using a configuration file
    // instead of an attribute applied to a specific XML Web service
    // method.
    public override object GetInitializer(Type WebServiceType)
    {
      return null;
    }

    // Receive the file name stored by GetInitializer and store it in a
    // member variable for this specific instance.
    public override void Initialize(object initializer)
    {
    }

    public override Stream ChainStream(Stream stream)
    {
      return stream;
    }

    // If the SoapMessageStage is such that the SoapRequest or
    // SoapResponse is still in the SOAP format to be sent or received,
    // save it out to a file.
    public override void ProcessMessage(SoapMessage message)
    {
      switch (message.Stage)
      {
        case SoapMessageStage.BeforeSerialize: break;
        case SoapMessageStage.AfterSerialize:
          foreach (SoapHeader header in message.Headers)
          {
            if (header.GetType() == Type.GetType(
                "com.google.api.adwords.v11.developerToken"))
            {
              this.currentToken =
                  ((com.google.api.adwords.v11.developerToken)header).Text[0];
            }
            if (header.GetType() == Type.GetType(
                "com.google.api.adwords.v12.developerToken"))
            {
              this.currentToken =
                  ((com.google.api.adwords.v12.developerToken)header).Text[0];
            }
          }
          break;
        case SoapMessageStage.BeforeDeserialize: break;
        case SoapMessageStage.AfterDeserialize:
          int units = 0;
          foreach (SoapHeader header in message.Headers)
          {
            if (header.GetType() == Type.GetType(
                "com.google.api.adwords.v11.units"))
            {
              units = Int32.Parse(((
                  com.google.api.adwords.v11.units)header).Text[0]);
            }
            if (header.GetType() == Type.GetType(
                "com.google.api.adwords.v12.units"))
            {
              units = Int32.Parse(((
                  com.google.api.adwords.v12.units)header).Text[0]);
            }
          }

          AdWordsUser.addUnits(this.currentToken, units);
          break;
        default: throw new Exception("Invalid stage");
      }
    }

  }

  // Create a SoapExtensionAttribute for the SOAP Extension that can be
  // applied to an XML Web service method.
  [AttributeUsage(AttributeTargets.Method)]
  public class QuotaExtensionAttribute : SoapExtensionAttribute
  {
    private int priority;

    public override Type ExtensionType
    {
      get { return typeof(QuotaExtension); }
    }

    public override int Priority
    {
      get { return priority; }
      set { priority = value; }
    }
  }
}

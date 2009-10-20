// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;

using System;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Web.Services.Protocols;

namespace com.google.api.adwords {
  /// <summary>
  /// Soap Request header for AdWords API services.
  /// </summary>
  public class RequestHeader : SoapHeader, IXmlSerializable {
    /// <summary>
    /// Application token.
    /// </summary>
    private string applicationTokenField;

    /// <summary>
    /// Auth token.
    /// </summary>
    private string authTokenField;

    /// <summary>
    /// Client customer Id.
    /// </summary>
    private string clientCustomerIdField;

    /// <summary>
    /// Client email.
    /// </summary>
    private string clientEmailField;

    /// <summary>
    /// Developer token.
    /// </summary>
    private string developerTokenField;

    /// <summary>
    /// User agent.
    /// </summary>
    private string userAgentField;

    /// <summary>
    /// Validate only header - useful if you just want to check if the call is fine.
    /// </summary>
    private bool? validateOnlyField;

    /// <summary>
    /// The xml namespace under which the members should be serialized.
    /// </summary>
    private string targetNamespace;

    /// <summary>
    /// Gets or sets the application token.
    /// </summary>
    public string applicationToken {
      get {
        return this.applicationTokenField;
      }
      set {
        this.applicationTokenField = value;
      }
    }

    /// <summary>
    /// Gets or sets the auth token.
    /// </summary>
    public string authToken {
      get {
        return this.authTokenField;
      }
      set {
        this.authTokenField = value;
      }
    }

    /// <summary>
    /// Gets or sets the client customer id.
    /// </summary>
    public string clientCustomerId {
      get {
        return this.clientCustomerIdField;
      }
      set {
        this.clientCustomerIdField = value;
      }
    }

    /// <summary>
    /// Gets or sets the client email.
    /// </summary>
    public string clientEmail {
      get {
        return this.clientEmailField;
      }
      set {
        this.clientEmailField = value;
      }
    }

    /// <summary>
    /// Gets or sets the developer token.
    /// </summary>
    public string developerToken {
      get {
        return this.developerTokenField;
      }
      set {
        this.developerTokenField = value;
      }
    }

    /// <summary>
    /// Gets or sets the user agent.
    /// </summary>
    public string userAgent {
      get {
        return this.userAgentField;
      }
      set {
        this.userAgentField = value;
      }
    }

    /// <summary>
    /// Gets or sets the validateOnly header.
    /// </summary>
    public bool? validateOnly {
      get {
        return this.validateOnlyField;
      }
      set {
        this.validateOnlyField = value;
      }
    }

    /// <summary>
    /// Gets or sets the target namespace under which the child
    /// nodes should be serialized.
    /// </summary>
    public string TargetNamespace {
      get {
        return targetNamespace;
      }
      set {
        targetNamespace = value;
      }
    }

    /// <summary>
    /// Gets the schema for this object. Can return null if the
    /// server does not expect the schema.
    /// </summary>
    /// <returns>The xml schema associated with this class, or null
    /// if not applicable.</returns>
    public XmlSchema GetSchema() {
      return null;
    }

    /// <summary>
    /// Deserialize the object from xml.
    /// </summary>
    /// <param name="reader">The xml reader for reading the
    /// serialized xml.</param>
    public void ReadXml(XmlReader reader) {
      return;
    }

    /// <summary>
    /// Serialize the object into an xml.
    /// </summary>
    /// <param name="writer">The writer to which the serialized data
    /// should be written.</param>
    public void WriteXml(XmlWriter writer) {
      string[] headersToSerialize = {"applicationToken", "authToken", "clientCustomerId",
        "clientEmail", "developerToken", "userAgent", "validateOnly"};
      foreach (string headerName in headersToSerialize) {
        PropertyInfo propInfo = this.GetType().GetProperty(headerName);
        object value = propInfo.GetValue(this, null);
        if (value != null) {
          if (String.IsNullOrEmpty(targetNamespace)) {
            writer.WriteElementString(propInfo.Name, value.ToString());
          } else {
            writer.WriteElementString(propInfo.Name, targetNamespace, value.ToString());
          }
        }
      }
    }
  }

  /// <summary>
  /// Soap Response header for AdWords API services.
  /// </summary>
  public class ResponseHeader : SoapHeader, IXmlSerializable {
    /// <summary>
    /// Request ID for this API call.
    /// </summary>
    private string requestIdField;

    /// <summary>
    /// Number of operations for this API call.
    /// </summary>
    private long? operationsField;

    /// <summary>
    /// Response time for this API call.
    /// </summary>
    private long? responseTimeField;

    /// <summary>
    /// Units consumed for this API call.
    /// </summary>
    private long? unitsField;

    /// <summary>
    /// Gets or sets the request id for this API call.
    /// </summary>
    public string requestId {
      get {
        return this.requestIdField;
      }
      set {
        this.requestIdField = value;
      }
    }

    /// <summary>
    /// Gets or sets the number of operations for this API call.
    /// </summary>
    public long? operations {
      get {
        return this.operationsField;
      }
      set {
        this.operationsField = value;
      }
    }

    /// <summary>
    /// Gets or sets the response time for this API call.
    /// </summary>
    public long? responseTime {
      get {
        return this.responseTimeField;
      }
      set {
        this.responseTimeField = value;
      }
    }

    /// <summary>
    /// Gets or sets the number of units consumed for this API call.
    /// </summary>
    public long? units {
      get {
        return this.unitsField;
      }
      set {
        this.unitsField = value;
      }
    }

    /// <summary>
    /// Gets the schema for this object. Can return null if the
    /// server does not expect the schema.
    /// </summary>
    /// <returns>The xml schema associated with this class, or null
    /// if not applicable.</returns>
    public XmlSchema GetSchema() {
      return null;
    }

    /// <summary>
    /// Deserialize the object from xml.
    /// </summary>
    /// <param name="reader">The xml reader for reading the
    /// serialized xml.</param>
    public void ReadXml(XmlReader reader) {
      long tempVal = 0;

      string elementXml = reader.ReadOuterXml();
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(elementXml);
      foreach (XmlNode node in doc.DocumentElement.ChildNodes) {
        if (node is XmlElement) {
          switch (node.Name) {
            case "requestId":
              requestId = node.InnerText;
              break;

            case "operations":
              if (long.TryParse(node.InnerText, out tempVal)) {
                operations = tempVal;
              };
              break;

            case "responseTime":
              if (long.TryParse(node.InnerText, out tempVal)) {
                responseTime = tempVal;
              };
              break;

            case "units":
              if (long.TryParse(node.InnerText, out tempVal)) {
                units = tempVal;
              };
              break;
          }
        }
      }
    }

    /// <summary>
    /// Serialize the object into an xml.
    /// </summary>
    /// <param name="writer">The writer to which the serialized data
    /// should be written.</param>
    public void WriteXml(XmlWriter writer) {
      return;
    }
  }

  /// <summary>
  /// Base class for all AdWords API services.
  /// </summary>
  public class AdWordsApiService : SoapServiceBase {
  }
}


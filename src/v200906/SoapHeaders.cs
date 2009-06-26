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

using System;

namespace com.google.api.adwords.v200906 {
  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/cm/v200906")]
  [System.Xml.Serialization.XmlRootAttribute("RequestHeader", Namespace = "https://adwords.google.com/api/adwords/cm/v200906", IsNullable = false)]
  public partial class RequestHeader : System.Web.Services.Protocols.SoapHeader {
    private string applicationTokenField;
    private string authTokenField;
    private string clientCustomerIdField;
    private string clientEmailField;
    private string developerTokenField;
    private string userAgentField;

    /// <remarks/>
    public string applicationToken {
      get {
        return this.applicationTokenField;
      }
      set {
        this.applicationTokenField = value;
      }
    }

    /// <remarks/>
    public string authToken {
      get {
        return this.authTokenField;
      }
      set {
        this.authTokenField = value;
      }
    }

    /// <remarks/>
    public string clientCustomerId {
      get {
        return this.clientCustomerIdField;
      }
      set {
        this.clientCustomerIdField = value;
      }
    }

    /// <remarks/>
    public string clientEmail {
      get {
        return this.clientEmailField;
      }
      set {
        this.clientEmailField = value;
      }
    }

    /// <remarks/>
    public string developerToken {
      get {
        return this.developerTokenField;
      }
      set {
        this.developerTokenField = value;
      }
    }

    /// <remarks/>
    public string userAgent {
      get {
        return this.userAgentField;
      }
      set {
        this.userAgentField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/cm/v200906")]
  [System.Xml.Serialization.XmlRootAttribute("ResponseHeader", Namespace = "https://adwords.google.com/api/adwords/cm/v200906", IsNullable = false)]
  public partial class ResponseHeader : System.Web.Services.Protocols.SoapHeader {
    private string requestIdField;
    private long operationsField;
    private bool operationsFieldSpecified;
    private long responseTimeField;
    private bool responseTimeFieldSpecified;
    private long unitsField;
    private bool unitsFieldSpecified;

    /// <remarks/>
    public string requestId {
      get {
        return this.requestIdField;
      }
      set {
        this.requestIdField = value;
      }
    }

    /// <remarks/>
    public long operations {
      get {
        return this.operationsField;
      }
      set {
        this.operationsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool operationsSpecified {
      get {
        return this.operationsFieldSpecified;
      }
      set {
        this.operationsFieldSpecified = value;
      }
    }

    /// <remarks/>
    public long responseTime {
      get {
        return this.responseTimeField;
      }
      set {
        this.responseTimeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool responseTimeSpecified {
      get {
        return this.responseTimeFieldSpecified;
      }
      set {
        this.responseTimeFieldSpecified = value;
      }
    }

    /// <remarks/>
    public long units {
      get {
        return this.unitsField;
      }
      set {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool unitsSpecified {
      get {
        return this.unitsFieldSpecified;
      }
      set {
        this.unitsFieldSpecified = value;
      }
    }
  }
}


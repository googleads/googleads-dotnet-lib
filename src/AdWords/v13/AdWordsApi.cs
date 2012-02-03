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

#pragma warning disable 1591

namespace Google.Api.Ads.AdWords.v13 {
  using Google.Api.Ads.AdWords.Lib;

  using System;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Xml.Serialization;
  using System.Web.Services;
  using System.Web.Services.Protocols;

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "AccountServiceSoapBinding", Namespace = "https://adwords.google.com/api/adwords/v13")]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class AccountService : LegacyAdWordsSoapClient {
    private useragent useragentValueField;

    private password passwordValueField;

    private email emailValueField;

    private clientEmail clientEmailValueField;

    private clientCustomerId clientCustomerIdValueField;

    private developerToken developerTokenValueField;

    private applicationToken applicationTokenValueField;

    private responseTime responseTimeValueField;

    private operations operationsValueField;

    private units unitsValueField;

    private requestId requestIdValueField;

    public AccountService() {
      this.Url = "https://adwords.google.com/api/adwords/v13/AccountService";
    }

    public useragent useragentValue {
      get {
        return this.useragentValueField;
      }
      set {
        this.useragentValueField = value;
      }
    }

    public password passwordValue {
      get {
        return this.passwordValueField;
      }
      set {
        this.passwordValueField = value;
      }
    }

    public email emailValue {
      get {
        return this.emailValueField;
      }
      set {
        this.emailValueField = value;
      }
    }

    public clientEmail clientEmailValue {
      get {
        return this.clientEmailValueField;
      }
      set {
        this.clientEmailValueField = value;
      }
    }

    public clientCustomerId clientCustomerIdValue {
      get {
        return this.clientCustomerIdValueField;
      }
      set {
        this.clientCustomerIdValueField = value;
      }
    }

    public developerToken developerTokenValue {
      get {
        return this.developerTokenValueField;
      }
      set {
        this.developerTokenValueField = value;
      }
    }

    public applicationToken applicationTokenValue {
      get {
        return this.applicationTokenValueField;
      }
      set {
        this.applicationTokenValueField = value;
      }
    }

    public responseTime responseTimeValue {
      get {
        return this.responseTimeValueField;
      }
      set {
        this.responseTimeValueField = value;
      }
    }

    public operations operationsValue {
      get {
        return this.operationsValueField;
      }
      set {
        this.operationsValueField = value;
      }
    }

    public units unitsValue {
      get {
        return this.unitsValueField;
      }
      set {
        this.unitsValueField = value;
      }
    }

    public requestId requestIdValue {
      get {
        return this.requestIdValueField;
      }
      set {
        this.requestIdValueField = value;
      }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("clientEmailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("passwordValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("responseTimeValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("emailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("requestIdValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("unitsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("developerTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("applicationTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("operationsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("useragentValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("clientCustomerIdValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://adwords.google.com/api/adwords/v13", ResponseNamespace = "https://adwords.google.com/api/adwords/v13", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("getAccountInfoReturn")]
    public AccountInfo getAccountInfo() {
      object[] results = this.Invoke("getAccountInfo", new object[0]);
      return ((AccountInfo) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("clientEmailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("passwordValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("responseTimeValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("emailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("requestIdValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("unitsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("developerTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("applicationTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("operationsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("useragentValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("clientCustomerIdValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://adwords.google.com/api/adwords/v13", ResponseNamespace = "https://adwords.google.com/api/adwords/v13", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("getClientAccountInfosReturn")]
    public ClientAccountInfo[] getClientAccountInfos() {
      object[] results = this.Invoke("getClientAccountInfos", new object[0]);
      return ((ClientAccountInfo[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("clientEmailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("passwordValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("responseTimeValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("emailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("requestIdValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("unitsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("developerTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("applicationTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("operationsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("useragentValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("clientCustomerIdValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://adwords.google.com/api/adwords/v13", ResponseNamespace = "https://adwords.google.com/api/adwords/v13", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("getClientAccountsReturn")]
    public string[] getClientAccounts() {
      object[] results = this.Invoke("getClientAccounts", new object[0]);
      return ((string[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("clientEmailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("passwordValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("responseTimeValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("emailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("requestIdValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("unitsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("developerTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("applicationTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("operationsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("useragentValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("clientCustomerIdValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://adwords.google.com/api/adwords/v13", ResponseNamespace = "https://adwords.google.com/api/adwords/v13", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("getMccAlertsReturn")]
    public MccAlert[] getMccAlerts() {
      object[] results = this.Invoke("getMccAlerts", new object[0]);
      return ((MccAlert[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("clientEmailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("passwordValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("responseTimeValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("emailValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("requestIdValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("unitsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("developerTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("applicationTokenValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("operationsValue", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapHeaderAttribute("useragentValue")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("clientCustomerIdValue")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://adwords.google.com/api/adwords/v13", ResponseNamespace = "https://adwords.google.com/api/adwords/v13", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void updateAccountInfo(AccountInfo accountInfo) {
      this.Invoke("updateAccountInfo", new object[] {accountInfo});
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class clientEmail : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class password : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class responseTime : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class email : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class requestId : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class units : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class developerToken : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class applicationToken : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class operations : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class useragent : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13", IsNullable = false)]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class clientCustomerId : System.Web.Services.Protocols.SoapHeader {
    private string[] valueField;

    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Value {
      get {
        return this.valueField;
      }
      set {
        this.valueField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class AccountInfo {
    private Address billingAddressField;

    private string currencyCodeField;

    private long? customerIdField;

    private bool customerIdFieldSpecified;

    private NetworkType[] defaultNetworkTargetingField;

    private string descriptiveNameField;

    private EmailPromotionsPreferences emailPromotionsPreferencesField;

    private string languagePreferenceField;

    private Address primaryAddressField;

    private string primaryBusinessCategoryField;

    private long? timeZoneEffectiveDateField;

    private bool timeZoneEffectiveDateFieldSpecified;

    private string timeZoneIdField;

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public Address billingAddress {
      get {
        return this.billingAddressField;
      }
      set {
        this.billingAddressField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string currencyCode {
      get {
        return this.currencyCodeField;
      }
      set {
        this.currencyCodeField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public long? customerId {
      get {
        return this.customerIdField;
      }
      set {
        this.customerIdField = value;
        this.customerIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool customerIdSpecified {
      get {
        return this.customerIdFieldSpecified;
      }
      set {
        this.customerIdFieldSpecified = value;
      }
    }

    [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
    [System.Xml.Serialization.XmlArrayItemAttribute("networkTypes", IsNullable = false)]
    public NetworkType[] defaultNetworkTargeting {
      get {
        return this.defaultNetworkTargetingField;
      }
      set {
        this.defaultNetworkTargetingField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string descriptiveName {
      get {
        return this.descriptiveNameField;
      }
      set {
        this.descriptiveNameField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public EmailPromotionsPreferences emailPromotionsPreferences {
      get {
        return this.emailPromotionsPreferencesField;
      }
      set {
        this.emailPromotionsPreferencesField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string languagePreference {
      get {
        return this.languagePreferenceField;
      }
      set {
        this.languagePreferenceField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public Address primaryAddress {
      get {
        return this.primaryAddressField;
      }
      set {
        this.primaryAddressField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string primaryBusinessCategory {
      get {
        return this.primaryBusinessCategoryField;
      }
      set {
        this.primaryBusinessCategoryField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public long? timeZoneEffectiveDate {
      get {
        return this.timeZoneEffectiveDateField;
      }
      set {
        this.timeZoneEffectiveDateField = value;
        this.timeZoneEffectiveDateSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool timeZoneEffectiveDateSpecified {
      get {
        return this.timeZoneEffectiveDateFieldSpecified;
      }
      set {
        this.timeZoneEffectiveDateFieldSpecified = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string timeZoneId {
      get {
        return this.timeZoneIdField;
      }
      set {
        this.timeZoneIdField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class Address {
    private string addressLine1Field;

    private string addressLine2Field;

    private string cityField;

    private string companyNameField;

    private string countryCodeField;

    private string emailAddressField;

    private string faxNumberField;

    private string nameField;

    private string phoneNumberField;

    private string postalCodeField;

    private string stateField;

    public string addressLine1 {
      get {
        return this.addressLine1Field;
      }
      set {
        this.addressLine1Field = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string addressLine2 {
      get {
        return this.addressLine2Field;
      }
      set {
        this.addressLine2Field = value;
      }
    }

    public string city {
      get {
        return this.cityField;
      }
      set {
        this.cityField = value;
      }
    }

    public string companyName {
      get {
        return this.companyNameField;
      }
      set {
        this.companyNameField = value;
      }
    }

    public string countryCode {
      get {
        return this.countryCodeField;
      }
      set {
        this.countryCodeField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string emailAddress {
      get {
        return this.emailAddressField;
      }
      set {
        this.emailAddressField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string faxNumber {
      get {
        return this.faxNumberField;
      }
      set {
        this.faxNumberField = value;
      }
    }

    public string name {
      get {
        return this.nameField;
      }
      set {
        this.nameField = value;
      }
    }

    public string phoneNumber {
      get {
        return this.phoneNumberField;
      }
      set {
        this.phoneNumberField = value;
      }
    }

    public string postalCode {
      get {
        return this.postalCodeField;
      }
      set {
        this.postalCodeField = value;
      }
    }

    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string state {
      get {
        return this.stateField;
      }
      set {
        this.stateField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class MccAlert {
    private string clientCompanyNameField;

    private long clientCustomerIdField;

    private string clientLoginField;

    private string clientNameField;

    private MccAlertPriority priorityField;

    private System.DateTime triggerTimeField;

    private MccAlertType typeField;

    public string clientCompanyName {
      get {
        return this.clientCompanyNameField;
      }
      set {
        this.clientCompanyNameField = value;
      }
    }

    public long clientCustomerId {
      get {
        return this.clientCustomerIdField;
      }
      set {
        this.clientCustomerIdField = value;
      }
    }

    public string clientLogin {
      get {
        return this.clientLoginField;
      }
      set {
        this.clientLoginField = value;
      }
    }

    public string clientName {
      get {
        return this.clientNameField;
      }
      set {
        this.clientNameField = value;
      }
    }

    public MccAlertPriority priority {
      get {
        return this.priorityField;
      }
      set {
        this.priorityField = value;
      }
    }

    public System.DateTime triggerTime {
      get {
        return this.triggerTimeField;
      }
      set {
        this.triggerTimeField = value;
      }
    }

    public MccAlertType type {
      get {
        return this.typeField;
      }
      set {
        this.typeField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public enum MccAlertPriority {
    Low,
    High
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public enum MccAlertType {
    AccountOnTarget,
    DeclinedPayment,
    CreditCardExpiring,
    AccountBudgetEnding,
    CampaignEnding,
    CreativeDisapproved,
    PaymentNotEntered,
    MissingBankReferenceNumber,
    CampaignEnded,
    KeywordBelowMinCpc,
    AccountBudgetBurnRate
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class ClientAccountInfo {
    private string emailAddressField;

    private bool isCustomerManagerField;

    public string emailAddress {
      get {
        return this.emailAddressField;
      }
      set {
        this.emailAddressField = value;
      }
    }

    public bool isCustomerManager {
      get {
        return this.isCustomerManagerField;
      }
      set {
        this.isCustomerManagerField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public partial class EmailPromotionsPreferences {
    private bool accountPerformanceEnabledField;

    private bool disapprovedAdsEnabledField;

    private bool marketResearchEnabledField;

    private bool newsletterEnabledField;

    private bool promotionsEnabledField;

    public bool accountPerformanceEnabled {
      get {
        return this.accountPerformanceEnabledField;
      }
      set {
        this.accountPerformanceEnabledField = value;
      }
    }

    public bool disapprovedAdsEnabled {
      get {
        return this.disapprovedAdsEnabledField;
      }
      set {
        this.disapprovedAdsEnabledField = value;
      }
    }

    public bool marketResearchEnabled {
      get {
        return this.marketResearchEnabledField;
      }
      set {
        this.marketResearchEnabledField = value;
      }
    }

    public bool newsletterEnabled {
      get {
        return this.newsletterEnabledField;
      }
      set {
        this.newsletterEnabledField = value;
      }
    }

    public bool promotionsEnabled {
      get {
        return this.promotionsEnabledField;
      }
      set {
        this.promotionsEnabledField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [Obsolete("This API version is deprecated; use v201109 version of API instead.")]
  public enum NetworkType {
    GoogleSearch,
    SearchNetwork,
    ContentNetwork
  }
}

#pragma warning restore 1591
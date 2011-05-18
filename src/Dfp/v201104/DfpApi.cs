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

namespace Google.Api.Ads.Dfp.v201104 {
  using Google.Api.Ads.Dfp.Headers;
  using Google.Api.Ads.Dfp.Lib;

  using System;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Xml.Serialization;
  using System.Web.Services;
  using System.Web.Services.Protocols;

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "PlacementServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SiteTargetingInfo))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class PlacementService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public PlacementService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/PlacementService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Placement createPlacement(Placement placement) {
      object[] results = this.Invoke("createPlacement", new object[] {placement});
      return ((Placement) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Placement[] createPlacements([System.Xml.Serialization.XmlElementAttribute("placements")]
Placement[] placements) {
      object[] results = this.Invoke("createPlacements", new object[] {placements});
      return ((Placement[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Placement getPlacement(long placementId) {
      object[] results = this.Invoke("getPlacement", new object[] {placementId});
      return ((Placement) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public PlacementPage getPlacementsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getPlacementsByStatement", new object[] {filterStatement});
      return ((PlacementPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public UpdateResult performPlacementAction(PlacementAction placementAction, Statement filterStatement) {
      object[] results = this.Invoke("performPlacementAction", new object[] {placementAction, filterStatement});
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Placement updatePlacement(Placement placement) {
      object[] results = this.Invoke("updatePlacement", new object[] {placement});
      return ((Placement) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Placement[] updatePlacements([System.Xml.Serialization.XmlElementAttribute("placements")]
Placement[] placements) {
      object[] results = this.Invoke("updatePlacements", new object[] {placements});
      return ((Placement[]) (results[0]));
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OAuth))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ClientLogin))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class Authentication {
    private string authenticationTypeField;

    [System.Xml.Serialization.XmlElementAttribute("Authentication.Type")]
    public string AuthenticationType {
      get { return this.authenticationTypeField; }
      set { this.authenticationTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class UpdateResult {
    private int numChangesField;

    private bool numChangesFieldSpecified;

    public int numChanges {
      get { return this.numChangesField; }
      set {
        this.numChangesField = value;
        this.numChangesSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool numChangesSpecified {
      get { return this.numChangesFieldSpecified; }
      set { this.numChangesFieldSpecified = value; }
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivatePlacements))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchivePlacements))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivatePlacements))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class PlacementAction {
    private string placementActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("PlacementAction.Type")]
    public string PlacementActionType {
      get { return this.placementActionTypeField; }
      set { this.placementActionTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DeactivatePlacements : PlacementAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ArchivePlacements : PlacementAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ActivatePlacements : PlacementAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class PlacementPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Placement[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public Placement[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Placement : SiteTargetingInfo {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string descriptionField;

    private string placementCodeField;

    private InventoryStatus statusField;

    private bool statusFieldSpecified;

    private bool isAdSenseTargetingEnabledField;

    private bool isAdSenseTargetingEnabledFieldSpecified;

    private bool isAdPlannerTargetingEnabledField;

    private bool isAdPlannerTargetingEnabledFieldSpecified;

    private string adSenseTargetingLocaleField;

    private string[] targetedAdUnitIdsField;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }

    public string placementCode {
      get { return this.placementCodeField; }
      set { this.placementCodeField = value; }
    }

    public InventoryStatus status {
      get { return this.statusField; }
      set {
        this.statusField = value;
        this.statusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool statusSpecified {
      get { return this.statusFieldSpecified; }
      set { this.statusFieldSpecified = value; }
    }

    public bool isAdSenseTargetingEnabled {
      get { return this.isAdSenseTargetingEnabledField; }
      set {
        this.isAdSenseTargetingEnabledField = value;
        this.isAdSenseTargetingEnabledSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isAdSenseTargetingEnabledSpecified {
      get { return this.isAdSenseTargetingEnabledFieldSpecified; }
      set { this.isAdSenseTargetingEnabledFieldSpecified = value; }
    }

    public bool isAdPlannerTargetingEnabled {
      get { return this.isAdPlannerTargetingEnabledField; }
      set {
        this.isAdPlannerTargetingEnabledField = value;
        this.isAdPlannerTargetingEnabledSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isAdPlannerTargetingEnabledSpecified {
      get { return this.isAdPlannerTargetingEnabledFieldSpecified; }
      set { this.isAdPlannerTargetingEnabledFieldSpecified = value; }
    }

    public string adSenseTargetingLocale {
      get { return this.adSenseTargetingLocaleField; }
      set { this.adSenseTargetingLocaleField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("targetedAdUnitIds")]
    public string[] targetedAdUnitIds {
      get { return this.targetedAdUnitIdsField; }
      set { this.targetedAdUnitIdsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum InventoryStatus {
    ACTIVE,
    INACTIVE,
    ARCHIVED
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(Placement))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class SiteTargetingInfo {
    private string targetingDescriptionField;

    private string targetingSiteNameField;

    private string targetingAdLocationField;

    private string siteTargetingInfoTypeField;

    public string targetingDescription {
      get { return this.targetingDescriptionField; }
      set { this.targetingDescriptionField = value; }
    }

    public string targetingSiteName {
      get { return this.targetingSiteNameField; }
      set { this.targetingSiteNameField = value; }
    }

    public string targetingAdLocation {
      get { return this.targetingAdLocationField; }
      set { this.targetingAdLocationField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("SiteTargetingInfo.Type")]
    public string SiteTargetingInfoType {
      get { return this.siteTargetingInfoTypeField; }
      set { this.siteTargetingInfoTypeField = value; }
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(TextValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(NumberValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BooleanValue))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class Value {
    private string valueTypeField;

    [System.Xml.Serialization.XmlElementAttribute("Value.Type")]
    public string ValueType {
      get { return this.valueTypeField; }
      set { this.valueTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class TextValue : Value {
    private string valueField;

    public string value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class NumberValue : Value {
    private string valueField;

    public string value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class BooleanValue : Value {
    private bool valueField;

    private bool valueFieldSpecified;

    public bool value {
      get { return this.valueField; }
      set {
        this.valueField = value;
        this.valueSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool valueSpecified {
      get { return this.valueFieldSpecified; }
      set { this.valueFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class String_ValueMapEntry {
    private string keyField;

    private Value valueField;

    public string key {
      get { return this.keyField; }
      set { this.keyField = value; }
    }

    public Value value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Statement {
    private string queryField;

    private String_ValueMapEntry[] valuesField;

    public string query {
      get { return this.queryField; }
      set { this.queryField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("values")]
    public String_ValueMapEntry[] values {
      get { return this.valuesField; }
      set { this.valuesField = value; }
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UniqueError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(TypeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(StringLengthError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(StatementError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ServerError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequiredError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RegExError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RangeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(QuotaError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PermissionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ParseError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(NullError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(NotNullError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InternalApiError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CommonError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AuthenticationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiVersionError))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReservationDetailsError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequiredSizeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequiredNumberError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequiredCollectionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PublisherQueryLanguageSyntaxError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PublisherQueryLanguageContextError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderActionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemOperationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemFlightDateError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryUnitError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InvalidUrlError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InvalidEmailError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FileError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CreativeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdUnitHierarchyError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdUnitCodeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdUnitAfcSizeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReportError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ForecastError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FrequencyCapError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserDomainTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(GeoTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DayPartTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemCreativeAssociationOperationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemCreativeAssociationError))]
  public abstract partial class ApiError {
    private string fieldPathField;

    private string triggerField;

    private string errorStringField;

    private string apiErrorTypeField;

    public string fieldPath {
      get { return this.fieldPathField; }
      set { this.fieldPathField = value; }
    }

    public string trigger {
      get { return this.triggerField; }
      set { this.triggerField = value; }
    }

    public string errorString {
      get { return this.errorStringField; }
      set { this.errorStringField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("ApiError.Type")]
    public string ApiErrorType {
      get { return this.apiErrorTypeField; }
      set { this.apiErrorTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class UniqueError : ApiError {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class TypeError : ApiError {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class StringLengthError : ApiError {
    private StringLengthErrorReason reasonField;

    private bool reasonFieldSpecified;

    public StringLengthErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "StringLengthError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum StringLengthErrorReason {
    TOO_LONG,
    TOO_SHORT
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class StatementError : ApiError {
    private StatementErrorReason reasonField;

    private bool reasonFieldSpecified;

    public StatementErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "StatementError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum StatementErrorReason {
    VARIABLE_NOT_BOUND_TO_VALUE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ServerError : ApiError {
    private ServerErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ServerErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ServerError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ServerErrorReason {
    SERVER_ERROR,
    SERVER_BUSY
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class RequiredError : ApiError {
    private RequiredErrorReason reasonField;

    private bool reasonFieldSpecified;

    public RequiredErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RequiredError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum RequiredErrorReason {
    REQUIRED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class RegExError : ApiError {
    private RegExErrorReason reasonField;

    private bool reasonFieldSpecified;

    public RegExErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RegExError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum RegExErrorReason {
    INVALID,
    NULL
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class RangeError : ApiError {
    private RangeErrorReason reasonField;

    private bool reasonFieldSpecified;

    public RangeErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RangeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum RangeErrorReason {
    TOO_HIGH,
    TOO_LOW
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class QuotaError : ApiError {
    private QuotaErrorReason reasonField;

    private bool reasonFieldSpecified;

    public QuotaErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "QuotaError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum QuotaErrorReason {
    EXCEEDED_QUOTA
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class PermissionError : ApiError {
    private PermissionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public PermissionErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PermissionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum PermissionErrorReason {
    PERMISSION_DENIED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ParseError : ApiError {
    private ParseErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ParseErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ParseError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ParseErrorReason {
    UNPARSABLE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class NullError : ApiError {
    private NullErrorReason reasonField;

    private bool reasonFieldSpecified;

    public NullErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "NullError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum NullErrorReason {
    NULL_CONTENT
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class NotNullError : ApiError {
    private NotNullErrorReason reasonField;

    private bool reasonFieldSpecified;

    public NotNullErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "NotNullError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum NotNullErrorReason {
    ARG1_NULL,
    ARG2_NULL,
    ARG3_NULL,
    NULL
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class InternalApiError : ApiError {
    private InternalApiErrorReason reasonField;

    private bool reasonFieldSpecified;

    public InternalApiErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InternalApiError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum InternalApiErrorReason {
    UNEXPECTED_INTERNAL_API_ERROR,
    UNKNOWN
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CommonError : ApiError {
    private CommonErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CommonErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CommonError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CommonErrorReason {
    NOT_FOUND,
    ALREADY_EXISTS,
    DUPLICATE_OBJECT,
    CANNOT_UPDATE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class AuthenticationError : ApiError {
    private AuthenticationErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AuthenticationErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AuthenticationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum AuthenticationErrorReason {
    AMBIGUOUS_SOAP_REQUEST_HEADER,
    INVALID_EMAIL,
    INVALID_OAUTH_SIGNATURE,
    MISSING_SOAP_REQUEST_HEADER,
    NOT_WHITELISTED_FOR_API_ACCESS,
    NO_NETWORKS_TO_ACCESS,
    NETWORK_NOT_FOUND,
    NETWORK_CODE_REQUIRED,
    CONNECTION_ERROR
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ApiVersionError : ApiError {
    private ApiVersionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ApiVersionErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ApiVersionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ApiVersionErrorReason {
    UPDATE_TO_NEWER_VERSION
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiException))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ApplicationException {
    private string messageField;

    private string applicationExceptionTypeField;

    public string message {
      get { return this.messageField; }
      set { this.messageField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("ApplicationException.Type")]
    public string ApplicationExceptionType {
      get { return this.applicationExceptionTypeField; }
      set { this.applicationExceptionTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ApiException : ApplicationException {
    private ApiError[] errorsField;

    [System.Xml.Serialization.XmlElementAttribute("errors")]
    public ApiError[] errors {
      get { return this.errorsField; }
      set { this.errorsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class OAuth : Authentication {
    private string parametersField;

    public string parameters {
      get { return this.parametersField; }
      set { this.parametersField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ClientLogin : Authentication {
    private string tokenField;

    public string token {
      get { return this.tokenField; }
      set { this.tokenField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "PublisherQueryLanguageServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class PublisherQueryLanguageService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public PublisherQueryLanguageService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/PublisherQueryLanguageServi" + "ce";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public ResultSet select(Statement selectStatement) {
      object[] results = this.Invoke("select", new object[] {selectStatement});
      return ((ResultSet) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Row {
    private Value[] valuesField;

    private string dummyField;

    [System.Xml.Serialization.XmlElementAttribute("values")]
    public Value[] values {
      get { return this.valuesField; }
      set { this.valuesField = value; }
    }

    public string dummy {
      get { return this.dummyField; }
      set { this.dummyField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ColumnType {
    private string labelNameField;

    public string labelName {
      get { return this.labelNameField; }
      set { this.labelNameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ResultSet {
    private ColumnType[] columnTypesField;

    private Row[] rowsField;

    [System.Xml.Serialization.XmlElementAttribute("columnTypes")]
    public ColumnType[] columnTypes {
      get { return this.columnTypesField; }
      set { this.columnTypesField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("rows")]
    public Row[] rows {
      get { return this.rowsField; }
      set { this.rowsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ReservationDetailsError : ApiError {
    private ReservationDetailsErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ReservationDetailsErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ReservationDetailsError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ReservationDetailsErrorReason {
    UNLIMITED_UNITS_BOUGHT_NOT_ALLOWED,
    UNLIMITED_END_DATE_TIME_NOT_ALLOWED,
    PERCENTAGE_UNITS_BOUGHT_TOO_HIGH,
    DURATION_NOT_ALLOWED,
    UNIT_TYPE_NOT_ALLOWED,
    COST_TYPE_NOT_ALLOWED,
    COST_TYPE_UNIT_TYPE_MISMATCH_NOT_ALLOWED,
    LINE_ITEM_TYPE_NOT_ALLOWED,
    NETWORK_REMNANT_ORDER_CANNOT_UPDATE_LINEITEM_TYPE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class RequiredSizeError : ApiError {
    private RequiredSizeErrorReason reasonField;

    private bool reasonFieldSpecified;

    public RequiredSizeErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RequiredSizeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum RequiredSizeErrorReason {
    REQUIRED,
    NOT_ALLOWED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class RequiredNumberError : ApiError {
    private RequiredNumberErrorReason reasonField;

    private bool reasonFieldSpecified;

    public RequiredNumberErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RequiredNumberError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum RequiredNumberErrorReason {
    REQUIRED,
    TOO_LARGE,
    TOO_SMALL,
    TOO_LARGE_WITH_DETAILS,
    TOO_SMALL_WITH_DETAILS
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class RequiredCollectionError : ApiError {
    private RequiredCollectionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public RequiredCollectionErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RequiredCollectionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum RequiredCollectionErrorReason {
    REQUIRED,
    TOO_LARGE,
    TOO_SMALL
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class PublisherQueryLanguageSyntaxError : ApiError {
    private PublisherQueryLanguageSyntaxErrorReason reasonField;

    private bool reasonFieldSpecified;

    public PublisherQueryLanguageSyntaxErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PublisherQueryLanguageSyntaxError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum PublisherQueryLanguageSyntaxErrorReason {
    UNPARSABLE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class PublisherQueryLanguageContextError : ApiError {
    private PublisherQueryLanguageContextErrorReason reasonField;

    private bool reasonFieldSpecified;

    public PublisherQueryLanguageContextErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PublisherQueryLanguageContextError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum PublisherQueryLanguageContextErrorReason {
    UNEXECUTABLE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class OrderError : ApiError {
    private OrderErrorReason reasonField;

    private bool reasonFieldSpecified;

    public OrderErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "OrderError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum OrderErrorReason {
    UPDATE_CANCELED_ORDER_NOT_ALLOWED,
    UPDATE_PENDING_APPROVAL_ORDER_NOT_ALLOWED,
    UPDATE_ARCHIVED_ORDER_NOT_ALLOWED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class OrderActionError : ApiError {
    private OrderActionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public OrderActionErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "OrderActionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum OrderActionErrorReason {
    PERMISSION_DENIED,
    NOT_APPLICABLE,
    IS_ARCHIVED,
    HAS_ENDED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItemOperationError : ApiError {
    private LineItemOperationErrorReason reasonField;

    private bool reasonFieldSpecified;

    public LineItemOperationErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemOperationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum LineItemOperationErrorReason {
    NOT_ALLOWED,
    NOT_APPLICABLE,
    HAS_COMPLETED,
    HAS_NO_ACTIVE_CREATIVES,
    CANNOT_ACTIVATE_LEGACY_DFP_LINE_ITEM
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItemFlightDateError : ApiError {
    private LineItemFlightDateErrorReason reasonField;

    private bool reasonFieldSpecified;

    public LineItemFlightDateErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemFlightDateError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum LineItemFlightDateErrorReason {
    START_DATE_TIME_IS_IN_PAST,
    END_DATE_TIME_IS_IN_PAST,
    END_DATE_TIME_NOT_AFTER_START_TIME,
    END_DATE_TIME_TOO_LATE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class InventoryUnitError : ApiError {
    private InventoryUnitErrorReason reasonField;

    private bool reasonFieldSpecified;

    public InventoryUnitErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InventoryUnitError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum InventoryUnitErrorReason {
    EXPLICIT_TARGETING_NOT_ALLOWED,
    EXPLICITLY_TARGETED_AD_UNIT_CANNOT_BE_PART_OF_ANY_PLACEMENT
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class InventoryTargetingError : ApiError {
    private InventoryTargetingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public InventoryTargetingErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InventoryTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum InventoryTargetingErrorReason {
    AT_LEAST_ONE_PLACEMENT_OR_INVENTORY_UNIT_REQUIRED,
    INVENTORY_CANNOT_BE_TARGETED_AND_EXCLUDED,
    PARENT_CONTAINS_INVALID_MIX_OF_TARGETED_AND_EXCLUDED_AD_UNITS,
    INVENTORY_EXCLUSIONS_MUST_HAVE_PARENT_INVENTORY_UNIT,
    INVENTORY_UNIT_CANNOT_BE_TARGETED_IF_ANCESTOR_IS_TARGETED,
    INVENTORY_UNIT_CANNOT_BE_TARGETED_IF_ANCESTOR_IS_EXCLUDED,
    INVENTORY_UNIT_CANNOT_BE_EXCLUDED_IF_ANCESTOR_IS_EXCLUDED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class InvalidUrlError : ApiError {
    private InvalidUrlErrorReason reasonField;

    private bool reasonFieldSpecified;

    public InvalidUrlErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InvalidUrlError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum InvalidUrlErrorReason {
    ILLEGAL_CHARACTERS,
    INVALID_FORMAT
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class InvalidEmailError : ApiError {
    private InvalidEmailErrorReason reasonField;

    private bool reasonFieldSpecified;

    public InvalidEmailErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InvalidEmailError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum InvalidEmailErrorReason {
    INVALID_FORMAT
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class FileError : ApiError {
    private FileErrorReason reasonField;

    private bool reasonFieldSpecified;

    public FileErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "FileError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum FileErrorReason {
    MISSING_CONTENTS,
    SIZE_TOO_LARGE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CreativeError : ApiError {
    private CreativeErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CreativeErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CreativeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CreativeErrorReason {
    FLASH_AND_FALLBACK_URL_ARE_SAME,
    INVALID_INTERNAL_REDIRECT_URL,
    DESTINATION_URL_REQUIRED,
    CANNOT_CREATE_OR_UPDATE_LEGACY_DFP_CREATIVE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class AdUnitHierarchyError : ApiError {
    private AdUnitHierarchyErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AdUnitHierarchyErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdUnitHierarchyError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum AdUnitHierarchyErrorReason {
    INVALID_DEPTH,
    INVALID_PARENT
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class AdUnitCodeError : ApiError {
    private AdUnitCodeErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AdUnitCodeErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdUnitCodeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum AdUnitCodeErrorReason {
    INVALID_CHARACTERS
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class AdUnitAfcSizeError : ApiError {
    private AdUnitAfcSizeErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AdUnitAfcSizeErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdUnitAfcSizeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum AdUnitAfcSizeErrorReason {
    INVALID,
    DOESNT_FIT,
    NOT_APPLICABLE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ReportServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ReportService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ReportService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/ReportService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public string getReportDownloadURL(long reportJobId, ExportFormat exportFormat) {
      object[] results = this.Invoke("getReportDownloadURL", new object[] {reportJobId, exportFormat});
      return ((string) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public ReportJob getReportJob(long reportJobId) {
      object[] results = this.Invoke("getReportJob", new object[] {reportJobId});
      return ((ReportJob) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public ReportJob runReportJob(ReportJob reportJob) {
      object[] results = this.Invoke("runReportJob", new object[] {reportJob});
      return ((ReportJob) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Date {
    private int yearField;

    private bool yearFieldSpecified;

    private int monthField;

    private bool monthFieldSpecified;

    private int dayField;

    private bool dayFieldSpecified;

    public int year {
      get { return this.yearField; }
      set {
        this.yearField = value;
        this.yearSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool yearSpecified {
      get { return this.yearFieldSpecified; }
      set { this.yearFieldSpecified = value; }
    }

    public int month {
      get { return this.monthField; }
      set {
        this.monthField = value;
        this.monthSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool monthSpecified {
      get { return this.monthFieldSpecified; }
      set { this.monthFieldSpecified = value; }
    }

    public int day {
      get { return this.dayField; }
      set {
        this.dayField = value;
        this.daySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool daySpecified {
      get { return this.dayFieldSpecified; }
      set { this.dayFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ReportQuery {
    private Dimension[] dimensionsField;

    private Column[] columnsField;

    private Date startDateField;

    private Date endDateField;

    private DateRangeType dateRangeTypeField;

    private bool dateRangeTypeFieldSpecified;

    private DimensionFilter[] dimensionFiltersField;

    [System.Xml.Serialization.XmlElementAttribute("dimensions")]
    public Dimension[] dimensions {
      get { return this.dimensionsField; }
      set { this.dimensionsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("columns")]
    public Column[] columns {
      get { return this.columnsField; }
      set { this.columnsField = value; }
    }

    public Date startDate {
      get { return this.startDateField; }
      set { this.startDateField = value; }
    }

    public Date endDate {
      get { return this.endDateField; }
      set { this.endDateField = value; }
    }

    public DateRangeType dateRangeType {
      get { return this.dateRangeTypeField; }
      set {
        this.dateRangeTypeField = value;
        this.dateRangeTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool dateRangeTypeSpecified {
      get { return this.dateRangeTypeFieldSpecified; }
      set { this.dateRangeTypeFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("dimensionFilters")]
    public DimensionFilter[] dimensionFilters {
      get { return this.dimensionFiltersField; }
      set { this.dimensionFiltersField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum Dimension {
    MONTH,
    WEEK,
    DATE,
    DAY,
    HOUR,
    LINE_ITEM,
    LINE_ITEM_TYPE,
    ORDER,
    ADVERTISER,
    SALESPERSON,
    CREATIVE,
    CREATIVE_SIZE,
    AD_UNIT,
    PLACEMENT,
    GENERIC_CRITERION_NAME,
    COUNTRY_NAME,
    REGION_NAME,
    CITY_NAME,
    METRO_NAME,
    CUSTOM_TARGETING
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum Column {
    AD_SERVER_IMPRESSIONS,
    AD_SERVER_CLICKS,
    AD_SERVER_CTR,
    AD_SERVER_REVENUE,
    AD_SERVER_AVERAGE_ECPM,
    AD_SERVER_PERCENT_IMPRESSIONS,
    AD_SERVER_PERCENT_CLICKS,
    AD_SERVER_PERCENT_REVENUE,
    AD_SERVER_DELIVERY_INDICATOR,
    ADSENSE_IMPRESSIONS,
    ADSENSE_CLICKS,
    ADSENSE_CTR,
    ADSENSE_REVENUE,
    ADSENSE_AVERAGE_ECPM,
    ADSENSE_PERCENT_IMPRESSIONS,
    ADSENSE_PERCENT_CLICKS,
    ADSENSE_PERCENT_REVENUE,
    TOTAL_IMPRESSIONS,
    TOTAL_CLICKS,
    TOTAL_CTR,
    TOTAL_REVENUE,
    TOTAL_AVERAGE_ECPM,
    TOTAL_UNFILLED_IMPRESSIONS,
    MERGED_AD_SERVER_IMPRESSIONS,
    MERGED_AD_SERVER_CLICKS,
    MERGED_AD_SERVER_CTR,
    MERGED_AD_SERVER_REVENUE,
    MERGED_AD_SERVER_AVERAGE_ECPM,
    OPTIMIZATION_CONTROL_IMPRESSIONS,
    OPTIMIZATION_CONTROL_CLICKS,
    OPTIMIZATION_CONTROL_CTR,
    OPTIMIZATION_OPTIMIZED_IMPRESSIONS,
    OPTIMIZATION_OPTIMIZED_CLICKS,
    OPTIMIZATION_OPTIMIZED_CTR,
    OPTIMIZATION_LIFT
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum DateRangeType {
    TODAY,
    YESTERDAY,
    LAST_WEEK,
    LAST_MONTH,
    CUSTOM_DATE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum DimensionFilter {
    ADVERTISERS,
    ACTIVE_ADVERTISERS,
    HOUSE_ADVERTISERS,
    NON_HOUSE_ADVERTISERS,
    ALL_SALESPEOPLE,
    WHOLE_NETWORK,
    CURRENT_USER_ORDERS,
    STARTED_ORDERS,
    COMPLETED_ORDERS,
    ACTIVE_AD_UNITS,
    INACTIVE_AD_UNITS,
    ARCHIVED_AD_UNITS,
    ACTIVE_PLACEMENTS,
    INACTIVE_PLACEMENTS,
    ARCHIVED_PLACEMENTS,
    OPTIMIZABLE_ORDERS
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ReportJob {
    private long idField;

    private bool idFieldSpecified;

    private ReportQuery reportQueryField;

    private ReportJobStatus reportJobStatusField;

    private bool reportJobStatusFieldSpecified;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public ReportQuery reportQuery {
      get { return this.reportQueryField; }
      set { this.reportQueryField = value; }
    }

    public ReportJobStatus reportJobStatus {
      get { return this.reportJobStatusField; }
      set {
        this.reportJobStatusField = value;
        this.reportJobStatusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reportJobStatusSpecified {
      get { return this.reportJobStatusFieldSpecified; }
      set { this.reportJobStatusFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ReportJobStatus {
    COMPLETED,
    IN_PROGRESS,
    FAILED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ReportError : ApiError {
    private ReportErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ReportErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ReportError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ReportErrorReason {
    DEFAULT,
    REPORT_ACCESS_NOT_ALLOWED,
    DIMENSION_VIEW_NOT_ALLOWED,
    COLUMN_VIEW_NOT_ALLOWED,
    TOO_MANY_CONCURRENT_REPORTS,
    REPORT_TOO_BIG,
    INVALID_DIMENSIONS,
    INVALID_COLUMNS,
    INVALID_DIMENSION_FILTERS,
    END_DATE_TIME_NOT_AFTER_START_TIME,
    NOT_NULL,
    COLUMNS_NOT_SUPPORTED_FOR_REQUESTED_DIMENSIONS,
    FAILED_TO_STORE_REPORT,
    REPORT_NOT_FOUND,
    SR_CANNOT_RUN_REPORT_IN_ANOTHER_NETWORK
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ExportFormat {
    TSV,
    CSV
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "UserServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserRecord))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class UserService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public UserService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/UserService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public User createUser(User user) {
      object[] results = this.Invoke("createUser", new object[] {user});
      return ((User) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public User[] createUsers([System.Xml.Serialization.XmlElementAttribute("users")]
User[] users) {
      object[] results = this.Invoke("createUsers", new object[] {users});
      return ((User[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Role[] getAllRoles() {
      object[] results = this.Invoke("getAllRoles", new object[0]);
      return ((Role[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public User getUser(long userId) {
      object[] results = this.Invoke("getUser", new object[] {userId});
      return ((User) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public UserPage getUsersByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getUsersByStatement", new object[] {filterStatement});
      return ((UserPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public UpdateResult performUserAction(UserAction userAction, Statement filterStatement) {
      object[] results = this.Invoke("performUserAction", new object[] {userAction, filterStatement});
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public User updateUser(User user) {
      object[] results = this.Invoke("updateUser", new object[] {user});
      return ((User) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public User[] updateUsers([System.Xml.Serialization.XmlElementAttribute("users")]
User[] users) {
      object[] results = this.Invoke("updateUsers", new object[] {users});
      return ((User[]) (results[0]));
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateUsers))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateUsers))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class UserAction {
    private string userActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("UserAction.Type")]
    public string UserActionType {
      get { return this.userActionTypeField; }
      set { this.userActionTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DeactivateUsers : UserAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ActivateUsers : UserAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class UserPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private User[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public User[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class User : UserRecord {
    private bool isActiveField;

    private bool isActiveFieldSpecified;

    private bool isEmailNotificationAllowedField;

    private bool isEmailNotificationAllowedFieldSpecified;

    public bool isActive {
      get { return this.isActiveField; }
      set {
        this.isActiveField = value;
        this.isActiveSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isActiveSpecified {
      get { return this.isActiveFieldSpecified; }
      set { this.isActiveFieldSpecified = value; }
    }

    public bool isEmailNotificationAllowed {
      get { return this.isEmailNotificationAllowedField; }
      set {
        this.isEmailNotificationAllowedField = value;
        this.isEmailNotificationAllowedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isEmailNotificationAllowedSpecified {
      get { return this.isEmailNotificationAllowedFieldSpecified; }
      set { this.isEmailNotificationAllowedFieldSpecified = value; }
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(User))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class UserRecord {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string emailField;

    private long roleIdField;

    private bool roleIdFieldSpecified;

    private string roleNameField;

    private string preferredLocaleField;

    private string userRecordTypeField;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public string email {
      get { return this.emailField; }
      set { this.emailField = value; }
    }

    public long roleId {
      get { return this.roleIdField; }
      set {
        this.roleIdField = value;
        this.roleIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool roleIdSpecified {
      get { return this.roleIdFieldSpecified; }
      set { this.roleIdFieldSpecified = value; }
    }

    public string roleName {
      get { return this.roleNameField; }
      set { this.roleNameField = value; }
    }

    public string preferredLocale {
      get { return this.preferredLocaleField; }
      set { this.preferredLocaleField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("UserRecord.Type")]
    public string UserRecordType {
      get { return this.userRecordTypeField; }
      set { this.userRecordTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Role {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string descriptionField;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CompanyServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CompanyService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CompanyService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/CompanyService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Company[] createCompanies([System.Xml.Serialization.XmlElementAttribute("companies")]
Company[] companies) {
      object[] results = this.Invoke("createCompanies", new object[] {companies});
      return ((Company[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Company createCompany(Company company) {
      object[] results = this.Invoke("createCompany", new object[] {company});
      return ((Company) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public CompanyPage getCompaniesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCompaniesByStatement", new object[] {filterStatement});
      return ((CompanyPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Company getCompany(long companyId) {
      object[] results = this.Invoke("getCompany", new object[] {companyId});
      return ((Company) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Company[] updateCompanies([System.Xml.Serialization.XmlElementAttribute("companies")]
Company[] companies) {
      object[] results = this.Invoke("updateCompanies", new object[] {companies});
      return ((Company[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Company updateCompany(Company company) {
      object[] results = this.Invoke("updateCompany", new object[] {company});
      return ((Company) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CompanyPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Company[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public Company[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Company {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private CompanyType typeField;

    private bool typeFieldSpecified;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public CompanyType type {
      get { return this.typeField; }
      set {
        this.typeField = value;
        this.typeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool typeSpecified {
      get { return this.typeFieldSpecified; }
      set { this.typeFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Company.Type", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CompanyType {
    HOUSE_ADVERTISER,
    HOUSE_AGENCY,
    ADVERTISER,
    AGENCY,
    AD_NETWORK
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CreativeServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CreativeService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CreativeService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/CreativeService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Creative createCreative(Creative creative) {
      object[] results = this.Invoke("createCreative", new object[] {creative});
      return ((Creative) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Creative[] createCreatives([System.Xml.Serialization.XmlElementAttribute("creatives")]
Creative[] creatives) {
      object[] results = this.Invoke("createCreatives", new object[] {creatives});
      return ((Creative[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Creative getCreative(long creativeId) {
      object[] results = this.Invoke("getCreative", new object[] {creativeId});
      return ((Creative) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public CreativePage getCreativesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCreativesByStatement", new object[] {filterStatement});
      return ((CreativePage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Creative updateCreative(Creative creative) {
      object[] results = this.Invoke("updateCreative", new object[] {creative});
      return ((Creative) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Creative[] updateCreatives([System.Xml.Serialization.XmlElementAttribute("creatives")]
Creative[] creatives) {
      object[] results = this.Invoke("updateCreatives", new object[] {creatives});
      return ((Creative[]) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CreativePage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Creative[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public Creative[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ThirdPartyCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(HasDestinationUrlCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class Creative {
    private long advertiserIdField;

    private bool advertiserIdFieldSpecified;

    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private Size sizeField;

    private string previewUrlField;

    private string creativeTypeField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set {
        this.advertiserIdField = value;
        this.advertiserIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool advertiserIdSpecified {
      get { return this.advertiserIdFieldSpecified; }
      set { this.advertiserIdFieldSpecified = value; }
    }

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public Size size {
      get { return this.sizeField; }
      set { this.sizeField = value; }
    }

    public string previewUrl {
      get { return this.previewUrlField; }
      set { this.previewUrlField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("Creative.Type")]
    public string CreativeType {
      get { return this.creativeTypeField; }
      set { this.creativeTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Size {
    private int widthField;

    private bool widthFieldSpecified;

    private int heightField;

    private bool heightFieldSpecified;

    private bool isAspectRatioField;

    private bool isAspectRatioFieldSpecified;

    public int width {
      get { return this.widthField; }
      set {
        this.widthField = value;
        this.widthSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool widthSpecified {
      get { return this.widthFieldSpecified; }
      set { this.widthFieldSpecified = value; }
    }

    public int height {
      get { return this.heightField; }
      set {
        this.heightField = value;
        this.heightSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool heightSpecified {
      get { return this.heightFieldSpecified; }
      set { this.heightFieldSpecified = value; }
    }

    public bool isAspectRatio {
      get { return this.isAspectRatioField; }
      set {
        this.isAspectRatioField = value;
        this.isAspectRatioSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isAspectRatioSpecified {
      get { return this.isAspectRatioFieldSpecified; }
      set { this.isAspectRatioFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ThirdPartyCreative : Creative {
    private string snippetField;

    private string expandedSnippetField;

    public string snippet {
      get { return this.snippetField; }
      set { this.snippetField = value; }
    }

    public string expandedSnippet {
      get { return this.expandedSnippetField; }
      set { this.expandedSnippetField = value; }
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class HasDestinationUrlCreative : Creative {
    private string destinationUrlField;

    public string destinationUrl {
      get { return this.destinationUrlField; }
      set { this.destinationUrlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ImageRedirectCreative : HasDestinationUrlCreative {
    private string imageUrlField;

    public string imageUrl {
      get { return this.imageUrlField; }
      set { this.imageUrlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ImageCreative : HasDestinationUrlCreative {
    private string imageNameField;

    private byte[] imageByteArrayField;

    private bool overrideSizeField;

    private bool overrideSizeFieldSpecified;

    private Size assetSizeField;

    private string imageUrlField;

    public string imageName {
      get { return this.imageNameField; }
      set { this.imageNameField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
    public byte[] imageByteArray {
      get { return this.imageByteArrayField; }
      set { this.imageByteArrayField = value; }
    }

    public bool overrideSize {
      get { return this.overrideSizeField; }
      set {
        this.overrideSizeField = value;
        this.overrideSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool overrideSizeSpecified {
      get { return this.overrideSizeFieldSpecified; }
      set { this.overrideSizeFieldSpecified = value; }
    }

    public Size assetSize {
      get { return this.assetSizeField; }
      set { this.assetSizeField = value; }
    }

    public string imageUrl {
      get { return this.imageUrlField; }
      set { this.imageUrlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class FlashRedirectCreative : HasDestinationUrlCreative {
    private string flashUrlField;

    private string fallbackUrlField;

    private string fallbackPreviewUrlField;

    public string flashUrl {
      get { return this.flashUrlField; }
      set { this.flashUrlField = value; }
    }

    public string fallbackUrl {
      get { return this.fallbackUrlField; }
      set { this.fallbackUrlField = value; }
    }

    public string fallbackPreviewUrl {
      get { return this.fallbackPreviewUrlField; }
      set { this.fallbackPreviewUrlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class FlashCreative : HasDestinationUrlCreative {
    private string flashNameField;

    private byte[] flashByteArrayField;

    private string fallbackImageNameField;

    private byte[] fallbackImageByteArrayField;

    private bool overrideSizeField;

    private bool overrideSizeFieldSpecified;

    private bool clickTagRequiredField;

    private bool clickTagRequiredFieldSpecified;

    private string fallbackPreviewUrlField;

    private Size flashAssetSizeField;

    private Size fallbackAssetSizeField;

    public string flashName {
      get { return this.flashNameField; }
      set { this.flashNameField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
    public byte[] flashByteArray {
      get { return this.flashByteArrayField; }
      set { this.flashByteArrayField = value; }
    }

    public string fallbackImageName {
      get { return this.fallbackImageNameField; }
      set { this.fallbackImageNameField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
    public byte[] fallbackImageByteArray {
      get { return this.fallbackImageByteArrayField; }
      set { this.fallbackImageByteArrayField = value; }
    }

    public bool overrideSize {
      get { return this.overrideSizeField; }
      set {
        this.overrideSizeField = value;
        this.overrideSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool overrideSizeSpecified {
      get { return this.overrideSizeFieldSpecified; }
      set { this.overrideSizeFieldSpecified = value; }
    }

    public bool clickTagRequired {
      get { return this.clickTagRequiredField; }
      set {
        this.clickTagRequiredField = value;
        this.clickTagRequiredSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool clickTagRequiredSpecified {
      get { return this.clickTagRequiredFieldSpecified; }
      set { this.clickTagRequiredFieldSpecified = value; }
    }

    public string fallbackPreviewUrl {
      get { return this.fallbackPreviewUrlField; }
      set { this.fallbackPreviewUrlField = value; }
    }

    public Size flashAssetSize {
      get { return this.flashAssetSizeField; }
      set { this.flashAssetSizeField = value; }
    }

    public Size fallbackAssetSize {
      get { return this.fallbackAssetSizeField; }
      set { this.fallbackAssetSizeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ImageError : ApiError {
    private ImageErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ImageErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ImageError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ImageErrorReason {
    INVALID_IMAGE,
    INVALID_SIZE,
    UNEXPECTED_SIZE,
    ANIMATED_NOT_ALLOWED,
    ANIMATION_TOO_LONG,
    CMYK_JPEG_NOT_ALLOWED,
    FLASH_NOT_ALLOWED,
    FLASH_WITHOUT_CLICKTAG,
    ANIMATED_VISUAL_EFFECT,
    FLASH_ERROR,
    LAYOUT_PROBLEM,
    FLASH_HAS_NETWORK_OBJECTS,
    FLASH_HAS_NETWORK_METHODS,
    FLASH_HAS_URL,
    FLASH_HAS_MOUSE_TRACKING,
    FLASH_HAS_RANDOM_NUM,
    FLASH_SELF_TARGETS,
    FLASH_BAD_GETURL_TARGET,
    FLASH_VERSION_NOT_SUPPORTED,
    FILE_TOO_LARGE,
    SYSTEM_ERROR
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CustomTargetingServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CustomTargetingService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CustomTargetingService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/CustomTargetingService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public CustomTargetingKey[] createCustomTargetingKeys([System.Xml.Serialization.XmlElementAttribute("keys")]
CustomTargetingKey[] keys) {
      object[] results = this.Invoke("createCustomTargetingKeys", new object[] {keys});
      return ((CustomTargetingKey[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public CustomTargetingValue[] createCustomTargetingValues([System.Xml.Serialization.XmlElementAttribute("values")]
CustomTargetingValue[] values) {
      object[] results = this.Invoke("createCustomTargetingValues", new object[] {values});
      return ((CustomTargetingValue[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public CustomTargetingKeyPage getCustomTargetingKeysByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCustomTargetingKeysByStatement", new object[] {filterStatement});
      return ((CustomTargetingKeyPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public CustomTargetingValuePage getCustomTargetingValuesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCustomTargetingValuesByStatement", new object[] {filterStatement});
      return ((CustomTargetingValuePage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public UpdateResult performCustomTargetingKeyAction(CustomTargetingKeyAction customTargetingKeyAction, Statement filterStatement) {
      object[] results = this.Invoke("performCustomTargetingKeyAction", new object[] {customTargetingKeyAction, filterStatement});
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public UpdateResult performCustomTargetingValueAction(CustomTargetingValueAction customTargetingValueAction, Statement filterStatement) {
      object[] results = this.Invoke("performCustomTargetingValueAction", new object[] {customTargetingValueAction, filterStatement});
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public CustomTargetingKey[] updateCustomTargetingKeys([System.Xml.Serialization.XmlElementAttribute("keys")]
CustomTargetingKey[] keys) {
      object[] results = this.Invoke("updateCustomTargetingKeys", new object[] {keys});
      return ((CustomTargetingKey[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public CustomTargetingValue[] updateCustomTargetingValues([System.Xml.Serialization.XmlElementAttribute("values")]
CustomTargetingValue[] values) {
      object[] results = this.Invoke("updateCustomTargetingValues", new object[] {values});
      return ((CustomTargetingValue[]) (results[0]));
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteCustomTargetingValues))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class CustomTargetingValueAction {
    private string customTargetingValueActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("CustomTargetingValueAction.Type")]
    public string CustomTargetingValueActionType {
      get { return this.customTargetingValueActionTypeField; }
      set { this.customTargetingValueActionTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DeleteCustomTargetingValues : CustomTargetingValueAction {
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteCustomTargetingKeys))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class CustomTargetingKeyAction {
    private string customTargetingKeyActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("CustomTargetingKeyAction.Type")]
    public string CustomTargetingKeyActionType {
      get { return this.customTargetingKeyActionTypeField; }
      set { this.customTargetingKeyActionTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DeleteCustomTargetingKeys : CustomTargetingKeyAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CustomTargetingValuePage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private CustomTargetingValue[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public CustomTargetingValue[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CustomTargetingValue {
    private long customTargetingKeyIdField;

    private bool customTargetingKeyIdFieldSpecified;

    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string displayNameField;

    private CustomTargetingValueMatchType matchTypeField;

    private bool matchTypeFieldSpecified;

    public long customTargetingKeyId {
      get { return this.customTargetingKeyIdField; }
      set {
        this.customTargetingKeyIdField = value;
        this.customTargetingKeyIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool customTargetingKeyIdSpecified {
      get { return this.customTargetingKeyIdFieldSpecified; }
      set { this.customTargetingKeyIdFieldSpecified = value; }
    }

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public string displayName {
      get { return this.displayNameField; }
      set { this.displayNameField = value; }
    }

    public CustomTargetingValueMatchType matchType {
      get { return this.matchTypeField; }
      set {
        this.matchTypeField = value;
        this.matchTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool matchTypeSpecified {
      get { return this.matchTypeFieldSpecified; }
      set { this.matchTypeFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomTargetingValue.MatchType", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CustomTargetingValueMatchType {
    EXACT,
    BROAD,
    PREFIX,
    BROAD_PREFIX
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CustomTargetingKeyPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private CustomTargetingKey[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public CustomTargetingKey[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CustomTargetingKey {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string displayNameField;

    private CustomTargetingKeyType typeField;

    private bool typeFieldSpecified;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public string displayName {
      get { return this.displayNameField; }
      set { this.displayNameField = value; }
    }

    public CustomTargetingKeyType type {
      get { return this.typeField; }
      set {
        this.typeField = value;
        this.typeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool typeSpecified {
      get { return this.typeFieldSpecified; }
      set { this.typeFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomTargetingKey.Type", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CustomTargetingKeyType {
    PREDEFINED,
    FREEFORM
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CustomTargetingError : ApiError {
    private CustomTargetingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CustomTargetingErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CustomTargetingErrorReason {
    KEY_NOT_FOUND,
    KEY_COUNT_TOO_LARGE,
    KEY_NAME_DUPLICATE,
    KEY_NAME_EMPTY,
    KEY_NAME_INVALID_LENGTH,
    KEY_NAME_INVALID_CHARS,
    KEY_DISPLAY_NAME_INVALID_LENGTH,
    VALUE_NOT_FOUND,
    GET_VALUES_BY_STATEMENT_MUST_CONTAIN_KEY_ID,
    VALUE_COUNT_FOR_KEY_TOO_LARGE,
    VALUE_NAME_DUPLICATE,
    VALUE_NAME_EMPTY,
    VALUE_NAME_INVALID_LENGTH,
    VALUE_NAME_INVALID_CHARS,
    VALUE_DISPLAY_NAME_INVALID_LENGTH,
    VALUE_MATCH_TYPE_NOT_ALLOWED,
    VALUE_MATCH_TYPE_NOT_EXACT_FOR_PREDEFINED_KEY,
    KEY_WITH_MISSING_VALUES,
    CANNOT_OR_DIFFERENT_KEYS,
    CANNOT_AND_SAME_KEYS,
    INVALID_TARGETING_EXPRESSION,
    DELETED_KEY_CANNOT_BE_USED_FOR_TARGETING,
    DELETED_VALUE_CANNOT_BE_USED_FOR_TARGETING
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ForecastServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemSummary))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ForecastService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ForecastService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/ForecastService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Forecast getForecast(LineItem lineItem) {
      object[] results = this.Invoke("getForecast", new object[] {lineItem});
      return ((Forecast) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Forecast getForecastById(long lineItemId) {
      object[] results = this.Invoke("getForecastById", new object[] {lineItemId});
      return ((Forecast) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Forecast {
    private long idField;

    private bool idFieldSpecified;

    private long orderIdField;

    private bool orderIdFieldSpecified;

    private UnitType unitTypeField;

    private bool unitTypeFieldSpecified;

    private long availableUnitsField;

    private bool availableUnitsFieldSpecified;

    private long deliveredUnitsField;

    private bool deliveredUnitsFieldSpecified;

    private long matchedUnitsField;

    private bool matchedUnitsFieldSpecified;

    private long possibleUnitsField;

    private bool possibleUnitsFieldSpecified;

    private long reservedUnitsField;

    private bool reservedUnitsFieldSpecified;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public long orderId {
      get { return this.orderIdField; }
      set {
        this.orderIdField = value;
        this.orderIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool orderIdSpecified {
      get { return this.orderIdFieldSpecified; }
      set { this.orderIdFieldSpecified = value; }
    }

    public UnitType unitType {
      get { return this.unitTypeField; }
      set {
        this.unitTypeField = value;
        this.unitTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool unitTypeSpecified {
      get { return this.unitTypeFieldSpecified; }
      set { this.unitTypeFieldSpecified = value; }
    }

    public long availableUnits {
      get { return this.availableUnitsField; }
      set {
        this.availableUnitsField = value;
        this.availableUnitsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool availableUnitsSpecified {
      get { return this.availableUnitsFieldSpecified; }
      set { this.availableUnitsFieldSpecified = value; }
    }

    public long deliveredUnits {
      get { return this.deliveredUnitsField; }
      set {
        this.deliveredUnitsField = value;
        this.deliveredUnitsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool deliveredUnitsSpecified {
      get { return this.deliveredUnitsFieldSpecified; }
      set { this.deliveredUnitsFieldSpecified = value; }
    }

    public long matchedUnits {
      get { return this.matchedUnitsField; }
      set {
        this.matchedUnitsField = value;
        this.matchedUnitsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool matchedUnitsSpecified {
      get { return this.matchedUnitsFieldSpecified; }
      set { this.matchedUnitsFieldSpecified = value; }
    }

    public long possibleUnits {
      get { return this.possibleUnitsField; }
      set {
        this.possibleUnitsField = value;
        this.possibleUnitsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool possibleUnitsSpecified {
      get { return this.possibleUnitsFieldSpecified; }
      set { this.possibleUnitsFieldSpecified = value; }
    }

    public long reservedUnits {
      get { return this.reservedUnitsField; }
      set {
        this.reservedUnitsField = value;
        this.reservedUnitsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reservedUnitsSpecified {
      get { return this.reservedUnitsFieldSpecified; }
      set { this.reservedUnitsFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum UnitType {
    IMPRESSIONS,
    CLICKS
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class UserDomainTargeting {
    private string[] domainsField;

    private bool targetedField;

    private bool targetedFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("domains")]
    public string[] domains {
      get { return this.domainsField; }
      set { this.domainsField = value; }
    }

    public bool targeted {
      get { return this.targetedField; }
      set {
        this.targetedField = value;
        this.targetedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool targetedSpecified {
      get { return this.targetedFieldSpecified; }
      set { this.targetedFieldSpecified = value; }
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomCriteriaSet))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomCriteria))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class CustomCriteriaNode {
    private string customCriteriaNodeTypeField;

    [System.Xml.Serialization.XmlElementAttribute("CustomCriteriaNode.Type")]
    public string CustomCriteriaNodeType {
      get { return this.customCriteriaNodeTypeField; }
      set { this.customCriteriaNodeTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CustomCriteriaSet : CustomCriteriaNode {
    private CustomCriteriaSetLogicalOperator logicalOperatorField;

    private bool logicalOperatorFieldSpecified;

    private CustomCriteriaNode[] childrenField;

    public CustomCriteriaSetLogicalOperator logicalOperator {
      get { return this.logicalOperatorField; }
      set {
        this.logicalOperatorField = value;
        this.logicalOperatorSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool logicalOperatorSpecified {
      get { return this.logicalOperatorFieldSpecified; }
      set { this.logicalOperatorFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("children")]
    public CustomCriteriaNode[] children {
      get { return this.childrenField; }
      set { this.childrenField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomCriteriaSet.LogicalOperator", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CustomCriteriaSetLogicalOperator {
    AND,
    OR
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CustomCriteria : CustomCriteriaNode {
    private long keyIdField;

    private bool keyIdFieldSpecified;

    private long[] valueIdsField;

    private CustomCriteriaComparisonOperator operatorField;

    private bool operatorFieldSpecified;

    public long keyId {
      get { return this.keyIdField; }
      set {
        this.keyIdField = value;
        this.keyIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool keyIdSpecified {
      get { return this.keyIdFieldSpecified; }
      set { this.keyIdFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("valueIds")]
    public long[] valueIds {
      get { return this.valueIdsField; }
      set { this.valueIdsField = value; }
    }

    public CustomCriteriaComparisonOperator @operator {
      get { return this.operatorField; }
      set {
        this.operatorField = value;
        this.operatorSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool operatorSpecified {
      get { return this.operatorFieldSpecified; }
      set { this.operatorFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomCriteria.ComparisonOperator", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CustomCriteriaComparisonOperator {
    IS,
    IS_NOT
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class TimeOfDay {
    private int hourField;

    private bool hourFieldSpecified;

    private MinuteOfHour minuteField;

    private bool minuteFieldSpecified;

    public int hour {
      get { return this.hourField; }
      set {
        this.hourField = value;
        this.hourSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool hourSpecified {
      get { return this.hourFieldSpecified; }
      set { this.hourFieldSpecified = value; }
    }

    public MinuteOfHour minute {
      get { return this.minuteField; }
      set {
        this.minuteField = value;
        this.minuteSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool minuteSpecified {
      get { return this.minuteFieldSpecified; }
      set { this.minuteFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum MinuteOfHour {
    ZERO,
    FIFTEEN,
    THIRTY,
    FORTY_FIVE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DayPart {
    private DayOfWeek dayOfWeekField;

    private bool dayOfWeekFieldSpecified;

    private TimeOfDay startTimeField;

    private TimeOfDay endTimeField;

    public DayOfWeek dayOfWeek {
      get { return this.dayOfWeekField; }
      set {
        this.dayOfWeekField = value;
        this.dayOfWeekSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool dayOfWeekSpecified {
      get { return this.dayOfWeekFieldSpecified; }
      set { this.dayOfWeekFieldSpecified = value; }
    }

    public TimeOfDay startTime {
      get { return this.startTimeField; }
      set { this.startTimeField = value; }
    }

    public TimeOfDay endTime {
      get { return this.endTimeField; }
      set { this.endTimeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum DayOfWeek {
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY,
    SUNDAY
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DayPartTargeting {
    private DayPart[] dayPartsField;

    private DeliveryTimeZone timeZoneField;

    private bool timeZoneFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("dayParts")]
    public DayPart[] dayParts {
      get { return this.dayPartsField; }
      set { this.dayPartsField = value; }
    }

    public DeliveryTimeZone timeZone {
      get { return this.timeZoneField; }
      set {
        this.timeZoneField = value;
        this.timeZoneSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool timeZoneSpecified {
      get { return this.timeZoneFieldSpecified; }
      set { this.timeZoneFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum DeliveryTimeZone {
    PUBLISHER,
    BROWSER
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class InventoryTargeting {
    private string[] targetedAdUnitIdsField;

    private string[] excludedAdUnitIdsField;

    private long[] targetedPlacementIdsField;

    [System.Xml.Serialization.XmlElementAttribute("targetedAdUnitIds")]
    public string[] targetedAdUnitIds {
      get { return this.targetedAdUnitIdsField; }
      set { this.targetedAdUnitIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedAdUnitIds")]
    public string[] excludedAdUnitIds {
      get { return this.excludedAdUnitIdsField; }
      set { this.excludedAdUnitIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("targetedPlacementIds")]
    public long[] targetedPlacementIds {
      get { return this.targetedPlacementIdsField; }
      set { this.targetedPlacementIdsField = value; }
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RegionLocation))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(MetroLocation))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CountryLocation))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CityLocation))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Location {
    private long idField;

    private bool idFieldSpecified;

    private string locationTypeField;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("Location.Type")]
    public string LocationType {
      get { return this.locationTypeField; }
      set { this.locationTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class RegionLocation : Location {
    private string regionCodeField;

    public string regionCode {
      get { return this.regionCodeField; }
      set { this.regionCodeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class MetroLocation : Location {
    private string metroCodeField;

    private string countryCodeField;

    public string metroCode {
      get { return this.metroCodeField; }
      set { this.metroCodeField = value; }
    }

    public string countryCode {
      get { return this.countryCodeField; }
      set { this.countryCodeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CountryLocation : Location {
    private string countryCodeField;

    public string countryCode {
      get { return this.countryCodeField; }
      set { this.countryCodeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class CityLocation : Location {
    private string cityNameField;

    private string regionCodeField;

    private string countryCodeField;

    public string cityName {
      get { return this.cityNameField; }
      set { this.cityNameField = value; }
    }

    public string regionCode {
      get { return this.regionCodeField; }
      set { this.regionCodeField = value; }
    }

    public string countryCode {
      get { return this.countryCodeField; }
      set { this.countryCodeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class GeoTargeting {
    private Location[] targetedLocationsField;

    private Location[] excludedLocationsField;

    [System.Xml.Serialization.XmlElementAttribute("targetedLocations")]
    public Location[] targetedLocations {
      get { return this.targetedLocationsField; }
      set { this.targetedLocationsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedLocations")]
    public Location[] excludedLocations {
      get { return this.excludedLocationsField; }
      set { this.excludedLocationsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Targeting {
    private GeoTargeting geoTargetingField;

    private InventoryTargeting inventoryTargetingField;

    private DayPartTargeting dayPartTargetingField;

    private CustomCriteriaSet customTargetingField;

    private UserDomainTargeting userDomainTargetingField;

    public GeoTargeting geoTargeting {
      get { return this.geoTargetingField; }
      set { this.geoTargetingField = value; }
    }

    public InventoryTargeting inventoryTargeting {
      get { return this.inventoryTargetingField; }
      set { this.inventoryTargetingField = value; }
    }

    public DayPartTargeting dayPartTargeting {
      get { return this.dayPartTargetingField; }
      set { this.dayPartTargetingField = value; }
    }

    public CustomCriteriaSet customTargeting {
      get { return this.customTargetingField; }
      set { this.customTargetingField = value; }
    }

    public UserDomainTargeting userDomainTargeting {
      get { return this.userDomainTargetingField; }
      set { this.userDomainTargetingField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DeliveryIndicator {
    private double expectedDeliveryPercentageField;

    private bool expectedDeliveryPercentageFieldSpecified;

    private double actualDeliveryPercentageField;

    private bool actualDeliveryPercentageFieldSpecified;

    public double expectedDeliveryPercentage {
      get { return this.expectedDeliveryPercentageField; }
      set {
        this.expectedDeliveryPercentageField = value;
        this.expectedDeliveryPercentageSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool expectedDeliveryPercentageSpecified {
      get { return this.expectedDeliveryPercentageFieldSpecified; }
      set { this.expectedDeliveryPercentageFieldSpecified = value; }
    }

    public double actualDeliveryPercentage {
      get { return this.actualDeliveryPercentageField; }
      set {
        this.actualDeliveryPercentageField = value;
        this.actualDeliveryPercentageSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool actualDeliveryPercentageSpecified {
      get { return this.actualDeliveryPercentageFieldSpecified; }
      set { this.actualDeliveryPercentageFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Stats {
    private long impressionsDeliveredField;

    private bool impressionsDeliveredFieldSpecified;

    private long clicksDeliveredField;

    private bool clicksDeliveredFieldSpecified;

    public long impressionsDelivered {
      get { return this.impressionsDeliveredField; }
      set {
        this.impressionsDeliveredField = value;
        this.impressionsDeliveredSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool impressionsDeliveredSpecified {
      get { return this.impressionsDeliveredFieldSpecified; }
      set { this.impressionsDeliveredFieldSpecified = value; }
    }

    public long clicksDelivered {
      get { return this.clicksDeliveredField; }
      set {
        this.clicksDeliveredField = value;
        this.clicksDeliveredSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool clicksDeliveredSpecified {
      get { return this.clicksDeliveredFieldSpecified; }
      set { this.clicksDeliveredFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Money {
    private string currencyCodeField;

    private long microAmountField;

    private bool microAmountFieldSpecified;

    public string currencyCode {
      get { return this.currencyCodeField; }
      set { this.currencyCodeField = value; }
    }

    public long microAmount {
      get { return this.microAmountField; }
      set {
        this.microAmountField = value;
        this.microAmountSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool microAmountSpecified {
      get { return this.microAmountFieldSpecified; }
      set { this.microAmountFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class FrequencyCap {
    private int maxImpressionsField;

    private bool maxImpressionsFieldSpecified;

    private TimeUnit timeUnitField;

    private bool timeUnitFieldSpecified;

    public int maxImpressions {
      get { return this.maxImpressionsField; }
      set {
        this.maxImpressionsField = value;
        this.maxImpressionsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool maxImpressionsSpecified {
      get { return this.maxImpressionsFieldSpecified; }
      set { this.maxImpressionsFieldSpecified = value; }
    }

    public TimeUnit timeUnit {
      get { return this.timeUnitField; }
      set {
        this.timeUnitField = value;
        this.timeUnitSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool timeUnitSpecified {
      get { return this.timeUnitFieldSpecified; }
      set { this.timeUnitFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum TimeUnit {
    MINUTE,
    HOUR,
    DAY,
    WEEK,
    MONTH,
    LIFETIME
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DateTime {
    private Date dateField;

    private int hourField;

    private bool hourFieldSpecified;

    private int minuteField;

    private bool minuteFieldSpecified;

    private int secondField;

    private bool secondFieldSpecified;

    private string timeZoneIDField;

    public Date date {
      get { return this.dateField; }
      set { this.dateField = value; }
    }

    public int hour {
      get { return this.hourField; }
      set {
        this.hourField = value;
        this.hourSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool hourSpecified {
      get { return this.hourFieldSpecified; }
      set { this.hourFieldSpecified = value; }
    }

    public int minute {
      get { return this.minuteField; }
      set {
        this.minuteField = value;
        this.minuteSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool minuteSpecified {
      get { return this.minuteFieldSpecified; }
      set { this.minuteFieldSpecified = value; }
    }

    public int second {
      get { return this.secondField; }
      set {
        this.secondField = value;
        this.secondSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool secondSpecified {
      get { return this.secondFieldSpecified; }
      set { this.secondFieldSpecified = value; }
    }

    public string timeZoneID {
      get { return this.timeZoneIDField; }
      set { this.timeZoneIDField = value; }
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItem))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItemSummary {
    private long orderIdField;

    private bool orderIdFieldSpecified;

    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string orderNameField;

    private DateTime startDateTimeField;

    private StartDateTimeType startDateTimeTypeField;

    private bool startDateTimeTypeFieldSpecified;

    private DateTime endDateTimeField;

    private bool unlimitedEndDateTimeField;

    private bool unlimitedEndDateTimeFieldSpecified;

    private CreativeRotationType creativeRotationTypeField;

    private bool creativeRotationTypeFieldSpecified;

    private DeliveryRateType deliveryRateTypeField;

    private bool deliveryRateTypeFieldSpecified;

    private RoadblockingType roadblockingTypeField;

    private bool roadblockingTypeFieldSpecified;

    private FrequencyCap[] frequencyCapsField;

    private LineItemType lineItemTypeField;

    private bool lineItemTypeFieldSpecified;

    private UnitType unitTypeField;

    private bool unitTypeFieldSpecified;

    private LineItemSummaryDuration durationField;

    private bool durationFieldSpecified;

    private long unitsBoughtField;

    private bool unitsBoughtFieldSpecified;

    private Money costPerUnitField;

    private Money valueCostPerUnitField;

    private CostType costTypeField;

    private bool costTypeFieldSpecified;

    private LineItemDiscountType discountTypeField;

    private bool discountTypeFieldSpecified;

    private double discountField;

    private bool discountFieldSpecified;

    private Size[] creativeSizesField;

    private bool allowOverbookField;

    private bool allowOverbookFieldSpecified;

    private Stats statsField;

    private DeliveryIndicator deliveryIndicatorField;

    private long[] deliveryDataField;

    private Money budgetField;

    private ComputedStatus statusField;

    private bool statusFieldSpecified;

    private LineItemSummaryReservationStatus reservationStatusField;

    private bool reservationStatusFieldSpecified;

    private bool isArchivedField;

    private bool isArchivedFieldSpecified;

    private string lineItemSummaryTypeField;

    public long orderId {
      get { return this.orderIdField; }
      set {
        this.orderIdField = value;
        this.orderIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool orderIdSpecified {
      get { return this.orderIdFieldSpecified; }
      set { this.orderIdFieldSpecified = value; }
    }

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public string orderName {
      get { return this.orderNameField; }
      set { this.orderNameField = value; }
    }

    public DateTime startDateTime {
      get { return this.startDateTimeField; }
      set { this.startDateTimeField = value; }
    }

    public StartDateTimeType startDateTimeType {
      get { return this.startDateTimeTypeField; }
      set {
        this.startDateTimeTypeField = value;
        this.startDateTimeTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startDateTimeTypeSpecified {
      get { return this.startDateTimeTypeFieldSpecified; }
      set { this.startDateTimeTypeFieldSpecified = value; }
    }

    public DateTime endDateTime {
      get { return this.endDateTimeField; }
      set { this.endDateTimeField = value; }
    }

    public bool unlimitedEndDateTime {
      get { return this.unlimitedEndDateTimeField; }
      set {
        this.unlimitedEndDateTimeField = value;
        this.unlimitedEndDateTimeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool unlimitedEndDateTimeSpecified {
      get { return this.unlimitedEndDateTimeFieldSpecified; }
      set { this.unlimitedEndDateTimeFieldSpecified = value; }
    }

    public CreativeRotationType creativeRotationType {
      get { return this.creativeRotationTypeField; }
      set {
        this.creativeRotationTypeField = value;
        this.creativeRotationTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool creativeRotationTypeSpecified {
      get { return this.creativeRotationTypeFieldSpecified; }
      set { this.creativeRotationTypeFieldSpecified = value; }
    }

    public DeliveryRateType deliveryRateType {
      get { return this.deliveryRateTypeField; }
      set {
        this.deliveryRateTypeField = value;
        this.deliveryRateTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool deliveryRateTypeSpecified {
      get { return this.deliveryRateTypeFieldSpecified; }
      set { this.deliveryRateTypeFieldSpecified = value; }
    }

    public RoadblockingType roadblockingType {
      get { return this.roadblockingTypeField; }
      set {
        this.roadblockingTypeField = value;
        this.roadblockingTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool roadblockingTypeSpecified {
      get { return this.roadblockingTypeFieldSpecified; }
      set { this.roadblockingTypeFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("frequencyCaps")]
    public FrequencyCap[] frequencyCaps {
      get { return this.frequencyCapsField; }
      set { this.frequencyCapsField = value; }
    }

    public LineItemType lineItemType {
      get { return this.lineItemTypeField; }
      set {
        this.lineItemTypeField = value;
        this.lineItemTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool lineItemTypeSpecified {
      get { return this.lineItemTypeFieldSpecified; }
      set { this.lineItemTypeFieldSpecified = value; }
    }

    public UnitType unitType {
      get { return this.unitTypeField; }
      set {
        this.unitTypeField = value;
        this.unitTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool unitTypeSpecified {
      get { return this.unitTypeFieldSpecified; }
      set { this.unitTypeFieldSpecified = value; }
    }

    public LineItemSummaryDuration duration {
      get { return this.durationField; }
      set {
        this.durationField = value;
        this.durationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool durationSpecified {
      get { return this.durationFieldSpecified; }
      set { this.durationFieldSpecified = value; }
    }

    public long unitsBought {
      get { return this.unitsBoughtField; }
      set {
        this.unitsBoughtField = value;
        this.unitsBoughtSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool unitsBoughtSpecified {
      get { return this.unitsBoughtFieldSpecified; }
      set { this.unitsBoughtFieldSpecified = value; }
    }

    public Money costPerUnit {
      get { return this.costPerUnitField; }
      set { this.costPerUnitField = value; }
    }

    public Money valueCostPerUnit {
      get { return this.valueCostPerUnitField; }
      set { this.valueCostPerUnitField = value; }
    }

    public CostType costType {
      get { return this.costTypeField; }
      set {
        this.costTypeField = value;
        this.costTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool costTypeSpecified {
      get { return this.costTypeFieldSpecified; }
      set { this.costTypeFieldSpecified = value; }
    }

    public LineItemDiscountType discountType {
      get { return this.discountTypeField; }
      set {
        this.discountTypeField = value;
        this.discountTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool discountTypeSpecified {
      get { return this.discountTypeFieldSpecified; }
      set { this.discountTypeFieldSpecified = value; }
    }

    public double discount {
      get { return this.discountField; }
      set {
        this.discountField = value;
        this.discountSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool discountSpecified {
      get { return this.discountFieldSpecified; }
      set { this.discountFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("creativeSizes")]
    public Size[] creativeSizes {
      get { return this.creativeSizesField; }
      set { this.creativeSizesField = value; }
    }

    public bool allowOverbook {
      get { return this.allowOverbookField; }
      set {
        this.allowOverbookField = value;
        this.allowOverbookSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowOverbookSpecified {
      get { return this.allowOverbookFieldSpecified; }
      set { this.allowOverbookFieldSpecified = value; }
    }

    public Stats stats {
      get { return this.statsField; }
      set { this.statsField = value; }
    }

    public DeliveryIndicator deliveryIndicator {
      get { return this.deliveryIndicatorField; }
      set { this.deliveryIndicatorField = value; }
    }

    [System.Xml.Serialization.XmlArrayItemAttribute("units", IsNullable = false)]
    public long[] deliveryData {
      get { return this.deliveryDataField; }
      set { this.deliveryDataField = value; }
    }

    public Money budget {
      get { return this.budgetField; }
      set { this.budgetField = value; }
    }

    public ComputedStatus status {
      get { return this.statusField; }
      set {
        this.statusField = value;
        this.statusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool statusSpecified {
      get { return this.statusFieldSpecified; }
      set { this.statusFieldSpecified = value; }
    }

    public LineItemSummaryReservationStatus reservationStatus {
      get { return this.reservationStatusField; }
      set {
        this.reservationStatusField = value;
        this.reservationStatusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reservationStatusSpecified {
      get { return this.reservationStatusFieldSpecified; }
      set { this.reservationStatusFieldSpecified = value; }
    }

    public bool isArchived {
      get { return this.isArchivedField; }
      set {
        this.isArchivedField = value;
        this.isArchivedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isArchivedSpecified {
      get { return this.isArchivedFieldSpecified; }
      set { this.isArchivedFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("LineItemSummary.Type")]
    public string LineItemSummaryType {
      get { return this.lineItemSummaryTypeField; }
      set { this.lineItemSummaryTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum StartDateTimeType {
    USE_START_DATE_TIME,
    IMMEDIATELY,
    ONE_HOUR_FROM_NOW
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CreativeRotationType {
    EVEN,
    OPTIMIZED,
    MANUAL
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum DeliveryRateType {
    EVENLY,
    FRONTLOADED,
    AS_FAST_AS_POSSIBLE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum RoadblockingType {
    ONLY_ONE,
    ONE_OR_MORE,
    AS_MANY_AS_POSSIBLE,
    ALL_ROADBLOCK
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum LineItemType {
    SPONSORSHIP,
    STANDARD,
    NETWORK,
    BULK,
    PRICE_PRIORITY,
    HOUSE,
    LEGACY_DFP
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemSummary.Duration", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum LineItemSummaryDuration {
    NONE,
    LIFETIME,
    DAILY
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum CostType {
    CPC,
    CPD,
    CPM
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum LineItemDiscountType {
    ABSOLUTE_VALUE,
    PERCENTAGE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ComputedStatus {
    DELIVERING,
    READY,
    PAUSED,
    NEEDS_CREATIVES,
    PAUSED_INVENTORY_RELEASED,
    PENDING_APPROVAL,
    COMPLETED,
    DISAPPROVED,
    DRAFT,
    CANCELED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemSummary.ReservationStatus", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum LineItemSummaryReservationStatus {
    RESERVED,
    UNRESERVED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItem : LineItemSummary {
    private Targeting targetingField;

    public Targeting targeting {
      get { return this.targetingField; }
      set { this.targetingField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ForecastError : ApiError {
    private ForecastErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ForecastErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ForecastError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum ForecastErrorReason {
    SERVER_NOT_AVAILABLE,
    INTERNAL_ERROR,
    NO_FORECAST_YET,
    NOT_ENOUGH_INVENTORY,
    SUCCESS,
    ZERO_LENGTH_RESERVATION
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "InventoryServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class InventoryService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public InventoryService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/InventoryService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public AdUnit createAdUnit(AdUnit adUnit) {
      object[] results = this.Invoke("createAdUnit", new object[] {adUnit});
      return ((AdUnit) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public AdUnit[] createAdUnits([System.Xml.Serialization.XmlElementAttribute("adUnits")]
AdUnit[] adUnits) {
      object[] results = this.Invoke("createAdUnits", new object[] {adUnits});
      return ((AdUnit[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public AdUnit getAdUnit(string adUnitId) {
      object[] results = this.Invoke("getAdUnit", new object[] {adUnitId});
      return ((AdUnit) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public AdUnitPage getAdUnitsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getAdUnitsByStatement", new object[] {filterStatement});
      return ((AdUnitPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public UpdateResult performAdUnitAction(AdUnitAction adUnitAction, Statement filterStatement) {
      object[] results = this.Invoke("performAdUnitAction", new object[] {adUnitAction, filterStatement});
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public AdUnit updateAdUnit(AdUnit adUnit) {
      object[] results = this.Invoke("updateAdUnit", new object[] {adUnit});
      return ((AdUnit) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public AdUnit[] updateAdUnits([System.Xml.Serialization.XmlElementAttribute("adUnits")]
AdUnit[] adUnits) {
      object[] results = this.Invoke("updateAdUnits", new object[] {adUnits});
      return ((AdUnit[]) (results[0]));
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateAdUnits))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AssignAdUnitsToPlacement))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveAdUnits))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateAdUnits))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class AdUnitAction {
    private string adUnitActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("AdUnitAction.Type")]
    public string AdUnitActionType {
      get { return this.adUnitActionTypeField; }
      set { this.adUnitActionTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DeactivateAdUnits : AdUnitAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class AssignAdUnitsToPlacement : AdUnitAction {
    private long placementIdField;

    private bool placementIdFieldSpecified;

    public long placementId {
      get { return this.placementIdField; }
      set {
        this.placementIdField = value;
        this.placementIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool placementIdSpecified {
      get { return this.placementIdFieldSpecified; }
      set { this.placementIdFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ArchiveAdUnits : AdUnitAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ActivateAdUnits : AdUnitAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class AdUnitPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private AdUnit[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public AdUnit[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class AdUnit {
    private string idField;

    private string parentIdField;

    private string nameField;

    private string descriptionField;

    private AdUnitTargetWindow targetWindowField;

    private bool targetWindowFieldSpecified;

    private InventoryStatus statusField;

    private bool statusFieldSpecified;

    private string adUnitCodeField;

    private Size[] sizesField;

    private bool explicitlyTargetedField;

    private bool explicitlyTargetedFieldSpecified;

    private AdSenseSettingsInheritedProperty inheritedAdSenseSettingsField;

    public string id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    public string parentId {
      get { return this.parentIdField; }
      set { this.parentIdField = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }

    public AdUnitTargetWindow targetWindow {
      get { return this.targetWindowField; }
      set {
        this.targetWindowField = value;
        this.targetWindowSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool targetWindowSpecified {
      get { return this.targetWindowFieldSpecified; }
      set { this.targetWindowFieldSpecified = value; }
    }

    public InventoryStatus status {
      get { return this.statusField; }
      set {
        this.statusField = value;
        this.statusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool statusSpecified {
      get { return this.statusFieldSpecified; }
      set { this.statusFieldSpecified = value; }
    }

    public string adUnitCode {
      get { return this.adUnitCodeField; }
      set { this.adUnitCodeField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("sizes")]
    public Size[] sizes {
      get { return this.sizesField; }
      set { this.sizesField = value; }
    }

    public bool explicitlyTargeted {
      get { return this.explicitlyTargetedField; }
      set {
        this.explicitlyTargetedField = value;
        this.explicitlyTargetedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool explicitlyTargetedSpecified {
      get { return this.explicitlyTargetedFieldSpecified; }
      set { this.explicitlyTargetedFieldSpecified = value; }
    }

    public AdSenseSettingsInheritedProperty inheritedAdSenseSettings {
      get { return this.inheritedAdSenseSettingsField; }
      set { this.inheritedAdSenseSettingsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdUnit.TargetWindow", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum AdUnitTargetWindow {
    TOP,
    BLANK
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class AdSenseSettingsInheritedProperty {
    private AdSenseSettings valueField;

    public AdSenseSettings value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class AdSenseSettings {
    private bool adSenseEnabledField;

    private bool adSenseEnabledFieldSpecified;

    private string borderColorField;

    private string titleColorField;

    private string backgroundColorField;

    private string textColorField;

    private string urlColorField;

    private AdSenseSettingsAdType adTypeField;

    private bool adTypeFieldSpecified;

    private AdSenseSettingsBorderStyle borderStyleField;

    private bool borderStyleFieldSpecified;

    private AdSenseSettingsFontFamily fontFamilyField;

    private bool fontFamilyFieldSpecified;

    private AdSenseSettingsFontSize fontSizeField;

    private bool fontSizeFieldSpecified;

    private Size_StringMapEntry[] afcFormatsField;

    public bool adSenseEnabled {
      get { return this.adSenseEnabledField; }
      set {
        this.adSenseEnabledField = value;
        this.adSenseEnabledSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool adSenseEnabledSpecified {
      get { return this.adSenseEnabledFieldSpecified; }
      set { this.adSenseEnabledFieldSpecified = value; }
    }

    public string borderColor {
      get { return this.borderColorField; }
      set { this.borderColorField = value; }
    }

    public string titleColor {
      get { return this.titleColorField; }
      set { this.titleColorField = value; }
    }

    public string backgroundColor {
      get { return this.backgroundColorField; }
      set { this.backgroundColorField = value; }
    }

    public string textColor {
      get { return this.textColorField; }
      set { this.textColorField = value; }
    }

    public string urlColor {
      get { return this.urlColorField; }
      set { this.urlColorField = value; }
    }

    public AdSenseSettingsAdType adType {
      get { return this.adTypeField; }
      set {
        this.adTypeField = value;
        this.adTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool adTypeSpecified {
      get { return this.adTypeFieldSpecified; }
      set { this.adTypeFieldSpecified = value; }
    }

    public AdSenseSettingsBorderStyle borderStyle {
      get { return this.borderStyleField; }
      set {
        this.borderStyleField = value;
        this.borderStyleSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool borderStyleSpecified {
      get { return this.borderStyleFieldSpecified; }
      set { this.borderStyleFieldSpecified = value; }
    }

    public AdSenseSettingsFontFamily fontFamily {
      get { return this.fontFamilyField; }
      set {
        this.fontFamilyField = value;
        this.fontFamilySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool fontFamilySpecified {
      get { return this.fontFamilyFieldSpecified; }
      set { this.fontFamilyFieldSpecified = value; }
    }

    public AdSenseSettingsFontSize fontSize {
      get { return this.fontSizeField; }
      set {
        this.fontSizeField = value;
        this.fontSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool fontSizeSpecified {
      get { return this.fontSizeFieldSpecified; }
      set { this.fontSizeFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("afcFormats")]
    public Size_StringMapEntry[] afcFormats {
      get { return this.afcFormatsField; }
      set { this.afcFormatsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdSenseSettings.AdType", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum AdSenseSettingsAdType {
    TEXT,
    IMAGE,
    TEXT_AND_IMAGE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdSenseSettings.BorderStyle", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum AdSenseSettingsBorderStyle {
    DEFAULT,
    NOT_ROUNDED,
    SLIGHTLY_ROUNDED,
    VERY_ROUNDED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdSenseSettings.FontFamily", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum AdSenseSettingsFontFamily {
    DEFAULT,
    ARIAL,
    TAHOMA,
    GEORGIA,
    TIMES,
    VERDANA
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdSenseSettings.FontSize", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum AdSenseSettingsFontSize {
    DEFAULT,
    SMALL,
    MEDIUM,
    LARGE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Size_StringMapEntry {
    private Size keyField;

    private string valueField;

    public Size key {
      get { return this.keyField; }
      set { this.keyField = value; }
    }

    public string value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class FrequencyCapError : ApiError {
    private FrequencyCapErrorReason reasonField;

    private bool reasonFieldSpecified;

    public FrequencyCapErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "FrequencyCapError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum FrequencyCapErrorReason {
    IMPRESSION_LIMIT_EXCEEDED,
    IMPRESSIONS_TOO_LOW,
    RANGE_LIMIT_EXCEEDED,
    RANGE_TOO_LOW,
    DUPLICATE_TIME_RANGE,
    TOO_MANY_FREQUENCY_CAPS
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "LineItemServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemSummary))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class LineItemService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public LineItemService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/LineItemService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItem createLineItem(LineItem lineItem) {
      object[] results = this.Invoke("createLineItem", new object[] {lineItem});
      return ((LineItem) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItem[] createLineItems([System.Xml.Serialization.XmlElementAttribute("lineItems")]
LineItem[] lineItems) {
      object[] results = this.Invoke("createLineItems", new object[] {lineItems});
      return ((LineItem[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItem getLineItem(long lineItemId) {
      object[] results = this.Invoke("getLineItem", new object[] {lineItemId});
      return ((LineItem) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItemPage getLineItemsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getLineItemsByStatement", new object[] {filterStatement});
      return ((LineItemPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public UpdateResult performLineItemAction(LineItemAction lineItemAction, Statement filterStatement) {
      object[] results = this.Invoke("performLineItemAction", new object[] {lineItemAction, filterStatement});
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItem updateLineItem(LineItem lineItem) {
      object[] results = this.Invoke("updateLineItem", new object[] {lineItem});
      return ((LineItem) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItem[] updateLineItems([System.Xml.Serialization.XmlElementAttribute("lineItems")]
LineItem[] lineItems) {
      object[] results = this.Invoke("updateLineItems", new object[] {lineItems});
      return ((LineItem[]) (results[0]));
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UnarchiveLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeAndOverbookLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReserveLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReserveAndOverbookLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReleaseLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PauseLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateLineItems))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class LineItemAction {
    private string lineItemActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("LineItemAction.Type")]
    public string LineItemActionType {
      get { return this.lineItemActionTypeField; }
      set { this.lineItemActionTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class UnarchiveLineItems : LineItemAction {
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeAndOverbookLineItems))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ResumeLineItems : LineItemAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ResumeAndOverbookLineItems : ResumeLineItems {
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReserveAndOverbookLineItems))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ReserveLineItems : LineItemAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ReserveAndOverbookLineItems : ReserveLineItems {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ReleaseLineItems : LineItemAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class PauseLineItems : LineItemAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ArchiveLineItems : LineItemAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ActivateLineItems : LineItemAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItemPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private LineItem[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public LineItem[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class UserDomainTargetingError : ApiError {
    private UserDomainTargetingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public UserDomainTargetingErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "UserDomainTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum UserDomainTargetingErrorReason {
    INVALID_DOMAIN_NAMES
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class GeoTargetingError : ApiError {
    private GeoTargetingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public GeoTargetingErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "GeoTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum GeoTargetingErrorReason {
    TARGETED_LOCATIONS_NOT_EXCLUDABLE,
    EXCLUDED_LOCATIONS_CANNOT_HAVE_CHILDREN_TARGETED,
    POSTAL_CODES_CANNOT_BE_EXCLUDED,
    UNTARGETABLE_LOCATION
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DayPartTargetingError : ApiError {
    private DayPartTargetingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public DayPartTargetingErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "DayPartTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum DayPartTargetingErrorReason {
    INVALID_HOUR,
    INVALID_MINUTE,
    END_TIME_NOT_AFTER_START_TIME,
    TIME_PERIODS_OVERLAP
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "LineItemCreativeAssociationServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class LineItemCreativeAssociationService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public LineItemCreativeAssociationService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/LineItemCreativeAssociation" + "Service";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItemCreativeAssociation createLineItemCreativeAssociation(LineItemCreativeAssociation lineItemCreativeAssociation) {
      object[] results = this.Invoke("createLineItemCreativeAssociation", new object[] {lineItemCreativeAssociation});
      return ((LineItemCreativeAssociation) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItemCreativeAssociation[] createLineItemCreativeAssociations([System.Xml.Serialization.XmlElementAttribute("lineItemCreativeAssociations")]
LineItemCreativeAssociation[] lineItemCreativeAssociations) {
      object[] results = this.Invoke("createLineItemCreativeAssociations", new object[] {lineItemCreativeAssociations});
      return ((LineItemCreativeAssociation[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItemCreativeAssociation getLineItemCreativeAssociation(long lineItemId, long creativeId) {
      object[] results = this.Invoke("getLineItemCreativeAssociation", new object[] {lineItemId, creativeId});
      return ((LineItemCreativeAssociation) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItemCreativeAssociationPage getLineItemCreativeAssociationsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getLineItemCreativeAssociationsByStatement", new object[] {filterStatement});
      return ((LineItemCreativeAssociationPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public UpdateResult performLineItemCreativeAssociationAction(LineItemCreativeAssociationAction lineItemCreativeAssociationAction, Statement filterStatement) {
      object[] results = this.Invoke("performLineItemCreativeAssociationAction", new object[] {lineItemCreativeAssociationAction, filterStatement});
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItemCreativeAssociation updateLineItemCreativeAssociation(LineItemCreativeAssociation lineItemCreativeAssociation) {
      object[] results = this.Invoke("updateLineItemCreativeAssociation", new object[] {lineItemCreativeAssociation});
      return ((LineItemCreativeAssociation) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public LineItemCreativeAssociation[] updateLineItemCreativeAssociations([System.Xml.Serialization.XmlElementAttribute("lineItemCreativeAssociations")]
LineItemCreativeAssociation[] lineItemCreativeAssociations) {
      object[] results = this.Invoke("updateLineItemCreativeAssociations", new object[] {lineItemCreativeAssociations});
      return ((LineItemCreativeAssociation[]) (results[0]));
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateLineItemCreativeAssociations))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateLineItemCreativeAssociations))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class LineItemCreativeAssociationAction {
    private string lineItemCreativeAssociationActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("LineItemCreativeAssociationAction.Type")]
    public string LineItemCreativeAssociationActionType {
      get { return this.lineItemCreativeAssociationActionTypeField; }
      set { this.lineItemCreativeAssociationActionTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DeactivateLineItemCreativeAssociations : LineItemCreativeAssociationAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ActivateLineItemCreativeAssociations : LineItemCreativeAssociationAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItemCreativeAssociationPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private LineItemCreativeAssociation[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public LineItemCreativeAssociation[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItemCreativeAssociation {
    private long lineItemIdField;

    private bool lineItemIdFieldSpecified;

    private long creativeIdField;

    private bool creativeIdFieldSpecified;

    private double manualCreativeRotationWeightField;

    private bool manualCreativeRotationWeightFieldSpecified;

    private DateTime startDateTimeField;

    private StartDateTimeType startDateTimeTypeField;

    private bool startDateTimeTypeFieldSpecified;

    private DateTime endDateTimeField;

    private string destinationUrlField;

    private Size[] sizesField;

    private LineItemCreativeAssociationStatus statusField;

    private bool statusFieldSpecified;

    private LineItemCreativeAssociationStats statsField;

    public long lineItemId {
      get { return this.lineItemIdField; }
      set {
        this.lineItemIdField = value;
        this.lineItemIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool lineItemIdSpecified {
      get { return this.lineItemIdFieldSpecified; }
      set { this.lineItemIdFieldSpecified = value; }
    }

    public long creativeId {
      get { return this.creativeIdField; }
      set {
        this.creativeIdField = value;
        this.creativeIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool creativeIdSpecified {
      get { return this.creativeIdFieldSpecified; }
      set { this.creativeIdFieldSpecified = value; }
    }

    public double manualCreativeRotationWeight {
      get { return this.manualCreativeRotationWeightField; }
      set {
        this.manualCreativeRotationWeightField = value;
        this.manualCreativeRotationWeightSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool manualCreativeRotationWeightSpecified {
      get { return this.manualCreativeRotationWeightFieldSpecified; }
      set { this.manualCreativeRotationWeightFieldSpecified = value; }
    }

    public DateTime startDateTime {
      get { return this.startDateTimeField; }
      set { this.startDateTimeField = value; }
    }

    public StartDateTimeType startDateTimeType {
      get { return this.startDateTimeTypeField; }
      set {
        this.startDateTimeTypeField = value;
        this.startDateTimeTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startDateTimeTypeSpecified {
      get { return this.startDateTimeTypeFieldSpecified; }
      set { this.startDateTimeTypeFieldSpecified = value; }
    }

    public DateTime endDateTime {
      get { return this.endDateTimeField; }
      set { this.endDateTimeField = value; }
    }

    public string destinationUrl {
      get { return this.destinationUrlField; }
      set { this.destinationUrlField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("sizes")]
    public Size[] sizes {
      get { return this.sizesField; }
      set { this.sizesField = value; }
    }

    public LineItemCreativeAssociationStatus status {
      get { return this.statusField; }
      set {
        this.statusField = value;
        this.statusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool statusSpecified {
      get { return this.statusFieldSpecified; }
      set { this.statusFieldSpecified = value; }
    }

    public LineItemCreativeAssociationStats stats {
      get { return this.statsField; }
      set { this.statsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemCreativeAssociation.Status", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum LineItemCreativeAssociationStatus {
    ACTIVE,
    NOT_SERVING,
    INACTIVE,
    DELETED
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItemCreativeAssociationStats {
    private Stats statsField;

    private Money costInOrderCurrencyField;

    public Stats stats {
      get { return this.statsField; }
      set { this.statsField = value; }
    }

    public Money costInOrderCurrency {
      get { return this.costInOrderCurrencyField; }
      set { this.costInOrderCurrencyField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItemCreativeAssociationOperationError : ApiError {
    private LineItemCreativeAssociationOperationErrorReason reasonField;

    private bool reasonFieldSpecified;

    public LineItemCreativeAssociationOperationErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemCreativeAssociationOperationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum LineItemCreativeAssociationOperationErrorReason {
    NOT_ALLOWED,
    NOT_APPLICABLE,
    CANNOT_ACTIVATE_INVALID_CREATIVE
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class LineItemCreativeAssociationError : ApiError {
    private LineItemCreativeAssociationErrorReason reasonField;

    private bool reasonFieldSpecified;

    public LineItemCreativeAssociationErrorReason reason {
      get { return this.reasonField; }
      set {
        this.reasonField = value;
        this.reasonSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reasonSpecified {
      get { return this.reasonFieldSpecified; }
      set { this.reasonFieldSpecified = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemCreativeAssociationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum LineItemCreativeAssociationErrorReason {
    CREATIVE_IN_WRONG_ADVERTISERS_LIBRARY,
    STARTDATE_BEFORE_LINEITEM_STARTDATE,
    STARTDATE_NOT_BEFORE_LINEITEM_ENDDATE,
    ENDDATE_AFTER_LINEITEM_ENDDATE,
    ENDDATE_NOT_AFTER_LINEITEM_STARTDATE,
    ENDDATE_NOT_AFTER_STARTDATE,
    CANNOT_COPY_WITHIN_SAME_LINE_ITEM
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "NetworkServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class NetworkService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public NetworkService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/NetworkService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Network[] getAllNetworks() {
      object[] results = this.Invoke("getAllNetworks", new object[0]);
      return ((Network[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Network getCurrentNetwork() {
      object[] results = this.Invoke("getCurrentNetwork", new object[0]);
      return ((Network) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Network updateNetwork(Network network) {
      object[] results = this.Invoke("updateNetwork", new object[] {network});
      return ((Network) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Network {
    private long idField;

    private bool idFieldSpecified;

    private string displayNameField;

    private string networkCodeField;

    private string propertyCodeField;

    private string timeZoneField;

    private string currencyCodeField;

    private string effectiveRootAdUnitIdField;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string displayName {
      get { return this.displayNameField; }
      set { this.displayNameField = value; }
    }

    public string networkCode {
      get { return this.networkCodeField; }
      set { this.networkCodeField = value; }
    }

    public string propertyCode {
      get { return this.propertyCodeField; }
      set { this.propertyCodeField = value; }
    }

    public string timeZone {
      get { return this.timeZoneField; }
      set { this.timeZoneField = value; }
    }

    public string currencyCode {
      get { return this.currencyCodeField; }
      set { this.currencyCodeField = value; }
    }

    public string effectiveRootAdUnitId {
      get { return this.effectiveRootAdUnitIdField; }
      set { this.effectiveRootAdUnitIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "OrderServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class OrderService : DfpSoapClient {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public OrderService() {
      this.Url = "https://sandbox.google.com/apis/ads/publisher/v201104/OrderService";
    }

    public RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Order createOrder(Order order) {
      object[] results = this.Invoke("createOrder", new object[] {order});
      return ((Order) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Order[] createOrders([System.Xml.Serialization.XmlElementAttribute("orders")]
Order[] orders) {
      object[] results = this.Invoke("createOrders", new object[] {orders});
      return ((Order[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Order getOrder(long orderId) {
      object[] results = this.Invoke("getOrder", new object[] {orderId});
      return ((Order) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public OrderPage getOrdersByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getOrdersByStatement", new object[] {filterStatement});
      return ((OrderPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public UpdateResult performOrderAction(OrderAction orderAction, Statement filterStatement) {
      object[] results = this.Invoke("performOrderAction", new object[] {orderAction, filterStatement});
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Order updateOrder(Order order) {
      object[] results = this.Invoke("updateOrder", new object[] {order});
      return ((Order) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201104", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201104", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public Order[] updateOrders([System.Xml.Serialization.XmlElementAttribute("orders")]
Order[] orders) {
      object[] results = this.Invoke("updateOrders", new object[] {orders});
      return ((Order[]) (results[0]));
    }
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UnarchiveOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitOrdersForApproval))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitOrdersForApprovalAndOverbook))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetractOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeAndOverbookOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PauseOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DisapproveOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApproveOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApproveAndOverbookOrders))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public abstract partial class OrderAction {
    private string orderActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("OrderAction.Type")]
    public string OrderActionType {
      get { return this.orderActionTypeField; }
      set { this.orderActionTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class UnarchiveOrders : OrderAction {
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitOrdersForApprovalAndOverbook))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class SubmitOrdersForApproval : OrderAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class SubmitOrdersForApprovalAndOverbook : SubmitOrdersForApproval {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class RetractOrders : OrderAction {
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeAndOverbookOrders))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ResumeOrders : OrderAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ResumeAndOverbookOrders : ResumeOrders {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class PauseOrders : OrderAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class DisapproveOrders : OrderAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ArchiveOrders : OrderAction {
  }

  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApproveAndOverbookOrders))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ApproveOrders : OrderAction {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class ApproveAndOverbookOrders : ApproveOrders {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class OrderPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Order[] resultsField;

    public int totalResultSetSize {
      get { return this.totalResultSetSizeField; }
      set {
        this.totalResultSetSizeField = value;
        this.totalResultSetSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalResultSetSizeSpecified {
      get { return this.totalResultSetSizeFieldSpecified; }
      set { this.totalResultSetSizeFieldSpecified = value; }
    }

    public int startIndex {
      get { return this.startIndexField; }
      set {
        this.startIndexField = value;
        this.startIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool startIndexSpecified {
      get { return this.startIndexFieldSpecified; }
      set { this.startIndexFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public Order[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public partial class Order {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private DateTime startDateTimeField;

    private DateTime endDateTimeField;

    private bool unlimitedEndDateTimeField;

    private bool unlimitedEndDateTimeFieldSpecified;

    private OrderStatus statusField;

    private bool statusFieldSpecified;

    private bool isArchivedField;

    private bool isArchivedFieldSpecified;

    private string notesField;

    private string externalIdField;

    private string currencyCodeField;

    private long advertiserIdField;

    private bool advertiserIdFieldSpecified;

    private long agencyIdField;

    private bool agencyIdFieldSpecified;

    private long creatorIdField;

    private bool creatorIdFieldSpecified;

    private long traffickerIdField;

    private bool traffickerIdFieldSpecified;

    private long salespersonIdField;

    private bool salespersonIdFieldSpecified;

    private long totalImpressionsDeliveredField;

    private bool totalImpressionsDeliveredFieldSpecified;

    private long totalClicksDeliveredField;

    private bool totalClicksDeliveredFieldSpecified;

    private Money totalBudgetField;

    public long id {
      get { return this.idField; }
      set {
        this.idField = value;
        this.idSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool idSpecified {
      get { return this.idFieldSpecified; }
      set { this.idFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public DateTime startDateTime {
      get { return this.startDateTimeField; }
      set { this.startDateTimeField = value; }
    }

    public DateTime endDateTime {
      get { return this.endDateTimeField; }
      set { this.endDateTimeField = value; }
    }

    public bool unlimitedEndDateTime {
      get { return this.unlimitedEndDateTimeField; }
      set {
        this.unlimitedEndDateTimeField = value;
        this.unlimitedEndDateTimeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool unlimitedEndDateTimeSpecified {
      get { return this.unlimitedEndDateTimeFieldSpecified; }
      set { this.unlimitedEndDateTimeFieldSpecified = value; }
    }

    public OrderStatus status {
      get { return this.statusField; }
      set {
        this.statusField = value;
        this.statusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool statusSpecified {
      get { return this.statusFieldSpecified; }
      set { this.statusFieldSpecified = value; }
    }

    public bool isArchived {
      get { return this.isArchivedField; }
      set {
        this.isArchivedField = value;
        this.isArchivedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isArchivedSpecified {
      get { return this.isArchivedFieldSpecified; }
      set { this.isArchivedFieldSpecified = value; }
    }

    public string notes {
      get { return this.notesField; }
      set { this.notesField = value; }
    }

    public string externalId {
      get { return this.externalIdField; }
      set { this.externalIdField = value; }
    }

    public string currencyCode {
      get { return this.currencyCodeField; }
      set { this.currencyCodeField = value; }
    }

    public long advertiserId {
      get { return this.advertiserIdField; }
      set {
        this.advertiserIdField = value;
        this.advertiserIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool advertiserIdSpecified {
      get { return this.advertiserIdFieldSpecified; }
      set { this.advertiserIdFieldSpecified = value; }
    }

    public long agencyId {
      get { return this.agencyIdField; }
      set {
        this.agencyIdField = value;
        this.agencyIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool agencyIdSpecified {
      get { return this.agencyIdFieldSpecified; }
      set { this.agencyIdFieldSpecified = value; }
    }

    public long creatorId {
      get { return this.creatorIdField; }
      set {
        this.creatorIdField = value;
        this.creatorIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool creatorIdSpecified {
      get { return this.creatorIdFieldSpecified; }
      set { this.creatorIdFieldSpecified = value; }
    }

    public long traffickerId {
      get { return this.traffickerIdField; }
      set {
        this.traffickerIdField = value;
        this.traffickerIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool traffickerIdSpecified {
      get { return this.traffickerIdFieldSpecified; }
      set { this.traffickerIdFieldSpecified = value; }
    }

    public long salespersonId {
      get { return this.salespersonIdField; }
      set {
        this.salespersonIdField = value;
        this.salespersonIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool salespersonIdSpecified {
      get { return this.salespersonIdFieldSpecified; }
      set { this.salespersonIdFieldSpecified = value; }
    }

    public long totalImpressionsDelivered {
      get { return this.totalImpressionsDeliveredField; }
      set {
        this.totalImpressionsDeliveredField = value;
        this.totalImpressionsDeliveredSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalImpressionsDeliveredSpecified {
      get { return this.totalImpressionsDeliveredFieldSpecified; }
      set { this.totalImpressionsDeliveredFieldSpecified = value; }
    }

    public long totalClicksDelivered {
      get { return this.totalClicksDeliveredField; }
      set {
        this.totalClicksDeliveredField = value;
        this.totalClicksDeliveredSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalClicksDeliveredSpecified {
      get { return this.totalClicksDeliveredFieldSpecified; }
      set { this.totalClicksDeliveredFieldSpecified = value; }
    }

    public Money totalBudget {
      get { return this.totalBudgetField; }
      set { this.totalBudgetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201104")]
  public enum OrderStatus {
    DRAFT,
    PENDING_APPROVAL,
    APPROVED,
    DISAPPROVED,
    PAUSED,
    CANCELED
  }
}
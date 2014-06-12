// Copyright 2014, Google Inc. All Rights Reserved.
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

// Author: Chris Seeley (https://github.com/Narwalter)

namespace Google.Api.Ads.Dfp.v201306 {
  using Google.Api.Ads.Dfp.Lib;
  using Google.Api.Ads.Dfp.Headers;
  using System;
  using System.Web.Services;
  using System.Diagnostics;
  using System.Web.Services.Protocols;
  using System.Xml.Serialization;
  using System.ComponentModel;
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "AudienceSegmentServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class AudienceSegmentService : DfpSoapClient, IAudienceSegmentService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public AudienceSegmentService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/AudienceSegmentService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AudienceSegmentPage getAudienceSegmentsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getAudienceSegmentsByStatement", new object[] { filterStatement });
      return ((AudienceSegmentPage) (results[0]));
    }
  }











  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OAuth))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ClientLogin))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class Authentication {
    private string authenticationTypeField;

    [System.Xml.Serialization.XmlElementAttribute("Authentication.Type")]
    public string AuthenticationType {
      get { return this.authenticationTypeField; }
      set { this.authenticationTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AudienceSegment {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string descriptionField;

    private AudienceSegmentStatus statusField;

    private bool statusFieldSpecified;

    private long sizeField;

    private bool sizeFieldSpecified;

    private string audienceSegmentTypeField;

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

    public AudienceSegmentStatus status {
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

    public long size {
      get { return this.sizeField; }
      set {
        this.sizeField = value;
        this.sizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool sizeSpecified {
      get { return this.sizeFieldSpecified; }
      set { this.sizeFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("AudienceSegment.Type")]
    public string AudienceSegmentType {
      get { return this.audienceSegmentTypeField; }
      set { this.audienceSegmentTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AudienceSegment.Status", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AudienceSegmentStatus {
    ACTIVE,
    INACTIVE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AudienceSegmentPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private AudienceSegment[] resultsField;

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
    public AudienceSegment[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(TextValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(NumberValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DateValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DateTimeValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BooleanValue))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class Value {
    private string valueTypeField;

    [System.Xml.Serialization.XmlElementAttribute("Value.Type")]
    public string ValueType {
      get { return this.valueTypeField; }
      set { this.valueTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class TextValue : Value {
    private string valueField;

    public string value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class NumberValue : Value {
    private string valueField;

    public string value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DateValue : Value {
    private Date valueField;

    public Date value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DateTimeValue : Value {
    private DateTime valueField;

    public DateTime value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(StatementError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ServerError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequiredError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(QuotaError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PublisherQueryLanguageSyntaxError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PublisherQueryLanguageContextError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PermissionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(NotNullError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InternalApiError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FeatureError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CommonError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AuthenticationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiVersionError))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UniqueError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(TypeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ParseError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LabelEntityAssociationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(StringLengthError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(NullError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LabelError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CreativeWrapperError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(TeamError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InvalidEmailError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CompanyError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CompanyCreditStatusError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequiredNumberError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PoddingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdRuleSlotError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdRulePriorityError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdRuleFrequencyCapError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdRuleDateError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(NetworkError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReconciliationImportError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReconciliationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RangeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequiredCollectionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InvalidUrlError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ContentPartnerError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityLimitReachedError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomFieldError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserDomainTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(TechnologyTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReservationDetailsError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProposalLineItemError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProposalLineItemActionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProposalError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PrecisionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(GeoTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(GenericTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FrequencyCapError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DayPartTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomFieldValueError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BillingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReportError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivityError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequiredSizeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemOperationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemFlightDateError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemCreativeAssociationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ForecastError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DateTimeRangeTargetingError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CreativeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ClickTrackingLineItemError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AudienceExtensionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(TemplateInstantiatedCreativeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SwiffyConversionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RichMediaStudioCreativeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemCreativeAssociationOperationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InvalidPhoneNumberError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FileError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomCreativeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CreativeSetError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CreativeAssetMacroError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AssetError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdSenseAccountError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductActionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseRateError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RateCardCustomizationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseRateActionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RegExError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdUnitTypeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryUnitSizesError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryUnitPartnerAssociationError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryUnitError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InvalidColorError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdUnitHierarchyError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdUnitCodeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdUnitAfcSizeError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductTemplateError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductTemplateActionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderActionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RateCardActionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PlacementError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(WorkflowActionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProposalActionError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CreativeTemplateError))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ContactError))]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "StatementError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum StatementErrorReason {
    VARIABLE_NOT_BOUND_TO_VALUE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ServerError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ServerErrorReason {
    SERVER_ERROR,
    SERVER_BUSY,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RequiredError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RequiredErrorReason {
    REQUIRED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "QuotaError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum QuotaErrorReason {
    EXCEEDED_QUOTA,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PublisherQueryLanguageSyntaxError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum PublisherQueryLanguageSyntaxErrorReason {
    UNPARSABLE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PublisherQueryLanguageContextError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum PublisherQueryLanguageContextErrorReason {
    UNEXECUTABLE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PermissionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum PermissionErrorReason {
    PERMISSION_DENIED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "NotNullError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum NotNullErrorReason {
    ARG1_NULL,
    ARG2_NULL,
    ARG3_NULL,
    NULL,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InternalApiError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InternalApiErrorReason {
    UNEXPECTED_INTERNAL_API_ERROR,
    UNKNOWN,
    DOWNTIME
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class FeatureError : ApiError {
    private FeatureErrorReason reasonField;

    private bool reasonFieldSpecified;

    public FeatureErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "FeatureError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum FeatureErrorReason {
    MISSING_FEATURE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CommonError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CommonErrorReason {
    NOT_FOUND,
    ALREADY_EXISTS,
    DUPLICATE_OBJECT,
    CANNOT_UPDATE,
    CONCURRENT_MODIFICATION,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AuthenticationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AuthenticationErrorReason {
    AMBIGUOUS_SOAP_REQUEST_HEADER,
    INVALID_EMAIL,
    AUTHENTICATION_FAILED,
    INVALID_OAUTH_SIGNATURE,
    INVALID_SERVICE,
    MISSING_SOAP_REQUEST_HEADER,
    MISSING_AUTHENTICATION_HTTP_HEADER,
    MISSING_AUTHENTICATION,
    NOT_WHITELISTED_FOR_API_ACCESS,
    NO_NETWORKS_TO_ACCESS,
    NETWORK_NOT_FOUND,
    NETWORK_CODE_REQUIRED,
    CONNECTION_ERROR,
    GOOGLE_ACCOUNT_ALREADY_ASSOCIATED_WITH_NETWORK,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ApiVersionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ApiVersionErrorReason {
    UPDATE_TO_NEWER_VERSION,
    UNKNOWN
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApiException))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ApiException : ApplicationException {
    private ApiError[] errorsField;

    [System.Xml.Serialization.XmlElementAttribute("errors")]
    public ApiError[] errors {
      get { return this.errorsField; }
      set { this.errorsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class OAuth : Authentication {
    private string parametersField;

    public string parameters {
      get { return this.parametersField; }
      set { this.parametersField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ClientLogin : Authentication {
    private string tokenField;

    public string token {
      get { return this.tokenField; }
      set { this.tokenField = value; }
    }
  }









  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "SuggestedAdUnitServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class SuggestedAdUnitService : DfpSoapClient, ISuggestedAdUnitService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public SuggestedAdUnitService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/SuggestedAdUnitService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual SuggestedAdUnit getSuggestedAdUnit(string suggestedAdUnitId) {
      object[] results = this.Invoke("getSuggestedAdUnit", new object[] { suggestedAdUnitId });
      return ((SuggestedAdUnit) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual SuggestedAdUnitPage getSuggestedAdUnitsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getSuggestedAdUnitsByStatement", new object[] { filterStatement });
      return ((SuggestedAdUnitPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual SuggestedAdUnitUpdateResult performSuggestedAdUnitAction(SuggestedAdUnitAction suggestedAdUnitAction, Statement filterStatement) {
      object[] results = this.Invoke("performSuggestedAdUnitAction", new object[] { suggestedAdUnitAction, filterStatement });
      return ((SuggestedAdUnitUpdateResult) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SuggestedAdUnitUpdateResult {
    private string[] newAdUnitIdsField;

    private int numChangesField;

    private bool numChangesFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("newAdUnitIds")]
    public string[] newAdUnitIds {
      get { return this.newAdUnitIdsField; }
      set { this.newAdUnitIdsField = value; }
    }

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


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApproveSuggestedAdUnit))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class SuggestedAdUnitAction {
    private string suggestedAdUnitActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("SuggestedAdUnitAction.Type")]
    public string SuggestedAdUnitActionType {
      get { return this.suggestedAdUnitActionTypeField; }
      set { this.suggestedAdUnitActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ApproveSuggestedAdUnit : SuggestedAdUnitAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SuggestedAdUnitPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private SuggestedAdUnit[] resultsField;

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
    public SuggestedAdUnit[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SuggestedAdUnit {
    private string idField;

    private long numRequestsField;

    private bool numRequestsFieldSpecified;

    private string[] pathField;

    private AdUnitParent[] parentPathField;

    private AdUnitTargetWindow targetWindowField;

    private bool targetWindowFieldSpecified;

    private TargetPlatform targetPlatformField;

    private bool targetPlatformFieldSpecified;

    private AdUnitSize[] suggestedAdUnitSizesField;

    public string id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    public long numRequests {
      get { return this.numRequestsField; }
      set {
        this.numRequestsField = value;
        this.numRequestsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool numRequestsSpecified {
      get { return this.numRequestsFieldSpecified; }
      set { this.numRequestsFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("path")]
    public string[] path {
      get { return this.pathField; }
      set { this.pathField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("parentPath")]
    public AdUnitParent[] parentPath {
      get { return this.parentPathField; }
      set { this.parentPathField = value; }
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

    public TargetPlatform targetPlatform {
      get { return this.targetPlatformField; }
      set {
        this.targetPlatformField = value;
        this.targetPlatformSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool targetPlatformSpecified {
      get { return this.targetPlatformFieldSpecified; }
      set { this.targetPlatformFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("suggestedAdUnitSizes")]
    public AdUnitSize[] suggestedAdUnitSizes {
      get { return this.suggestedAdUnitSizesField; }
      set { this.suggestedAdUnitSizesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdUnitParent {
    private string idField;

    private string nameField;

    public string id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdUnit.TargetWindow", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdUnitTargetWindow {
    TOP,
    BLANK
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum TargetPlatform {
    WEB,
    MOBILE,
    ANY
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdUnitSize {
    private Size sizeField;

    private EnvironmentType environmentTypeField;

    private bool environmentTypeFieldSpecified;

    private AdUnitSize[] companionsField;

    private string fullDisplayStringField;

    public Size size {
      get { return this.sizeField; }
      set { this.sizeField = value; }
    }

    public EnvironmentType environmentType {
      get { return this.environmentTypeField; }
      set {
        this.environmentTypeField = value;
        this.environmentTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool environmentTypeSpecified {
      get { return this.environmentTypeFieldSpecified; }
      set { this.environmentTypeFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("companions")]
    public AdUnitSize[] companions {
      get { return this.companionsField; }
      set { this.companionsField = value; }
    }

    public string fullDisplayString {
      get { return this.fullDisplayStringField; }
      set { this.fullDisplayStringField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum EnvironmentType {
    BROWSER,
    VIDEO_PLAYER
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UniqueError : ApiError {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class TypeError : ApiError {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ParseError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ParseErrorReason {
    UNPARSABLE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LabelEntityAssociationError : ApiError {
    private LabelEntityAssociationErrorReason reasonField;

    private bool reasonFieldSpecified;

    public LabelEntityAssociationErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LabelEntityAssociationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LabelEntityAssociationErrorReason {
    INVALID_COMPANY_TYPE,
    DUPLICATE_ASSOCIATION,
    INVALID_ASSOCIATION,
    DUPLICATE_ASSOCIATION_WITH_NEGATION,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "LabelServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class LabelService : DfpSoapClient, ILabelService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public LabelService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/LabelService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Label createLabel(Label label) {
      object[] results = this.Invoke("createLabel", new object[] { label });
      return ((Label) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Label[] createLabels([System.Xml.Serialization.XmlElementAttribute("labels")]
Label[] labels) {
      object[] results = this.Invoke("createLabels", new object[] { labels });
      return ((Label[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Label getLabel(long labelId) {
      object[] results = this.Invoke("getLabel", new object[] { labelId });
      return ((Label) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LabelPage getLabelsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getLabelsByStatement", new object[] { filterStatement });
      return ((LabelPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performLabelAction(LabelAction labelAction, Statement filterStatement) {
      object[] results = this.Invoke("performLabelAction", new object[] { labelAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Label updateLabel(Label label) {
      object[] results = this.Invoke("updateLabel", new object[] { label });
      return ((Label) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Label[] updateLabels([System.Xml.Serialization.XmlElementAttribute("labels")]
Label[] labels) {
      object[] results = this.Invoke("updateLabels", new object[] { labels });
      return ((Label[]) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateLabels))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateLabels))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class LabelAction {
    private string labelActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("LabelAction.Type")]
    public string LabelActionType {
      get { return this.labelActionTypeField; }
      set { this.labelActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateLabels : LabelAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateLabels : LabelAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LabelPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Label[] resultsField;

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
    public Label[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Label {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string descriptionField;

    private bool isActiveField;

    private bool isActiveFieldSpecified;

    private LabelType[] typesField;

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

    [System.Xml.Serialization.XmlElementAttribute("types")]
    public LabelType[] types {
      get { return this.typesField; }
      set { this.typesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LabelType {
    COMPETITIVE_EXCLUSION,
    AD_UNIT_FREQUENCY_CAP,
    AD_EXCLUSION,
    CREATIVE_WRAPPER,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "StringLengthError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum StringLengthErrorReason {
    TOO_LONG,
    TOO_SHORT,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "NullError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum NullErrorReason {
    NULL_CONTENT
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LabelError : ApiError {
    private LabelErrorReason reasonField;

    private bool reasonFieldSpecified;

    public LabelErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LabelError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LabelErrorReason {
    INVALID_PREFIX,
    NAME_INVALID_CHARS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeWrapperError : ApiError {
    private CreativeWrapperErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CreativeWrapperErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CreativeWrapperError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeWrapperErrorReason {
    LABEL_ALREADY_ASSOCIATED_WITH_CREATIVE_WRAPPER,
    INVALID_LABEL_TYPE,
    UNRECOGNIZED_MACRO,
    NEITHER_HEADER_NOR_FOOTER_SPECIFIED,
    CANNOT_USE_CREATIVE_WRAPPER_TYPE,
    CANNOT_UPDATE_LABEL_ID,
    CANNOT_APPLY_TO_AD_UNIT_WITH_VIDEO_SIZES,
    CANNOT_APPLY_TO_MOBILE_AD_UNIT,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CompanyServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CompanyService : DfpSoapClient, ICompanyService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CompanyService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/CompanyService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Company[] createCompanies([System.Xml.Serialization.XmlElementAttribute("companies")]
Company[] companies) {
      object[] results = this.Invoke("createCompanies", new object[] { companies });
      return ((Company[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Company createCompany(Company company) {
      object[] results = this.Invoke("createCompany", new object[] { company });
      return ((Company) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CompanyPage getCompaniesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCompaniesByStatement", new object[] { filterStatement });
      return ((CompanyPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Company getCompany(long companyId) {
      object[] results = this.Invoke("getCompany", new object[] { companyId });
      return ((Company) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Company[] updateCompanies([System.Xml.Serialization.XmlElementAttribute("companies")]
Company[] companies) {
      object[] results = this.Invoke("updateCompanies", new object[] { companies });
      return ((Company[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Company updateCompany(Company company) {
      object[] results = this.Invoke("updateCompany", new object[] { company });
      return ((Company) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Company {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private CompanyType typeField;

    private bool typeFieldSpecified;

    private string addressField;

    private string emailField;

    private string faxPhoneField;

    private string primaryPhoneField;

    private string externalIdField;

    private string commentField;

    private CompanyCreditStatus creditStatusField;

    private bool creditStatusFieldSpecified;

    private AppliedLabel[] appliedLabelsField;

    private bool enableSameAdvertiserCompetitiveExclusionField;

    private bool enableSameAdvertiserCompetitiveExclusionFieldSpecified;

    private long primaryContactIdField;

    private bool primaryContactIdFieldSpecified;

    private long[] appliedTeamIdsField;

    private int thirdPartyCompanyIdField;

    private bool thirdPartyCompanyIdFieldSpecified;

    private DateTime lastModifiedDateTimeField;

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

    public string address {
      get { return this.addressField; }
      set { this.addressField = value; }
    }

    public string email {
      get { return this.emailField; }
      set { this.emailField = value; }
    }

    public string faxPhone {
      get { return this.faxPhoneField; }
      set { this.faxPhoneField = value; }
    }

    public string primaryPhone {
      get { return this.primaryPhoneField; }
      set { this.primaryPhoneField = value; }
    }

    public string externalId {
      get { return this.externalIdField; }
      set { this.externalIdField = value; }
    }

    public string comment {
      get { return this.commentField; }
      set { this.commentField = value; }
    }

    public CompanyCreditStatus creditStatus {
      get { return this.creditStatusField; }
      set {
        this.creditStatusField = value;
        this.creditStatusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool creditStatusSpecified {
      get { return this.creditStatusFieldSpecified; }
      set { this.creditStatusFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedLabels")]
    public AppliedLabel[] appliedLabels {
      get { return this.appliedLabelsField; }
      set { this.appliedLabelsField = value; }
    }

    public bool enableSameAdvertiserCompetitiveExclusion {
      get { return this.enableSameAdvertiserCompetitiveExclusionField; }
      set {
        this.enableSameAdvertiserCompetitiveExclusionField = value;
        this.enableSameAdvertiserCompetitiveExclusionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool enableSameAdvertiserCompetitiveExclusionSpecified {
      get { return this.enableSameAdvertiserCompetitiveExclusionFieldSpecified; }
      set { this.enableSameAdvertiserCompetitiveExclusionFieldSpecified = value; }
    }

    public long primaryContactId {
      get { return this.primaryContactIdField; }
      set {
        this.primaryContactIdField = value;
        this.primaryContactIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool primaryContactIdSpecified {
      get { return this.primaryContactIdFieldSpecified; }
      set { this.primaryContactIdFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedTeamIds")]
    public long[] appliedTeamIds {
      get { return this.appliedTeamIdsField; }
      set { this.appliedTeamIdsField = value; }
    }

    public int thirdPartyCompanyId {
      get { return this.thirdPartyCompanyIdField; }
      set {
        this.thirdPartyCompanyIdField = value;
        this.thirdPartyCompanyIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool thirdPartyCompanyIdSpecified {
      get { return this.thirdPartyCompanyIdFieldSpecified; }
      set { this.thirdPartyCompanyIdFieldSpecified = value; }
    }

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Company.Type", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CompanyType {
    HOUSE_ADVERTISER,
    HOUSE_AGENCY,
    ADVERTISER,
    AGENCY,
    AD_NETWORK,
    AFFILIATE_DISTRIBUTION_PARTNER,
    CONTENT_PARTNER,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Company.CreditStatus", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CompanyCreditStatus {
    ACTIVE,
    ON_HOLD,
    CREDIT_STOP,
    INACTIVE,
    BLOCKED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AppliedLabel {
    private long labelIdField;

    private bool labelIdFieldSpecified;

    private bool isNegatedField;

    private bool isNegatedFieldSpecified;

    public long labelId {
      get { return this.labelIdField; }
      set {
        this.labelIdField = value;
        this.labelIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool labelIdSpecified {
      get { return this.labelIdFieldSpecified; }
      set { this.labelIdFieldSpecified = value; }
    }

    public bool isNegated {
      get { return this.isNegatedField; }
      set {
        this.isNegatedField = value;
        this.isNegatedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isNegatedSpecified {
      get { return this.isNegatedFieldSpecified; }
      set { this.isNegatedFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class TeamError : ApiError {
    private TeamErrorReason reasonField;

    private bool reasonFieldSpecified;

    public TeamErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "TeamError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum TeamErrorReason {
    ENTITY_NOT_ON_USERS_TEAMS,
    MISSING_USERS_TEAM,
    ALL_TEAM_ASSOCIATION_NOT_ALLOWED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InvalidEmailError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InvalidEmailErrorReason {
    INVALID_FORMAT,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CompanyError : ApiError {
    private CompanyErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CompanyErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CompanyError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CompanyErrorReason {
    CANNOT_SET_THIRD_PARTY_COMPANY_DUE_TO_TYPE,
    CANNOT_UPDATE_COMPANY_TYPE,
    INVALID_COMPANY_TYPE,
    PRIMARY_CONTACT_DOES_NOT_BELONG_TO_THIS_COMPANY,
    THIRD_PARTY_STATS_PROVIDER_IS_WRONG_ROLE_TYPE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CompanyCreditStatusError : ApiError {
    private CompanyCreditStatusErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CompanyCreditStatusErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CompanyCreditStatusError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CompanyCreditStatusErrorReason {
    COMPANY_CREDIT_STATUS_CHANGE_NOT_ALLOWED,
    CANNOT_USE_CREDIT_STATUS_SETTING,
    CANNOT_USE_ADVANCED_CREDIT_STATUS_SETTING,
    UNACCEPTABLE_COMPANY_CREDIT_STATUS_FOR_ORDER,
    UNACCEPTABLE_COMPANY_CREDIT_STATUS_FOR_LINE_ITEM,
    CANNOT_BLOCK_COMPANY_TOO_MANY_APPROVED_ORDERS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "AdRuleServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class AdRuleService : DfpSoapClient, IAdRuleService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public AdRuleService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/AdRuleService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdRule createAdRule(AdRule adRule) {
      object[] results = this.Invoke("createAdRule", new object[] { adRule });
      return ((AdRule) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdRule[] createAdRules([System.Xml.Serialization.XmlElementAttribute("adRules")]
AdRule[] adRules) {
      object[] results = this.Invoke("createAdRules", new object[] { adRules });
      return ((AdRule[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdRule getAdRule(int adRuleId) {
      object[] results = this.Invoke("getAdRule", new object[] { adRuleId });
      return ((AdRule) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdRulePage getAdRulesByStatement(Statement statement) {
      object[] results = this.Invoke("getAdRulesByStatement", new object[] { statement });
      return ((AdRulePage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performAdRuleAction(AdRuleAction adRuleAction, Statement filterStatement) {
      object[] results = this.Invoke("performAdRuleAction", new object[] { adRuleAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdRule updateAdRule(AdRule adRule) {
      object[] results = this.Invoke("updateAdRule", new object[] { adRule });
      return ((AdRule) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdRule[] updateAdRules([System.Xml.Serialization.XmlElementAttribute("adRules")]
AdRule[] adRules) {
      object[] results = this.Invoke("updateAdRules", new object[] { adRules });
      return ((AdRule[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteAdRules))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateAdRules))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateAdRules))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class AdRuleAction {
    private string adRuleActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("AdRuleAction.Type")]
    public string AdRuleActionType {
      get { return this.adRuleActionTypeField; }
      set { this.adRuleActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeleteAdRules : AdRuleAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateAdRules : AdRuleAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateAdRules : AdRuleAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdRulePage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private AdRule[] resultsField;

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
    public AdRule[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdRule {
    private int idField;

    private bool idFieldSpecified;

    private string nameField;

    private int priorityField;

    private bool priorityFieldSpecified;

    private Targeting targetingField;

    private DateTime startDateTimeField;

    private StartDateTimeType startDateTimeTypeField;

    private bool startDateTimeTypeFieldSpecified;

    private DateTime endDateTimeField;

    private bool unlimitedEndDateTimeField;

    private bool unlimitedEndDateTimeFieldSpecified;

    private AdRuleStatus statusField;

    private bool statusFieldSpecified;

    private FrequencyCapBehavior frequencyCapBehaviorField;

    private bool frequencyCapBehaviorFieldSpecified;

    private int maxImpressionsPerLineItemPerStreamField;

    private bool maxImpressionsPerLineItemPerStreamFieldSpecified;

    private int maxImpressionsPerLineItemPerPodField;

    private bool maxImpressionsPerLineItemPerPodFieldSpecified;

    private BaseAdRuleSlot prerollField;

    private BaseAdRuleSlot midrollField;

    private BaseAdRuleSlot postrollField;

    public int id {
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

    public int priority {
      get { return this.priorityField; }
      set {
        this.priorityField = value;
        this.prioritySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool prioritySpecified {
      get { return this.priorityFieldSpecified; }
      set { this.priorityFieldSpecified = value; }
    }

    public Targeting targeting {
      get { return this.targetingField; }
      set { this.targetingField = value; }
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

    public AdRuleStatus status {
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

    public FrequencyCapBehavior frequencyCapBehavior {
      get { return this.frequencyCapBehaviorField; }
      set {
        this.frequencyCapBehaviorField = value;
        this.frequencyCapBehaviorSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool frequencyCapBehaviorSpecified {
      get { return this.frequencyCapBehaviorFieldSpecified; }
      set { this.frequencyCapBehaviorFieldSpecified = value; }
    }

    public int maxImpressionsPerLineItemPerStream {
      get { return this.maxImpressionsPerLineItemPerStreamField; }
      set {
        this.maxImpressionsPerLineItemPerStreamField = value;
        this.maxImpressionsPerLineItemPerStreamSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool maxImpressionsPerLineItemPerStreamSpecified {
      get { return this.maxImpressionsPerLineItemPerStreamFieldSpecified; }
      set { this.maxImpressionsPerLineItemPerStreamFieldSpecified = value; }
    }

    public int maxImpressionsPerLineItemPerPod {
      get { return this.maxImpressionsPerLineItemPerPodField; }
      set {
        this.maxImpressionsPerLineItemPerPodField = value;
        this.maxImpressionsPerLineItemPerPodSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool maxImpressionsPerLineItemPerPodSpecified {
      get { return this.maxImpressionsPerLineItemPerPodFieldSpecified; }
      set { this.maxImpressionsPerLineItemPerPodFieldSpecified = value; }
    }

    public BaseAdRuleSlot preroll {
      get { return this.prerollField; }
      set { this.prerollField = value; }
    }

    public BaseAdRuleSlot midroll {
      get { return this.midrollField; }
      set { this.midrollField = value; }
    }

    public BaseAdRuleSlot postroll {
      get { return this.postrollField; }
      set { this.postrollField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Targeting {
    private GeoTargeting geoTargetingField;

    private InventoryTargeting inventoryTargetingField;

    private DayPartTargeting dayPartTargetingField;

    private TechnologyTargeting technologyTargetingField;

    private CustomCriteriaSet customTargetingField;

    private UserDomainTargeting userDomainTargetingField;

    private ContentTargeting contentTargetingField;

    private VideoPositionTargeting videoPositionTargetingField;

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

    public TechnologyTargeting technologyTargeting {
      get { return this.technologyTargetingField; }
      set { this.technologyTargetingField = value; }
    }

    public CustomCriteriaSet customTargeting {
      get { return this.customTargetingField; }
      set { this.customTargetingField = value; }
    }

    public UserDomainTargeting userDomainTargeting {
      get { return this.userDomainTargetingField; }
      set { this.userDomainTargetingField = value; }
    }

    public ContentTargeting contentTargeting {
      get { return this.contentTargetingField; }
      set { this.contentTargetingField = value; }
    }

    public VideoPositionTargeting videoPositionTargeting {
      get { return this.videoPositionTargetingField; }
      set { this.videoPositionTargetingField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RegionLocation))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PostalCodeLocation))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(MetroLocation))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CountryLocation))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CityLocation))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RegionLocation : Location {
    private string regionCodeField;

    public string regionCode {
      get { return this.regionCodeField; }
      set { this.regionCodeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class PostalCodeLocation : Location {
    private string postalCodeField;

    private string countryCodeField;

    public string postalCode {
      get { return this.postalCodeField; }
      set { this.postalCodeField = value; }
    }

    public string countryCode {
      get { return this.countryCodeField; }
      set { this.countryCodeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CountryLocation : Location {
    private string countryCodeField;

    public string countryCode {
      get { return this.countryCodeField; }
      set { this.countryCodeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class InventoryTargeting {
    private AdUnitTargeting[] targetedAdUnitsField;

    private AdUnitTargeting[] excludedAdUnitsField;

    private long[] targetedPlacementIdsField;

    [System.Xml.Serialization.XmlElementAttribute("targetedAdUnits")]
    public AdUnitTargeting[] targetedAdUnits {
      get { return this.targetedAdUnitsField; }
      set { this.targetedAdUnitsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedAdUnits")]
    public AdUnitTargeting[] excludedAdUnits {
      get { return this.excludedAdUnitsField; }
      set { this.excludedAdUnitsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("targetedPlacementIds")]
    public long[] targetedPlacementIds {
      get { return this.targetedPlacementIdsField; }
      set { this.targetedPlacementIdsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdUnitTargeting {
    private string adUnitIdField;

    private bool includeDescendantsField;

    private bool includeDescendantsFieldSpecified;

    public string adUnitId {
      get { return this.adUnitIdField; }
      set { this.adUnitIdField = value; }
    }

    public bool includeDescendants {
      get { return this.includeDescendantsField; }
      set {
        this.includeDescendantsField = value;
        this.includeDescendantsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool includeDescendantsSpecified {
      get { return this.includeDescendantsFieldSpecified; }
      set { this.includeDescendantsFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum DayOfWeek {
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY,
    SUNDAY
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum MinuteOfHour {
    ZERO,
    FIFTEEN,
    THIRTY,
    FORTY_FIVE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum DeliveryTimeZone {
    PUBLISHER,
    BROWSER
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class TechnologyTargeting {
    private BandwidthGroupTargeting bandwidthGroupTargetingField;

    private BrowserTargeting browserTargetingField;

    private BrowserLanguageTargeting browserLanguageTargetingField;

    private DeviceCapabilityTargeting deviceCapabilityTargetingField;

    private DeviceCategoryTargeting deviceCategoryTargetingField;

    private DeviceManufacturerTargeting deviceManufacturerTargetingField;

    private MobileCarrierTargeting mobileCarrierTargetingField;

    private MobileDeviceTargeting mobileDeviceTargetingField;

    private MobileDeviceSubmodelTargeting mobileDeviceSubmodelTargetingField;

    private OperatingSystemTargeting operatingSystemTargetingField;

    private OperatingSystemVersionTargeting operatingSystemVersionTargetingField;

    public BandwidthGroupTargeting bandwidthGroupTargeting {
      get { return this.bandwidthGroupTargetingField; }
      set { this.bandwidthGroupTargetingField = value; }
    }

    public BrowserTargeting browserTargeting {
      get { return this.browserTargetingField; }
      set { this.browserTargetingField = value; }
    }

    public BrowserLanguageTargeting browserLanguageTargeting {
      get { return this.browserLanguageTargetingField; }
      set { this.browserLanguageTargetingField = value; }
    }

    public DeviceCapabilityTargeting deviceCapabilityTargeting {
      get { return this.deviceCapabilityTargetingField; }
      set { this.deviceCapabilityTargetingField = value; }
    }

    public DeviceCategoryTargeting deviceCategoryTargeting {
      get { return this.deviceCategoryTargetingField; }
      set { this.deviceCategoryTargetingField = value; }
    }

    public DeviceManufacturerTargeting deviceManufacturerTargeting {
      get { return this.deviceManufacturerTargetingField; }
      set { this.deviceManufacturerTargetingField = value; }
    }

    public MobileCarrierTargeting mobileCarrierTargeting {
      get { return this.mobileCarrierTargetingField; }
      set { this.mobileCarrierTargetingField = value; }
    }

    public MobileDeviceTargeting mobileDeviceTargeting {
      get { return this.mobileDeviceTargetingField; }
      set { this.mobileDeviceTargetingField = value; }
    }

    public MobileDeviceSubmodelTargeting mobileDeviceSubmodelTargeting {
      get { return this.mobileDeviceSubmodelTargetingField; }
      set { this.mobileDeviceSubmodelTargetingField = value; }
    }

    public OperatingSystemTargeting operatingSystemTargeting {
      get { return this.operatingSystemTargetingField; }
      set { this.operatingSystemTargetingField = value; }
    }

    public OperatingSystemVersionTargeting operatingSystemVersionTargeting {
      get { return this.operatingSystemVersionTargetingField; }
      set { this.operatingSystemVersionTargetingField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BandwidthGroupTargeting {
    private bool isTargetedField;

    private bool isTargetedFieldSpecified;

    private Technology[] bandwidthGroupsField;

    public bool isTargeted {
      get { return this.isTargetedField; }
      set {
        this.isTargetedField = value;
        this.isTargetedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isTargetedSpecified {
      get { return this.isTargetedFieldSpecified; }
      set { this.isTargetedFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("bandwidthGroups")]
    public Technology[] bandwidthGroups {
      get { return this.bandwidthGroupsField; }
      set { this.bandwidthGroupsField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OperatingSystemVersion))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OperatingSystem))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(MobileDeviceSubmodel))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(MobileDevice))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(MobileCarrier))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeviceManufacturer))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeviceCategory))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeviceCapability))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BrowserLanguage))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(Browser))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BandwidthGroup))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Technology {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string technologyTypeField;

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

    [System.Xml.Serialization.XmlElementAttribute("Technology.Type")]
    public string TechnologyType {
      get { return this.technologyTypeField; }
      set { this.technologyTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class OperatingSystemVersion : Technology {
    private int majorVersionField;

    private bool majorVersionFieldSpecified;

    private int minorVersionField;

    private bool minorVersionFieldSpecified;

    private int microVersionField;

    private bool microVersionFieldSpecified;

    public int majorVersion {
      get { return this.majorVersionField; }
      set {
        this.majorVersionField = value;
        this.majorVersionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool majorVersionSpecified {
      get { return this.majorVersionFieldSpecified; }
      set { this.majorVersionFieldSpecified = value; }
    }

    public int minorVersion {
      get { return this.minorVersionField; }
      set {
        this.minorVersionField = value;
        this.minorVersionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool minorVersionSpecified {
      get { return this.minorVersionFieldSpecified; }
      set { this.minorVersionFieldSpecified = value; }
    }

    public int microVersion {
      get { return this.microVersionField; }
      set {
        this.microVersionField = value;
        this.microVersionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool microVersionSpecified {
      get { return this.microVersionFieldSpecified; }
      set { this.microVersionFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class OperatingSystem : Technology {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class MobileDeviceSubmodel : Technology {
    private long mobileDeviceCriterionIdField;

    private bool mobileDeviceCriterionIdFieldSpecified;

    private long deviceManufacturerCriterionIdField;

    private bool deviceManufacturerCriterionIdFieldSpecified;

    public long mobileDeviceCriterionId {
      get { return this.mobileDeviceCriterionIdField; }
      set {
        this.mobileDeviceCriterionIdField = value;
        this.mobileDeviceCriterionIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool mobileDeviceCriterionIdSpecified {
      get { return this.mobileDeviceCriterionIdFieldSpecified; }
      set { this.mobileDeviceCriterionIdFieldSpecified = value; }
    }

    public long deviceManufacturerCriterionId {
      get { return this.deviceManufacturerCriterionIdField; }
      set {
        this.deviceManufacturerCriterionIdField = value;
        this.deviceManufacturerCriterionIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool deviceManufacturerCriterionIdSpecified {
      get { return this.deviceManufacturerCriterionIdFieldSpecified; }
      set { this.deviceManufacturerCriterionIdFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class MobileDevice : Technology {
    private long manufacturerCriterionIdField;

    private bool manufacturerCriterionIdFieldSpecified;

    public long manufacturerCriterionId {
      get { return this.manufacturerCriterionIdField; }
      set {
        this.manufacturerCriterionIdField = value;
        this.manufacturerCriterionIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool manufacturerCriterionIdSpecified {
      get { return this.manufacturerCriterionIdFieldSpecified; }
      set { this.manufacturerCriterionIdFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class MobileCarrier : Technology {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeviceManufacturer : Technology {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeviceCategory : Technology {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeviceCapability : Technology {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BrowserLanguage : Technology {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Browser : Technology {
    private string majorVersionField;

    private string minorVersionField;

    public string majorVersion {
      get { return this.majorVersionField; }
      set { this.majorVersionField = value; }
    }

    public string minorVersion {
      get { return this.minorVersionField; }
      set { this.minorVersionField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BandwidthGroup : Technology {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BrowserTargeting {
    private bool isTargetedField;

    private bool isTargetedFieldSpecified;

    private Technology[] browsersField;

    public bool isTargeted {
      get { return this.isTargetedField; }
      set {
        this.isTargetedField = value;
        this.isTargetedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isTargetedSpecified {
      get { return this.isTargetedFieldSpecified; }
      set { this.isTargetedFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("browsers")]
    public Technology[] browsers {
      get { return this.browsersField; }
      set { this.browsersField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BrowserLanguageTargeting {
    private bool isTargetedField;

    private bool isTargetedFieldSpecified;

    private Technology[] browserLanguagesField;

    public bool isTargeted {
      get { return this.isTargetedField; }
      set {
        this.isTargetedField = value;
        this.isTargetedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isTargetedSpecified {
      get { return this.isTargetedFieldSpecified; }
      set { this.isTargetedFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("browserLanguages")]
    public Technology[] browserLanguages {
      get { return this.browserLanguagesField; }
      set { this.browserLanguagesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeviceCapabilityTargeting {
    private Technology[] targetedDeviceCapabilitiesField;

    private Technology[] excludedDeviceCapabilitiesField;

    [System.Xml.Serialization.XmlElementAttribute("targetedDeviceCapabilities")]
    public Technology[] targetedDeviceCapabilities {
      get { return this.targetedDeviceCapabilitiesField; }
      set { this.targetedDeviceCapabilitiesField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedDeviceCapabilities")]
    public Technology[] excludedDeviceCapabilities {
      get { return this.excludedDeviceCapabilitiesField; }
      set { this.excludedDeviceCapabilitiesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeviceCategoryTargeting {
    private Technology[] targetedDeviceCategoriesField;

    private Technology[] excludedDeviceCategoriesField;

    [System.Xml.Serialization.XmlElementAttribute("targetedDeviceCategories")]
    public Technology[] targetedDeviceCategories {
      get { return this.targetedDeviceCategoriesField; }
      set { this.targetedDeviceCategoriesField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedDeviceCategories")]
    public Technology[] excludedDeviceCategories {
      get { return this.excludedDeviceCategoriesField; }
      set { this.excludedDeviceCategoriesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeviceManufacturerTargeting {
    private bool isTargetedField;

    private bool isTargetedFieldSpecified;

    private Technology[] deviceManufacturersField;

    public bool isTargeted {
      get { return this.isTargetedField; }
      set {
        this.isTargetedField = value;
        this.isTargetedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isTargetedSpecified {
      get { return this.isTargetedFieldSpecified; }
      set { this.isTargetedFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("deviceManufacturers")]
    public Technology[] deviceManufacturers {
      get { return this.deviceManufacturersField; }
      set { this.deviceManufacturersField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class MobileCarrierTargeting {
    private bool isTargetedField;

    private bool isTargetedFieldSpecified;

    private Technology[] mobileCarriersField;

    public bool isTargeted {
      get { return this.isTargetedField; }
      set {
        this.isTargetedField = value;
        this.isTargetedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isTargetedSpecified {
      get { return this.isTargetedFieldSpecified; }
      set { this.isTargetedFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("mobileCarriers")]
    public Technology[] mobileCarriers {
      get { return this.mobileCarriersField; }
      set { this.mobileCarriersField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class MobileDeviceTargeting {
    private Technology[] targetedMobileDevicesField;

    private Technology[] excludedMobileDevicesField;

    [System.Xml.Serialization.XmlElementAttribute("targetedMobileDevices")]
    public Technology[] targetedMobileDevices {
      get { return this.targetedMobileDevicesField; }
      set { this.targetedMobileDevicesField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedMobileDevices")]
    public Technology[] excludedMobileDevices {
      get { return this.excludedMobileDevicesField; }
      set { this.excludedMobileDevicesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class MobileDeviceSubmodelTargeting {
    private Technology[] targetedMobileDeviceSubmodelsField;

    private Technology[] excludedMobileDeviceSubmodelsField;

    [System.Xml.Serialization.XmlElementAttribute("targetedMobileDeviceSubmodels")]
    public Technology[] targetedMobileDeviceSubmodels {
      get { return this.targetedMobileDeviceSubmodelsField; }
      set { this.targetedMobileDeviceSubmodelsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedMobileDeviceSubmodels")]
    public Technology[] excludedMobileDeviceSubmodels {
      get { return this.excludedMobileDeviceSubmodelsField; }
      set { this.excludedMobileDeviceSubmodelsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class OperatingSystemTargeting {
    private bool isTargetedField;

    private bool isTargetedFieldSpecified;

    private Technology[] operatingSystemsField;

    public bool isTargeted {
      get { return this.isTargetedField; }
      set {
        this.isTargetedField = value;
        this.isTargetedSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isTargetedSpecified {
      get { return this.isTargetedFieldSpecified; }
      set { this.isTargetedFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("operatingSystems")]
    public Technology[] operatingSystems {
      get { return this.operatingSystemsField; }
      set { this.operatingSystemsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class OperatingSystemVersionTargeting {
    private Technology[] targetedOperatingSystemVersionsField;

    private Technology[] excludedOperatingSystemVersionsField;

    [System.Xml.Serialization.XmlElementAttribute("targetedOperatingSystemVersions")]
    public Technology[] targetedOperatingSystemVersions {
      get { return this.targetedOperatingSystemVersionsField; }
      set { this.targetedOperatingSystemVersionsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedOperatingSystemVersions")]
    public Technology[] excludedOperatingSystemVersions {
      get { return this.excludedOperatingSystemVersionsField; }
      set { this.excludedOperatingSystemVersionsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomCriteriaSet.LogicalOperator", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomCriteriaSetLogicalOperator {
    AND,
    OR
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomCriteriaLeaf))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AudienceSegmentCriteria))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomCriteria))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomCriteriaSet))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class CustomCriteriaNode {
    private string customCriteriaNodeTypeField;

    [System.Xml.Serialization.XmlElementAttribute("CustomCriteriaNode.Type")]
    public string CustomCriteriaNodeType {
      get { return this.customCriteriaNodeTypeField; }
      set { this.customCriteriaNodeTypeField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AudienceSegmentCriteria))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomCriteria))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class CustomCriteriaLeaf : CustomCriteriaNode {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AudienceSegmentCriteria : CustomCriteriaLeaf {
    private AudienceSegmentCriteriaComparisonOperator operatorField;

    private bool operatorFieldSpecified;

    private long[] audienceSegmentIdsField;

    public AudienceSegmentCriteriaComparisonOperator @operator {
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

    [System.Xml.Serialization.XmlElementAttribute("audienceSegmentIds")]
    public long[] audienceSegmentIds {
      get { return this.audienceSegmentIdsField; }
      set { this.audienceSegmentIdsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AudienceSegmentCriteria.ComparisonOperator", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AudienceSegmentCriteriaComparisonOperator {
    IS,
    IS_NOT
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomCriteria : CustomCriteriaLeaf {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomCriteria.ComparisonOperator", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomCriteriaComparisonOperator {
    IS,
    IS_NOT
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ContentTargeting {
    private long[] targetedContentIdsField;

    private long[] excludedContentIdsField;

    private long[] targetedVideoCategoryIdsField;

    private long[] excludedVideoCategoryIdsField;

    [System.Xml.Serialization.XmlElementAttribute("targetedContentIds")]
    public long[] targetedContentIds {
      get { return this.targetedContentIdsField; }
      set { this.targetedContentIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedContentIds")]
    public long[] excludedContentIds {
      get { return this.excludedContentIdsField; }
      set { this.excludedContentIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("targetedVideoCategoryIds")]
    public long[] targetedVideoCategoryIds {
      get { return this.targetedVideoCategoryIdsField; }
      set { this.targetedVideoCategoryIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("excludedVideoCategoryIds")]
    public long[] excludedVideoCategoryIds {
      get { return this.excludedVideoCategoryIdsField; }
      set { this.excludedVideoCategoryIdsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VideoPositionTargeting {
    private VideoPositionTarget[] targetedPositionsField;

    private string dummyField;

    [System.Xml.Serialization.XmlElementAttribute("targetedPositions")]
    public VideoPositionTarget[] targetedPositions {
      get { return this.targetedPositionsField; }
      set { this.targetedPositionsField = value; }
    }

    public string dummy {
      get { return this.dummyField; }
      set { this.dummyField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VideoPositionTarget {
    private VideoPosition videoPositionField;

    private VideoBumperType videoBumperTypeField;

    private bool videoBumperTypeFieldSpecified;

    private VideoPositionWithinPod videoPositionWithinPodField;

    public VideoPosition videoPosition {
      get { return this.videoPositionField; }
      set { this.videoPositionField = value; }
    }

    public VideoBumperType videoBumperType {
      get { return this.videoBumperTypeField; }
      set {
        this.videoBumperTypeField = value;
        this.videoBumperTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool videoBumperTypeSpecified {
      get { return this.videoBumperTypeFieldSpecified; }
      set { this.videoBumperTypeFieldSpecified = value; }
    }

    public VideoPositionWithinPod videoPositionWithinPod {
      get { return this.videoPositionWithinPodField; }
      set { this.videoPositionWithinPodField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VideoPosition {
    private VideoPositionType positionTypeField;

    private bool positionTypeFieldSpecified;

    private int midrollIndexField;

    private bool midrollIndexFieldSpecified;

    public VideoPositionType positionType {
      get { return this.positionTypeField; }
      set {
        this.positionTypeField = value;
        this.positionTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool positionTypeSpecified {
      get { return this.positionTypeFieldSpecified; }
      set { this.positionTypeFieldSpecified = value; }
    }

    public int midrollIndex {
      get { return this.midrollIndexField; }
      set {
        this.midrollIndexField = value;
        this.midrollIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool midrollIndexSpecified {
      get { return this.midrollIndexFieldSpecified; }
      set { this.midrollIndexFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "VideoPosition.Type", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum VideoPositionType {
    PREROLL,
    MIDROLL,
    POSTROLL
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum VideoBumperType {
    BEFORE,
    AFTER
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VideoPositionWithinPod {
    private int indexField;

    private bool indexFieldSpecified;

    public int index {
      get { return this.indexField; }
      set {
        this.indexField = value;
        this.indexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool indexSpecified {
      get { return this.indexFieldSpecified; }
      set { this.indexFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum StartDateTimeType {
    USE_START_DATE_TIME,
    IMMEDIATELY,
    ONE_HOUR_FROM_NOW
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdRuleStatus {
    ACTIVE,
    INACTIVE,
    DELETED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum FrequencyCapBehavior {
    TURN_ON,
    TURN_OFF,
    DEFER,
    UNKNOWN
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UnknownAdRuleSlot))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(StandardPoddingAdRuleSlot))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OptimizedPoddingAdRuleSlot))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(NoPoddingAdRuleSlot))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseAdRuleSlot {
    private long idField;

    private bool idFieldSpecified;

    private AdRuleSlotBehavior slotBehaviorField;

    private bool slotBehaviorFieldSpecified;

    private long minVideoAdDurationField;

    private bool minVideoAdDurationFieldSpecified;

    private long maxVideoAdDurationField;

    private bool maxVideoAdDurationFieldSpecified;

    private MidrollFrequencyType videoMidrollFrequencyTypeField;

    private bool videoMidrollFrequencyTypeFieldSpecified;

    private string videoMidrollFrequencyField;

    private AdRuleSlotBumper bumperField;

    private bool bumperFieldSpecified;

    private long maxBumperDurationField;

    private bool maxBumperDurationFieldSpecified;

    private long minPodDurationField;

    private bool minPodDurationFieldSpecified;

    private long maxPodDurationField;

    private bool maxPodDurationFieldSpecified;

    private int maxAdsInPodField;

    private bool maxAdsInPodFieldSpecified;

    private string baseAdRuleSlotTypeField;

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

    public AdRuleSlotBehavior slotBehavior {
      get { return this.slotBehaviorField; }
      set {
        this.slotBehaviorField = value;
        this.slotBehaviorSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool slotBehaviorSpecified {
      get { return this.slotBehaviorFieldSpecified; }
      set { this.slotBehaviorFieldSpecified = value; }
    }

    public long minVideoAdDuration {
      get { return this.minVideoAdDurationField; }
      set {
        this.minVideoAdDurationField = value;
        this.minVideoAdDurationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool minVideoAdDurationSpecified {
      get { return this.minVideoAdDurationFieldSpecified; }
      set { this.minVideoAdDurationFieldSpecified = value; }
    }

    public long maxVideoAdDuration {
      get { return this.maxVideoAdDurationField; }
      set {
        this.maxVideoAdDurationField = value;
        this.maxVideoAdDurationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool maxVideoAdDurationSpecified {
      get { return this.maxVideoAdDurationFieldSpecified; }
      set { this.maxVideoAdDurationFieldSpecified = value; }
    }

    public MidrollFrequencyType videoMidrollFrequencyType {
      get { return this.videoMidrollFrequencyTypeField; }
      set {
        this.videoMidrollFrequencyTypeField = value;
        this.videoMidrollFrequencyTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool videoMidrollFrequencyTypeSpecified {
      get { return this.videoMidrollFrequencyTypeFieldSpecified; }
      set { this.videoMidrollFrequencyTypeFieldSpecified = value; }
    }

    public string videoMidrollFrequency {
      get { return this.videoMidrollFrequencyField; }
      set { this.videoMidrollFrequencyField = value; }
    }

    public AdRuleSlotBumper bumper {
      get { return this.bumperField; }
      set {
        this.bumperField = value;
        this.bumperSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool bumperSpecified {
      get { return this.bumperFieldSpecified; }
      set { this.bumperFieldSpecified = value; }
    }

    public long maxBumperDuration {
      get { return this.maxBumperDurationField; }
      set {
        this.maxBumperDurationField = value;
        this.maxBumperDurationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool maxBumperDurationSpecified {
      get { return this.maxBumperDurationFieldSpecified; }
      set { this.maxBumperDurationFieldSpecified = value; }
    }

    public long minPodDuration {
      get { return this.minPodDurationField; }
      set {
        this.minPodDurationField = value;
        this.minPodDurationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool minPodDurationSpecified {
      get { return this.minPodDurationFieldSpecified; }
      set { this.minPodDurationFieldSpecified = value; }
    }

    public long maxPodDuration {
      get { return this.maxPodDurationField; }
      set {
        this.maxPodDurationField = value;
        this.maxPodDurationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool maxPodDurationSpecified {
      get { return this.maxPodDurationFieldSpecified; }
      set { this.maxPodDurationFieldSpecified = value; }
    }

    public int maxAdsInPod {
      get { return this.maxAdsInPodField; }
      set {
        this.maxAdsInPodField = value;
        this.maxAdsInPodSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool maxAdsInPodSpecified {
      get { return this.maxAdsInPodFieldSpecified; }
      set { this.maxAdsInPodFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("BaseAdRuleSlot.Type")]
    public string BaseAdRuleSlotType {
      get { return this.baseAdRuleSlotTypeField; }
      set { this.baseAdRuleSlotTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdRuleSlotBehavior {
    ALWAYS_SHOW,
    NEVER_SHOW,
    DEFER,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum MidrollFrequencyType {
    NONE,
    EVERY_N_SECONDS,
    FIXED_TIME,
    EVERY_N_CUEPOINTS,
    FIXED_CUE_POINTS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdRuleSlotBumper {
    NONE,
    BEFORE,
    AFTER,
    BEFORE_AND_AFTER,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UnknownAdRuleSlot : BaseAdRuleSlot {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class StandardPoddingAdRuleSlot : BaseAdRuleSlot {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class OptimizedPoddingAdRuleSlot : BaseAdRuleSlot {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class NoPoddingAdRuleSlot : BaseAdRuleSlot {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RequiredNumberError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RequiredNumberErrorReason {
    REQUIRED,
    TOO_LARGE,
    TOO_SMALL,
    TOO_LARGE_WITH_DETAILS,
    TOO_SMALL_WITH_DETAILS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class PoddingError : ApiError {
    private PoddingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public PoddingErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PoddingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum PoddingErrorReason {
    INVALID_PODDING_TYPE_NONE,
    INVALID_PODDING_TYPE_STANDARD,
    INVALID_OPTIMIZED_POD_WITHOUT_DURATION,
    INVALID_OPTIMIZED_POD_WITHOUT_ADS,
    INVALID_POD_DURATION_RANGE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InventoryTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InventoryTargetingErrorReason {
    AT_LEAST_ONE_PLACEMENT_OR_INVENTORY_UNIT_REQUIRED,
    INVENTORY_CANNOT_BE_TARGETED_AND_EXCLUDED,
    PARENT_CONTAINS_INVALID_MIX_OF_TARGETED_AND_EXCLUDED_AD_UNITS,
    INVENTORY_EXCLUSIONS_MUST_HAVE_PARENT_INVENTORY_UNIT,
    INVENTORY_UNIT_CANNOT_BE_TARGETED_IF_ANCESTOR_IS_TARGETED,
    INVENTORY_UNIT_CANNOT_BE_TARGETED_IF_ANCESTOR_IS_EXCLUDED,
    INVENTORY_UNIT_CANNOT_BE_EXCLUDED_IF_ANCESTOR_IS_EXCLUDED,
    EXPLICITLY_TARGETED_INVENTORY_UNIT_CANNOT_BE_TARGETED,
    EXPLICITLY_TARGETED_INVENTORY_UNIT_CANNOT_BE_EXCLUDED,
    SELF_ONLY_INVENTORY_UNIT_NOT_ALLOWED,
    SELF_ONLY_INVENTORY_UNIT_WITHOUT_DESCENDANTS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomTargetingErrorReason {
    KEY_NOT_FOUND,
    KEY_COUNT_TOO_LARGE,
    KEY_NAME_DUPLICATE,
    KEY_NAME_EMPTY,
    KEY_NAME_INVALID_LENGTH,
    KEY_NAME_INVALID_CHARS,
    KEY_NAME_RESERVED,
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
    INVALID_TARGETING_EXPRESSION,
    DELETED_KEY_CANNOT_BE_USED_FOR_TARGETING,
    DELETED_VALUE_CANNOT_BE_USED_FOR_TARGETING,
    VIDEO_BROWSE_BY_KEY_CANNOT_BE_USED_FOR_CUSTOM_TARGETING,
    CANNOT_TARGET_AUDIENCE_SEGMENT,
    CANNOT_TARGET_INACTIVE_AUDIENCE_SEGMENT,
    INVALID_AUDIENCE_SEGMENTS,
    ONLY_APPROVED_AUDIENCE_SEGMENTS_CAN_BE_TARGETED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdRuleSlotError : ApiError {
    private AdRuleSlotErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AdRuleSlotErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdRuleSlotError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdRuleSlotErrorReason {
    DIFFERENT_STATUS_THAN_AD_RULE,
    INVALID_VIDEO_AD_DURATION_RANGE,
    INVALID_VIDEO_MIDROLL_FREQUENCY_TYPE,
    MALFORMED_VIDEO_MIDROLL_FREQUENCY_CSV,
    MALFORMED_VIDEO_MIDROLL_FREQUENCY_SINGLE_NUMBER,
    INVALID_OVERLAY_AD_DURATION_RANGE,
    INVALID_OVERLAY_MIDROLL_FREQUENCY_TYPE,
    MALFORMED_OVERLAY_MIDROLL_FREQUENCY_CSV,
    MALFORMED_OVERLAY_MIDROLL_FREQUENCY_SINGLE_NUMBER,
    INVALID_BUMPER_MAX_DURATION,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdRulePriorityError : ApiError {
    private AdRulePriorityErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AdRulePriorityErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdRulePriorityError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdRulePriorityErrorReason {
    DUPLICATE_PRIORITY,
    PRIORITIES_NOT_SEQUENTIAL,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdRuleFrequencyCapError : ApiError {
    private AdRuleFrequencyCapErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AdRuleFrequencyCapErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdRuleFrequencyCapError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdRuleFrequencyCapErrorReason {
    NO_FREQUENCY_CAPS_SPECIFIED_WHEN_FREQUENCY_CAPS_TURNED_ON,
    FREQUENCY_CAPS_SPECIFIED_WHEN_FREQUENCY_CAPS_TURNED_OFF,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdRuleDateError : ApiError {
    private AdRuleDateErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AdRuleDateErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdRuleDateError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdRuleDateErrorReason {
    START_DATE_TIME_IS_IN_PAST,
    END_DATE_TIME_IS_IN_PAST,
    END_DATE_TIME_NOT_AFTER_START_TIME,
    END_DATE_TIME_TOO_LATE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "WorkflowActionServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class WorkflowActionService : DfpSoapClient, IWorkflowActionService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public WorkflowActionService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/WorkflowActionService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual WorkflowAction getWorkflowAction(long workflowActionId) {
      object[] results = this.Invoke("getWorkflowAction", new object[] { workflowActionId });
      return ((WorkflowAction) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual WorkflowActionPage getWorkflowActionsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getWorkflowActionsByStatement", new object[] { filterStatement });
      return ((WorkflowActionPage) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class WorkflowActionPage {
    private WorkflowAction[] resultsField;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public WorkflowAction[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
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
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class WorkflowAction {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private long[] userIdsField;

    private WorkflowActionType typeField;

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

    [System.Xml.Serialization.XmlElementAttribute("userIds")]
    public long[] userIds {
      get { return this.userIdsField; }
      set { this.userIdsField = value; }
    }

    public WorkflowActionType type {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum WorkflowActionType {
    APPROVAL,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "NetworkServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class NetworkService : DfpSoapClient, INetworkService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public NetworkService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/NetworkService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Network[] getAllNetworks() {
      object[] results = this.Invoke("getAllNetworks", new object[0]);
      return ((Network[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Network getCurrentNetwork() {
      object[] results = this.Invoke("getCurrentNetwork", new object[0]);
      return ((Network) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Network makeTestNetwork() {
      object[] results = this.Invoke("makeTestNetwork", new object[0]);
      return ((Network) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Network updateNetwork(Network network) {
      object[] results = this.Invoke("updateNetwork", new object[] { network });
      return ((Network) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Network {
    private long idField;

    private bool idFieldSpecified;

    private string displayNameField;

    private string networkCodeField;

    private string propertyCodeField;

    private string timeZoneField;

    private string currencyCodeField;

    private string[] secondaryCurrencyCodesField;

    private string effectiveRootAdUnitIdField;

    private long contentBrowseCustomTargetingKeyIdField;

    private bool contentBrowseCustomTargetingKeyIdFieldSpecified;

    private bool isTestField;

    private bool isTestFieldSpecified;

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

    [System.Xml.Serialization.XmlElementAttribute("secondaryCurrencyCodes")]
    public string[] secondaryCurrencyCodes {
      get { return this.secondaryCurrencyCodesField; }
      set { this.secondaryCurrencyCodesField = value; }
    }

    public string effectiveRootAdUnitId {
      get { return this.effectiveRootAdUnitIdField; }
      set { this.effectiveRootAdUnitIdField = value; }
    }

    public long contentBrowseCustomTargetingKeyId {
      get { return this.contentBrowseCustomTargetingKeyIdField; }
      set {
        this.contentBrowseCustomTargetingKeyIdField = value;
        this.contentBrowseCustomTargetingKeyIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool contentBrowseCustomTargetingKeyIdSpecified {
      get { return this.contentBrowseCustomTargetingKeyIdFieldSpecified; }
      set { this.contentBrowseCustomTargetingKeyIdFieldSpecified = value; }
    }

    public bool isTest {
      get { return this.isTestField; }
      set {
        this.isTestField = value;
        this.isTestSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isTestSpecified {
      get { return this.isTestFieldSpecified; }
      set { this.isTestFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class NetworkError : ApiError {
    private NetworkErrorReason reasonField;

    private bool reasonFieldSpecified;

    public NetworkErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "NetworkError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum NetworkErrorReason {
    UNKNOWN,
    MULTI_CURRENCY_NOT_SUPPORTED,
    UNSUPPORTED_CURRENCY,
    NETWORK_CURRENCY_CANNOT_BE_SAME_AS_SECONDARY
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ReconciliationReportServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ReconciliationReportService : DfpSoapClient, IReconciliationReportService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ReconciliationReportService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ReconciliationReportService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReconciliationReport getReconciliationReport(long reconciliationReportId) {
      object[] results = this.Invoke("getReconciliationReport", new object[] { reconciliationReportId });
      return ((ReconciliationReport) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReconciliationReportPage getReconciliationReportsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getReconciliationReportsByStatement", new object[] { filterStatement });
      return ((ReconciliationReportPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReconciliationReport updateReconciliationReport(ReconciliationReport reconciliationReport) {
      object[] results = this.Invoke("updateReconciliationReport", new object[] { reconciliationReport });
      return ((ReconciliationReport) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReconciliationReport[] updateReconciliationReports([System.Xml.Serialization.XmlElementAttribute("reconciliationReports")]
ReconciliationReport[] reconciliationReports) {
      object[] results = this.Invoke("updateReconciliationReports", new object[] { reconciliationReports });
      return ((ReconciliationReport[]) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReconciliationReportPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private ReconciliationReport[] resultsField;

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
    public ReconciliationReport[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReconciliationReport {
    private long idField;

    private bool idFieldSpecified;

    private ReconciliationReportStatus statusField;

    private bool statusFieldSpecified;

    private Date startDateField;

    private string notesField;

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

    public ReconciliationReportStatus status {
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

    public Date startDate {
      get { return this.startDateField; }
      set { this.startDateField = value; }
    }

    public string notes {
      get { return this.notesField; }
      set { this.notesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ReconciliationReportStatus {
    DRAFT,
    RECONCILED,
    REVERTED,
    PENDING,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReconciliationImportError : ApiError {
    private ReconciliationImportErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ReconciliationImportErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ReconciliationImportError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ReconciliationImportErrorReason {
    MISSING_EDITABLE_COLUMN,
    INCONSISTENT_IMPORT_COLUMNS,
    ILLEGAL_CONVERTION,
    INCONSISTENT_COLUMNS_COUNT,
    IMPORT_INTERNAL_ERROR,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReconciliationError : ApiError {
    private ReconciliationErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ReconciliationErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ReconciliationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ReconciliationErrorReason {
    CANNOT_CREATE_RECONCILIATION_REPORT_VERSION,
    INVALID_RECONCILIATION_REPORT_STATE_TRANSITION,
    USER_CANNOT_RECONCILE_RECONCILIATION_REPORT,
    CONTRACTED_VOLUME_CANNOT_BE_NULL,
    DFP_VOLUME_CANNOT_BE_NULL,
    MANUAL_VOLUME_CANNOT_BE_NULL,
    THIRD_PARTY_VOLUME_CANNOT_BE_NULL,
    DUPLICATE_LINE_ITEM_AND_CREATIVE,
    CANNOT_RECONCILE_LINE_ITEM_CREATIVE,
    LINE_ITEM_DAYS_MISMATCH,
    LINE_ITEM_BILL_OFF_OF_MISMATCH,
    CANNOT_MODIFY_RECONCILED_ORDER,
    CANNOT_MODIFY_ACROSS_MULTIPLE_RECONCILIATION_REPORTS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RangeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RangeErrorReason {
    TOO_HIGH,
    TOO_LOW,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CreativeWrapperServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CreativeWrapperService : DfpSoapClient, ICreativeWrapperService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CreativeWrapperService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/CreativeWrapperService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeWrapper createCreativeWrapper(CreativeWrapper creativeWrapper) {
      object[] results = this.Invoke("createCreativeWrapper", new object[] { creativeWrapper });
      return ((CreativeWrapper) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeWrapper[] createCreativeWrappers([System.Xml.Serialization.XmlElementAttribute("creativeWrappers")]
CreativeWrapper[] creativeWrappers) {
      object[] results = this.Invoke("createCreativeWrappers", new object[] { creativeWrappers });
      return ((CreativeWrapper[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeWrapper getCreativeWrapper(long creativeWrapperId) {
      object[] results = this.Invoke("getCreativeWrapper", new object[] { creativeWrapperId });
      return ((CreativeWrapper) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeWrapperPage getCreativeWrappersByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCreativeWrappersByStatement", new object[] { filterStatement });
      return ((CreativeWrapperPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performCreativeWrapperAction(CreativeWrapperAction creativeWrapperAction, Statement filterStatement) {
      object[] results = this.Invoke("performCreativeWrapperAction", new object[] { creativeWrapperAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeWrapper updateCreativeWrapper(CreativeWrapper creativeWrapper) {
      object[] results = this.Invoke("updateCreativeWrapper", new object[] { creativeWrapper });
      return ((CreativeWrapper) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeWrapper[] updateCreativeWrappers([System.Xml.Serialization.XmlElementAttribute("creativeWrappers")]
CreativeWrapper[] creativeWrappers) {
      object[] results = this.Invoke("updateCreativeWrappers", new object[] { creativeWrappers });
      return ((CreativeWrapper[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateCreativeWrappers))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateCreativeWrappers))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class CreativeWrapperAction {
    private string creativeWrapperActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("CreativeWrapperAction.Type")]
    public string CreativeWrapperActionType {
      get { return this.creativeWrapperActionTypeField; }
      set { this.creativeWrapperActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateCreativeWrappers : CreativeWrapperAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateCreativeWrappers : CreativeWrapperAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeWrapperPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private CreativeWrapper[] resultsField;

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
    public CreativeWrapper[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeWrapper {
    private long idField;

    private bool idFieldSpecified;

    private long labelIdField;

    private bool labelIdFieldSpecified;

    private CreativeWrapperHtmlSnippet headerField;

    private CreativeWrapperHtmlSnippet footerField;

    private CreativeWrapperOrdering orderingField;

    private bool orderingFieldSpecified;

    private CreativeWrapperStatus statusField;

    private bool statusFieldSpecified;

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

    public long labelId {
      get { return this.labelIdField; }
      set {
        this.labelIdField = value;
        this.labelIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool labelIdSpecified {
      get { return this.labelIdFieldSpecified; }
      set { this.labelIdFieldSpecified = value; }
    }

    public CreativeWrapperHtmlSnippet header {
      get { return this.headerField; }
      set { this.headerField = value; }
    }

    public CreativeWrapperHtmlSnippet footer {
      get { return this.footerField; }
      set { this.footerField = value; }
    }

    public CreativeWrapperOrdering ordering {
      get { return this.orderingField; }
      set {
        this.orderingField = value;
        this.orderingSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool orderingSpecified {
      get { return this.orderingFieldSpecified; }
      set { this.orderingFieldSpecified = value; }
    }

    public CreativeWrapperStatus status {
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
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeWrapperHtmlSnippet {
    private string htmlSnippetField;

    public string htmlSnippet {
      get { return this.htmlSnippetField; }
      set { this.htmlSnippetField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeWrapperOrdering {
    NO_PREFERENCE,
    INNER,
    OUTER
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeWrapperStatus {
    ACTIVE,
    INACTIVE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ContentServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ContentService : DfpSoapClient, IContentService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ContentService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ContentService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ContentPage getContentByStatement(Statement statement) {
      object[] results = this.Invoke("getContentByStatement", new object[] { statement });
      return ((ContentPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ContentPage getContentByStatementAndCustomTargetingValue(Statement filterStatement, long customTargetingValueId) {
      object[] results = this.Invoke("getContentByStatementAndCustomTargetingValue", new object[] { filterStatement, customTargetingValueId });
      return ((ContentPage) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CmsContent {
    private long idField;

    private bool idFieldSpecified;

    private string displayNameField;

    private string cmsContentIdField;

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

    public string cmsContentId {
      get { return this.cmsContentIdField; }
      set { this.cmsContentIdField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Content {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private ContentStatus statusField;

    private bool statusFieldSpecified;

    private ContentStatusDefinedBy statusDefinedByField;

    private bool statusDefinedByFieldSpecified;

    private long[] userDefinedCustomTargetingValueIdsField;

    private long[] mappingRuleDefinedCustomTargetingValueIdsField;

    private CmsContent[] cmsSourcesField;

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

    public ContentStatus status {
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

    public ContentStatusDefinedBy statusDefinedBy {
      get { return this.statusDefinedByField; }
      set {
        this.statusDefinedByField = value;
        this.statusDefinedBySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool statusDefinedBySpecified {
      get { return this.statusDefinedByFieldSpecified; }
      set { this.statusDefinedByFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("userDefinedCustomTargetingValueIds")]
    public long[] userDefinedCustomTargetingValueIds {
      get { return this.userDefinedCustomTargetingValueIdsField; }
      set { this.userDefinedCustomTargetingValueIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("mappingRuleDefinedCustomTargetingValueIds")]
    public long[] mappingRuleDefinedCustomTargetingValueIds {
      get { return this.mappingRuleDefinedCustomTargetingValueIdsField; }
      set { this.mappingRuleDefinedCustomTargetingValueIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("cmsSources")]
    public CmsContent[] cmsSources {
      get { return this.cmsSourcesField; }
      set { this.cmsSourcesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ContentStatus {
    ACTIVE,
    INACTIVE,
    ARCHIVED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ContentStatusDefinedBy {
    CMS,
    USER
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ContentPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Content[] resultsField;

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
    public Content[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RequiredCollectionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RequiredCollectionErrorReason {
    REQUIRED,
    TOO_LARGE,
    TOO_SMALL,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InvalidUrlError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InvalidUrlErrorReason {
    ILLEGAL_CHARACTERS,
    INVALID_FORMAT,
    INSECURE_SCHEME,
    NO_SCHEME,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ContentPartnerError : ApiError {
    private ContentPartnerErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ContentPartnerErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ContentPartnerError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ContentPartnerErrorReason {
    FEATURE_NOT_ENABLED,
    INVALID_PARTNER_TYPE,
    NO_PARTNER_CATCH_ALL,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CustomFieldServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CustomFieldService : DfpSoapClient, ICustomFieldService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CustomFieldService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/CustomFieldService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomField createCustomField(CustomField customField) {
      object[] results = this.Invoke("createCustomField", new object[] { customField });
      return ((CustomField) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomFieldOption createCustomFieldOption(CustomFieldOption customFieldOption) {
      object[] results = this.Invoke("createCustomFieldOption", new object[] { customFieldOption });
      return ((CustomFieldOption) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomFieldOption[] createCustomFieldOptions([System.Xml.Serialization.XmlElementAttribute("customFieldOptions")]
CustomFieldOption[] customFieldOptions) {
      object[] results = this.Invoke("createCustomFieldOptions", new object[] { customFieldOptions });
      return ((CustomFieldOption[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomField[] createCustomFields([System.Xml.Serialization.XmlElementAttribute("customFields")]
CustomField[] customFields) {
      object[] results = this.Invoke("createCustomFields", new object[] { customFields });
      return ((CustomField[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomField getCustomField(long customFieldId) {
      object[] results = this.Invoke("getCustomField", new object[] { customFieldId });
      return ((CustomField) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomFieldOption getCustomFieldOption(long customFieldOptionId) {
      object[] results = this.Invoke("getCustomFieldOption", new object[] { customFieldOptionId });
      return ((CustomFieldOption) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomFieldPage getCustomFieldsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCustomFieldsByStatement", new object[] { filterStatement });
      return ((CustomFieldPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performCustomFieldAction(CustomFieldAction customFieldAction, Statement filterStatement) {
      object[] results = this.Invoke("performCustomFieldAction", new object[] { customFieldAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomField updateCustomField(CustomField customField) {
      object[] results = this.Invoke("updateCustomField", new object[] { customField });
      return ((CustomField) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomFieldOption updateCustomFieldOption(CustomFieldOption customFieldOption) {
      object[] results = this.Invoke("updateCustomFieldOption", new object[] { customFieldOption });
      return ((CustomFieldOption) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomFieldOption[] updateCustomFieldOptions([System.Xml.Serialization.XmlElementAttribute("customFieldOptions")]
CustomFieldOption[] customFieldOptions) {
      object[] results = this.Invoke("updateCustomFieldOptions", new object[] { customFieldOptions });
      return ((CustomFieldOption[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomField[] updateCustomFields([System.Xml.Serialization.XmlElementAttribute("customFields")]
CustomField[] customFields) {
      object[] results = this.Invoke("updateCustomFields", new object[] { customFields });
      return ((CustomField[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateCustomFields))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateCustomFields))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomFieldAction {
    private string customFieldActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("CustomFieldAction.Type")]
    public string CustomFieldActionType {
      get { return this.customFieldActionTypeField; }
      set { this.customFieldActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateCustomFields : CustomFieldAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateCustomFields : CustomFieldAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomFieldPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private CustomField[] resultsField;

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
    public CustomField[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DropDownCustomField))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomField {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string descriptionField;

    private bool isActiveField;

    private bool isActiveFieldSpecified;

    private CustomFieldEntityType entityTypeField;

    private bool entityTypeFieldSpecified;

    private CustomFieldDataType dataTypeField;

    private bool dataTypeFieldSpecified;

    private CustomFieldVisibility visibilityField;

    private bool visibilityFieldSpecified;

    private string customFieldTypeField;

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

    public CustomFieldEntityType entityType {
      get { return this.entityTypeField; }
      set {
        this.entityTypeField = value;
        this.entityTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool entityTypeSpecified {
      get { return this.entityTypeFieldSpecified; }
      set { this.entityTypeFieldSpecified = value; }
    }

    public CustomFieldDataType dataType {
      get { return this.dataTypeField; }
      set {
        this.dataTypeField = value;
        this.dataTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool dataTypeSpecified {
      get { return this.dataTypeFieldSpecified; }
      set { this.dataTypeFieldSpecified = value; }
    }

    public CustomFieldVisibility visibility {
      get { return this.visibilityField; }
      set {
        this.visibilityField = value;
        this.visibilitySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool visibilitySpecified {
      get { return this.visibilityFieldSpecified; }
      set { this.visibilityFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("CustomField.Type")]
    public string CustomFieldType {
      get { return this.customFieldTypeField; }
      set { this.customFieldTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomFieldEntityType {
    LINE_ITEM,
    ORDER,
    CREATIVE,
    PRODUCT_TEMPLATE,
    PRODUCT,
    PROPOSAL,
    PROPOSAL_LINE_ITEM,
    USER,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomFieldDataType {
    STRING,
    NUMBER,
    TOGGLE,
    DROP_DOWN,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomFieldVisibility {
    API_ONLY,
    READ_ONLY,
    FULL
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DropDownCustomField : CustomField {
    private CustomFieldOption[] optionsField;

    [System.Xml.Serialization.XmlElementAttribute("options")]
    public CustomFieldOption[] options {
      get { return this.optionsField; }
      set { this.optionsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomFieldOption {
    private long idField;

    private bool idFieldSpecified;

    private long customFieldIdField;

    private bool customFieldIdFieldSpecified;

    private string displayNameField;

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

    public long customFieldId {
      get { return this.customFieldIdField; }
      set {
        this.customFieldIdField = value;
        this.customFieldIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool customFieldIdSpecified {
      get { return this.customFieldIdFieldSpecified; }
      set { this.customFieldIdFieldSpecified = value; }
    }

    public string displayName {
      get { return this.displayNameField; }
      set { this.displayNameField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class EntityLimitReachedError : ApiError {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomFieldError : ApiError {
    private CustomFieldErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CustomFieldErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomFieldError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomFieldErrorReason {
    INVALID_CUSTOM_FIELD_FOR_OPTION,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ProposalLineItemServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ProposalLineItemService : DfpSoapClient, IProposalLineItemService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ProposalLineItemService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ProposalLineItemService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProposalLineItem createProposalLineItem(ProposalLineItem proposalLineItem) {
      object[] results = this.Invoke("createProposalLineItem", new object[] { proposalLineItem });
      return ((ProposalLineItem) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProposalLineItem[] createProposalLineItems([System.Xml.Serialization.XmlElementAttribute("proposalLineItems")]
ProposalLineItem[] proposalLineItems) {
      object[] results = this.Invoke("createProposalLineItems", new object[] { proposalLineItems });
      return ((ProposalLineItem[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProposalLineItem getProposalLineItem(long proposalLineItemId) {
      object[] results = this.Invoke("getProposalLineItem", new object[] { proposalLineItemId });
      return ((ProposalLineItem) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProposalLineItemPage getProposalLineItemsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getProposalLineItemsByStatement", new object[] { filterStatement });
      return ((ProposalLineItemPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performProposalLineItemAction(ProposalLineItemAction proposalLineItemAction, Statement filterStatement) {
      object[] results = this.Invoke("performProposalLineItemAction", new object[] { proposalLineItemAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProposalLineItem updateProposalLineItem(ProposalLineItem proposalLineItem) {
      object[] results = this.Invoke("updateProposalLineItem", new object[] { proposalLineItem });
      return ((ProposalLineItem) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProposalLineItem[] updateProposalLineItems([System.Xml.Serialization.XmlElementAttribute("proposalLineItems")]
ProposalLineItem[] proposalLineItems) {
      object[] results = this.Invoke("updateProposalLineItems", new object[] { proposalLineItems });
      return ((ProposalLineItem[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UnarchiveProposalLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveProposalLineItems))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class ProposalLineItemAction {
    private string proposalLineItemActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("ProposalLineItemAction.Type")]
    public string ProposalLineItemActionType {
      get { return this.proposalLineItemActionTypeField; }
      set { this.proposalLineItemActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UnarchiveProposalLineItems : ProposalLineItemAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ArchiveProposalLineItems : ProposalLineItemAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProposalLineItemPage {
    private ProposalLineItem[] resultsField;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public ProposalLineItem[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
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
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProposalLineItem {
    private long idField;

    private bool idFieldSpecified;

    private long proposalIdField;

    private bool proposalIdFieldSpecified;

    private long rateCardIdField;

    private bool rateCardIdFieldSpecified;

    private string productIdField;

    private string nameField;

    private DateTime startDateTimeField;

    private DateTime endDateTimeField;

    private string notesField;

    private CostAdjustment costAdjustmentField;

    private bool costAdjustmentFieldSpecified;

    private bool isArchivedField;

    private bool isArchivedFieldSpecified;

    private long unitsBoughtField;

    private bool unitsBoughtFieldSpecified;

    private int unitsBoughtBufferField;

    private bool unitsBoughtBufferFieldSpecified;

    private DeliveryRateType deliveryRateTypeField;

    private bool deliveryRateTypeFieldSpecified;

    private RoadblockingType roadblockingTypeField;

    private bool roadblockingTypeFieldSpecified;

    private CompanionDeliveryOption companionDeliveryOptionField;

    private bool companionDeliveryOptionFieldSpecified;

    private CreativeRotationType creativeRotationTypeField;

    private bool creativeRotationTypeFieldSpecified;

    private FrequencyCap[] frequencyCapsField;

    private LineItemType lineItemTypeField;

    private bool lineItemTypeFieldSpecified;

    private int lineItemPriorityField;

    private bool lineItemPriorityFieldSpecified;

    private CostType costTypeField;

    private bool costTypeFieldSpecified;

    private CreativePlaceholder[] creativePlaceholdersField;

    private Targeting targetingField;

    private BaseCustomFieldValue[] customFieldValuesField;

    private AppliedLabel[] appliedLabelsField;

    private AppliedLabel[] effectiveAppliedLabelsField;

    private ProposalLineItemPremium[] premiumsField;

    private Money baseRateField;

    private Money costPerUnitField;

    private Money costField;

    private DeliveryIndicator deliveryIndicatorField;

    private long[] deliveryDataField;

    private ComputedStatus computedStatusField;

    private bool computedStatusFieldSpecified;

    private BillingCap billingCapField;

    private bool billingCapFieldSpecified;

    private BillingSchedule billingScheduleField;

    private bool billingScheduleFieldSpecified;

    private BillingSource billingSourceField;

    private bool billingSourceFieldSpecified;

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

    public long proposalId {
      get { return this.proposalIdField; }
      set {
        this.proposalIdField = value;
        this.proposalIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool proposalIdSpecified {
      get { return this.proposalIdFieldSpecified; }
      set { this.proposalIdFieldSpecified = value; }
    }

    public long rateCardId {
      get { return this.rateCardIdField; }
      set {
        this.rateCardIdField = value;
        this.rateCardIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool rateCardIdSpecified {
      get { return this.rateCardIdFieldSpecified; }
      set { this.rateCardIdFieldSpecified = value; }
    }

    public string productId {
      get { return this.productIdField; }
      set { this.productIdField = value; }
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

    public string notes {
      get { return this.notesField; }
      set { this.notesField = value; }
    }

    public CostAdjustment costAdjustment {
      get { return this.costAdjustmentField; }
      set {
        this.costAdjustmentField = value;
        this.costAdjustmentSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool costAdjustmentSpecified {
      get { return this.costAdjustmentFieldSpecified; }
      set { this.costAdjustmentFieldSpecified = value; }
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

    public int unitsBoughtBuffer {
      get { return this.unitsBoughtBufferField; }
      set {
        this.unitsBoughtBufferField = value;
        this.unitsBoughtBufferSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool unitsBoughtBufferSpecified {
      get { return this.unitsBoughtBufferFieldSpecified; }
      set { this.unitsBoughtBufferFieldSpecified = value; }
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

    public CompanionDeliveryOption companionDeliveryOption {
      get { return this.companionDeliveryOptionField; }
      set {
        this.companionDeliveryOptionField = value;
        this.companionDeliveryOptionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool companionDeliveryOptionSpecified {
      get { return this.companionDeliveryOptionFieldSpecified; }
      set { this.companionDeliveryOptionFieldSpecified = value; }
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

    public int lineItemPriority {
      get { return this.lineItemPriorityField; }
      set {
        this.lineItemPriorityField = value;
        this.lineItemPrioritySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool lineItemPrioritySpecified {
      get { return this.lineItemPriorityFieldSpecified; }
      set { this.lineItemPriorityFieldSpecified = value; }
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

    [System.Xml.Serialization.XmlElementAttribute("creativePlaceholders")]
    public CreativePlaceholder[] creativePlaceholders {
      get { return this.creativePlaceholdersField; }
      set { this.creativePlaceholdersField = value; }
    }

    public Targeting targeting {
      get { return this.targetingField; }
      set { this.targetingField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customFieldValues")]
    public BaseCustomFieldValue[] customFieldValues {
      get { return this.customFieldValuesField; }
      set { this.customFieldValuesField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedLabels")]
    public AppliedLabel[] appliedLabels {
      get { return this.appliedLabelsField; }
      set { this.appliedLabelsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("effectiveAppliedLabels")]
    public AppliedLabel[] effectiveAppliedLabels {
      get { return this.effectiveAppliedLabelsField; }
      set { this.effectiveAppliedLabelsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("premiums")]
    public ProposalLineItemPremium[] premiums {
      get { return this.premiumsField; }
      set { this.premiumsField = value; }
    }

    public Money baseRate {
      get { return this.baseRateField; }
      set { this.baseRateField = value; }
    }

    public Money costPerUnit {
      get { return this.costPerUnitField; }
      set { this.costPerUnitField = value; }
    }

    public Money cost {
      get { return this.costField; }
      set { this.costField = value; }
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

    public ComputedStatus computedStatus {
      get { return this.computedStatusField; }
      set {
        this.computedStatusField = value;
        this.computedStatusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool computedStatusSpecified {
      get { return this.computedStatusFieldSpecified; }
      set { this.computedStatusFieldSpecified = value; }
    }

    public BillingCap billingCap {
      get { return this.billingCapField; }
      set {
        this.billingCapField = value;
        this.billingCapSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool billingCapSpecified {
      get { return this.billingCapFieldSpecified; }
      set { this.billingCapFieldSpecified = value; }
    }

    public BillingSchedule billingSchedule {
      get { return this.billingScheduleField; }
      set {
        this.billingScheduleField = value;
        this.billingScheduleSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool billingScheduleSpecified {
      get { return this.billingScheduleFieldSpecified; }
      set { this.billingScheduleFieldSpecified = value; }
    }

    public BillingSource billingSource {
      get { return this.billingSourceField; }
      set {
        this.billingSourceField = value;
        this.billingSourceSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool billingSourceSpecified {
      get { return this.billingSourceFieldSpecified; }
      set { this.billingSourceFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CostAdjustment {
    NONE,
    MAKE_GOOD,
    BARTER,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum DeliveryRateType {
    EVENLY,
    FRONTLOADED,
    AS_FAST_AS_POSSIBLE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RoadblockingType {
    ONLY_ONE,
    ONE_OR_MORE,
    AS_MANY_AS_POSSIBLE,
    ALL_ROADBLOCK,
    CREATIVE_SET
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CompanionDeliveryOption {
    OPTIONAL,
    AT_LEAST_ONE,
    ALL,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeRotationType {
    EVEN,
    OPTIMIZED,
    MANUAL,
    SEQUENTIAL
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class FrequencyCap {
    private int maxImpressionsField;

    private bool maxImpressionsFieldSpecified;

    private int numTimeUnitsField;

    private bool numTimeUnitsFieldSpecified;

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

    public int numTimeUnits {
      get { return this.numTimeUnitsField; }
      set {
        this.numTimeUnitsField = value;
        this.numTimeUnitsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool numTimeUnitsSpecified {
      get { return this.numTimeUnitsFieldSpecified; }
      set { this.numTimeUnitsFieldSpecified = value; }
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum TimeUnit {
    MINUTE,
    HOUR,
    DAY,
    WEEK,
    MONTH,
    LIFETIME,
    POD,
    STREAM
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemType {
    SPONSORSHIP,
    STANDARD,
    NETWORK,
    BULK,
    PRICE_PRIORITY,
    HOUSE,
    LEGACY_DFP,
    CLICK_TRACKING,
    ADSENSE,
    AD_EXCHANGE,
    BUMPER,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CostType {
    CPC,
    CPD,
    CPM,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativePlaceholder {
    private Size sizeField;

    private CreativePlaceholder[] companionsField;

    private AppliedLabel[] appliedLabelsField;

    private AppliedLabel[] effectiveAppliedLabelsField;

    private long idField;

    private bool idFieldSpecified;

    private int expectedCreativeCountField;

    private bool expectedCreativeCountFieldSpecified;

    private CreativeSizeType creativeSizeTypeField;

    private bool creativeSizeTypeFieldSpecified;

    public Size size {
      get { return this.sizeField; }
      set { this.sizeField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("companions")]
    public CreativePlaceholder[] companions {
      get { return this.companionsField; }
      set { this.companionsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedLabels")]
    public AppliedLabel[] appliedLabels {
      get { return this.appliedLabelsField; }
      set { this.appliedLabelsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("effectiveAppliedLabels")]
    public AppliedLabel[] effectiveAppliedLabels {
      get { return this.effectiveAppliedLabelsField; }
      set { this.effectiveAppliedLabelsField = value; }
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

    public int expectedCreativeCount {
      get { return this.expectedCreativeCountField; }
      set {
        this.expectedCreativeCountField = value;
        this.expectedCreativeCountSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool expectedCreativeCountSpecified {
      get { return this.expectedCreativeCountFieldSpecified; }
      set { this.expectedCreativeCountFieldSpecified = value; }
    }

    public CreativeSizeType creativeSizeType {
      get { return this.creativeSizeTypeField; }
      set {
        this.creativeSizeTypeField = value;
        this.creativeSizeTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool creativeSizeTypeSpecified {
      get { return this.creativeSizeTypeFieldSpecified; }
      set { this.creativeSizeTypeFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeSizeType {
    PIXEL,
    ASPECT_RATIO,
    INTERSTITIAL
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DropDownCustomFieldValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomFieldValue))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseCustomFieldValue {
    private long customFieldIdField;

    private bool customFieldIdFieldSpecified;

    private string baseCustomFieldValueTypeField;

    public long customFieldId {
      get { return this.customFieldIdField; }
      set {
        this.customFieldIdField = value;
        this.customFieldIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool customFieldIdSpecified {
      get { return this.customFieldIdFieldSpecified; }
      set { this.customFieldIdFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("BaseCustomFieldValue.Type")]
    public string BaseCustomFieldValueType {
      get { return this.baseCustomFieldValueTypeField; }
      set { this.baseCustomFieldValueTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DropDownCustomFieldValue : BaseCustomFieldValue {
    private long customFieldOptionIdField;

    private bool customFieldOptionIdFieldSpecified;

    public long customFieldOptionId {
      get { return this.customFieldOptionIdField; }
      set {
        this.customFieldOptionIdField = value;
        this.customFieldOptionIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool customFieldOptionIdSpecified {
      get { return this.customFieldOptionIdFieldSpecified; }
      set { this.customFieldOptionIdFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomFieldValue : BaseCustomFieldValue {
    private Value valueField;

    public Value value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProposalLineItemPremium {
    private long rateCardCustomizationIdField;

    private bool rateCardCustomizationIdFieldSpecified;

    private ProposalLineItemPremiumStatus statusField;

    private bool statusFieldSpecified;

    public long rateCardCustomizationId {
      get { return this.rateCardCustomizationIdField; }
      set {
        this.rateCardCustomizationIdField = value;
        this.rateCardCustomizationIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool rateCardCustomizationIdSpecified {
      get { return this.rateCardCustomizationIdFieldSpecified; }
      set { this.rateCardCustomizationIdFieldSpecified = value; }
    }

    public ProposalLineItemPremiumStatus status {
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
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProposalLineItemPremiumStatus {
    INCLUDED,
    EXCLUDED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ComputedStatus {
    DELIVERY_EXTENDED,
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum BillingCap {
    NO_CAP,
    CAPPED_CUMULATIVE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum BillingSchedule {
    PRORATED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum BillingSource {
    CONTRACTED,
    DFP_VOLUME,
    THIRD_PARTY_VOLUME,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "UserDomainTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum UserDomainTargetingErrorReason {
    INVALID_DOMAIN_NAMES,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class TechnologyTargetingError : ApiError {
    private TechnologyTargetingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public TechnologyTargetingErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "TechnologyTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum TechnologyTargetingErrorReason {
    MOBILE_LINE_ITEM_CONTAINS_WEB_TECH_CRITERIA,
    WEB_LINE_ITEM_CONTAINS_MOBILE_TECH_CRITERIA,
    MOBILE_CARRIER_TARGETING_FEATURE_NOT_ENABLED,
    DEVICE_CAPABILITY_TARGETING_FEATURE_NOT_ENABLED,
    DEVICE_CATEGORY_TARGETING_FEATURE_NOT_ENABLED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ReservationDetailsError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ReservationDetailsErrorReason {
    UNLIMITED_UNITS_BOUGHT_NOT_ALLOWED,
    UNLIMITED_END_DATE_TIME_NOT_ALLOWED,
    PERCENTAGE_UNITS_BOUGHT_TOO_HIGH,
    DURATION_NOT_ALLOWED,
    UNIT_TYPE_NOT_ALLOWED,
    COST_TYPE_NOT_ALLOWED,
    COST_TYPE_UNIT_TYPE_MISMATCH_NOT_ALLOWED,
    LINE_ITEM_TYPE_NOT_ALLOWED,
    NETWORK_REMNANT_ORDER_CANNOT_UPDATE_LINEITEM_TYPE,
    BACKFILL_WEBPROPERTY_CODE_NOT_ALLOWED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProposalLineItemError : ApiError {
    private ProposalLineItemErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ProposalLineItemErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ProposalLineItemError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProposalLineItemErrorReason {
    NOT_SAME_RATE_CARD,
    LINE_ITEM_TYPE_NOT_ALLOWED,
    END_DATE_TIME_NOT_AFTER_START_TIME,
    END_DATE_TIME_TOO_LATE,
    START_DATE_TIME_IS_IN_PAST,
    END_DATE_TIME_IS_IN_PAST,
    FRONTLOADED_NOT_ALLOWED,
    ALL_ROADBLOCK_NOT_ALLOWED,
    CREATIVE_SET_ROADBLOCK_NOT_ALLOWED,
    ALREADY_STARTED,
    CONFLICT_WITH_PRODUCT,
    MOBILE_TECH_CRITERIA_NOT_SUPPORTED,
    UNSUPPORTED_TARGETING_TYPE,
    WRONG_COST,
    INVALID_PRIORITY_FOR_LINE_ITEM_TYPE,
    UPDATE_PROPOSAL_LINE_ITEM_NOT_ALLOWED,
    CANNOT_UPDATE_TO_OR_FROM_CREATIVE_SET_ROADBLOCK,
    SEQUENTIAL_CREATIVE_ROTATION_NOT_ALLOWED,
    UPDATE_RESERVATION_NOT_ALLOWED,
    INVALID_COMPANION_DELIVERY_OPTION_FOR_ROADBLOCKING_TYPE,
    INCONSISTENT_ROADBLOCK_TYPE,
    INVALID_UNITS_BOUGHT_BUFFER,
    UPDATE_COST_ADJUSTMENT_NOT_ALLOWED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProposalLineItemActionError : ApiError {
    private ProposalLineItemActionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ProposalLineItemActionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ProposalLineItemActionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProposalLineItemActionErrorReason {
    NOT_APPLICABLE,
    PROPOSAL_NOT_EDITABLE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProposalError : ApiError {
    private ProposalErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ProposalErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ProposalError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProposalErrorReason {
    AD_SERVER_UNKNOWN_ERROR,
    AD_SERVER_API_ERROR,
    UPDATE_ADVERTISER_NOT_ALLOWED,
    UPDATE_PROPOSAL_NOT_ALLOWED,
    INVALID_CONTACT,
    DUPLICATED_CONTACT,
    UNACCEPTABLE_COMPANY_CREDIT_STATUS,
    PRIMARY_AGENCY_REQUIRED,
    PRIMARY_AGENCY_NOT_UNIQUE,
    DUPLICATED_COMPANY_ASSOCIATION,
    DUPLICATED_SALESPERSON,
    DUPLICATED_SALES_PLANNER,
    DUPLICATED_TRAFFICKER,
    HAS_NO_UNARCHIVED_PROPOSAL_LINEITEMS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductError : ApiError {
    private ProductErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ProductErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ProductError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProductErrorReason {
    TEMPLATE_NOT_FOUND,
    MALFORMED_PRODUCT_ID,
    BAD_PRODUCT_ID_FEATURE,
    BAD_PRODUCT_TEMPLATE_ID,
    CANNOT_UPDATE_ARCHIVED_PRODUCT,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class PrecisionError : ApiError {
    private PrecisionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public PrecisionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PrecisionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum PrecisionErrorReason {
    WRONG_PRECISION,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "GeoTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum GeoTargetingErrorReason {
    TARGETED_LOCATIONS_NOT_EXCLUDABLE,
    EXCLUDED_LOCATIONS_CANNOT_HAVE_CHILDREN_TARGETED,
    POSTAL_CODES_CANNOT_BE_EXCLUDED,
    UNTARGETABLE_LOCATION,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class GenericTargetingError : ApiError {
    private GenericTargetingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public GenericTargetingErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "GenericTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum GenericTargetingErrorReason {
    CONFLICTING_INCLUSION_OR_EXCLUSION_OF_SIBLINGS,
    INCLUDING_DESCENDANTS_OF_EXCLUDED_CRITERIA,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "FrequencyCapError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum FrequencyCapErrorReason {
    IMPRESSION_LIMIT_EXCEEDED,
    IMPRESSIONS_TOO_LOW,
    RANGE_LIMIT_EXCEEDED,
    RANGE_TOO_LOW,
    DUPLICATE_TIME_RANGE,
    TOO_MANY_FREQUENCY_CAPS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "DayPartTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum DayPartTargetingErrorReason {
    INVALID_HOUR,
    INVALID_MINUTE,
    END_TIME_NOT_AFTER_START_TIME,
    TIME_PERIODS_OVERLAP,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomFieldValueError : ApiError {
    private CustomFieldValueErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CustomFieldValueErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomFieldValueError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomFieldValueErrorReason {
    CUSTOM_FIELD_NOT_FOUND,
    CUSTOM_FIELD_INACTIVE,
    CUSTOM_FIELD_OPTION_NOT_FOUND,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BillingError : ApiError {
    private BillingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public BillingErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "BillingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum BillingErrorReason {
    UNSUPPORTED_BILLING_SCHEDULE,
    UNSUPPORTED_BILLING_CAP,
    MISSING_BILLING_SOURCE,
    MISSING_BILLING_SCHEDULE,
    MISSING_BILLING_CAP,
    INVALID_BILLING_SOURCE_FOR_OFFLINE,
    UPDATE_BILLING_NOT_ALLOWED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ReportServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ReportService : DfpSoapClient, IReportService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ReportService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ReportService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual string getReportDownloadURL(long reportJobId, ExportFormat exportFormat) {
      object[] results = this.Invoke("getReportDownloadURL", new object[] { reportJobId, exportFormat });
      return ((string) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual string getReportDownloadUrlWithOptions(long reportJobId, ReportDownloadOptions reportDownloadOptions) {
      object[] results = this.Invoke("getReportDownloadUrlWithOptions", new object[] { reportJobId, reportDownloadOptions });
      return ((string) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReportJob getReportJob(long reportJobId) {
      object[] results = this.Invoke("getReportJob", new object[] { reportJobId });
      return ((ReportJob) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReportJob runReportJob(ReportJob reportJob) {
      object[] results = this.Invoke("runReportJob", new object[] { reportJob });
      return ((ReportJob) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReportQuery {
    private Dimension[] dimensionsField;

    private ReportQueryAdUnitView adUnitViewField;

    private bool adUnitViewFieldSpecified;

    private Column[] columnsField;

    private DimensionAttribute[] dimensionAttributesField;

    private long[] customFieldIdsField;

    private Date startDateField;

    private Date endDateField;

    private DateRangeType dateRangeTypeField;

    private bool dateRangeTypeFieldSpecified;

    private DimensionFilter[] dimensionFiltersField;

    private Statement statementField;

    private string timeZoneField;

    [System.Xml.Serialization.XmlElementAttribute("dimensions")]
    public Dimension[] dimensions {
      get { return this.dimensionsField; }
      set { this.dimensionsField = value; }
    }

    public ReportQueryAdUnitView adUnitView {
      get { return this.adUnitViewField; }
      set {
        this.adUnitViewField = value;
        this.adUnitViewSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool adUnitViewSpecified {
      get { return this.adUnitViewFieldSpecified; }
      set { this.adUnitViewFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("columns")]
    public Column[] columns {
      get { return this.columnsField; }
      set { this.columnsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("dimensionAttributes")]
    public DimensionAttribute[] dimensionAttributes {
      get { return this.dimensionAttributesField; }
      set { this.dimensionAttributesField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customFieldIds")]
    public long[] customFieldIds {
      get { return this.customFieldIdsField; }
      set { this.customFieldIdsField = value; }
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

    public Statement statement {
      get { return this.statementField; }
      set { this.statementField = value; }
    }

    public string timeZone {
      get { return this.timeZoneField; }
      set { this.timeZoneField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum Dimension {
    MONTH,
    WEEK,
    DATE,
    DAY,
    HOUR,
    LINE_ITEM_ID,
    LINE_ITEM_NAME,
    LINE_ITEM_TYPE,
    ORDER_ID,
    ORDER_NAME,
    ADVERTISER_ID,
    ADVERTISER_NAME,
    SALESPERSON_ID,
    SALESPERSON_NAME,
    CREATIVE_ID,
    CREATIVE_NAME,
    CREATIVE_TYPE,
    CREATIVE_SIZE,
    AD_UNIT_ID,
    AD_UNIT_NAME,
    PLACEMENT_ID,
    PLACEMENT_NAME,
    GENERIC_CRITERION_NAME,
    COUNTRY_CRITERIA_ID,
    COUNTRY_NAME,
    REGION_CRITERIA_ID,
    REGION_NAME,
    CITY_CRITERIA_ID,
    CITY_NAME,
    METRO_CRITERIA_ID,
    METRO_NAME,
    POSTAL_CODE_CRITERIA_ID,
    POSTAL_CODE,
    CUSTOM_TARGETING_VALUE_ID,
    CUSTOM_CRITERIA,
    ACTIVITY_ID,
    ACTIVITY_NAME,
    ACTIVITY_GROUP_ID,
    ACTIVITY_GROUP_NAME,
    CONTENT_ID,
    CONTENT_NAME,
    AD_REQUEST_AD_UNIT_SIZES,
    AD_REQUEST_CUSTOM_CRITERIA,
    MASTER_COMPANION_CREATIVE_ID,
    MASTER_COMPANION_CREATIVE_NAME,
    DISTRIBUTION_PARTNER_ID,
    DISTRIBUTION_PARTNER_NAME,
    CONTENT_PARTNER_ID,
    CONTENT_PARTNER_NAME,
    RIGHTS_HOLDER_ID,
    RIGHTS_HOLDER_NAME,
    PROPOSAL_LINE_ITEM_ID,
    PROPOSAL_LINE_ITEM_NAME,
    PROPOSAL_ID,
    PROPOSAL_NAME,
    PROPOSAL_ADVERTISER_ID,
    PROPOSAL_ADVERTISER_NAME,
    PROPOSAL_AGENCY_ID,
    PROPOSAL_AGENCY_NAME,
    PRODUCT_ID,
    PRODUCT_NAME,
    PRODUCT_TEMPLATE_ID,
    PRODUCT_TEMPLATE_NAME,
    AUDIENCE_SEGMENT_ID,
    AUDIENCE_SEGMENT_NAME
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ReportQuery.AdUnitView", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ReportQueryAdUnitView {
    TOP_LEVEL,
    FLAT,
    HIERARCHICAL
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum Column {
    AD_SERVER_IMPRESSIONS,
    AD_SERVER_CLICKS,
    AD_SERVER_CTR,
    AD_SERVER_CPM_AND_CPC_REVENUE,
    AD_SERVER_CPD_REVENUE,
    AD_SERVER_ALL_REVENUE,
    AD_SERVER_WITHOUT_CPD_AVERAGE_ECPM,
    AD_SERVER_WITH_CPD_AVERAGE_ECPM,
    AD_SERVER_INVENTORY_LEVEL_PERCENT_IMPRESSIONS,
    AD_SERVER_LINE_ITEM_LEVEL_PERCENT_IMPRESSIONS,
    AD_SERVER_INVENTORY_LEVEL_PERCENT_CLICKS,
    AD_SERVER_LINE_ITEM_LEVEL_PERCENT_CLICKS,
    AD_SERVER_INVENTORY_LEVEL_WITHOUT_CPD_PERCENT_REVENUE,
    AD_SERVER_INVENTORY_LEVEL_WITH_CPD_PERCENT_REVENUE,
    AD_SERVER_LINE_ITEM_LEVEL_WITHOUT_CPD_PERCENT_REVENUE,
    AD_SERVER_LINE_ITEM_LEVEL_WITH_CPD_PERCENT_REVENUE,
    AD_SERVER_DELIVERY_INDICATOR,
    DYNAMIC_ALLOCATION_INVENTORY_LEVEL_IMPRESSIONS,
    ADSENSE_LINE_ITEM_LEVEL_IMPRESSIONS,
    DYNAMIC_ALLOCATION_INVENTORY_LEVEL_CLICKS,
    ADSENSE_LINE_ITEM_LEVEL_CLICKS,
    DYNAMIC_ALLOCATION_INVENTORY_LEVEL_CTR,
    ADSENSE_LINE_ITEM_LEVEL_CTR,
    DYNAMIC_ALLOCATION_INVENTORY_LEVEL_REVENUE,
    ADSENSE_LINE_ITEM_LEVEL_REVENUE,
    DYNAMIC_ALLOCATION_INVENTORY_LEVEL_AVERAGE_ECPM,
    ADSENSE_LINE_ITEM_LEVEL_AVERAGE_ECPM,
    DYNAMIC_ALLOCATION_INVENTORY_LEVEL_PERCENT_IMPRESSIONS,
    ADSENSE_LINE_ITEM_LEVEL_PERCENT_IMPRESSIONS,
    DYNAMIC_ALLOCATION_INVENTORY_LEVEL_PERCENT_CLICKS,
    ADSENSE_LINE_ITEM_LEVEL_PERCENT_CLICKS,
    DYNAMIC_ALLOCATION_INVENTORY_LEVEL_WITHOUT_CPD_PERCENT_REVENUE,
    DYNAMIC_ALLOCATION_INVENTORY_LEVEL_WITH_CPD_PERCENT_REVENUE,
    ADSENSE_LINE_ITEM_LEVEL_WITHOUT_CPD_PERCENT_REVENUE,
    ADSENSE_LINE_ITEM_LEVEL_WITH_CPD_PERCENT_REVENUE,
    AD_EXCHANGE_LINE_ITEM_LEVEL_IMPRESSIONS,
    AD_EXCHANGE_LINE_ITEM_LEVEL_CLICKS,
    AD_EXCHANGE_LINE_ITEM_LEVEL_CTR,
    AD_EXCHANGE_LINE_ITEM_LEVEL_PERCENT_IMPRESSIONS,
    AD_EXCHANGE_LINE_ITEM_LEVEL_PERCENT_CLICKS,
    AD_EXCHANGE_LINE_ITEM_LEVEL_REVENUE,
    AD_EXCHANGE_LINE_ITEM_LEVEL_WITHOUT_CPD_PERCENT_REVENUE,
    AD_EXCHANGE_LINE_ITEM_LEVEL_WITH_CPD_PERCENT_REVENUE,
    AD_EXCHANGE_LINE_ITEM_LEVEL_AVERAGE_ECPM,
    TOTAL_INVENTORY_LEVEL_IMPRESSIONS,
    TOTAL_LINE_ITEM_LEVEL_IMPRESSIONS,
    TOTAL_INVENTORY_LEVEL_CLICKS,
    TOTAL_LINE_ITEM_LEVEL_CLICKS,
    TOTAL_INVENTORY_LEVEL_CTR,
    TOTAL_LINE_ITEM_LEVEL_CTR,
    TOTAL_INVENTORY_LEVEL_CPM_AND_CPC_REVENUE,
    TOTAL_INVENTORY_LEVEL_ALL_REVENUE,
    TOTAL_LINE_ITEM_LEVEL_CPM_AND_CPC_REVENUE,
    TOTAL_LINE_ITEM_LEVEL_ALL_REVENUE,
    TOTAL_INVENTORY_LEVEL_WITHOUT_CPD_AVERAGE_ECPM,
    TOTAL_INVENTORY_LEVEL_WITH_CPD_AVERAGE_ECPM,
    TOTAL_LINE_ITEM_LEVEL_WITHOUT_CPD_AVERAGE_ECPM,
    TOTAL_LINE_ITEM_LEVEL_WITH_CPD_AVERAGE_ECPM,
    TOTAL_INVENTORY_LEVEL_UNFILLED_IMPRESSIONS,
    MERGED_AD_SERVER_DELIVERY_INDICATOR,
    MERGED_AD_SERVER_IMPRESSIONS,
    MERGED_AD_SERVER_CLICKS,
    MERGED_AD_SERVER_CTR,
    MERGED_AD_SERVER_CPM_AND_CPC_REVENUE,
    MERGED_AD_SERVER_ALL_REVENUE,
    MERGED_AD_SERVER_WITHOUT_CPD_AVERAGE_ECPM,
    MERGED_AD_SERVER_WITH_CPD_AVERAGE_ECPM,
    OPTIMIZATION_CONTROL_IMPRESSIONS,
    OPTIMIZATION_CONTROL_CLICKS,
    OPTIMIZATION_CONTROL_CTR,
    OPTIMIZATION_OPTIMIZED_IMPRESSIONS,
    OPTIMIZATION_OPTIMIZED_CLICKS,
    OPTIMIZATION_NON_OPTIMIZED_IMPRESSIONS,
    OPTIMIZATION_NON_OPTIMIZED_CLICKS,
    OPTIMIZATION_EXTRA_CLICKS,
    OPTIMIZATION_OPTIMIZED_CTR,
    OPTIMIZATION_LIFT,
    OPTIMIZATION_COVERAGE,
    OPTIMIZATION_BEHIND_SCHEDULE_IMPRESSIONS,
    OPTIMIZATION_NO_CLICKS_RECORDED_IMPRESSIONS,
    OPTIMIZATION_SPONSORSHIP_IMPRESSIONS,
    OPTIMIZATION_AS_FAST_AS_POSSIBLE_IMPRESSIONS,
    OPTIMIZATION_NO_ABSOLUTE_LIFETIME_GOAL_IMPRESSIONS,
    OPTIMIZATION_CONTROL_REVENUE,
    OPTIMIZATION_OPTIMIZED_REVENUE,
    OPTIMIZATION_CONTROL_ECPM,
    OPTIMIZATION_OPTIMIZED_ECPM,
    OPTIMIZATION_FREED_UP_IMPRESSIONS,
    OPTIMIZATION_ECPM_LIFT,
    REACH_FREQUENCY,
    REACH_AVERAGE_REVENUE,
    REACH,
    SELL_THROUGH_FORECASTED_IMPRESSIONS,
    SELL_THROUGH_AVAILABLE_IMPRESSIONS,
    SELL_THROUGH_RESERVED_IMPRESSIONS,
    SELL_THROUGH_SELL_THROUGH_RATE,
    RICH_MEDIA_BACKUP_IMAGES,
    RICH_MEDIA_DISPLAY_TIME,
    RICH_MEDIA_AVERAGE_DISPLAY_TIME,
    RICH_MEDIA_EXPANSIONS,
    RICH_MEDIA_EXPANDING_TIME,
    RICH_MEDIA_INTERACTION_TIME,
    RICH_MEDIA_INTERACTION_COUNT,
    RICH_MEDIA_INTERACTION_RATE,
    RICH_MEDIA_AVERAGE_INTERACTION_TIME,
    RICH_MEDIA_INTERACTION_IMPRESSIONS,
    RICH_MEDIA_MANUAL_CLOSES,
    RICH_MEDIA_FULL_SCREEN_IMPRESSIONS,
    RICH_MEDIA_VIDEO_INTERACTIONS,
    RICH_MEDIA_VIDEO_INTERACTION_RATE,
    RICH_MEDIA_VIDEO_MUTES,
    RICH_MEDIA_VIDEO_PAUSES,
    RICH_MEDIA_VIDEO_PLAYES,
    RICH_MEDIA_VIDEO_MIDPOINTS,
    RICH_MEDIA_VIDEO_COMPLETES,
    RICH_MEDIA_VIDEO_REPLAYS,
    RICH_MEDIA_VIDEO_STOPS,
    RICH_MEDIA_VIDEO_UNMUTES,
    RICH_MEDIA_VIDEO_VIEW_TIME,
    RICH_MEDIA_VIDEO_VIEW_RATE,
    RICH_MEDIA_CUSTOM_CONVERSION_TIME_VALUE,
    RICH_MEDIA_CUSTOM_CONVERSION_COUNT_VALUE,
    VIDEO_INTERACTION_START,
    VIDEO_INTERACTION_FIRST_QUARTILE,
    VIDEO_INTERACTION_MIDPOINT,
    VIDEO_INTERACTION_THIRD_QUARTILE,
    VIDEO_INTERACTION_COMPLETE,
    VIDEO_INTERACTION_VIEW_RATE,
    VIDEO_INTERACTION_AVERAGE_VIEW_RATE,
    VIDEO_INTERACTION_COMPLETION_RATE,
    VIDEO_INTERACTION_ERROR_COUNT,
    VIDEO_INTERACTION_VIDEO_LENGTH,
    VIDEO_INTERACTION_PAUSE,
    VIDEO_INTERACTION_RESUME,
    VIDEO_INTERACTION_REWIND,
    VIDEO_INTERACTION_MUTE,
    VIDEO_INTERACTION_UNMUTE,
    VIDEO_INTERACTION_COLLAPSE,
    VIDEO_INTERACTION_EXPAND,
    VIDEO_INTERACTION_FULL_SCREEN,
    VIDEO_INTERACTION_AVERAGE_INTERACTION_RATE,
    VIDEO_INTERACTION_VIDEO_SKIPS,
    VIDEO_INTERACTION_VIDEO_SKIP_SHOWN,
    VIDEO_INTERACTION_ENGAGED_VIEW,
    VIDEO_INTERACTION_VIEW_THROUGH_RATE,
    DISTRIBUTION_PARTNER_IMPRESSIONS,
    DISTRIBUTION_PARTNER_CLICKS,
    DISTRIBUTION_PARTNER_CTR,
    DISTRIBUTION_PARTNER_GROSS_REVENUE,
    DISTRIBUTION_PARTNER_HOST_REVENUE,
    DISTRIBUTION_PARTNER_HOST_ECPM,
    DISTRIBUTION_PARTNER_PARTNER_REVENUE,
    DISTRIBUTION_PARTNER_PARTNER_ECPM,
    CONTENT_PARTNER_IMPRESSIONS,
    CONTENT_PARTNER_CLICKS,
    CONTENT_PARTNER_CTR,
    CONTENT_PARTNER_GROSS_REVENUE,
    CONTENT_PARTNER_HOST_REVENUE,
    CONTENT_PARTNER_HOST_ECPM,
    CONTENT_PARTNER_PARTNER_REVENUE,
    CONTENT_PARTNER_PARTNER_ECPM,
    RIGHTS_HOLDER_IMPRESSIONS,
    RIGHTS_HOLDER_CLICKS,
    RIGHTS_HOLDER_CTR,
    RIGHTS_HOLDER_GROSS_REVENUE,
    RIGHTS_HOLDER_HOST_REVENUE,
    RIGHTS_HOLDER_HOST_ECPM,
    RIGHTS_HOLDER_PARTNER_REVENUE,
    RIGHTS_HOLDER_PARTNER_ECPM,
    VIEW_THROUGH_CONVERSIONS,
    CONVERSIONS_PER_THOUSAND_IMPRESSIONS,
    CLICK_THROUGH_CONVERSIONS,
    CONVERSIONS_PER_CLICK,
    VIEW_THROUGH_REVENUE,
    CLICK_THROUGH_REVENUE,
    TOTAL_CONVERSIONS,
    TOTAL_CONVERSION_REVENUE,
    SALES_DFP_REVENUE,
    SALES_FORECASTED_NET_REVENUE,
    SALES_CONTRACTED_NET_REVENUE,
    SALES_CONTRACTED_PLACEMENT_NET_REVENUE,
    SALES_CONTRACTED_IMPRESSIONS,
    SALES_CONTRACTED_CLICKS,
    SALES_CONTRACTED_VOLUME,
    SALES_BUDGET,
    SALES_REMAINING_BUDGET,
    SALES_UNIFIED_NET_REVENUE,
    SALES_PIPELINE_NET_REVENUE,
    BILLING_AND_RECONCILIATION_LAST_DATE_TIME,
    BILLING_AND_RECONCILIATION_RECONCILIATION_STATUS,
    BILLING_AND_RECONCILIATION_DFP_VOLUME,
    BILLING_AND_RECONCILIATION_THIRD_PARTY_VOLUME,
    BILLING_AND_RECONCILIATION_RECONCILED_VOLUME,
    BILLING_AND_RECONCILIATION_BILLABLE_NET_REVENUE,
    SALES_ADDITIONAL_ADJUSTMENT
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum DimensionAttribute {
    LINE_ITEM_LABELS,
    LINE_ITEM_LABEL_IDS,
    LINE_ITEM_OPTIMIZABLE,
    LINE_ITEM_DELIVERY_PACING,
    LINE_ITEM_FREQUENCY_CAP,
    ADVERTISER_EXTERNAL_ID,
    ORDER_START_DATE_TIME,
    ORDER_END_DATE_TIME,
    ORDER_EXTERNAL_ID,
    ORDER_PO_NUMBER,
    ORDER_AGENCY,
    ORDER_AGENCY_ID,
    ORDER_LABELS,
    ORDER_LABEL_IDS,
    ORDER_TRAFFICKER,
    ORDER_SECONDARY_TRAFFICKERS,
    ORDER_SALESPERSON,
    ORDER_SECONDARY_SALESPEOPLE,
    ORDER_LIFETIME_IMPRESSIONS,
    ORDER_LIFETIME_CLICKS,
    ORDER_LIFETIME_MERGED_IMPRESSIONS,
    ORDER_LIFETIME_MERGED_CLICKS,
    ORDER_BOOKED_CPM,
    ORDER_BOOKED_CPC,
    LINE_ITEM_START_DATE_TIME,
    LINE_ITEM_END_DATE_TIME,
    LINE_ITEM_EXTERNAL_ID,
    LINE_ITEM_COST_TYPE,
    LINE_ITEM_COST_PER_UNIT,
    LINE_ITEM_CURRENCY_CODE,
    LINE_ITEM_GOAL_QUANTITY,
    LINE_ITEM_SPONSORSHIP_GOAL_PERCENTAGE,
    LINE_ITEM_LIFETIME_IMPRESSIONS,
    LINE_ITEM_LIFETIME_CLICKS,
    LINE_ITEM_LIFETIME_MERGED_IMPRESSIONS,
    LINE_ITEM_LIFETIME_MERGED_CLICKS,
    LINE_ITEM_PRIORITY,
    CREATIVE_OR_CREATIVE_SET,
    MASTER_COMPANION_TYPE,
    LINE_ITEM_CONTRACTED_QUANTITY,
    LINE_ITEM_DISCOUNT,
    LINE_ITEM_NON_CPD_BOOKED_REVENUE,
    ADVERTISER_LABELS,
    ADVERTISER_LABEL_IDS,
    CREATIVE_CLICK_THROUGH_URL,
    LINE_ITEM_CREATIVE_START_DATE,
    LINE_ITEM_CREATIVE_END_DATE,
    PROPOSAL_START_DATE_TIME,
    PROPOSAL_END_DATE_TIME,
    PROPOSAL_CREATION_DATE_TIME,
    PROPOSAL_PROBABILITY_TO_CLOSE,
    PROPOSAL_STATUS,
    PROPOSAL_CURRENCY,
    PROPOSAL_AGENCY_COMMISSION_RATE,
    PROPOSAL_VAT_RATE,
    PROPOSAL_BILLING_SOURCE,
    PROPOSAL_BILLING_CAP,
    PROPOSAL_BILLING_SCHEDULE,
    PROPOSAL_LINE_ITEM_START_DATE_TIME,
    PROPOSAL_LINE_ITEM_END_DATE_TIME,
    PROPOSAL_LINE_ITEM_RATE_TYPE,
    PROPOSAL_LINE_ITEM_COST_PER_UNIT,
    PROPOSAL_LINE_ITEM_PRODUCT_TYPE,
    AD_UNIT_CODE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum DateRangeType {
    TODAY,
    YESTERDAY,
    LAST_WEEK,
    LAST_MONTH,
    REACH_LIFETIME,
    CUSTOM_DATE,
    NEXT_DAY,
    NEXT_90_DAYS,
    NEXT_WEEK,
    NEXT_MONTH,
    CURRENT_AND_NEXT_MONTH,
    NEXT_QUARTER,
    NEXT_3_MONTHS,
    NEXT_12_MONTHS
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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
    MOBILE_LINE_ITEMS,
    WEB_LINE_ITEMS,
    ACTIVE_AD_UNITS,
    INACTIVE_AD_UNITS,
    MOBILE_INVENTORY_UNITS,
    WEB_INVENTORY_UNITS,
    ARCHIVED_AD_UNITS,
    ACTIVE_PLACEMENTS,
    INACTIVE_PLACEMENTS,
    ARCHIVED_PLACEMENTS,
    OPTIMIZABLE_ORDERS,
    PARTNER_STATS_TYPE_ESTIMATED,
    PARTNER_STATS_TYPE_RECONCILED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ReportJobStatus {
    COMPLETED,
    IN_PROGRESS,
    FAILED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReportDownloadOptions {
    private ExportFormat exportFormatField;

    private bool exportFormatFieldSpecified;

    private bool includeReportPropertiesField;

    private bool includeReportPropertiesFieldSpecified;

    private bool includeTotalsRowField;

    private bool includeTotalsRowFieldSpecified;

    private bool useGzipCompressionField;

    private bool useGzipCompressionFieldSpecified;

    public ExportFormat exportFormat {
      get { return this.exportFormatField; }
      set {
        this.exportFormatField = value;
        this.exportFormatSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool exportFormatSpecified {
      get { return this.exportFormatFieldSpecified; }
      set { this.exportFormatFieldSpecified = value; }
    }

    public bool includeReportProperties {
      get { return this.includeReportPropertiesField; }
      set {
        this.includeReportPropertiesField = value;
        this.includeReportPropertiesSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool includeReportPropertiesSpecified {
      get { return this.includeReportPropertiesFieldSpecified; }
      set { this.includeReportPropertiesFieldSpecified = value; }
    }

    public bool includeTotalsRow {
      get { return this.includeTotalsRowField; }
      set {
        this.includeTotalsRowField = value;
        this.includeTotalsRowSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool includeTotalsRowSpecified {
      get { return this.includeTotalsRowFieldSpecified; }
      set { this.includeTotalsRowFieldSpecified = value; }
    }

    public bool useGzipCompression {
      get { return this.useGzipCompressionField; }
      set {
        this.useGzipCompressionField = value;
        this.useGzipCompressionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool useGzipCompressionSpecified {
      get { return this.useGzipCompressionFieldSpecified; }
      set { this.useGzipCompressionFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ExportFormat {
    TSV,
    CSV_EXCEL,
    CSV_DUMP,
    XML,
    XLS,
    XLSX
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ReportError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ReportErrorReason {
    DEFAULT,
    REPORT_ACCESS_NOT_ALLOWED,
    DIMENSION_VIEW_NOT_ALLOWED,
    DIMENSION_COMBINATION_NOT_ALLOWED,
    ATTRIBUTE_VIEW_NOT_ALLOWED,
    COLUMN_VIEW_NOT_ALLOWED,
    TOO_MANY_CONCURRENT_REPORTS,
    REPORT_TOO_BIG,
    INVALID_OPERATION_FOR_REPORT_STATE,
    INVALID_DIMENSIONS,
    INVALID_ATTRIBUTES,
    INVALID_COLUMNS,
    INVALID_DIMENSION_FILTERS,
    INVALID_DATE,
    END_DATE_TIME_NOT_AFTER_START_TIME,
    NOT_NULL,
    ATTRIBUTES_NOT_SUPPORTED_FOR_REQUEST,
    COLUMNS_NOT_SUPPORTED_FOR_REQUESTED_DIMENSIONS,
    FAILED_TO_STORE_REPORT,
    REPORT_NOT_FOUND,
    SR_CANNOT_RUN_REPORT_IN_ANOTHER_NETWORK,
    INVALID_TIME_ZONE_FOR_FEATURE_NOT_ENABLED,
    INVALID_TIME_ZONE_FOR_ID,
    UNSUPPORTED_TIME_ZONE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ActivityGroupServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ActivityGroupService : DfpSoapClient, IActivityGroupService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ActivityGroupService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ActivityGroupService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ActivityGroup createActivityGroup(ActivityGroup activityGroup) {
      object[] results = this.Invoke("createActivityGroup", new object[] { activityGroup });
      return ((ActivityGroup) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ActivityGroup[] createActivityGroups([System.Xml.Serialization.XmlElementAttribute("activityGroups")]
ActivityGroup[] activityGroups) {
      object[] results = this.Invoke("createActivityGroups", new object[] { activityGroups });
      return ((ActivityGroup[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ActivityGroup getActivityGroup(int activityGroupId) {
      object[] results = this.Invoke("getActivityGroup", new object[] { activityGroupId });
      return ((ActivityGroup) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ActivityGroupPage getActivityGroupsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getActivityGroupsByStatement", new object[] { filterStatement });
      return ((ActivityGroupPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ActivityGroup updateActivityGroup(ActivityGroup activityGroup) {
      object[] results = this.Invoke("updateActivityGroup", new object[] { activityGroup });
      return ((ActivityGroup) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ActivityGroup[] updateActivityGroups([System.Xml.Serialization.XmlElementAttribute("activityGroups")]
ActivityGroup[] activityGroups) {
      object[] results = this.Invoke("updateActivityGroups", new object[] { activityGroups });
      return ((ActivityGroup[]) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivityGroupPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private ActivityGroup[] resultsField;

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
    public ActivityGroup[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivityGroup {
    private int idField;

    private bool idFieldSpecified;

    private string nameField;

    private long[] companyIdsField;

    private int impressionsLookbackField;

    private bool impressionsLookbackFieldSpecified;

    private int clicksLookbackField;

    private bool clicksLookbackFieldSpecified;

    private ActivityGroupStatus statusField;

    private bool statusFieldSpecified;

    public int id {
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

    [System.Xml.Serialization.XmlElementAttribute("companyIds")]
    public long[] companyIds {
      get { return this.companyIdsField; }
      set { this.companyIdsField = value; }
    }

    public int impressionsLookback {
      get { return this.impressionsLookbackField; }
      set {
        this.impressionsLookbackField = value;
        this.impressionsLookbackSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool impressionsLookbackSpecified {
      get { return this.impressionsLookbackFieldSpecified; }
      set { this.impressionsLookbackFieldSpecified = value; }
    }

    public int clicksLookback {
      get { return this.clicksLookbackField; }
      set {
        this.clicksLookbackField = value;
        this.clicksLookbackSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool clicksLookbackSpecified {
      get { return this.clicksLookbackFieldSpecified; }
      set { this.clicksLookbackFieldSpecified = value; }
    }

    public ActivityGroupStatus status {
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
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ActivityGroup.Status", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ActivityGroupStatus {
    ACTIVE,
    INACTIVE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivityError : ApiError {
    private ActivityErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ActivityErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ActivityError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ActivityErrorReason {
    ACTIVITIES_FEATURE_REQUIRED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "LineItemServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemSummary))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class LineItemService : DfpSoapClient, ILineItemService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public LineItemService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/LineItemService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItem createLineItem(LineItem lineItem) {
      object[] results = this.Invoke("createLineItem", new object[] { lineItem });
      return ((LineItem) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItem[] createLineItems([System.Xml.Serialization.XmlElementAttribute("lineItems")]
LineItem[] lineItems) {
      object[] results = this.Invoke("createLineItems", new object[] { lineItems });
      return ((LineItem[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItem getLineItem(long lineItemId) {
      object[] results = this.Invoke("getLineItem", new object[] { lineItemId });
      return ((LineItem) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItemPage getLineItemsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getLineItemsByStatement", new object[] { filterStatement });
      return ((LineItemPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performLineItemAction(LineItemAction lineItemAction, Statement filterStatement) {
      object[] results = this.Invoke("performLineItemAction", new object[] { lineItemAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItem updateLineItem(LineItem lineItem) {
      object[] results = this.Invoke("updateLineItem", new object[] { lineItem });
      return ((LineItem) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItem[] updateLineItems([System.Xml.Serialization.XmlElementAttribute("lineItems")]
LineItem[] lineItems) {
      object[] results = this.Invoke("updateLineItems", new object[] { lineItems });
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
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveLineItems))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateLineItems))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class LineItemAction {
    private string lineItemActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("LineItemAction.Type")]
    public string LineItemActionType {
      get { return this.lineItemActionTypeField; }
      set { this.lineItemActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UnarchiveLineItems : LineItemAction {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeAndOverbookLineItems))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ResumeLineItems : LineItemAction {
    private bool skipInventoryCheckField;

    private bool skipInventoryCheckFieldSpecified;

    public bool skipInventoryCheck {
      get { return this.skipInventoryCheckField; }
      set {
        this.skipInventoryCheckField = value;
        this.skipInventoryCheckSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool skipInventoryCheckSpecified {
      get { return this.skipInventoryCheckFieldSpecified; }
      set { this.skipInventoryCheckFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ResumeAndOverbookLineItems : ResumeLineItems {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReserveAndOverbookLineItems))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReserveLineItems : LineItemAction {
    private bool skipInventoryCheckField;

    private bool skipInventoryCheckFieldSpecified;

    public bool skipInventoryCheck {
      get { return this.skipInventoryCheckField; }
      set {
        this.skipInventoryCheckField = value;
        this.skipInventoryCheckSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool skipInventoryCheckSpecified {
      get { return this.skipInventoryCheckFieldSpecified; }
      set { this.skipInventoryCheckFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReserveAndOverbookLineItems : ReserveLineItems {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReleaseLineItems : LineItemAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class PauseLineItems : LineItemAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeleteLineItems : LineItemAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ArchiveLineItems : LineItemAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateLineItems : LineItemAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LineItem : LineItemSummary {
    private Targeting targetingField;

    public Targeting targeting {
      get { return this.targetingField; }
      set { this.targetingField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItem))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LineItemSummary {
    private long orderIdField;

    private bool orderIdFieldSpecified;

    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string externalIdField;

    private string orderNameField;

    private DateTime startDateTimeField;

    private StartDateTimeType startDateTimeTypeField;

    private bool startDateTimeTypeFieldSpecified;

    private DateTime endDateTimeField;

    private int autoExtensionDaysField;

    private bool autoExtensionDaysFieldSpecified;

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

    private int priorityField;

    private bool priorityFieldSpecified;

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

    private long contractedUnitsBoughtField;

    private bool contractedUnitsBoughtFieldSpecified;

    private CreativePlaceholder[] creativePlaceholdersField;

    private TargetPlatform targetPlatformField;

    private bool targetPlatformFieldSpecified;

    private EnvironmentType environmentTypeField;

    private bool environmentTypeFieldSpecified;

    private CompanionDeliveryOption companionDeliveryOptionField;

    private bool companionDeliveryOptionFieldSpecified;

    private CreativePersistenceType creativePersistenceTypeField;

    private bool creativePersistenceTypeFieldSpecified;

    private bool allowOverbookField;

    private bool allowOverbookFieldSpecified;

    private bool skipInventoryCheckField;

    private bool skipInventoryCheckFieldSpecified;

    private bool reserveAtCreationField;

    private bool reserveAtCreationFieldSpecified;

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

    private string webPropertyCodeField;

    private AppliedLabel[] appliedLabelsField;

    private AppliedLabel[] effectiveAppliedLabelsField;

    private bool disableSameAdvertiserCompetitiveExclusionField;

    private bool disableSameAdvertiserCompetitiveExclusionFieldSpecified;

    private string lastModifiedByAppField;

    private string notesField;

    private DateTime lastModifiedDateTimeField;

    private DateTime creationDateTimeField;

    private BaseCustomFieldValue[] customFieldValuesField;

    private bool isMissingCreativesField;

    private bool isMissingCreativesFieldSpecified;

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

    public string externalId {
      get { return this.externalIdField; }
      set { this.externalIdField = value; }
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

    public int autoExtensionDays {
      get { return this.autoExtensionDaysField; }
      set {
        this.autoExtensionDaysField = value;
        this.autoExtensionDaysSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool autoExtensionDaysSpecified {
      get { return this.autoExtensionDaysFieldSpecified; }
      set { this.autoExtensionDaysFieldSpecified = value; }
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

    public int priority {
      get { return this.priorityField; }
      set {
        this.priorityField = value;
        this.prioritySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool prioritySpecified {
      get { return this.priorityFieldSpecified; }
      set { this.priorityFieldSpecified = value; }
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

    public long contractedUnitsBought {
      get { return this.contractedUnitsBoughtField; }
      set {
        this.contractedUnitsBoughtField = value;
        this.contractedUnitsBoughtSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool contractedUnitsBoughtSpecified {
      get { return this.contractedUnitsBoughtFieldSpecified; }
      set { this.contractedUnitsBoughtFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("creativePlaceholders")]
    public CreativePlaceholder[] creativePlaceholders {
      get { return this.creativePlaceholdersField; }
      set { this.creativePlaceholdersField = value; }
    }

    public TargetPlatform targetPlatform {
      get { return this.targetPlatformField; }
      set {
        this.targetPlatformField = value;
        this.targetPlatformSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool targetPlatformSpecified {
      get { return this.targetPlatformFieldSpecified; }
      set { this.targetPlatformFieldSpecified = value; }
    }

    public EnvironmentType environmentType {
      get { return this.environmentTypeField; }
      set {
        this.environmentTypeField = value;
        this.environmentTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool environmentTypeSpecified {
      get { return this.environmentTypeFieldSpecified; }
      set { this.environmentTypeFieldSpecified = value; }
    }

    public CompanionDeliveryOption companionDeliveryOption {
      get { return this.companionDeliveryOptionField; }
      set {
        this.companionDeliveryOptionField = value;
        this.companionDeliveryOptionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool companionDeliveryOptionSpecified {
      get { return this.companionDeliveryOptionFieldSpecified; }
      set { this.companionDeliveryOptionFieldSpecified = value; }
    }

    public CreativePersistenceType creativePersistenceType {
      get { return this.creativePersistenceTypeField; }
      set {
        this.creativePersistenceTypeField = value;
        this.creativePersistenceTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool creativePersistenceTypeSpecified {
      get { return this.creativePersistenceTypeFieldSpecified; }
      set { this.creativePersistenceTypeFieldSpecified = value; }
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

    public bool skipInventoryCheck {
      get { return this.skipInventoryCheckField; }
      set {
        this.skipInventoryCheckField = value;
        this.skipInventoryCheckSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool skipInventoryCheckSpecified {
      get { return this.skipInventoryCheckFieldSpecified; }
      set { this.skipInventoryCheckFieldSpecified = value; }
    }

    public bool reserveAtCreation {
      get { return this.reserveAtCreationField; }
      set {
        this.reserveAtCreationField = value;
        this.reserveAtCreationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reserveAtCreationSpecified {
      get { return this.reserveAtCreationFieldSpecified; }
      set { this.reserveAtCreationFieldSpecified = value; }
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

    public string webPropertyCode {
      get { return this.webPropertyCodeField; }
      set { this.webPropertyCodeField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedLabels")]
    public AppliedLabel[] appliedLabels {
      get { return this.appliedLabelsField; }
      set { this.appliedLabelsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("effectiveAppliedLabels")]
    public AppliedLabel[] effectiveAppliedLabels {
      get { return this.effectiveAppliedLabelsField; }
      set { this.effectiveAppliedLabelsField = value; }
    }

    public bool disableSameAdvertiserCompetitiveExclusion {
      get { return this.disableSameAdvertiserCompetitiveExclusionField; }
      set {
        this.disableSameAdvertiserCompetitiveExclusionField = value;
        this.disableSameAdvertiserCompetitiveExclusionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool disableSameAdvertiserCompetitiveExclusionSpecified {
      get { return this.disableSameAdvertiserCompetitiveExclusionFieldSpecified; }
      set { this.disableSameAdvertiserCompetitiveExclusionFieldSpecified = value; }
    }

    public string lastModifiedByApp {
      get { return this.lastModifiedByAppField; }
      set { this.lastModifiedByAppField = value; }
    }

    public string notes {
      get { return this.notesField; }
      set { this.notesField = value; }
    }

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }

    public DateTime creationDateTime {
      get { return this.creationDateTimeField; }
      set { this.creationDateTimeField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customFieldValues")]
    public BaseCustomFieldValue[] customFieldValues {
      get { return this.customFieldValuesField; }
      set { this.customFieldValuesField = value; }
    }

    public bool isMissingCreatives {
      get { return this.isMissingCreativesField; }
      set {
        this.isMissingCreativesField = value;
        this.isMissingCreativesSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isMissingCreativesSpecified {
      get { return this.isMissingCreativesFieldSpecified; }
      set { this.isMissingCreativesFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("LineItemSummary.Type")]
    public string LineItemSummaryType {
      get { return this.lineItemSummaryTypeField; }
      set { this.lineItemSummaryTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum UnitType {
    IMPRESSIONS,
    CLICKS
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemSummary.Duration", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemSummaryDuration {
    NONE,
    LIFETIME,
    DAILY
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemDiscountType {
    ABSOLUTE_VALUE,
    PERCENTAGE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativePersistenceType {
    NOT_PERSISTENT,
    PERSISTENT_AND_EXCLUDE_NONE,
    PERSISTENT_AND_EXCLUDE_DISPLAY,
    PERSISTENT_AND_EXCLUDE_VIDEO,
    PERSISTENT_AND_EXCLUDE_ALL
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemSummary.ReservationStatus", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemSummaryReservationStatus {
    RESERVED,
    UNRESERVED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RequiredSizeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RequiredSizeErrorReason {
    REQUIRED,
    NOT_ALLOWED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "OrderError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum OrderErrorReason {
    UPDATE_CANCELED_ORDER_NOT_ALLOWED,
    UPDATE_PENDING_APPROVAL_ORDER_NOT_ALLOWED,
    UPDATE_ARCHIVED_ORDER_NOT_ALLOWED,
    CANNOT_MODIFY_PROPOSAL_ID,
    PRIMARY_USER_REQUIRED,
    PRIMARY_USER_CANNOT_BE_SECONDARY,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemOperationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemOperationErrorReason {
    NOT_ALLOWED,
    NOT_APPLICABLE,
    HAS_COMPLETED,
    HAS_NO_ACTIVE_CREATIVES,
    CANNOT_ACTIVATE_LEGACY_DFP_LINE_ITEM,
    CANNOT_DELETE_DELIVERED_LINE_ITEM,
    CANNOT_RESERVE_COMPANY_CREDIT_STATUS_NOT_ACTIVE,
    CANNOT_ACTIVATE_INVALID_COMPANY_CREDIT_STATUS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemFlightDateError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemFlightDateErrorReason {
    START_DATE_TIME_IS_IN_PAST,
    END_DATE_TIME_IS_IN_PAST,
    END_DATE_TIME_NOT_AFTER_START_TIME,
    END_DATE_TIME_TOO_LATE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LineItemError : ApiError {
    private LineItemErrorReason reasonField;

    private bool reasonFieldSpecified;

    public LineItemErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemErrorReason {
    ALREADY_STARTED,
    UPDATE_RESERVATION_NOT_ALLOWED,
    ALL_ROADBLOCK_NOT_ALLOWED,
    CREATIVE_SET_ROADBLOCK_NOT_ALLOWED,
    FRACTIONAL_PERCENTAGE_NOT_ALLOWED,
    DISCOUNT_NOT_ALLOWED,
    UPDATE_CANCELED_LINE_ITEM_NOT_ALLOWED,
    UPDATE_PENDING_APPROVAL_LINE_ITEM_NOT_ALLOWED,
    UPDATE_ARCHIVED_LINE_ITEM_NOT_ALLOWED,
    FRONTLOADED_NOT_ALLOWED,
    CREATE_OR_UPDATE_LEGACY_DFP_LINE_ITEM_TYPE_NOT_ALLOWED,
    COPY_LINE_ITEM_FROM_DIFFERENT_COMPANY_NOT_ALLOWED,
    INVALID_SIZE_FOR_PLATFORM,
    INVALID_LINE_ITEM_TYPE_FOR_PLATFORM,
    INVALID_WEB_PROPERTY_FOR_PLATFORM,
    INVALID_WEB_PROPERTY_FOR_ENVIRONMENT,
    AFMA_BACKFILL_NOT_ALLOWED,
    UPDATE_ENVIRONMENT_TYPE_NOT_ALLOWED,
    COMPANIONS_NOT_ALLOWED,
    ROADBLOCKS_WITH_NONROADBLOCKS_NOT_ALLOWED,
    CANNOT_UPDATE_TO_OR_FROM_CREATIVE_SET_ROADBLOCK,
    UPDATE_FROM_BACKFILL_LINE_ITEM_TYPE_NOT_ALLOWED,
    UPDATE_TO_BACKFILL_LINE_ITEM_TYPE_NOT_ALLOWED,
    UPDATE_BACKFILL_WEB_PROPERTY_NOT_ALLOWED,
    INVALID_COMPANION_DELIVERY_OPTION_FOR_ENVIRONMENT_TYPE,
    COMPANION_BACKFILL_REQUIRES_VIDEO,
    COMPANION_DELIVERY_OPTION_REQUIRE_PREMIUM,
    DUPLICATE_MASTER_SIZES,
    INVALID_PRIORITY_FOR_LINE_ITEM_TYPE,
    INVALID_ENVIRONMENT_TYPE,
    INVALID_ENVIRONMENT_TYPE_FOR_PLATFORM,
    INVALID_TYPE_FOR_AUTO_EXTENSION,
    INVALID_TYPE_FOR_CONTRACTED_UNITS_BOUGHT,
    VIDEO_INVALID_ROADBLOCKING,
    BACKFILL_TYPE_NOT_ALLOWED,
    COMPANION_DELIVERY_OPTIONS_NOT_ALLOWED_WITH_BACKFILL,
    INVALID_WEB_PROPERTY_FOR_ADX_BACKFILL,
    INVALID_SIZE_FOR_ENVIRONMENT,
    TARGET_PLATOFRM_NOT_ALLOWED,
    INVALID_LINE_ITEM_CURRENCY,
    LINE_ITEM_CANNOT_HAVE_MULTIPLE_CURRENCIES,
    CANNOT_CHANGE_CURRENCY,
    INVALID_FOR_OFFLINE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemCreativeAssociationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemCreativeAssociationErrorReason {
    CREATIVE_IN_WRONG_ADVERTISERS_LIBRARY,
    INVALID_LINEITEM_CREATIVE_PAIRING,
    STARTDATE_BEFORE_LINEITEM_STARTDATE,
    STARTDATE_NOT_BEFORE_LINEITEM_ENDDATE,
    ENDDATE_AFTER_LINEITEM_ENDDATE,
    ENDDATE_NOT_AFTER_LINEITEM_STARTDATE,
    ENDDATE_NOT_AFTER_STARTDATE,
    ENDDATE_IN_THE_PAST,
    CANNOT_COPY_WITHIN_SAME_LINE_ITEM,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ForecastError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ForecastErrorReason {
    SERVER_NOT_AVAILABLE,
    INTERNAL_ERROR,
    NO_FORECAST_YET,
    NOT_ENOUGH_INVENTORY,
    SUCCESS,
    ZERO_LENGTH_RESERVATION,
    EXCEEDED_QUOTA,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DateTimeRangeTargetingError : ApiError {
    private DateTimeRangeTargetingErrorReason reasonField;

    private bool reasonFieldSpecified;

    public DateTimeRangeTargetingErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "DateTimeRangeTargetingError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum DateTimeRangeTargetingErrorReason {
    EMPTY_RANGES,
    NOT_SPONSORSHIP_LINEITEM,
    PAST_RANGES_CHANGED,
    RANGES_OVERLAP,
    RANGES_OUT_OF_LINEITEM_ACTIVE_PERIOD,
    START_TIME_IS_NOT_START_OF_DAY,
    END_TIME_IS_NOT_END_OF_DAY,
    START_DATE_TIME_IS_IN_PAST,
    RANGE_END_TIME_BEFORE_START_TIME,
    END_DATE_TIME_IS_TOO_LATE,
    LIMITED_RANGES_IN_UNLIMITED_LINEITEM,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CreativeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeErrorReason {
    FLASH_AND_FALLBACK_URL_ARE_SAME,
    INVALID_INTERNAL_REDIRECT_URL,
    DESTINATION_URL_REQUIRED,
    CANNOT_CREATE_OR_UPDATE_LEGACY_DFP_CREATIVE,
    CANNOT_CREATE_OR_UPDATE_LEGACY_DFP_MOBILE_CREATIVE,
    MISSING_FEATURE,
    INVALID_COMPANY_TYPE,
    INVALID_ADSENSE_CREATIVE_SIZE,
    INVALID_AD_EXCHANGE_CREATIVE_SIZE,
    DUPLICATE_ASSET_IN_CREATIVE,
    CREATIVE_ASSET_CANNOT_HAVE_ID_AND_BYTE_ARRAY,
    CANNOT_CREATE_OR_UPDATE_UNSUPPORTED_CREATIVE,
    CANNOT_COPY_VIDEO_CREATIVE_ACROSS_ADVERTISERS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ClickTrackingLineItemError : ApiError {
    private ClickTrackingLineItemErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ClickTrackingLineItemErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ClickTrackingLineItemError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ClickTrackingLineItemErrorReason {
    TYPE_IMMUTABLE,
    INVALID_TARGETING_TYPE,
    INVALID_ROADBLOCKING_TYPE,
    INVALID_CREATIVEROTATION_TYPE,
    INVALID_DELIVERY_RATE_TYPE,
    UNSUPPORTED_FIELD,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AudienceExtensionError : ApiError {
    private AudienceExtensionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AudienceExtensionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AudienceExtensionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AudienceExtensionErrorReason {
    FREQUENCY_CAPS_NOT_SUPPORTED,
    INVALID_TARGETING,
    INVENTORY_UNIT_TARGETING_INVALID,
    INVALID_CREATIVE_ROTATION,
    INVALID_EXTERNAL_ENTITY_ID,
    INVALID_LINE_ITEM_TYPE,
    INVALID_MAX_BID,
    AUDIENCE_EXTENSION_BULK_UPDATE_NOT_ALLOWED,
    UNEXPECTED_AUDIENCE_EXTENSION_ERROR,
    MAX_DAILY_BUDGET_AMOUNT_EXCEEDED,
    EXTERNAL_CAMPAIGN_ALREADY_EXISTS,
    AUDIENCE_EXTENSION_WITHOUT_FEATURE,
    AUDIENCE_EXTENSION_WITHOUT_LINKED_ACCOUNT,
    CANNOT_OVERRIDE_CREATIVE_SIZE_WITH_AUDIENCE_EXTENSION,
    CANNOT_OVERRIDE_FIELD_WITH_AUDIENCE_EXTENSION,
    ONLY_ONE_CREATIVE_PLACEHOLDER_ALLOWED,
    MULTIPLE_AUDIENCE_EXTENSION_LINE_ITEMS_ON_ORDER,
    CANNOT_COPY_AUDIENCE_EXTENSION_LINE_ITEMS_AND_CREATIVES_TOGETHER,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "LineItemTemplateServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class LineItemTemplateService : DfpSoapClient, ILineItemTemplateService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public LineItemTemplateService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/LineItemTemplateService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItemTemplatePage getLineItemTemplatesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getLineItemTemplatesByStatement", new object[] { filterStatement });
      return ((LineItemTemplatePage) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LineItemTemplate {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private bool isDefaultField;

    private bool isDefaultFieldSpecified;

    private string lineItemNameField;

    private TargetPlatform targetPlatformField;

    private bool targetPlatformFieldSpecified;

    private bool enabledForSameAdvertiserExceptionField;

    private bool enabledForSameAdvertiserExceptionFieldSpecified;

    private string notesField;

    private LineItemType lineItemTypeField;

    private bool lineItemTypeFieldSpecified;

    private DateTime startTimeField;

    private DateTime endTimeField;

    private DeliveryRateType deliveryRateTypeField;

    private bool deliveryRateTypeFieldSpecified;

    private RoadblockingType roadblockingTypeField;

    private bool roadblockingTypeFieldSpecified;

    private CreativeRotationType creativeRotationTypeField;

    private bool creativeRotationTypeFieldSpecified;

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

    public bool isDefault {
      get { return this.isDefaultField; }
      set {
        this.isDefaultField = value;
        this.isDefaultSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isDefaultSpecified {
      get { return this.isDefaultFieldSpecified; }
      set { this.isDefaultFieldSpecified = value; }
    }

    public string lineItemName {
      get { return this.lineItemNameField; }
      set { this.lineItemNameField = value; }
    }

    public TargetPlatform targetPlatform {
      get { return this.targetPlatformField; }
      set {
        this.targetPlatformField = value;
        this.targetPlatformSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool targetPlatformSpecified {
      get { return this.targetPlatformFieldSpecified; }
      set { this.targetPlatformFieldSpecified = value; }
    }

    public bool enabledForSameAdvertiserException {
      get { return this.enabledForSameAdvertiserExceptionField; }
      set {
        this.enabledForSameAdvertiserExceptionField = value;
        this.enabledForSameAdvertiserExceptionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool enabledForSameAdvertiserExceptionSpecified {
      get { return this.enabledForSameAdvertiserExceptionFieldSpecified; }
      set { this.enabledForSameAdvertiserExceptionFieldSpecified = value; }
    }

    public string notes {
      get { return this.notesField; }
      set { this.notesField = value; }
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

    public DateTime startTime {
      get { return this.startTimeField; }
      set { this.startTimeField = value; }
    }

    public DateTime endTime {
      get { return this.endTimeField; }
      set { this.endTimeField = value; }
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
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LineItemTemplatePage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private LineItemTemplate[] resultsField;

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
    public LineItemTemplate[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "UserTeamAssociationServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserRecordTeamAssociation))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class UserTeamAssociationService : DfpSoapClient, IUserTeamAssociationService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public UserTeamAssociationService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/UserTeamAssociationService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UserTeamAssociation createUserTeamAssociation(UserTeamAssociation userTeamAssociation) {
      object[] results = this.Invoke("createUserTeamAssociation", new object[] { userTeamAssociation });
      return ((UserTeamAssociation) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UserTeamAssociation[] createUserTeamAssociations([System.Xml.Serialization.XmlElementAttribute("userTeamAssociations")]
UserTeamAssociation[] userTeamAssociations) {
      object[] results = this.Invoke("createUserTeamAssociations", new object[] { userTeamAssociations });
      return ((UserTeamAssociation[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UserTeamAssociation getUserTeamAssociation(long teamId, long userId) {
      object[] results = this.Invoke("getUserTeamAssociation", new object[] { teamId, userId });
      return ((UserTeamAssociation) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UserTeamAssociationPage getUserTeamAssociationsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getUserTeamAssociationsByStatement", new object[] { filterStatement });
      return ((UserTeamAssociationPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performUserTeamAssociationAction(UserTeamAssociationAction userTeamAssociationAction, Statement statement) {
      object[] results = this.Invoke("performUserTeamAssociationAction", new object[] { userTeamAssociationAction, statement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UserTeamAssociation updateUserTeamAssociation(UserTeamAssociation userTeamAssociation) {
      object[] results = this.Invoke("updateUserTeamAssociation", new object[] { userTeamAssociation });
      return ((UserTeamAssociation) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UserTeamAssociation[] updateUserTeamAssociations([System.Xml.Serialization.XmlElementAttribute("userTeamAssociations")]
UserTeamAssociation[] userTeamAssociations) {
      object[] results = this.Invoke("updateUserTeamAssociations", new object[] { userTeamAssociations });
      return ((UserTeamAssociation[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteUserTeamAssociations))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class UserTeamAssociationAction {
    private string userTeamAssociationActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("UserTeamAssociationAction.Type")]
    public string UserTeamAssociationActionType {
      get { return this.userTeamAssociationActionTypeField; }
      set { this.userTeamAssociationActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeleteUserTeamAssociations : UserTeamAssociationAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UserTeamAssociationPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private UserTeamAssociation[] resultsField;

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
    public UserTeamAssociation[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UserTeamAssociation : UserRecordTeamAssociation {
    private long userIdField;

    private bool userIdFieldSpecified;

    public long userId {
      get { return this.userIdField; }
      set {
        this.userIdField = value;
        this.userIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool userIdSpecified {
      get { return this.userIdFieldSpecified; }
      set { this.userIdFieldSpecified = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserTeamAssociation))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class UserRecordTeamAssociation {
    private long teamIdField;

    private bool teamIdFieldSpecified;

    private TeamAccessType overriddenTeamAccessTypeField;

    private bool overriddenTeamAccessTypeFieldSpecified;

    private TeamAccessType defaultTeamAccessTypeField;

    private bool defaultTeamAccessTypeFieldSpecified;

    private string userRecordTeamAssociationTypeField;

    public long teamId {
      get { return this.teamIdField; }
      set {
        this.teamIdField = value;
        this.teamIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool teamIdSpecified {
      get { return this.teamIdFieldSpecified; }
      set { this.teamIdFieldSpecified = value; }
    }

    public TeamAccessType overriddenTeamAccessType {
      get { return this.overriddenTeamAccessTypeField; }
      set {
        this.overriddenTeamAccessTypeField = value;
        this.overriddenTeamAccessTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool overriddenTeamAccessTypeSpecified {
      get { return this.overriddenTeamAccessTypeFieldSpecified; }
      set { this.overriddenTeamAccessTypeFieldSpecified = value; }
    }

    public TeamAccessType defaultTeamAccessType {
      get { return this.defaultTeamAccessTypeField; }
      set {
        this.defaultTeamAccessTypeField = value;
        this.defaultTeamAccessTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool defaultTeamAccessTypeSpecified {
      get { return this.defaultTeamAccessTypeFieldSpecified; }
      set { this.defaultTeamAccessTypeFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("UserRecordTeamAssociation.Type")]
    public string UserRecordTeamAssociationType {
      get { return this.userRecordTeamAssociationTypeField; }
      set { this.userRecordTeamAssociationTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum TeamAccessType {
    NONE,
    READ_ONLY,
    READ_WRITE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "LineItemCreativeAssociationServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class LineItemCreativeAssociationService : DfpSoapClient, ILineItemCreativeAssociationService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public LineItemCreativeAssociationService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/LineItemCreativeAssociationServ" + "ice";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItemCreativeAssociation createLineItemCreativeAssociation(LineItemCreativeAssociation lineItemCreativeAssociation) {
      object[] results = this.Invoke("createLineItemCreativeAssociation", new object[] { lineItemCreativeAssociation });
      return ((LineItemCreativeAssociation) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItemCreativeAssociation[] createLineItemCreativeAssociations([System.Xml.Serialization.XmlElementAttribute("lineItemCreativeAssociations")]
LineItemCreativeAssociation[] lineItemCreativeAssociations) {
      object[] results = this.Invoke("createLineItemCreativeAssociations", new object[] { lineItemCreativeAssociations });
      return ((LineItemCreativeAssociation[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItemCreativeAssociation getLineItemCreativeAssociation(long lineItemId, long creativeId) {
      object[] results = this.Invoke("getLineItemCreativeAssociation", new object[] { lineItemId, creativeId });
      return ((LineItemCreativeAssociation) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItemCreativeAssociationPage getLineItemCreativeAssociationsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getLineItemCreativeAssociationsByStatement", new object[] { filterStatement });
      return ((LineItemCreativeAssociationPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual string getPreviewUrl(long lineItemId, long creativeId, string siteUrl) {
      object[] results = this.Invoke("getPreviewUrl", new object[] { lineItemId, creativeId, siteUrl });
      return ((string) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performLineItemCreativeAssociationAction(LineItemCreativeAssociationAction lineItemCreativeAssociationAction, Statement filterStatement) {
      object[] results = this.Invoke("performLineItemCreativeAssociationAction", new object[] { lineItemCreativeAssociationAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItemCreativeAssociation updateLineItemCreativeAssociation(LineItemCreativeAssociation lineItemCreativeAssociation) {
      object[] results = this.Invoke("updateLineItemCreativeAssociation", new object[] { lineItemCreativeAssociation });
      return ((LineItemCreativeAssociation) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual LineItemCreativeAssociation[] updateLineItemCreativeAssociations([System.Xml.Serialization.XmlElementAttribute("lineItemCreativeAssociations")]
LineItemCreativeAssociation[] lineItemCreativeAssociations) {
      object[] results = this.Invoke("updateLineItemCreativeAssociations", new object[] { lineItemCreativeAssociations });
      return ((LineItemCreativeAssociation[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateLineItemCreativeAssociations))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateLineItemCreativeAssociations))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class LineItemCreativeAssociationAction {
    private string lineItemCreativeAssociationActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("LineItemCreativeAssociationAction.Type")]
    public string LineItemCreativeAssociationActionType {
      get { return this.lineItemCreativeAssociationActionTypeField; }
      set { this.lineItemCreativeAssociationActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateLineItemCreativeAssociations : LineItemCreativeAssociationAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateLineItemCreativeAssociations : LineItemCreativeAssociationAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LineItemCreativeAssociation {
    private long lineItemIdField;

    private bool lineItemIdFieldSpecified;

    private long creativeIdField;

    private bool creativeIdFieldSpecified;

    private long creativeSetIdField;

    private bool creativeSetIdFieldSpecified;

    private double manualCreativeRotationWeightField;

    private bool manualCreativeRotationWeightFieldSpecified;

    private int sequentialCreativeRotationIndexField;

    private bool sequentialCreativeRotationIndexFieldSpecified;

    private DateTime startDateTimeField;

    private StartDateTimeType startDateTimeTypeField;

    private bool startDateTimeTypeFieldSpecified;

    private DateTime endDateTimeField;

    private string destinationUrlField;

    private Size[] sizesField;

    private LineItemCreativeAssociationStatus statusField;

    private bool statusFieldSpecified;

    private LineItemCreativeAssociationStats statsField;

    private DateTime lastModifiedDateTimeField;

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

    public long creativeSetId {
      get { return this.creativeSetIdField; }
      set {
        this.creativeSetIdField = value;
        this.creativeSetIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool creativeSetIdSpecified {
      get { return this.creativeSetIdFieldSpecified; }
      set { this.creativeSetIdFieldSpecified = value; }
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

    public int sequentialCreativeRotationIndex {
      get { return this.sequentialCreativeRotationIndexField; }
      set {
        this.sequentialCreativeRotationIndexField = value;
        this.sequentialCreativeRotationIndexSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool sequentialCreativeRotationIndexSpecified {
      get { return this.sequentialCreativeRotationIndexFieldSpecified; }
      set { this.sequentialCreativeRotationIndexFieldSpecified = value; }
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

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemCreativeAssociation.Status", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemCreativeAssociationStatus {
    ACTIVE,
    NOT_SERVING,
    INACTIVE,
    DELETED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LineItemCreativeAssociationStats {
    private Stats statsField;

    private Long_StatsMapEntry[] creativeSetStatsField;

    private Money costInOrderCurrencyField;

    public Stats stats {
      get { return this.statsField; }
      set { this.statsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("creativeSetStats")]
    public Long_StatsMapEntry[] creativeSetStats {
      get { return this.creativeSetStatsField; }
      set { this.creativeSetStatsField = value; }
    }

    public Money costInOrderCurrency {
      get { return this.costInOrderCurrencyField; }
      set { this.costInOrderCurrencyField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Long_StatsMapEntry {
    private long keyField;

    private bool keyFieldSpecified;

    private Stats valueField;

    public long key {
      get { return this.keyField; }
      set {
        this.keyField = value;
        this.keySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool keySpecified {
      get { return this.keyFieldSpecified; }
      set { this.keyFieldSpecified = value; }
    }

    public Stats value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class TemplateInstantiatedCreativeError : ApiError {
    private TemplateInstantiatedCreativeErrorReason reasonField;

    private bool reasonFieldSpecified;

    public TemplateInstantiatedCreativeErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "TemplateInstantiatedCreativeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum TemplateInstantiatedCreativeErrorReason {
    INACTIVE_CREATIVE_TEMPLATE,
    FILE_TYPE_NOT_ALLOWED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SwiffyConversionError : ApiError {
    private SwiffyConversionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public SwiffyConversionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "SwiffyConversionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum SwiffyConversionErrorReason {
    SERVER_ERROR,
    INVALID_FLASH_FILE,
    UNSUPPORTED_FLASH,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RichMediaStudioCreativeError : ApiError {
    private RichMediaStudioCreativeErrorReason reasonField;

    private bool reasonFieldSpecified;

    public RichMediaStudioCreativeErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RichMediaStudioCreativeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RichMediaStudioCreativeErrorReason {
    CREATION_NOT_ALLOWED,
    UKNOWN_ERROR,
    INVALID_CODE_GENERATION_REQUEST,
    INVALID_CREATIVE_OBJECT,
    STUDIO_CONNECTION_ERROR,
    PUSHDOWN_DURATION_NOT_ALLOWED,
    INVALID_POSITION,
    INVALID_Z_INDEX,
    INVALID_PUSHDOWN_DURATION,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "LineItemCreativeAssociationOperationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum LineItemCreativeAssociationOperationErrorReason {
    NOT_ALLOWED,
    NOT_APPLICABLE,
    CANNOT_ACTIVATE_INVALID_CREATIVE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class InvalidPhoneNumberError : ApiError {
    private InvalidPhoneNumberErrorReason reasonField;

    private bool reasonFieldSpecified;

    public InvalidPhoneNumberErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InvalidPhoneNumberError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InvalidPhoneNumberErrorReason {
    INVALID_FORMAT,
    TOO_SHORT,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ImageError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ImageErrorReason {
    INVALID_IMAGE,
    INVALID_SIZE,
    UNEXPECTED_SIZE,
    OVERLAY_SIZE_TOO_LARGE,
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
    SYSTEM_ERROR,
    UNEXPECTED_PRIMARY_ASSET_DENSITY,
    DUPLICATE_ASSET_DENSITY,
    MISSING_DEFAULT_ASSET,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "FileError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum FileErrorReason {
    MISSING_CONTENTS,
    SIZE_TOO_LARGE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomCreativeError : ApiError {
    private CustomCreativeErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CustomCreativeErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomCreativeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomCreativeErrorReason {
    DUPLICATE_MACRO_NAME_FOR_CREATIVE,
    SNIPPET_REFERENCES_MISSING_MACRO,
    UNRECOGNIZED_MACRO,
    CUSTOM_CREATIVE_NOT_ALLOWED,
    MISSING_INTERSTITIAL_MACRO,
    DUPLICATE_ASSET_IN_MACROS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeSetError : ApiError {
    private CreativeSetErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CreativeSetErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CreativeSetError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeSetErrorReason {
    VIDEO_FEATURE_REQUIRED,
    CANNOT_CREATE_OR_UPDATE_VIDEO_CREATIVES,
    ROADBLOCK_FEATURE_REQUIRED,
    MASTER_CREATIVE_CANNOT_BE_COMPANION,
    VIDEO_CREATIVE_NOT_ALLOWED,
    INVALID_ADVERTISER,
    UPDATE_MASTER_CREATIVE_NOT_ALLOWED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeAssetMacroError : ApiError {
    private CreativeAssetMacroErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CreativeAssetMacroErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CreativeAssetMacroError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeAssetMacroErrorReason {
    INVALID_MACRO_NAME,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AssetError : ApiError {
    private AssetErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AssetErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AssetError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AssetErrorReason {
    NON_UNIQUE_NAME,
    FILE_NAME_TOO_LONG,
    FILE_SIZE_TOO_LARGE,
    MISSING_REQUIRED_DYNAMIC_ALLOCATION_CLIENT,
    MISSING_REQUIRED_DYNAMIC_ALLOCATION_HEIGHT,
    MISSING_REQUIRED_DYNAMIC_ALLOCATION_WIDTH,
    MISSING_REQUIRED_DYNAMIC_ALLOCATION_FORMAT,
    INVALID_CODE_SNIPPET_PARAMETER_VALUE,
    INVALID_ASSET_ID,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdSenseAccountError : ApiError {
    private AdSenseAccountErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AdSenseAccountErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdSenseAccountError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdSenseAccountErrorReason {
    ASSOCIATE_ACCOUNT_API_ERROR,
    GET_AD_SLOT_API_ERROR,
    GET_BULK_ACCOUNT_STATUSES_API_ERROR,
    RESEND_VERIFICATION_EMAIL_ERROR,
    UNEXPECTED_API_RESPONSE_ERROR,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ProductServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ProductService : DfpSoapClient, IProductService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ProductService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ProductService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Product getProduct(string productId) {
      object[] results = this.Invoke("getProduct", new object[] { productId });
      return ((Product) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProductPage getProductsByStatement(Statement statement) {
      object[] results = this.Invoke("getProductsByStatement", new object[] { statement });
      return ((ProductPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performProductAction(ProductAction productAction, Statement filterStatement) {
      object[] results = this.Invoke("performProductAction", new object[] { productAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Product updateProduct(Product product) {
      object[] results = this.Invoke("updateProduct", new object[] { product });
      return ((Product) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Product[] updateProducts([System.Xml.Serialization.XmlElementAttribute("products")]
Product[] products) {
      object[] results = this.Invoke("updateProducts", new object[] { products });
      return ((Product[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateProducts))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveProducts))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateProducts))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class ProductAction {
    private string productActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("ProductAction.Type")]
    public string ProductActionType {
      get { return this.productActionTypeField; }
      set { this.productActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateProducts : ProductAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ArchiveProducts : ProductAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateProducts : ProductAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Product[] resultsField;

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
    public Product[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Product {
    private string nameField;

    private ProductStatus statusField;

    private bool statusFieldSpecified;

    private ProductType productTypeField;

    private bool productTypeFieldSpecified;

    private long productTemplateIdField;

    private bool productTemplateIdFieldSpecified;

    private string idField;

    private string notesField;

    private string productTemplateDescriptionField;

    private DateTime lastModifiedDateTimeField;

    private RateType rateTypeField;

    private bool rateTypeFieldSpecified;

    private RoadblockingType roadblockingTypeField;

    private bool roadblockingTypeFieldSpecified;

    private CreativePlaceholder[] creativePlaceholdersField;

    private LineItemType lineItemTypeField;

    private bool lineItemTypeFieldSpecified;

    private int priorityField;

    private bool priorityFieldSpecified;

    private FrequencyCap[] frequencyCapsField;

    private bool allowFrequencyCapsCustomizationField;

    private bool allowFrequencyCapsCustomizationFieldSpecified;

    private ProductTemplateTargeting targetingField;

    private BaseCustomFieldValue[] customFieldValuesField;

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public ProductStatus status {
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

    public ProductType productType {
      get { return this.productTypeField; }
      set {
        this.productTypeField = value;
        this.productTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool productTypeSpecified {
      get { return this.productTypeFieldSpecified; }
      set { this.productTypeFieldSpecified = value; }
    }

    public long productTemplateId {
      get { return this.productTemplateIdField; }
      set {
        this.productTemplateIdField = value;
        this.productTemplateIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool productTemplateIdSpecified {
      get { return this.productTemplateIdFieldSpecified; }
      set { this.productTemplateIdFieldSpecified = value; }
    }

    public string id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    public string notes {
      get { return this.notesField; }
      set { this.notesField = value; }
    }

    public string productTemplateDescription {
      get { return this.productTemplateDescriptionField; }
      set { this.productTemplateDescriptionField = value; }
    }

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }

    public RateType rateType {
      get { return this.rateTypeField; }
      set {
        this.rateTypeField = value;
        this.rateTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool rateTypeSpecified {
      get { return this.rateTypeFieldSpecified; }
      set { this.rateTypeFieldSpecified = value; }
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

    [System.Xml.Serialization.XmlElementAttribute("creativePlaceholders")]
    public CreativePlaceholder[] creativePlaceholders {
      get { return this.creativePlaceholdersField; }
      set { this.creativePlaceholdersField = value; }
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

    public int priority {
      get { return this.priorityField; }
      set {
        this.priorityField = value;
        this.prioritySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool prioritySpecified {
      get { return this.priorityFieldSpecified; }
      set { this.priorityFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("frequencyCaps")]
    public FrequencyCap[] frequencyCaps {
      get { return this.frequencyCapsField; }
      set { this.frequencyCapsField = value; }
    }

    public bool allowFrequencyCapsCustomization {
      get { return this.allowFrequencyCapsCustomizationField; }
      set {
        this.allowFrequencyCapsCustomizationField = value;
        this.allowFrequencyCapsCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowFrequencyCapsCustomizationSpecified {
      get { return this.allowFrequencyCapsCustomizationFieldSpecified; }
      set { this.allowFrequencyCapsCustomizationFieldSpecified = value; }
    }

    public ProductTemplateTargeting targeting {
      get { return this.targetingField; }
      set { this.targetingField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customFieldValues")]
    public BaseCustomFieldValue[] customFieldValues {
      get { return this.customFieldValuesField; }
      set { this.customFieldValuesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProductStatus {
    ACTIVE,
    INACTIVE,
    ARCHIVED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProductType {
    DFP,
    OFFLINE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RateType {
    CPM,
    CPC,
    CPD,
    FLAT_RATE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductTemplateTargeting {
    private GeoTargeting geoTargetingField;

    private bool allowGeoTargetingCustomizationField;

    private bool allowGeoTargetingCustomizationFieldSpecified;

    private InventoryTargeting inventoryTargetingField;

    private bool allowAdUnitTargetingCustomizationField;

    private bool allowAdUnitTargetingCustomizationFieldSpecified;

    private bool allowPlacementTargetingCustomizationField;

    private bool allowPlacementTargetingCustomizationFieldSpecified;

    private long[] customizableCustomTargetingKeyIdsField;

    private UserDomainTargeting userDomainTargetingField;

    private bool allowUserDomainTargetingCustomizationField;

    private bool allowUserDomainTargetingCustomizationFieldSpecified;

    private BandwidthGroupTargeting bandwidthGroupTargetingField;

    private bool allowBandwidthGroupTargetingCustomizationField;

    private bool allowBandwidthGroupTargetingCustomizationFieldSpecified;

    private BrowserTargeting browserTargetingField;

    private bool allowBrowserTargetingCustomizationField;

    private bool allowBrowserTargetingCustomizationFieldSpecified;

    private BrowserLanguageTargeting browserLanguageTargetingField;

    private bool allowBrowserLanguageTargetingCustomizationField;

    private bool allowBrowserLanguageTargetingCustomizationFieldSpecified;

    private OperatingSystemTargeting operatingSystemTargetingField;

    private bool allowOperatingSystemTargetingCustomizationField;

    private bool allowOperatingSystemTargetingCustomizationFieldSpecified;

    public GeoTargeting geoTargeting {
      get { return this.geoTargetingField; }
      set { this.geoTargetingField = value; }
    }

    public bool allowGeoTargetingCustomization {
      get { return this.allowGeoTargetingCustomizationField; }
      set {
        this.allowGeoTargetingCustomizationField = value;
        this.allowGeoTargetingCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowGeoTargetingCustomizationSpecified {
      get { return this.allowGeoTargetingCustomizationFieldSpecified; }
      set { this.allowGeoTargetingCustomizationFieldSpecified = value; }
    }

    public InventoryTargeting inventoryTargeting {
      get { return this.inventoryTargetingField; }
      set { this.inventoryTargetingField = value; }
    }

    public bool allowAdUnitTargetingCustomization {
      get { return this.allowAdUnitTargetingCustomizationField; }
      set {
        this.allowAdUnitTargetingCustomizationField = value;
        this.allowAdUnitTargetingCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowAdUnitTargetingCustomizationSpecified {
      get { return this.allowAdUnitTargetingCustomizationFieldSpecified; }
      set { this.allowAdUnitTargetingCustomizationFieldSpecified = value; }
    }

    public bool allowPlacementTargetingCustomization {
      get { return this.allowPlacementTargetingCustomizationField; }
      set {
        this.allowPlacementTargetingCustomizationField = value;
        this.allowPlacementTargetingCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowPlacementTargetingCustomizationSpecified {
      get { return this.allowPlacementTargetingCustomizationFieldSpecified; }
      set { this.allowPlacementTargetingCustomizationFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customizableCustomTargetingKeyIds")]
    public long[] customizableCustomTargetingKeyIds {
      get { return this.customizableCustomTargetingKeyIdsField; }
      set { this.customizableCustomTargetingKeyIdsField = value; }
    }

    public UserDomainTargeting userDomainTargeting {
      get { return this.userDomainTargetingField; }
      set { this.userDomainTargetingField = value; }
    }

    public bool allowUserDomainTargetingCustomization {
      get { return this.allowUserDomainTargetingCustomizationField; }
      set {
        this.allowUserDomainTargetingCustomizationField = value;
        this.allowUserDomainTargetingCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowUserDomainTargetingCustomizationSpecified {
      get { return this.allowUserDomainTargetingCustomizationFieldSpecified; }
      set { this.allowUserDomainTargetingCustomizationFieldSpecified = value; }
    }

    public BandwidthGroupTargeting bandwidthGroupTargeting {
      get { return this.bandwidthGroupTargetingField; }
      set { this.bandwidthGroupTargetingField = value; }
    }

    public bool allowBandwidthGroupTargetingCustomization {
      get { return this.allowBandwidthGroupTargetingCustomizationField; }
      set {
        this.allowBandwidthGroupTargetingCustomizationField = value;
        this.allowBandwidthGroupTargetingCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowBandwidthGroupTargetingCustomizationSpecified {
      get { return this.allowBandwidthGroupTargetingCustomizationFieldSpecified; }
      set { this.allowBandwidthGroupTargetingCustomizationFieldSpecified = value; }
    }

    public BrowserTargeting browserTargeting {
      get { return this.browserTargetingField; }
      set { this.browserTargetingField = value; }
    }

    public bool allowBrowserTargetingCustomization {
      get { return this.allowBrowserTargetingCustomizationField; }
      set {
        this.allowBrowserTargetingCustomizationField = value;
        this.allowBrowserTargetingCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowBrowserTargetingCustomizationSpecified {
      get { return this.allowBrowserTargetingCustomizationFieldSpecified; }
      set { this.allowBrowserTargetingCustomizationFieldSpecified = value; }
    }

    public BrowserLanguageTargeting browserLanguageTargeting {
      get { return this.browserLanguageTargetingField; }
      set { this.browserLanguageTargetingField = value; }
    }

    public bool allowBrowserLanguageTargetingCustomization {
      get { return this.allowBrowserLanguageTargetingCustomizationField; }
      set {
        this.allowBrowserLanguageTargetingCustomizationField = value;
        this.allowBrowserLanguageTargetingCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowBrowserLanguageTargetingCustomizationSpecified {
      get { return this.allowBrowserLanguageTargetingCustomizationFieldSpecified; }
      set { this.allowBrowserLanguageTargetingCustomizationFieldSpecified = value; }
    }

    public OperatingSystemTargeting operatingSystemTargeting {
      get { return this.operatingSystemTargetingField; }
      set { this.operatingSystemTargetingField = value; }
    }

    public bool allowOperatingSystemTargetingCustomization {
      get { return this.allowOperatingSystemTargetingCustomizationField; }
      set {
        this.allowOperatingSystemTargetingCustomizationField = value;
        this.allowOperatingSystemTargetingCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowOperatingSystemTargetingCustomizationSpecified {
      get { return this.allowOperatingSystemTargetingCustomizationFieldSpecified; }
      set { this.allowOperatingSystemTargetingCustomizationFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductActionError : ApiError {
    private ProductActionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ProductActionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ProductActionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProductActionErrorReason {
    NOT_APPLICABLE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BaseRateError : ApiError {
    private BaseRateErrorReason reasonField;

    private bool reasonFieldSpecified;

    public BaseRateErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "BaseRateError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum BaseRateErrorReason {
    CANNOT_QUERY_BOTH_PRODUCT_TEMPLATE_ID_AND_PRODUCT_ID,
    INVALID_CURRENCY_CODE,
    UNSUPPORTED_OPERATION,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ReconciliationReportRowServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ReconciliationReportRowService : DfpSoapClient, IReconciliationReportRowService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ReconciliationReportRowService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ReconciliationReportRowService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReconciliationReportRowPage getReconciliationReportRowsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getReconciliationReportRowsByStatement", new object[] { filterStatement });
      return ((ReconciliationReportRowPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReconciliationReportRow[] updateReconciliationReportRows([System.Xml.Serialization.XmlElementAttribute("reconciliationReportRows")]
ReconciliationReportRow[] reconciliationReportRows) {
      object[] results = this.Invoke("updateReconciliationReportRows", new object[] { reconciliationReportRows });
      return ((ReconciliationReportRow[]) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReconciliationReportRow {
    private long reconciliationReportIdField;

    private bool reconciliationReportIdFieldSpecified;

    private long lineItemIdField;

    private bool lineItemIdFieldSpecified;

    private long creativeIdField;

    private bool creativeIdFieldSpecified;

    private long orderIdField;

    private bool orderIdFieldSpecified;

    private long advertiserIdField;

    private bool advertiserIdFieldSpecified;

    private BillFrom billFromField;

    private bool billFromFieldSpecified;

    private CostType lineItemCostTypeField;

    private bool lineItemCostTypeFieldSpecified;

    private Money lineItemCostPerUnitField;

    private long lineItemContractedUnitsBoughtField;

    private bool lineItemContractedUnitsBoughtFieldSpecified;

    private long dfpClicksField;

    private bool dfpClicksFieldSpecified;

    private long dfpImpressionsField;

    private bool dfpImpressionsFieldSpecified;

    private int dfpLineItemDaysField;

    private bool dfpLineItemDaysFieldSpecified;

    private long thirdPartyClicksField;

    private bool thirdPartyClicksFieldSpecified;

    private long thirdPartyImpressionsField;

    private bool thirdPartyImpressionsFieldSpecified;

    private int thirdPartyLineItemDaysField;

    private bool thirdPartyLineItemDaysFieldSpecified;

    private long manualClicksField;

    private bool manualClicksFieldSpecified;

    private long manualImpressionsField;

    private bool manualImpressionsFieldSpecified;

    private int manualLineItemDaysField;

    private bool manualLineItemDaysFieldSpecified;

    private long reconciledClicksField;

    private bool reconciledClicksFieldSpecified;

    private long reconciledImpressionsField;

    private bool reconciledImpressionsFieldSpecified;

    private int reconciledLineItemDaysField;

    private bool reconciledLineItemDaysFieldSpecified;

    private Money contractedRevenueField;

    private Money dfpRevenueField;

    private Money thirdPartyRevenueField;

    private Money manualRevenueField;

    private Money reconciledRevenueField;

    private string commentsField;

    public long reconciliationReportId {
      get { return this.reconciliationReportIdField; }
      set {
        this.reconciliationReportIdField = value;
        this.reconciliationReportIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reconciliationReportIdSpecified {
      get { return this.reconciliationReportIdFieldSpecified; }
      set { this.reconciliationReportIdFieldSpecified = value; }
    }

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

    public BillFrom billFrom {
      get { return this.billFromField; }
      set {
        this.billFromField = value;
        this.billFromSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool billFromSpecified {
      get { return this.billFromFieldSpecified; }
      set { this.billFromFieldSpecified = value; }
    }

    public CostType lineItemCostType {
      get { return this.lineItemCostTypeField; }
      set {
        this.lineItemCostTypeField = value;
        this.lineItemCostTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool lineItemCostTypeSpecified {
      get { return this.lineItemCostTypeFieldSpecified; }
      set { this.lineItemCostTypeFieldSpecified = value; }
    }

    public Money lineItemCostPerUnit {
      get { return this.lineItemCostPerUnitField; }
      set { this.lineItemCostPerUnitField = value; }
    }

    public long lineItemContractedUnitsBought {
      get { return this.lineItemContractedUnitsBoughtField; }
      set {
        this.lineItemContractedUnitsBoughtField = value;
        this.lineItemContractedUnitsBoughtSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool lineItemContractedUnitsBoughtSpecified {
      get { return this.lineItemContractedUnitsBoughtFieldSpecified; }
      set { this.lineItemContractedUnitsBoughtFieldSpecified = value; }
    }

    public long dfpClicks {
      get { return this.dfpClicksField; }
      set {
        this.dfpClicksField = value;
        this.dfpClicksSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool dfpClicksSpecified {
      get { return this.dfpClicksFieldSpecified; }
      set { this.dfpClicksFieldSpecified = value; }
    }

    public long dfpImpressions {
      get { return this.dfpImpressionsField; }
      set {
        this.dfpImpressionsField = value;
        this.dfpImpressionsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool dfpImpressionsSpecified {
      get { return this.dfpImpressionsFieldSpecified; }
      set { this.dfpImpressionsFieldSpecified = value; }
    }

    public int dfpLineItemDays {
      get { return this.dfpLineItemDaysField; }
      set {
        this.dfpLineItemDaysField = value;
        this.dfpLineItemDaysSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool dfpLineItemDaysSpecified {
      get { return this.dfpLineItemDaysFieldSpecified; }
      set { this.dfpLineItemDaysFieldSpecified = value; }
    }

    public long thirdPartyClicks {
      get { return this.thirdPartyClicksField; }
      set {
        this.thirdPartyClicksField = value;
        this.thirdPartyClicksSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool thirdPartyClicksSpecified {
      get { return this.thirdPartyClicksFieldSpecified; }
      set { this.thirdPartyClicksFieldSpecified = value; }
    }

    public long thirdPartyImpressions {
      get { return this.thirdPartyImpressionsField; }
      set {
        this.thirdPartyImpressionsField = value;
        this.thirdPartyImpressionsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool thirdPartyImpressionsSpecified {
      get { return this.thirdPartyImpressionsFieldSpecified; }
      set { this.thirdPartyImpressionsFieldSpecified = value; }
    }

    public int thirdPartyLineItemDays {
      get { return this.thirdPartyLineItemDaysField; }
      set {
        this.thirdPartyLineItemDaysField = value;
        this.thirdPartyLineItemDaysSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool thirdPartyLineItemDaysSpecified {
      get { return this.thirdPartyLineItemDaysFieldSpecified; }
      set { this.thirdPartyLineItemDaysFieldSpecified = value; }
    }

    public long manualClicks {
      get { return this.manualClicksField; }
      set {
        this.manualClicksField = value;
        this.manualClicksSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool manualClicksSpecified {
      get { return this.manualClicksFieldSpecified; }
      set { this.manualClicksFieldSpecified = value; }
    }

    public long manualImpressions {
      get { return this.manualImpressionsField; }
      set {
        this.manualImpressionsField = value;
        this.manualImpressionsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool manualImpressionsSpecified {
      get { return this.manualImpressionsFieldSpecified; }
      set { this.manualImpressionsFieldSpecified = value; }
    }

    public int manualLineItemDays {
      get { return this.manualLineItemDaysField; }
      set {
        this.manualLineItemDaysField = value;
        this.manualLineItemDaysSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool manualLineItemDaysSpecified {
      get { return this.manualLineItemDaysFieldSpecified; }
      set { this.manualLineItemDaysFieldSpecified = value; }
    }

    public long reconciledClicks {
      get { return this.reconciledClicksField; }
      set {
        this.reconciledClicksField = value;
        this.reconciledClicksSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reconciledClicksSpecified {
      get { return this.reconciledClicksFieldSpecified; }
      set { this.reconciledClicksFieldSpecified = value; }
    }

    public long reconciledImpressions {
      get { return this.reconciledImpressionsField; }
      set {
        this.reconciledImpressionsField = value;
        this.reconciledImpressionsSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reconciledImpressionsSpecified {
      get { return this.reconciledImpressionsFieldSpecified; }
      set { this.reconciledImpressionsFieldSpecified = value; }
    }

    public int reconciledLineItemDays {
      get { return this.reconciledLineItemDaysField; }
      set {
        this.reconciledLineItemDaysField = value;
        this.reconciledLineItemDaysSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reconciledLineItemDaysSpecified {
      get { return this.reconciledLineItemDaysFieldSpecified; }
      set { this.reconciledLineItemDaysFieldSpecified = value; }
    }

    public Money contractedRevenue {
      get { return this.contractedRevenueField; }
      set { this.contractedRevenueField = value; }
    }

    public Money dfpRevenue {
      get { return this.dfpRevenueField; }
      set { this.dfpRevenueField = value; }
    }

    public Money thirdPartyRevenue {
      get { return this.thirdPartyRevenueField; }
      set { this.thirdPartyRevenueField = value; }
    }

    public Money manualRevenue {
      get { return this.manualRevenueField; }
      set { this.manualRevenueField = value; }
    }

    public Money reconciledRevenue {
      get { return this.reconciledRevenueField; }
      set { this.reconciledRevenueField = value; }
    }

    public string comments {
      get { return this.commentsField; }
      set { this.commentsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum BillFrom {
    DEFAULT,
    DFP,
    THIRD_PARTY,
    MANUAL,
    CONTRACTED_GOAL,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReconciliationReportRowPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private ReconciliationReportRow[] resultsField;

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
    public ReconciliationReportRow[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "RateCardCustomizationServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class RateCardCustomizationService : DfpSoapClient, IRateCardCustomizationService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public RateCardCustomizationService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/RateCardCustomizationService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCardCustomization createRateCardCustomization(RateCardCustomization rateCardCustomization) {
      object[] results = this.Invoke("createRateCardCustomization", new object[] { rateCardCustomization });
      return ((RateCardCustomization) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCardCustomization[] createRateCardCustomizations([System.Xml.Serialization.XmlElementAttribute("rateCardCustomizations")]
RateCardCustomization[] rateCardCustomizations) {
      object[] results = this.Invoke("createRateCardCustomizations", new object[] { rateCardCustomizations });
      return ((RateCardCustomization[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCardCustomization getRateCardCustomization(long rateCardCustomizationId) {
      object[] results = this.Invoke("getRateCardCustomization", new object[] { rateCardCustomizationId });
      return ((RateCardCustomization) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCardCustomizationPage getRateCardCustomizationsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getRateCardCustomizationsByStatement", new object[] { filterStatement });
      return ((RateCardCustomizationPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performRateCardCustomizationAction(RateCardCustomizationAction rateCardCustomizationAction, Statement filterStatement) {
      object[] results = this.Invoke("performRateCardCustomizationAction", new object[] { rateCardCustomizationAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCardCustomization updateRateCardCustomization(RateCardCustomization rateCardCustomization) {
      object[] results = this.Invoke("updateRateCardCustomization", new object[] { rateCardCustomization });
      return ((RateCardCustomization) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCardCustomization[] updateRateCardCustomizations([System.Xml.Serialization.XmlElementAttribute("rateCardCustomizations")]
RateCardCustomization[] rateCardCustomizations) {
      object[] results = this.Invoke("updateRateCardCustomizations", new object[] { rateCardCustomizations });
      return ((RateCardCustomization[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateRateCardCustomizations))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateRateCardCustomizations))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class RateCardCustomizationAction {
    private string rateCardCustomizationActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("RateCardCustomizationAction.Type")]
    public string RateCardCustomizationActionType {
      get { return this.rateCardCustomizationActionTypeField; }
      set { this.rateCardCustomizationActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateRateCardCustomizations : RateCardCustomizationAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateRateCardCustomizations : RateCardCustomizationAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RateCardCustomizationPage {
    private RateCardCustomization[] resultsField;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public RateCardCustomization[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
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
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RateCardCustomization {
    private long rateCardIdField;

    private bool rateCardIdFieldSpecified;

    private long idField;

    private bool idFieldSpecified;

    private RateCardCustomizationStatus statusField;

    private bool statusFieldSpecified;

    private RateCardFeature rateCardFeatureField;

    private RateCardCustomizationAdjustmentType adjustmentTypeField;

    private bool adjustmentTypeFieldSpecified;

    private long adjustmentSizeField;

    private bool adjustmentSizeFieldSpecified;

    private RateType rateTypeField;

    private bool rateTypeFieldSpecified;

    public long rateCardId {
      get { return this.rateCardIdField; }
      set {
        this.rateCardIdField = value;
        this.rateCardIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool rateCardIdSpecified {
      get { return this.rateCardIdFieldSpecified; }
      set { this.rateCardIdFieldSpecified = value; }
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

    public RateCardCustomizationStatus status {
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

    public RateCardFeature rateCardFeature {
      get { return this.rateCardFeatureField; }
      set { this.rateCardFeatureField = value; }
    }

    public RateCardCustomizationAdjustmentType adjustmentType {
      get { return this.adjustmentTypeField; }
      set {
        this.adjustmentTypeField = value;
        this.adjustmentTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool adjustmentTypeSpecified {
      get { return this.adjustmentTypeFieldSpecified; }
      set { this.adjustmentTypeFieldSpecified = value; }
    }

    public long adjustmentSize {
      get { return this.adjustmentSizeField; }
      set {
        this.adjustmentSizeField = value;
        this.adjustmentSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool adjustmentSizeSpecified {
      get { return this.adjustmentSizeFieldSpecified; }
      set { this.adjustmentSizeFieldSpecified = value; }
    }

    public RateType rateType {
      get { return this.rateTypeField; }
      set {
        this.rateTypeField = value;
        this.rateTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool rateTypeSpecified {
      get { return this.rateTypeFieldSpecified; }
      set { this.rateTypeFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RateCardCustomizationStatus {
    ACTIVE,
    INACTIVE,
    UNKNOWN
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserDomainRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UnknownRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PlacementRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(OperatingSystemRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(GeographyRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FrequencyCapRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomTargetingRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BrowserLanguageRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BrowserRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BandwidthRateCardFeature))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdUnitRateCardFeature))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class RateCardFeature {
    private string rateCardFeatureTypeField;

    [System.Xml.Serialization.XmlElementAttribute("RateCardFeature.Type")]
    public string RateCardFeatureType {
      get { return this.rateCardFeatureTypeField; }
      set { this.rateCardFeatureTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UserDomainRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UnknownRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class PlacementRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class OperatingSystemRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class GeographyRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class FrequencyCapRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomTargetingRateCardFeature : RateCardFeature {
    private long customTargetingKeyIdField;

    private bool customTargetingKeyIdFieldSpecified;

    private long customTargetingValueIdField;

    private bool customTargetingValueIdFieldSpecified;

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

    public long customTargetingValueId {
      get { return this.customTargetingValueIdField; }
      set {
        this.customTargetingValueIdField = value;
        this.customTargetingValueIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool customTargetingValueIdSpecified {
      get { return this.customTargetingValueIdFieldSpecified; }
      set { this.customTargetingValueIdFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BrowserLanguageRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BrowserRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BandwidthRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdUnitRateCardFeature : RateCardFeature {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RateCardCustomizationAdjustmentType {
    PERCENTAGE,
    ABSOLUTE_VALUE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RateCardCustomizationError : ApiError {
    private RateCardCustomizationErrorReason reasonField;

    private bool reasonFieldSpecified;

    public RateCardCustomizationErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RateCardCustomizationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RateCardCustomizationErrorReason {
    INVALID_RATE_TYPE,
    UNSUPPORTED_OPERATION,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CreativeSetServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CreativeSetService : DfpSoapClient, ICreativeSetService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CreativeSetService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/CreativeSetService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeSet createCreativeSet(CreativeSet creativeSet) {
      object[] results = this.Invoke("createCreativeSet", new object[] { creativeSet });
      return ((CreativeSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeSet getCreativeSet(long creativeSetId) {
      object[] results = this.Invoke("getCreativeSet", new object[] { creativeSetId });
      return ((CreativeSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeSetPage getCreativeSetsByStatement(Statement statement) {
      object[] results = this.Invoke("getCreativeSetsByStatement", new object[] { statement });
      return ((CreativeSetPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeSet updateCreativeSet(CreativeSet creativeSet) {
      object[] results = this.Invoke("updateCreativeSet", new object[] { creativeSet });
      return ((CreativeSet) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeSetPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private CreativeSet[] resultsField;

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
    public CreativeSet[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeSet {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private long masterCreativeIdField;

    private bool masterCreativeIdFieldSpecified;

    private long[] companionCreativeIdsField;

    private DateTime lastModifiedDateTimeField;

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

    public long masterCreativeId {
      get { return this.masterCreativeIdField; }
      set {
        this.masterCreativeIdField = value;
        this.masterCreativeIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool masterCreativeIdSpecified {
      get { return this.masterCreativeIdFieldSpecified; }
      set { this.masterCreativeIdFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("companionCreativeIds")]
    public long[] companionCreativeIds {
      get { return this.companionCreativeIdsField; }
      set { this.companionCreativeIdsField = value; }
    }

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "BaseRateServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class BaseRateService : DfpSoapClient, IBaseRateService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public BaseRateService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/BaseRateService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual BaseRate createBaseRate(BaseRate baseRate) {
      object[] results = this.Invoke("createBaseRate", new object[] { baseRate });
      return ((BaseRate) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual BaseRate[] createBaseRates([System.Xml.Serialization.XmlElementAttribute("baseRates")]
BaseRate[] baseRates) {
      object[] results = this.Invoke("createBaseRates", new object[] { baseRates });
      return ((BaseRate[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual BaseRate getBaseRate(long baseRateId) {
      object[] results = this.Invoke("getBaseRate", new object[] { baseRateId });
      return ((BaseRate) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual BaseRatePage getBaseRatesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getBaseRatesByStatement", new object[] { filterStatement });
      return ((BaseRatePage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performBaseRateAction(BaseRateAction baseRateAction, Statement filterStatement) {
      object[] results = this.Invoke("performBaseRateAction", new object[] { baseRateAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual BaseRate updateBaseRate(BaseRate baseRate) {
      object[] results = this.Invoke("updateBaseRate", new object[] { baseRate });
      return ((BaseRate) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual BaseRate[] updateBaseRates([System.Xml.Serialization.XmlElementAttribute("baseRates")]
BaseRate[] baseRates) {
      object[] results = this.Invoke("updateBaseRates", new object[] { baseRates });
      return ((BaseRate[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateBaseRates))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateBaseRates))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseRateAction {
    private string baseRateActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("BaseRateAction.Type")]
    public string BaseRateActionType {
      get { return this.baseRateActionTypeField; }
      set { this.baseRateActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateBaseRates : BaseRateAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateBaseRates : BaseRateAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BaseRatePage {
    private BaseRate[] resultsField;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public BaseRate[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
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
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductTemplateBaseRate))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductBaseRate))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseRate {
    private long rateCardIdField;

    private bool rateCardIdFieldSpecified;

    private long idField;

    private bool idFieldSpecified;

    private BaseRateStatus statusField;

    private bool statusFieldSpecified;

    private string baseRateTypeField;

    public long rateCardId {
      get { return this.rateCardIdField; }
      set {
        this.rateCardIdField = value;
        this.rateCardIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool rateCardIdSpecified {
      get { return this.rateCardIdFieldSpecified; }
      set { this.rateCardIdFieldSpecified = value; }
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

    public BaseRateStatus status {
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

    [System.Xml.Serialization.XmlElementAttribute("BaseRate.Type")]
    public string BaseRateType {
      get { return this.baseRateTypeField; }
      set { this.baseRateTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum BaseRateStatus {
    ACTIVE,
    INACTIVE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductTemplateBaseRate : BaseRate {
    private long productTemplateIdField;

    private bool productTemplateIdFieldSpecified;

    private Money rateField;

    public long productTemplateId {
      get { return this.productTemplateIdField; }
      set {
        this.productTemplateIdField = value;
        this.productTemplateIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool productTemplateIdSpecified {
      get { return this.productTemplateIdFieldSpecified; }
      set { this.productTemplateIdFieldSpecified = value; }
    }

    public Money rate {
      get { return this.rateField; }
      set { this.rateField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductBaseRate : BaseRate {
    private string productIdField;

    private Money rateField;

    public string productId {
      get { return this.productIdField; }
      set { this.productIdField = value; }
    }

    public Money rate {
      get { return this.rateField; }
      set { this.rateField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BaseRateActionError : ApiError {
    private BaseRateActionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public BaseRateActionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "BaseRateActionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum BaseRateActionErrorReason {
    DUPLICATED_BASE_RATES,
    ACTIVE_BASE_RATE_ALREADY_EXISTS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "InventoryServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class InventoryService : DfpSoapClient, IInventoryService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public InventoryService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/InventoryService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdUnit createAdUnit(AdUnit adUnit) {
      object[] results = this.Invoke("createAdUnit", new object[] { adUnit });
      return ((AdUnit) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdUnit[] createAdUnits([System.Xml.Serialization.XmlElementAttribute("adUnits")]
AdUnit[] adUnits) {
      object[] results = this.Invoke("createAdUnits", new object[] { adUnits });
      return ((AdUnit[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdUnit getAdUnit(string adUnitId) {
      object[] results = this.Invoke("getAdUnit", new object[] { adUnitId });
      return ((AdUnit) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdUnitSize[] getAdUnitSizesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getAdUnitSizesByStatement", new object[] { filterStatement });
      return ((AdUnitSize[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdUnitPage getAdUnitsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getAdUnitsByStatement", new object[] { filterStatement });
      return ((AdUnitPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performAdUnitAction(AdUnitAction adUnitAction, Statement filterStatement) {
      object[] results = this.Invoke("performAdUnitAction", new object[] { adUnitAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdUnit updateAdUnit(AdUnit adUnit) {
      object[] results = this.Invoke("updateAdUnit", new object[] { adUnit });
      return ((AdUnit) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual AdUnit[] updateAdUnits([System.Xml.Serialization.XmlElementAttribute("adUnits")]
AdUnit[] adUnits) {
      object[] results = this.Invoke("updateAdUnits", new object[] { adUnits });
      return ((AdUnit[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RemoveAdUnitsFromPlacement))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateAdUnits))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AssignAdUnitsToPlacement))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveAdUnits))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateAdUnits))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class AdUnitAction {
    private string adUnitActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("AdUnitAction.Type")]
    public string AdUnitActionType {
      get { return this.adUnitActionTypeField; }
      set { this.adUnitActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RemoveAdUnitsFromPlacement : AdUnitAction {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateAdUnits : AdUnitAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ArchiveAdUnits : AdUnitAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateAdUnits : AdUnitAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdUnit {
    private string idField;

    private string parentIdField;

    private bool hasChildrenField;

    private bool hasChildrenFieldSpecified;

    private AdUnitParent[] parentPathField;

    private string nameField;

    private string descriptionField;

    private AdUnitTargetWindow targetWindowField;

    private bool targetWindowFieldSpecified;

    private InventoryStatus statusField;

    private bool statusFieldSpecified;

    private string adUnitCodeField;

    private AdUnitSize[] adUnitSizesField;

    private TargetPlatform targetPlatformField;

    private bool targetPlatformFieldSpecified;

    private MobilePlatform mobilePlatformField;

    private bool mobilePlatformFieldSpecified;

    private bool explicitlyTargetedField;

    private bool explicitlyTargetedFieldSpecified;

    private AdSenseSettingsInheritedProperty inheritedAdSenseSettingsField;

    private long partnerIdField;

    private bool partnerIdFieldSpecified;

    private LabelFrequencyCap[] appliedLabelFrequencyCapsField;

    private LabelFrequencyCap[] effectiveLabelFrequencyCapsField;

    private AppliedLabel[] appliedLabelsField;

    private AppliedLabel[] effectiveAppliedLabelsField;

    private long[] effectiveTeamIdsField;

    private long[] appliedTeamIdsField;

    private DateTime lastModifiedDateTimeField;

    private SmartSizeMode smartSizeModeField;

    private bool smartSizeModeFieldSpecified;

    public string id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    public string parentId {
      get { return this.parentIdField; }
      set { this.parentIdField = value; }
    }

    public bool hasChildren {
      get { return this.hasChildrenField; }
      set {
        this.hasChildrenField = value;
        this.hasChildrenSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool hasChildrenSpecified {
      get { return this.hasChildrenFieldSpecified; }
      set { this.hasChildrenFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("parentPath")]
    public AdUnitParent[] parentPath {
      get { return this.parentPathField; }
      set { this.parentPathField = value; }
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

    [System.Xml.Serialization.XmlElementAttribute("adUnitSizes")]
    public AdUnitSize[] adUnitSizes {
      get { return this.adUnitSizesField; }
      set { this.adUnitSizesField = value; }
    }

    public TargetPlatform targetPlatform {
      get { return this.targetPlatformField; }
      set {
        this.targetPlatformField = value;
        this.targetPlatformSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool targetPlatformSpecified {
      get { return this.targetPlatformFieldSpecified; }
      set { this.targetPlatformFieldSpecified = value; }
    }

    public MobilePlatform mobilePlatform {
      get { return this.mobilePlatformField; }
      set {
        this.mobilePlatformField = value;
        this.mobilePlatformSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool mobilePlatformSpecified {
      get { return this.mobilePlatformFieldSpecified; }
      set { this.mobilePlatformFieldSpecified = value; }
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

    public long partnerId {
      get { return this.partnerIdField; }
      set {
        this.partnerIdField = value;
        this.partnerIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool partnerIdSpecified {
      get { return this.partnerIdFieldSpecified; }
      set { this.partnerIdFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedLabelFrequencyCaps")]
    public LabelFrequencyCap[] appliedLabelFrequencyCaps {
      get { return this.appliedLabelFrequencyCapsField; }
      set { this.appliedLabelFrequencyCapsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("effectiveLabelFrequencyCaps")]
    public LabelFrequencyCap[] effectiveLabelFrequencyCaps {
      get { return this.effectiveLabelFrequencyCapsField; }
      set { this.effectiveLabelFrequencyCapsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedLabels")]
    public AppliedLabel[] appliedLabels {
      get { return this.appliedLabelsField; }
      set { this.appliedLabelsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("effectiveAppliedLabels")]
    public AppliedLabel[] effectiveAppliedLabels {
      get { return this.effectiveAppliedLabelsField; }
      set { this.effectiveAppliedLabelsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("effectiveTeamIds")]
    public long[] effectiveTeamIds {
      get { return this.effectiveTeamIdsField; }
      set { this.effectiveTeamIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedTeamIds")]
    public long[] appliedTeamIds {
      get { return this.appliedTeamIdsField; }
      set { this.appliedTeamIdsField = value; }
    }

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }

    public SmartSizeMode smartSizeMode {
      get { return this.smartSizeModeField; }
      set {
        this.smartSizeModeField = value;
        this.smartSizeModeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool smartSizeModeSpecified {
      get { return this.smartSizeModeFieldSpecified; }
      set { this.smartSizeModeFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InventoryStatus {
    ACTIVE,
    INACTIVE,
    ARCHIVED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum MobilePlatform {
    SITE,
    APPLICATION
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdSenseSettingsInheritedProperty {
    private AdSenseSettings valueField;

    public AdSenseSettings value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdSenseSettings.AdType", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdSenseSettingsAdType {
    TEXT,
    IMAGE,
    TEXT_AND_IMAGE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdSenseSettings.BorderStyle", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdSenseSettingsBorderStyle {
    DEFAULT,
    NOT_ROUNDED,
    SLIGHTLY_ROUNDED,
    VERY_ROUNDED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdSenseSettings.FontFamily", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdSenseSettingsFontFamily {
    DEFAULT,
    ARIAL,
    TAHOMA,
    GEORGIA,
    TIMES,
    VERDANA
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdSenseSettings.FontSize", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdSenseSettingsFontSize {
    DEFAULT,
    SMALL,
    MEDIUM,
    LARGE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LabelFrequencyCap {
    private FrequencyCap frequencyCapField;

    private long labelIdField;

    private bool labelIdFieldSpecified;

    public FrequencyCap frequencyCap {
      get { return this.frequencyCapField; }
      set { this.frequencyCapField = value; }
    }

    public long labelId {
      get { return this.labelIdField; }
      set {
        this.labelIdField = value;
        this.labelIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool labelIdSpecified {
      get { return this.labelIdFieldSpecified; }
      set { this.labelIdFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum SmartSizeMode {
    UNKNOWN,
    NONE,
    SMART_BANNER,
    DYNAMIC_SIZE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RegExError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RegExErrorReason {
    INVALID,
    NULL,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdUnitTypeError : ApiError {
    private AdUnitTypeErrorReason reasonField;

    private bool reasonFieldSpecified;

    public AdUnitTypeErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdUnitTypeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdUnitTypeErrorReason {
    MOBILE_APP_PLATFORM_NOT_VALID,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class InventoryUnitSizesError : ApiError {
    private InventoryUnitSizesErrorReason reasonField;

    private bool reasonFieldSpecified;

    public InventoryUnitSizesErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InventoryUnitSizesError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InventoryUnitSizesErrorReason {
    INVALID_SIZES,
    INVALID_SIZE_FOR_PLATFORM,
    VIDEO_FEATURE_MISSING,
    VIDEO_MOBILE_LINE_ITEM_FEATURE_MISSING,
    INVALID_SIZE_FOR_MASTER,
    INVALID_SIZE_FOR_COMPANION,
    DUPLICATE_MASTER_SIZES,
    ASPECT_RATIO_NOT_SUPPORTED,
    VIDEO_COMPANIONS_NOT_SUPPORTED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class InventoryUnitPartnerAssociationError : ApiError {
    private InventoryUnitPartnerAssociationErrorReason reasonField;

    private bool reasonFieldSpecified;

    public InventoryUnitPartnerAssociationErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InventoryUnitPartnerAssociationError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InventoryUnitPartnerAssociationErrorReason {
    ANCESTOR_AD_UNIT_HAS_PARTNER_ASSOCIATION,
    DESCENDANT_AD_UNIT_HAS_PARTNER_ASSOCIATION,
    SAME_PARTNER_ASSOCIATION_IN_INVENTORY_HIERARCHY,
    NO_PARTNER_CATCH_ALL,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InventoryUnitError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InventoryUnitErrorReason {
    EXPLICIT_TARGETING_NOT_ALLOWED,
    TARGET_PLATFORM_NOT_APPLICABLE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class InvalidColorError : ApiError {
    private InvalidColorErrorReason reasonField;

    private bool reasonFieldSpecified;

    public InvalidColorErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "InvalidColorError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum InvalidColorErrorReason {
    INVALID_FORMAT,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdUnitHierarchyError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdUnitHierarchyErrorReason {
    INVALID_DEPTH,
    INVALID_PARENT,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdUnitCodeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdUnitCodeErrorReason {
    INVALID_CHARACTERS,
    INVALID_CHARACTERS_WHEN_UTF_CHARACTERS_ARE_ALLOWED,
    LEADING_FORWARD_SLASH,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AdUnitAfcSizeError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AdUnitAfcSizeErrorReason {
    INVALID,
    DOESNT_FIT,
    NOT_APPLICABLE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ProductTemplateServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ProductTemplateService : DfpSoapClient, IProductTemplateService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ProductTemplateService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ProductTemplateService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProductTemplate createProductTemplate(ProductTemplate productTemplate) {
      object[] results = this.Invoke("createProductTemplate", new object[] { productTemplate });
      return ((ProductTemplate) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProductTemplate[] createProductTemplates([System.Xml.Serialization.XmlElementAttribute("productTemplates")]
ProductTemplate[] productTemplates) {
      object[] results = this.Invoke("createProductTemplates", new object[] { productTemplates });
      return ((ProductTemplate[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProductTemplate getProductTemplate(long productTemplateId) {
      object[] results = this.Invoke("getProductTemplate", new object[] { productTemplateId });
      return ((ProductTemplate) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProductTemplatePage getProductTemplatesByStatement(Statement statement) {
      object[] results = this.Invoke("getProductTemplatesByStatement", new object[] { statement });
      return ((ProductTemplatePage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performProductTemplateAction(ProductTemplateAction action, Statement filterStatement) {
      object[] results = this.Invoke("performProductTemplateAction", new object[] { action, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProductTemplate updateProductTemplate(ProductTemplate productTemplate) {
      object[] results = this.Invoke("updateProductTemplate", new object[] { productTemplate });
      return ((ProductTemplate) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProductTemplate[] updateProductTemplates([System.Xml.Serialization.XmlElementAttribute("productTemplates")]
ProductTemplate[] productTemplates) {
      object[] results = this.Invoke("updateProductTemplates", new object[] { productTemplates });
      return ((ProductTemplate[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateProductTemplates))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveProducTemplates))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateProductTemplates))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class ProductTemplateAction {
    private string productTemplateActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("ProductTemplateAction.Type")]
    public string ProductTemplateActionType {
      get { return this.productTemplateActionTypeField; }
      set { this.productTemplateActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateProductTemplates : ProductTemplateAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ArchiveProducTemplates : ProductTemplateAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateProductTemplates : ProductTemplateAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductTemplatePage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private ProductTemplate[] resultsField;

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
    public ProductTemplate[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductTemplate {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private DateTime creationDateTimeField;

    private DateTime lastModifiedDateTimeField;

    private string descriptionField;

    private string nameMacroField;

    private ProductTemplateStatus statusField;

    private bool statusFieldSpecified;

    private ProductType productTypeField;

    private bool productTypeFieldSpecified;

    private long creatorIdField;

    private bool creatorIdFieldSpecified;

    private RateType rateTypeField;

    private bool rateTypeFieldSpecified;

    private RoadblockingType roadblockingTypeField;

    private bool roadblockingTypeFieldSpecified;

    private CreativePlaceholder[] creativePlaceholdersField;

    private LineItemType lineItemTypeField;

    private bool lineItemTypeFieldSpecified;

    private int priorityField;

    private bool priorityFieldSpecified;

    private FrequencyCap[] frequencyCapsField;

    private bool allowFrequencyCapsCustomizationField;

    private bool allowFrequencyCapsCustomizationFieldSpecified;

    private ProductSegmentation productSegmentationField;

    private ProductTemplateTargeting targetingField;

    private BaseCustomFieldValue[] customFieldValuesField;

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

    public DateTime creationDateTime {
      get { return this.creationDateTimeField; }
      set { this.creationDateTimeField = value; }
    }

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }

    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }

    public string nameMacro {
      get { return this.nameMacroField; }
      set { this.nameMacroField = value; }
    }

    public ProductTemplateStatus status {
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

    public ProductType productType {
      get { return this.productTypeField; }
      set {
        this.productTypeField = value;
        this.productTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool productTypeSpecified {
      get { return this.productTypeFieldSpecified; }
      set { this.productTypeFieldSpecified = value; }
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

    public RateType rateType {
      get { return this.rateTypeField; }
      set {
        this.rateTypeField = value;
        this.rateTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool rateTypeSpecified {
      get { return this.rateTypeFieldSpecified; }
      set { this.rateTypeFieldSpecified = value; }
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

    [System.Xml.Serialization.XmlElementAttribute("creativePlaceholders")]
    public CreativePlaceholder[] creativePlaceholders {
      get { return this.creativePlaceholdersField; }
      set { this.creativePlaceholdersField = value; }
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

    public int priority {
      get { return this.priorityField; }
      set {
        this.priorityField = value;
        this.prioritySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool prioritySpecified {
      get { return this.priorityFieldSpecified; }
      set { this.priorityFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("frequencyCaps")]
    public FrequencyCap[] frequencyCaps {
      get { return this.frequencyCapsField; }
      set { this.frequencyCapsField = value; }
    }

    public bool allowFrequencyCapsCustomization {
      get { return this.allowFrequencyCapsCustomizationField; }
      set {
        this.allowFrequencyCapsCustomizationField = value;
        this.allowFrequencyCapsCustomizationSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowFrequencyCapsCustomizationSpecified {
      get { return this.allowFrequencyCapsCustomizationFieldSpecified; }
      set { this.allowFrequencyCapsCustomizationFieldSpecified = value; }
    }

    public ProductSegmentation productSegmentation {
      get { return this.productSegmentationField; }
      set { this.productSegmentationField = value; }
    }

    public ProductTemplateTargeting targeting {
      get { return this.targetingField; }
      set { this.targetingField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customFieldValues")]
    public BaseCustomFieldValue[] customFieldValues {
      get { return this.customFieldValuesField; }
      set { this.customFieldValuesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProductTemplateStatus {
    ACTIVE,
    DRAFT,
    CANCELED,
    ARCHIVED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductSegmentation {
    private GeoTargeting geoSegmentField;

    private AdUnitTargeting[] adUnitSegmentsField;

    private long[] placementSegmentField;

    private UserDomainTargeting userDomainSegmentField;

    private BandwidthGroupTargeting bandwidthSegmentField;

    private BrowserTargeting browserSegmentField;

    private BrowserLanguageTargeting browserLanguageSegmentField;

    private OperatingSystemTargeting operatingSystemSegmentField;

    public GeoTargeting geoSegment {
      get { return this.geoSegmentField; }
      set { this.geoSegmentField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("adUnitSegments")]
    public AdUnitTargeting[] adUnitSegments {
      get { return this.adUnitSegmentsField; }
      set { this.adUnitSegmentsField = value; }
    }

    [System.Xml.Serialization.XmlArrayItemAttribute("targetedPlacementIds", IsNullable = false)]
    public long[] placementSegment {
      get { return this.placementSegmentField; }
      set { this.placementSegmentField = value; }
    }

    public UserDomainTargeting userDomainSegment {
      get { return this.userDomainSegmentField; }
      set { this.userDomainSegmentField = value; }
    }

    public BandwidthGroupTargeting bandwidthSegment {
      get { return this.bandwidthSegmentField; }
      set { this.bandwidthSegmentField = value; }
    }

    public BrowserTargeting browserSegment {
      get { return this.browserSegmentField; }
      set { this.browserSegmentField = value; }
    }

    public BrowserLanguageTargeting browserLanguageSegment {
      get { return this.browserLanguageSegmentField; }
      set { this.browserLanguageSegmentField = value; }
    }

    public OperatingSystemTargeting operatingSystemSegment {
      get { return this.operatingSystemSegmentField; }
      set { this.operatingSystemSegmentField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductTemplateError : ApiError {
    private ProductTemplateErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ProductTemplateErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ProductTemplateError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProductTemplateErrorReason {
    INVALID_FEATURE_EXPANDED_EXCLUSIVE,
    INVALID_EXPANDED_FEATURE_DEFAULT_NOT_TARGETED,
    INVALID_EXPANDED_FEATURE_DEFAULT_LOCKED,
    INVALID_EXPANDED_FEATURE_VALUE_TARGETED,
    INVALID_EXPANDED_FEATURE_VALUE_LOCKED,
    INVALID_FEATURE_TYPE,
    INVALID_ROADBLOCKING_TYPE,
    INVALID_TARGETING,
    INVALID_FREQUENCY_CAPS,
    INVALID_TECHNOLOGY_INCLUDE_EXCLUDE,
    INVALID_EXPANDED_PRODUCT_COUNT,
    INVALID_TARGET_PLATFORM,
    INVALID_NON_TARGETING_FEATURE,
    INVALID_FEATURE_CARDINALITY_AT_LEAST_ONE,
    INVALID_FEATURE_CARDINALITY_AT_MOST_ONE,
    INVALID_FEATURE_CARDINALITY_EXACTLY_ONE,
    INVALID_FEATURE_FOR_OFFLINE,
    INVALID_RATE_TYPE_FOR_OFFLINE,
    INVALID_RATE_TYPE_FOR_DFP,
    INVALID_LINE_ITEM_PRIORITY,
    INVALID_LINE_ITEM_TYPE,
    DUPLICATED_PLACEHOLDER_IN_NAMEMACRO,
    INVALID_EXPANDED_FEATURE_IN_NON_EXPANDABLE_AFFINITY,
    INVALID_FEATURE_DEFAULT_TARGET_TYPE,
    INVALID_FEATURE_VALUE_TARGET_TYPE,
    INVALID_FEATURE_AND_VALUE_LOCK_EXCLUSIVE,
    DUPLICATED_FEATURE,
    DUPLICATED_CUSTOM_TARGETING_KEY,
    INVALID_CUSTOM_TARGETING_KEY_ID,
    INVALID_CUSTOM_TARGETING_VALUE_ID,
    DUPLICATED_FEATURE_VALUE,
    MISSING_EXPANDED_FEATURE_PLACEHOLDER_IN_NAMEMACRO,
    MISSING_FEATURE_VALUE_OF_NAMEMACRO_PLACEHOLDER,
    MISSING_FEATURE_OF_NAMEMACRO_PLACEHOLDER,
    MISSING_SUBTYPE_FOR_CUSTOM_TARGETING,
    COMPANION_NOT_ALLOWED,
    MISSING_COMPANION,
    CANNOT_MODIFY_READONLY_FEATURE,
    CANNOT_MODIFY_PRODUCT_TYPE,
    CANNOT_ADD_SEGMENTATION,
    CANNOT_REMOVE_SEGMENTATION,
    CANNOT_REMOVE_VALUE_FROM_SEGMENTATION,
    CANNOT_ADD_FEATURE_VALUE_FOR_CUSTOM_TARGETING,
    CANNOT_MODIFY_BUILTIN_TARGETING_FEATURE,
    CANNOT_UPDATE_ARCHIVED_PRODUCT_TEMPLATE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProductTemplateActionError : ApiError {
    private ProductTemplateActionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ProductTemplateActionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ProductTemplateActionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProductTemplateActionErrorReason {
    NOT_APPLICABLE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ForecastServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemSummary))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ForecastService : DfpSoapClient, IForecastService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ForecastService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ForecastService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Forecast getForecast(LineItem lineItem) {
      object[] results = this.Invoke("getForecast", new object[] { lineItem });
      return ((Forecast) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Forecast getForecastById(long lineItemId) {
      object[] results = this.Invoke("getForecastById", new object[] { lineItemId });
      return ((Forecast) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "UserServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UserRecord))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class UserService : DfpSoapClient, IUserService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public UserService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/UserService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual User createUser(User user) {
      object[] results = this.Invoke("createUser", new object[] { user });
      return ((User) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual User[] createUsers([System.Xml.Serialization.XmlElementAttribute("users")]
User[] users) {
      object[] results = this.Invoke("createUsers", new object[] { users });
      return ((User[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Role[] getAllRoles() {
      object[] results = this.Invoke("getAllRoles", new object[0]);
      return ((Role[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual User getCurrentUser() {
      object[] results = this.Invoke("getCurrentUser", new object[0]);
      return ((User) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual User getUser(long userId) {
      object[] results = this.Invoke("getUser", new object[] { userId });
      return ((User) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UserPage getUsersByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getUsersByStatement", new object[] { filterStatement });
      return ((UserPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performUserAction(UserAction userAction, Statement filterStatement) {
      object[] results = this.Invoke("performUserAction", new object[] { userAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual User updateUser(User user) {
      object[] results = this.Invoke("updateUser", new object[] { user });
      return ((User) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual User[] updateUsers([System.Xml.Serialization.XmlElementAttribute("users")]
User[] users) {
      object[] results = this.Invoke("updateUsers", new object[] { users });
      return ((User[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateUsers))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateUsers))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class UserAction {
    private string userActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("UserAction.Type")]
    public string UserActionType {
      get { return this.userActionTypeField; }
      set { this.userActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateUsers : UserAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateUsers : UserAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class User : UserRecord {
    private bool isActiveField;

    private bool isActiveFieldSpecified;

    private bool isEmailNotificationAllowedField;

    private bool isEmailNotificationAllowedFieldSpecified;

    private string externalIdField;

    private string ordersUiLocalTimeZoneIdField;

    private BaseCustomFieldValue[] customFieldValuesField;

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

    public string externalId {
      get { return this.externalIdField; }
      set { this.externalIdField = value; }
    }

    public string ordersUiLocalTimeZoneId {
      get { return this.ordersUiLocalTimeZoneIdField; }
      set { this.ordersUiLocalTimeZoneIdField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customFieldValues")]
    public BaseCustomFieldValue[] customFieldValues {
      get { return this.customFieldValuesField; }
      set { this.customFieldValuesField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(User))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "PublisherQueryLanguageServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class PublisherQueryLanguageService : DfpSoapClient, IPublisherQueryLanguageService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public PublisherQueryLanguageService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/PublisherQueryLanguageService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ResultSet select(Statement selectStatement) {
      object[] results = this.Invoke("select", new object[] { selectStatement });
      return ((ResultSet) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ColumnType {
    private string labelNameField;

    public string labelName {
      get { return this.labelNameField; }
      set { this.labelNameField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "OrderActionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum OrderActionErrorReason {
    PERMISSION_DENIED,
    NOT_APPLICABLE,
    IS_ARCHIVED,
    HAS_ENDED,
    CANNOT_APPROVE_WITH_UNRESERVED_LINE_ITEMS,
    CANNOT_DELETE_ORDER_WITH_DELIVERED_LINEITEMS,
    CANNOT_APPROVE_COMPANY_CREDIT_STATUS_NOT_ACTIVE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "TeamServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class TeamService : DfpSoapClient, ITeamService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public TeamService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/TeamService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Team createTeam(Team team) {
      object[] results = this.Invoke("createTeam", new object[] { team });
      return ((Team) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Team[] createTeams([System.Xml.Serialization.XmlElementAttribute("teams")]
Team[] teams) {
      object[] results = this.Invoke("createTeams", new object[] { teams });
      return ((Team[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Team getTeam(long teamId) {
      object[] results = this.Invoke("getTeam", new object[] { teamId });
      return ((Team) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual TeamPage getTeamsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getTeamsByStatement", new object[] { filterStatement });
      return ((TeamPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Team updateTeam(Team team) {
      object[] results = this.Invoke("updateTeam", new object[] { team });
      return ((Team) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Team[] updateTeams([System.Xml.Serialization.XmlElementAttribute("teams")]
Team[] teams) {
      object[] results = this.Invoke("updateTeams", new object[] { teams });
      return ((Team[]) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class TeamPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Team[] resultsField;

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
    public Team[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Team {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string descriptionField;

    private bool hasAllCompaniesField;

    private bool hasAllCompaniesFieldSpecified;

    private bool hasAllInventoryField;

    private bool hasAllInventoryFieldSpecified;

    private TeamAccessType teamAccessTypeField;

    private bool teamAccessTypeFieldSpecified;

    private long[] companyIdsField;

    private string[] adUnitIdsField;

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

    public bool hasAllCompanies {
      get { return this.hasAllCompaniesField; }
      set {
        this.hasAllCompaniesField = value;
        this.hasAllCompaniesSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool hasAllCompaniesSpecified {
      get { return this.hasAllCompaniesFieldSpecified; }
      set { this.hasAllCompaniesFieldSpecified = value; }
    }

    public bool hasAllInventory {
      get { return this.hasAllInventoryField; }
      set {
        this.hasAllInventoryField = value;
        this.hasAllInventorySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool hasAllInventorySpecified {
      get { return this.hasAllInventoryFieldSpecified; }
      set { this.hasAllInventoryFieldSpecified = value; }
    }

    public TeamAccessType teamAccessType {
      get { return this.teamAccessTypeField; }
      set {
        this.teamAccessTypeField = value;
        this.teamAccessTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool teamAccessTypeSpecified {
      get { return this.teamAccessTypeFieldSpecified; }
      set { this.teamAccessTypeFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("companyIds")]
    public long[] companyIds {
      get { return this.companyIdsField; }
      set { this.companyIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("adUnitIds")]
    public string[] adUnitIds {
      get { return this.adUnitIdsField; }
      set { this.adUnitIdsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "RateCardServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class RateCardService : DfpSoapClient, IRateCardService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public RateCardService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/RateCardService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCard createRateCard(RateCard rateCard) {
      object[] results = this.Invoke("createRateCard", new object[] { rateCard });
      return ((RateCard) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCard[] createRateCards([System.Xml.Serialization.XmlElementAttribute("rateCards")]
RateCard[] rateCards) {
      object[] results = this.Invoke("createRateCards", new object[] { rateCards });
      return ((RateCard[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCard getRateCard(long rateCardId) {
      object[] results = this.Invoke("getRateCard", new object[] { rateCardId });
      return ((RateCard) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCardPage getRateCardsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getRateCardsByStatement", new object[] { filterStatement });
      return ((RateCardPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performRateCardAction(RateCardAction rateCardAction, Statement filterStatement) {
      object[] results = this.Invoke("performRateCardAction", new object[] { rateCardAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCard updateRateCard(RateCard rateCard) {
      object[] results = this.Invoke("updateRateCard", new object[] { rateCard });
      return ((RateCard) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual RateCard[] updateRateCards([System.Xml.Serialization.XmlElementAttribute("rateCards")]
RateCard[] rateCards) {
      object[] results = this.Invoke("updateRateCards", new object[] { rateCards });
      return ((RateCard[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivateRateCards))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivateRateCards))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class RateCardAction {
    private string rateCardActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("RateCardAction.Type")]
    public string RateCardActionType {
      get { return this.rateCardActionTypeField; }
      set { this.rateCardActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivateRateCards : RateCardAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivateRateCards : RateCardAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RateCardPage {
    private RateCard[] resultsField;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("results")]
    public RateCard[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
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
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RateCard {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private RateCardStatus statusField;

    private bool statusFieldSpecified;

    private long[] appliedTeamIdsField;

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

    public RateCardStatus status {
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

    [System.Xml.Serialization.XmlElementAttribute("appliedTeamIds")]
    public long[] appliedTeamIds {
      get { return this.appliedTeamIdsField; }
      set { this.appliedTeamIdsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RateCardStatus {
    ACTIVE,
    INACTIVE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RateCardActionError : ApiError {
    private RateCardActionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public RateCardActionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RateCardActionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RateCardActionErrorReason {
    NOT_APPLICABLE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "PlacementServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SiteTargetingInfo))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class PlacementService : DfpSoapClient, IPlacementService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public PlacementService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/PlacementService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Placement createPlacement(Placement placement) {
      object[] results = this.Invoke("createPlacement", new object[] { placement });
      return ((Placement) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Placement[] createPlacements([System.Xml.Serialization.XmlElementAttribute("placements")]
Placement[] placements) {
      object[] results = this.Invoke("createPlacements", new object[] { placements });
      return ((Placement[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Placement getPlacement(long placementId) {
      object[] results = this.Invoke("getPlacement", new object[] { placementId });
      return ((Placement) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual PlacementPage getPlacementsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getPlacementsByStatement", new object[] { filterStatement });
      return ((PlacementPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performPlacementAction(PlacementAction placementAction, Statement filterStatement) {
      object[] results = this.Invoke("performPlacementAction", new object[] { placementAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Placement updatePlacement(Placement placement) {
      object[] results = this.Invoke("updatePlacement", new object[] { placement });
      return ((Placement) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Placement[] updatePlacements([System.Xml.Serialization.XmlElementAttribute("placements")]
Placement[] placements) {
      object[] results = this.Invoke("updatePlacements", new object[] { placements });
      return ((Placement[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeactivatePlacements))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchivePlacements))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ActivatePlacements))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class PlacementAction {
    private string placementActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("PlacementAction.Type")]
    public string PlacementActionType {
      get { return this.placementActionTypeField; }
      set { this.placementActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeactivatePlacements : PlacementAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ArchivePlacements : PlacementAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivatePlacements : PlacementAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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

    private DateTime lastModifiedDateTimeField;

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

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(Placement))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class PlacementError : ApiError {
    private PlacementErrorReason reasonField;

    private bool reasonFieldSpecified;

    public PlacementErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "PlacementError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum PlacementErrorReason {
    INVALID_ENTITY_TYPE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ReconciliationOrderReportServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ReconciliationOrderReportService : DfpSoapClient, IReconciliationOrderReportService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ReconciliationOrderReportService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ReconciliationOrderReportServic" + "e";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReconciliationOrderReport getReconciliationOrderReport(long reconciliationOrderReportId) {
      object[] results = this.Invoke("getReconciliationOrderReport", new object[] { reconciliationOrderReportId });
      return ((ReconciliationOrderReport) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ReconciliationOrderReportPage getReconciliationOrderReportsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getReconciliationOrderReportsByStatement", new object[] { filterStatement });
      return ((ReconciliationOrderReportPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performReconciliationOrderReportAction(ReconciliationOrderReportAction reconciliationOrderReportAction, Statement filterStatement) {
      object[] results = this.Invoke("performReconciliationOrderReportAction", new object[] { reconciliationOrderReportAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RevertReconciliationOrderReports))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitReconciliationOrderReports))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class ReconciliationOrderReportAction {
    private string reconciliationOrderReportActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("ReconciliationOrderReportAction.Type")]
    public string ReconciliationOrderReportActionType {
      get { return this.reconciliationOrderReportActionTypeField; }
      set { this.reconciliationOrderReportActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RevertReconciliationOrderReports : ReconciliationOrderReportAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SubmitReconciliationOrderReports : ReconciliationOrderReportAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReconciliationOrderReportPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private ReconciliationOrderReport[] resultsField;

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
    public ReconciliationOrderReport[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ReconciliationOrderReport {
    private long idField;

    private bool idFieldSpecified;

    private long reconciliationReportIdField;

    private bool reconciliationReportIdFieldSpecified;

    private long orderIdField;

    private bool orderIdFieldSpecified;

    private ReconciliationOrderReportStatus statusField;

    private bool statusFieldSpecified;

    private DateTime submissionDateTimeField;

    private long submitterIdField;

    private bool submitterIdFieldSpecified;

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

    public long reconciliationReportId {
      get { return this.reconciliationReportIdField; }
      set {
        this.reconciliationReportIdField = value;
        this.reconciliationReportIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool reconciliationReportIdSpecified {
      get { return this.reconciliationReportIdFieldSpecified; }
      set { this.reconciliationReportIdFieldSpecified = value; }
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

    public ReconciliationOrderReportStatus status {
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

    public DateTime submissionDateTime {
      get { return this.submissionDateTimeField; }
      set { this.submissionDateTimeField = value; }
    }

    public long submitterId {
      get { return this.submitterIdField; }
      set {
        this.submitterIdField = value;
        this.submitterIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool submitterIdSpecified {
      get { return this.submitterIdFieldSpecified; }
      set { this.submitterIdFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ReconciliationOrderReportStatus {
    DRAFT,
    RECONCILED,
    REVERTED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ActivityServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ActivityService : DfpSoapClient, IActivityService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ActivityService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ActivityService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Activity[] createActivities([System.Xml.Serialization.XmlElementAttribute("activities")]
Activity[] activities) {
      object[] results = this.Invoke("createActivities", new object[] { activities });
      return ((Activity[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Activity createActivity(Activity activity) {
      object[] results = this.Invoke("createActivity", new object[] { activity });
      return ((Activity) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ActivityPage getActivitiesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getActivitiesByStatement", new object[] { filterStatement });
      return ((ActivityPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Activity getActivity(int activityId) {
      object[] results = this.Invoke("getActivity", new object[] { activityId });
      return ((Activity) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Activity[] updateActivities([System.Xml.Serialization.XmlElementAttribute("activities")]
Activity[] activities) {
      object[] results = this.Invoke("updateActivities", new object[] { activities });
      return ((Activity[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Activity updateActivity(Activity activity) {
      object[] results = this.Invoke("updateActivity", new object[] { activity });
      return ((Activity) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ActivityPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Activity[] resultsField;

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
    public Activity[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Activity {
    private int idField;

    private bool idFieldSpecified;

    private int activityGroupIdField;

    private bool activityGroupIdFieldSpecified;

    private string nameField;

    private string expectedURLField;

    private ActivityStatus statusField;

    private bool statusFieldSpecified;

    private ActivityType typeField;

    private bool typeFieldSpecified;

    public int id {
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

    public int activityGroupId {
      get { return this.activityGroupIdField; }
      set {
        this.activityGroupIdField = value;
        this.activityGroupIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool activityGroupIdSpecified {
      get { return this.activityGroupIdFieldSpecified; }
      set { this.activityGroupIdFieldSpecified = value; }
    }

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public string expectedURL {
      get { return this.expectedURLField; }
      set { this.expectedURLField = value; }
    }

    public ActivityStatus status {
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

    public ActivityType type {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Activity.Status", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ActivityStatus {
    ACTIVE,
    INACTIVE
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Activity.Type", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ActivityType {
    PAGE_VIEWS,
    DAILY_VISITS,
    CUSTOM,
    ITEMS_PURCHASED,
    TRANSACTIONS,
    IOS_APPLICATION_DOWNLOADS,
    ANDROID_APPLICATION_DOWNLOADS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "OrderServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class OrderService : DfpSoapClient, IOrderService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public OrderService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/OrderService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Order createOrder(Order order) {
      object[] results = this.Invoke("createOrder", new object[] { order });
      return ((Order) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Order[] createOrders([System.Xml.Serialization.XmlElementAttribute("orders")]
Order[] orders) {
      object[] results = this.Invoke("createOrders", new object[] { orders });
      return ((Order[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Order getOrder(long orderId) {
      object[] results = this.Invoke("getOrder", new object[] { orderId });
      return ((Order) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual OrderPage getOrdersByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getOrdersByStatement", new object[] { filterStatement });
      return ((OrderPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performOrderAction(OrderAction orderAction, Statement filterStatement) {
      object[] results = this.Invoke("performOrderAction", new object[] { orderAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Order updateOrder(Order order) {
      object[] results = this.Invoke("updateOrder", new object[] { order });
      return ((Order) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Order[] updateOrders([System.Xml.Serialization.XmlElementAttribute("orders")]
Order[] orders) {
      object[] results = this.Invoke("updateOrders", new object[] { orders });
      return ((Order[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UnarchiveOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitOrdersForApprovalWithoutReservationChanges))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitOrdersForApproval))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitOrdersForApprovalAndOverbook))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetractOrdersWithoutReservationChanges))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetractOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeAndOverbookOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(PauseOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DisapproveOrdersWithoutReservationChanges))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DisapproveOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApproveOrdersWithoutReservationChanges))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApproveOrders))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApproveAndOverbookOrders))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class OrderAction {
    private string orderActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("OrderAction.Type")]
    public string OrderActionType {
      get { return this.orderActionTypeField; }
      set { this.orderActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UnarchiveOrders : OrderAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SubmitOrdersForApprovalWithoutReservationChanges : OrderAction {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitOrdersForApprovalAndOverbook))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SubmitOrdersForApproval : OrderAction {
    private bool skipInventoryCheckField;

    private bool skipInventoryCheckFieldSpecified;

    public bool skipInventoryCheck {
      get { return this.skipInventoryCheckField; }
      set {
        this.skipInventoryCheckField = value;
        this.skipInventoryCheckSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool skipInventoryCheckSpecified {
      get { return this.skipInventoryCheckFieldSpecified; }
      set { this.skipInventoryCheckFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SubmitOrdersForApprovalAndOverbook : SubmitOrdersForApproval {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RetractOrdersWithoutReservationChanges : OrderAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RetractOrders : OrderAction {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResumeAndOverbookOrders))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ResumeOrders : OrderAction {
    private bool skipInventoryCheckField;

    private bool skipInventoryCheckFieldSpecified;

    public bool skipInventoryCheck {
      get { return this.skipInventoryCheckField; }
      set {
        this.skipInventoryCheckField = value;
        this.skipInventoryCheckSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool skipInventoryCheckSpecified {
      get { return this.skipInventoryCheckFieldSpecified; }
      set { this.skipInventoryCheckFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ResumeAndOverbookOrders : ResumeOrders {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class PauseOrders : OrderAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DisapproveOrdersWithoutReservationChanges : OrderAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DisapproveOrders : OrderAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeleteOrders : OrderAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ArchiveOrders : OrderAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ApproveOrdersWithoutReservationChanges : OrderAction {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApproveAndOverbookOrders))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ApproveOrders : OrderAction {
    private bool skipInventoryCheckField;

    private bool skipInventoryCheckFieldSpecified;

    public bool skipInventoryCheck {
      get { return this.skipInventoryCheckField; }
      set {
        this.skipInventoryCheckField = value;
        this.skipInventoryCheckSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool skipInventoryCheckSpecified {
      get { return this.skipInventoryCheckFieldSpecified; }
      set { this.skipInventoryCheckFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ApproveAndOverbookOrders : ApproveOrders {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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

    private int externalOrderIdField;

    private bool externalOrderIdFieldSpecified;

    private string poNumberField;

    private string currencyCodeField;

    private long advertiserIdField;

    private bool advertiserIdFieldSpecified;

    private long[] advertiserContactIdsField;

    private long agencyIdField;

    private bool agencyIdFieldSpecified;

    private long[] agencyContactIdsField;

    private long creatorIdField;

    private bool creatorIdFieldSpecified;

    private long traffickerIdField;

    private bool traffickerIdFieldSpecified;

    private long[] secondaryTraffickerIdsField;

    private long salespersonIdField;

    private bool salespersonIdFieldSpecified;

    private long[] secondarySalespersonIdsField;

    private long totalImpressionsDeliveredField;

    private bool totalImpressionsDeliveredFieldSpecified;

    private long totalClicksDeliveredField;

    private bool totalClicksDeliveredFieldSpecified;

    private Money totalBudgetField;

    private AppliedLabel[] appliedLabelsField;

    private AppliedLabel[] effectiveAppliedLabelsField;

    private string lastModifiedByAppField;

    private long[] appliedTeamIdsField;

    private DateTime lastModifiedDateTimeField;

    private BaseCustomFieldValue[] customFieldValuesField;

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

    public int externalOrderId {
      get { return this.externalOrderIdField; }
      set {
        this.externalOrderIdField = value;
        this.externalOrderIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool externalOrderIdSpecified {
      get { return this.externalOrderIdFieldSpecified; }
      set { this.externalOrderIdFieldSpecified = value; }
    }

    public string poNumber {
      get { return this.poNumberField; }
      set { this.poNumberField = value; }
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

    [System.Xml.Serialization.XmlElementAttribute("advertiserContactIds")]
    public long[] advertiserContactIds {
      get { return this.advertiserContactIdsField; }
      set { this.advertiserContactIdsField = value; }
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

    [System.Xml.Serialization.XmlElementAttribute("agencyContactIds")]
    public long[] agencyContactIds {
      get { return this.agencyContactIdsField; }
      set { this.agencyContactIdsField = value; }
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

    [System.Xml.Serialization.XmlElementAttribute("secondaryTraffickerIds")]
    public long[] secondaryTraffickerIds {
      get { return this.secondaryTraffickerIdsField; }
      set { this.secondaryTraffickerIdsField = value; }
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

    [System.Xml.Serialization.XmlElementAttribute("secondarySalespersonIds")]
    public long[] secondarySalespersonIds {
      get { return this.secondarySalespersonIdsField; }
      set { this.secondarySalespersonIdsField = value; }
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

    [System.Xml.Serialization.XmlElementAttribute("appliedLabels")]
    public AppliedLabel[] appliedLabels {
      get { return this.appliedLabelsField; }
      set { this.appliedLabelsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("effectiveAppliedLabels")]
    public AppliedLabel[] effectiveAppliedLabels {
      get { return this.effectiveAppliedLabelsField; }
      set { this.effectiveAppliedLabelsField = value; }
    }

    public string lastModifiedByApp {
      get { return this.lastModifiedByAppField; }
      set { this.lastModifiedByAppField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedTeamIds")]
    public long[] appliedTeamIds {
      get { return this.appliedTeamIdsField; }
      set { this.appliedTeamIdsField = value; }
    }

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customFieldValues")]
    public BaseCustomFieldValue[] customFieldValues {
      get { return this.customFieldValuesField; }
      set { this.customFieldValuesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum OrderStatus {
    DRAFT,
    PENDING_APPROVAL,
    APPROVED,
    DISAPPROVED,
    PAUSED,
    CANCELED,
    DELETED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ProposalServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ProposalService : DfpSoapClient, IProposalService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ProposalService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ProposalService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Proposal createProposal(Proposal proposal) {
      object[] results = this.Invoke("createProposal", new object[] { proposal });
      return ((Proposal) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Proposal[] createProposals([System.Xml.Serialization.XmlElementAttribute("proposals")]
Proposal[] proposals) {
      object[] results = this.Invoke("createProposals", new object[] { proposals });
      return ((Proposal[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Proposal getProposal(long proposalId) {
      object[] results = this.Invoke("getProposal", new object[] { proposalId });
      return ((Proposal) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ProposalPage getProposalsByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getProposalsByStatement", new object[] { filterStatement });
      return ((ProposalPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performProposalAction(ProposalAction proposalAction, Statement filterStatement) {
      object[] results = this.Invoke("performProposalAction", new object[] { proposalAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Proposal updateProposal(Proposal proposal) {
      object[] results = this.Invoke("updateProposal", new object[] { proposal });
      return ((Proposal) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Proposal[] updateProposals([System.Xml.Serialization.XmlElementAttribute("proposals")]
Proposal[] proposals) {
      object[] results = this.Invoke("updateProposals", new object[] { proposals });
      return ((Proposal[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UnarchiveProposals))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitProposalsForApproval))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RetractProposals))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RejectProposals))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ArchiveProposals))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApproveProposals))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class ProposalAction {
    private string proposalActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("ProposalAction.Type")]
    public string ProposalActionType {
      get { return this.proposalActionTypeField; }
      set { this.proposalActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UnarchiveProposals : ProposalAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SubmitProposalsForApproval : ProposalAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RetractProposals : ProposalAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RejectProposals : ProposalAction {
    private long[] workflowActionIdsField;

    private string commentField;

    [System.Xml.Serialization.XmlElementAttribute("workflowActionIds")]
    public long[] workflowActionIds {
      get { return this.workflowActionIdsField; }
      set { this.workflowActionIdsField = value; }
    }

    public string comment {
      get { return this.commentField; }
      set { this.commentField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ArchiveProposals : ProposalAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ApproveProposals : ProposalAction {
    private long[] workflowActionIdsField;

    [System.Xml.Serialization.XmlElementAttribute("workflowActionIds")]
    public long[] workflowActionIds {
      get { return this.workflowActionIdsField; }
      set { this.workflowActionIdsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProposalPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Proposal[] resultsField;

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
    public Proposal[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Proposal {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private DateTime startDateTimeField;

    private DateTime endDateTimeField;

    private ProposalStatus statusField;

    private bool statusFieldSpecified;

    private bool isArchivedField;

    private bool isArchivedFieldSpecified;

    private ProposalCompanyAssociation advertiserField;

    private ProposalCompanyAssociation[] agenciesField;

    private int probabilityToCloseField;

    private bool probabilityToCloseFieldSpecified;

    private BillingCap billingCapField;

    private bool billingCapFieldSpecified;

    private BillingSchedule billingScheduleField;

    private bool billingScheduleFieldSpecified;

    private BillingSource billingSourceField;

    private bool billingSourceFieldSpecified;

    private string poNumberField;

    private string notesField;

    private Money budgetField;

    private SalespersonSplit primarySalespersonField;

    private SalespersonSplit[] secondarySalespeopleField;

    private long[] salesPlannerIdsField;

    private long primaryTraffickerIdField;

    private bool primaryTraffickerIdFieldSpecified;

    private long[] secondaryTraffickerIdsField;

    private long[] appliedTeamIdsField;

    private BaseCustomFieldValue[] customFieldValuesField;

    private AppliedLabel[] appliedLabelsField;

    private AppliedLabel[] effectiveAppliedLabelsField;

    private long advertiserDiscountField;

    private bool advertiserDiscountFieldSpecified;

    private long proposalDiscountField;

    private bool proposalDiscountFieldSpecified;

    private Money additionalAdjustmentField;

    private long exchangeRateField;

    private bool exchangeRateFieldSpecified;

    private bool refreshExchangeRateField;

    private bool refreshExchangeRateFieldSpecified;

    private long agencyCommissionField;

    private bool agencyCommissionFieldSpecified;

    private long valueAddedTaxField;

    private bool valueAddedTaxFieldSpecified;

    private long[] approvalWorkflowActionIdsField;

    private ProposalApprovalStatus approvalStatusField;

    private bool approvalStatusFieldSpecified;

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

    public ProposalStatus status {
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

    public ProposalCompanyAssociation advertiser {
      get { return this.advertiserField; }
      set { this.advertiserField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("agencies")]
    public ProposalCompanyAssociation[] agencies {
      get { return this.agenciesField; }
      set { this.agenciesField = value; }
    }

    public int probabilityToClose {
      get { return this.probabilityToCloseField; }
      set {
        this.probabilityToCloseField = value;
        this.probabilityToCloseSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool probabilityToCloseSpecified {
      get { return this.probabilityToCloseFieldSpecified; }
      set { this.probabilityToCloseFieldSpecified = value; }
    }

    public BillingCap billingCap {
      get { return this.billingCapField; }
      set {
        this.billingCapField = value;
        this.billingCapSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool billingCapSpecified {
      get { return this.billingCapFieldSpecified; }
      set { this.billingCapFieldSpecified = value; }
    }

    public BillingSchedule billingSchedule {
      get { return this.billingScheduleField; }
      set {
        this.billingScheduleField = value;
        this.billingScheduleSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool billingScheduleSpecified {
      get { return this.billingScheduleFieldSpecified; }
      set { this.billingScheduleFieldSpecified = value; }
    }

    public BillingSource billingSource {
      get { return this.billingSourceField; }
      set {
        this.billingSourceField = value;
        this.billingSourceSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool billingSourceSpecified {
      get { return this.billingSourceFieldSpecified; }
      set { this.billingSourceFieldSpecified = value; }
    }

    public string poNumber {
      get { return this.poNumberField; }
      set { this.poNumberField = value; }
    }

    public string notes {
      get { return this.notesField; }
      set { this.notesField = value; }
    }

    public Money budget {
      get { return this.budgetField; }
      set { this.budgetField = value; }
    }

    public SalespersonSplit primarySalesperson {
      get { return this.primarySalespersonField; }
      set { this.primarySalespersonField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("secondarySalespeople")]
    public SalespersonSplit[] secondarySalespeople {
      get { return this.secondarySalespeopleField; }
      set { this.secondarySalespeopleField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("salesPlannerIds")]
    public long[] salesPlannerIds {
      get { return this.salesPlannerIdsField; }
      set { this.salesPlannerIdsField = value; }
    }

    public long primaryTraffickerId {
      get { return this.primaryTraffickerIdField; }
      set {
        this.primaryTraffickerIdField = value;
        this.primaryTraffickerIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool primaryTraffickerIdSpecified {
      get { return this.primaryTraffickerIdFieldSpecified; }
      set { this.primaryTraffickerIdFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("secondaryTraffickerIds")]
    public long[] secondaryTraffickerIds {
      get { return this.secondaryTraffickerIdsField; }
      set { this.secondaryTraffickerIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedTeamIds")]
    public long[] appliedTeamIds {
      get { return this.appliedTeamIdsField; }
      set { this.appliedTeamIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customFieldValues")]
    public BaseCustomFieldValue[] customFieldValues {
      get { return this.customFieldValuesField; }
      set { this.customFieldValuesField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("appliedLabels")]
    public AppliedLabel[] appliedLabels {
      get { return this.appliedLabelsField; }
      set { this.appliedLabelsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("effectiveAppliedLabels")]
    public AppliedLabel[] effectiveAppliedLabels {
      get { return this.effectiveAppliedLabelsField; }
      set { this.effectiveAppliedLabelsField = value; }
    }

    public long advertiserDiscount {
      get { return this.advertiserDiscountField; }
      set {
        this.advertiserDiscountField = value;
        this.advertiserDiscountSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool advertiserDiscountSpecified {
      get { return this.advertiserDiscountFieldSpecified; }
      set { this.advertiserDiscountFieldSpecified = value; }
    }

    public long proposalDiscount {
      get { return this.proposalDiscountField; }
      set {
        this.proposalDiscountField = value;
        this.proposalDiscountSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool proposalDiscountSpecified {
      get { return this.proposalDiscountFieldSpecified; }
      set { this.proposalDiscountFieldSpecified = value; }
    }

    public Money additionalAdjustment {
      get { return this.additionalAdjustmentField; }
      set { this.additionalAdjustmentField = value; }
    }

    public long exchangeRate {
      get { return this.exchangeRateField; }
      set {
        this.exchangeRateField = value;
        this.exchangeRateSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool exchangeRateSpecified {
      get { return this.exchangeRateFieldSpecified; }
      set { this.exchangeRateFieldSpecified = value; }
    }

    public bool refreshExchangeRate {
      get { return this.refreshExchangeRateField; }
      set {
        this.refreshExchangeRateField = value;
        this.refreshExchangeRateSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool refreshExchangeRateSpecified {
      get { return this.refreshExchangeRateFieldSpecified; }
      set { this.refreshExchangeRateFieldSpecified = value; }
    }

    public long agencyCommission {
      get { return this.agencyCommissionField; }
      set {
        this.agencyCommissionField = value;
        this.agencyCommissionSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool agencyCommissionSpecified {
      get { return this.agencyCommissionFieldSpecified; }
      set { this.agencyCommissionFieldSpecified = value; }
    }

    public long valueAddedTax {
      get { return this.valueAddedTaxField; }
      set {
        this.valueAddedTaxField = value;
        this.valueAddedTaxSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool valueAddedTaxSpecified {
      get { return this.valueAddedTaxFieldSpecified; }
      set { this.valueAddedTaxFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("approvalWorkflowActionIds")]
    public long[] approvalWorkflowActionIds {
      get { return this.approvalWorkflowActionIdsField; }
      set { this.approvalWorkflowActionIdsField = value; }
    }

    public ProposalApprovalStatus approvalStatus {
      get { return this.approvalStatusField; }
      set {
        this.approvalStatusField = value;
        this.approvalStatusSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool approvalStatusSpecified {
      get { return this.approvalStatusFieldSpecified; }
      set { this.approvalStatusFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProposalStatus {
    DRAFT,
    PENDING_APPROVAL,
    APPROVED,
    REJECTED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProposalCompanyAssociation {
    private long companyIdField;

    private bool companyIdFieldSpecified;

    private ProposalCompanyAssociationType typeField;

    private bool typeFieldSpecified;

    private long[] contactIdsField;

    public long companyId {
      get { return this.companyIdField; }
      set {
        this.companyIdField = value;
        this.companyIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool companyIdSpecified {
      get { return this.companyIdFieldSpecified; }
      set { this.companyIdFieldSpecified = value; }
    }

    public ProposalCompanyAssociationType type {
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

    [System.Xml.Serialization.XmlElementAttribute("contactIds")]
    public long[] contactIds {
      get { return this.contactIdsField; }
      set { this.contactIdsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProposalCompanyAssociationType {
    PRIMARY_AGENCY,
    BILLING_AGENCY,
    BRANDING_AGENCY,
    OTHER_AGENCY,
    ADVERTISER,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SalespersonSplit {
    private long userIdField;

    private bool userIdFieldSpecified;

    private int splitField;

    private bool splitFieldSpecified;

    public long userId {
      get { return this.userIdField; }
      set {
        this.userIdField = value;
        this.userIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool userIdSpecified {
      get { return this.userIdFieldSpecified; }
      set { this.userIdFieldSpecified = value; }
    }

    public int split {
      get { return this.splitField; }
      set {
        this.splitField = value;
        this.splitSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool splitSpecified {
      get { return this.splitFieldSpecified; }
      set { this.splitFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProposalApprovalStatus {
    PENDING,
    NON_PENDING,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class WorkflowActionError : ApiError {
    private WorkflowActionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public WorkflowActionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "WorkflowActionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum WorkflowActionErrorReason {
    NOT_APPLICABLE,
    WORKFLOW_DEFINITION_NOT_FOUND,
    EMPTY_ACTION_LIST,
    NOT_ACTION_APPROVER,
    WORKFLOW_ALREADY_COMPLETED,
    WORKFLOW_ALREADY_FAILED,
    WORKFLOW_ALREADY_CANCELED,
    ACTION_COMPLETED,
    ACTION_FAILED,
    ACTION_CANCELED,
    ACTION_NOT_ACTIVE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ProposalActionError : ApiError {
    private ProposalActionErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ProposalActionErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ProposalActionError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ProposalActionErrorReason {
    NOT_APPLICABLE,
    IS_ARCHIVED,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CreativeTemplateServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CreativeTemplateService : DfpSoapClient, ICreativeTemplateService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CreativeTemplateService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/CreativeTemplateService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeTemplate getCreativeTemplate(long creativeTemplateId) {
      object[] results = this.Invoke("getCreativeTemplate", new object[] { creativeTemplateId });
      return ((CreativeTemplate) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativeTemplatePage getCreativeTemplatesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCreativeTemplatesByStatement", new object[] { filterStatement });
      return ((CreativeTemplatePage) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeTemplatePage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private CreativeTemplate[] resultsField;

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
    public CreativeTemplate[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeTemplate {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private string descriptionField;

    private CreativeTemplateVariable[] variablesField;

    private CreativeTemplateStatus statusField;

    private bool statusFieldSpecified;

    private CreativeTemplateType typeField;

    private bool typeFieldSpecified;

    private bool isInterstitialField;

    private bool isInterstitialFieldSpecified;

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

    [System.Xml.Serialization.XmlElementAttribute("variables")]
    public CreativeTemplateVariable[] variables {
      get { return this.variablesField; }
      set { this.variablesField = value; }
    }

    public CreativeTemplateStatus status {
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

    public CreativeTemplateType type {
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

    public bool isInterstitial {
      get { return this.isInterstitialField; }
      set {
        this.isInterstitialField = value;
        this.isInterstitialSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isInterstitialSpecified {
      get { return this.isInterstitialFieldSpecified; }
      set { this.isInterstitialFieldSpecified = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UrlCreativeTemplateVariable))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(StringCreativeTemplateVariable))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ListStringCreativeTemplateVariable))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LongCreativeTemplateVariable))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AssetCreativeTemplateVariable))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class CreativeTemplateVariable {
    private string labelField;

    private string uniqueNameField;

    private string descriptionField;

    private bool isRequiredField;

    private bool isRequiredFieldSpecified;

    private string creativeTemplateVariableTypeField;

    public string label {
      get { return this.labelField; }
      set { this.labelField = value; }
    }

    public string uniqueName {
      get { return this.uniqueNameField; }
      set { this.uniqueNameField = value; }
    }

    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }

    public bool isRequired {
      get { return this.isRequiredField; }
      set {
        this.isRequiredField = value;
        this.isRequiredSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isRequiredSpecified {
      get { return this.isRequiredFieldSpecified; }
      set { this.isRequiredFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("CreativeTemplateVariable.Type")]
    public string CreativeTemplateVariableType {
      get { return this.creativeTemplateVariableTypeField; }
      set { this.creativeTemplateVariableTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UrlCreativeTemplateVariable : CreativeTemplateVariable {
    private string defaultValueField;

    public string defaultValue {
      get { return this.defaultValueField; }
      set { this.defaultValueField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ListStringCreativeTemplateVariable))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class StringCreativeTemplateVariable : CreativeTemplateVariable {
    private string defaultValueField;

    public string defaultValue {
      get { return this.defaultValueField; }
      set { this.defaultValueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ListStringCreativeTemplateVariable : StringCreativeTemplateVariable {
    private ListStringCreativeTemplateVariableVariableChoice[] choicesField;

    private bool allowOtherChoiceField;

    private bool allowOtherChoiceFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("choices")]
    public ListStringCreativeTemplateVariableVariableChoice[] choices {
      get { return this.choicesField; }
      set { this.choicesField = value; }
    }

    public bool allowOtherChoice {
      get { return this.allowOtherChoiceField; }
      set {
        this.allowOtherChoiceField = value;
        this.allowOtherChoiceSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowOtherChoiceSpecified {
      get { return this.allowOtherChoiceFieldSpecified; }
      set { this.allowOtherChoiceFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ListStringCreativeTemplateVariable.VariableChoice", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ListStringCreativeTemplateVariableVariableChoice {
    private string labelField;

    private string valueField;

    public string label {
      get { return this.labelField; }
      set { this.labelField = value; }
    }

    public string value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LongCreativeTemplateVariable : CreativeTemplateVariable {
    private long defaultValueField;

    private bool defaultValueFieldSpecified;

    public long defaultValue {
      get { return this.defaultValueField; }
      set {
        this.defaultValueField = value;
        this.defaultValueSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool defaultValueSpecified {
      get { return this.defaultValueFieldSpecified; }
      set { this.defaultValueFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AssetCreativeTemplateVariable : CreativeTemplateVariable {
    private AssetCreativeTemplateVariableMimeType[] mimeTypesField;

    [System.Xml.Serialization.XmlElementAttribute("mimeTypes")]
    public AssetCreativeTemplateVariableMimeType[] mimeTypes {
      get { return this.mimeTypesField; }
      set { this.mimeTypesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "AssetCreativeTemplateVariable.MimeType", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum AssetCreativeTemplateVariableMimeType {
    JPG,
    PNG,
    GIF,
    SWF
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeTemplateStatus {
    ACTIVE,
    INACTIVE,
    DELETED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeTemplateType {
    SYSTEM_DEFINED,
    USER_DEFINED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeTemplateError : ApiError {
    private CreativeTemplateErrorReason reasonField;

    private bool reasonFieldSpecified;

    public CreativeTemplateErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CreativeTemplateError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CreativeTemplateErrorReason {
    CANNOT_PARSE_CREATIVE_TEMPLATE,
    VARIABLE_DUPLICATE_UNIQUE_NAME,
    VARIABLE_INVALID_UNIQUE_NAME,
    LIST_CHOICE_DUPLICATE_VALUE,
    LIST_CHOICE_NEEDS_DEFAULT,
    LIST_CHOICES_EMPTY,
    NO_TARGET_PLATFORMS,
    MULTIPLE_TARGET_PLATFORMS,
    UNRECOGNIZED_PLACEHOLDER,
    PLACEHOLDERS_NOT_IN_FORMATTER,
    MISSING_INTERSTITIAL_MACRO,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CreativeServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(Asset))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CreativeService : DfpSoapClient, ICreativeService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CreativeService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/CreativeService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Creative createCreative(Creative creative) {
      object[] results = this.Invoke("createCreative", new object[] { creative });
      return ((Creative) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Creative[] createCreatives([System.Xml.Serialization.XmlElementAttribute("creatives")]
Creative[] creatives) {
      object[] results = this.Invoke("createCreatives", new object[] { creatives });
      return ((Creative[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Creative getCreative(long creativeId) {
      object[] results = this.Invoke("getCreative", new object[] { creativeId });
      return ((Creative) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CreativePage getCreativesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCreativesByStatement", new object[] { filterStatement });
      return ((CreativePage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Creative updateCreative(Creative creative) {
      object[] results = this.Invoke("updateCreative", new object[] { creative });
      return ((Creative) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Creative[] updateCreatives([System.Xml.Serialization.XmlElementAttribute("creatives")]
Creative[] creatives) {
      object[] results = this.Invoke("updateCreatives", new object[] { creatives });
      return ((Creative[]) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VastRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UnsupportedCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ThirdPartyCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(TemplateCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LegacyDfpCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(InternalRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(HasDestinationUrlCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VpaidLinearRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VpaidLinearCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LegacyDfpMobileCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseVideoCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VideoRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VideoCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseImageRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageRedirectOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseImageCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseFlashRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashRedirectOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseFlashCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AspectRatioImageCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ClickTrackingCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseRichMediaStudioCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RichMediaStudioCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseDynamicAllocationCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(HasHtmlSnippetDynamicAllocationCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdSenseCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdExchangeCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class Creative {
    private long advertiserIdField;

    private bool advertiserIdFieldSpecified;

    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private Size sizeField;

    private string previewUrlField;

    private AppliedLabel[] appliedLabelsField;

    private DateTime lastModifiedDateTimeField;

    private BaseCustomFieldValue[] customFieldValuesField;

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

    [System.Xml.Serialization.XmlElementAttribute("appliedLabels")]
    public AppliedLabel[] appliedLabels {
      get { return this.appliedLabelsField; }
      set { this.appliedLabelsField = value; }
    }

    public DateTime lastModifiedDateTime {
      get { return this.lastModifiedDateTimeField; }
      set { this.lastModifiedDateTimeField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customFieldValues")]
    public BaseCustomFieldValue[] customFieldValues {
      get { return this.customFieldValuesField; }
      set { this.customFieldValuesField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("Creative.Type")]
    public string CreativeType {
      get { return this.creativeTypeField; }
      set { this.creativeTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VastRedirectCreative : Creative {
    private string vastXmlUrlField;

    private VastRedirectType vastRedirectTypeField;

    private bool vastRedirectTypeFieldSpecified;

    private int durationField;

    private bool durationFieldSpecified;

    private long[] companionCreativeIdsField;

    private ConversionEvent_TrackingUrlsMapEntry[] trackingUrlsField;

    private string vastPreviewUrlField;

    public string vastXmlUrl {
      get { return this.vastXmlUrlField; }
      set { this.vastXmlUrlField = value; }
    }

    public VastRedirectType vastRedirectType {
      get { return this.vastRedirectTypeField; }
      set {
        this.vastRedirectTypeField = value;
        this.vastRedirectTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool vastRedirectTypeSpecified {
      get { return this.vastRedirectTypeFieldSpecified; }
      set { this.vastRedirectTypeFieldSpecified = value; }
    }

    public int duration {
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

    [System.Xml.Serialization.XmlElementAttribute("companionCreativeIds")]
    public long[] companionCreativeIds {
      get { return this.companionCreativeIdsField; }
      set { this.companionCreativeIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("trackingUrls")]
    public ConversionEvent_TrackingUrlsMapEntry[] trackingUrls {
      get { return this.trackingUrlsField; }
      set { this.trackingUrlsField = value; }
    }

    public string vastPreviewUrl {
      get { return this.vastPreviewUrlField; }
      set { this.vastPreviewUrlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum VastRedirectType {
    LINEAR,
    NON_LINEAR,
    LINEAR_AND_NON_LINEAR
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ConversionEvent_TrackingUrlsMapEntry {
    private ConversionEvent keyField;

    private bool keyFieldSpecified;

    private string[] valueField;

    public ConversionEvent key {
      get { return this.keyField; }
      set {
        this.keyField = value;
        this.keySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool keySpecified {
      get { return this.keyFieldSpecified; }
      set { this.keyFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlArrayItemAttribute("urls", IsNullable = false)]
    public string[] value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ConversionEvent {
    CREATIVE_VIEW,
    START,
    SKIP_SHOWN,
    FIRST_QUARTILE,
    MIDPOINT,
    THIRD_QUARTILE,
    ENGAGED_VIEW,
    COMPLETE,
    MUTE,
    UNMUTE,
    PAUSE,
    REWIND,
    RESUME,
    SKIPPED,
    FULLSCREEN,
    EXPAND,
    COLLAPSE,
    ACCEPT_INVITATION,
    CLOSE,
    CLICK_TRACKING,
    SURVEY,
    CUSTOM_CLICK
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UnsupportedCreative : Creative {
    private string unsupportedCreativeTypeField;

    public string unsupportedCreativeType {
      get { return this.unsupportedCreativeTypeField; }
      set { this.unsupportedCreativeTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class TemplateCreative : Creative {
    private long creativeTemplateIdField;

    private bool creativeTemplateIdFieldSpecified;

    private bool isInterstitialField;

    private bool isInterstitialFieldSpecified;

    private string destinationUrlField;

    private BaseCreativeTemplateVariableValue[] creativeTemplateVariableValuesField;

    public long creativeTemplateId {
      get { return this.creativeTemplateIdField; }
      set {
        this.creativeTemplateIdField = value;
        this.creativeTemplateIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool creativeTemplateIdSpecified {
      get { return this.creativeTemplateIdFieldSpecified; }
      set { this.creativeTemplateIdFieldSpecified = value; }
    }

    public bool isInterstitial {
      get { return this.isInterstitialField; }
      set {
        this.isInterstitialField = value;
        this.isInterstitialSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isInterstitialSpecified {
      get { return this.isInterstitialFieldSpecified; }
      set { this.isInterstitialFieldSpecified = value; }
    }

    public string destinationUrl {
      get { return this.destinationUrlField; }
      set { this.destinationUrlField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("creativeTemplateVariableValues")]
    public BaseCreativeTemplateVariableValue[] creativeTemplateVariableValues {
      get { return this.creativeTemplateVariableValuesField; }
      set { this.creativeTemplateVariableValuesField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(UrlCreativeTemplateVariableValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(StringCreativeTemplateVariableValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LongCreativeTemplateVariableValue))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AssetCreativeTemplateVariableValue))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseCreativeTemplateVariableValue {
    private string uniqueNameField;

    private string baseCreativeTemplateVariableValueTypeField;

    public string uniqueName {
      get { return this.uniqueNameField; }
      set { this.uniqueNameField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("BaseCreativeTemplateVariableValue.Type")]
    public string BaseCreativeTemplateVariableValueType {
      get { return this.baseCreativeTemplateVariableValueTypeField; }
      set { this.baseCreativeTemplateVariableValueTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class UrlCreativeTemplateVariableValue : BaseCreativeTemplateVariableValue {
    private string valueField;

    public string value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class StringCreativeTemplateVariableValue : BaseCreativeTemplateVariableValue {
    private string valueField;

    public string value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LongCreativeTemplateVariableValue : BaseCreativeTemplateVariableValue {
    private long valueField;

    private bool valueFieldSpecified;

    public long value {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AssetCreativeTemplateVariableValue : BaseCreativeTemplateVariableValue {
    private long assetIdField;

    private bool assetIdFieldSpecified;

    private byte[] assetByteArrayField;

    private string fileNameField;

    public long assetId {
      get { return this.assetIdField; }
      set {
        this.assetIdField = value;
        this.assetIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool assetIdSpecified {
      get { return this.assetIdFieldSpecified; }
      set { this.assetIdFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
    public byte[] assetByteArray {
      get { return this.assetByteArrayField; }
      set { this.assetByteArrayField = value; }
    }

    public string fileName {
      get { return this.fileNameField; }
      set { this.fileNameField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LegacyDfpCreative : Creative {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class InternalRedirectCreative : Creative {
    private Size assetSizeField;

    private string internalRedirectUrlField;

    private bool overrideSizeField;

    private bool overrideSizeFieldSpecified;

    private bool isInterstitialField;

    private bool isInterstitialFieldSpecified;

    public Size assetSize {
      get { return this.assetSizeField; }
      set { this.assetSizeField = value; }
    }

    public string internalRedirectUrl {
      get { return this.internalRedirectUrlField; }
      set { this.internalRedirectUrlField = value; }
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

    public bool isInterstitial {
      get { return this.isInterstitialField; }
      set {
        this.isInterstitialField = value;
        this.isInterstitialSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isInterstitialSpecified {
      get { return this.isInterstitialFieldSpecified; }
      set { this.isInterstitialFieldSpecified = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VpaidLinearRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VpaidLinearCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(LegacyDfpMobileCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseVideoCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VideoRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VideoCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseImageRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageRedirectOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseImageCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseFlashRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashRedirectOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseFlashCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AspectRatioImageCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class HasDestinationUrlCreative : Creative {
    private string destinationUrlField;

    public string destinationUrl {
      get { return this.destinationUrlField; }
      set { this.destinationUrlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VpaidLinearRedirectCreative : HasDestinationUrlCreative {
    private long[] companionCreativeIdsField;

    private ConversionEvent_TrackingUrlsMapEntry[] trackingUrlsField;

    private string customParametersField;

    private int durationField;

    private bool durationFieldSpecified;

    private string flashUrlField;

    private Size flashAssetSizeField;

    [System.Xml.Serialization.XmlElementAttribute("companionCreativeIds")]
    public long[] companionCreativeIds {
      get { return this.companionCreativeIdsField; }
      set { this.companionCreativeIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("trackingUrls")]
    public ConversionEvent_TrackingUrlsMapEntry[] trackingUrls {
      get { return this.trackingUrlsField; }
      set { this.trackingUrlsField = value; }
    }

    public string customParameters {
      get { return this.customParametersField; }
      set { this.customParametersField = value; }
    }

    public int duration {
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

    public string flashUrl {
      get { return this.flashUrlField; }
      set { this.flashUrlField = value; }
    }

    public Size flashAssetSize {
      get { return this.flashAssetSizeField; }
      set { this.flashAssetSizeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VpaidLinearCreative : HasDestinationUrlCreative {
    private string flashNameField;

    private byte[] flashByteArrayField;

    private bool overrideSizeField;

    private bool overrideSizeFieldSpecified;

    private Size flashAssetSizeField;

    private long[] companionCreativeIdsField;

    private ConversionEvent_TrackingUrlsMapEntry[] trackingUrlsField;

    private string customParametersField;

    private int durationField;

    private bool durationFieldSpecified;

    private string vastPreviewUrlField;

    public string flashName {
      get { return this.flashNameField; }
      set { this.flashNameField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
    public byte[] flashByteArray {
      get { return this.flashByteArrayField; }
      set { this.flashByteArrayField = value; }
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

    public Size flashAssetSize {
      get { return this.flashAssetSizeField; }
      set { this.flashAssetSizeField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("companionCreativeIds")]
    public long[] companionCreativeIds {
      get { return this.companionCreativeIdsField; }
      set { this.companionCreativeIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("trackingUrls")]
    public ConversionEvent_TrackingUrlsMapEntry[] trackingUrls {
      get { return this.trackingUrlsField; }
      set { this.trackingUrlsField = value; }
    }

    public string customParameters {
      get { return this.customParametersField; }
      set { this.customParametersField = value; }
    }

    public int duration {
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

    public string vastPreviewUrl {
      get { return this.vastPreviewUrlField; }
      set { this.vastPreviewUrlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class LegacyDfpMobileCreative : HasDestinationUrlCreative {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomCreative : HasDestinationUrlCreative {
    private string htmlSnippetField;

    private CustomCreativeAsset[] customCreativeAssetsField;

    private bool isInterstitialField;

    private bool isInterstitialFieldSpecified;

    public string htmlSnippet {
      get { return this.htmlSnippetField; }
      set { this.htmlSnippetField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customCreativeAssets")]
    public CustomCreativeAsset[] customCreativeAssets {
      get { return this.customCreativeAssetsField; }
      set { this.customCreativeAssetsField = value; }
    }

    public bool isInterstitial {
      get { return this.isInterstitialField; }
      set {
        this.isInterstitialField = value;
        this.isInterstitialSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isInterstitialSpecified {
      get { return this.isInterstitialFieldSpecified; }
      set { this.isInterstitialFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CustomCreativeAsset {
    private string macroNameField;

    private long assetIdField;

    private bool assetIdFieldSpecified;

    private byte[] assetByteArrayField;

    private string fileNameField;

    private long fileSizeField;

    private bool fileSizeFieldSpecified;

    public string macroName {
      get { return this.macroNameField; }
      set { this.macroNameField = value; }
    }

    public long assetId {
      get { return this.assetIdField; }
      set {
        this.assetIdField = value;
        this.assetIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool assetIdSpecified {
      get { return this.assetIdFieldSpecified; }
      set { this.assetIdFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
    public byte[] assetByteArray {
      get { return this.assetByteArrayField; }
      set { this.assetByteArrayField = value; }
    }

    public string fileName {
      get { return this.fileNameField; }
      set { this.fileNameField = value; }
    }

    public long fileSize {
      get { return this.fileSizeField; }
      set {
        this.fileSizeField = value;
        this.fileSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool fileSizeSpecified {
      get { return this.fileSizeFieldSpecified; }
      set { this.fileSizeFieldSpecified = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VideoRedirectCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VideoCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseVideoCreative : HasDestinationUrlCreative {
    private int durationField;

    private bool durationFieldSpecified;

    private bool allowDurationOverrideField;

    private bool allowDurationOverrideFieldSpecified;

    private ConversionEvent_TrackingUrlsMapEntry[] trackingUrlsField;

    private long[] companionCreativeIdsField;

    private string customParametersField;

    private string vastPreviewUrlField;

    public int duration {
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

    public bool allowDurationOverride {
      get { return this.allowDurationOverrideField; }
      set {
        this.allowDurationOverrideField = value;
        this.allowDurationOverrideSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool allowDurationOverrideSpecified {
      get { return this.allowDurationOverrideFieldSpecified; }
      set { this.allowDurationOverrideFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("trackingUrls")]
    public ConversionEvent_TrackingUrlsMapEntry[] trackingUrls {
      get { return this.trackingUrlsField; }
      set { this.trackingUrlsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("companionCreativeIds")]
    public long[] companionCreativeIds {
      get { return this.companionCreativeIdsField; }
      set { this.companionCreativeIdsField = value; }
    }

    public string customParameters {
      get { return this.customParametersField; }
      set { this.customParametersField = value; }
    }

    public string vastPreviewUrl {
      get { return this.vastPreviewUrlField; }
      set { this.vastPreviewUrlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VideoRedirectCreative : BaseVideoCreative {
    private VideoRedirectAsset[] videoAssetsField;

    [System.Xml.Serialization.XmlElementAttribute("videoAssets")]
    public VideoRedirectAsset[] videoAssets {
      get { return this.videoAssetsField; }
      set { this.videoAssetsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VideoRedirectAsset : RedirectAsset {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VideoRedirectAsset))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class RedirectAsset : Asset {
    private string redirectUrlField;

    public string redirectUrl {
      get { return this.redirectUrlField; }
      set { this.redirectUrlField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RedirectAsset))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(VideoRedirectAsset))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class Asset {
    private string assetTypeField;

    [System.Xml.Serialization.XmlElementAttribute("Asset.Type")]
    public string AssetType {
      get { return this.assetTypeField; }
      set { this.assetTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class VideoCreative : BaseVideoCreative {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageRedirectOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageRedirectCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseImageRedirectCreative : HasDestinationUrlCreative {
    private string imageUrlField;

    public string imageUrl {
      get { return this.imageUrlField; }
      set { this.imageUrlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ImageRedirectOverlayCreative : BaseImageRedirectCreative {
    private Size assetSizeField;

    private int durationField;

    private bool durationFieldSpecified;

    private long[] companionCreativeIdsField;

    private ConversionEvent_TrackingUrlsMapEntry[] trackingUrlsField;

    private string customParametersField;

    private string vastPreviewUrlField;

    public Size assetSize {
      get { return this.assetSizeField; }
      set { this.assetSizeField = value; }
    }

    public int duration {
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

    [System.Xml.Serialization.XmlElementAttribute("companionCreativeIds")]
    public long[] companionCreativeIds {
      get { return this.companionCreativeIdsField; }
      set { this.companionCreativeIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("trackingUrls")]
    public ConversionEvent_TrackingUrlsMapEntry[] trackingUrls {
      get { return this.trackingUrlsField; }
      set { this.trackingUrlsField = value; }
    }

    public string customParameters {
      get { return this.customParametersField; }
      set { this.customParametersField = value; }
    }

    public string vastPreviewUrl {
      get { return this.vastPreviewUrlField; }
      set { this.vastPreviewUrlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ImageRedirectCreative : BaseImageRedirectCreative {
    private string altTextField;

    private string thirdPartyImpressionUrlField;

    public string altText {
      get { return this.altTextField; }
      set { this.altTextField = value; }
    }

    public string thirdPartyImpressionUrl {
      get { return this.thirdPartyImpressionUrlField; }
      set { this.thirdPartyImpressionUrlField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ImageCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseImageCreative : HasDestinationUrlCreative {
    private bool overrideSizeField;

    private bool overrideSizeFieldSpecified;

    private CreativeAsset primaryImageAssetField;

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

    public CreativeAsset primaryImageAsset {
      get { return this.primaryImageAssetField; }
      set { this.primaryImageAssetField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class CreativeAsset {
    private long assetIdField;

    private bool assetIdFieldSpecified;

    private byte[] assetByteArrayField;

    private string fileNameField;

    private long fileSizeField;

    private bool fileSizeFieldSpecified;

    private string assetUrlField;

    private Size sizeField;

    private ImageDensity imageDensityField;

    private bool imageDensityFieldSpecified;

    public long assetId {
      get { return this.assetIdField; }
      set {
        this.assetIdField = value;
        this.assetIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool assetIdSpecified {
      get { return this.assetIdFieldSpecified; }
      set { this.assetIdFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
    public byte[] assetByteArray {
      get { return this.assetByteArrayField; }
      set { this.assetByteArrayField = value; }
    }

    public string fileName {
      get { return this.fileNameField; }
      set { this.fileNameField = value; }
    }

    public long fileSize {
      get { return this.fileSizeField; }
      set {
        this.fileSizeField = value;
        this.fileSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool fileSizeSpecified {
      get { return this.fileSizeFieldSpecified; }
      set { this.fileSizeFieldSpecified = value; }
    }

    public string assetUrl {
      get { return this.assetUrlField; }
      set { this.assetUrlField = value; }
    }

    public Size size {
      get { return this.sizeField; }
      set { this.sizeField = value; }
    }

    public ImageDensity imageDensity {
      get { return this.imageDensityField; }
      set {
        this.imageDensityField = value;
        this.imageDensitySpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool imageDensitySpecified {
      get { return this.imageDensityFieldSpecified; }
      set { this.imageDensityFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ImageDensity {
    ONE_TO_ONE,
    THREE_TO_TWO,
    TWO_TO_ONE,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ImageOverlayCreative : BaseImageCreative {
    private long[] companionCreativeIdsField;

    private ConversionEvent_TrackingUrlsMapEntry[] trackingUrlsField;

    private string customParametersField;

    private int durationField;

    private bool durationFieldSpecified;

    private string vastPreviewUrlField;

    [System.Xml.Serialization.XmlElementAttribute("companionCreativeIds")]
    public long[] companionCreativeIds {
      get { return this.companionCreativeIdsField; }
      set { this.companionCreativeIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("trackingUrls")]
    public ConversionEvent_TrackingUrlsMapEntry[] trackingUrls {
      get { return this.trackingUrlsField; }
      set { this.trackingUrlsField = value; }
    }

    public string customParameters {
      get { return this.customParametersField; }
      set { this.customParametersField = value; }
    }

    public int duration {
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

    public string vastPreviewUrl {
      get { return this.vastPreviewUrlField; }
      set { this.vastPreviewUrlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ImageCreative : BaseImageCreative {
    private string altTextField;

    private string thirdPartyImpressionUrlField;

    private CreativeAsset[] secondaryImageAssetsField;

    public string altText {
      get { return this.altTextField; }
      set { this.altTextField = value; }
    }

    public string thirdPartyImpressionUrl {
      get { return this.thirdPartyImpressionUrlField; }
      set { this.thirdPartyImpressionUrlField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("secondaryImageAssets")]
    public CreativeAsset[] secondaryImageAssets {
      get { return this.secondaryImageAssetsField; }
      set { this.secondaryImageAssetsField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashRedirectOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashRedirectCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseFlashRedirectCreative : HasDestinationUrlCreative {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class FlashRedirectOverlayCreative : BaseFlashRedirectCreative {
    private long[] companionCreativeIdsField;

    private ConversionEvent_TrackingUrlsMapEntry[] trackingUrlsField;

    private string customParametersField;

    private ApiFramework apiFrameworkField;

    private bool apiFrameworkFieldSpecified;

    private int durationField;

    private bool durationFieldSpecified;

    private Size flashAssetSizeField;

    private string vastPreviewUrlField;

    [System.Xml.Serialization.XmlElementAttribute("companionCreativeIds")]
    public long[] companionCreativeIds {
      get { return this.companionCreativeIdsField; }
      set { this.companionCreativeIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("trackingUrls")]
    public ConversionEvent_TrackingUrlsMapEntry[] trackingUrls {
      get { return this.trackingUrlsField; }
      set { this.trackingUrlsField = value; }
    }

    public string customParameters {
      get { return this.customParametersField; }
      set { this.customParametersField = value; }
    }

    public ApiFramework apiFramework {
      get { return this.apiFrameworkField; }
      set {
        this.apiFrameworkField = value;
        this.apiFrameworkSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool apiFrameworkSpecified {
      get { return this.apiFrameworkFieldSpecified; }
      set { this.apiFrameworkFieldSpecified = value; }
    }

    public int duration {
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

    public Size flashAssetSize {
      get { return this.flashAssetSizeField; }
      set { this.flashAssetSizeField = value; }
    }

    public string vastPreviewUrl {
      get { return this.vastPreviewUrlField; }
      set { this.vastPreviewUrlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ApiFramework {
    NONE,
    CLICKTAG,
    VPAID
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class FlashRedirectCreative : BaseFlashRedirectCreative {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashOverlayCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(FlashCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseFlashCreative : HasDestinationUrlCreative {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class FlashOverlayCreative : BaseFlashCreative {
    private long[] companionCreativeIdsField;

    private ConversionEvent_TrackingUrlsMapEntry[] trackingUrlsField;

    private string customParametersField;

    private ApiFramework apiFrameworkField;

    private bool apiFrameworkFieldSpecified;

    private int durationField;

    private bool durationFieldSpecified;

    private string vastPreviewUrlField;

    [System.Xml.Serialization.XmlElementAttribute("companionCreativeIds")]
    public long[] companionCreativeIds {
      get { return this.companionCreativeIdsField; }
      set { this.companionCreativeIdsField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("trackingUrls")]
    public ConversionEvent_TrackingUrlsMapEntry[] trackingUrls {
      get { return this.trackingUrlsField; }
      set { this.trackingUrlsField = value; }
    }

    public string customParameters {
      get { return this.customParametersField; }
      set { this.customParametersField = value; }
    }

    public ApiFramework apiFramework {
      get { return this.apiFrameworkField; }
      set {
        this.apiFrameworkField = value;
        this.apiFrameworkSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool apiFrameworkSpecified {
      get { return this.apiFrameworkFieldSpecified; }
      set { this.apiFrameworkFieldSpecified = value; }
    }

    public int duration {
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

    public string vastPreviewUrl {
      get { return this.vastPreviewUrlField; }
      set { this.vastPreviewUrlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class FlashCreative : BaseFlashCreative {
    private SwiffyFallbackAsset swiffyAssetField;

    private bool createSwiffyAssetField;

    private bool createSwiffyAssetFieldSpecified;

    public SwiffyFallbackAsset swiffyAsset {
      get { return this.swiffyAssetField; }
      set { this.swiffyAssetField = value; }
    }

    public bool createSwiffyAsset {
      get { return this.createSwiffyAssetField; }
      set {
        this.createSwiffyAssetField = value;
        this.createSwiffyAssetSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool createSwiffyAssetSpecified {
      get { return this.createSwiffyAssetFieldSpecified; }
      set { this.createSwiffyAssetFieldSpecified = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class SwiffyFallbackAsset {
    private CreativeAsset assetField;

    private Html5Feature[] html5FeaturesField;

    private string[] localizedInfoMessagesField;

    public CreativeAsset asset {
      get { return this.assetField; }
      set { this.assetField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("html5Features")]
    public Html5Feature[] html5Features {
      get { return this.html5FeaturesField; }
      set { this.html5FeaturesField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("localizedInfoMessages")]
    public string[] localizedInfoMessages {
      get { return this.localizedInfoMessagesField; }
      set { this.localizedInfoMessagesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum Html5Feature {
    BASIC_SVG,
    SVG_FILTERS,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AspectRatioImageCreative : HasDestinationUrlCreative {
    private CreativeAsset[] imageAssetsField;

    private string altTextField;

    private string thirdPartyImpressionUrlField;

    private bool overrideSizeField;

    private bool overrideSizeFieldSpecified;

    [System.Xml.Serialization.XmlElementAttribute("imageAssets")]
    public CreativeAsset[] imageAssets {
      get { return this.imageAssetsField; }
      set { this.imageAssetsField = value; }
    }

    public string altText {
      get { return this.altTextField; }
      set { this.altTextField = value; }
    }

    public string thirdPartyImpressionUrl {
      get { return this.thirdPartyImpressionUrlField; }
      set { this.thirdPartyImpressionUrlField = value; }
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
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ClickTrackingCreative : Creative {
    private string clickTrackingUrlField;

    public string clickTrackingUrl {
      get { return this.clickTrackingUrlField; }
      set { this.clickTrackingUrlField = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(RichMediaStudioCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseRichMediaStudioCreative : Creative {
    private long studioCreativeIdField;

    private bool studioCreativeIdFieldSpecified;

    private RichMediaStudioCreativeFormat creativeFormatField;

    private bool creativeFormatFieldSpecified;

    private RichMediaStudioCreativeArtworkType artworkTypeField;

    private bool artworkTypeFieldSpecified;

    private long totalFileSizeField;

    private bool totalFileSizeFieldSpecified;

    private string[] adTagKeysField;

    private string[] customKeyValuesField;

    private string surveyUrlField;

    private string allImpressionsUrlField;

    private string richMediaImpressionsUrlField;

    private string backupImageImpressionsUrlField;

    private string overrideCssField;

    private string requiredFlashPluginVersionField;

    private int durationField;

    private bool durationFieldSpecified;

    private RichMediaStudioCreativeBillingAttribute billingAttributeField;

    private bool billingAttributeFieldSpecified;

    private RichMediaStudioChildAssetProperty[] richMediaStudioChildAssetPropertiesField;

    public long studioCreativeId {
      get { return this.studioCreativeIdField; }
      set {
        this.studioCreativeIdField = value;
        this.studioCreativeIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool studioCreativeIdSpecified {
      get { return this.studioCreativeIdFieldSpecified; }
      set { this.studioCreativeIdFieldSpecified = value; }
    }

    public RichMediaStudioCreativeFormat creativeFormat {
      get { return this.creativeFormatField; }
      set {
        this.creativeFormatField = value;
        this.creativeFormatSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool creativeFormatSpecified {
      get { return this.creativeFormatFieldSpecified; }
      set { this.creativeFormatFieldSpecified = value; }
    }

    public RichMediaStudioCreativeArtworkType artworkType {
      get { return this.artworkTypeField; }
      set {
        this.artworkTypeField = value;
        this.artworkTypeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool artworkTypeSpecified {
      get { return this.artworkTypeFieldSpecified; }
      set { this.artworkTypeFieldSpecified = value; }
    }

    public long totalFileSize {
      get { return this.totalFileSizeField; }
      set {
        this.totalFileSizeField = value;
        this.totalFileSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalFileSizeSpecified {
      get { return this.totalFileSizeFieldSpecified; }
      set { this.totalFileSizeFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("adTagKeys")]
    public string[] adTagKeys {
      get { return this.adTagKeysField; }
      set { this.adTagKeysField = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("customKeyValues")]
    public string[] customKeyValues {
      get { return this.customKeyValuesField; }
      set { this.customKeyValuesField = value; }
    }

    public string surveyUrl {
      get { return this.surveyUrlField; }
      set { this.surveyUrlField = value; }
    }

    public string allImpressionsUrl {
      get { return this.allImpressionsUrlField; }
      set { this.allImpressionsUrlField = value; }
    }

    public string richMediaImpressionsUrl {
      get { return this.richMediaImpressionsUrlField; }
      set { this.richMediaImpressionsUrlField = value; }
    }

    public string backupImageImpressionsUrl {
      get { return this.backupImageImpressionsUrlField; }
      set { this.backupImageImpressionsUrlField = value; }
    }

    public string overrideCss {
      get { return this.overrideCssField; }
      set { this.overrideCssField = value; }
    }

    public string requiredFlashPluginVersion {
      get { return this.requiredFlashPluginVersionField; }
      set { this.requiredFlashPluginVersionField = value; }
    }

    public int duration {
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

    public RichMediaStudioCreativeBillingAttribute billingAttribute {
      get { return this.billingAttributeField; }
      set {
        this.billingAttributeField = value;
        this.billingAttributeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool billingAttributeSpecified {
      get { return this.billingAttributeFieldSpecified; }
      set { this.billingAttributeFieldSpecified = value; }
    }

    [System.Xml.Serialization.XmlElementAttribute("richMediaStudioChildAssetProperties")]
    public RichMediaStudioChildAssetProperty[] richMediaStudioChildAssetProperties {
      get { return this.richMediaStudioChildAssetPropertiesField; }
      set { this.richMediaStudioChildAssetPropertiesField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RichMediaStudioCreativeFormat {
    IN_PAGE,
    EXPANDING,
    IM_EXPANDING,
    FLOATING,
    PEEL_DOWN,
    IN_PAGE_WITH_FLOATING,
    FLASH_IN_FLASH,
    FLASH_IN_FLASH_EXPANDING,
    IN_APP,
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RichMediaStudioCreativeArtworkType {
    FLASH,
    HTML5,
    MIXED
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RichMediaStudioCreativeBillingAttribute {
    IN_PAGE,
    FLOATING_EXPANDING,
    VIDEO,
    FLASH_IN_FLASH
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RichMediaStudioChildAssetProperty {
    private string nameField;

    private RichMediaStudioChildAssetPropertyType typeField;

    private bool typeFieldSpecified;

    private long totalFileSizeField;

    private bool totalFileSizeFieldSpecified;

    private int widthField;

    private bool widthFieldSpecified;

    private int heightField;

    private bool heightFieldSpecified;

    private string urlField;

    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public RichMediaStudioChildAssetPropertyType type {
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

    public long totalFileSize {
      get { return this.totalFileSizeField; }
      set {
        this.totalFileSizeField = value;
        this.totalFileSizeSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool totalFileSizeSpecified {
      get { return this.totalFileSizeFieldSpecified; }
      set { this.totalFileSizeFieldSpecified = value; }
    }

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

    public string url {
      get { return this.urlField; }
      set { this.urlField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RichMediaStudioChildAssetProperty.Type", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum RichMediaStudioChildAssetPropertyType {
    FLASH,
    VIDEO,
    IMAGE,
    DATA
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class RichMediaStudioCreative : BaseRichMediaStudioCreative {
    private bool isInterstitialField;

    private bool isInterstitialFieldSpecified;

    public bool isInterstitial {
      get { return this.isInterstitialField; }
      set {
        this.isInterstitialField = value;
        this.isInterstitialSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool isInterstitialSpecified {
      get { return this.isInterstitialFieldSpecified; }
      set { this.isInterstitialFieldSpecified = value; }
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(HasHtmlSnippetDynamicAllocationCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdSenseCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdExchangeCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class BaseDynamicAllocationCreative : Creative {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdSenseCreative))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(AdExchangeCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class HasHtmlSnippetDynamicAllocationCreative : BaseDynamicAllocationCreative {
    private string codeSnippetField;

    public string codeSnippet {
      get { return this.codeSnippetField; }
      set { this.codeSnippetField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdSenseCreative : HasHtmlSnippetDynamicAllocationCreative {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class AdExchangeCreative : HasHtmlSnippetDynamicAllocationCreative {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "ContactServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseContact))]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class ContactService : DfpSoapClient, IContactService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public ContactService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/ContactService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Contact createContact(Contact contact) {
      object[] results = this.Invoke("createContact", new object[] { contact });
      return ((Contact) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Contact[] createContacts([System.Xml.Serialization.XmlElementAttribute("contacts")]
Contact[] contacts) {
      object[] results = this.Invoke("createContacts", new object[] { contacts });
      return ((Contact[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Contact getContact(long contactId) {
      object[] results = this.Invoke("getContact", new object[] { contactId });
      return ((Contact) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual ContactPage getContactsByStatement(Statement statement) {
      object[] results = this.Invoke("getContactsByStatement", new object[] { statement });
      return ((ContactPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Contact updateContact(Contact contact) {
      object[] results = this.Invoke("updateContact", new object[] { contact });
      return ((Contact) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual Contact[] updateContacts([System.Xml.Serialization.XmlElementAttribute("contacts")]
Contact[] contacts) {
      object[] results = this.Invoke("updateContacts", new object[] { contacts });
      return ((Contact[]) (results[0]));
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ContactPage {
    private int totalResultSetSizeField;

    private bool totalResultSetSizeFieldSpecified;

    private int startIndexField;

    private bool startIndexFieldSpecified;

    private Contact[] resultsField;

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
    public Contact[] results {
      get { return this.resultsField; }
      set { this.resultsField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class Contact : BaseContact {
    private long idField;

    private bool idFieldSpecified;

    private string nameField;

    private long companyIdField;

    private bool companyIdFieldSpecified;

    private ContactStatus statusField;

    private bool statusFieldSpecified;

    private string addressField;

    private string cellPhoneField;

    private string commentField;

    private string emailField;

    private string faxPhoneField;

    private string titleField;

    private string workPhoneField;

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

    public long companyId {
      get { return this.companyIdField; }
      set {
        this.companyIdField = value;
        this.companyIdSpecified = true;
      }
    }

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool companyIdSpecified {
      get { return this.companyIdFieldSpecified; }
      set { this.companyIdFieldSpecified = value; }
    }

    public ContactStatus status {
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

    public string address {
      get { return this.addressField; }
      set { this.addressField = value; }
    }

    public string cellPhone {
      get { return this.cellPhoneField; }
      set { this.cellPhoneField = value; }
    }

    public string comment {
      get { return this.commentField; }
      set { this.commentField = value; }
    }

    public string email {
      get { return this.emailField; }
      set { this.emailField = value; }
    }

    public string faxPhone {
      get { return this.faxPhoneField; }
      set { this.faxPhoneField = value; }
    }

    public string title {
      get { return this.titleField; }
      set { this.titleField = value; }
    }

    public string workPhone {
      get { return this.workPhoneField; }
      set { this.workPhoneField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "Contact.Status", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ContactStatus {
    UNINVITED,
    INVITE_PENDNG,
    INVITE_EXPIRED,
    INVITE_CANCELED,
    USER_ACTIVE,
    USER_DISABLED,
    UNKNOWN
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(Contact))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class BaseContact {
    private string baseContactTypeField;

    [System.Xml.Serialization.XmlElementAttribute("BaseContact.Type")]
    public string BaseContactType {
      get { return this.baseContactTypeField; }
      set { this.baseContactTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class ContactError : ApiError {
    private ContactErrorReason reasonField;

    private bool reasonFieldSpecified;

    public ContactErrorReason reason {
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ContactError.Reason", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum ContactErrorReason {
    UNKNOWN
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "CustomTargetingServiceSoapBinding", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  [System.Xml.Serialization.XmlIncludeAttribute(typeof(ApplicationException))]
  public partial class CustomTargetingService : DfpSoapClient, ICustomTargetingService {
    private RequestHeader requestHeaderField;

    private ResponseHeader responseHeaderField;

    public CustomTargetingService() {
      this.Url = "https://www.google.com/apis/ads/publisher/v201306/CustomTargetingService";
    }

    public virtual RequestHeader RequestHeader {
      get { return this.requestHeaderField; }
      set { this.requestHeaderField = value; }
    }

    public virtual ResponseHeader ResponseHeader {
      get { return this.responseHeaderField; }
      set { this.responseHeaderField = value; }
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomTargetingKey[] createCustomTargetingKeys([System.Xml.Serialization.XmlElementAttribute("keys")]
CustomTargetingKey[] keys) {
      object[] results = this.Invoke("createCustomTargetingKeys", new object[] { keys });
      return ((CustomTargetingKey[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomTargetingValue[] createCustomTargetingValues([System.Xml.Serialization.XmlElementAttribute("values")]
CustomTargetingValue[] values) {
      object[] results = this.Invoke("createCustomTargetingValues", new object[] { values });
      return ((CustomTargetingValue[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomTargetingKeyPage getCustomTargetingKeysByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCustomTargetingKeysByStatement", new object[] { filterStatement });
      return ((CustomTargetingKeyPage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomTargetingValuePage getCustomTargetingValuesByStatement(Statement filterStatement) {
      object[] results = this.Invoke("getCustomTargetingValuesByStatement", new object[] { filterStatement });
      return ((CustomTargetingValuePage) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performCustomTargetingKeyAction(CustomTargetingKeyAction customTargetingKeyAction, Statement filterStatement) {
      object[] results = this.Invoke("performCustomTargetingKeyAction", new object[] { customTargetingKeyAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual UpdateResult performCustomTargetingValueAction(CustomTargetingValueAction customTargetingValueAction, Statement filterStatement) {
      object[] results = this.Invoke("performCustomTargetingValueAction", new object[] { customTargetingValueAction, filterStatement });
      return ((UpdateResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomTargetingKey[] updateCustomTargetingKeys([System.Xml.Serialization.XmlElementAttribute("keys")]
CustomTargetingKey[] keys) {
      object[] results = this.Invoke("updateCustomTargetingKeys", new object[] { keys });
      return ((CustomTargetingKey[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
    [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "https://www.google.com/apis/ads/publisher/v201306", ResponseNamespace = "https://www.google.com/apis/ads/publisher/v201306", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("rval")]
    public virtual CustomTargetingValue[] updateCustomTargetingValues([System.Xml.Serialization.XmlElementAttribute("values")]
CustomTargetingValue[] values) {
      object[] results = this.Invoke("updateCustomTargetingValues", new object[] { values });
      return ((CustomTargetingValue[]) (results[0]));
    }
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteCustomTargetingValues))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class CustomTargetingValueAction {
    private string customTargetingValueActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("CustomTargetingValueAction.Type")]
    public string CustomTargetingValueActionType {
      get { return this.customTargetingValueActionTypeField; }
      set { this.customTargetingValueActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeleteCustomTargetingValues : CustomTargetingValueAction {
  }


  [System.Xml.Serialization.XmlIncludeAttribute(typeof(DeleteCustomTargetingKeys))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public abstract partial class CustomTargetingKeyAction {
    private string customTargetingKeyActionTypeField;

    [System.Xml.Serialization.XmlElementAttribute("CustomTargetingKeyAction.Type")]
    public string CustomTargetingKeyActionType {
      get { return this.customTargetingKeyActionTypeField; }
      set { this.customTargetingKeyActionTypeField = value; }
    }
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public partial class DeleteCustomTargetingKeys : CustomTargetingKeyAction {
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomTargetingValue.MatchType", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomTargetingValueMatchType {
    EXACT,
    BROAD,
    PREFIX,
    BROAD_PREFIX
  }


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
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


  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "CustomTargetingKey.Type", Namespace = "https://www.google.com/apis/ads/publisher/v201306")]
  public enum CustomTargetingKeyType {
    PREDEFINED,
    FREEFORM
  }
  public interface IAudienceSegmentService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    AudienceSegmentPage getAudienceSegmentsByStatement(Statement filterStatement);
  }
  public interface ISuggestedAdUnitService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    SuggestedAdUnit getSuggestedAdUnit(string suggestedAdUnitId);
    SuggestedAdUnitPage getSuggestedAdUnitsByStatement(Statement filterStatement);
    SuggestedAdUnitUpdateResult performSuggestedAdUnitAction(SuggestedAdUnitAction suggestedAdUnitAction, Statement filterStatement);
  }
  public interface ILabelService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Label createLabel(Label label);
    Label[] createLabels(Label[] labels);
    Label getLabel(long labelId);
    LabelPage getLabelsByStatement(Statement filterStatement);
    UpdateResult performLabelAction(LabelAction labelAction, Statement filterStatement);
    Label updateLabel(Label label);
    Label[] updateLabels(Label[] labels);
  }
  public interface ICompanyService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Company[] createCompanies(Company[] companies);
    Company createCompany(Company company);
    CompanyPage getCompaniesByStatement(Statement filterStatement);
    Company getCompany(long companyId);
    Company[] updateCompanies(Company[] companies);
    Company updateCompany(Company company);
  }
  public interface IAdRuleService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    AdRule createAdRule(AdRule adRule);
    AdRule[] createAdRules(AdRule[] adRules);
    AdRule getAdRule(int adRuleId);
    AdRulePage getAdRulesByStatement(Statement statement);
    UpdateResult performAdRuleAction(AdRuleAction adRuleAction, Statement filterStatement);
    AdRule updateAdRule(AdRule adRule);
    AdRule[] updateAdRules(AdRule[] adRules);
  }
  public interface IWorkflowActionService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    WorkflowAction getWorkflowAction(long workflowActionId);
    WorkflowActionPage getWorkflowActionsByStatement(Statement filterStatement);
  }
  public interface INetworkService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Network[] getAllNetworks();
    Network getCurrentNetwork();
    Network makeTestNetwork();
    Network updateNetwork(Network network);
  }
  public interface IReconciliationReportService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    ReconciliationReport getReconciliationReport(long reconciliationReportId);
    ReconciliationReportPage getReconciliationReportsByStatement(Statement filterStatement);
    ReconciliationReport updateReconciliationReport(ReconciliationReport reconciliationReport);
    ReconciliationReport[] updateReconciliationReports(ReconciliationReport[] reconciliationReports);
  }
  public interface ICreativeWrapperService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    CreativeWrapper createCreativeWrapper(CreativeWrapper creativeWrapper);
    CreativeWrapper[] createCreativeWrappers(CreativeWrapper[] creativeWrappers);
    CreativeWrapper getCreativeWrapper(long creativeWrapperId);
    CreativeWrapperPage getCreativeWrappersByStatement(Statement filterStatement);
    UpdateResult performCreativeWrapperAction(CreativeWrapperAction creativeWrapperAction, Statement filterStatement);
    CreativeWrapper updateCreativeWrapper(CreativeWrapper creativeWrapper);
    CreativeWrapper[] updateCreativeWrappers(CreativeWrapper[] creativeWrappers);
  }
  public interface IContentService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    ContentPage getContentByStatement(Statement statement);
    ContentPage getContentByStatementAndCustomTargetingValue(Statement filterStatement, long customTargetingValueId);
  }
  public interface ICustomFieldService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    CustomField createCustomField(CustomField customField);
    CustomFieldOption createCustomFieldOption(CustomFieldOption customFieldOption);
    CustomFieldOption[] createCustomFieldOptions(CustomFieldOption[] customFieldOptions);
    CustomField[] createCustomFields(CustomField[] customFields);
    CustomField getCustomField(long customFieldId);
    CustomFieldOption getCustomFieldOption(long customFieldOptionId);
    CustomFieldPage getCustomFieldsByStatement(Statement filterStatement);
    UpdateResult performCustomFieldAction(CustomFieldAction customFieldAction, Statement filterStatement);
    CustomField updateCustomField(CustomField customField);
    CustomFieldOption updateCustomFieldOption(CustomFieldOption customFieldOption);
    CustomFieldOption[] updateCustomFieldOptions(CustomFieldOption[] customFieldOptions);
    CustomField[] updateCustomFields(CustomField[] customFields);
  }
  public interface IProposalLineItemService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    ProposalLineItem createProposalLineItem(ProposalLineItem proposalLineItem);
    ProposalLineItem[] createProposalLineItems(ProposalLineItem[] proposalLineItems);
    ProposalLineItem getProposalLineItem(long proposalLineItemId);
    ProposalLineItemPage getProposalLineItemsByStatement(Statement filterStatement);
    UpdateResult performProposalLineItemAction(ProposalLineItemAction proposalLineItemAction, Statement filterStatement);
    ProposalLineItem updateProposalLineItem(ProposalLineItem proposalLineItem);
    ProposalLineItem[] updateProposalLineItems(ProposalLineItem[] proposalLineItems);
  }
  public interface IReportService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    string getReportDownloadURL(long reportJobId, ExportFormat exportFormat);
    string getReportDownloadUrlWithOptions(long reportJobId, ReportDownloadOptions reportDownloadOptions);
    ReportJob getReportJob(long reportJobId);
    ReportJob runReportJob(ReportJob reportJob);
  }
  public interface IActivityGroupService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    ActivityGroup createActivityGroup(ActivityGroup activityGroup);
    ActivityGroup[] createActivityGroups(ActivityGroup[] activityGroups);
    ActivityGroup getActivityGroup(int activityGroupId);
    ActivityGroupPage getActivityGroupsByStatement(Statement filterStatement);
    ActivityGroup updateActivityGroup(ActivityGroup activityGroup);
    ActivityGroup[] updateActivityGroups(ActivityGroup[] activityGroups);
  }
  public interface ILineItemService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    LineItem createLineItem(LineItem lineItem);
    LineItem[] createLineItems(LineItem[] lineItems);
    LineItem getLineItem(long lineItemId);
    LineItemPage getLineItemsByStatement(Statement filterStatement);
    UpdateResult performLineItemAction(LineItemAction lineItemAction, Statement filterStatement);
    LineItem updateLineItem(LineItem lineItem);
    LineItem[] updateLineItems(LineItem[] lineItems);
  }
  public interface ILineItemTemplateService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    LineItemTemplatePage getLineItemTemplatesByStatement(Statement filterStatement);
  }
  public interface IUserTeamAssociationService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    UserTeamAssociation createUserTeamAssociation(UserTeamAssociation userTeamAssociation);
    UserTeamAssociation[] createUserTeamAssociations(UserTeamAssociation[] userTeamAssociations);
    UserTeamAssociation getUserTeamAssociation(long teamId, long userId);
    UserTeamAssociationPage getUserTeamAssociationsByStatement(Statement filterStatement);
    UpdateResult performUserTeamAssociationAction(UserTeamAssociationAction userTeamAssociationAction, Statement statement);
    UserTeamAssociation updateUserTeamAssociation(UserTeamAssociation userTeamAssociation);
    UserTeamAssociation[] updateUserTeamAssociations(UserTeamAssociation[] userTeamAssociations);
  }
  public interface ILineItemCreativeAssociationService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    LineItemCreativeAssociation createLineItemCreativeAssociation(LineItemCreativeAssociation lineItemCreativeAssociation);
    LineItemCreativeAssociation[] createLineItemCreativeAssociations(LineItemCreativeAssociation[] lineItemCreativeAssociations);
    LineItemCreativeAssociation getLineItemCreativeAssociation(long lineItemId, long creativeId);
    LineItemCreativeAssociationPage getLineItemCreativeAssociationsByStatement(Statement filterStatement);
    string getPreviewUrl(long lineItemId, long creativeId, string siteUrl);
    UpdateResult performLineItemCreativeAssociationAction(LineItemCreativeAssociationAction lineItemCreativeAssociationAction, Statement filterStatement);
    LineItemCreativeAssociation updateLineItemCreativeAssociation(LineItemCreativeAssociation lineItemCreativeAssociation);
    LineItemCreativeAssociation[] updateLineItemCreativeAssociations(LineItemCreativeAssociation[] lineItemCreativeAssociations);
  }
  public interface IProductService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Product getProduct(string productId);
    ProductPage getProductsByStatement(Statement statement);
    UpdateResult performProductAction(ProductAction productAction, Statement filterStatement);
    Product updateProduct(Product product);
    Product[] updateProducts(Product[] products);
  }
  public interface IReconciliationReportRowService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    ReconciliationReportRowPage getReconciliationReportRowsByStatement(Statement filterStatement);
    ReconciliationReportRow[] updateReconciliationReportRows(ReconciliationReportRow[] reconciliationReportRows);
  }
  public interface IRateCardCustomizationService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    RateCardCustomization createRateCardCustomization(RateCardCustomization rateCardCustomization);
    RateCardCustomization[] createRateCardCustomizations(RateCardCustomization[] rateCardCustomizations);
    RateCardCustomization getRateCardCustomization(long rateCardCustomizationId);
    RateCardCustomizationPage getRateCardCustomizationsByStatement(Statement filterStatement);
    UpdateResult performRateCardCustomizationAction(RateCardCustomizationAction rateCardCustomizationAction, Statement filterStatement);
    RateCardCustomization updateRateCardCustomization(RateCardCustomization rateCardCustomization);
    RateCardCustomization[] updateRateCardCustomizations(RateCardCustomization[] rateCardCustomizations);
  }
  public interface ICreativeSetService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    CreativeSet createCreativeSet(CreativeSet creativeSet);
    CreativeSet getCreativeSet(long creativeSetId);
    CreativeSetPage getCreativeSetsByStatement(Statement statement);
    CreativeSet updateCreativeSet(CreativeSet creativeSet);
  }
  public interface IBaseRateService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    BaseRate createBaseRate(BaseRate baseRate);
    BaseRate[] createBaseRates(BaseRate[] baseRates);
    BaseRate getBaseRate(long baseRateId);
    BaseRatePage getBaseRatesByStatement(Statement filterStatement);
    UpdateResult performBaseRateAction(BaseRateAction baseRateAction, Statement filterStatement);
    BaseRate updateBaseRate(BaseRate baseRate);
    BaseRate[] updateBaseRates(BaseRate[] baseRates);
  }
  public interface IInventoryService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    AdUnit createAdUnit(AdUnit adUnit);
    AdUnit[] createAdUnits(AdUnit[] adUnits);
    AdUnit getAdUnit(string adUnitId);
    AdUnitSize[] getAdUnitSizesByStatement(Statement filterStatement);
    AdUnitPage getAdUnitsByStatement(Statement filterStatement);
    UpdateResult performAdUnitAction(AdUnitAction adUnitAction, Statement filterStatement);
    AdUnit updateAdUnit(AdUnit adUnit);
    AdUnit[] updateAdUnits(AdUnit[] adUnits);
  }
  public interface IProductTemplateService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    ProductTemplate createProductTemplate(ProductTemplate productTemplate);
    ProductTemplate[] createProductTemplates(ProductTemplate[] productTemplates);
    ProductTemplate getProductTemplate(long productTemplateId);
    ProductTemplatePage getProductTemplatesByStatement(Statement statement);
    UpdateResult performProductTemplateAction(ProductTemplateAction action, Statement filterStatement);
    ProductTemplate updateProductTemplate(ProductTemplate productTemplate);
    ProductTemplate[] updateProductTemplates(ProductTemplate[] productTemplates);
  }
  public interface IForecastService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Forecast getForecast(LineItem lineItem);
    Forecast getForecastById(long lineItemId);
  }
  public interface IUserService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    User createUser(User user);
    User[] createUsers(User[] users);
    Role[] getAllRoles();
    User getCurrentUser();
    User getUser(long userId);
    UserPage getUsersByStatement(Statement filterStatement);
    UpdateResult performUserAction(UserAction userAction, Statement filterStatement);
    User updateUser(User user);
    User[] updateUsers(User[] users);
  }
  public interface IPublisherQueryLanguageService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    ResultSet select(Statement selectStatement);
  }
  public interface ITeamService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Team createTeam(Team team);
    Team[] createTeams(Team[] teams);
    Team getTeam(long teamId);
    TeamPage getTeamsByStatement(Statement filterStatement);
    Team updateTeam(Team team);
    Team[] updateTeams(Team[] teams);
  }
  public interface IRateCardService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    RateCard createRateCard(RateCard rateCard);
    RateCard[] createRateCards(RateCard[] rateCards);
    RateCard getRateCard(long rateCardId);
    RateCardPage getRateCardsByStatement(Statement filterStatement);
    UpdateResult performRateCardAction(RateCardAction rateCardAction, Statement filterStatement);
    RateCard updateRateCard(RateCard rateCard);
    RateCard[] updateRateCards(RateCard[] rateCards);
  }
  public interface IPlacementService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Placement createPlacement(Placement placement);
    Placement[] createPlacements(Placement[] placements);
    Placement getPlacement(long placementId);
    PlacementPage getPlacementsByStatement(Statement filterStatement);
    UpdateResult performPlacementAction(PlacementAction placementAction, Statement filterStatement);
    Placement updatePlacement(Placement placement);
    Placement[] updatePlacements(Placement[] placements);
  }
  public interface IReconciliationOrderReportService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    ReconciliationOrderReport getReconciliationOrderReport(long reconciliationOrderReportId);
    ReconciliationOrderReportPage getReconciliationOrderReportsByStatement(Statement filterStatement);
    UpdateResult performReconciliationOrderReportAction(ReconciliationOrderReportAction reconciliationOrderReportAction, Statement filterStatement);
  }
  public interface IActivityService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Activity[] createActivities(Activity[] activities);
    Activity createActivity(Activity activity);
    ActivityPage getActivitiesByStatement(Statement filterStatement);
    Activity getActivity(int activityId);
    Activity[] updateActivities(Activity[] activities);
    Activity updateActivity(Activity activity);
  }
  public interface IOrderService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Order createOrder(Order order);
    Order[] createOrders(Order[] orders);
    Order getOrder(long orderId);
    OrderPage getOrdersByStatement(Statement filterStatement);
    UpdateResult performOrderAction(OrderAction orderAction, Statement filterStatement);
    Order updateOrder(Order order);
    Order[] updateOrders(Order[] orders);
  }
  public interface IProposalService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Proposal createProposal(Proposal proposal);
    Proposal[] createProposals(Proposal[] proposals);
    Proposal getProposal(long proposalId);
    ProposalPage getProposalsByStatement(Statement filterStatement);
    UpdateResult performProposalAction(ProposalAction proposalAction, Statement filterStatement);
    Proposal updateProposal(Proposal proposal);
    Proposal[] updateProposals(Proposal[] proposals);
  }
  public interface ICreativeTemplateService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    CreativeTemplate getCreativeTemplate(long creativeTemplateId);
    CreativeTemplatePage getCreativeTemplatesByStatement(Statement filterStatement);
  }
  public interface ICreativeService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Creative createCreative(Creative creative);
    Creative[] createCreatives(Creative[] creatives);
    Creative getCreative(long creativeId);
    CreativePage getCreativesByStatement(Statement filterStatement);
    Creative updateCreative(Creative creative);
    Creative[] updateCreatives(Creative[] creatives);
  }
  public interface IContactService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    Contact createContact(Contact contact);
    Contact[] createContacts(Contact[] contacts);
    Contact getContact(long contactId);
    ContactPage getContactsByStatement(Statement statement);
    Contact updateContact(Contact contact);
    Contact[] updateContacts(Contact[] contacts);
  }
  public interface ICustomTargetingService {
    RequestHeader RequestHeader {
      get;
      set;
    }
    ResponseHeader ResponseHeader {
      get;
      set;
    }
    CustomTargetingKey[] createCustomTargetingKeys(CustomTargetingKey[] keys);
    CustomTargetingValue[] createCustomTargetingValues(CustomTargetingValue[] values);
    CustomTargetingKeyPage getCustomTargetingKeysByStatement(Statement filterStatement);
    CustomTargetingValuePage getCustomTargetingValuesByStatement(Statement filterStatement);
    UpdateResult performCustomTargetingKeyAction(CustomTargetingKeyAction customTargetingKeyAction, Statement filterStatement);
    UpdateResult performCustomTargetingValueAction(CustomTargetingValueAction customTargetingValueAction, Statement filterStatement);
    CustomTargetingKey[] updateCustomTargetingKeys(CustomTargetingKey[] keys);
    CustomTargetingValue[] updateCustomTargetingValues(CustomTargetingValue[] values);
  }
}

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

namespace Google.Api.Ads.Dfa.v1_14 {
  using Google.Api.Ads.Dfa.Lib;

  using System;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Xml.Serialization;
  using System.Web.Services;
  using System.Web.Services.Protocols;

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "loginSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class LoginRemoteService : DfaSoapClient {
    public LoginRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/login";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserProfile")]
    public UserProfile authenticate(string username, string password) {
      object[] results = this.Invoke("authenticate", new object[] {username, password});
      return ((UserProfile) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserProfile")]
    public UserProfile impersonateUser(string username, string token, string userToImpersonate) {
      object[] results = this.Invoke("impersonateUser", new object[] {username, token, userToImpersonate});
      return ((UserProfile) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserProfile")]
    public UserProfile impersonateNetwork(string username, string token, long networkId) {
      object[] results = this.Invoke("impersonateNetwork", new object[] {username, token, networkId});
      return ((UserProfile) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserProfile")]
    public UserProfile changePassword(ChangePasswordRequest changePasswordRequest) {
      object[] results = this.Invoke("changePassword", new object[] {changePasswordRequest});
      return ((UserProfile) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserProfile : UserBase {
    private System.DateTime? lastAccessTimeField;

    private string networkNameField;

    private string tokenField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? lastAccessTime {
      get { return this.lastAccessTimeField; }
      set { this.lastAccessTimeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string networkName {
      get { return this.networkNameField; }
      set { this.networkNameField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string token {
      get { return this.tokenField; }
      set { this.tokenField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(UserProfile))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class UserBase : Base {
    private bool activeField;

    private bool apiUserField;

    private string emailField;

    private long languageIdField;

    private long networkIdField;

    private string passwordField;

    private long subnetworkIdField;

    private long userGroupIdField;

    private long userLevelIdField;

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }

    public bool apiUser {
      get { return this.apiUserField; }
      set { this.apiUserField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string email {
      get { return this.emailField; }
      set { this.emailField = value; }
    }

    public long languageId {
      get { return this.languageIdField; }
      set { this.languageIdField = value; }
    }

    public long networkId {
      get { return this.networkIdField; }
      set { this.networkIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string password {
      get { return this.passwordField; }
      set { this.passwordField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }

    public long userGroupId {
      get { return this.userGroupIdField; }
      set { this.userGroupIdField = value; }
    }

    public long userLevelId {
      get { return this.userLevelIdField; }
      set { this.userLevelIdField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(UserBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(UserProfile))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class Base {
    private long idField;

    private string nameField;

    private ApiException dummyExceptionField;

    public long id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    public ApiException dummyException {
      get { return this.dummyExceptionField; }
      set { this.dummyExceptionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ApiException {
    private long errorCodeField;

    private string errorMessageField;

    private string messageField;

    public long errorCode {
      get { return this.errorCodeField; }
      set { this.errorCodeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string message {
      get { return this.messageField; }
      set { this.messageField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ChangePasswordRequest {
    private string confirmPasswordField;

    private string newPasswordField;

    private string oldPasswordField;

    private string tokenField;

    private string usernameField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string confirmPassword {
      get { return this.confirmPasswordField; }
      set { this.confirmPasswordField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string newPassword {
      get { return this.newPasswordField; }
      set { this.newPasswordField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string oldPassword {
      get { return this.oldPasswordField; }
      set { this.oldPasswordField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string token {
      get { return this.tokenField; }
      set { this.tokenField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string username {
      get { return this.usernameField; }
      set { this.usernameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "networkSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  public partial class NetworkRemoteService : DfaSoapClient {
    public NetworkRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/network";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("NetworkRecordSet")]
    public NetworkRecordSet getNetworks(NetworkSearchCriteria networkSearchCriteria) {
      object[] results = this.Invoke("getNetworks", new object[] {networkSearchCriteria});
      return ((NetworkRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Network")]
    public Network getNetwork(long networkId) {
      object[] results = this.Invoke("getNetwork", new object[] {networkId});
      return ((Network) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("NetworkSaveResult")]
    public NetworkSaveResult saveNetwork(Network network) {
      object[] results = this.Invoke("saveNetwork", new object[] {network});
      return ((NetworkSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Currencies")]
    public Currency[] getCurrencies() {
      object[] results = this.Invoke("getCurrencies", new object[0]);
      return ((Currency[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("LanguageEncodings")]
    public LanguageEncoding[] getLanguageEncodingList() {
      object[] results = this.Invoke("getLanguageEncodingList", new object[0]);
      return ((LanguageEncoding[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("TimeZones")]
    public TimeZone[] getTimeZoneList() {
      object[] results = this.Invoke("getTimeZoneList", new object[0]);
      return ((TimeZone[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Permissions")]
    public Permission[] getAllPermissions() {
      object[] results = this.Invoke("getAllPermissions", new object[0]);
      return ((Permission[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("NetworkPermissions")]
    public NetworkPermission[] getAllNetworkPermissions() {
      object[] results = this.Invoke("getAllNetworkPermissions", new object[0]);
      return ((NetworkPermission[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("NetworkPermissions")]
    public NetworkPermission[] getAssignedNetworkPermissions(long networkId) {
      object[] results = this.Invoke("getAssignedNetworkPermissions", new object[] {networkId});
      return ((NetworkPermission[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdministratorPermission")]
    public AdministratorPermission[] getAdministratorPermissions() {
      object[] results = this.Invoke("getAdministratorPermissions", new object[0]);
      return ((AdministratorPermission[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("WidgetImageUploadResponse")]
    public WidgetImageUploadResponse uploadNetworkWidgetImage(WidgetImageUploadRequest request) {
      object[] results = this.Invoke("uploadNetworkWidgetImage", new object[] {request});
      return ((WidgetImageUploadResponse) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class NetworkSearchCriteria : PageableSearchCriteriaBase {
    private ActiveFilter activeFilterField;

    private SortOrder sortOrderField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ActiveFilter activeFilter {
      get { return this.activeFilterField; }
      set { this.activeFilterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ActiveFilter {
    private bool activeOnlyField;

    private bool inactiveOnlyField;

    public bool activeOnly {
      get { return this.activeOnlyField; }
      set { this.activeOnlyField = value; }
    }

    public bool inactiveOnly {
      get { return this.inactiveOnlyField; }
      set { this.inactiveOnlyField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class WidgetImageUploadResponse {
    private double imageSizeField;

    private string relativeImageUrlField;

    private string staticDataUrlField;

    public double imageSize {
      get { return this.imageSizeField; }
      set { this.imageSizeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string relativeImageUrl {
      get { return this.relativeImageUrlField; }
      set { this.relativeImageUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string staticDataUrl {
      get { return this.staticDataUrlField; }
      set { this.staticDataUrlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class WidgetImageUploadRequest {
    private long campaignField;

    private byte[] filedataField;

    private string filenameField;

    private long networkField;

    private bool networkWidgetImageUploadField;

    private string profileField;

    public long campaign {
      get { return this.campaignField; }
      set { this.campaignField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(DataType = "base64Binary", IsNullable = true)]
    public byte[] filedata {
      get { return this.filedataField; }
      set { this.filedataField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string filename {
      get { return this.filenameField; }
      set { this.filenameField = value; }
    }

    public long network {
      get { return this.networkField; }
      set { this.networkField = value; }
    }

    public bool networkWidgetImageUpload {
      get { return this.networkWidgetImageUploadField; }
      set { this.networkWidgetImageUploadField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string profile {
      get { return this.profileField; }
      set { this.profileField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(NetworkSaveResult))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class SaveResult {
    private long idField;

    public long id {
      get { return this.idField; }
      set { this.idField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class NetworkSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class WidgetNetworkConfig {
    private bool activeField;

    private double imageSizeField;

    private string imageUrlField;

    private string staticDataURLField;

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }

    public double imageSize {
      get { return this.imageSizeField; }
      set { this.imageSizeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string imageUrl {
      get { return this.imageUrlField; }
      set { this.imageUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string staticDataURL {
      get { return this.staticDataURLField; }
      set { this.staticDataURLField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class BillingCustomer {
    private string accessCodeTypeField;

    private bool activeField;

    private string billingCodeField;

    private System.DateTime? expirationDateField;

    private bool publisherField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string accessCodeType {
      get { return this.accessCodeTypeField; }
      set { this.accessCodeTypeField = value; }
    }

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string billingCode {
      get { return this.billingCodeField; }
      set { this.billingCodeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? expirationDate {
      get { return this.expirationDateField; }
      set { this.expirationDateField = value; }
    }

    public bool publisher {
      get { return this.publisherField; }
      set { this.publisherField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaNetworkConfiguration {
    private BillingCustomer billingCustomerField;

    private long changeUserIdField;

    private long customerIdField;

    private System.DateTime? dateAssignedField;

    private long networkField;

    private long teaserSizeLimitField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public BillingCustomer billingCustomer {
      get { return this.billingCustomerField; }
      set { this.billingCustomerField = value; }
    }

    public long changeUserId {
      get { return this.changeUserIdField; }
      set { this.changeUserIdField = value; }
    }

    public long customerId {
      get { return this.customerIdField; }
      set { this.customerIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? dateAssigned {
      get { return this.dateAssignedField; }
      set { this.dateAssignedField = value; }
    }

    public long network {
      get { return this.networkField; }
      set { this.networkField = value; }
    }

    public long teaserSizeLimit {
      get { return this.teaserSizeLimitField; }
      set { this.teaserSizeLimitField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ReachReportConfiguration {
    private bool adTypeFrequencyAndReachField;

    private bool pageLevelFrequencyField;

    private bool placementFrequencyField;

    private bool siteLevelFrequencyField;

    public bool adTypeFrequencyAndReach {
      get { return this.adTypeFrequencyAndReachField; }
      set { this.adTypeFrequencyAndReachField = value; }
    }

    public bool pageLevelFrequency {
      get { return this.pageLevelFrequencyField; }
      set { this.pageLevelFrequencyField = value; }
    }

    public bool placementFrequency {
      get { return this.placementFrequencyField; }
      set { this.placementFrequencyField = value; }
    }

    public bool siteLevelFrequency {
      get { return this.siteLevelFrequencyField; }
      set { this.siteLevelFrequencyField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class LookbackConfiguration {
    private int clickDurationField;

    private int postImpressionActivitiesDurationField;

    private int reportsDaysField;

    private int richMediaEventsDaysField;

    public int clickDuration {
      get { return this.clickDurationField; }
      set { this.clickDurationField = value; }
    }

    public int postImpressionActivitiesDuration {
      get { return this.postImpressionActivitiesDurationField; }
      set { this.postImpressionActivitiesDurationField = value; }
    }

    public int reportsDays {
      get { return this.reportsDaysField; }
      set { this.reportsDaysField = value; }
    }

    public int richMediaEventsDays {
      get { return this.richMediaEventsDaysField; }
      set { this.richMediaEventsDaysField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ExposureToConversionConfiguration {
    private int activitiesToTrackField;

    private bool exposureToConversionEnabledField;

    private int exposuresToTrackField;

    public int activitiesToTrack {
      get { return this.activitiesToTrackField; }
      set { this.activitiesToTrackField = value; }
    }

    public bool exposureToConversionEnabled {
      get { return this.exposureToConversionEnabledField; }
      set { this.exposureToConversionEnabledField = value; }
    }

    public int exposuresToTrack {
      get { return this.exposuresToTrackField; }
      set { this.exposuresToTrackField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvancedReportsConfiguration {
    private int crossSiteDuplicationField;

    private int frequencyToConversionField;

    private int timeLagToConversionField;

    public int crossSiteDuplication {
      get { return this.crossSiteDuplicationField; }
      set { this.crossSiteDuplicationField = value; }
    }

    public int frequencyToConversion {
      get { return this.frequencyToConversionField; }
      set { this.frequencyToConversionField = value; }
    }

    public int timeLagToConversion {
      get { return this.timeLagToConversionField; }
      set { this.timeLagToConversionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ReportsConfiguration {
    private AdvancedReportsConfiguration advancedReportsConfigurationField;

    private ExposureToConversionConfiguration exposureToConversionConfigurationField;

    private LookbackConfiguration lookbackConfigurationField;

    private int minimumClickRateField;

    private int minimumClicksField;

    private int minimumDaysField;

    private int minimumImpressionsField;

    private ReachReportConfiguration reachReportConfigurationField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public AdvancedReportsConfiguration advancedReportsConfiguration {
      get { return this.advancedReportsConfigurationField; }
      set { this.advancedReportsConfigurationField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ExposureToConversionConfiguration exposureToConversionConfiguration {
      get { return this.exposureToConversionConfigurationField; }
      set { this.exposureToConversionConfigurationField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public LookbackConfiguration lookbackConfiguration {
      get { return this.lookbackConfigurationField; }
      set { this.lookbackConfigurationField = value; }
    }

    public int minimumClickRate {
      get { return this.minimumClickRateField; }
      set { this.minimumClickRateField = value; }
    }

    public int minimumClicks {
      get { return this.minimumClicksField; }
      set { this.minimumClicksField = value; }
    }

    public int minimumDays {
      get { return this.minimumDaysField; }
      set { this.minimumDaysField = value; }
    }

    public int minimumImpressions {
      get { return this.minimumImpressionsField; }
      set { this.minimumImpressionsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ReachReportConfiguration reachReportConfiguration {
      get { return this.reachReportConfigurationField; }
      set { this.reachReportConfigurationField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(NetworkPermissionGroup))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class NetworkPermissionGroupBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class NetworkPermissionGroup : NetworkPermissionGroupBase {
    private int displayOrderField;

    public int displayOrder {
      get { return this.displayOrderField; }
      set { this.displayOrderField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(NetworkPermission))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class NetworkPermissionBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class NetworkPermission : NetworkPermissionBase {
    private NetworkPermissionGroup permissionGroupField;

    private string shortNameField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public NetworkPermissionGroup permissionGroup {
      get { return this.permissionGroupField; }
      set { this.permissionGroupField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string shortName {
      get { return this.shortNameField; }
      set { this.shortNameField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PermissionGroup))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PermissionGroupBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PermissionGroup : PermissionGroupBase {
    private long displayOrderField;

    public long displayOrder {
      get { return this.displayOrderField; }
      set { this.displayOrderField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(AdministratorPermission))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Permission))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PermissionBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdministratorPermission : PermissionBase {
    private int accessLevelField;

    public int accessLevel {
      get { return this.accessLevelField; }
      set { this.accessLevelField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Permission : PermissionBase {
    private long displayOrderField;

    private PermissionGroup permissionGroupField;

    private string permissionStringField;

    public long displayOrder {
      get { return this.displayOrderField; }
      set { this.displayOrderField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PermissionGroup permissionGroup {
      get { return this.permissionGroupField; }
      set { this.permissionGroupField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string permissionString {
      get { return this.permissionStringField; }
      set { this.permissionStringField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class TimeZone : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class LanguageEncoding : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Currency : Base {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(FrequencyCapGroup))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class FrequencyCapGroupBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class FrequencyCapGroup : FrequencyCapGroupBase {
    private long? durationField;

    private long? impressionsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long? duration {
      get { return this.durationField; }
      set { this.durationField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long? impressions {
      get { return this.impressionsField; }
      set { this.impressionsField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Network))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class NetworkBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Network : NetworkBase {
    private string abbreviationField;

    private bool activeField;

    private int activeAdsLimitField;

    private int availableAdsField;

    private long[] availablePermissionsField;

    private string countryField;

    private long currencyField;

    private long defaultCreativeSizeField;

    private long defaultEncodingField;

    private long defaultLanguageField;

    private string descriptionField;

    private FrequencyCapGroup[] frequencyCapGroupsField;

    private int loginAttemptsField;

    private long maximumImageSizeField;

    private int minimumPasswordLengthField;

    private long[] networkPermissionsField;

    private string notificationEmailAddressField;

    private long passwordExpirePeriodField;

    private long reportGenerationTimeZoneField;

    private ReportsConfiguration reportsConfigurationField;

    private RichMediaNetworkConfiguration richmediaNetworkConfigField;

    private int totalActiveAdsField;

    private WidgetNetworkConfig widgetNetworkConfigField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string abbreviation {
      get { return this.abbreviationField; }
      set { this.abbreviationField = value; }
    }

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }

    public int activeAdsLimit {
      get { return this.activeAdsLimitField; }
      set { this.activeAdsLimitField = value; }
    }

    public int availableAds {
      get { return this.availableAdsField; }
      set { this.availableAdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] availablePermissions {
      get { return this.availablePermissionsField; }
      set { this.availablePermissionsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string country {
      get { return this.countryField; }
      set { this.countryField = value; }
    }

    public long currency {
      get { return this.currencyField; }
      set { this.currencyField = value; }
    }

    public long defaultCreativeSize {
      get { return this.defaultCreativeSizeField; }
      set { this.defaultCreativeSizeField = value; }
    }

    public long defaultEncoding {
      get { return this.defaultEncodingField; }
      set { this.defaultEncodingField = value; }
    }

    public long defaultLanguage {
      get { return this.defaultLanguageField; }
      set { this.defaultLanguageField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public FrequencyCapGroup[] frequencyCapGroups {
      get { return this.frequencyCapGroupsField; }
      set { this.frequencyCapGroupsField = value; }
    }

    public int loginAttempts {
      get { return this.loginAttemptsField; }
      set { this.loginAttemptsField = value; }
    }

    public long maximumImageSize {
      get { return this.maximumImageSizeField; }
      set { this.maximumImageSizeField = value; }
    }

    public int minimumPasswordLength {
      get { return this.minimumPasswordLengthField; }
      set { this.minimumPasswordLengthField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] networkPermissions {
      get { return this.networkPermissionsField; }
      set { this.networkPermissionsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string notificationEmailAddress {
      get { return this.notificationEmailAddressField; }
      set { this.notificationEmailAddressField = value; }
    }

    public long passwordExpirePeriod {
      get { return this.passwordExpirePeriodField; }
      set { this.passwordExpirePeriodField = value; }
    }

    public long reportGenerationTimeZone {
      get { return this.reportGenerationTimeZoneField; }
      set { this.reportGenerationTimeZoneField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ReportsConfiguration reportsConfiguration {
      get { return this.reportsConfigurationField; }
      set { this.reportsConfigurationField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaNetworkConfiguration richmediaNetworkConfig {
      get { return this.richmediaNetworkConfigField; }
      set { this.richmediaNetworkConfigField = value; }
    }

    public int totalActiveAds {
      get { return this.totalActiveAdsField; }
      set { this.totalActiveAdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public WidgetNetworkConfig widgetNetworkConfig {
      get { return this.widgetNetworkConfigField; }
      set { this.widgetNetworkConfigField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(NetworkRecordSet))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class PagedRecordSet {
    private int pageNumberField;

    private int totalNumberOfPagesField;

    private int totalNumberOfRecordsField;

    public int pageNumber {
      get { return this.pageNumberField; }
      set { this.pageNumberField = value; }
    }

    public int totalNumberOfPages {
      get { return this.totalNumberOfPagesField; }
      set { this.totalNumberOfPagesField = value; }
    }

    public int totalNumberOfRecords {
      get { return this.totalNumberOfRecordsField; }
      set { this.totalNumberOfRecordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class NetworkRecordSet : PagedRecordSet {
    private Network[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Network[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SortOrder {
    private bool descendingField;

    private string fieldNameField;

    public bool descending {
      get { return this.descendingField; }
      set { this.descendingField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string fieldName {
      get { return this.fieldNameField; }
      set { this.fieldNameField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PageableSearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(NetworkSearchCriteria))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class SearchCriteriaBase {
    private long[] idsField;

    private string searchStringField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] ids {
      get { return this.idsField; }
      set { this.idsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string searchString {
      get { return this.searchStringField; }
      set { this.searchStringField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(NetworkSearchCriteria))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class PageableSearchCriteriaBase : SearchCriteriaBase {
    private int pageNumberField;

    private int pageSizeField;

    public int pageNumber {
      get { return this.pageNumberField; }
      set { this.pageNumberField = value; }
    }

    public int pageSize {
      get { return this.pageSizeField; }
      set { this.pageSizeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "placementSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PlacementTagInfo))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(ClickCommandInfo))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PlacementTagCriteria))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(BrowserVersion))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(AreaCode))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaExitOverride))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PlacementAssignment))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SpotlightActivityWeight))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TagSettingsBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PricingPeriod))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class PlacementRemoteService : DfaSoapClient {
    public PlacementRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/placement";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Placement")]
    public Placement getPlacement(long placementId) {
      object[] results = this.Invoke("getPlacement", new object[] {placementId});
      return ((Placement) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementSaveResult")]
    public PlacementSaveResult savePlacement(Placement placement) {
      object[] results = this.Invoke("savePlacement", new object[] {placement});
      return ((PlacementSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deletePlacement(long placementId) {
      this.Invoke("deletePlacement", new object[] {placementId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementGroup")]
    public PlacementGroup getPlacementGroup(long placementGroupId) {
      object[] results = this.Invoke("getPlacementGroup", new object[] {placementGroupId});
      return ((PlacementGroup) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementGroupSaveResult")]
    public PlacementGroupSaveResult savePlacementGroup(PlacementGroup placementGroup) {
      object[] results = this.Invoke("savePlacementGroup", new object[] {placementGroup});
      return ((PlacementGroupSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deletePlacementGroup(long placementGroupId) {
      this.Invoke("deletePlacementGroup", new object[] {placementGroupId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PricingType")]
    public PricingType[] getPricingTypes() {
      object[] results = this.Invoke("getPricingTypes", new object[0]);
      return ((PricingType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementGroupType")]
    public PlacementGroupType[] getPlacementGroupTypes() {
      object[] results = this.Invoke("getPlacementGroupTypes", new object[0]);
      return ((PlacementGroupType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementType")]
    public PlacementType[] getPlacementTypes() {
      object[] results = this.Invoke("getPlacementTypes", new object[0]);
      return ((PlacementType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementTagData")]
    public PlacementTagData getPlacementTagData(long campaignId, PlacementTagCriteria[] placementTagCriterias) {
      object[] results = this.Invoke("getPlacementTagData", new object[] {campaignId, placementTagCriterias});
      return ((PlacementTagData) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementTagOption")]
    public PlacementTagOption[] getRegularPlacementTagOptions() {
      object[] results = this.Invoke("getRegularPlacementTagOptions", new object[0]);
      return ((PlacementTagOption[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementTagOption")]
    public PlacementTagOption[] getInterstitialPlacementTagOptions() {
      object[] results = this.Invoke("getInterstitialPlacementTagOptions", new object[0]);
      return ((PlacementTagOption[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementTagOption")]
    public PlacementTagOption[] getMobilePlacementTagOptions() {
      object[] results = this.Invoke("getMobilePlacementTagOptions", new object[0]);
      return ((PlacementTagOption[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementTagOption")]
    public PlacementTagOption[] getInStreamVideoPlacementTagOptions() {
      object[] results = this.Invoke("getInStreamVideoPlacementTagOptions", new object[0]);
      return ((PlacementTagOption[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementRecordSet")]
    public PlacementRecordSet getPlacementsByCriteria(PlacementSearchCriteria searchCriteria) {
      object[] results = this.Invoke("getPlacementsByCriteria", new object[] {searchCriteria});
      return ((PlacementRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementGroupRecordSet")]
    public PlacementGroupRecordSet getPlacementGroupsByCriteria(PlacementGroupSearchCriteria searchCriteria) {
      object[] results = this.Invoke("getPlacementGroupsByCriteria", new object[] {searchCriteria});
      return ((PlacementGroupRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementUpdateResultSet")]
    public PlacementUpdateResultSet updatePlacements(PlacementUpdateRequest placementUpdateRequest) {
      object[] results = this.Invoke("updatePlacements", new object[] {placementUpdateRequest});
      return ((PlacementUpdateResultSet) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Placement : PlacementBase {
    private bool archivedField;

    private long campaignIdField;

    private string commentsField;

    private long contentCategoryIdField;

    private long dfaSiteIdField;

    private LookbackWindow lookbackWindowField;

    private long placementGroupIdField;

    private long placementStrategyIdField;

    private int placementTypeField;

    private PricingSchedule pricingScheduleField;

    private long siteIdField;

    private long sizeIdField;

    private TagSettings tagSettingsField;

    public bool archived {
      get { return this.archivedField; }
      set { this.archivedField = value; }
    }

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string comments {
      get { return this.commentsField; }
      set { this.commentsField = value; }
    }

    public long contentCategoryId {
      get { return this.contentCategoryIdField; }
      set { this.contentCategoryIdField = value; }
    }

    public long dfaSiteId {
      get { return this.dfaSiteIdField; }
      set { this.dfaSiteIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public LookbackWindow lookbackWindow {
      get { return this.lookbackWindowField; }
      set { this.lookbackWindowField = value; }
    }

    public long placementGroupId {
      get { return this.placementGroupIdField; }
      set { this.placementGroupIdField = value; }
    }

    public long placementStrategyId {
      get { return this.placementStrategyIdField; }
      set { this.placementStrategyIdField = value; }
    }

    public int placementType {
      get { return this.placementTypeField; }
      set { this.placementTypeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PricingSchedule pricingSchedule {
      get { return this.pricingScheduleField; }
      set { this.pricingScheduleField = value; }
    }

    public long siteId {
      get { return this.siteIdField; }
      set { this.siteIdField = value; }
    }

    public long sizeId {
      get { return this.sizeIdField; }
      set { this.sizeIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public TagSettings tagSettings {
      get { return this.tagSettingsField; }
      set { this.tagSettingsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class LookbackWindow {
    private int postClickEventLookbackWindowField;

    private int postImpressionEventLookbackWindowField;

    private int richMediaEventLookbackWindowField;

    public int postClickEventLookbackWindow {
      get { return this.postClickEventLookbackWindowField; }
      set { this.postClickEventLookbackWindowField = value; }
    }

    public int postImpressionEventLookbackWindow {
      get { return this.postImpressionEventLookbackWindowField; }
      set { this.postImpressionEventLookbackWindowField = value; }
    }

    public int richMediaEventLookbackWindow {
      get { return this.richMediaEventLookbackWindowField; }
      set { this.richMediaEventLookbackWindowField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementUpdateResultSet {
    private bool inErrorField;

    private PlacementUpdateResult[] resultField;

    public bool inError {
      get { return this.inErrorField; }
      set { this.inErrorField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PlacementUpdateResult[] result {
      get { return this.resultField; }
      set { this.resultField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementUpdateResult : SaveResult {
    private string errorMessageField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementGroupSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementUpdateRequest {
    private long campaignIdField;

    private System.DateTime? endDateField;

    private int flightingOptionField;

    private System.DateTime? startDateField;

    private int updateOptionField;

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? endDate {
      get { return this.endDateField; }
      set { this.endDateField = value; }
    }

    public int flightingOption {
      get { return this.flightingOptionField; }
      set { this.flightingOptionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? startDate {
      get { return this.startDateField; }
      set { this.startDateField = value; }
    }

    public int updateOption {
      get { return this.updateOptionField; }
      set { this.updateOptionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementGroupRecordSet : PagedRecordSet {
    private PlacementGroup[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PlacementGroup[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementGroup : PlacementGroupBase {
    private bool archivedField;

    private long campaignIdField;

    private string commentsField;

    private long contentCategoryIdField;

    private long dfaSiteIdField;

    private int placementGroupTypeField;

    private long[] placementIdsField;

    private long placementStrategyIdField;

    private PricingSchedule pricingScheduleField;

    private long primaryPlacementIdField;

    private long siteIdField;

    public bool archived {
      get { return this.archivedField; }
      set { this.archivedField = value; }
    }

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string comments {
      get { return this.commentsField; }
      set { this.commentsField = value; }
    }

    public long contentCategoryId {
      get { return this.contentCategoryIdField; }
      set { this.contentCategoryIdField = value; }
    }

    public long dfaSiteId {
      get { return this.dfaSiteIdField; }
      set { this.dfaSiteIdField = value; }
    }

    public int placementGroupType {
      get { return this.placementGroupTypeField; }
      set { this.placementGroupTypeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] placementIds {
      get { return this.placementIdsField; }
      set { this.placementIdsField = value; }
    }

    public long placementStrategyId {
      get { return this.placementStrategyIdField; }
      set { this.placementStrategyIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PricingSchedule pricingSchedule {
      get { return this.pricingScheduleField; }
      set { this.pricingScheduleField = value; }
    }

    public long primaryPlacementId {
      get { return this.primaryPlacementIdField; }
      set { this.primaryPlacementIdField = value; }
    }

    public long siteId {
      get { return this.siteIdField; }
      set { this.siteIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PricingSchedule {
    private int capCostOptionField;

    private System.DateTime? endDateField;

    private bool flightedField;

    private PricingPeriod[] pricingPeriodsField;

    private int pricingTypeField;

    private System.DateTime? startDateField;

    private System.DateTime? testingStartDateField;

    public int capCostOption {
      get { return this.capCostOptionField; }
      set { this.capCostOptionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? endDate {
      get { return this.endDateField; }
      set { this.endDateField = value; }
    }

    public bool flighted {
      get { return this.flightedField; }
      set { this.flightedField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PricingPeriod[] pricingPeriods {
      get { return this.pricingPeriodsField; }
      set { this.pricingPeriodsField = value; }
    }

    public int pricingType {
      get { return this.pricingTypeField; }
      set { this.pricingTypeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? startDate {
      get { return this.startDateField; }
      set { this.startDateField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? testingStartDate {
      get { return this.testingStartDateField; }
      set { this.testingStartDateField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PricingPeriod {
    private string commentsField;

    private System.DateTime? endDateField;

    private double rateOrCostField;

    private System.DateTime? startDateField;

    private long unitsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string comments {
      get { return this.commentsField; }
      set { this.commentsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? endDate {
      get { return this.endDateField; }
      set { this.endDateField = value; }
    }

    public double rateOrCost {
      get { return this.rateOrCostField; }
      set { this.rateOrCostField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? startDate {
      get { return this.startDateField; }
      set { this.startDateField = value; }
    }

    public long units {
      get { return this.unitsField; }
      set { this.unitsField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PlacementGroup))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementGroupBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementTagOption : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class State : Base {
    private string abbreviationField;

    private long countryIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string abbreviation {
      get { return this.abbreviationField; }
      set { this.abbreviationField = value; }
    }

    public long countryId {
      get { return this.countryIdField; }
      set { this.countryIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class OperatingSystem : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DomainType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DomainNameBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DesignatedMarketArea : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class City : Base {
    private long countryIdField;

    private long regionIdField;

    public long countryId {
      get { return this.countryIdField; }
      set { this.countryIdField = value; }
    }

    public long regionId {
      get { return this.regionIdField; }
      set { this.regionIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Browser : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Bandwidth : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class OSP : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ISP : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class MobilePlatform : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Country : Base {
    private string countryCodeField;

    private bool secureField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string countryCode {
      get { return this.countryCodeField; }
      set { this.countryCodeField = value; }
    }

    public bool secure {
      get { return this.secureField; }
      set { this.secureField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TrackingAd))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TargetableAdBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeAd))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RotationGroup))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(ClickTracker))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(MobileAd))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(DefaultAd))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class AdBase : Base {
    private bool activeField;

    private bool archivedField;

    private long campaignIdField;

    private string commentsField;

    private System.DateTime? endTimeField;

    private PlacementAssignment[] placementAssignmentsField;

    private long sizeIdField;

    private System.DateTime? startTimeField;

    private long typeIdField;

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }

    public bool archived {
      get { return this.archivedField; }
      set { this.archivedField = value; }
    }

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string comments {
      get { return this.commentsField; }
      set { this.commentsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? endTime {
      get { return this.endTimeField; }
      set { this.endTimeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PlacementAssignment[] placementAssignments {
      get { return this.placementAssignmentsField; }
      set { this.placementAssignmentsField = value; }
    }

    public long sizeId {
      get { return this.sizeIdField; }
      set { this.sizeIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? startTime {
      get { return this.startTimeField; }
      set { this.startTimeField = value; }
    }

    public long typeId {
      get { return this.typeIdField; }
      set { this.typeIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementAssignment {
    private bool activeField;

    private long placementIdField;

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }

    public long placementId {
      get { return this.placementIdField; }
      set { this.placementIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class TrackingAd : AdBase {
    private CreativeAssignment[] creativeAssignmentsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeAssignment[] creativeAssignments {
      get { return this.creativeAssignmentsField; }
      set { this.creativeAssignmentsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeAssignment {
    private bool activeField;

    private string alternalteTextField;

    private ClickThroughUrl clickThroughUrlField;

    private CreativeGroupAssignment creativeGroupAssignmentField;

    private long creativeIdField;

    private System.DateTime? endDateField;

    private RichMediaExitOverride[] richMediaExitOverridesField;

    private int sequenceField;

    private System.DateTime? startDateField;

    private int weightField;

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string alternalteText {
      get { return this.alternalteTextField; }
      set { this.alternalteTextField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ClickThroughUrl clickThroughUrl {
      get { return this.clickThroughUrlField; }
      set { this.clickThroughUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeGroupAssignment creativeGroupAssignment {
      get { return this.creativeGroupAssignmentField; }
      set { this.creativeGroupAssignmentField = value; }
    }

    public long creativeId {
      get { return this.creativeIdField; }
      set { this.creativeIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? endDate {
      get { return this.endDateField; }
      set { this.endDateField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaExitOverride[] richMediaExitOverrides {
      get { return this.richMediaExitOverridesField; }
      set { this.richMediaExitOverridesField = value; }
    }

    public int sequence {
      get { return this.sequenceField; }
      set { this.sequenceField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? startDate {
      get { return this.startDateField; }
      set { this.startDateField = value; }
    }

    public int weight {
      get { return this.weightField; }
      set { this.weightField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ClickThroughUrl {
    private string customClickThroughUrlField;

    private bool defaultLandingPageUsedField;

    private long landingPageIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string customClickThroughUrl {
      get { return this.customClickThroughUrlField; }
      set { this.customClickThroughUrlField = value; }
    }

    public bool defaultLandingPageUsed {
      get { return this.defaultLandingPageUsedField; }
      set { this.defaultLandingPageUsedField = value; }
    }

    public long landingPageId {
      get { return this.landingPageIdField; }
      set { this.landingPageIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeGroupAssignment {
    private long creativeGroup1IdField;

    private long creativeGroup2IdField;

    public long creativeGroup1Id {
      get { return this.creativeGroup1IdField; }
      set { this.creativeGroup1IdField = value; }
    }

    public long creativeGroup2Id {
      get { return this.creativeGroup2IdField; }
      set { this.creativeGroup2IdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaExitOverride {
    private string clickThroughUrlField;

    private long exitIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string clickThroughUrl {
      get { return this.clickThroughUrlField; }
      set { this.clickThroughUrlField = value; }
    }

    public long exitId {
      get { return this.exitIdField; }
      set { this.exitIdField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeAd))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RotationGroup))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class TargetableAdBase : AdBase {
    private ISP[] iSPsField;

    private OSP[] oSPsField;

    private AreaCode[] areaCodesField;

    private long audienceSegmentIdField;

    private Bandwidth[] bandwidthsField;

    private BrowserVersion[] browserVersionsField;

    private City[] citiesField;

    private int costTypeField;

    private CountryTargetingCriteria countryTargetingCriteriaField;

    private int[] daysOfWeekField;

    private int deliveryLimitField;

    private bool deliveryLimitEnabledField;

    private DesignatedMarketArea[] designatedMarketAreasField;

    private DomainNameBase[] domainNamesField;

    private DomainType[] domainTypesField;

    private int frequencyCapField;

    private long frequencyCapPeriodField;

    private bool hardCutOffField;

    private int[] hoursOfDayField;

    private string keywordExpressionField;

    private OperatingSystem[] operatingSystemsField;

    private string[] postalCodesField;

    private int priorityField;

    private int ratioField;

    private State[] statesField;

    private UserListExpression userListExpressionField;

    private bool userLocalTimeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ISP[] ISPs {
      get { return this.iSPsField; }
      set { this.iSPsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public OSP[] OSPs {
      get { return this.oSPsField; }
      set { this.oSPsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public AreaCode[] areaCodes {
      get { return this.areaCodesField; }
      set { this.areaCodesField = value; }
    }

    public long audienceSegmentId {
      get { return this.audienceSegmentIdField; }
      set { this.audienceSegmentIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Bandwidth[] bandwidths {
      get { return this.bandwidthsField; }
      set { this.bandwidthsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public BrowserVersion[] browserVersions {
      get { return this.browserVersionsField; }
      set { this.browserVersionsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public City[] cities {
      get { return this.citiesField; }
      set { this.citiesField = value; }
    }

    public int costType {
      get { return this.costTypeField; }
      set { this.costTypeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CountryTargetingCriteria countryTargetingCriteria {
      get { return this.countryTargetingCriteriaField; }
      set { this.countryTargetingCriteriaField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public int[] daysOfWeek {
      get { return this.daysOfWeekField; }
      set { this.daysOfWeekField = value; }
    }

    public int deliveryLimit {
      get { return this.deliveryLimitField; }
      set { this.deliveryLimitField = value; }
    }

    public bool deliveryLimitEnabled {
      get { return this.deliveryLimitEnabledField; }
      set { this.deliveryLimitEnabledField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DesignatedMarketArea[] designatedMarketAreas {
      get { return this.designatedMarketAreasField; }
      set { this.designatedMarketAreasField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DomainNameBase[] domainNames {
      get { return this.domainNamesField; }
      set { this.domainNamesField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DomainType[] domainTypes {
      get { return this.domainTypesField; }
      set { this.domainTypesField = value; }
    }

    public int frequencyCap {
      get { return this.frequencyCapField; }
      set { this.frequencyCapField = value; }
    }

    public long frequencyCapPeriod {
      get { return this.frequencyCapPeriodField; }
      set { this.frequencyCapPeriodField = value; }
    }

    public bool hardCutOff {
      get { return this.hardCutOffField; }
      set { this.hardCutOffField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public int[] hoursOfDay {
      get { return this.hoursOfDayField; }
      set { this.hoursOfDayField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string keywordExpression {
      get { return this.keywordExpressionField; }
      set { this.keywordExpressionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public OperatingSystem[] operatingSystems {
      get { return this.operatingSystemsField; }
      set { this.operatingSystemsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string[] postalCodes {
      get { return this.postalCodesField; }
      set { this.postalCodesField = value; }
    }

    public int priority {
      get { return this.priorityField; }
      set { this.priorityField = value; }
    }

    public int ratio {
      get { return this.ratioField; }
      set { this.ratioField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public State[] states {
      get { return this.statesField; }
      set { this.statesField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public UserListExpression userListExpression {
      get { return this.userListExpressionField; }
      set { this.userListExpressionField = value; }
    }

    public bool userLocalTime {
      get { return this.userLocalTimeField; }
      set { this.userLocalTimeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AreaCode {
    private long areaCodeField;

    private long countryIdField;

    public long areaCode {
      get { return this.areaCodeField; }
      set { this.areaCodeField = value; }
    }

    public long countryId {
      get { return this.countryIdField; }
      set { this.countryIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class BrowserVersion {
    private Browser browserField;

    private int majorVersionField;

    private string minorVersionField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Browser browser {
      get { return this.browserField; }
      set { this.browserField = value; }
    }

    public int majorVersion {
      get { return this.majorVersionField; }
      set { this.majorVersionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string minorVersion {
      get { return this.minorVersionField; }
      set { this.minorVersionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CountryTargetingCriteria {
    private Country[] countriesField;

    private bool excludeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Country[] countries {
      get { return this.countriesField; }
      set { this.countriesField = value; }
    }

    public bool exclude {
      get { return this.excludeField; }
      set { this.excludeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserListExpression {
    private string idExpressionField;

    private string nameExpressionField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string idExpression {
      get { return this.idExpressionField; }
      set { this.idExpressionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string nameExpression {
      get { return this.nameExpressionField; }
      set { this.nameExpressionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeAd : TargetableAdBase {
    private ClickThroughUrl clickThroughUrlField;

    private CreativeGroupAssignment creativeGroupAssignmentField;

    private long creativeIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ClickThroughUrl clickThroughUrl {
      get { return this.clickThroughUrlField; }
      set { this.clickThroughUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeGroupAssignment creativeGroupAssignment {
      get { return this.creativeGroupAssignmentField; }
      set { this.creativeGroupAssignmentField = value; }
    }

    public long creativeId {
      get { return this.creativeIdField; }
      set { this.creativeIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RotationGroup : TargetableAdBase {
    private FrequencyCapGroup[] assignedFrequencyCapGroupsField;

    private CreativeAssignment[] creativeAssignmentsField;

    private bool creativeOptimizationEnabledField;

    private int rotationTypeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public FrequencyCapGroup[] assignedFrequencyCapGroups {
      get { return this.assignedFrequencyCapGroupsField; }
      set { this.assignedFrequencyCapGroupsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeAssignment[] creativeAssignments {
      get { return this.creativeAssignmentsField; }
      set { this.creativeAssignmentsField = value; }
    }

    public bool creativeOptimizationEnabled {
      get { return this.creativeOptimizationEnabledField; }
      set { this.creativeOptimizationEnabledField = value; }
    }

    public int rotationType {
      get { return this.rotationTypeField; }
      set { this.rotationTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ClickTracker : AdBase {
    private ClickThroughUrl clickThroughUrlField;

    private CreativeGroupAssignment creativeGroupAssignmentField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ClickThroughUrl clickThroughUrl {
      get { return this.clickThroughUrlField; }
      set { this.clickThroughUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeGroupAssignment creativeGroupAssignment {
      get { return this.creativeGroupAssignmentField; }
      set { this.creativeGroupAssignmentField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class MobileAd : AdBase {
    private CountryTargetingCriteria countryTargetingCriteriaField;

    private CreativeAssignment[] creativeAssignmentsField;

    private int[] daysOfWeekField;

    private int deliveryLimitField;

    private bool deliveryLimitEnabledField;

    private bool hardCutOffField;

    private int[] hoursOfDayField;

    private string keywordExpressionField;

    private MobilePlatform[] mobilePlatformsField;

    private int priorityField;

    private int ratioField;

    private bool userLocalTimeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CountryTargetingCriteria countryTargetingCriteria {
      get { return this.countryTargetingCriteriaField; }
      set { this.countryTargetingCriteriaField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeAssignment[] creativeAssignments {
      get { return this.creativeAssignmentsField; }
      set { this.creativeAssignmentsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public int[] daysOfWeek {
      get { return this.daysOfWeekField; }
      set { this.daysOfWeekField = value; }
    }

    public int deliveryLimit {
      get { return this.deliveryLimitField; }
      set { this.deliveryLimitField = value; }
    }

    public bool deliveryLimitEnabled {
      get { return this.deliveryLimitEnabledField; }
      set { this.deliveryLimitEnabledField = value; }
    }

    public bool hardCutOff {
      get { return this.hardCutOffField; }
      set { this.hardCutOffField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public int[] hoursOfDay {
      get { return this.hoursOfDayField; }
      set { this.hoursOfDayField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string keywordExpression {
      get { return this.keywordExpressionField; }
      set { this.keywordExpressionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public MobilePlatform[] mobilePlatforms {
      get { return this.mobilePlatformsField; }
      set { this.mobilePlatformsField = value; }
    }

    public int priority {
      get { return this.priorityField; }
      set { this.priorityField = value; }
    }

    public int ratio {
      get { return this.ratioField; }
      set { this.ratioField = value; }
    }

    public bool userLocalTime {
      get { return this.userLocalTimeField; }
      set { this.userLocalTimeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DefaultAd : AdBase {
    private CreativeAssignment creativeAssignmentField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeAssignment creativeAssignment {
      get { return this.creativeAssignmentField; }
      set { this.creativeAssignmentField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AudienceSegment : Base {
    private int percentageAllocationField;

    public int percentageAllocation {
      get { return this.percentageAllocationField; }
      set { this.percentageAllocationField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AudienceSegmentGroup : Base {
    private AudienceSegment[] audienceSegmentsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public AudienceSegment[] audienceSegments {
      get { return this.audienceSegmentsField; }
      set { this.audienceSegmentsField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CampaignSummary))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Campaign))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CampaignBase : Base {
    private long advertiserIdField;

    private bool archivedField;

    private string billingInvoiceNotationField;

    private string commentsField;

    private long defaultLandingPageIdField;

    private System.DateTime? endDateField;

    private System.DateTime? startDateField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public bool archived {
      get { return this.archivedField; }
      set { this.archivedField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string billingInvoiceNotation {
      get { return this.billingInvoiceNotationField; }
      set { this.billingInvoiceNotationField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string comments {
      get { return this.commentsField; }
      set { this.commentsField = value; }
    }

    public long defaultLandingPageId {
      get { return this.defaultLandingPageIdField; }
      set { this.defaultLandingPageIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? endDate {
      get { return this.endDateField; }
      set { this.endDateField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? startDate {
      get { return this.startDateField; }
      set { this.startDateField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CampaignSummary : CampaignBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Campaign : CampaignBase {
    private AudienceSegmentGroup[] audienceSegmentGroupsField;

    private long[] creativeGroupIdsField;

    private CreativeOptimizationConfiguration creativeOptimizationConfigurationField;

    private long[] landingPageIdsField;

    private LookbackWindow lookbackWindowField;

    private ReachReportConfiguration reachReportConfigurationField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public AudienceSegmentGroup[] audienceSegmentGroups {
      get { return this.audienceSegmentGroupsField; }
      set { this.audienceSegmentGroupsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] creativeGroupIds {
      get { return this.creativeGroupIdsField; }
      set { this.creativeGroupIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeOptimizationConfiguration creativeOptimizationConfiguration {
      get { return this.creativeOptimizationConfigurationField; }
      set { this.creativeOptimizationConfigurationField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] landingPageIds {
      get { return this.landingPageIdsField; }
      set { this.landingPageIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public LookbackWindow lookbackWindow {
      get { return this.lookbackWindowField; }
      set { this.lookbackWindowField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ReachReportConfiguration reachReportConfiguration {
      get { return this.reachReportConfigurationField; }
      set { this.reachReportConfigurationField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeOptimizationConfiguration {
    private int minimumCreativeWeightField;

    private int optimizationModelIdField;

    private int relativeStrengthField;

    private SpotlightActivityWeight[] spotlightActivitiesField;

    public int minimumCreativeWeight {
      get { return this.minimumCreativeWeightField; }
      set { this.minimumCreativeWeightField = value; }
    }

    public int optimizationModelId {
      get { return this.optimizationModelIdField; }
      set { this.optimizationModelIdField = value; }
    }

    public int relativeStrength {
      get { return this.relativeStrengthField; }
      set { this.relativeStrengthField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SpotlightActivityWeight[] spotlightActivities {
      get { return this.spotlightActivitiesField; }
      set { this.spotlightActivitiesField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityWeight {
    private long activityIdField;

    private int weightField;

    public long activityId {
      get { return this.activityIdField; }
      set { this.activityIdField = value; }
    }

    public int weight {
      get { return this.weightField; }
      set { this.weightField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Advertiser))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvertiserBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Advertiser : AdvertiserBase {
    private long advertiserGroupIdField;

    private bool approvedField;

    private bool hiddenField;

    private bool impressionExchangeEnabledField;

    private bool inventoryAdvertiserField;

    private long networkIdField;

    private long spotIdField;

    private long subnetworkIdField;

    public long advertiserGroupId {
      get { return this.advertiserGroupIdField; }
      set { this.advertiserGroupIdField = value; }
    }

    public bool approved {
      get { return this.approvedField; }
      set { this.approvedField = value; }
    }

    public bool hidden {
      get { return this.hiddenField; }
      set { this.hiddenField = value; }
    }

    public bool impressionExchangeEnabled {
      get { return this.impressionExchangeEnabledField; }
      set { this.impressionExchangeEnabledField = value; }
    }

    public bool inventoryAdvertiser {
      get { return this.inventoryAdvertiserField; }
      set { this.inventoryAdvertiserField = value; }
    }

    public long networkId {
      get { return this.networkIdField; }
      set { this.networkIdField = value; }
    }

    public long spotId {
      get { return this.spotIdField; }
      set { this.spotIdField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementGroupType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PricingType : Base {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Placement))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementRecordSet : PagedRecordSet {
    private Placement[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Placement[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DateInterval {
    private System.DateTime? endDateField;

    private System.DateTime? startDateField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? endDate {
      get { return this.endDateField; }
      set { this.endDateField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? startDate {
      get { return this.startDateField; }
      set { this.startDateField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementGroupSearchCriteria : PageableSearchCriteriaBase {
    private ActiveFilter archiveFilterField;

    private long[] campaignIdsField;

    private long[] dfaSiteIdsField;

    private DateInterval endDateRangeField;

    private int placementGroupFilterField;

    private long[] placementStrategyIdsField;

    private long[] pricingTypeIdsField;

    private long[] siteIdsField;

    private SortOrder sortOrderField;

    private DateInterval startDateRangeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ActiveFilter archiveFilter {
      get { return this.archiveFilterField; }
      set { this.archiveFilterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] campaignIds {
      get { return this.campaignIdsField; }
      set { this.campaignIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] dfaSiteIds {
      get { return this.dfaSiteIdsField; }
      set { this.dfaSiteIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DateInterval endDateRange {
      get { return this.endDateRangeField; }
      set { this.endDateRangeField = value; }
    }

    public int placementGroupFilter {
      get { return this.placementGroupFilterField; }
      set { this.placementGroupFilterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] placementStrategyIds {
      get { return this.placementStrategyIdsField; }
      set { this.placementStrategyIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] pricingTypeIds {
      get { return this.pricingTypeIdsField; }
      set { this.pricingTypeIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] siteIds {
      get { return this.siteIdsField; }
      set { this.siteIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DateInterval startDateRange {
      get { return this.startDateRangeField; }
      set { this.startDateRangeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementSearchCriteria : PageableSearchCriteriaBase {
    private ActiveFilter archiveFilterField;

    private long[] campaignIdsField;

    private long[] dfaSiteIdsField;

    private DateInterval endDateRangeField;

    private int placementFilterField;

    private long[] placementStrategyIdsField;

    private long[] placementTypeIdsField;

    private long[] pricingTypeIdsField;

    private long[] siteIdsField;

    private long[] sizeIdsField;

    private SortOrder sortOrderField;

    private DateInterval startDateRangeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ActiveFilter archiveFilter {
      get { return this.archiveFilterField; }
      set { this.archiveFilterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] campaignIds {
      get { return this.campaignIdsField; }
      set { this.campaignIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] dfaSiteIds {
      get { return this.dfaSiteIdsField; }
      set { this.dfaSiteIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DateInterval endDateRange {
      get { return this.endDateRangeField; }
      set { this.endDateRangeField = value; }
    }

    public int placementFilter {
      get { return this.placementFilterField; }
      set { this.placementFilterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] placementStrategyIds {
      get { return this.placementStrategyIdsField; }
      set { this.placementStrategyIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] placementTypeIds {
      get { return this.placementTypeIdsField; }
      set { this.placementTypeIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] pricingTypeIds {
      get { return this.pricingTypeIdsField; }
      set { this.pricingTypeIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] siteIds {
      get { return this.siteIdsField; }
      set { this.siteIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] sizeIds {
      get { return this.sizeIdsField; }
      set { this.sizeIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DateInterval startDateRange {
      get { return this.startDateRangeField; }
      set { this.startDateRangeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementTagInfo {
    private string clickThroughUrlTagField;

    private string iframeJavaScriptTagField;

    private string imageRedirectUrlTagField;

    private string internalRedirectTagField;

    private string javaScriptTagField;

    private string motifInstructionsField;

    private PlacementBase placementField;

    private string prefetchTagField;

    private string standardTagField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string clickThroughUrlTag {
      get { return this.clickThroughUrlTagField; }
      set { this.clickThroughUrlTagField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string iframeJavaScriptTag {
      get { return this.iframeJavaScriptTagField; }
      set { this.iframeJavaScriptTagField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string imageRedirectUrlTag {
      get { return this.imageRedirectUrlTagField; }
      set { this.imageRedirectUrlTagField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string internalRedirectTag {
      get { return this.internalRedirectTagField; }
      set { this.internalRedirectTagField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string javaScriptTag {
      get { return this.javaScriptTagField; }
      set { this.javaScriptTagField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string motifInstructions {
      get { return this.motifInstructionsField; }
      set { this.motifInstructionsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PlacementBase placement {
      get { return this.placementField; }
      set { this.placementField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string prefetchTag {
      get { return this.prefetchTagField; }
      set { this.prefetchTagField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string standardTag {
      get { return this.standardTagField; }
      set { this.standardTagField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ClickCommandInfo {
    private AdBase adField;

    private string clickCommandField;

    private PlacementBase placementField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public AdBase ad {
      get { return this.adField; }
      set { this.adField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string clickCommand {
      get { return this.clickCommandField; }
      set { this.clickCommandField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PlacementBase placement {
      get { return this.placementField; }
      set { this.placementField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementTagData {
    private Advertiser advertiserField;

    private CampaignBase campaignField;

    private ClickCommandInfo[] clickCommandInfosField;

    private PlacementTagInfo[] placementTagInfosField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Advertiser advertiser {
      get { return this.advertiserField; }
      set { this.advertiserField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CampaignBase campaign {
      get { return this.campaignField; }
      set { this.campaignField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ClickCommandInfo[] clickCommandInfos {
      get { return this.clickCommandInfosField; }
      set { this.clickCommandInfosField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PlacementTagInfo[] placementTagInfos {
      get { return this.placementTagInfosField; }
      set { this.placementTagInfosField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementTagCriteria {
    private long idField;

    private long[] tagOptionIdsField;

    public long id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] tagOptionIds {
      get { return this.tagOptionIdsField; }
      set { this.tagOptionIdsField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TagSettings))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class TagSettingsBase {
    private string additionalKeyValuesField;

    private bool includeClickTrackingStringInTagsField;

    private int keywordHandlingOptionField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string additionalKeyValues {
      get { return this.additionalKeyValuesField; }
      set { this.additionalKeyValuesField = value; }
    }

    public bool includeClickTrackingStringInTags {
      get { return this.includeClickTrackingStringInTagsField; }
      set { this.includeClickTrackingStringInTagsField = value; }
    }

    public int keywordHandlingOption {
      get { return this.keywordHandlingOptionField; }
      set { this.keywordHandlingOptionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class TagSettings : TagSettingsBase {
    private int[] tagTypesField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public int[] tagTypes {
      get { return this.tagTypesField; }
      set { this.tagTypesField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "reportSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class ReportRemoteService : DfaSoapClient {
    public ReportRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/report";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("getReportsByCriteriaReturn")]
    public ReportInfoRecordSet getReportsByCriteria(ReportSearchCriteria ReportSearchCriteria) {
      object[] results = this.Invoke("getReportsByCriteria", new object[] {ReportSearchCriteria});
      return ((ReportInfoRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("getReportReturn")]
    public ReportInfo getReport(ReportRequest ReportRequest) {
      object[] results = this.Invoke("getReport", new object[] {ReportRequest});
      return ((ReportInfo) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("runDeferredReportReturn")]
    public ReportInfo runDeferredReport(ReportRequest ReportRequest) {
      object[] results = this.Invoke("runDeferredReport", new object[] {ReportRequest});
      return ((ReportInfo) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ReportSearchCriteria {
    private DateInterval intervalField;

    private long queryIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DateInterval interval {
      get { return this.intervalField; }
      set { this.intervalField = value; }
    }

    public long queryId {
      get { return this.queryIdField; }
      set { this.queryIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ReportRequest {
    private long queryIdField;

    private long reportIdField;

    public long queryId {
      get { return this.queryIdField; }
      set { this.queryIdField = value; }
    }

    public long reportId {
      get { return this.reportIdField; }
      set { this.reportIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ReportStatusType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ReportInfo {
    private long queryIdField;

    private long reportIdField;

    private ReportStatusType statusField;

    private string urlField;

    public long queryId {
      get { return this.queryIdField; }
      set { this.queryIdField = value; }
    }

    public long reportId {
      get { return this.reportIdField; }
      set { this.reportIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ReportStatusType status {
      get { return this.statusField; }
      set { this.statusField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string url {
      get { return this.urlField; }
      set { this.urlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ReportInfoRecordSet {
    private ReportInfo[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ReportInfo[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "siteSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SiteDirectoryDfaSiteMappingResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SiteDirectoryDfaSiteMappingRequest))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SiteDirectorySiteImportRequest))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TagSettingsBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  public partial class SiteRemoteService : DfaSoapClient {
    public SiteRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/site";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SiteRecordSet")]
    public SiteRecordSet getSitesByCriteria(SiteSearchCriteria siteSearchCriteria) {
      object[] results = this.Invoke("getSitesByCriteria", new object[] {siteSearchCriteria});
      return ((SiteRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SiteDirectorySiteImportResults")]
    public SiteDirectorySiteImportResult[] importSiteDirectorySites(SiteDirectorySiteImportRequest[] siteDirectorySiteImportRequests) {
      object[] results = this.Invoke("importSiteDirectorySites", new object[] {siteDirectorySiteImportRequests});
      return ((SiteDirectorySiteImportResult[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SiteSaveResult")]
    public SiteSaveResult saveSiteDirectorySite(Site site) {
      object[] results = this.Invoke("saveSiteDirectorySite", new object[] {site});
      return ((SiteSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void linkDfaSiteToSiteDirectorySite(long dfaSiteId, long siteDirectorySiteId) {
      this.Invoke("linkDfaSiteToSiteDirectorySite", new object[] {dfaSiteId, siteDirectorySiteId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SiteDirectoryDfaSiteMappingResults")]
    public SiteDirectoryDfaSiteMappingResult[] linkDfaSitesToSiteDirectorySites(SiteDirectoryDfaSiteMappingRequest[] siteDirectoryDfaSiteMappingRequests) {
      object[] results = this.Invoke("linkDfaSitesToSiteDirectorySites", new object[] {siteDirectoryDfaSiteMappingRequests});
      return ((SiteDirectoryDfaSiteMappingResult[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("DfaSite")]
    public DfaSite getDfaSite(long dfaSiteId) {
      object[] results = this.Invoke("getDfaSite", new object[] {dfaSiteId});
      return ((DfaSite) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("DfaSiteRecordSet")]
    public DfaSiteRecordSet getDfaSites(DfaSiteSearchCriteria dfaSiteSearchCriteria) {
      object[] results = this.Invoke("getDfaSites", new object[] {dfaSiteSearchCriteria});
      return ((DfaSiteRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("DfaSiteSaveResult")]
    public DfaSiteSaveResult saveDfaSite(DfaSite dfaSite) {
      object[] results = this.Invoke("saveDfaSite", new object[] {dfaSite});
      return ((DfaSiteSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("ContactRecordSet")]
    public ContactRecordSet getContacts(ContactSearchCriteria contactSearchCriteria) {
      object[] results = this.Invoke("getContacts", new object[] {contactSearchCriteria});
      return ((ContactRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("DfaSiteContactType")]
    public DfaSiteContactType[] getAvailableDfaSiteContactTypes() {
      object[] results = this.Invoke("getAvailableDfaSiteContactTypes", new object[0]);
      return ((DfaSiteContactType[]) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteSearchCriteria : PageableSearchCriteriaBase {
    private bool activeField;

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteDirectoryDfaSiteMappingResult {
    private long dfaSiteIdField;

    private string errorMessageField;

    private long siteDirectorySiteIdField;

    public long dfaSiteId {
      get { return this.dfaSiteIdField; }
      set { this.dfaSiteIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }

    public long siteDirectorySiteId {
      get { return this.siteDirectorySiteIdField; }
      set { this.siteDirectorySiteIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteDirectoryDfaSiteMappingRequest {
    private long dfaSiteIdField;

    private long siteDirectorySiteIdField;

    public long dfaSiteId {
      get { return this.dfaSiteIdField; }
      set { this.dfaSiteIdField = value; }
    }

    public long siteDirectorySiteId {
      get { return this.siteDirectorySiteIdField; }
      set { this.siteDirectorySiteIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DfaSiteSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteDirectorySiteImportResult : SaveResult {
    private long dfaSiteIdField;

    private string errorMessageField;

    private long subnetworkIdField;

    public long dfaSiteId {
      get { return this.dfaSiteIdField; }
      set { this.dfaSiteIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteDirectorySiteImportRequest {
    private long siteDirectorySiteIdField;

    private long subnetworkIdField;

    public long siteDirectorySiteId {
      get { return this.siteDirectorySiteIdField; }
      set { this.siteDirectorySiteIdField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteTagSettings : TagSettingsBase {
    private string keywordReferrerField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string keywordReferrer {
      get { return this.keywordReferrerField; }
      set { this.keywordReferrerField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteRichMediaSettings {
    private string alternateTextField;

    private string frameAndLayerFooterField;

    private string frameAndLayerHeaderField;

    private string iframeFooterField;

    private string iframeHeaderField;

    private string targetWindowField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string alternateText {
      get { return this.alternateTextField; }
      set { this.alternateTextField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string frameAndLayerFooter {
      get { return this.frameAndLayerFooterField; }
      set { this.frameAndLayerFooterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string frameAndLayerHeader {
      get { return this.frameAndLayerHeaderField; }
      set { this.frameAndLayerHeaderField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string iframeFooter {
      get { return this.iframeFooterField; }
      set { this.iframeFooterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string iframeHeader {
      get { return this.iframeHeaderField; }
      set { this.iframeHeaderField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string targetWindow {
      get { return this.targetWindowField; }
      set { this.targetWindowField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DfaSiteContactType : Base {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Contact))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ContactBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Contact : ContactBase {
    private bool allowedToEditSiteField;

    private string cityField;

    private string countryField;

    private string firstNameField;

    private string lastNameField;

    private string phoneField;

    private string postalCodeField;

    private string stateField;

    private string titleField;

    private string typeField;

    public bool allowedToEditSite {
      get { return this.allowedToEditSiteField; }
      set { this.allowedToEditSiteField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string city {
      get { return this.cityField; }
      set { this.cityField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string country {
      get { return this.countryField; }
      set { this.countryField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string firstName {
      get { return this.firstNameField; }
      set { this.firstNameField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string lastName {
      get { return this.lastNameField; }
      set { this.lastNameField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string phone {
      get { return this.phoneField; }
      set { this.phoneField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string postalCode {
      get { return this.postalCodeField; }
      set { this.postalCodeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string state {
      get { return this.stateField; }
      set { this.stateField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string title {
      get { return this.titleField; }
      set { this.titleField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string type {
      get { return this.typeField; }
      set { this.typeField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(DfaSiteContact))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DfaSiteContactBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DfaSiteContact : DfaSiteContactBase {
    private long dfaSiteContactTypeIdField;

    private long dfaSiteIdField;

    private string firstNameField;

    private string lastNameField;

    public long dfaSiteContactTypeId {
      get { return this.dfaSiteContactTypeIdField; }
      set { this.dfaSiteContactTypeIdField = value; }
    }

    public long dfaSiteId {
      get { return this.dfaSiteIdField; }
      set { this.dfaSiteIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string firstName {
      get { return this.firstNameField; }
      set { this.firstNameField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string lastName {
      get { return this.lastNameField; }
      set { this.lastNameField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(DfaSite))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DfaSiteBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DfaSite : DfaSiteBase {
    private bool approvedField;

    private long countryIdField;

    private DfaSiteContact[] dfaSiteContactField;

    private bool displayAlternateTextBelowRichMediaCreativesField;

    private bool existingCookiesIgnoredField;

    private bool explicitApprovalNeededField;

    private string keynameField;

    private LookbackWindow lookbackWindowField;

    private long networkIdField;

    private bool newCookiesDisabledField;

    private SiteRichMediaSettings richMediaSettingsField;

    private long siteDirectorySiteIdField;

    private long subnetworkIdField;

    private SiteTagSettings tagSettingsField;

    public bool approved {
      get { return this.approvedField; }
      set { this.approvedField = value; }
    }

    public long countryId {
      get { return this.countryIdField; }
      set { this.countryIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DfaSiteContact[] dfaSiteContact {
      get { return this.dfaSiteContactField; }
      set { this.dfaSiteContactField = value; }
    }

    public bool displayAlternateTextBelowRichMediaCreatives {
      get { return this.displayAlternateTextBelowRichMediaCreativesField; }
      set { this.displayAlternateTextBelowRichMediaCreativesField = value; }
    }

    public bool existingCookiesIgnored {
      get { return this.existingCookiesIgnoredField; }
      set { this.existingCookiesIgnoredField = value; }
    }

    public bool explicitApprovalNeeded {
      get { return this.explicitApprovalNeededField; }
      set { this.explicitApprovalNeededField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string keyname {
      get { return this.keynameField; }
      set { this.keynameField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public LookbackWindow lookbackWindow {
      get { return this.lookbackWindowField; }
      set { this.lookbackWindowField = value; }
    }

    public long networkId {
      get { return this.networkIdField; }
      set { this.networkIdField = value; }
    }

    public bool newCookiesDisabled {
      get { return this.newCookiesDisabledField; }
      set { this.newCookiesDisabledField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SiteRichMediaSettings richMediaSettings {
      get { return this.richMediaSettingsField; }
      set { this.richMediaSettingsField = value; }
    }

    public long siteDirectorySiteId {
      get { return this.siteDirectorySiteIdField; }
      set { this.siteDirectorySiteIdField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SiteTagSettings tagSettings {
      get { return this.tagSettingsField; }
      set { this.tagSettingsField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Site))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Site : SiteBase {
    private bool acceptingInterstitialPlacementsField;

    private bool acceptingMobilePlacementsField;

    private bool acceptingPublisherPaidPlacementsField;

    private bool activeField;

    private long[] inpageTagSettingsField;

    private long[] interstitialTagSettingsField;

    private string[] urlsField;

    public bool acceptingInterstitialPlacements {
      get { return this.acceptingInterstitialPlacementsField; }
      set { this.acceptingInterstitialPlacementsField = value; }
    }

    public bool acceptingMobilePlacements {
      get { return this.acceptingMobilePlacementsField; }
      set { this.acceptingMobilePlacementsField = value; }
    }

    public bool acceptingPublisherPaidPlacements {
      get { return this.acceptingPublisherPaidPlacementsField; }
      set { this.acceptingPublisherPaidPlacementsField = value; }
    }

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] inpageTagSettings {
      get { return this.inpageTagSettingsField; }
      set { this.inpageTagSettingsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] interstitialTagSettings {
      get { return this.interstitialTagSettingsField; }
      set { this.interstitialTagSettingsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string[] urls {
      get { return this.urlsField; }
      set { this.urlsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ContactRecordSet : PagedRecordSet {
    private Contact[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Contact[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DfaSiteRecordSet : PagedRecordSet {
    private DfaSite[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DfaSite[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SiteRecordSet : PagedRecordSet {
    private Site[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Site[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ContactSearchCriteria : PageableSearchCriteriaBase {
    private bool includeParentContactsField;

    private long[] siteDirectorySiteIdsField;

    private SortOrder sortOrderField;

    public bool includeParentContacts {
      get { return this.includeParentContactsField; }
      set { this.includeParentContactsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] siteDirectorySiteIds {
      get { return this.siteDirectorySiteIdsField; }
      set { this.siteDirectorySiteIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DfaSiteSearchCriteria : PageableSearchCriteriaBase {
    private long[] sDSiteIdsField;

    private long[] campaignIdsField;

    private bool excludeSitesMappedToSiteDirectoryField;

    private long networkIdField;

    private SortOrder sortOrderField;

    private long subnetworkIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] SDSiteIds {
      get { return this.sDSiteIdsField; }
      set { this.sDSiteIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] campaignIds {
      get { return this.campaignIdsField; }
      set { this.campaignIdsField = value; }
    }

    public bool excludeSitesMappedToSiteDirectory {
      get { return this.excludeSitesMappedToSiteDirectoryField; }
      set { this.excludeSitesMappedToSiteDirectoryField = value; }
    }

    public long networkId {
      get { return this.networkIdField; }
      set { this.networkIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "sizeSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  public partial class SizeRemoteService : DfaSoapClient {
    public SizeRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/size";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SizeSaveResult")]
    public SizeSaveResult saveSize(Size size) {
      object[] results = this.Invoke("saveSize", new object[] {size});
      return ((SizeSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Size")]
    public Size getSize(int width, int height) {
      object[] results = this.Invoke("getSize", new object[] {width, height});
      return ((Size) (results[0]));
    }

    [System.Web.Services.WebMethodAttribute(MessageName = "getSize1")]
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Size")]
    public Size getSize(long id) {
      object[] results = this.Invoke("getSize1", new object[] {id});
      return ((Size) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SizeRecordSet")]
    public SizeRecordSet getSizes(SizeSearchCriteria searchCriteria) {
      object[] results = this.Invoke("getSizes", new object[] {searchCriteria});
      return ((SizeRecordSet) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Size {
    private int heightField;

    private long idField;

    private int widthField;

    public int height {
      get { return this.heightField; }
      set { this.heightField = value; }
    }

    public long id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    public int width {
      get { return this.widthField; }
      set { this.widthField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SizeRecordSet {
    private Size[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Size[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SizeSearchCriteria : SearchCriteriaBase {
    private int heightField;

    private int widthField;

    public int height {
      get { return this.heightField; }
      set { this.heightField = value; }
    }

    public int width {
      get { return this.widthField; }
      set { this.widthField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SizeSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "spotlightSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class SpotlightRemoteService : DfaSoapClient {
    public SpotlightRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/spotlight";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightConfiguration")]
    public SpotlightConfiguration getSpotlightConfiguration(long id) {
      object[] results = this.Invoke("getSpotlightConfiguration", new object[] {id});
      return ((SpotlightConfiguration) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightConfigurationSaveResult")]
    public SpotlightConfigurationSaveResult saveSpotlightConfiguration(SpotlightConfiguration spotlightConfiguration) {
      object[] results = this.Invoke("saveSpotlightConfiguration", new object[] {spotlightConfiguration});
      return ((SpotlightConfigurationSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("StandardVariable")]
    public StandardVariable[] getAvailableStandardVariables() {
      object[] results = this.Invoke("getAvailableStandardVariables", new object[0]);
      return ((StandardVariable[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CustomSpotlightVariable")]
    public CustomSpotlightVariable[] getAvailableCustomSpotlightVariables() {
      object[] results = this.Invoke("getAvailableCustomSpotlightVariables", new object[0]);
      return ((CustomSpotlightVariable[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightActivitySaveResult")]
    public SpotlightActivitySaveResult saveSpotlightActivity(SpotlightActivity spotlightActivity) {
      object[] results = this.Invoke("saveSpotlightActivity", new object[] {spotlightActivity});
      return ((SpotlightActivitySaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightActivityGroupSaveResult")]
    public SpotlightActivityGroupSaveResult saveSpotlightActivityGroup(SpotlightActivityGroup spotlightActivityGroup) {
      object[] results = this.Invoke("saveSpotlightActivityGroup", new object[] {spotlightActivityGroup});
      return ((SpotlightActivityGroupSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightActivityRecordSet")]
    public SpotlightActivityRecordSet getSpotlightActivities(SpotlightActivitySearchCriteria spotlightActivitySearchCriteria) {
      object[] results = this.Invoke("getSpotlightActivities", new object[] {spotlightActivitySearchCriteria});
      return ((SpotlightActivityRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightActivity")]
    public SpotlightActivity getSpotlightActivity(long spotlightActivityId) {
      object[] results = this.Invoke("getSpotlightActivity", new object[] {spotlightActivityId});
      return ((SpotlightActivity) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightActivityGroupRecordSet")]
    public SpotlightActivityGroupRecordSet getSpotlightActivityGroups(SpotlightActivityGroupSearchCriteria spotlightActivityGroupSearchCriteria) {
      object[] results = this.Invoke("getSpotlightActivityGroups", new object[] {spotlightActivityGroupSearchCriteria});
      return ((SpotlightActivityGroupRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteSpotlightActivity(long spolightActivityId) {
      this.Invoke("deleteSpotlightActivity", new object[] {spolightActivityId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteSpotlightActivityGroup(long spolightActivityGroupId) {
      this.Invoke("deleteSpotlightActivityGroup", new object[] {spolightActivityGroupId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightTagMethodType")]
    public SpotlightTagMethodType[] getSpotlightTagMethodTypes() {
      object[] results = this.Invoke("getSpotlightTagMethodTypes", new object[0]);
      return ((SpotlightTagMethodType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightActivityType")]
    public SpotlightActivityType[] getSpotlightActivityTypes() {
      object[] results = this.Invoke("getSpotlightActivityTypes", new object[0]);
      return ((SpotlightActivityType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightTagFormatType")]
    public SpotlightTagFormatType[] getSpotlightTagFormatTypes() {
      object[] results = this.Invoke("getSpotlightTagFormatTypes", new object[0]);
      return ((SpotlightTagFormatType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightTagCodeType")]
    public SpotlightTagCodeType[] getSpotlightTagCodeTypes() {
      object[] results = this.Invoke("getSpotlightTagCodeTypes", new object[0]);
      return ((SpotlightTagCodeType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.w3.org/2001/XMLSchema")]
    [return: System.Xml.Serialization.SoapElementAttribute("string")]
    public string generateTags(long[] activityIds) {
      object[] results = this.Invoke("generateTags", new object[] {activityIds});
      return ((string) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Country")]
    public Country[] getCountriesByCriteria(CountrySearchCriteria countrySearchCriteria) {
      object[] results = this.Invoke("getCountriesByCriteria", new object[] {countrySearchCriteria});
      return ((Country[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SpotlightActivityImageTagsSaveResult")]
    public SpotlightActivityImageTagsSaveResult[] updateActivityImageTags(SpotlightActivityImageTagsSaveRequest request) {
      object[] results = this.Invoke("updateActivityImageTags", new object[] {request});
      return ((SpotlightActivityImageTagsSaveResult[]) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightConfiguration : SpotlightConfigurationBase {
    private int clickDaysField;

    private bool crossSiteDuplicateReportField;

    private CustomSpotlightVariableConfiguration[] customSpotlightVariableConfigurationsField;

    private bool dynamicSpotlightEnabledField;

    private int exposureActivitiesCountField;

    private int exposureDaysField;

    private int exposureLevelField;

    private bool exposureToConversionReportField;

    private int firstDayOfWeekField;

    private bool frequencyConversionReportField;

    private bool imageTagsEnabledField;

    private int impressionDaysField;

    private int motifEventsDaysField;

    private long naturalSearchConversionAttributionOptionField;

    private bool omnitureCostDataEnabledField;

    private bool omnitureIntegrationEnabledField;

    private long[] standardVariableIdsField;

    private bool timeLagConversionReportField;

    private int timeSlotField;

    public int clickDays {
      get { return this.clickDaysField; }
      set { this.clickDaysField = value; }
    }

    public bool crossSiteDuplicateReport {
      get { return this.crossSiteDuplicateReportField; }
      set { this.crossSiteDuplicateReportField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CustomSpotlightVariableConfiguration[] customSpotlightVariableConfigurations {
      get { return this.customSpotlightVariableConfigurationsField; }
      set { this.customSpotlightVariableConfigurationsField = value; }
    }

    public bool dynamicSpotlightEnabled {
      get { return this.dynamicSpotlightEnabledField; }
      set { this.dynamicSpotlightEnabledField = value; }
    }

    public int exposureActivitiesCount {
      get { return this.exposureActivitiesCountField; }
      set { this.exposureActivitiesCountField = value; }
    }

    public int exposureDays {
      get { return this.exposureDaysField; }
      set { this.exposureDaysField = value; }
    }

    public int exposureLevel {
      get { return this.exposureLevelField; }
      set { this.exposureLevelField = value; }
    }

    public bool exposureToConversionReport {
      get { return this.exposureToConversionReportField; }
      set { this.exposureToConversionReportField = value; }
    }

    public int firstDayOfWeek {
      get { return this.firstDayOfWeekField; }
      set { this.firstDayOfWeekField = value; }
    }

    public bool frequencyConversionReport {
      get { return this.frequencyConversionReportField; }
      set { this.frequencyConversionReportField = value; }
    }

    public bool imageTagsEnabled {
      get { return this.imageTagsEnabledField; }
      set { this.imageTagsEnabledField = value; }
    }

    public int impressionDays {
      get { return this.impressionDaysField; }
      set { this.impressionDaysField = value; }
    }

    public int motifEventsDays {
      get { return this.motifEventsDaysField; }
      set { this.motifEventsDaysField = value; }
    }

    public long naturalSearchConversionAttributionOption {
      get { return this.naturalSearchConversionAttributionOptionField; }
      set { this.naturalSearchConversionAttributionOptionField = value; }
    }

    public bool omnitureCostDataEnabled {
      get { return this.omnitureCostDataEnabledField; }
      set { this.omnitureCostDataEnabledField = value; }
    }

    public bool omnitureIntegrationEnabled {
      get { return this.omnitureIntegrationEnabledField; }
      set { this.omnitureIntegrationEnabledField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] standardVariableIds {
      get { return this.standardVariableIdsField; }
      set { this.standardVariableIdsField = value; }
    }

    public bool timeLagConversionReport {
      get { return this.timeLagConversionReportField; }
      set { this.timeLagConversionReportField = value; }
    }

    public int timeSlot {
      get { return this.timeSlotField; }
      set { this.timeSlotField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CustomSpotlightVariableConfiguration : VariableBase {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CustomSpotlightVariableConfiguration))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class VariableBase : Base {
    private int typeField;

    public int type {
      get { return this.typeField; }
      set { this.typeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityImageTagsSaveRequest {
    private long[] activityIdsField;

    private bool statusField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] activityIds {
      get { return this.activityIdsField; }
      set { this.activityIdsField = value; }
    }

    public bool status {
      get { return this.statusField; }
      set { this.statusField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CountrySearchCriteria {
    private bool secureField;

    public bool secure {
      get { return this.secureField; }
      set { this.secureField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityGroupRecordSet {
    private SpotlightActivityGroup[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SpotlightActivityGroup[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityGroup : SpotlightActivityGroupBase {
    private int groupTypeField;

    private long spotlightConfigurationIdField;

    private string tagStringField;

    public int groupType {
      get { return this.groupTypeField; }
      set { this.groupTypeField = value; }
    }

    public long spotlightConfigurationId {
      get { return this.spotlightConfigurationIdField; }
      set { this.spotlightConfigurationIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string tagString {
      get { return this.tagStringField; }
      set { this.tagStringField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SpotlightActivityGroup))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class SpotlightActivityGroupBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityRecordSet : PagedRecordSet {
    private SpotlightActivity[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SpotlightActivity[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivity : SpotlightActivityBase {
    private long activityGroupIdField;

    private long activityTypeIdField;

    private long[] assignedCustomSpotlightVariableIdsField;

    private long countryIdField;

    private FloodlightTag[] defaultFloodlightTagsField;

    private string expectedUrlField;

    private bool imageTagsEnabledField;

    private int minimumExpectedEventsField;

    private FloodlightPublisherTag[] publisherTagsField;

    private bool secureField;

    private long tagCodeTypeIdField;

    private long tagFormatIdField;

    private long tagMethodTypeIdField;

    private SpotlightActivityTagProperty tagPropertyField;

    private string tagStringField;

    public long activityGroupId {
      get { return this.activityGroupIdField; }
      set { this.activityGroupIdField = value; }
    }

    public long activityTypeId {
      get { return this.activityTypeIdField; }
      set { this.activityTypeIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] assignedCustomSpotlightVariableIds {
      get { return this.assignedCustomSpotlightVariableIdsField; }
      set { this.assignedCustomSpotlightVariableIdsField = value; }
    }

    public long countryId {
      get { return this.countryIdField; }
      set { this.countryIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public FloodlightTag[] defaultFloodlightTags {
      get { return this.defaultFloodlightTagsField; }
      set { this.defaultFloodlightTagsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string expectedUrl {
      get { return this.expectedUrlField; }
      set { this.expectedUrlField = value; }
    }

    public bool imageTagsEnabled {
      get { return this.imageTagsEnabledField; }
      set { this.imageTagsEnabledField = value; }
    }

    public int minimumExpectedEvents {
      get { return this.minimumExpectedEventsField; }
      set { this.minimumExpectedEventsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public FloodlightPublisherTag[] publisherTags {
      get { return this.publisherTagsField; }
      set { this.publisherTagsField = value; }
    }

    public bool secure {
      get { return this.secureField; }
      set { this.secureField = value; }
    }

    public long tagCodeTypeId {
      get { return this.tagCodeTypeIdField; }
      set { this.tagCodeTypeIdField = value; }
    }

    public long tagFormatId {
      get { return this.tagFormatIdField; }
      set { this.tagFormatIdField = value; }
    }

    public long tagMethodTypeId {
      get { return this.tagMethodTypeIdField; }
      set { this.tagMethodTypeIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SpotlightActivityTagProperty tagProperty {
      get { return this.tagPropertyField; }
      set { this.tagPropertyField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string tagString {
      get { return this.tagStringField; }
      set { this.tagStringField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(FloodlightPublisherTag))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class FloodlightTag : Base {
    private string urlField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string url {
      get { return this.urlField; }
      set { this.urlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class FloodlightPublisherTag : FloodlightTag {
    private bool clickThroughField;

    private long siteIdField;

    private bool viewThroughField;

    public bool clickThrough {
      get { return this.clickThroughField; }
      set { this.clickThroughField = value; }
    }

    public long siteId {
      get { return this.siteIdField; }
      set { this.siteIdField = value; }
    }

    public bool viewThrough {
      get { return this.viewThroughField; }
      set { this.viewThroughField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityTagProperty {
    private int averageNumEventsField;

    private bool errorField;

    private int lastDayNumEventsField;

    private string topReferrerURLField;

    public int averageNumEvents {
      get { return this.averageNumEventsField; }
      set { this.averageNumEventsField = value; }
    }

    public bool error {
      get { return this.errorField; }
      set { this.errorField = value; }
    }

    public int lastDayNumEvents {
      get { return this.lastDayNumEventsField; }
      set { this.lastDayNumEventsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string topReferrerURL {
      get { return this.topReferrerURLField; }
      set { this.topReferrerURLField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SpotlightActivity))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityGroupSearchCriteria : SearchCriteriaBase {
    private long advertiserIdField;

    private int typeField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public int type {
      get { return this.typeField; }
      set { this.typeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivitySearchCriteria : PageableSearchCriteriaBase {
    private int activityTypeField;

    private long advertiserIdField;

    private long[] spotlightActivityGroupIdsField;

    public int activityType {
      get { return this.activityTypeField; }
      set { this.activityTypeField = value; }
    }

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] spotlightActivityGroupIds {
      get { return this.spotlightActivityGroupIdsField; }
      set { this.spotlightActivityGroupIdsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityImageTagsSaveResult : SaveResult {
    private string errorMessageField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityGroupSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivitySaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightConfigurationSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightTagCodeType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightTagFormatType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightActivityType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightTagMethodType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CustomSpotlightVariable : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class StandardVariable : Base {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SpotlightConfiguration))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SpotlightConfigurationBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "strategySoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class PlacementStrategyRemoteService : DfaSoapClient {
    public PlacementStrategyRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/strategy";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementStrategy")]
    public PlacementStrategy getPlacementStrategy(long placementStrategyId) {
      object[] results = this.Invoke("getPlacementStrategy", new object[] {placementStrategyId});
      return ((PlacementStrategy) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementStrategyRecordSet")]
    public PlacementStrategyRecordSet getPlacementStrategiesByCriteria(PlacementStrategySearchCriteria placementStrategySearchCriteria) {
      object[] results = this.Invoke("getPlacementStrategiesByCriteria", new object[] {placementStrategySearchCriteria});
      return ((PlacementStrategyRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("PlacementStrategySaveResult")]
    public PlacementStrategySaveResult savePlacementStrategy(PlacementStrategy placementStrategy) {
      object[] results = this.Invoke("savePlacementStrategy", new object[] {placementStrategy});
      return ((PlacementStrategySaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deletePlacementStrategy(long placementStrategyId) {
      this.Invoke("deletePlacementStrategy", new object[] {placementStrategyId});
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementStrategy : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementStrategySaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementStrategyRecordSet : PagedRecordSet {
    private PlacementStrategy[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public PlacementStrategy[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class PlacementStrategySearchCriteria : PageableSearchCriteriaBase {
    private SortOrder sortOrderField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "subnetworkSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  public partial class SubnetworkRemoteService : DfaSoapClient {
    public SubnetworkRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/subnetwork";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SubnetworkRecordSet")]
    public SubnetworkRecordSet getSubnetworks(SubnetworkSearchCriteria subnetworkSearchCriteria) {
      object[] results = this.Invoke("getSubnetworks", new object[] {subnetworkSearchCriteria});
      return ((SubnetworkRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SubnetworkSummaryRecordSet")]
    public SubnetworkSummaryRecordSet getSubnetworkSummaries(SubnetworkSearchCriteria subnetworkSearchCriteria) {
      object[] results = this.Invoke("getSubnetworkSummaries", new object[] {subnetworkSearchCriteria});
      return ((SubnetworkSummaryRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Subnetwork")]
    public Subnetwork getSubnetwork(long subnetworkId) {
      object[] results = this.Invoke("getSubnetwork", new object[] {subnetworkId});
      return ((Subnetwork) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("SubnetworkSaveResult")]
    public SubnetworkSaveResult saveSubnetwork(Subnetwork subnetwork) {
      object[] results = this.Invoke("saveSubnetwork", new object[] {subnetwork});
      return ((SubnetworkSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Permissions")]
    public Permission[] getAllAvailablePermissions() {
      object[] results = this.Invoke("getAllAvailablePermissions", new object[0]);
      return ((Permission[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Permissions")]
    public Permission[] getDefaultPermissions() {
      object[] results = this.Invoke("getDefaultPermissions", new object[0]);
      return ((Permission[]) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SubnetworkSearchCriteria : PageableSearchCriteriaBase {
    private SortOrder sortOrderField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SubnetworkSaveResult : SaveResult {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SubnetworkSummary))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Subnetwork))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class SubnetworkBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SubnetworkSummary : SubnetworkBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Subnetwork : SubnetworkBase {
    private long[] availablePermissionsField;

    private long networkIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] availablePermissions {
      get { return this.availablePermissionsField; }
      set { this.availablePermissionsField = value; }
    }

    public long networkId {
      get { return this.networkIdField; }
      set { this.networkIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SubnetworkSummaryRecordSet : PagedRecordSet {
    private SubnetworkSummary[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SubnetworkSummary[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class SubnetworkRecordSet : PagedRecordSet {
    private Subnetwork[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Subnetwork[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "userSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  public partial class UserRemoteService : DfaSoapClient {
    public UserRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/user";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserRecordSet")]
    public UserRecordSet getUsersByCriteria(UserSearchCriteria userSearchCriteria) {
      object[] results = this.Invoke("getUsersByCriteria", new object[] {userSearchCriteria});
      return ((UserRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("User")]
    public User getUser(long userId) {
      object[] results = this.Invoke("getUser", new object[] {userId});
      return ((User) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserSaveResult")]
    public UserSaveResult saveUser(User user) {
      object[] results = this.Invoke("saveUser", new object[] {user});
      return ((UserSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserFilterTypes")]
    public UserFilterType[] getAvailableUserFilterTypes() {
      object[] results = this.Invoke("getAvailableUserFilterTypes", new object[0]);
      return ((UserFilterType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserFilterCriteriaTypes")]
    public UserFilterCriteriaType[] getAvailableUserFilterCriteriaTypes() {
      object[] results = this.Invoke("getAvailableUserFilterCriteriaTypes", new object[0]);
      return ((UserFilterCriteriaType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("TraffickerTypes")]
    public TraffickerType[] getAvailableTraffickerTypes() {
      object[] results = this.Invoke("getAvailableTraffickerTypes", new object[0]);
      return ((TraffickerType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.w3.org/2001/XMLSchema")]
    [return: System.Xml.Serialization.SoapElementAttribute("string")]
    public string generateUniqueUsername(string username) {
      object[] results = this.Invoke("generateUniqueUsername", new object[] {username});
      return ((string) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void sendUserInvitationEmail(UserInvitationEmailRequest emailRequest) {
      this.Invoke("sendUserInvitationEmail", new object[] {emailRequest});
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserSearchCriteria : PageableSearchCriteriaBase {
    private ActiveFilter activeFilterField;

    private SortOrder sortOrderField;

    private long subnetworkIdField;

    private long userRoleIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ActiveFilter activeFilter {
      get { return this.activeFilterField; }
      set { this.activeFilterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }

    public long userRoleId {
      get { return this.userRoleIdField; }
      set { this.userRoleIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserInvitationEmailRequest {
    private string emailMessageField;

    private string emailSubjectField;

    private long idField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string emailMessage {
      get { return this.emailMessageField; }
      set { this.emailMessageField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string emailSubject {
      get { return this.emailSubjectField; }
      set { this.emailSubjectField = value; }
    }

    public long id {
      get { return this.idField; }
      set { this.idField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserSaveResult : SaveResult {
    private string tokenField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string token {
      get { return this.tokenField; }
      set { this.tokenField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserFilter {
    private ObjectFilter[] objectFiltersField;

    private int userFilterCriteriaIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ObjectFilter[] objectFilters {
      get { return this.objectFiltersField; }
      set { this.objectFiltersField = value; }
    }

    public int userFilterCriteriaId {
      get { return this.userFilterCriteriaIdField; }
      set { this.userFilterCriteriaIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ObjectFilter : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class TraffickerType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserFilterCriteriaType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserFilterType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class User : UserBase {
    private UserFilter advertiserUserFilterField;

    private UserFilter campaignUserFilterField;

    private string commentsField;

    private bool gaiaEnabledField;

    private long languageEncodingIdField;

    private UserFilter siteUserFilterField;

    private long traffickerTypeField;

    private UserFilter userRoleUserFilterField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public UserFilter advertiserUserFilter {
      get { return this.advertiserUserFilterField; }
      set { this.advertiserUserFilterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public UserFilter campaignUserFilter {
      get { return this.campaignUserFilterField; }
      set { this.campaignUserFilterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string comments {
      get { return this.commentsField; }
      set { this.commentsField = value; }
    }

    public bool gaiaEnabled {
      get { return this.gaiaEnabledField; }
      set { this.gaiaEnabledField = value; }
    }

    public long languageEncodingId {
      get { return this.languageEncodingIdField; }
      set { this.languageEncodingIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public UserFilter siteUserFilter {
      get { return this.siteUserFilterField; }
      set { this.siteUserFilterField = value; }
    }

    public long traffickerType {
      get { return this.traffickerTypeField; }
      set { this.traffickerTypeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public UserFilter userRoleUserFilter {
      get { return this.userRoleUserFilterField; }
      set { this.userRoleUserFilterField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserRecordSet : PagedRecordSet {
    private User[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public User[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "adSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(OverridableAdPropertiesBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaExitOverride))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(BrowserVersion))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(AreaCode))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PlacementAssignment))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class AdRemoteService : DfaSoapClient {
    public AdRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/ad";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdBase")]
    public AdBase getAd(long adId) {
      object[] results = this.Invoke("getAd", new object[] {adId});
      return ((AdBase) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdSaveResult")]
    public AdSaveResult saveAd(AdBase ad) {
      object[] results = this.Invoke("saveAd", new object[] {ad});
      return ((AdSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteAd(long adId) {
      this.Invoke("deleteAd", new object[] {adId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdCopyResults")]
    public AdCopyResult[] copyAds(AdCopyRequest[] adCopyRequests) {
      object[] results = this.Invoke("copyAds", new object[] {adCopyRequests});
      return ((AdCopyResult[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdRecordSet")]
    public AdRecordSet getAds(AdSearchCriteria adSearchCriteria) {
      object[] results = this.Invoke("getAds", new object[] {adSearchCriteria});
      return ((AdRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdTypes")]
    public AdType[] getAdTypes() {
      object[] results = this.Invoke("getAdTypes", new object[0]);
      return ((AdType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("OverridableAdPropertiesSaveResult")]
    public OverridableAdPropertiesSaveResult overrideAdProperties(OverridableAdProperties overridableAdProperties) {
      object[] results = this.Invoke("overrideAdProperties", new object[] {overridableAdProperties});
      return ((OverridableAdPropertiesSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("DesignatedMarketAreas")]
    public DesignatedMarketArea[] getDesignatedMarketAreas() {
      object[] results = this.Invoke("getDesignatedMarketAreas", new object[0]);
      return ((DesignatedMarketArea[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Countries")]
    public Country[] getCountries() {
      object[] results = this.Invoke("getCountries", new object[0]);
      return ((Country[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Regions")]
    public Region[] getRegions(long[] countryIds) {
      object[] results = this.Invoke("getRegions", new object[] {countryIds});
      return ((Region[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("States")]
    public State[] getStates(long[] countryIds) {
      object[] results = this.Invoke("getStates", new object[] {countryIds});
      return ((State[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AreaCodes")]
    public AreaCode[] getAreaCodes(long[] countryIds) {
      object[] results = this.Invoke("getAreaCodes", new object[] {countryIds});
      return ((AreaCode[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Cities")]
    public City[] getCities(CitySearchCriteria citySearchCriteria) {
      object[] results = this.Invoke("getCities", new object[] {citySearchCriteria});
      return ((City[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Browsers")]
    public Browser[] getBrowsers() {
      object[] results = this.Invoke("getBrowsers", new object[0]);
      return ((Browser[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Bandwidths")]
    public Bandwidth[] getBandwidths() {
      object[] results = this.Invoke("getBandwidths", new object[0]);
      return ((Bandwidth[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("OperatingSystems")]
    public OperatingSystem[] getOperatingSystems() {
      object[] results = this.Invoke("getOperatingSystems", new object[0]);
      return ((OperatingSystem[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserListGroups")]
    public UserListGroup[] getUserListGroupsByCriteria(UserListSearchCriteria searchCriteria) {
      object[] results = this.Invoke("getUserListGroupsByCriteria", new object[] {searchCriteria});
      return ((UserListGroup[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserLists")]
    public UserList[] getUserListsByCriteria(UserListSearchCriteria searchCriteria) {
      object[] results = this.Invoke("getUserListsByCriteria", new object[] {searchCriteria});
      return ((UserList[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("DomainTypes")]
    public DomainType[] getDomainTypes() {
      object[] results = this.Invoke("getDomainTypes", new object[0]);
      return ((DomainType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("OSPs")]
    public OSP[] getOSPs() {
      object[] results = this.Invoke("getOSPs", new object[0]);
      return ((OSP[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("ISPs")]
    public ISP[] getISPs() {
      object[] results = this.Invoke("getISPs", new object[0]);
      return ((ISP[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("DomainNameRecordSet")]
    public DomainNameRecordSet getDomainNamesBySearchCriteria(DomainNameSearchCriteria searchCriteria) {
      object[] results = this.Invoke("getDomainNamesBySearchCriteria", new object[] {searchCriteria});
      return ((DomainNameRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("MobilePlatforms")]
    public MobilePlatform[] getMobilePlatforms() {
      object[] results = this.Invoke("getMobilePlatforms", new object[0]);
      return ((MobilePlatform[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeAdAssociationSaveResults")]
    public CreativeAdAssociationSaveResultSet updateCreativeAssignmentProperties(CreativeAdAssociationUpdateRequest associationUpdateRequest) {
      object[] results = this.Invoke("updateCreativeAssignmentProperties", new object[] {associationUpdateRequest});
      return ((CreativeAdAssociationSaveResultSet) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeAdAssociationSaveResultSet {
    private bool inErrorField;

    private CreativeAdAssociationSaveResult[] saveResultsField;

    public bool inError {
      get { return this.inErrorField; }
      set { this.inErrorField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeAdAssociationSaveResult[] saveResults {
      get { return this.saveResultsField; }
      set { this.saveResultsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeAdAssociationSaveResult : SaveResult {
    private string errorMessageField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdCopyResult : SaveResult {
    private AdCopyRequest adCopyRequestField;

    private string errorMessageField;

    private string nameField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public AdCopyRequest adCopyRequest {
      get { return this.adCopyRequestField; }
      set { this.adCopyRequestField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdCopyRequest {
    private long adIdField;

    private long campaignIdField;

    private bool copyCreativeAssociationField;

    private bool copyPlacementAssignmentField;

    public long adId {
      get { return this.adIdField; }
      set { this.adIdField = value; }
    }

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    public bool copyCreativeAssociation {
      get { return this.copyCreativeAssociationField; }
      set { this.copyCreativeAssociationField = value; }
    }

    public bool copyPlacementAssignment {
      get { return this.copyPlacementAssignmentField; }
      set { this.copyPlacementAssignmentField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeAdAssociationUpdateRequest {
    private long[] adIdsField;

    private bool associationStatusField;

    private string clickThroughUrlField;

    private long creativeIdField;

    private System.DateTime? endDateField;

    private string[] propertiesToUpdateField;

    private System.DateTime? startDateField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] adIds {
      get { return this.adIdsField; }
      set { this.adIdsField = value; }
    }

    public bool associationStatus {
      get { return this.associationStatusField; }
      set { this.associationStatusField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string clickThroughUrl {
      get { return this.clickThroughUrlField; }
      set { this.clickThroughUrlField = value; }
    }

    public long creativeId {
      get { return this.creativeIdField; }
      set { this.creativeIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? endDate {
      get { return this.endDateField; }
      set { this.endDateField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string[] propertiesToUpdate {
      get { return this.propertiesToUpdateField; }
      set { this.propertiesToUpdateField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? startDate {
      get { return this.startDateField; }
      set { this.startDateField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CitySearchCriteria {
    private long[] countryIdsField;

    private long[] regionIdsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] countryIds {
      get { return this.countryIdsField; }
      set { this.countryIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] regionIds {
      get { return this.regionIdsField; }
      set { this.regionIdsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class OverridableAdPropertiesSaveResult {
    private OverridableAdPropertiesKey overridableAdPropertiesKeyField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public OverridableAdPropertiesKey overridableAdPropertiesKey {
      get { return this.overridableAdPropertiesKeyField; }
      set { this.overridableAdPropertiesKeyField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class OverridableAdPropertiesKey {
    private long adIdField;

    private long placementIdField;

    public long adId {
      get { return this.adIdField; }
      set { this.adIdField = value; }
    }

    public long placementId {
      get { return this.placementIdField; }
      set { this.placementIdField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(OverridableAdProperties))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class OverridableAdPropertiesBase {
    private AreaCode[] areaCodesField;

    private long audienceSegmentIdField;

    private City[] citiesField;

    private int costTypeField;

    private CountryTargetingCriteria countryTargetingCriteriaField;

    private long deliveryLimitField;

    private bool deliveryLimitEnabledField;

    private DesignatedMarketArea[] designatedMarketAreasField;

    private System.DateTime? endTimeField;

    private int frequencyCapField;

    private long frequencyCapPeriodField;

    private bool hardCutOffField;

    private OverridableAdPropertiesKey overridableAdPropertiesKeyField;

    private string[] postalCodesField;

    private int priorityField;

    private int ratioField;

    private System.DateTime? startTimeField;

    private State[] statesField;

    private UserListExpression userListExpressionField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public AreaCode[] areaCodes {
      get { return this.areaCodesField; }
      set { this.areaCodesField = value; }
    }

    public long audienceSegmentId {
      get { return this.audienceSegmentIdField; }
      set { this.audienceSegmentIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public City[] cities {
      get { return this.citiesField; }
      set { this.citiesField = value; }
    }

    public int costType {
      get { return this.costTypeField; }
      set { this.costTypeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CountryTargetingCriteria countryTargetingCriteria {
      get { return this.countryTargetingCriteriaField; }
      set { this.countryTargetingCriteriaField = value; }
    }

    public long deliveryLimit {
      get { return this.deliveryLimitField; }
      set { this.deliveryLimitField = value; }
    }

    public bool deliveryLimitEnabled {
      get { return this.deliveryLimitEnabledField; }
      set { this.deliveryLimitEnabledField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DesignatedMarketArea[] designatedMarketAreas {
      get { return this.designatedMarketAreasField; }
      set { this.designatedMarketAreasField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? endTime {
      get { return this.endTimeField; }
      set { this.endTimeField = value; }
    }

    public int frequencyCap {
      get { return this.frequencyCapField; }
      set { this.frequencyCapField = value; }
    }

    public long frequencyCapPeriod {
      get { return this.frequencyCapPeriodField; }
      set { this.frequencyCapPeriodField = value; }
    }

    public bool hardCutOff {
      get { return this.hardCutOffField; }
      set { this.hardCutOffField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public OverridableAdPropertiesKey overridableAdPropertiesKey {
      get { return this.overridableAdPropertiesKeyField; }
      set { this.overridableAdPropertiesKeyField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string[] postalCodes {
      get { return this.postalCodesField; }
      set { this.postalCodesField = value; }
    }

    public int priority {
      get { return this.priorityField; }
      set { this.priorityField = value; }
    }

    public int ratio {
      get { return this.ratioField; }
      set { this.ratioField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? startTime {
      get { return this.startTimeField; }
      set { this.startTimeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public State[] states {
      get { return this.statesField; }
      set { this.statesField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public UserListExpression userListExpression {
      get { return this.userListExpressionField; }
      set { this.userListExpressionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserList : Base {
    private string descriptionField;

    private int estimatedUserCountField;

    private System.DateTime? modifiedTimestampField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }

    public int estimatedUserCount {
      get { return this.estimatedUserCountField; }
      set { this.estimatedUserCountField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? modifiedTimestamp {
      get { return this.modifiedTimestampField; }
      set { this.modifiedTimestampField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserListGroup : Base {
    private string descriptionField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Region : Base {
    private long countryIdField;

    public long countryId {
      get { return this.countryIdField; }
      set { this.countryIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DomainName : DomainNameBase {
    private string companyNameField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string companyName {
      get { return this.companyNameField; }
      set { this.companyNameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class OverridableAdProperties : OverridableAdPropertiesBase {
    private ClickThroughUrl clickThroughUrlField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ClickThroughUrl clickThroughUrl {
      get { return this.clickThroughUrlField; }
      set { this.clickThroughUrlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DomainNameRecordSet : PagedRecordSet {
    private DomainName[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DomainName[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdRecordSet : PagedRecordSet {
    private AdBase[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public AdBase[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserListSearchCriteria : SearchCriteriaBase {
    private long advertiserIdField;

    private long[] userListGroupIdsField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] userListGroupIds {
      get { return this.userListGroupIdsField; }
      set { this.userListGroupIdsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class DomainNameSearchCriteria : PageableSearchCriteriaBase {
    private string domainField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string domain {
      get { return this.domainField; }
      set { this.domainField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdSearchCriteria : PageableSearchCriteriaBase {
    private ActiveFilter activeFilterField;

    private long campaignIdField;

    private long[] sizeIdsField;

    private long[] typeIdsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ActiveFilter activeFilter {
      get { return this.activeFilterField; }
      set { this.activeFilterField = value; }
    }

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] sizeIds {
      get { return this.sizeIdsField; }
      set { this.sizeIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] typeIds {
      get { return this.typeIdsField; }
      set { this.typeIdsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "userroleSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class UserRoleRemoteService : DfaSoapClient {
    public UserRoleRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/userrole";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserRoleSaveResult")]
    public UserRoleSaveResult saveUserRole(UserRole userRole) {
      object[] results = this.Invoke("saveUserRole", new object[] {userRole});
      return ((UserRoleSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserRole")]
    public UserRole getUserRole(long userRoleId) {
      object[] results = this.Invoke("getUserRole", new object[] {userRoleId});
      return ((UserRole) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserRoleRecordSet")]
    public UserRoleRecordSet getUserRoles(UserRoleSearchCriteria userRoleSearchCriteria) {
      object[] results = this.Invoke("getUserRoles", new object[] {userRoleSearchCriteria});
      return ((UserRoleRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("UserRoleSummaryRecordSet")]
    public UserRoleSummaryRecordSet getUserRoleSummaries(UserRoleSearchCriteria userRoleSearchCriteria) {
      object[] results = this.Invoke("getUserRoleSummaries", new object[] {userRoleSearchCriteria});
      return ((UserRoleSummaryRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Permissions")]
    public Permission[] getAvailablePermissions(long subnetworkId) {
      object[] results = this.Invoke("getAvailablePermissions", new object[] {subnetworkId});
      return ((Permission[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteUserRole(long id) {
      this.Invoke("deleteUserRole", new object[] {id});
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserRole : UserRoleBase {
    private long parentUserRoleIdField;

    private Permission[] permissionsField;

    private long totalAssignedUsersField;

    public long parentUserRoleId {
      get { return this.parentUserRoleIdField; }
      set { this.parentUserRoleIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Permission[] permissions {
      get { return this.permissionsField; }
      set { this.permissionsField = value; }
    }

    public long totalAssignedUsers {
      get { return this.totalAssignedUsersField; }
      set { this.totalAssignedUsersField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserRoleSummaryRecordSet : PagedRecordSet {
    private UserRoleSummary[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public UserRoleSummary[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserRoleSummary : UserRoleBase {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(UserRoleSummary))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(UserRole))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class UserRoleBase : Base {
    private bool defaultUserRoleField;

    private long subnetworkIdField;

    public bool defaultUserRole {
      get { return this.defaultUserRoleField; }
      set { this.defaultUserRoleField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserRoleRecordSet : PagedRecordSet {
    private UserRole[] userRolesField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public UserRole[] userRoles {
      get { return this.userRolesField; }
      set { this.userRolesField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserRoleSearchCriteria : PageableSearchCriteriaBase {
    private bool parentNetworkUserRolesOnlyField;

    private SortOrder sortOrderField;

    private long subnetworkIdField;

    public bool parentNetworkUserRolesOnly {
      get { return this.parentNetworkUserRolesOnlyField; }
      set { this.parentNetworkUserRolesOnlyField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class UserRoleSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "advertiserSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  public partial class AdvertiserRemoteService : DfaSoapClient {
    public AdvertiserRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/advertiser";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdvertiserRecordSet")]
    public AdvertiserRecordSet getAdvertisers(AdvertiserSearchCriteria advertiserSearchCriteria) {
      object[] results = this.Invoke("getAdvertisers", new object[] {advertiserSearchCriteria});
      return ((AdvertiserRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdvertiserSaveResult")]
    public AdvertiserSaveResult saveAdvertiser(Advertiser advertiser) {
      object[] results = this.Invoke("saveAdvertiser", new object[] {advertiser});
      return ((AdvertiserSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteAdvertiser(long advertiserId) {
      this.Invoke("deleteAdvertiser", new object[] {advertiserId});
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvertiserSearchCriteria : PageableSearchCriteriaBase {
    private long[] advertiserGroupIdsField;

    private bool includeAdvertisersWithOutGroupOnlyField;

    private bool includeInventoryAdvertisersOnlyField;

    private long[] spotIdsField;

    private long subnetworkIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] advertiserGroupIds {
      get { return this.advertiserGroupIdsField; }
      set { this.advertiserGroupIdsField = value; }
    }

    public bool includeAdvertisersWithOutGroupOnly {
      get { return this.includeAdvertisersWithOutGroupOnlyField; }
      set { this.includeAdvertisersWithOutGroupOnlyField = value; }
    }

    public bool includeInventoryAdvertisersOnly {
      get { return this.includeInventoryAdvertisersOnlyField; }
      set { this.includeInventoryAdvertisersOnlyField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] spotIds {
      get { return this.spotIdsField; }
      set { this.spotIdsField = value; }
    }

    public long subnetworkId {
      get { return this.subnetworkIdField; }
      set { this.subnetworkIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvertiserSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvertiserRecordSet : PagedRecordSet {
    private Advertiser[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Advertiser[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "advertisergroupSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class AdvertiserGroupRemoteService : DfaSoapClient {
    public AdvertiserGroupRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/advertisergroup";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdvertiserGroup")]
    public AdvertiserGroup getAdvertiserGroup(long advertiserGroupId) {
      object[] results = this.Invoke("getAdvertiserGroup", new object[] {advertiserGroupId});
      return ((AdvertiserGroup) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdvertiserGroupRecordSet")]
    public AdvertiserGroupRecordSet getAdvertiserGroups(AdvertiserGroupSearchCriteria advertiserGroupSearchCriteria) {
      object[] results = this.Invoke("getAdvertiserGroups", new object[] {advertiserGroupSearchCriteria});
      return ((AdvertiserGroupRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("AdvertiserGroupSaveResult")]
    public AdvertiserGroupSaveResult saveAdvertiserGroup(AdvertiserGroup advertiserGroup) {
      object[] results = this.Invoke("saveAdvertiserGroup", new object[] {advertiserGroup});
      return ((AdvertiserGroupSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteAdvertiserGroup(long advertiserGroupId) {
      this.Invoke("deleteAdvertiserGroup", new object[] {advertiserGroupId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void assignAdvertisersToAdvertiserGroup(long advertiserGroupId, long[] advertiserIds) {
      this.Invoke("assignAdvertisersToAdvertiserGroup", new object[] {advertiserGroupId, advertiserIds});
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvertiserGroup : AdvertiserGroupBase {
    private int advertiserCountField;

    public int advertiserCount {
      get { return this.advertiserCountField; }
      set { this.advertiserCountField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(AdvertiserGroup))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvertiserGroupBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvertiserGroupSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvertiserGroupRecordSet : PagedRecordSet {
    private AdvertiserGroup[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public AdvertiserGroup[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class AdvertiserGroupSearchCriteria : PageableSearchCriteriaBase {
    private SortOrder sortOrderField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "campaignSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SpotlightActivityWeight))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class CampaignRemoteService : DfaSoapClient {
    public CampaignRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/campaign";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("Campaign")]
    public Campaign getCampaign(long campaignId) {
      object[] results = this.Invoke("getCampaign", new object[] {campaignId});
      return ((Campaign) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CampaignSaveResult")]
    public CampaignSaveResult saveCampaign(Campaign campaign) {
      object[] results = this.Invoke("saveCampaign", new object[] {campaign});
      return ((CampaignSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteCampaign(long campaignId) {
      this.Invoke("deleteCampaign", new object[] {campaignId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("LandingPageSaveResult")]
    public LandingPageSaveResult saveLandingPage(LandingPage landingPage) {
      object[] results = this.Invoke("saveLandingPage", new object[] {landingPage});
      return ((LandingPageSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("LandingPageRecordSet")]
    public LandingPageRecordSet getLandingPagesForCampaign(long campaignId) {
      object[] results = this.Invoke("getLandingPagesForCampaign", new object[] {campaignId});
      return ((LandingPageRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("addLandingPageToCampaignReturn")]
    public bool addLandingPageToCampaign(long campaignId, LandingPage[] landingPages) {
      object[] results = this.Invoke("addLandingPageToCampaign", new object[] {campaignId, landingPages});
      return ((bool) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CampaignRecordSet")]
    public CampaignRecordSet getCampaignsByCriteria(CampaignSearchCriteria searchCriteria) {
      object[] results = this.Invoke("getCampaignsByCriteria", new object[] {searchCriteria});
      return ((CampaignRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CampaignMigrationResult")]
    public CampaignMigrationResult migrateCampaign(CampaignMigrationRequest campaignMigrationRequest) {
      object[] results = this.Invoke("migrateCampaign", new object[] {campaignMigrationRequest});
      return ((CampaignMigrationResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CampaignCopyResult")]
    public CampaignCopyResult[] copyCampaigns(CampaignCopyRequest[] campaignCopyRequests) {
      object[] results = this.Invoke("copyCampaigns", new object[] {campaignCopyRequests});
      return ((CampaignCopyResult[]) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CampaignMigrationRequest {
    private long campaignIdField;

    private string landingPageUrlField;

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string landingPageUrl {
      get { return this.landingPageUrlField; }
      set { this.landingPageUrlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CampaignRecordSet : PagedRecordSet {
    private Campaign[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Campaign[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CampaignSearchCriteria : PageableSearchCriteriaBase {
    private long[] advertiserIdsField;

    private ActiveFilter archiveFilterField;

    private SortOrder sortOrderField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] advertiserIds {
      get { return this.advertiserIdsField; }
      set { this.advertiserIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ActiveFilter archiveFilter {
      get { return this.archiveFilterField; }
      set { this.archiveFilterField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class LandingPageRecordSet {
    private LandingPage[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public LandingPage[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class LandingPage : LandingPageBase {
    private string urlField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string url {
      get { return this.urlField; }
      set { this.urlField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(LandingPage))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class LandingPageBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CampaignCopyRequest {
    private long campaignIdField;

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CampaignCopyResult : SaveResult {
    private CampaignCopyRequest copyRequestField;

    private string errorMessageField;

    private string nameField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CampaignCopyRequest copyRequest {
      get { return this.copyRequestField; }
      set { this.copyRequestField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CampaignMigrationResult : SaveResult {
    private string errorMessageField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class LandingPageSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CampaignSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "changelogSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class ChangeLogRemoteService : DfaSoapClient {
    public ChangeLogRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/changelog";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("ChangeLogObjectTypes")]
    public ChangeLogObjectType[] getChangeLogObjectTypes() {
      object[] results = this.Invoke("getChangeLogObjectTypes", new object[0]);
      return ((ChangeLogObjectType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("ChangeLogRecordSet")]
    public ChangeLogRecordSet getChangeLogRecords(ChangeLogRecordSearchCriteria changeLogRecordSearchCriteria) {
      object[] results = this.Invoke("getChangeLogRecords", new object[] {changeLogRecordSearchCriteria});
      return ((ChangeLogRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("ChangeLogRecord")]
    public ChangeLogRecord getChangeLogRecord(long changeLogRecordId) {
      object[] results = this.Invoke("getChangeLogRecord", new object[] {changeLogRecordId});
      return ((ChangeLogRecord) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("ChangeLogRecord")]
    public ChangeLogRecord getChangeLogRecordForObjectType(long changeLogRecordId, long objectTypeId) {
      object[] results = this.Invoke("getChangeLogRecordForObjectType", new object[] {changeLogRecordId, objectTypeId});
      return ((ChangeLogRecord) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void updateChangeLogRecordComments(long changeLogRecordId, string comments) {
      this.Invoke("updateChangeLogRecordComments", new object[] {changeLogRecordId, comments});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void updateChangeLogRecordCommentsForObjectType(long changeLogRecordId, string comments, long objectTypeId) {
      this.Invoke("updateChangeLogRecordCommentsForObjectType", new object[] {changeLogRecordId, comments, objectTypeId});
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ChangeLogObjectType : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ChangeLogRecord {
    private string actionField;

    private System.DateTime? changeDateField;

    private string commentsField;

    private string contextField;

    private long groupIdField;

    private long idField;

    private string newValueField;

    private long objectIdField;

    private string objectTypeField;

    private string oldValueField;

    private long userIdField;

    private string usernameField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string action {
      get { return this.actionField; }
      set { this.actionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? changeDate {
      get { return this.changeDateField; }
      set { this.changeDateField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string comments {
      get { return this.commentsField; }
      set { this.commentsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string context {
      get { return this.contextField; }
      set { this.contextField = value; }
    }

    public long groupId {
      get { return this.groupIdField; }
      set { this.groupIdField = value; }
    }

    public long id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string newValue {
      get { return this.newValueField; }
      set { this.newValueField = value; }
    }

    public long objectId {
      get { return this.objectIdField; }
      set { this.objectIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string objectType {
      get { return this.objectTypeField; }
      set { this.objectTypeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string oldValue {
      get { return this.oldValueField; }
      set { this.oldValueField = value; }
    }

    public long userId {
      get { return this.userIdField; }
      set { this.userIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string username {
      get { return this.usernameField; }
      set { this.usernameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ChangeLogRecordSet : PagedRecordSet {
    private ChangeLogRecord[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ChangeLogRecord[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ChangeLogRecordSearchCriteria : PageableSearchCriteriaBase {
    private long objectIdField;

    private long objectTypeIdField;

    private SortOrder sortOrderField;

    public long objectId {
      get { return this.objectIdField; }
      set { this.objectIdField = value; }
    }

    public long objectTypeId {
      get { return this.objectTypeIdField; }
      set { this.objectTypeIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "contentcategorySoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class ContentCategoryRemoteService : DfaSoapClient {
    public ContentCategoryRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/contentcategory";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("ContentCategorySaveResult")]
    public ContentCategorySaveResult saveContentCategory(ContentCategory contentCategory) {
      object[] results = this.Invoke("saveContentCategory", new object[] {contentCategory});
      return ((ContentCategorySaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("ContentCategory")]
    public ContentCategory getContentCategory(long contentCategoryId) {
      object[] results = this.Invoke("getContentCategory", new object[] {contentCategoryId});
      return ((ContentCategory) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("ContentCategoryRecordSet")]
    public ContentCategoryRecordSet getContentCategories(ContentCategorySearchCriteria searchCriteria) {
      object[] results = this.Invoke("getContentCategories", new object[] {searchCriteria});
      return ((ContentCategoryRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteContentCategory(long contentCategoryId) {
      this.Invoke("deleteContentCategory", new object[] {contentCategoryId});
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ContentCategory : ContentCategoryBase {
    private string descriptionField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(ContentCategory))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ContentCategoryBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ContentCategoryRecordSet : PagedRecordSet {
    private ContentCategory[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ContentCategory[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ContentCategorySearchCriteria : PageableSearchCriteriaBase {
    private SortOrder sortOrderField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ContentCategorySaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "creativeSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeRenderingResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RawFileSummary))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeSaveRequest))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativePlacementAssignment))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeAssetBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(FlashClickTag))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeFieldAssignment))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class CreativeRemoteService : DfaSoapClient {
    public CreativeRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/creative";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeBase")]
    public CreativeBase getCreative(long creativeId) {
      object[] results = this.Invoke("getCreative", new object[] {creativeId});
      return ((CreativeBase) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeRecordSet")]
    public CreativeRecordSet getCreatives(CreativeSearchCriteria creativeSearchCriteria) {
      object[] results = this.Invoke("getCreatives", new object[] {creativeSearchCriteria});
      return ((CreativeRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeSaveResult")]
    public CreativeSaveResult saveCreative(CreativeBase creative, long campaignId) {
      object[] results = this.Invoke("saveCreative", new object[] {creative, campaignId});
      return ((CreativeSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteCreative(long creativeId) {
      this.Invoke("deleteCreative", new object[] {creativeId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeAsset")]
    public CreativeAssetSaveResult saveCreativeAsset(CreativeAsset creativeAsset) {
      object[] results = this.Invoke("saveCreativeAsset", new object[] {creativeAsset});
      return ((CreativeAssetSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeAssetRecordSet")]
    public CreativeAssetRecordSet getCreativeAssets(CreativeAssetSearchCriteria creativeAssetSearchCriteria) {
      object[] results = this.Invoke("getCreativeAssets", new object[] {creativeAssetSearchCriteria});
      return ((CreativeAssetRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeType")]
    public CreativeType[] getCreativeTypes() {
      object[] results = this.Invoke("getCreativeTypes", new object[0]);
      return ((CreativeType[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void assignCreativeGroupsToCreative(long campaignId, long creativeId, long[] creativeGroupIds) {
      this.Invoke("assignCreativeGroupsToCreative", new object[] {campaignId, creativeId, creativeGroupIds});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeGroupRecordSet")]
    public CreativeGroupRecordSet getAssignedCreativeGroupsToCreative(long campaignId, long creativeId) {
      object[] results = this.Invoke("getAssignedCreativeGroupsToCreative", new object[] {campaignId, creativeId});
      return ((CreativeGroupRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeCampaignAssociationResults")]
    public CreativeCampaignAssociationResult[] associateCreativesToCampaign(long campaignId, long[] creativeIds) {
      object[] results = this.Invoke("associateCreativesToCampaign", new object[] {campaignId, creativeIds});
      return ((CreativeCampaignAssociationResult[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativePlacementAssignmentResults")]
    public CreativePlacementAssignmentResult[] assignCreativesToPlacements(CreativePlacementAssignment[] creativePlacementAssignments) {
      object[] results = this.Invoke("assignCreativesToPlacements", new object[] {creativePlacementAssignments});
      return ((CreativePlacementAssignmentResult[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("RichMediaCreativeBase")]
    public RichMediaCreativeBase uploadRichMediaAsset(RichMediaAssetUploadRequest uploadRequest) {
      object[] results = this.Invoke("uploadRichMediaAsset", new object[] {uploadRequest});
      return ((RichMediaCreativeBase) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("RichMediaCreativeBase")]
    public RichMediaCreativeBase replaceRichMediaAsset(string oldAssetFileName, RichMediaAssetUploadRequest replaceRequest) {
      object[] results = this.Invoke("replaceRichMediaAsset", new object[] {oldAssetFileName, replaceRequest});
      return ((RichMediaCreativeBase) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("RichMediaCreativeBase")]
    public RichMediaCreativeBase deleteRichMediaAsset(long creativeId, string assetFileName) {
      object[] results = this.Invoke("deleteRichMediaAsset", new object[] {creativeId, assetFileName});
      return ((RichMediaCreativeBase) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("RichMediaCreativeBase")]
    public RichMediaCreativeBase uploadRichMediaCreativePackage(long advertiserId, [System.Xml.Serialization.SoapElementAttribute(DataType = "base64Binary")]
byte[] fileData, bool uploadAsFlashInFlashCreative) {
      object[] results = this.Invoke("uploadRichMediaCreativePackage", new object[] {advertiserId, fileData, uploadAsFlashInFlashCreative});
      return ((RichMediaCreativeBase) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("RichMediaCreativeBase")]
    public RichMediaCreativeBase replaceRichMediaCreativePackage(long creativeId, [System.Xml.Serialization.SoapElementAttribute(DataType = "base64Binary")]
byte[] fileData) {
      object[] results = this.Invoke("replaceRichMediaCreativePackage", new object[] {creativeId, fileData});
      return ((RichMediaCreativeBase) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.w3.org/2001/XMLSchema")]
    [return: System.Xml.Serialization.SoapElementAttribute("string")]
    public string getRichMediaPreviewURL(long creativeId) {
      object[] results = this.Invoke("getRichMediaPreviewURL", new object[] {creativeId});
      return ((string) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeCopyResults")]
    public CreativeCopyResult[] copyCreative(CreativeCopyRequest[] copyRequests) {
      object[] results = this.Invoke("copyCreative", new object[] {copyRequests});
      return ((CreativeCopyResult[]) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeUploadSession")]
    public CreativeUploadSession createCreativesFromCreativeUploadSession(CreativeUploadSession creativeUploadSession) {
      object[] results = this.Invoke("createCreativesFromCreativeUploadSession", new object[] {creativeUploadSession});
      return ((CreativeUploadSession) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeUploadSession")]
    public CreativeUploadSession generateCreativeUploadSession(CreativeUploadSessionRequest creativeUploadSessionRequest) {
      object[] results = this.Invoke("generateCreativeUploadSession", new object[] {creativeUploadSessionRequest});
      return ((CreativeUploadSession) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeUploadSession")]
    public CreativeUploadSession getCompleteCreativeUploadSession(CreativeUploadSessionSummary creativeUploadSessionSummary) {
      object[] results = this.Invoke("getCompleteCreativeUploadSession", new object[] {creativeUploadSessionSummary});
      return ((CreativeUploadSession) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeUploadSession")]
    public CreativeUploadSession uploadCreativeFiles(CreativeUploadRequest creativeUploadRequest) {
      object[] results = this.Invoke("uploadCreativeFiles", new object[] {creativeUploadRequest});
      return ((CreativeUploadSession) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeRenderingResults")]
    public CreativeRenderingResult[] getCreativeRenderings(CreativeRenderingRequest creativeRenderingRequest) {
      object[] results = this.Invoke("getCreativeRenderings", new object[] {creativeRenderingRequest});
      return ((CreativeRenderingResult[]) (results[0]));
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TrackingTextCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(HTMLCreativeBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(HTMLInterstitialCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(FlashInpageCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(HTMLCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TrackingHTMLCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(MobileDisplayCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RedirectCreativeBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(InternalRedirectCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RedirectCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(InterstitialInternalRedirectCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(ImageCreativeBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(ImageCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TrackingImageCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaCreativeBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaFloatingWithReminderCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaImageWithFloatingCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaInPageCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaInPageWithOverlayCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaExpandingCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaFloatingCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaOverlayCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaImageWithOverlayCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaInPageWithFloatingCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaFlashInFlashCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class CreativeBase : Base {
    private bool activeField;

    private long advertiserIdField;

    private bool archivedField;

    private CreativeFieldAssignment[] creativeFieldAssignmentsField;

    private long sizeIdField;

    private long typeIdField;

    private int versionField;

    public bool active {
      get { return this.activeField; }
      set { this.activeField = value; }
    }

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public bool archived {
      get { return this.archivedField; }
      set { this.archivedField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeFieldAssignment[] creativeFieldAssignments {
      get { return this.creativeFieldAssignmentsField; }
      set { this.creativeFieldAssignmentsField = value; }
    }

    public long sizeId {
      get { return this.sizeIdField; }
      set { this.sizeIdField = value; }
    }

    public long typeId {
      get { return this.typeIdField; }
      set { this.typeIdField = value; }
    }

    public int version {
      get { return this.versionField; }
      set { this.versionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldAssignment {
    private long creativeFieldIdField;

    private long creativeFieldValueIdField;

    public long creativeFieldId {
      get { return this.creativeFieldIdField; }
      set { this.creativeFieldIdField = value; }
    }

    public long creativeFieldValueId {
      get { return this.creativeFieldValueIdField; }
      set { this.creativeFieldValueIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeRenderingResult {
    private long creativeIdField;

    private long renderingIdField;

    private long renderingVersionField;

    public long creativeId {
      get { return this.creativeIdField; }
      set { this.creativeIdField = value; }
    }

    public long renderingId {
      get { return this.renderingIdField; }
      set { this.renderingIdField = value; }
    }

    public long renderingVersion {
      get { return this.renderingVersionField; }
      set { this.renderingVersionField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeRenderingRequest {
    private long[] creativeIdsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] creativeIds {
      get { return this.creativeIdsField; }
      set { this.creativeIdsField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RawFile))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RawFileSummary {
    private string filenameField;

    private string mimeTypeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string filename {
      get { return this.filenameField; }
      set { this.filenameField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string mimeType {
      get { return this.mimeTypeField; }
      set { this.mimeTypeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RawFile : RawFileSummary {
    private byte[] fileDataField;

    [System.Xml.Serialization.SoapElementAttribute(DataType = "base64Binary", IsNullable = true)]
    public byte[] fileData {
      get { return this.fileDataField; }
      set { this.fileDataField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeUploadRequest {
    private CreativeUploadSessionSummary creativeUploadSessionSummaryField;

    private RawFile[] rawFilesField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeUploadSessionSummary creativeUploadSessionSummary {
      get { return this.creativeUploadSessionSummaryField; }
      set { this.creativeUploadSessionSummaryField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RawFile[] rawFiles {
      get { return this.rawFilesField; }
      set { this.rawFilesField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeUploadSession))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeUploadSessionSummary {
    private long advertiserIdField;

    private long campaignIdField;

    private long creativeUploadIdField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    public long creativeUploadId {
      get { return this.creativeUploadIdField; }
      set { this.creativeUploadIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeUploadSession : CreativeUploadSessionSummary {
    private CreativeSaveRequest[] creativeSaveRequestsField;

    private CreativeUploadFileCount fileCountField;

    private int statusField;

    private CreativeUploadFile[] uploadedFilesField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeSaveRequest[] creativeSaveRequests {
      get { return this.creativeSaveRequestsField; }
      set { this.creativeSaveRequestsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeUploadFileCount fileCount {
      get { return this.fileCountField; }
      set { this.fileCountField = value; }
    }

    public int status {
      get { return this.statusField; }
      set { this.statusField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeUploadFile[] uploadedFiles {
      get { return this.uploadedFilesField; }
      set { this.uploadedFilesField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeSaveRequest {
    private long creativeIdField;

    private string errorMessageField;

    private FlashFile flashFileField;

    private ImageFile imageFileField;

    public long creativeId {
      get { return this.creativeIdField; }
      set { this.creativeIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public FlashFile flashFile {
      get { return this.flashFileField; }
      set { this.flashFileField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ImageFile imageFile {
      get { return this.imageFileField; }
      set { this.imageFileField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class FlashFile : CreativeUploadFileBase {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(ImageFile))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(FlashFile))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class CreativeUploadFileBase : Base {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeUploadFile))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeUploadFileSummary : Base {
    private string sourcePathField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string sourcePath {
      get { return this.sourcePathField; }
      set { this.sourcePathField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeUploadFile : CreativeUploadFileSummary {
    private Dimensions dimensionsField;

    private string errorMessageField;

    private long fileLengthField;

    private bool flashFileField;

    private CreativeUploadFileSummary[] matchedFilesField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Dimensions dimensions {
      get { return this.dimensionsField; }
      set { this.dimensionsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }

    public long fileLength {
      get { return this.fileLengthField; }
      set { this.fileLengthField = value; }
    }

    public bool flashFile {
      get { return this.flashFileField; }
      set { this.flashFileField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeUploadFileSummary[] matchedFiles {
      get { return this.matchedFilesField; }
      set { this.matchedFilesField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class Dimensions {
    private int heightField;

    private int widthField;

    public int height {
      get { return this.heightField; }
      set { this.heightField = value; }
    }

    public int width {
      get { return this.widthField; }
      set { this.widthField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeGroup))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeGroupBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeGroup : CreativeGroupBase {
    private long advertiserIdField;

    private int groupNumberField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public int groupNumber {
      get { return this.groupNumberField; }
      set { this.groupNumberField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeType : Base {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaTimerEvent))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaExitEvent))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaCounterEvent))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaEventBase : Base {
    private string descriptionField;

    private long parentAssetIdField;

    private string typeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string description {
      get { return this.descriptionField; }
      set { this.descriptionField = value; }
    }

    public long parentAssetId {
      get { return this.parentAssetIdField; }
      set { this.parentAssetIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string type {
      get { return this.typeField; }
      set { this.typeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaTimerEvent : RichMediaEventBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaExitEvent : RichMediaEventBase {
    private RichMediaExitWindowProperties exitWindowPropertiesField;

    private string targetField;

    private string urlField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaExitWindowProperties exitWindowProperties {
      get { return this.exitWindowPropertiesField; }
      set { this.exitWindowPropertiesField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string target {
      get { return this.targetField; }
      set { this.targetField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string url {
      get { return this.urlField; }
      set { this.urlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaExitWindowProperties {
    private int heightField;

    private int leftField;

    private bool locationBarField;

    private bool menuBarField;

    private bool resizableField;

    private bool scrollBarsField;

    private bool statusBarField;

    private bool toolBarField;

    private int topField;

    private int widthField;

    public int height {
      get { return this.heightField; }
      set { this.heightField = value; }
    }

    public int left {
      get { return this.leftField; }
      set { this.leftField = value; }
    }

    public bool locationBar {
      get { return this.locationBarField; }
      set { this.locationBarField = value; }
    }

    public bool menuBar {
      get { return this.menuBarField; }
      set { this.menuBarField = value; }
    }

    public bool resizable {
      get { return this.resizableField; }
      set { this.resizableField = value; }
    }

    public bool scrollBars {
      get { return this.scrollBarsField; }
      set { this.scrollBarsField = value; }
    }

    public bool statusBar {
      get { return this.statusBarField; }
      set { this.statusBarField = value; }
    }

    public bool toolBar {
      get { return this.toolBarField; }
      set { this.toolBarField = value; }
    }

    public int top {
      get { return this.topField; }
      set { this.topField = value; }
    }

    public int width {
      get { return this.widthField; }
      set { this.widthField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaCounterEvent : RichMediaEventBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ImageFile : CreativeUploadFileBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeUploadFileCount {
    private int flashFileCountField;

    private int imageFileCountField;

    private int otherFileCountField;

    public int flashFileCount {
      get { return this.flashFileCountField; }
      set { this.flashFileCountField = value; }
    }

    public int imageFileCount {
      get { return this.imageFileCountField; }
      set { this.imageFileCountField = value; }
    }

    public int otherFileCount {
      get { return this.otherFileCountField; }
      set { this.otherFileCountField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeUploadSessionRequest {
    private long advertiserIdField;

    private long campaignIdField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaAssetUploadRequest {
    private string assetFileNameField;

    private long creativeIdField;

    private byte[] fileDataField;

    private int parentAssetIdField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string assetFileName {
      get { return this.assetFileNameField; }
      set { this.assetFileNameField = value; }
    }

    public long creativeId {
      get { return this.creativeIdField; }
      set { this.creativeIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(DataType = "base64Binary", IsNullable = true)]
    public byte[] fileData {
      get { return this.fileDataField; }
      set { this.fileDataField = value; }
    }

    public int parentAssetId {
      get { return this.parentAssetIdField; }
      set { this.parentAssetIdField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativePlacementAssignmentResult))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativePlacementAssignment {
    private string adNameField;

    private long creativeIdField;

    private long placementIdField;

    private long[] placementIdsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string adName {
      get { return this.adNameField; }
      set { this.adNameField = value; }
    }

    public long creativeId {
      get { return this.creativeIdField; }
      set { this.creativeIdField = value; }
    }

    public long placementId {
      get { return this.placementIdField; }
      set { this.placementIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] placementIds {
      get { return this.placementIdsField; }
      set { this.placementIdsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativePlacementAssignmentResult : CreativePlacementAssignment {
    private long adIdField;

    private string errorMessageField;

    public long adId {
      get { return this.adIdField; }
      set { this.adIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeGroupRecordSet {
    private CreativeGroup[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeGroup[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeAssetRecordSet {
    private CreativeAssetBase[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeAssetBase[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeAsset))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class CreativeAssetBase {
    private long advertiserIdField;

    private bool forHTMLCreativesField;

    private string nameField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public bool forHTMLCreatives {
      get { return this.forHTMLCreativesField; }
      set { this.forHTMLCreativesField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeAsset : CreativeAssetBase {
    private byte[] contentField;

    [System.Xml.Serialization.SoapElementAttribute(DataType = "base64Binary", IsNullable = true)]
    public byte[] content {
      get { return this.contentField; }
      set { this.contentField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeAssetSearchCriteria {
    private long advertiserIdField;

    private string assetFilenameField;

    private bool forHTMLCreativesField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string assetFilename {
      get { return this.assetFilenameField; }
      set { this.assetFilenameField = value; }
    }

    public bool forHTMLCreatives {
      get { return this.forHTMLCreativesField; }
      set { this.forHTMLCreativesField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeAssetSaveResult {
    private FlashClickTag[] clickTagsField;

    private int flashVersionField;

    private Dimensions frameSizeField;

    private string savedFilenameField;

    private Size sizeField;

    private string urlField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public FlashClickTag[] clickTags {
      get { return this.clickTagsField; }
      set { this.clickTagsField = value; }
    }

    public int flashVersion {
      get { return this.flashVersionField; }
      set { this.flashVersionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Dimensions frameSize {
      get { return this.frameSizeField; }
      set { this.frameSizeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string savedFilename {
      get { return this.savedFilenameField; }
      set { this.savedFilenameField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Size size {
      get { return this.sizeField; }
      set { this.sizeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string url {
      get { return this.urlField; }
      set { this.urlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class FlashClickTag {
    private string nameField;

    private string valueField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string value {
      get { return this.valueField; }
      set { this.valueField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeCopyRequest {
    private long advertiserIdField;

    private long campaignIdField;

    private long copyModeField;

    private long creativeIdField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    public long copyMode {
      get { return this.copyModeField; }
      set { this.copyModeField = value; }
    }

    public long creativeId {
      get { return this.creativeIdField; }
      set { this.creativeIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeCopyResult : SaveResult {
    private CreativeCopyRequest copyRequestField;

    private string errorMessageField;

    private string nameField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeCopyRequest copyRequest {
      get { return this.copyRequestField; }
      set { this.copyRequestField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string name {
      get { return this.nameField; }
      set { this.nameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeCampaignAssociationResult : SaveResult {
    private long campaignIdField;

    private string errorMessageField;

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string errorMessage {
      get { return this.errorMessageField; }
      set { this.errorMessageField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeSaveResult : SaveResult {
    private long adIdField;

    public long adId {
      get { return this.adIdField; }
      set { this.adIdField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeRecordSet : PagedRecordSet {
    private CreativeBase[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeBase[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeSearchCriteria : PageableSearchCriteriaBase {
    private ActiveFilter activeStatusField;

    private long advertiserIdField;

    private long archiveStatusFilterField;

    private long campaignIdField;

    private DateInterval creativeCreationDateRangeField;

    private bool studioCreativeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public ActiveFilter activeStatus {
      get { return this.activeStatusField; }
      set { this.activeStatusField = value; }
    }

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public long archiveStatusFilter {
      get { return this.archiveStatusFilterField; }
      set { this.archiveStatusFilterField = value; }
    }

    public long campaignId {
      get { return this.campaignIdField; }
      set { this.campaignIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public DateInterval creativeCreationDateRange {
      get { return this.creativeCreationDateRangeField; }
      set { this.creativeCreationDateRangeField = value; }
    }

    public bool studioCreative {
      get { return this.studioCreativeField; }
      set { this.studioCreativeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class TargetWindow {
    private string optionField;

    private string otherValueField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string option {
      get { return this.optionField; }
      set { this.optionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string otherValue {
      get { return this.otherValueField; }
      set { this.otherValueField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class FSCommand {
    private int leftField;

    private string optionField;

    private int topField;

    private Dimensions windowDimensionsField;

    public int left {
      get { return this.leftField; }
      set { this.leftField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string option {
      get { return this.optionField; }
      set { this.optionField = value; }
    }

    public int top {
      get { return this.topField; }
      set { this.topField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Dimensions windowDimensions {
      get { return this.windowDimensionsField; }
      set { this.windowDimensionsField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(MobileDisplayCreativeAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(HTMLCreativeFlashAsset))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class HTMLCreativeAsset {
    private string assetFilenameField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string assetFilename {
      get { return this.assetFilenameField; }
      set { this.assetFilenameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class MobileDisplayCreativeAsset : HTMLCreativeAsset {
    private Dimensions dimesionsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Dimensions dimesions {
      get { return this.dimesionsField; }
      set { this.dimesionsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class HTMLCreativeFlashAsset : HTMLCreativeAsset {
    private FlashClickTag[] clickTagsField;

    private int flashVersionField;

    private Dimensions frameSizeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public FlashClickTag[] clickTags {
      get { return this.clickTagsField; }
      set { this.clickTagsField = value; }
    }

    public int flashVersion {
      get { return this.flashVersionField; }
      set { this.flashVersionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public Dimensions frameSize {
      get { return this.frameSizeField; }
      set { this.frameSizeField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaVideoAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaImageAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaFlashAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaReminderAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaExpandingAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaOverlayAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaInPageAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaFloatingAsset))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaAsset {
    private string fileNameField;

    private int fileSizeField;

    private long idField;

    private long parentAssetIdField;

    private string typeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string fileName {
      get { return this.fileNameField; }
      set { this.fileNameField = value; }
    }

    public int fileSize {
      get { return this.fileSizeField; }
      set { this.fileSizeField = value; }
    }

    public long id {
      get { return this.idField; }
      set { this.idField = value; }
    }

    public long parentAssetId {
      get { return this.parentAssetIdField; }
      set { this.parentAssetIdField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string type {
      get { return this.typeField; }
      set { this.typeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaVideoAsset : RichMediaAsset {
    private string progressiveUrlField;

    private string streamingUrlField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string progressiveUrl {
      get { return this.progressiveUrlField; }
      set { this.progressiveUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string streamingUrl {
      get { return this.streamingUrlField; }
      set { this.streamingUrlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaImageAsset : RichMediaAsset {
    private string altTextField;

    private bool backupImageField;

    private RichMediaExitEvent exitEventField;

    private int heightField;

    private int widthField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string altText {
      get { return this.altTextField; }
      set { this.altTextField = value; }
    }

    public bool backupImage {
      get { return this.backupImageField; }
      set { this.backupImageField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaExitEvent exitEvent {
      get { return this.exitEventField; }
      set { this.exitEventField = value; }
    }

    public int height {
      get { return this.heightField; }
      set { this.heightField = value; }
    }

    public int width {
      get { return this.widthField; }
      set { this.widthField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaReminderAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaExpandingAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaOverlayAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaInPageAsset))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaFloatingAsset))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaFlashAsset : RichMediaAsset {
    private string creativeFormatField;

    private int heightField;

    private int widthField;

    private string wmodeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string creativeFormat {
      get { return this.creativeFormatField; }
      set { this.creativeFormatField = value; }
    }

    public int height {
      get { return this.heightField; }
      set { this.heightField = value; }
    }

    public int width {
      get { return this.widthField; }
      set { this.widthField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string wmode {
      get { return this.wmodeField; }
      set { this.wmodeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaReminderAsset : RichMediaFlashAsset {
    private string zIndexField;

    private int durationField;

    private int leftField;

    private bool leftLockPositionField;

    private string leftUnitField;

    private int startTimeField;

    private int topField;

    private bool topLockPositionField;

    private string topUnitField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string ZIndex {
      get { return this.zIndexField; }
      set { this.zIndexField = value; }
    }

    public int duration {
      get { return this.durationField; }
      set { this.durationField = value; }
    }

    public int left {
      get { return this.leftField; }
      set { this.leftField = value; }
    }

    public bool leftLockPosition {
      get { return this.leftLockPositionField; }
      set { this.leftLockPositionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string leftUnit {
      get { return this.leftUnitField; }
      set { this.leftUnitField = value; }
    }

    public int startTime {
      get { return this.startTimeField; }
      set { this.startTimeField = value; }
    }

    public int top {
      get { return this.topField; }
      set { this.topField = value; }
    }

    public bool topLockPosition {
      get { return this.topLockPositionField; }
      set { this.topLockPositionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string topUnit {
      get { return this.topUnitField; }
      set { this.topUnitField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaExpandingAsset : RichMediaFlashAsset {
    private string zIndexField;

    private RichMediaImageAsset alternateImageField;

    private int bottomOffsetField;

    private int expandedHeightField;

    private int expandedWidthField;

    private bool hideDropDownsField;

    private bool hideFlashObjectsField;

    private bool hideIframesField;

    private bool hideJavaAppletsField;

    private bool hideScrollBarsField;

    private int leftOffsetField;

    private bool multidirectionalField;

    private bool pushDownField;

    private double pushdownAnimationTimeField;

    private int rightOffsetField;

    private int topOffsetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string ZIndex {
      get { return this.zIndexField; }
      set { this.zIndexField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaImageAsset alternateImage {
      get { return this.alternateImageField; }
      set { this.alternateImageField = value; }
    }

    public int bottomOffset {
      get { return this.bottomOffsetField; }
      set { this.bottomOffsetField = value; }
    }

    public int expandedHeight {
      get { return this.expandedHeightField; }
      set { this.expandedHeightField = value; }
    }

    public int expandedWidth {
      get { return this.expandedWidthField; }
      set { this.expandedWidthField = value; }
    }

    public bool hideDropDowns {
      get { return this.hideDropDownsField; }
      set { this.hideDropDownsField = value; }
    }

    public bool hideFlashObjects {
      get { return this.hideFlashObjectsField; }
      set { this.hideFlashObjectsField = value; }
    }

    public bool hideIframes {
      get { return this.hideIframesField; }
      set { this.hideIframesField = value; }
    }

    public bool hideJavaApplets {
      get { return this.hideJavaAppletsField; }
      set { this.hideJavaAppletsField = value; }
    }

    public bool hideScrollBars {
      get { return this.hideScrollBarsField; }
      set { this.hideScrollBarsField = value; }
    }

    public int leftOffset {
      get { return this.leftOffsetField; }
      set { this.leftOffsetField = value; }
    }

    public bool multidirectional {
      get { return this.multidirectionalField; }
      set { this.multidirectionalField = value; }
    }

    public bool pushDown {
      get { return this.pushDownField; }
      set { this.pushDownField = value; }
    }

    public double pushdownAnimationTime {
      get { return this.pushdownAnimationTimeField; }
      set { this.pushdownAnimationTimeField = value; }
    }

    public int rightOffset {
      get { return this.rightOffsetField; }
      set { this.rightOffsetField = value; }
    }

    public int topOffset {
      get { return this.topOffsetField; }
      set { this.topOffsetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaOverlayAsset : RichMediaFlashAsset {
    private bool addressBarField;

    private int durationField;

    private int leftField;

    private string leftUnitField;

    private bool menuBarField;

    private bool overlayField;

    private int startTimeField;

    private bool statusBarField;

    private bool toolBarField;

    private int topField;

    private string topUnitField;

    private string windowTitleField;

    public bool addressBar {
      get { return this.addressBarField; }
      set { this.addressBarField = value; }
    }

    public int duration {
      get { return this.durationField; }
      set { this.durationField = value; }
    }

    public int left {
      get { return this.leftField; }
      set { this.leftField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string leftUnit {
      get { return this.leftUnitField; }
      set { this.leftUnitField = value; }
    }

    public bool menuBar {
      get { return this.menuBarField; }
      set { this.menuBarField = value; }
    }

    public bool overlay {
      get { return this.overlayField; }
      set { this.overlayField = value; }
    }

    public int startTime {
      get { return this.startTimeField; }
      set { this.startTimeField = value; }
    }

    public bool statusBar {
      get { return this.statusBarField; }
      set { this.statusBarField = value; }
    }

    public bool toolBar {
      get { return this.toolBarField; }
      set { this.toolBarField = value; }
    }

    public int top {
      get { return this.topField; }
      set { this.topField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string topUnit {
      get { return this.topUnitField; }
      set { this.topUnitField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string windowTitle {
      get { return this.windowTitleField; }
      set { this.windowTitleField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaInPageAsset : RichMediaFlashAsset {
    private RichMediaImageAsset alternateImageAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaImageAsset alternateImageAsset {
      get { return this.alternateImageAssetField; }
      set { this.alternateImageAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaFloatingAsset : RichMediaFlashAsset {
    private string zIndexField;

    private int durationField;

    private bool hideDropDownsField;

    private bool hideFlashObjectsField;

    private bool hideIframesField;

    private bool hideJavaAppletsField;

    private bool hideScrollBarsField;

    private int leftField;

    private bool leftLockPositionField;

    private string leftUnitField;

    private int startTimeField;

    private int topField;

    private bool topLockPositionField;

    private string topUnitField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string ZIndex {
      get { return this.zIndexField; }
      set { this.zIndexField = value; }
    }

    public int duration {
      get { return this.durationField; }
      set { this.durationField = value; }
    }

    public bool hideDropDowns {
      get { return this.hideDropDownsField; }
      set { this.hideDropDownsField = value; }
    }

    public bool hideFlashObjects {
      get { return this.hideFlashObjectsField; }
      set { this.hideFlashObjectsField = value; }
    }

    public bool hideIframes {
      get { return this.hideIframesField; }
      set { this.hideIframesField = value; }
    }

    public bool hideJavaApplets {
      get { return this.hideJavaAppletsField; }
      set { this.hideJavaAppletsField = value; }
    }

    public bool hideScrollBars {
      get { return this.hideScrollBarsField; }
      set { this.hideScrollBarsField = value; }
    }

    public int left {
      get { return this.leftField; }
      set { this.leftField = value; }
    }

    public bool leftLockPosition {
      get { return this.leftLockPositionField; }
      set { this.leftLockPositionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string leftUnit {
      get { return this.leftUnitField; }
      set { this.leftUnitField = value; }
    }

    public int startTime {
      get { return this.startTimeField; }
      set { this.startTimeField = value; }
    }

    public int top {
      get { return this.topField; }
      set { this.topField = value; }
    }

    public bool topLockPosition {
      get { return this.topLockPositionField; }
      set { this.topLockPositionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string topUnit {
      get { return this.topUnitField; }
      set { this.topUnitField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class TrackingTextCreative : CreativeBase {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(HTMLInterstitialCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(FlashInpageCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(HTMLCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TrackingHTMLCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(MobileDisplayCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class HTMLCreativeBase : CreativeBase {
    private string hTMLCodeField;

    private HTMLCreativeAsset[] creativeAssetsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string HTMLCode {
      get { return this.hTMLCodeField; }
      set { this.hTMLCodeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public HTMLCreativeAsset[] creativeAssets {
      get { return this.creativeAssetsField; }
      set { this.creativeAssetsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class HTMLInterstitialCreative : HTMLCreativeBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class FlashInpageCreative : HTMLCreativeBase {
    private FSCommand fSCommandField;

    private HTMLCreativeFlashAsset[] additionalFlashAssetsField;

    private HTMLCreativeAsset[] additionalImageAssetsField;

    private bool allowScriptAccessField;

    private string backgroundColorField;

    private string backupImageAlternateTextField;

    private HTMLCreativeAsset backupImageAssetField;

    private string backupImageClickThroughUrlField;

    private TargetWindow backupImageTargetWindowField;

    private FlashClickTag[] clickTagsField;

    private bool codeLockedField;

    private HTMLCreativeFlashAsset parentFlashAssetField;

    private string surveyUrlField;

    private string wmodeField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public FSCommand FSCommand {
      get { return this.fSCommandField; }
      set { this.fSCommandField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public HTMLCreativeFlashAsset[] additionalFlashAssets {
      get { return this.additionalFlashAssetsField; }
      set { this.additionalFlashAssetsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public HTMLCreativeAsset[] additionalImageAssets {
      get { return this.additionalImageAssetsField; }
      set { this.additionalImageAssetsField = value; }
    }

    public bool allowScriptAccess {
      get { return this.allowScriptAccessField; }
      set { this.allowScriptAccessField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string backgroundColor {
      get { return this.backgroundColorField; }
      set { this.backgroundColorField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string backupImageAlternateText {
      get { return this.backupImageAlternateTextField; }
      set { this.backupImageAlternateTextField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public HTMLCreativeAsset backupImageAsset {
      get { return this.backupImageAssetField; }
      set { this.backupImageAssetField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string backupImageClickThroughUrl {
      get { return this.backupImageClickThroughUrlField; }
      set { this.backupImageClickThroughUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public TargetWindow backupImageTargetWindow {
      get { return this.backupImageTargetWindowField; }
      set { this.backupImageTargetWindowField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public FlashClickTag[] clickTags {
      get { return this.clickTagsField; }
      set { this.clickTagsField = value; }
    }

    public bool codeLocked {
      get { return this.codeLockedField; }
      set { this.codeLockedField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public HTMLCreativeFlashAsset parentFlashAsset {
      get { return this.parentFlashAssetField; }
      set { this.parentFlashAssetField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string surveyUrl {
      get { return this.surveyUrlField; }
      set { this.surveyUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string wmode {
      get { return this.wmodeField; }
      set { this.wmodeField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class HTMLCreative : HTMLCreativeBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class TrackingHTMLCreative : HTMLCreativeBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class MobileDisplayCreative : HTMLCreativeBase {
    private string thirdPartyClickTrackingUrlField;

    private string thirdPartyImpressionTrackingUrlField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string thirdPartyClickTrackingUrl {
      get { return this.thirdPartyClickTrackingUrlField; }
      set { this.thirdPartyClickTrackingUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string thirdPartyImpressionTrackingUrl {
      get { return this.thirdPartyImpressionTrackingUrlField; }
      set { this.thirdPartyImpressionTrackingUrlField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(InternalRedirectCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RedirectCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(InterstitialInternalRedirectCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class RedirectCreativeBase : CreativeBase {
    private string redirectUrlField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string redirectUrl {
      get { return this.redirectUrlField; }
      set { this.redirectUrlField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class InternalRedirectCreative : RedirectCreativeBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RedirectCreative : RedirectCreativeBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class InterstitialInternalRedirectCreative : RedirectCreativeBase {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(ImageCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(TrackingImageCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class ImageCreativeBase : CreativeBase {
    private string alternateTextField;

    private string assetFilenameField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string alternateText {
      get { return this.alternateTextField; }
      set { this.alternateTextField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string assetFilename {
      get { return this.assetFilenameField; }
      set { this.assetFilenameField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class ImageCreative : ImageCreativeBase {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class TrackingImageCreative : ImageCreativeBase {
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaFloatingWithReminderCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaImageWithFloatingCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaInPageCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaInPageWithOverlayCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaExpandingCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaFloatingCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaOverlayCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaImageWithOverlayCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaInPageWithFloatingCreative))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(RichMediaFlashInFlashCreative))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public abstract partial class RichMediaCreativeBase : CreativeBase {
    private string actionScriptVersionField;

    private string adRequestKeysField;

    private string authoringApplicationField;

    private RichMediaAsset[] childAssetsField;

    private string commentsField;

    private RichMediaCounterEvent[] counterEventsField;

    private System.DateTime? createdDateField;

    private string[] creativeAttributesField;

    private string customKeyValuesField;

    private RichMediaExitEvent[] exitEventsField;

    private bool flashInFlashField;

    private bool interstitialField;

    private string metaDataField;

    private int placementHeightField;

    private int placementWidthField;

    private string requiredFlashPlayerVersionField;

    private string surveyUrlField;

    private string thirdPartyBackupImageImpressionUrlField;

    private string thirdPartyClickUrlField;

    private string thirdPartyFlashImpressionUrlField;

    private string thirdPartyImpressionUrlField;

    private RichMediaTimerEvent[] timerEventField;

    private int totalFileSizeField;

    private string typeField;

    private long videoLengthField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string actionScriptVersion {
      get { return this.actionScriptVersionField; }
      set { this.actionScriptVersionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string adRequestKeys {
      get { return this.adRequestKeysField; }
      set { this.adRequestKeysField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string authoringApplication {
      get { return this.authoringApplicationField; }
      set { this.authoringApplicationField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaAsset[] childAssets {
      get { return this.childAssetsField; }
      set { this.childAssetsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string comments {
      get { return this.commentsField; }
      set { this.commentsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaCounterEvent[] counterEvents {
      get { return this.counterEventsField; }
      set { this.counterEventsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public System.DateTime? createdDate {
      get { return this.createdDateField; }
      set { this.createdDateField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string[] creativeAttributes {
      get { return this.creativeAttributesField; }
      set { this.creativeAttributesField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string customKeyValues {
      get { return this.customKeyValuesField; }
      set { this.customKeyValuesField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaExitEvent[] exitEvents {
      get { return this.exitEventsField; }
      set { this.exitEventsField = value; }
    }

    public bool flashInFlash {
      get { return this.flashInFlashField; }
      set { this.flashInFlashField = value; }
    }

    public bool interstitial {
      get { return this.interstitialField; }
      set { this.interstitialField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string metaData {
      get { return this.metaDataField; }
      set { this.metaDataField = value; }
    }

    public int placementHeight {
      get { return this.placementHeightField; }
      set { this.placementHeightField = value; }
    }

    public int placementWidth {
      get { return this.placementWidthField; }
      set { this.placementWidthField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string requiredFlashPlayerVersion {
      get { return this.requiredFlashPlayerVersionField; }
      set { this.requiredFlashPlayerVersionField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string surveyUrl {
      get { return this.surveyUrlField; }
      set { this.surveyUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string thirdPartyBackupImageImpressionUrl {
      get { return this.thirdPartyBackupImageImpressionUrlField; }
      set { this.thirdPartyBackupImageImpressionUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string thirdPartyClickUrl {
      get { return this.thirdPartyClickUrlField; }
      set { this.thirdPartyClickUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string thirdPartyFlashImpressionUrl {
      get { return this.thirdPartyFlashImpressionUrlField; }
      set { this.thirdPartyFlashImpressionUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string thirdPartyImpressionUrl {
      get { return this.thirdPartyImpressionUrlField; }
      set { this.thirdPartyImpressionUrlField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaTimerEvent[] timerEvent {
      get { return this.timerEventField; }
      set { this.timerEventField = value; }
    }

    public int totalFileSize {
      get { return this.totalFileSizeField; }
      set { this.totalFileSizeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string type {
      get { return this.typeField; }
      set { this.typeField = value; }
    }

    public long videoLength {
      get { return this.videoLengthField; }
      set { this.videoLengthField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaFloatingWithReminderCreative : RichMediaCreativeBase {
    private RichMediaFloatingAsset floatingAssetField;

    private RichMediaReminderAsset reminderAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaFloatingAsset floatingAsset {
      get { return this.floatingAssetField; }
      set { this.floatingAssetField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaReminderAsset reminderAsset {
      get { return this.reminderAssetField; }
      set { this.reminderAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaImageWithFloatingCreative : RichMediaCreativeBase {
    private RichMediaFloatingAsset floatingAssetField;

    private RichMediaImageAsset imageAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaFloatingAsset floatingAsset {
      get { return this.floatingAssetField; }
      set { this.floatingAssetField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaImageAsset imageAsset {
      get { return this.imageAssetField; }
      set { this.imageAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaInPageCreative : RichMediaCreativeBase {
    private RichMediaInPageAsset inPageAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaInPageAsset inPageAsset {
      get { return this.inPageAssetField; }
      set { this.inPageAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaInPageWithOverlayCreative : RichMediaCreativeBase {
    private RichMediaInPageAsset inPageAssetField;

    private RichMediaOverlayAsset overlayAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaInPageAsset inPageAsset {
      get { return this.inPageAssetField; }
      set { this.inPageAssetField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaOverlayAsset overlayAsset {
      get { return this.overlayAssetField; }
      set { this.overlayAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaExpandingCreative : RichMediaCreativeBase {
    private RichMediaExpandingAsset expandingAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaExpandingAsset expandingAsset {
      get { return this.expandingAssetField; }
      set { this.expandingAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaFloatingCreative : RichMediaCreativeBase {
    private RichMediaFloatingAsset floatingAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaFloatingAsset floatingAsset {
      get { return this.floatingAssetField; }
      set { this.floatingAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaOverlayCreative : RichMediaCreativeBase {
    private RichMediaOverlayAsset overlayAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaOverlayAsset overlayAsset {
      get { return this.overlayAssetField; }
      set { this.overlayAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaImageWithOverlayCreative : RichMediaCreativeBase {
    private RichMediaImageAsset imageAssetField;

    private RichMediaOverlayAsset overlayAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaImageAsset imageAsset {
      get { return this.imageAssetField; }
      set { this.imageAssetField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaOverlayAsset overlayAsset {
      get { return this.overlayAssetField; }
      set { this.overlayAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaInPageWithFloatingCreative : RichMediaCreativeBase {
    private RichMediaFloatingAsset floatingAssetField;

    private RichMediaInPageAsset inPageAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaFloatingAsset floatingAsset {
      get { return this.floatingAssetField; }
      set { this.floatingAssetField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaInPageAsset inPageAsset {
      get { return this.inPageAssetField; }
      set { this.inPageAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class RichMediaFlashInFlashCreative : RichMediaCreativeBase {
    private string assetTypeField;

    private RichMediaFlashAsset flashAssetField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public string assetType {
      get { return this.assetTypeField; }
      set { this.assetTypeField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public RichMediaFlashAsset flashAsset {
      get { return this.flashAssetField; }
      set { this.flashAssetField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "creativefieldSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(PagedRecordSet))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class CreativeFieldRemoteService : DfaSoapClient {
    public CreativeFieldRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/creativefield";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeFieldSaveResult")]
    public CreativeFieldSaveResult saveCreativeField(CreativeField creativeField) {
      object[] results = this.Invoke("saveCreativeField", new object[] {creativeField});
      return ((CreativeFieldSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteCreativeField(long creativeFieldId) {
      this.Invoke("deleteCreativeField", new object[] {creativeFieldId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeFieldRecordSet")]
    public CreativeFieldRecordSet getCreativeFields(CreativeFieldSearchCriteria creativeFieldSearchCriteria) {
      object[] results = this.Invoke("getCreativeFields", new object[] {creativeFieldSearchCriteria});
      return ((CreativeFieldRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeFieldValueRecordSet")]
    public CreativeFieldValueRecordSet getCreativeFieldValues(CreativeFieldValueSearchCriteria creativeFieldValueSearchCriteria) {
      object[] results = this.Invoke("getCreativeFieldValues", new object[] {creativeFieldValueSearchCriteria});
      return ((CreativeFieldValueRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeField")]
    public CreativeField getCreativeField(long creativeFieldId) {
      object[] results = this.Invoke("getCreativeField", new object[] {creativeFieldId});
      return ((CreativeField) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeFieldValue")]
    public CreativeFieldValue getCreativeFieldValue(long creativeFieldValueId) {
      object[] results = this.Invoke("getCreativeFieldValue", new object[] {creativeFieldValueId});
      return ((CreativeFieldValue) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeFieldValueSaveResult")]
    public CreativeFieldValueSaveResult saveCreativeFieldValue(CreativeFieldValue creativeFieldValue) {
      object[] results = this.Invoke("saveCreativeFieldValue", new object[] {creativeFieldValue});
      return ((CreativeFieldValueSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteCreativeFieldValue(long creativeFieldValueId) {
      this.Invoke("deleteCreativeFieldValue", new object[] {creativeFieldValueId});
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeField : CreativeFieldBase {
    private long advertiserIdField;

    private long totalNumberOfValuesField;

    public long advertiserId {
      get { return this.advertiserIdField; }
      set { this.advertiserIdField = value; }
    }

    public long totalNumberOfValues {
      get { return this.totalNumberOfValuesField; }
      set { this.totalNumberOfValuesField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeField))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldValueRecordSet : PagedRecordSet {
    private CreativeFieldValue[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeFieldValue[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldValue : CreativeFieldValueBase {
    private long creativeFieldIdField;

    public long creativeFieldId {
      get { return this.creativeFieldIdField; }
      set { this.creativeFieldIdField = value; }
    }
  }

  [System.Xml.Serialization.SoapIncludeAttribute(typeof(CreativeFieldValue))]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldValueBase : Base {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldRecordSet : PagedRecordSet {
    private CreativeField[] recordsField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public CreativeField[] records {
      get { return this.recordsField; }
      set { this.recordsField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldValueSearchCriteria : PageableSearchCriteriaBase {
    private long[] creativeFieldIdsField;

    private SortOrder sortOrderField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] creativeFieldIds {
      get { return this.creativeFieldIdsField; }
      set { this.creativeFieldIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldSearchCriteria : PageableSearchCriteriaBase {
    private long[] advertiserIdsField;

    private SortOrder sortOrderField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] advertiserIds {
      get { return this.advertiserIdsField; }
      set { this.advertiserIdsField = value; }
    }

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public SortOrder sortOrder {
      get { return this.sortOrderField; }
      set { this.sortOrderField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldValueSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeFieldSaveResult : SaveResult {
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Web.Services.WebServiceBindingAttribute(Name = "creativegroupSoapBinding", Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCriteriaBase))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(SaveResult))]
  [System.Xml.Serialization.SoapIncludeAttribute(typeof(Base))]
  public partial class CreativeGroupRemoteService : DfaSoapClient {
    public CreativeGroupRemoteService() {
      this.Url = "http://advertisersapi.doubleclick.net/v1.14/api/dfa-api/creativegroup";
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeGroupSaveResult")]
    public CreativeGroupSaveResult saveCreativeGroup(CreativeGroup creativeGroup) {
      object[] results = this.Invoke("saveCreativeGroup", new object[] {creativeGroup});
      return ((CreativeGroupSaveResult) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    public void deleteCreativeGroup(long creativeGroupId) {
      this.Invoke("deleteCreativeGroup", new object[] {creativeGroupId});
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeGroupRecordSet")]
    public CreativeGroupRecordSet getCreativeGroups(CreativeGroupSearchCriteria searchCriteria) {
      object[] results = this.Invoke("getCreativeGroups", new object[] {searchCriteria});
      return ((CreativeGroupRecordSet) (results[0]));
    }

    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://www.doubleclick.net/dfa-api/v1.14", ResponseNamespace = "http://www.doubleclick.net/dfa-api/v1.14")]
    [return: System.Xml.Serialization.SoapElementAttribute("CreativeGroup")]
    public CreativeGroup getCreativeGroup(long id) {
      object[] results = this.Invoke("getCreativeGroup", new object[] {id});
      return ((CreativeGroup) (results[0]));
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeGroupSearchCriteria : SearchCriteriaBase {
    private long[] advertiserIdsField;

    private int groupNumberField;

    [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
    public long[] advertiserIds {
      get { return this.advertiserIdsField; }
      set { this.advertiserIdsField = value; }
    }

    public int groupNumber {
      get { return this.groupNumberField; }
      set { this.groupNumberField = value; }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://www.doubleclick.net/dfa-api/v1.14")]
  public partial class CreativeGroupSaveResult : SaveResult {
  }
}

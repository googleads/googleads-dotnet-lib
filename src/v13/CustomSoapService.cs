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
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Collections;
using System.Xml;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using com.google.api.adwords.lib;
using System.Web;

namespace com.google.api.adwords.v13 {
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  public class ApiService : SoapHttpClientProtocol {
    protected useragent useragent;
    protected password password;
    protected email email;
    protected clientEmail clientEmail;
    protected clientCustomerId clientCustomerId;
    protected developerToken developerToken;
    protected applicationToken applicationToken;
    protected responseTime responseTime;
    protected operations operations;
    protected units units;
    protected requestId requestId;
    private AdWordsUser parent = null;
    protected static Hashtable codeLookup = new Hashtable();

    delegate object[] CallAdWordsApi(string methodName, object[] parameters);

    static ApiService() {
      codeLookup.Add(
          new int[] {84, 85, 86, 119, 162, 163, 164, 165, 183},
          typeof(AccountException));
      codeLookup.Add(
          new int[] {50, 52, 53, 106, 107, 108, 109, 110, 114,
          118, 129, 130, 132}, typeof(BillingException));
      codeLookup.Add(
          new int[] {0, 18, 55, 60, 95, 98, 117, 143, 155, 166},
          typeof(GoogleInternalException));
      codeLookup.Add(
          new int[] {100, 101, 102, 103, 104, 105},
          typeof(WebPageException));
      codeLookup.Add(new int[] {116, 139}, typeof(SandboxException));
      codeLookup.Add(
          new int[] {1, 2, 3, 8, 41, 42, 73, 88, 89, 94, 115, 131, 184},
          typeof(InvalidRequestException));
      codeLookup.Add(
          new int[] {21, 120, 121},
          typeof(PolicyViolationException));
      codeLookup.Add(
          new int[] {25, 71, 76, 77, 78, 79, 90, 91, 92, 93, 128, 141,
          147, 170, 171, 172, 173, 174, 176, 177, 188, 189, 207},
          typeof(InvalidOperationException));
      codeLookup.Add(
          new int[] {6, 7, 9, 10, 12, 13, 14, 15, 16, 19, 20, 22, 23,
          24, 26, 27, 28, 29, 30, 31, 33, 34, 35, 36, 37, 38, 39, 44, 45, 46,
          47, 48, 49, 51, 54, 57, 59, 70, 72, 74, 75, 80, 82, 83, 97, 99, 111,
          122, 123, 124, 125, 127, 133, 137, 140, 142, 144, 145, 146, 149, 153,
          156, 157, 158, 186, 190, 206},
          typeof(InvalidParameterException));
      codeLookup.Add(
          new int[] {4, 5, 61, 62, 63, 138}, typeof(PermissionException));
      codeLookup.Add(
          new int[] {58, 87}, typeof(ConcurrencyException));
      codeLookup.Add(
          new int[] {17, 32, 40, 43, 81, 96, 112, 134},
          typeof(ExceededLimitsException));
    }

    public AdWordsUser Parent {
      get {return parent;}
      set {parent = value;}
    }

    public useragent useragentValue {
      get {return useragent;}
      set {useragent = value;}
    }

    public password passwordValue {
      get {return password;}
      set {password = value;}
    }

    public email emailValue {
      get {return email;}
      set {email = value;}
    }

    public clientEmail clientEmailValue {
      get {return clientEmail;}
      set {clientEmail = value;}
    }

    public clientCustomerId clientCustomerIdValue {
      get {return clientCustomerId;}
      set {clientCustomerId = value;}
    }

    public developerToken developerTokenValue {
      get {return developerToken;}
      set {developerToken = value;}
    }

    public applicationToken applicationTokenValue {
      get {return applicationToken;}
      set {applicationToken = value;}
    }

    public responseTime responseTimeValue {
      get {return responseTime;}
      set {responseTime = value;}
    }

    public operations operationsValue {
      get {return operations;}
      set {operations = value;}
    }

    public units unitsValue {
      get {return units;}
      set {units = value;}
    }

    public requestId requestIdValue {
      get {return requestId;}
      set {requestId = value;}
    }

    protected new object[] Invoke(string methodName, object[] parameters) {
      return MakeApiCall(methodName, parameters);
    }

    private object[] MakeApiCall(string methodName, object[] parameters) {
      try {
        if (HttpContext.Current != null) {
          HttpContext.Current.Items.Add("AdWordsParent", this.Parent);
        } else {
          CallContext.SetData("AdWordsParent", this.Parent);
        }
        return base.Invoke(methodName, parameters);
      } catch (SoapException ex) {
        throw GetCustomException(ex);
      } finally {
        if (HttpContext.Current != null) {
          HttpContext.Current.Items.Remove("AdWordsParent");
        } else {
          CallContext.FreeNamedDataSlot("AdWordsParent");
        }
      }
    }

    protected new IAsyncResult BeginInvoke(string methodName, object[] parameters,
        AsyncCallback callback, object asyncState) {
      CallAdWordsApi apiFunction = new CallAdWordsApi(MakeApiCall);
      return apiFunction.BeginInvoke(methodName, parameters, callback, apiFunction);
    }

    protected new object[] EndInvoke(IAsyncResult asyncResult) {
      CallAdWordsApi apiFunction = (CallAdWordsApi)asyncResult.AsyncState;
      return apiFunction.EndInvoke(asyncResult);
    }

    protected Exception GetCustomException(SoapException ex) {
      XmlNode detailsNode = ex.Detail;
      XmlNamespaceManager nsmgr = new XmlNamespaceManager(
          detailsNode.OwnerDocument.NameTable);
      nsmgr.AddNamespace("ns1", "https://adwords.google.com/api/adwords/v13");
      int code = int.Parse(detailsNode.SelectSingleNode(
          "ns1:fault/ns1:code", nsmgr).InnerText);
      string message = detailsNode.SelectSingleNode(
          "ns1:fault/ns1:message", nsmgr).InnerText;

      foreach (int[] key in codeLookup.Keys) {
        foreach (int targetCode in key) {
          if (code == targetCode) {
            Type targetType = (Type)codeLookup[key];
            ConstructorInfo ci = targetType.GetConstructor(new Type[]
                {typeof(int), typeof(string), typeof(Exception)});
            if (ci != null) {
              return (ApiException) ci.Invoke(new object[] {code, message, ex});
            }
          }
        }
      }
      return new ApiException(-1, "Unknown exception encountered", ex);
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class units : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class responseTime : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class clientEmail : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class applicationToken : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class clientCustomerId : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class developerToken : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class operations : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class requestId : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class email : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class password : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  [XmlRootAttribute(Namespace = "https://adwords.google.com/api/adwords/v13",
   IsNullable = false)]
  public class useragent : SoapHeader {
    private string[] Text;

    /// <remarks/>
    [XmlTextAttribute()]
    public string[] Value {
      get {return Text;}
      set {Text = value;}
    }
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  public class StatsRecord {

    /// <remarks/>
    public System.Double averagePosition;

    /// <remarks/>
    public long clicks;

    /// <remarks/>
    public System.Double conversionRate;

    /// <remarks/>
    public long conversions;

    /// <remarks/>
    public long cost;

    /// <remarks/>
    public long id;

    /// <remarks/>
    public long impressions;
  }

  /// <remarks/>
  [XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/v13")]
  public enum AdGroupStatus {

    /// <remarks/>
    Enabled,

    /// <remarks/>
    Paused,

    /// <remarks/>
    Deleted,
  }
}

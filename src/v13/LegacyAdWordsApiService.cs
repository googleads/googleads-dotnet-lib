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

using com.google.api.adwords.lib;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace com.google.api.adwords.v13 {
  /// <summary>
  /// Base class for AdWords API v13.
  /// </summary>
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  public class LegacyAdWordsApiService : SoapServiceBase {
    /// <summary>
    /// An internal lookup map for storing error codes.
    /// </summary>
    protected static Hashtable codeLookup = new Hashtable();

    /// <summary>
    /// Static constructor.
    /// </summary>
    static LegacyAdWordsApiService() {
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

    /// <summary>
    /// Convert the AdWords API error code number into a custom exception object.
    /// </summary>
    /// <param name="ex">The original exception thrown by SOAP Service.</param>
    /// <returns>A new exception object that wraps the SOAP exception.</returns>
    protected override Exception GetCustomException(SoapException ex) {
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
              return (LegacyAdWordsApiException) ci.Invoke(new object[] {code, message, ex});
            }
          }
        }
      }
      return new LegacyAdWordsApiException(-1, "Unknown exception encountered", ex);
    }
  }
}

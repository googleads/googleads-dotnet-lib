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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Google.Api.Ads.AdWords.Util.ApiCodes {
  /// <summary>
  /// Utility functions to retrive the list of various codes used by
  /// AdWords API.
  /// </summary>
  public static class CodeUtilities {
    /// <summary>
    /// Gets the list of all methods available in AdWords API.
    /// </summary>
    /// <returns>A list of OpRates objects.</returns>
    public static List<ApiMethod> GetAllMethods() {
      List<ApiMethod> retVal = new List<ApiMethod>();

      Type[] childTypes = typeof(AdWordsService).GetNestedTypes();

      foreach (Type childType in childTypes) {
        FieldInfo[] fieldInfos = childType.GetFields(BindingFlags.Static | BindingFlags.Public);
        foreach (FieldInfo fieldInfo in fieldInfos) {
          if (fieldInfo.FieldType != typeof(ServiceSignature)) {
            continue;
          }
          ServiceSignature signature = (ServiceSignature) fieldInfo.GetValue(null);
          string version = "";
          if (signature is AdWordsServiceSignature) {
            version = (signature as AdWordsServiceSignature).Version;
          } else if (signature is LegacyAdwordsServiceSignature) {
            version = (signature as LegacyAdwordsServiceSignature).Version;
          }

          if (string.IsNullOrEmpty(version)) {
            continue;
          }

          Type serviceType = Type.GetType("Google.Api.Ads.AdWords." + version + "." +
              signature.ServiceName);
          MethodInfo[] methodInfos = serviceType.GetMethods(BindingFlags.Public |
              BindingFlags.Instance | BindingFlags.DeclaredOnly);
          foreach (MethodInfo methodInfo in methodInfos) {
            if (!methodInfo.IsSpecialName) {
              ApiMethod method = new ApiMethod();
              method.Version = version;
              method.ServiceName = signature.ServiceName;
              method.MethodName = methodInfo.Name;
              retVal.Add(method);
            }
          }
        }
      }
      return retVal;
    }
  }
}

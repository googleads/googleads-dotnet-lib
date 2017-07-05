// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Google.Api.Ads.Common.Tests {

  /// <summary>
  /// Utility class that provide functionality to test the integrity of
  /// generated stub code for various API services.
  /// </summary>
  public class StubIntegrityTestHelper {

    /// <summary>
    /// The callback to be called when the <see cref="EnumerateServices"/> method
    /// finds a matching service signature.
    /// </summary>
    /// <param name="signature">The signature.</param>
    public delegate void EnumerateServiceCallback(ServiceSignature signature);

    /// <summary>
    /// The callback to be called when the <see cref="EnumerateEnumFields"/> method
    /// finds a matching service signature.
    /// </summary>
    /// <param name="hashedFieldName">The field name in a hashed format
    /// (typename_fieldname).</param>
    /// <param name="enumValue">The enum value.</param>
    public delegate void EnumerateEnumFieldsCallback(string hashedFieldName, int enumValue);

    /// <summary>
    /// Enumerates the services supported in a Ads Service.
    /// </summary>
    /// <typeparam name="T">The Ads service being tested
    /// (e.g. <see cref="AdWordsService"/>)</typeparam>
    /// <param name="onServiceFound">The callback to be called when a matching
    /// service is found.</param>
    public static void EnumerateServices<T>(EnumerateServiceCallback onServiceFound)
        where T : AdsService {
      Type serviceType = typeof(T);

      // All the supported service types are defined as nested classes within the
      // service type. Each of the nested type has multiple ServiceSignature entries,
      // one per supported service. This allows user to create a service like:
      //
      // var campaignService = user.GetService(AdWordsService.v201607.CampaignService);
      Type[] versionTypes = serviceType.GetNestedTypes();

      foreach (Type versionType in versionTypes) {
        FieldInfo[] serviceFields = versionType.GetFields
            (BindingFlags.Static | BindingFlags.Public);
        foreach (FieldInfo fieldInfo in serviceFields) {
          if (fieldInfo.FieldType == typeof(ServiceSignature)) {
            ServiceSignature value = (ServiceSignature) fieldInfo.GetValue(null);
            onServiceFound(value);
          }
        }
      }
    }

    /// <summary>
    /// Enumerates the enum fields and values under a given root namespace.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rootNameSpace">The root name space.</param>
    /// <param name="onEnumFieldDeclarationFound">The callback to be called when
    /// a matching enumeration field is found.</param>
    public static void EnumerateEnumFields<T>(string rootNameSpace,
        EnumerateEnumFieldsCallback onEnumFieldDeclarationFound) where T : AdsService {
      // Find all supported versions.
      Type serviceType = typeof(T);
      Type[] versionTypes = serviceType.GetNestedTypes();

      ISet<string> rootEnums = new HashSet<string>();

      // Find out the namespace for each supported API version.
      foreach (Type versionType in versionTypes) {
        string typeName = versionType.Name;
        string namespaceName = rootNameSpace + typeName;
        rootEnums.Add(namespaceName);
      }

      // Enumerate all types in the client library.
      foreach (Type declaredType in serviceType.Assembly.GetTypes()) {
        // If this is an enumeration within a namespace that corresponds to a
        // supported API version then process further.
        if (rootEnums.Contains(declaredType.Namespace) && declaredType.IsEnum) {
          Array values = declaredType.GetEnumValues();

          foreach (object value in values) {
            string enumName = declaredType.GetEnumName(value);

            // Generate a unique name as EnumTypeName.EnumFieldName.
            string hashedName = declaredType.Name + "." + enumName;
            int enumValue = (int) value;

            onEnumFieldDeclarationFound(hashedName, enumValue);
          }
        }
      }
    }
  }
}
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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.AdWords.Lib;

using NUnit.Framework;

using System;
using System.Reflection;

namespace Google.Api.Ads.AdWords.Tests {

  /// <summary>
  /// UnitTests for service creation.
  /// </summary>
  [TestFixture]
  public class ServiceCreationTests : ExampleTestsBase {

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ServiceCreationTests()
      : base() {
    }

    /// <summary>
    /// Test whether we can create all the services without any exceptions.
    /// </summary>
    [Test]
    public void TestCreateServices() {
      Type adwordsServiceType = typeof(AdWordsService);

      Type[] versionTypes = adwordsServiceType.GetNestedTypes();

      foreach (Type versionType in versionTypes) {
        FieldInfo[] serviceFields = versionType.GetFields
            (BindingFlags.Static | BindingFlags.Public);
        foreach (FieldInfo fieldInfo in serviceFields) {
          if (fieldInfo.FieldType == typeof(ServiceSignature)) {
            ServiceSignature value = (ServiceSignature) fieldInfo.GetValue(null);
            Assert.DoesNotThrow(delegate() {
              AdsClient service = user.GetService(value);
            });
          }
        }
      }
    }
  }
}
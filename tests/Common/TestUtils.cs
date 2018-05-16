// Copyright 2013, Google Inc. All Rights Reserved.
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

using NUnit.Framework;

using System;
using System.Reflection;

namespace Google.Api.Ads.Common.Tests {
  /// <summary>
  /// Support utility methods for running test cases.
  /// </summary>
  public class TestUtils {
    /// <summary>
    /// Validates whether an ArgumentNullException is called whenever a required
    /// property in targetObject is null, and testDelegate is invoked.
    /// </summary>
    /// <param name="targetObject">The target object.</param>
    /// <param name="propertyNames">The property names to be checked for null
    /// values.</param>
    /// <param name="testDelegate">The test delegate.</param>
    public static void ValidateRequiredParameters(object targetObject, string[] propertyNames,
        TestDelegate testDelegate) {
      foreach (string propertyName in propertyNames) {
        PropertyInfo propInfo = targetObject.GetType().GetProperty(propertyName);
        object oldValue = propInfo.GetValue(targetObject, null);
        if (propInfo.CanWrite) {
          propInfo.SetValue(targetObject, null, null);
          Assert.Throws<ArgumentNullException>(testDelegate);
          propInfo.SetValue(targetObject, oldValue, null);
        }
      }
    }
  }
}

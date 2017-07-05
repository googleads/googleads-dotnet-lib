// Copyright 2017, Google Inc. All Rights Reserved.
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

using System.Reflection;

namespace Google.Api.Ads.AdWords.v201702 {

  /// <summary>
  /// The API error base class that provides details about an error that
  /// occurred while processing a service request. <p>The OGNL field path is
  /// provided for parsers to identify the request data element that may have
  /// caused the error.</p>
  /// </summary>
  public partial class ApiError {

    /// <summary>
    /// Gets the index of the operation.
    /// </summary>
    /// <returns>Index of the operation that caused this error, or -1 if
    /// the index cannot be determined.</returns>
    public int GetOperationIndex() {
      FieldPathElement[] fieldPathElements = this.fieldPathElements;
      
      if (fieldPathElements == null || fieldPathElements.Length == 0) {
        return -1;
      } 
      
      FieldPathElement firstFieldPathElement = fieldPathElements[0];

      if (string.Compare(firstFieldPathElement.field, "operations") != 0) {
        return -1;
      }

      if (!firstFieldPathElement.indexSpecified) {
             return -1;
      }
      return firstFieldPathElement.index;
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      PropertyInfo propertyInfo = this.GetType().GetProperty("reason");
      if (propertyInfo != null) {
        string propertyName = propertyInfo.PropertyType.Name;
        string propertyValue = propertyInfo.GetValue(this, null).ToString();
        string key = propertyName + "." + propertyValue;
        string description = ErrorDescriptions.Lookup(key);
        return string.Format("{0}. (Error: {1}.{2}, FieldPath: {3}, Trigger: {4})", description,
            this.GetType().Name, propertyValue, this.fieldPath, this.trigger);
      }
      return base.ToString();
    }
  }
}
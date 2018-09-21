// Copyright 2018 Google LLC
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

namespace Google.Api.Ads.AdWords.v201806
{
    /// <summary>
    /// The API error base class that provides details about an error that
    /// occurred while processing a service request. <p>The OGNL field path is
    /// provided for parsers to identify the request data element that may have
    /// caused the error.</p>
    /// </summary>
    public partial class ApiError
    {
        /// <summary>
        /// Gets the index of the operation.
        /// </summary>
        /// <returns>Index of the operation that caused this error, or -1 if
        /// the index cannot be determined.</returns>
        public int GetOperationIndex()
        {
            return GetFieldPathIndex("operations");
        }

        /// <summary>
        /// Gets the index of a field in a field path expression.
        /// </summary>
        /// <param name="field">Name of the field to search for.</param>
        /// <returns>Index of the field name in the field path, or -1 if
        /// the index cannot be determined.</returns>
        public int GetFieldPathIndex(string field)
        {
            if (this.fieldPathElements != null)
            {
                foreach (FieldPathElement element in this.fieldPathElements)
                {
                    if (string.Compare(element.field, field) == 0 && element.indexSpecified)
                    {
                        return element.index;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            PropertyInfo propertyInfo = this.GetType().GetProperty("reason");
            if (propertyInfo != null)
            {
                string propertyName = propertyInfo.PropertyType.Name;
                string propertyValue = propertyInfo.GetValue(this, null).ToString();
                string key = propertyName + "." + propertyValue;
                string description = ErrorDescriptions.Lookup(key);
                return string.Format("{0}. (Error: {1}.{2}, FieldPath: {3}, Trigger: {4})",
                    description, this.GetType().Name, propertyValue, this.fieldPath, this.trigger);
            }

            return base.ToString();
        }
    }
}

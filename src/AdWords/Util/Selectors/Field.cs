// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Logging;

using System;
using System.Linq;

namespace Google.Api.Ads.AdWords.Util.Selectors
{
    /// <summary>
    /// Represents a Selector field.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// The registry for saving feature usage information..
        /// </summary>
        private static readonly AdsFeatureUsageRegistry featureUsageRegistry =
            AdsFeatureUsageRegistry.Instance;

        /// <summary>
        /// The feature ID for this class.
        /// </summary>
        private const AdsFeatureUsageRegistry.Features FEATURE_ID =
            AdsFeatureUsageRegistry.Features.SelectorField;

        /// <summary>
        /// True, if this field can be filtered on, false otherwise.
        /// </summary>
        private bool isFilterable;

        /// <summary>
        /// True, if this field can be selected, false otherwise.
        /// </summary>
        private bool isSelectable;

        /// <summary>
        /// The name to be used when selecting this field.
        /// </summary>
        private string fieldName;

        /// <summary>
        /// Gets a value indicating whether this field can be filtered on.
        /// </summary>
        public bool IsFilterable
        {
            get { return isFilterable; }
        }

        /// <summary>
        /// Gets a value indicating whether this field is selectable.
        /// </summary>
        public bool IsSelectable
        {
            get { return isSelectable; }
        }

        /// <summary>
        /// Gets the name of the field to be used when selecting this field.
        /// </summary>
        public string FieldName
        {
            get { return fieldName; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="isFilterable">True, if the field can be filtered on, false
        /// otherwise.</param>
        /// <param name="isSelectable">True, if this field can be selected, false
        /// otherwise.</param>
        public Field(string fieldName, bool isFilterable, bool isSelectable)
        {
            this.fieldName = fieldName;
            this.isFilterable = isFilterable;
            this.isSelectable = isSelectable;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            return this.fieldName;
        }

        /// <summary>
        /// Implicitly converts <paramref name="x"/> to a string when assigned to
        /// another string variable.
        /// </summary>
        /// <param name="x">The field to be converted to string.</param>
        /// <returns>A stringified representation of x.</returns>
        public static implicit operator string(Field x)
        {
            // Mark the usage.
            featureUsageRegistry.MarkUsage(FEATURE_ID);

            return x.ToString();
        }
    }

    /// <summary>
    /// Extension methods for <see cref="Field"/> class.
    /// </summary>
    public static class FieldExtensions
    {
        /// <summary>
        /// Returns the names of the fields as an array of strings.
        /// </summary>
        /// <param name="fields">The array of fields to convert.</param>
        /// <returns>The names of fields as a string array.</returns>
        public static string[] Names(this Field[] fields)
        {
            return Array.ConvertAll(fields, delegate(Field field) { return field.ToString(); });
        }
    }
}

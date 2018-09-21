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

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// Marker interface for ReportDefinitionField.
    /// </summary>
    public interface IReportDefinitionField
    {
        /// <summary>The field name. <span class="constraint ReadOnly">This field is read only and
        /// will be ignored when sent to the API.</span>
        /// </summary>
        string fieldName { get; set; }

        /// <summary>The XML attribute in the downloaded report. <span class="constraint
        /// ReadOnly">This field is read only and will be ignored when sent to the
        /// API.</span>
        /// </summary>
        string xmlAttributeName { get; set; }

        /// <summary>The type of field. Useful for knowing what operation type to pass in for a
        /// given field in a predicate. <span class="constraint ReadOnly">This field is read only
        /// and will be ignored when sent to the API.</span>
        /// </summary>
        string fieldType { get; set; }

        /// <summary>List of enum values for the corresponding field if and only if the field is an
        /// enum type. <span class="constraint ReadOnly">This field is read only and will be
        /// ignored when sent to the API.</span>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("enumValues")]
        string[] enumValues { get; set; }
    }
}

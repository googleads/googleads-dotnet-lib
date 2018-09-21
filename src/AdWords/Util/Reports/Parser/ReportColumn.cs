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

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// A type used for annotating properties that will hold the values
    /// of a row of a report. The names of the properties that are annotated
    /// with ReportColumn must match the name of the columns in the AdWords
    /// report, and the type of the property must be compatible with the data
    /// in that column.
    /// </summary>
    public class ReportColumn : System.Attribute
    {
        /// <summary>
        /// An optional column name.
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// The constructor for providing no column name.
        /// </summary>
        public ReportColumn()
        {
            this.ColumnName = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportColumn" /> class.
        /// </summary>
        /// <param name="colName">The column name</param>
        public ReportColumn(string colName)
        {
            this.ColumnName = colName;
        }
    }
}

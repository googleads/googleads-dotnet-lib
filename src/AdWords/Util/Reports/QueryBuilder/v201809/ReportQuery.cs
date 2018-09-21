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

namespace Google.Api.Ads.AdWords.Util.Reports.v201809
{
    /// <summary>
    /// A reporting query, returned by <see cref="ReportQueryBuilder"/>
    /// </summary>
    public class ReportQuery : IReportQuery<ReportQuery, ReportQueryBuilder>
    {
        /// <summary>
        /// The builder associated with this report query.
        /// </summary>
        private ReportQueryBuilder builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectQuery"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public ReportQuery(ReportQueryBuilder builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Gets the builder.
        /// </summary>
        public ReportQueryBuilder Builder
        {
            get { return builder; }
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return builder.ReportDefinition.ToQuery();
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="ReportQuery"/> to <see cref="string"/>.
        /// </summary>
        /// <param name="x">The <see cref="ReportQuery"/> to convert.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(ReportQuery x)
        {
            return x.ToString();
        }
    }
}

// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.v201806;

using System;
using System.Globalization;

namespace Google.Api.Ads.AdWords.Util.Reports.v201806
{
    /// <summary>
    /// Class for building report queries.
    /// </summary>
    public class ReportQueryBuilder : IReportQueryBuilder<ReportQueryBuilder, ReportQuery,
        ReportDefinitionReportType, ReportDefinitionDateRangeType>
    {
        /// <summary>
        /// The report definition instance for storing internal state.
        /// </summary>
        private ReportDefinition reportDefinition = new ReportDefinition()
        {
            selector = new Selector()
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportQueryBuilder"/> class.
        /// </summary>
        public ReportQueryBuilder()
        {
        }

        internal ReportDefinition ReportDefinition
        {
            get { return reportDefinition; }
        }

        /// <summary>
        /// Checks if the date format is in yyyyMMdd format.
        /// </summary>
        /// <param name="dateText">The date text.</param>
        /// <returns>True, if the date can be parsed, false otherwise.</returns>
        private bool IsDateFormatCorrect(string dateText)
        {
            DateTime temp;
            return DateTime.TryParseExact(dateText, "yyyyMMdd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out temp);
        }

        /// <summary>
        /// Adds a SELECT clause to the query.
        /// </summary>
        /// <param name="fields">The fields to be selected.</param>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        public ReportQueryBuilder Select(params string[] fields)
        {
            // Order matters, duplicate elements are allowed.
            reportDefinition.selector.fields = fields;
            return this;
        }

        /// <summary>
        /// Adds a FROM clause to the query.
        /// </summary>
        /// <param name="reportType">Type of the report.</param>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        public ReportQueryBuilder From(ReportDefinitionReportType reportType)
        {
            reportDefinition.reportType = reportType;
            return this;
        }

        /// <summary>
        /// Adds a FROM clause to the query.
        /// </summary>
        /// <param name="reportType">Type of the report.</param>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportQueryBuilder From(string reportType)
        {
            ReportDefinitionReportType result;
            if (!Enum.TryParse(reportType, out result))
            {
                throw new ArgumentException(string.Format("Unsupported report type - {0}.",
                    reportType));
            }

            reportDefinition.reportType = result;
            return this;
        }

        /// <summary>
        /// Adds a WHERE clause to the query.
        /// </summary>
        /// <param name="fieldName">Name of the field to filter on.</param>
        /// <returns>
        /// A builder for building the WHERE clause.
        /// </returns>
        public IWhereBuilder<ReportQueryBuilder> Where(string fieldName)
        {
            Predicate predicate = new Predicate()
            {
                field = fieldName
            };
            reportDefinition.selector.AddPredicate(predicate);
            return new WhereBuilder<ReportQueryBuilder>(predicate, this);
        }

        /// <summary>
        /// Adds a DURING clause to the query.
        /// </summary>
        /// <param name="dateRange">The predefined date range.</param>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if the dateRange cannot be parsed into a
        /// <see cref="ReportDefinitionDateRangeType"/> object.</exception>
        public ReportQueryBuilder During(string dateRange)
        {
            ReportDefinitionDateRangeType result;
            if (!Enum.TryParse(dateRange, out result))
            {
                throw new ArgumentException(string.Format("Unsupported date range type - {0}.",
                    dateRange));
            }

            reportDefinition.dateRangeType = result;
            return this;
        }

        /// <summary>
        /// Adds a DURING clause to the query.
        /// </summary>
        /// <param name="dateRangeType">Type of the date range.</param>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        public ReportQueryBuilder During(ReportDefinitionDateRangeType dateRangeType)
        {
            reportDefinition.dateRangeType = dateRangeType;
            return this;
        }

        /// <summary>
        /// Adds a DURING clause to the query.
        /// </summary>
        /// <param name="minDate">The minimum date in yyyyMMdd format.</param>
        /// <param name="maxDate">The maximum date in yyyyMMdd format.</param>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if the date is not in yyyyMMdd format.
        /// </exception>
        public ReportQueryBuilder During(string minDate, string maxDate)
        {
            if (!IsDateFormatCorrect(minDate))
            {
                throw new ArgumentException(string.Format("Unsupported date format for min - {0}.",
                    minDate));
            }

            if (!IsDateFormatCorrect(maxDate))
            {
                throw new ArgumentException(string.Format("Unsupported date format for max - {0}.",
                    maxDate));
            }

            reportDefinition.dateRangeType = ReportDefinitionDateRangeType.CUSTOM_DATE;
            reportDefinition.selector.dateRange = new DateRange()
            {
                max = maxDate,
                min = minDate
            };
            return this;
        }

        /// <summary>
        /// Adds a DURING clause to the query.
        /// </summary>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        public ReportQueryBuilder During(DateTime minDate, DateTime maxDate)
        {
            return During(minDate.ToString("yyyyMMdd"), maxDate.ToString("yyyyMMdd"));
        }

        /// <summary>
        /// Builds the query.
        /// </summary>
        /// <returns>
        /// The query.
        /// </returns>
        public ReportQuery Build()
        {
            return new ReportQuery(this);
        }
    }
}

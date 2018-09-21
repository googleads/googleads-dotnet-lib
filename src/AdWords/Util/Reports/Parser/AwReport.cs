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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// A class for parsing text AdWords reports into POCOs.
    /// </summary>
    /// <typeparam name="A">A type that holds data for one row of an AdWords report
    /// where the properties have names matching those of columns in the report and
    /// are annotated with [ReportColumn].</typeparam>
    public class AwReport<A> : IEnumerator<A>, IDisposable where A : new()
    {
        /// <summary>
        /// A name given to this report given in the constructor.
        /// </summary>
        public string ReportName { get; private set; }

        /// <summary>
        /// A given action that is used when there is an error in parsing
        /// an attribute from the report.
        /// </summary>
        public Action<ColumnValuePair, A> OnError { get; private set; }

        /// <summary>
        /// Returns a read only collection of POCOs of the rows of the report.
        /// Calling this forces this AwReport to walk through the entire
        /// text reader.
        /// </summary>
        /// <returns>A read only collection of all the rows of the report.</returns>
        public IEnumerable<A> Rows
        {
            get { return GetRows(); }
        }

        /// <summary>
        /// Holds the list of report rows as they're retrieved.
        /// </summary>
        private readonly List<A> rows = new List<A>();

        /// <summary>
        /// The reader that is the source of the report data.
        /// </summary>
        private readonly InputTextReader reader;

        /// <summary>
        /// The most recent row that has been parsed.
        /// </summary>
        private A current;

        /// <summary>
        /// Indicates if the most recent element from the InputTextReader has been parsed.
        /// </summary>
        private bool currentParsed = false;

        /// <summary>
        /// Indicates if the InputTextReader has been disposed.
        /// </summary>
        private bool streamClosed = false;

        /// <summary>
        /// The column names from the POCO type A. Includes properties annotated with [ReportColumn]
        /// </summary>
        private readonly HashSet<string> colNames = new HashSet<string>();

        /// <summary>
        /// Gets the most recently parsed row of the report.
        /// If we have walked through the entire report, then this will be the final row.
        /// </summary>
        /// <returns>A row of the report.</returns>
        public A Current
        {
            get
            {
                if (currentParsed)
                {
                    return current;
                }

                current = ParseCurrentRow();
                rows.Add(current);
                currentParsed = true;
                return current;
            }
        }

        /// <summary>
        /// This is the nongeneric version of the get property for obtaining the current row of the 
        /// report
        /// </summary>
        /// <returns>A row of the report.</returns>
        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="reader">Provides the text of the report.</param>
        /// <param name="reportName">An arbitrary name given to this report.</param>
        /// <param name="onError">A callback action that is used when there is
        /// an error in parsing an attribute from the report.</param>
        public AwReport(InputTextReader reader, string reportName,
            Action<ColumnValuePair, A> onError)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("The input text reader cannot be null.");
            }

            ReportParserUtils.ValidatePocoType<A>();
            this.ReportName = reportName;
            this.reader = reader;
            this.OnError = onError;
            ReportParserUtils.GetColumnNamesFromPocoType<A>().ToList()
                .ForEach(name => colNames.Add(name));
        }

        /// <summary>
        /// Constructor that provides the default throw-exception callback action upon
        /// parse error.
        /// </summary>
        /// <param name="reader">Provides the text of the report.</param>
        /// <param name="reportName">An arbitrary name given to this report.</param>
        public AwReport(InputTextReader reader, string reportName) : this(reader, reportName,
            (colVal, record) =>
            {
                throw new InvalidOperationException("Could not set the member " + colVal.ColName +
                    " with value " + colVal.Value);
            })
        {
        }

        /// <summary>
        /// Returns a read only collection of POCOs of the rows of the report.
        /// Calling this forces this <see ref="AwReport" /> to walk through the entire
        /// text reader.
        /// </summary>
        /// <returns>A read only collection of all the rows of the report.</returns>
        public IEnumerable<A> GetRows()
        {
            while (MoveNext())
            {
                var unused = Current;
            }

            return rows.AsReadOnly();
        }

        /// <summary>
        /// Parses the current row from the input reader.
        /// </summary>
        /// <returns>The row that was just parsed</returns>
        /// <exception cref="AdWordsReportsException">
        /// Throws an error if the current row could not be parsed because the reader has ended
        /// or there is invalid input.
        /// </exception>
        private A ParseCurrentRow()
        {
            var record = new A();
            var foundColumns = new HashSet<string>();
            IEnumerable<ColumnValuePair> rowData = reader.GetAttributes();

            foreach (ColumnValuePair attribute in rowData)
            {
                if (colNames.Contains(attribute.ColName))
                {
                    foundColumns.Add(attribute.ColName);
                    ReportParserUtils.SetColumnValue<A>(attribute, record, OnError);
                }
            }

            if (foundColumns.Count == 0 && colNames.Count != 0)
            {
                throw new AdWordsReportsException("None of the required columns " +
                    "were found. The stream might not have any of the required columns " +
                    "or moveNextRow() was not called before requesting the first row.");
            }

            return record;
        }

        /// <summary>
        /// Advances the text reader forward one row. This does not parse the new row into
        /// the POCO type. Once reaching the end of the text reader this will return false.
        /// </summary>
        /// <returns>
        /// True if the reader was advanced to the next row. 
        /// False if end was reached.
        /// </returns>
        public bool MoveNext()
        {
            if (streamClosed || !reader.Read())
            {
                if (!streamClosed)
                {
                    Dispose();
                }

                return false;
            }

            currentParsed = false;
            return true;
        }

        /// <summary>
        /// Disposes the underlying reader and prevents moving to the next row.
        /// </summary>
        public void Dispose()
        {
            reader.Dispose();
            streamClosed = true;
        }

        /// <summary>
        /// Throws an not supported exception because the <see ref="AwReport" /> does not support
        /// being reset.
        /// </summary>
        public void Reset()
        {
            throw new NotSupportedException("An Aw Report cannot be reset.");
        }
    }
}

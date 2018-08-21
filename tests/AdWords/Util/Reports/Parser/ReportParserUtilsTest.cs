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

using Google.Api.Ads.AdWords.Util.Reports;

using NUnit.Framework;

using System;
using System.Linq;

namespace Google.Api.Ads.AdWords.Tests.Util.Reports.Parser
{
    /// <summary>
    /// Unit tests for <see cref="ReportParserUtils"/> class.
    /// </summary>
    internal class ReportParserUtilsTest
    {
        /// <summary>
        /// Tests the function for getting property names from a POCO
        /// that represents a row of a report. Makes sure only
        /// properties marked with the <see cref="ReportColumn"/> annotation
        /// are considered.
        /// </summary>
        [Test]
        public void getColumnNamesFromPocoTypeTest()
        {
            var columns = ReportParserUtils.GetColumnNamesFromPocoType<TestRow>()
                .ToDictionary(name => name);

            Assert.AreEqual(3, columns.Count);
            Assert.IsTrue(columns.ContainsKey("Atrib1"));
            Assert.IsTrue(columns.ContainsKey("Atrib2"));
            Assert.IsTrue(columns.ContainsKey("Atrib3"));
        }

        /// <summary>
        /// Tests the function for converting string values to the proper
        /// types and setting the properties in the POCO object for holding a
        /// row of data from a report.
        /// </summary>
        [Test]
        public void setColumnValueTest()
        {
            var testRow = new TestRow();
            Action<ColumnValuePair, TestRow> doNothing = (a, b) => { };
            ReportParserUtils.SetColumnValue(new ColumnValuePair("Atrib1", "1"), testRow,
                doNothing);
            ReportParserUtils.SetColumnValue(new ColumnValuePair("Atrib2", "2"), testRow,
                doNothing);
            ReportParserUtils.SetColumnValue(new ColumnValuePair("Atrib3", "3"), testRow,
                doNothing);

            Assert.IsTrue(Math.Abs(1.0 - testRow.Atrib1) < 0.0000001);
            Assert.AreEqual("2", testRow.Atrib2);
            Assert.AreEqual(3, testRow.AtribX);
        }

        /// <summary>
        /// Tests that an invalid POCO type with repeated column names is detected.
        /// </summary>
        [Test]
        public void testValidatePocoType()
        {
            Assert.Throws<ArgumentException>(() =>
                ReportParserUtils.ValidatePocoType<InvalidTestRow>());
        }

        /// <summary>
        /// Tests the function for getting the column and property names for a POCO type.
        /// </summary>
        [Test]
        public void getColAndPropNamesTest()
        {
            var names = ReportParserUtils.GetColAndPropNames<TestRow>();
            Assert.AreEqual(3, names.Count());

            var propNames = names.ToDictionary(x => x.Item2);
            Assert.IsTrue(propNames.ContainsKey("Atrib1"));
            Assert.IsTrue(propNames.ContainsKey("Atrib2"));
            Assert.IsTrue(propNames.ContainsKey("AtribX"));
        }

        /// <summary>
        /// Tests the function for getting property names from column names.
        /// </summary>
        [Test]
        public void getPropNameFromColNameTest()
        {
            Assert.AreEqual("Atrib1", ReportParserUtils.GetPropNameFromColName<TestRow>("Atrib1"));
            Assert.AreEqual("Atrib2", ReportParserUtils.GetPropNameFromColName<TestRow>("Atrib2"));
            Assert.AreEqual("AtribX", ReportParserUtils.GetPropNameFromColName<TestRow>("Atrib3"));
        }
    }
}

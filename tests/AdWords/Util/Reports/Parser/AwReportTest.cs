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
    /// Unit tests for the <see cref="AwReport{A}"/> class.
    /// </summary>
    internal class AwReportTest
    {
        // A valid XML report for use with the <see ref="TestRow" /> POCO type.
        private readonly string testXml = Resources.ValidXMLDistinctRows;

        // An XML report that will cause errors with the <see ref="TestRow" /> POCO.
        private readonly string faultyTestXml = Resources.FaultyXML;

        private void testActionWithXmlReport(Action<AwReport<TestRow>> action)
        {
            TestUtils.testActionWithXmlTextReader(
                reader => { action(new AwReport<TestRow>(reader, "test")); }, testXml);
        }

        /// <summary>
        /// Tests the MoveNext function. The provided report only has 4 rows, but this
        /// test tries to move to next row 10 times, however only 4 of those calls
        /// must return true.
        /// </summary>
        [Test]
        public void moveNextRowTest()
        {
            testActionWithXmlReport(report =>
            {
                var moves = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (report.MoveNext())
                    {
                        moves++;
                    }
                }

                Assert.AreEqual(4, moves);
            });
        }

        /// <summary>
        /// Tests getting all the rows of the faulty XML report. This should result in an exception
        /// because none of the columns needed by <see ref="TestRow" /> appear in the XML.
        /// </summary>
        [Test]
        public void getFaultyRowsTest()
        {
            Assert.Throws<AdWordsReportsException>(() => TestUtils.testActionWithXmlTextReader(
                reader =>
                {
                    var report = new AwReport<TestRow>(reader, "test");
                    report.GetRows();
                }, faultyTestXml));
        }

        /// <summary>
        /// Tests getting all 4 rows of the report. Makes sure that only 4 objects are returned
        /// representing the 4 rows. After this operation all of the rows will have been
        /// enumerated, so GetCurrentRow should always return the same last row.
        /// </summary>
        [Test]
        public void getRowsTest()
        {
            testActionWithXmlReport(report =>
            {
                Assert.AreEqual(4, report.Rows.Count());
                Assert.IsFalse(report.MoveNext());

                var finalRow = report.Current;
                report.MoveNext();
                var finalRow2 = report.Current;

                Assert.AreEqual(finalRow.Atrib1, finalRow2.Atrib1);
                Assert.AreEqual(finalRow.Atrib2, finalRow2.Atrib2);
                Assert.AreEqual(finalRow.AtribX, finalRow2.AtribX);
            });
        }

        /// <summary>
        /// Manually walks through the report row by row using MoveNext.
        /// There are only 4 rows, but this test moves next row more times than that
        /// and makes sure the currentRow stays on the final row.
        /// </summary>
        [Test]
        public void getCurrentRowTest()
        {
            testActionWithXmlReport(report =>
            {
                report.MoveNext();
                var currentRow = report.Current;
                Assert.AreEqual("2", currentRow.Atrib2);
                report.MoveNext();
                currentRow = report.Current;
                Assert.AreEqual("4", currentRow.Atrib2);
                report.MoveNext();
                currentRow = report.Current;
                Assert.AreEqual("7", currentRow.Atrib2);
                report.MoveNext();
                currentRow = report.Current;
                Assert.AreEqual("10", currentRow.Atrib2);
                report.MoveNext();
                currentRow = report.Current;
                Assert.AreEqual("10", currentRow.Atrib2);
            });
        }

        /// <summary>
        /// Tests to make sure an exception is thrown when the current row
        /// is requested before moving onto the first row by first calling
        /// MoveNext.
        /// </summary>
        [Test]
        public void getCurrentRowBeforeMoveNextTest()
        {
            Assert.Throws<AdWordsReportsException>(() => testActionWithXmlReport(report =>
            {
                var unused = report.Current;
            }));
        }
    }
}

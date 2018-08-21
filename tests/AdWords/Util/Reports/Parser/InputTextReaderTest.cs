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
    /// Tests for the <see ref="InputTextReader" /> classes.
    /// </summary>
    internal class InputReaderTest
    {
        // A sample fake XML report for testing.
        private readonly string testXml = Resources.ValidXMLRepeatedRows;

        private void testActionWithXmlTextReader(Action<AwXmlTextReader> action)
        {
            TestUtils.testActionWithXmlTextReader(action, testXml);
        }

        /// <summary>
        /// A test for the <see ref="AwXmlTextReader" /> class. Tests that the reader
        /// can be advanced to the next row 4 times as there are only 4 rows in the
        /// sample xml.
        /// </summary>
        [Test]
        public void testXmlRead()
        {
            testActionWithXmlTextReader(reader =>
            {
                var rows = 0;

                while (reader.Read())
                {
                    rows++;
                }

                Assert.AreEqual(4, rows);
            });
        }

        /// <summary>
        /// Tests that the names and values of the columns in a row are
        /// retrieved properly.
        /// </summary>
        [Test]
        public void testXmlGetAttributes()
        {
            testActionWithXmlTextReader(reader =>
            {
                reader.Read();

                var attributes = reader.GetAttributes()
                    .ToDictionary(colval => colval.ColName, colval => colval.Value);

                Assert.AreEqual(3, attributes.Count);
                Assert.AreEqual("1", attributes["Atrib1"]);
                Assert.AreEqual("2", attributes["Atrib2"]);
                Assert.AreEqual("3", attributes["Atrib3"]);
            });
        }
    }
}

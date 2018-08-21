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

using System;
using System.IO;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.Util.Reports.Parser
{
    /// <summary>
    /// A static class containing common functions used in unit tests for report parsing.
    /// </summary>
    public static class TestUtils
    {
        /// <summary>
        /// Creates a Steam from a given string.
        /// </summary>
        /// <param name="s">The string from which to create the Stream.</param>
        /// <returns>A stream of the string.</returns>
        public static Stream GenerateStreamFromString(string s)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(s));
        }

        /// <summary>
        /// Run an action using a <see ref="AwXmlTextReader" /> from a provided XML string.
        /// </summary>
        /// <param name="action">The action to run with an <see ref="AwXmlTextReader" /></param>
        /// <param name="testXml">The string of XML from which the <see ref="AwXmlTextReader" />
        /// will be created</param>
        public static void testActionWithXmlTextReader(Action<AwXmlTextReader> action,
            string testXml)
        {
            using (var input = TestUtils.GenerateStreamFromString(testXml))
            {
                using (var reader = new AwXmlTextReader(input))
                {
                    action(reader);
                }
            }
        }
    }

    /// <summary>
    /// An example of a POCO that can be used to hold report data. Used for unit testing.
    /// </summary>
    internal class TestRow
    {
        [ReportColumn]
        public double Atrib1 { get; set; }

        [ReportColumn]
        public string Atrib2 { get; set; }

        [ReportColumn("Atrib3")]
        public long AtribX { get; set; }

        public decimal NonReportMember { get; set; }
    }

    /// <summary>
    /// An example of a POCO that has a duplicate column name.
    /// </summary>
    internal class InvalidTestRow
    {
        [ReportColumn]
        public double Atrib1 { get; set; }

        [ReportColumn("Atrib1")]
        public string Atrib2 { get; set; }
    }
}

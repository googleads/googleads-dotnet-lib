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

using Google.Api.Ads.AdWords.v201806;

using NUnit.Framework;

using System.IO;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201806;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201806;

namespace Google.Api.Ads.AdWords.Tests.v201806
{
    /// <summary>
    /// Test cases for all the code examples under v201806\Reporting.
    /// </summary>
    internal class ReportingTest : VersionedExampleTestsBase
    {
        private string outputFolderPath;
        private string outputFileName;

        private ReportDefinitionReportType reportType;

        /// <summary>
        /// Inits this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            outputFolderPath = Path.GetTempPath();
            outputFileName = utils.GetTimeStampAlpha() + ".gz";
            reportType = ReportDefinitionReportType.CRITERIA_PERFORMANCE_REPORT;
        }

        /// <summary>
        /// Tests the DownloadCriteriaReportWithSelector VB.NET code example.
        /// </summary>
        [Test]
        public void TestDownloadCriteriaReportWithSelectorVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.DownloadCriteriaReportWithSelector().Run(user, outputFileName);
            });
        }

        /// <summary>
        /// Tests the DownloadCriteriaReportWithSelector C# code example.
        /// </summary>
        [Test]
        public void TestDownloadCriteriaReportWithSelectorCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.DownloadCriteriaReportWithSelector().Run(user,
                    outputFileName);
            });
        }

        /// <summary>
        /// Tests the DownloadCriteriaReportWithAwql VB.NET code example.
        /// </summary>
        [Test]
        public void TestDownloadCriteriaReportWithAwqlVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.DownloadCriteriaReportWithAwql().Run(user, outputFileName);
            });
        }

        /// <summary>
        /// Tests the DownloadCriteriaReportWithAwql C# code example.
        /// </summary>
        [Test]
        public void TestDownloadCriteriaReportWithAwqlCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.DownloadCriteriaReportWithAwql().Run(user, outputFileName);
            });
        }

        /// <summary>
        /// Tests the GetReportFields VB.NET code example.
        /// </summary>
        [Test]
        public void TestGetReportFieldsVBExample()
        {
            RunExample(delegate() { new VBExamples.GetReportFields().Run(user, reportType); });
        }

        /// <summary>
        /// Tests the GetReportFields C# code example.
        /// </summary>
        [Test]
        public void TestGetReportFieldsCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.GetReportFields().Run(user, reportType); });
        }

        /// <summary>
        /// Tests the StreamCriteriaReportResults VB.NET code example.
        /// </summary>
        [Test]
        public void TestStreamCriteriaReportResultsVBExample()
        {
            RunExample(delegate() { new VBExamples.StreamCriteriaReportResults().Run(user); });
        }

        /// <summary>
        /// Tests the StreamCriteriaReportResults C# code example.
        /// </summary>
        [Test]
        public void TestStreamCriteriaReportResultsCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.StreamCriteriaReportResults().Run(user); });
        }

        /// <summary>
        /// Tests the StreamCriteriaReportToPoco C# code example.
        /// </summary>
        [Test]
        public void TestStreamCriteriaReportToPocoCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.StreamCriteriaReportToPoco().Run(user); });
        }

        /// <summary>
        /// Tests the StreamCriteriaReportToPoco VB.NET code example.
        /// </summary>
        [Test]
        public void TestStreamCriteriaReportToPocoVBExample()
        {
            RunExample(delegate() { new VBExamples.StreamCriteriaReportToPoco().Run(user); });
        }

        /// <summary>
        /// Tests the StreamReportToPredefinedReportRowType VB.NET code example.
        /// </summary>
        [Test]
        public void TestStreamReportToPredefinedReportRowTypeVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.StreamReportToPredefinedReportRowType().Run(user);
            });
        }

        /// <summary>
        /// Tests the StreamReportToPredefinedReportRowType C# code example.
        /// </summary>
        [Test]
        public void TestStreamReportToPredefinedReportRowTypeCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.StreamReportToPredefinedReportRowType().Run(user);
            });
        }

        /// <summary>
        /// Tests the ParallelReportDownload C# code example.
        /// </summary>
        [Test]
        public void TestParallelReportDownloadCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.ParallelReportDownload().Run(user, outputFolderPath);
            });
        }
    }
}

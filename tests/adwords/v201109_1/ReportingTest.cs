// Copyright 2012, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.v201109_1;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201109_1;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201109_1;

namespace Google.Api.Ads.AdWords.Tests.v201109_1 {
  /// <summary>
  /// Test cases for all the code examples under v201109_1\Reporting.
  /// </summary>
  class ReportingTest : ExampleBaseTests {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      parameters = new Dictionary<string, string>();

      parameters["OUTPUT_FILE_NAME"] = Path.GetTempFileName();
      parameters["REPORT_TYPE"] = ReportDefinitionReportType.CRITERIA_PERFORMANCE_REPORT.ToString();
    }

    /// <summary>
    /// Tests the DownloadCriteriaReport VB.NET code example.
    /// </summary>
    [Test]
    public void TestDownloadCriteriaReportVBExample() {
      RunExample(new VBExamples.DownloadCriteriaReport());
    }

    /// <summary>
    /// Tests the DownloadCriteriaReport C# code example.
    /// </summary>
    [Test]
    public void TestDownloadCriteriaReportCSharpExample() {
      RunExample(new CSharpExamples.DownloadCriteriaReport());
    }

    /// <summary>
    /// Tests the GetDefinedReports VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetDefinedReportsVBExample() {
      RunExample(new VBExamples.GetDefinedReports());
    }

    /// <summary>
    /// Tests the GetDefinedReports C# code example.
    /// </summary>
    [Test]
    public void TestGetDefinedReportsCSharpExample() {
      RunExample(new CSharpExamples.GetDefinedReports());
    }

    /// <summary>
    /// Tests the GetReportFields VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetReportFieldsVBExample() {
      RunExample(new VBExamples.GetReportFields());
    }

    /// <summary>
    /// Tests the GetReportFields C# code example.
    /// </summary>
    [Test]
    public void TestGetReportFieldsCSharpExample() {
      RunExample(new CSharpExamples.GetReportFields());
    }

    /// <summary>
    /// Tests the GetCampaignStats VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetCampaignStatsVBExample() {
      RunExample(new VBExamples.GetCampaignStats());
    }

    /// <summary>
    /// Tests the GetCampaignStats C# code example.
    /// </summary>
    [Test]
    public void TestGetCampaignStatsCSharpExample() {
      RunExample(new CSharpExamples.GetCampaignStats());
    }
  }
}

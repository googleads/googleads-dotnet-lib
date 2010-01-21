// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.lib.util;
using com.google.api.adwords.v13;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace com.google.api.adwords.tests.lib.util {
  /// <summary>
  /// UnitTests for <see cref="ReportUtilities"/> class.
  /// </summary>
  [TestFixture]
  public class ReportUtilitiesTests {
    /// <summary>
    /// Setup the test accounts.
    /// </summary>
    [SetUp]
    public void Setup() {
      AdWordsUser user = new AdWordsUser();
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      accountService.clientEmailValue = null;
      string[] clients = accountService.getClientAccounts();
    }

    /// <summary>
    /// Test for ReportUtilities.DownloadReportAsXml.
    /// </summary>
    [Test]
    public void TestDownloadReportAsXml() {
      AdWordsUser user = new AdWordsUser();
      string reportName = "";
      ReportUtilities utilities = new ReportUtilities(user);

      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.name = "Keyword Report";
      reportJob.selectedReportType = "Keyword";
      reportJob.aggregationTypes = new String[] {"Daily"};
      reportJob.adWordsType = AdWordsType.SearchOnly;
      reportJob.adWordsTypeSpecified = true;
      reportJob.endDay = DateTime.Today;  // defaults to today
      reportJob.startDay = new DateTime(2009, 1, 1);
      reportJob.selectedColumns = new String[] {
          "Campaign", "AdGroup", "Keyword", "KeywordStatus", "KeywordMinCPC",
          "KeywordDestUrlDisplay", "Impressions", "Clicks", "CTR", "AveragePosition"};

      // Report generation process should not throw an exception.
      reportName = Path.GetTempFileName();
      Assert.DoesNotThrow(
          delegate() {
            utilities.DownloadReportAsXml(reportJob, reportName);
          },
          "DownloadReportAsXml should not throw an exception.");

      // Downloaded report should be a valid xml.
      XmlDocument reportDoc = new XmlDocument();
      Assert.DoesNotThrow(
          delegate() {
            reportDoc.Load(reportName);
          },
          "DownloadReportAsXml did not download a valid xml report.");
    }

    /// <summary>
    /// Test for ReportUtilities.BeginDownloadReportAsXml and
    /// ReportUtilities.EndDownloadReportAsXml.
    /// </summary>
    [Test]
    public void TestDownloadReportAsXmlAsync() {
      AdWordsUser user = new AdWordsUser();
      string reportName = "";
      ReportUtilities utilities = new ReportUtilities(user);

      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.name = "Keyword Report";
      reportJob.selectedReportType = "Keyword";
      reportJob.aggregationTypes = new String[] {"Daily"};
      reportJob.adWordsType = AdWordsType.SearchOnly;
      reportJob.adWordsTypeSpecified = true;
      reportJob.endDay = DateTime.Today;  // defaults to today
      reportJob.startDay = new DateTime(2009, 1, 1);
      reportJob.selectedColumns = new String[] {
          "Campaign", "AdGroup", "Keyword", "KeywordStatus", "KeywordMinCPC",
          "KeywordDestUrlDisplay", "Impressions", "Clicks", "CTR", "AveragePosition"};

      // Report generation process should not throw an exception.
      reportName = Path.GetTempFileName();
      Assert.DoesNotThrow(
          delegate() {
            IAsyncResult asyncHandle =
                utilities.BeginDownloadReportAsXml(reportJob, reportName, null);
            asyncHandle.AsyncWaitHandle.WaitOne();
            utilities.EndDownloadReportAsXml(asyncHandle);
          },
          "DownloadReportAsXml (async) should not throw an exception.");

      // Downloaded report should be a valid xml.
      XmlDocument reportDoc = new XmlDocument();
      Assert.DoesNotThrow(
          delegate() {
            reportDoc.Load(reportName);
          },
          "DownloadReportAsXml did not download a valid xml report.");
    }

    /// <summary>
    /// Test for ReportUtilities.DownloadReportAsCsv.
    /// </summary>
    [Test]
    public void TestDownloadReportAsCsv() {
      AdWordsUser user = new AdWordsUser();
      string reportName = "";
      ReportUtilities utilities = new ReportUtilities(user);

      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.name = "Keyword Report";
      reportJob.selectedReportType = "Keyword";
      reportJob.aggregationTypes = new String[] {"Daily"};
      reportJob.adWordsType = AdWordsType.SearchOnly;
      reportJob.adWordsTypeSpecified = true;
      reportJob.endDay = DateTime.Today;  // defaults to today
      reportJob.startDay = new DateTime(2009, 1, 1);
      reportJob.selectedColumns = new String[] {
          "Campaign", "AdGroup", "Keyword", "KeywordStatus", "KeywordMinCPC",
          "KeywordDestUrlDisplay", "Impressions", "Clicks", "CTR", "AveragePosition"};

      // Report generation process should not throw an exception.
      reportName = Path.GetTempFileName();
      Assert.DoesNotThrow(
          delegate() {
            utilities.DownloadReportAsCsv(reportJob, reportName);
          },
          "DownloadReportAsCsv should not throw an exception.");

      // Downloaded report should be a valid csv.
      CsvFile csvDoc = new CsvFile();
      Assert.DoesNotThrow(
          delegate() {
            csvDoc.Read(reportName, true);
          },
          "DownloadReportAsCsv did not download a valid csv report.");
    }

    /// <summary>
    /// Test for ReportUtilities.BeginDownloadReportAsCsv and
    /// ReportUtilities.EndDownloadReportAsCsv.
    /// </summary>
    [Test]
    public void TestDownloadReportAsCsvAsync() {
      AdWordsUser user = new AdWordsUser();
      string reportName = "";
      ReportUtilities utilities = new ReportUtilities(user);

      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.name = "Keyword Report";
      reportJob.selectedReportType = "Keyword";
      reportJob.aggregationTypes = new String[] {"Daily"};
      reportJob.adWordsType = AdWordsType.SearchOnly;
      reportJob.adWordsTypeSpecified = true;
      reportJob.endDay = DateTime.Today;  // defaults to today
      reportJob.startDay = new DateTime(2009, 1, 1);
      reportJob.selectedColumns = new String[] {
          "Campaign", "AdGroup", "Keyword", "KeywordStatus", "KeywordMinCPC",
          "KeywordDestUrlDisplay", "Impressions", "Clicks", "CTR", "AveragePosition"};

      // Report generation process should not throw an exception.
      reportName = Path.GetTempFileName();
      Assert.DoesNotThrow(
          delegate() {
            IAsyncResult asyncHandle =
                utilities.BeginDownloadReportAsCsv(reportJob, reportName, null);
            asyncHandle.AsyncWaitHandle.WaitOne();
            utilities.EndDownloadReportAsCsv(asyncHandle);
          },
          "DownloadReportAsCsv (async) should not throw an exception.");

      // Downloaded report should be a valid csv.
      CsvFile csvDoc = new CsvFile();
      Assert.DoesNotThrow(
          delegate() {
            csvDoc.Read(reportName, true);
          },
          "DownloadReportAsCsv did not download a valid csv report.");
    }
  }
}

// Copyright 2011, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v13;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v13 {
  /// <summary>
  /// Unittest for ReportService.
  /// </summary>
  [TestFixture]
  class ReportServiceTests : BaseTests {
    /// <summary>
    /// ReportService object to be used in this test.
    /// </summary>
    private ReportService reportService;

    /// <summary>
    /// Predefined report ids for the AdWords sandbox.
    /// </summary>
    private enum ReportJobIds {
      Pending = 11,
      InProgress = 22,
      Completed = 33,
      Failed = 44
    };

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ReportServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      reportService = (ReportService) user.GetService(AdWordsService.v13.ReportService);
    }

    /// <summary>
    /// Test whether we can delete an existing report.
    /// </summary>
    [Test]
    public void TestDeleteReport() {
      Assert.DoesNotThrow(delegate() {
        reportService.deleteReport((long)ReportJobIds.Failed);
      });
    }

    /// <summary>
    /// Test whether we can fetch all existing report jobs.
    /// </summary>
    [Test]
    public void TestGetAllJobs() {
      Assert.DoesNotThrow(delegate() {
        ReportJob[] jobs = reportService.getAllJobs();
      });
    }

    /// <summary>
    /// Test whether we can fetch a Gzip report download URL.
    /// </summary>
    [Test]
    public void TestGetGzipReportDownloadUrl() {
      string reportDownloadUrl = null;
      Assert.DoesNotThrow(delegate() {
        reportDownloadUrl = reportService.getGzipReportDownloadUrl((long)ReportJobIds.Completed);
      });
      Assert.IsNotNullOrEmpty(reportDownloadUrl);
    }

    /// <summary>
    /// Test whether we can fetch a report download URL.
    /// </summary>
    [Test]
    public void TestGetReportDownloadUrl() {
      string reportDownloadUrl = null;
      Assert.DoesNotThrow(delegate() {
        reportDownloadUrl = reportService.getReportDownloadUrl((long)ReportJobIds.Completed);
      });
      Assert.IsNotNullOrEmpty(reportDownloadUrl);
    }

    /// <summary>
    /// Test whether we can fetch report job status.
    /// </summary>
    [Test]
    public void TestGetReportJobStatus() {
      ReportJobStatus status = ReportJobStatus.Pending;
      Assert.DoesNotThrow(delegate() {
        status = reportService.getReportJobStatus((long) ReportJobIds.Completed);
      });
      Assert.AreEqual(status, ReportJobStatus.Completed);
    }

    /// <summary>
    /// Test whether we can schedule a defined report job.
    /// </summary>
    [Test]
    public void TestScheduleDefinedReportJob() {
      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.adWordsType = AdWordsType.SearchOnly;
      reportJob.aggregationTypes = new string[] {"Daily"};
      reportJob.campaignStatuses =
          new CampaignStatus[]{CampaignStatus.Active | CampaignStatus.Paused};
      reportJob.crossClient = false;
      reportJob.endDay = new DateTime(2008, 1, 31);
      reportJob.includeZeroImpression = false;
      reportJob.name = "Test Report";
      reportJob.selectedColumns = new string[] {"Campaign", "CampaignId", "CPC", "CTR"};
      reportJob.startDay = new DateTime(2008, 1, 1);
      reportJob.selectedReportType = "Campaign";

      Assert.DoesNotThrow(delegate() {
        reportService.scheduleReportJob(reportJob);
      });
    }

    /// <summary>
    /// Test whether we can validate a report job.
    /// </summary>
    [Test]
    public void TestValidateReportJob() {
      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.adWordsType = AdWordsType.SearchOnly;
      reportJob.aggregationTypes = new string[] {"Daily"};
      reportJob.campaignStatuses =
          new CampaignStatus[]{CampaignStatus.Active | CampaignStatus.Paused};
      reportJob.crossClient = false;
      reportJob.endDay = new DateTime(2008, 1, 31);
      reportJob.includeZeroImpression = false;
      reportJob.name = "Test Report";
      reportJob.selectedColumns = new string[] {"Campaign", "CampaignId", "CPC", "CTR"};
      reportJob.startDay = new DateTime(2008, 1, 1);
      reportJob.selectedReportType = "Campaign";

      Assert.DoesNotThrow(delegate() {
        reportService.validateReportJob(reportJob);
      });
    }

    /// <summary>
    /// Test whether we can validate a structure report job.
    /// </summary>
    [Test]
    public void TestValidateReportJobStructure() {
      DefinedReportJob reportJob = new DefinedReportJob();
      reportJob.aggregationTypes = new string[] {"Keyword"};
      reportJob.name = "Test Report";
      reportJob.selectedColumns = new string[] {"CampaignId", "AdGroupId", "KeywordId", "Keyword"};
      reportJob.selectedReportType = "Structure";

      Assert.DoesNotThrow(delegate() {
        reportService.validateReportJob(reportJob);
      });
    }
  }
}

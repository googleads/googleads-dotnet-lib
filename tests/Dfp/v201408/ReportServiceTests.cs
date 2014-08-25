// Copyright 2014, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201408;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;

namespace Google.Api.Ads.Dfp.Tests.v201408 {
  /// <summary>
  /// UnitTests for <see cref="ReportService"/> class.
  /// </summary>
  [TestFixture]
  public class ReportServiceTests : BaseTests {
    /// <summary>
    /// UnitTests for <see cref="ReportService"/> class.
    /// </summary>
    private ReportService reportService;

    /// <summary>
    /// Report id for running further tests.
    /// </summary>
    long reportId;

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
      TestUtils utils = new TestUtils();
      reportService = (ReportService) user.GetService(DfpService.v201408.ReportService);
      reportId = utils.CreateReport(user).id;
    }

    /// <summary>
    /// Test whether we can run a delivery report.
    /// </summary>
    [Test]
    public void TestRunDeliveryReport() {
      ReportJob reportJob = new ReportJob();
      reportJob.reportQuery = new ReportQuery();
      reportJob.reportQuery.dimensions = new Dimension[] {Dimension.ORDER_ID, Dimension.ORDER_NAME};
      reportJob.reportQuery.columns = new Column[] {Column.AD_SERVER_IMPRESSIONS,
          Column.AD_SERVER_CLICKS, Column.AD_SERVER_CTR, Column.AD_SERVER_CPM_AND_CPC_REVENUE,
          Column.AD_SERVER_WITHOUT_CPD_AVERAGE_ECPM};
      reportJob.reportQuery.dateRangeType = DateRangeType.LAST_MONTH;

      Assert.DoesNotThrow(delegate() {
        reportJob = reportService.runReportJob(reportJob);
      });

      Assert.NotNull(reportJob);
    }

    /// <summary>
    /// Test whether we can run an inventory report.
    /// </summary>
    [Test]
    public void TestRunInventoryReport() {
      ReportJob reportJob = new ReportJob();
      reportJob.reportQuery = new ReportQuery();
      reportJob.reportQuery.dimensions = new Dimension[] {Dimension.DATE};
      reportJob.reportQuery.columns = new Column[] {Column.AD_SERVER_IMPRESSIONS,
          Column.AD_SERVER_CLICKS, Column.DYNAMIC_ALLOCATION_INVENTORY_LEVEL_IMPRESSIONS,
          Column.DYNAMIC_ALLOCATION_INVENTORY_LEVEL_CLICKS,
          Column.TOTAL_INVENTORY_LEVEL_IMPRESSIONS,
          Column.TOTAL_INVENTORY_LEVEL_CPM_AND_CPC_REVENUE};
      reportJob.reportQuery.dateRangeType = DateRangeType.LAST_WEEK;

      ReportJob newReportJob = null;

      Assert.DoesNotThrow(delegate() {
        reportJob = reportService.runReportJob(reportJob);
      });

      Assert.NotNull(reportJob);
    }

    /// <summary>
    /// Test whether we can run a sales report.
    /// </summary>
    [Test]
    public void TestRunSalesReport() {
      ReportJob reportJob = new ReportJob();
      reportJob.reportQuery = new ReportQuery();
      reportJob.reportQuery.dimensions = new Dimension[] {Dimension.SALESPERSON_ID,
          Dimension.SALESPERSON_NAME};
      reportJob.reportQuery.columns = new Column[] {Column.AD_SERVER_IMPRESSIONS,
          Column.AD_SERVER_CPM_AND_CPC_REVENUE, Column.AD_SERVER_WITHOUT_CPD_AVERAGE_ECPM};
      reportJob.reportQuery.dateRangeType = DateRangeType.LAST_MONTH;

      ReportJob newReportJob = null;

      Assert.DoesNotThrow(delegate() {
        reportJob = reportService.runReportJob(reportJob);
      });

      Assert.NotNull(reportJob);
    }

    /// <summary>
    /// Test whether we can retrieve existing report job.
    /// </summary>
    [Test]
    public void TestGetReportJob() {
      ReportJob reportJob = null;

      Assert.DoesNotThrow(delegate() {
        reportJob = reportService.getReportJob(reportId);
      });

      Assert.NotNull(reportJob);
      Assert.AreEqual(reportJob.id, reportId);
    }

    /// <summary>
    /// Test whether we can retrieve report download URL.
    /// </summary>
    [Test]
    public void TestGetReportDownloadUrl() {
      string url = null;

      Assert.DoesNotThrow(delegate() {
        url = reportService.getReportDownloadURL(reportId, ExportFormat.CSV_EXCEL);
      });

      Assert.True(Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute));

      Assert.DoesNotThrow(delegate() {
        url = reportService.getReportDownloadURL(reportId, ExportFormat.TSV);
      });

      Assert.True(Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute));
    }
  }
}

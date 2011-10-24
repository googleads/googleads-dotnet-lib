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
using Google.Api.Ads.AdWords.v201109;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201109 {
  /// <summary>
  /// UnitTests for <see cref="ReportDefinitionService"/> class.
  /// </summary>
  [TestFixture]
  class ReportDefinitionServiceTests : BaseTests {
    /// <summary>
    /// ReportDefinitionService object to be used in this test.
    /// </summary>
    private ReportDefinitionService reportDefinitionService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    private long adGroupId = 0;

    /// <summary>
    /// The criterion id for which tests are run.
    /// </summary>
    private long criterionId = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ReportDefinitionServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      reportDefinitionService = (ReportDefinitionService)user.GetService(
          AdWordsService.v201109.ReportDefinitionService);
      campaignId = utils.CreateCampaign(user, new ManualCPC());
      adGroupId = utils.CreateAdGroup(user, campaignId);
      criterionId = utils.CreateKeyword(user, adGroupId);
    }

    /// <summary>
    /// Test whether we can fetch report fields for ad performance report
    /// type.
    /// </summary>
    [Test]
    public void TestGetAdPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.AD_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for adgroup performance
    /// report type.
    /// </summary>
    [Test]
    public void TestGetAdGroupPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.ADGROUP_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for automatic placements
    /// performance report type.
    /// </summary>
    [Test]
    public void TestGetAutomaticPlacementsPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.AUTOMATIC_PLACEMENTS_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for campaign negative keywords
    /// performance report type.
    /// </summary>
    [Test]
    public void TestGetCampaignNegativeKeywordsPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.CAMPAIGN_NEGATIVE_KEYWORDS_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for campaign negative
    /// placements performance report type.
    /// </summary>
    [Test]
    public void TestGetCampaignNegativePlacementsPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.CAMPAIGN_NEGATIVE_PLACEMENTS_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for campaign performance
    /// report type.
    /// </summary>
    [Test]
    public void TestGetCampaignPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for keywords performance
    /// report type.
    /// </summary>
    [Test]
    public void TestGetKeywordsPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.KEYWORDS_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for managed placements
    /// performance report type.
    /// </summary>
    [Test]
    public void TestGetManagedPlacementsPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.MANAGED_PLACEMENTS_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for search query performance
    /// report type.
    /// </summary>
    [Test]
    public void TestGetSearchQueryPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.SEARCH_QUERY_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for url performance
    /// report type.
    /// </summary>
    [Test]
    public void TestUrlPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.URL_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }
  }
}

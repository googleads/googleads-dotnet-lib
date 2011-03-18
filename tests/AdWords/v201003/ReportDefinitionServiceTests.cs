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
using Google.Api.Ads.AdWords.v201003;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201003 {
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
          AdWordsService.v201003.ReportDefinitionService);
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
    /// Test whether we can fetch report fields for adgroup negative keyword
    /// performance report type.
    /// </summary>
    [Test]
    public void TestGetAdGroupNegativeKeywordsPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.ADGROUP_NEGATIVE_KEYWORDS_PERFORMANCE_REPORT);
      });
      Assert.NotNull(reportDefinitionFields);
      Assert.GreaterOrEqual(reportDefinitionFields.Length, 0);

      foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldName));
        Assert.False(String.IsNullOrEmpty(reportDefinitionField.fieldType));
      }
    }

    /// <summary>
    /// Test whether we can fetch report fields for adgroup negative placement
    /// performance report type.
    /// </summary>
    [Test]
    public void TestGetAdGroupNegativePlacementsPerformanceReportFields() {
      // Get report fields.
      ReportDefinitionField[] reportDefinitionFields = null;
      Assert.DoesNotThrow(delegate() {
        reportDefinitionFields = reportDefinitionService.getReportFields(
          ReportDefinitionReportType.ADGROUP_NEGATIVE_PLACEMENTS_PERFORMANCE_REPORT);
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

    /// <summary>
    /// Test whether we can add a keywords performance report.
    /// </summary>
    [Test]
    public void TestAddKeywordPerformanceReport() {
      // Create ad group predicate.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.EQUALS;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      // Create selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdGroupId", "Id", "KeywordText", "KeywordMatchType",
      "Impressions", "Clicks", "Cost"};
      selector.predicates = new Predicate[] {adGroupPredicate};
      selector.dateRange = new DateRange();
      selector.dateRange.min = "20100101";
      selector.dateRange.max = DateTime.Today.ToString("yyyyMMdd");

      // Create report definition.
      ReportDefinition reportDefinition = new ReportDefinition();
      reportDefinition.reportName = "Keywords performance report #" +
          new TestUtils().GetTimeStamp();
      reportDefinition.dateRangeType = ReportDefinitionDateRangeType.CUSTOM_DATE;
      reportDefinition.reportType = ReportDefinitionReportType.KEYWORDS_PERFORMANCE_REPORT;
      reportDefinition.downloadFormat = DownloadFormat.XML;
      reportDefinition.selector = selector;

      // Create operations.
      ReportDefinitionOperation operation = new ReportDefinitionOperation();
      operation.operand = reportDefinition;
      operation.@operator = Operator.ADD;

      ReportDefinition[] result = null;

      Assert.DoesNotThrow(delegate() {
        // Add report definition.
        result = reportDefinitionService.mutate(new ReportDefinitionOperation[] {operation});
      });

      Assert.NotNull(result);
      Assert.AreEqual(result.Length, 1);
      Assert.NotNull(result[0]);
      Assert.AreEqual(result[0].reportType, ReportDefinitionReportType.KEYWORDS_PERFORMANCE_REPORT);
      Assert.AreEqual(reportDefinition.downloadFormat, DownloadFormat.XML);
    }

    /// <summary>
    /// Test whether we can delete report definition.
    /// </summary>
    [Test]
    public void TestDeleteReportDefinition() {
      long reportId = new TestUtils().CreateKeywordPerformanceReport(user, adGroupId);

      ReportDefinition reportDefinition = new ReportDefinition();
      reportDefinition.id = reportId;

      ReportDefinitionOperation operation = new ReportDefinitionOperation();
      operation.@operator = Operator.REMOVE;
      operation.operand = reportDefinition;

      ReportDefinition[] result = null;

      Assert.DoesNotThrow(delegate() {
        // Add report definition.
        result = reportDefinitionService.mutate(new ReportDefinitionOperation[] {operation});
      });
    }
  }
}

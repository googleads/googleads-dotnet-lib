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
using Google.Api.Ads.AdWords.v201008;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201008 {
  /// <summary>
  /// UnitTests for <see cref="InfoServiceTests"/> class.
  /// </summary>
  [TestFixture]
  class InfoServiceTests : BaseTests {
    /// <summary>
    /// InfoService object to be used in this test.
    /// </summary>
    private InfoService infoService;


    /// <summary>
    /// Default public constructor.
    /// </summary>
    public InfoServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      infoService = (InfoService)user.GetService(AdWordsService.v201008.InfoService);
      infoService.RequestHeader.clientEmail = null;
    }

    /// <summary>
    /// Test whether we can get free usage units per month.
    /// </summary>
    [Test]
    public void TestGetFreeUsageUnitsPerMonth() {
      InfoSelector selector = new InfoSelector();
      selector.apiUsageType = ApiUsageType.FREE_USAGE_API_UNITS_PER_MONTH;

      ApiUsageInfo usageInfo = null;

      Assert.DoesNotThrow(delegate() {
        usageInfo = infoService.get(selector);
      });
      Assert.NotNull(usageInfo);
    }

    /// <summary>
    /// Test whether we can get total usage units per month.
    /// </summary>
    [Test]
    public void TestGetTotalUsageUnitsPerMonth() {
      InfoSelector selector = new InfoSelector();
      selector.apiUsageType = ApiUsageType.TOTAL_USAGE_API_UNITS_PER_MONTH;

      ApiUsageInfo usageInfo = null;

      Assert.DoesNotThrow(delegate() {
        usageInfo = infoService.get(selector);
      });
      Assert.NotNull(usageInfo);
    }

    /// <summary>
    /// Test whether we can get operation count.
    /// </summary>
    [Test]
    public void TestGetOperationCount() {
      InfoSelector selector = new InfoSelector();
      selector.apiUsageType = ApiUsageType.OPERATION_COUNT;
      selector.dateRange = new DateRange();
      selector.dateRange.min = DateTime.Today.ToString("yyyyMMdd");
      selector.dateRange.max = DateTime.Today.ToString("yyyyMMdd");

      ApiUsageInfo usageInfo = null;

      Assert.DoesNotThrow(delegate() {
        usageInfo = infoService.get(selector);
      });
      Assert.NotNull(usageInfo);
    }

    /// <summary>
    /// Test whether we can get unit count.
    /// </summary>
    [Test]
    public void TestGetUnitCount() {
      InfoSelector selector = new InfoSelector();
      selector.apiUsageType = ApiUsageType.OPERATION_COUNT;
      selector.dateRange = new DateRange();
      selector.dateRange.min = DateTime.Today.ToString("yyyyMMdd");
      selector.dateRange.max = DateTime.Today.ToString("yyyyMMdd");

      ApiUsageInfo usageInfo = null;

      Assert.DoesNotThrow(delegate() {
        usageInfo = infoService.get(selector);
      });
      Assert.NotNull(usageInfo);
    }

    /// <summary>
    /// Test whether we can get unit count for method.
    /// </summary>
    [Test]
    public void TestGetUnitCountForMethod() {
      InfoSelector selector = new InfoSelector();
      selector.apiUsageType = ApiUsageType.UNIT_COUNT;
      selector.dateRange = new DateRange();
      selector.dateRange.min = DateTime.Today.ToString("yyyyMMdd");
      selector.dateRange.max = DateTime.Today.ToString("yyyyMMdd");

      ApiUsageInfo usageInfo = null;

      Assert.DoesNotThrow(delegate() {
        usageInfo = infoService.get(selector);
      });
      Assert.NotNull(usageInfo);
    }

    /// <summary>
    /// Test whether we can get unit count for clients.
    /// </summary>
    [Test]
    public void TestGetUnitCountForClients() {
      InfoSelector selector = new InfoSelector();
      selector.apiUsageType = ApiUsageType.UNIT_COUNT_FOR_CLIENTS;
      selector.dateRange = new DateRange();
      selector.dateRange.min = DateTime.Today.ToString("yyyyMMdd");
      selector.dateRange.max = DateTime.Today.ToString("yyyyMMdd");

      ApiUsageInfo usageInfo = null;

      Assert.DoesNotThrow(delegate() {
        usageInfo = infoService.get(selector);
      });
      Assert.NotNull(usageInfo);
    }

    /// <summary>
    /// Test whether we can get unit count for clients.
    /// </summary>
    [Test]
    public void TestGetMethodCost() {
      InfoSelector selector = new InfoSelector();
      selector.apiUsageType = ApiUsageType.METHOD_COST;
      selector.dateRange = new DateRange();
      selector.dateRange.min = DateTime.Today.ToString("yyyyMMdd");
      selector.dateRange.max = DateTime.Today.ToString("yyyyMMdd");
      selector.serviceName = "AdGroupService";
      selector.methodName = "mutate";
      selector.@operator = Operator.SET;

      ApiUsageInfo usageInfo = null;

      Assert.DoesNotThrow(delegate() {
        usageInfo = infoService.get(selector);
      });
      Assert.NotNull(usageInfo);
    }
  }
}

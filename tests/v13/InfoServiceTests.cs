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
using com.google.api.adwords.v13;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tests.v13 {
  /// <summary>
  /// UnitTests for KeywordToolService class.
  /// </summary>
  [TestFixture]
  class InfoServiceTests  : BaseTests {
    /// <summary>
    /// InfoService object to be used in this test.
    /// </summary>
    InfoService infoService;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public InfoServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      AdWordsUser user = new AdWordsUser();
      infoService = (InfoService) user.GetService(AdWordsService.v13.InfoService);
    }

    /// <summary>
    /// Test whether we can get free usage quota for this month.
    /// </summary>
    [Test]
    public void TestGetFreeUsageQuotaThisMonth() {
      Assert.That(infoService.getFreeUsageQuotaThisMonth() is long);
    }

    /// <summary>
    /// Test whether we can get cost for a given method.
    /// </summary>
    [Test]
    public void TestGetMethodCost() {
      Assert.That(infoService.getMethodCost("AdGroupService", "mutate",
          new DateTime(2009, 8, 1)) is int);
    }

    /// <summary>
    /// Test whether we can get operation count for given date range.
    /// </summary>
    [Test]
    public void TestGetOperationCount() {
      Assert.That(infoService.getOperationCount(new DateTime(2009, 1, 1),
          new DateTime(2009, 1, 31)) is long);
    }

    /// <summary>
    /// Test whether we can get unit count for given date range.
    /// </summary>
    [Test]
    public void TestGetUnitCount() {
      Assert.That(infoService.getUnitCount(new DateTime(2009, 1, 1),
          new DateTime(2009, 1, 31)) is long);
    }

    /// <summary>
    /// Test whether we can get unit count for given date range.
    /// </summary>
    [Test]
    public void TestGetUnitCountForClients() {
      Assert.Throws(typeof(InvalidParameterException),
        delegate() {
          infoService.getUnitCountForClients(new string[] {"johndoe@example.com"},
          new DateTime(2009, 1, 1), new DateTime(2009, 1, 31));
        });
    }

    /// <summary>
    /// Test whether we can get unit count for given method and given date
    /// range.
    /// </summary>
    [Test]
    public void TestGetUnitCountForMethod() {
      Assert.That(infoService.getUnitCountForMethod("CampaignService", "getAllAdWordsCampaigns",
          new DateTime(2009, 1, 1), new DateTime(2009, 1, 31)) is long);
    }

    /// <summary>
    /// Test whether we can get unit count for given method and given date
    /// range.
    /// </summary>
    [Test]
    public void TestGetUsageQuotaThisMonth() {
      Assert.That(infoService.getUsageQuotaThisMonth() is long);
    }
  }
}

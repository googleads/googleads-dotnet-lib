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
  /// UnitTests for <see cref="CampaignTargetService"/> class.
  /// </summary>
  [TestFixture]
  class CampaignTargetServiceTests : BaseTests {
    /// <summary>
    /// CampaignTargetService object to be used in this test.
    /// </summary>
    private CampaignTargetService campaignTargetService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public CampaignTargetServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      campaignTargetService = (CampaignTargetService) user.GetService(
          AdWordsService.v201109.CampaignTargetService);
      campaignId = utils.CreateCampaign(user, new ManualCPC());
    }

    /// <summary>
    /// Test whether we can fetch all existing targets for given campaign.
    /// </summary>
    [Test]
    public void TestGetAllTargets() {
      CampaignTargetSelector selector = new CampaignTargetSelector();
      selector.campaignIds = new long[] {campaignId};

      CampaignTargetPage page = null;

      Assert.DoesNotThrow(delegate() {
        page = campaignTargetService.get(selector);
      });
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
    }

    /// <summary>
    /// Test whether we can add an ad schedule target to campaign.
    /// </summary>
    [Test]
    public void TestAddAdScheduleTarget() {
      // Create schedule targets.
      AdScheduleTargetList scheduleTargetList = new AdScheduleTargetList();
      scheduleTargetList.campaignId = campaignId;


      AdScheduleTarget scheduleTarget = new AdScheduleTarget();
      scheduleTarget.dayOfWeek = Google.Api.Ads.AdWords.v201109.DayOfWeek.MONDAY;
      scheduleTarget.startHour = 8;
      scheduleTarget.startMinute = 0;
      scheduleTarget.endHour = 17;
      scheduleTarget.endMinute = 0;
      scheduleTarget.bidMultiplier = 1.0;

      scheduleTargetList.targets = new AdScheduleTarget[] {scheduleTarget};

      // Create ad schedule target set operation.
      CampaignTargetOperation scheduleTargetOperation = new CampaignTargetOperation();
      scheduleTargetOperation.@operator = Operator.SET;
      scheduleTargetOperation.operand = scheduleTargetList;

      CampaignTargetReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {scheduleTargetOperation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
      Assert.That(retVal.value[0] is AdScheduleTargetList);
    }


    /// <summary>
    /// Test whether we can remove ad schedule targets.
    /// </summary>
    [Test]
    public void TestRemoveAllAdScheduleTargets() {
      AdScheduleTargetList adScheduleTargetList = new AdScheduleTargetList();
      adScheduleTargetList.campaignId = campaignId;
      adScheduleTargetList.targets = new AdScheduleTarget[] {};

      // Create ad schedule target operation.
      CampaignTargetOperation adScheduleTargetOperation = new CampaignTargetOperation();
      adScheduleTargetOperation.@operator = Operator.SET;
      adScheduleTargetOperation.operand = adScheduleTargetList;

      Assert.DoesNotThrow(delegate() {
        CampaignTargetReturnValue retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {adScheduleTargetOperation});
      });
    }
  }
}

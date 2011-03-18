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
using Google.Api.Ads.AdWords.v200909;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v200909 {
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
          AdWordsService.v200909.CampaignTargetService);
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
      scheduleTarget.dayOfWeek = Google.Api.Ads.AdWords.v200909.DayOfWeek.MONDAY;
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
    /// Test whether we can add an age target to campaign.
    /// </summary>
    [Test]
    public void TestAddDemographicTarget() {
      // Create demographic targets.
      DemographicTargetList demographicTargetList = new DemographicTargetList();
      demographicTargetList.campaignId = campaignId;

      AgeTarget ageTarget = new AgeTarget();
      ageTarget.age = AgeTargetAge.AGE_18_24;

      GenderTarget genderTarget = new GenderTarget();
      genderTarget.gender = GenderTargetGender.FEMALE;

      demographicTargetList.targets = new DemographicTarget[] {ageTarget, genderTarget};

      // Create demographic target set operation.
      CampaignTargetOperation demographicTargetOperation = new CampaignTargetOperation();
      demographicTargetOperation.@operator = Operator.SET;
      demographicTargetOperation.operand = demographicTargetList;

      CampaignTargetReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {demographicTargetOperation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
      Assert.That(retVal.value[0] is DemographicTargetList);
    }

    /// <summary>
    /// Test whether we can add a geo target to campaign.
    /// </summary>
    [Test]
    public void TestAddGeoTarget() {
      // Create geo targets.
      GeoTargetList geoTargetList = new GeoTargetList();
      geoTargetList.campaignId = campaignId;

      CityTarget cityTarget = new CityTarget();
      cityTarget.cityName = "New York";
      cityTarget.countryCode = "US";

      geoTargetList.targets = new GeoTarget[] {cityTarget};

      // Create geo target set operation.
      CampaignTargetOperation geoTargetOperation = new CampaignTargetOperation();
      geoTargetOperation.@operator = Operator.SET;
      geoTargetOperation.operand = geoTargetList;

      CampaignTargetReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {geoTargetOperation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
      Assert.That(retVal.value[0] is GeoTargetList);
    }

    /// <summary>
    /// Test whether we can add language targets to a campaign.
    /// </summary>
    [Test]
    public void TestAddLanguageTarget() {
      // Create language targets.
      LanguageTargetList langTargetList = new LanguageTargetList();
      langTargetList.campaignId = campaignId;

      LanguageTarget langTarget = new LanguageTarget();
      langTarget.languageCode = "en";

      langTargetList.targets = new LanguageTarget[] {langTarget};

      // Create language target set operation.
      CampaignTargetOperation langTargetOperation = new CampaignTargetOperation();
      langTargetOperation.@operator = Operator.SET;
      langTargetOperation.operand = langTargetList;

      CampaignTargetReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {langTargetOperation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
      Assert.That(retVal.value[0] is LanguageTargetList);
    }

    /// <summary>
    /// Test whether we can add a network and language targets to campaign.
    /// </summary>
    [Test]
    public void TestAddNetworkTarget() {
      // Create network targets.
      NetworkTargetList networkTargetList = new NetworkTargetList();
      networkTargetList.campaignId = campaignId;

      // Specifying GOOGLE_SEARCH is necessary if you want to target SEARCH_NETWORK.
      NetworkTarget networkTarget1 = new NetworkTarget();
      networkTarget1.networkCoverageType = NetworkCoverageType.GOOGLE_SEARCH;

      NetworkTarget networkTarget2 = new NetworkTarget();
      networkTarget2.networkCoverageType = NetworkCoverageType.SEARCH_NETWORK;

      networkTargetList.targets = new NetworkTarget[] {networkTarget1, networkTarget2};

      // Create network target set operation.
      CampaignTargetOperation networkTargetOperation = new CampaignTargetOperation();
      networkTargetOperation.@operator = Operator.SET;
      networkTargetOperation.operand = networkTargetList;

      CampaignTargetReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {networkTargetOperation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
      Assert.That(retVal.value[0] is NetworkTargetList);
    }

    /// <summary>
    /// Test whether we can add a platform target to campaign.
    /// </summary>
    [Test]
    public void TestAddPlatformTarget() {
      // Create platform targets.
      PlatformTargetList platformTargetList = new PlatformTargetList();
      platformTargetList.campaignId = campaignId;

      PlatformTarget platformTarget = new PlatformTarget();
      platformTarget.platformType = PlatformType.HIGH_END_MOBILE;

      platformTargetList.targets = new PlatformTarget[] {platformTarget};

      // Create platform target set operation.
      CampaignTargetOperation platformTargetOperation = new CampaignTargetOperation();
      platformTargetOperation.@operator = Operator.SET;
      platformTargetOperation.operand = platformTargetList;

      CampaignTargetReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {platformTargetOperation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
      Assert.That(retVal.value[0] is PlatformTargetList);
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

    /// <summary>
    /// Test whether we can remove demographic targets.
    /// </summary>
    [Test]
    public void TestRemoveAllDemographicTargets() {
      DemographicTargetList demographicTargetList = new DemographicTargetList();
      demographicTargetList.campaignId = campaignId;
      demographicTargetList.targets = new DemographicTarget[] {};

      // Create demographic target operation.
      CampaignTargetOperation demographicTargetOperation = new CampaignTargetOperation();
      demographicTargetOperation.@operator = Operator.SET;
      demographicTargetOperation.operand = demographicTargetList;

      Assert.DoesNotThrow(delegate() {
        CampaignTargetReturnValue retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {demographicTargetOperation});
      });
    }

    /// <summary>
    /// Test whether we can remove geo targets.
    /// </summary>
    [Test]
    public void TestRemoveAllGeoTargets() {
      GeoTargetList geoTargetList = new GeoTargetList();
      geoTargetList.campaignId = campaignId;
      geoTargetList.targets = new GeoTarget[] {};

      // Create geo target operation.
      CampaignTargetOperation geoTargetOperation = new CampaignTargetOperation();
      geoTargetOperation.@operator = Operator.SET;
      geoTargetOperation.operand = geoTargetList;

      Assert.DoesNotThrow(delegate() {
        CampaignTargetReturnValue retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {geoTargetOperation});
      });
    }

    /// <summary>
    /// Test whether we can remove language targets.
    /// </summary>
    [Test]
    public void TestRemoveAllLanguageTargets() {
      LanguageTargetList languageTargetList = new LanguageTargetList();
      languageTargetList.campaignId = campaignId;
      languageTargetList.targets = new LanguageTarget[] {};

      // Create language target operation.
      CampaignTargetOperation languageTargetOperation = new CampaignTargetOperation();
      languageTargetOperation.@operator = Operator.SET;
      languageTargetOperation.operand = languageTargetList;

      Assert.DoesNotThrow(delegate() {
        CampaignTargetReturnValue retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {languageTargetOperation});
      });
    }

    /// <summary>
    /// Test whether we can remove network targets.
    /// </summary>
    [Test]
    public void TestRemoveAllNetworkTargets() {
      NetworkTargetList networkTargetList = new NetworkTargetList();
      networkTargetList.campaignId = campaignId;
      networkTargetList.targets = new NetworkTarget[] {};

      // Create network target operation.
      CampaignTargetOperation networkTargetOperation = new CampaignTargetOperation();
      networkTargetOperation.@operator = Operator.SET;
      networkTargetOperation.operand = networkTargetList;

      Assert.DoesNotThrow(delegate() {
        CampaignTargetReturnValue retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {networkTargetOperation});
      });
    }

    /// <summary>
    /// Test whether we can remove platform targets.
    /// </summary>
    [Test]
    public void TestRemoveAllPlatformTargets() {
      PlatformTargetList platformTargetList = new PlatformTargetList();
      platformTargetList.campaignId = campaignId;
      platformTargetList.targets = new PlatformTarget[] {};

      // Create platform target operation.
      CampaignTargetOperation platformTargetOperation = new CampaignTargetOperation();
      platformTargetOperation.@operator = Operator.SET;
      platformTargetOperation.operand = platformTargetList;

      Assert.DoesNotThrow(delegate() {
        CampaignTargetReturnValue retVal = campaignTargetService.mutate(
            new CampaignTargetOperation[] {platformTargetOperation});
      });
    }

  }
}

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
using Google.Api.Ads.AdWords.v201101;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201101 {
  /// <summary>
  /// Unittest for TargetingIdeaService.
  /// </summary>
  [TestFixture]
  class TargetingIdeaServiceTests : BaseTests {
    /// <summary>
    /// TargetingIdeaService object to be used in this test.
    /// </summary>
    private TargetingIdeaService targetingIdeaService;

    /// <summary>
    /// AdGroupId to be used for test cases.
    /// </summary>
    private long adGroupId;

    /// <summary>
    /// CampaignId to be used for test cases.
    /// </summary>
    private long campaignId;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public TargetingIdeaServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      targetingIdeaService =
          (TargetingIdeaService) user.GetService(AdWordsService.v201101.TargetingIdeaService);
      campaignId = utils.CreateCampaign(user, new ManualCPC());
      adGroupId = utils.CreateAdGroup(user, campaignId);
    }

    /// <summary>
    /// Test whether we can catch required search parameter error in selector.
    /// </summary>
    [Test]
    public void TestGetEmptySelector() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();
      Assert.Throws(typeof(AdWordsApiException), delegate() {
        targetingIdeaService.get(selector);
      });
    }

    /// <summary>
    /// Test whether we can request ad type search parameter.
    /// </summary>
    [Test]
    public void TestGetAdTypeSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      AdTypeSearchParameter adtypeSearchParam = new AdTypeSearchParameter();
      adtypeSearchParam.adTypes = new SiteConstantsAdType[] {SiteConstantsAdType.DISPLAY};

      RelatedToUrlSearchParameter relatedToUrlSearchParam = new RelatedToUrlSearchParameter();
      relatedToUrlSearchParam.urls = new string[] {"http://news.google.com"};

      selector.searchParameters = new SearchParameter[] {
        adtypeSearchParam, relatedToUrlSearchParam
      };

      selector.ideaType = IdeaType.PLACEMENT;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request average targeted monthly search parameter.
    /// </summary>
    [Test]
    public void TestGetAverageTargetedMonthlySearchesSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      AverageTargetedMonthlySearchesSearchParameter avgMonthlySearchParam =
          new AverageTargetedMonthlySearchesSearchParameter();
      avgMonthlySearchParam.operation = new LongComparisonOperation();
      avgMonthlySearchParam.operation.minimum = 1;
      avgMonthlySearchParam.operation.maximum = 50;

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "flower shop";
      keyword.matchType = KeywordMatchType.BROAD;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {avgMonthlySearchParam, relatedToKeywordSearchParam};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request competition search parameter.
    /// </summary>
    [Test]
    public void TestGetCompetitionSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      CompetitionSearchParameter competitionSearchParam = new CompetitionSearchParameter();
      competitionSearchParam.levels = new CompetitionSearchParameterLevel[]
          {CompetitionSearchParameterLevel.MEDIUM, CompetitionSearchParameterLevel.HIGH};

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "cash for clunkers";
      keyword.matchType = KeywordMatchType.BROAD;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {competitionSearchParam, relatedToKeywordSearchParam};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request country target search parameter.
    /// </summary>
    [Test]
    public void TestGetCountryTargetSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      CountryTargetSearchParameter countryTargetSearchParam = new CountryTargetSearchParameter();
      CountryTarget target1 = new CountryTarget();
      target1.countryCode = "US";
      CountryTarget target2 = new CountryTarget();
      target2.countryCode = "CN";
      CountryTarget target3 = new CountryTarget();
      target3.countryCode = "JP";

      countryTargetSearchParam.countryTargets = new CountryTarget[] {target1, target2, target3};

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "global economy";
      keyword.matchType = KeywordMatchType.BROAD;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {countryTargetSearchParam, relatedToKeywordSearchParam};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request excluded keyword search parameter.
    /// </summary>
    [Test]
    public void TestGetExcludedKeywordSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();
      Keyword keyword = null;

      ExcludedKeywordSearchParameter excludedKwdSearchParam = new ExcludedKeywordSearchParameter();
      keyword = new Keyword();
      keyword.text = "media player";
      keyword.matchType = KeywordMatchType.EXACT;

      excludedKwdSearchParam.keywords = new Keyword[] {keyword};

      KeywordMatchTypeSearchParameter kwdMatchTypeSearchParam =
          new KeywordMatchTypeSearchParameter();

      kwdMatchTypeSearchParam.keywordMatchTypes =
          new KeywordMatchType[] {KeywordMatchType.BROAD, KeywordMatchType.EXACT};

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      keyword = new Keyword();
      keyword.text = "dvd player";
      keyword.matchType = KeywordMatchType.BROAD;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {excludedKwdSearchParam, kwdMatchTypeSearchParam,
              relatedToKeywordSearchParam};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request global monthly search parameter.
    /// </summary>
    [Test]
    public void TestGlobalMonthlySearchesSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      Keyword keyword = new Keyword();
      keyword.text = "media player";
      keyword.matchType = KeywordMatchType.EXACT;

      GlobalMonthlySearchesSearchParameter globalSearchParam =
          new GlobalMonthlySearchesSearchParameter();
      globalSearchParam.operation = new LongComparisonOperation();
      globalSearchParam.operation.minimum = 1000;
      globalSearchParam.operation.maximum = 10000;

      RelatedToKeywordSearchParameter relatedToSearchParam =
          new RelatedToKeywordSearchParameter();

      relatedToSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {globalSearchParam, relatedToSearchParam};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request include adult content search parameter.
    /// </summary>
    [Test]
    public void TestGetIncludeAdultContentSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      IncludeAdultContentSearchParameter includeAdultContentParam =
          new IncludeAdultContentSearchParameter();

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "books";
      keyword.matchType = KeywordMatchType.EXACT;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {includeAdultContentParam, relatedToKeywordSearchParam};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request keyword category id search parameter.
    /// </summary>
    [Test]
    public void TestGetKeywordCategoryIdSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      KeywordCategoryIdSearchParameter kwdCategoryIdSearchParameter =
          new KeywordCategoryIdSearchParameter();
      kwdCategoryIdSearchParameter.categoryId = 5;

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "rent video";
      keyword.matchType = KeywordMatchType.EXACT;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {kwdCategoryIdSearchParameter, relatedToKeywordSearchParam};

      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request keyword match type search parameter.
    /// </summary>
    [Test]
    public void TestGetKeywordMatchTypeSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      KeywordMatchTypeSearchParameter kwdMatchTypeSearchParam =
          new KeywordMatchTypeSearchParameter();

      kwdMatchTypeSearchParam.keywordMatchTypes =
          new KeywordMatchType[] {KeywordMatchType.BROAD, KeywordMatchType.EXACT};

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "cars";
      keyword.matchType = KeywordMatchType.EXACT;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {kwdMatchTypeSearchParam, relatedToKeywordSearchParam};

      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request keyword match type search parameter.
    /// </summary>
    [Test]
    public void TestGetLanguageTargetSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      LanguageTargetSearchParameter langTargetSearchParam =
          new LanguageTargetSearchParameter();

      LanguageTarget target1 = new LanguageTarget();
      target1.languageCode = "zh_CN";

      LanguageTarget target2 = new LanguageTarget();
      target2.languageCode = "ja";

      langTargetSearchParam.languageTargets = new LanguageTarget[] {target1, target2};

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "global economy";
      keyword.matchType = KeywordMatchType.EXACT;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {langTargetSearchParam, relatedToKeywordSearchParam};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request mobile search parameter.
    /// </summary>
    [Test]
    public void TestGetMobileSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      DeviceTypeSearchParameter mobileSearchParameter = new DeviceTypeSearchParameter();
      mobileSearchParameter.deviceType = DeviceType.MOBILE_WITH_FULL_BROWSER;

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "movie theater";
      keyword.matchType = KeywordMatchType.EXACT;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {mobileSearchParameter, relatedToKeywordSearchParam};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request placement type search parameter.
    /// </summary>
    [Test]
    public void TestGetPlacementTypeSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      PlacementTypeSearchParameter placementTypeSearchParam = new PlacementTypeSearchParameter();
      placementTypeSearchParam.placementTypes = new SiteConstantsPlacementType[] {
          SiteConstantsPlacementType.VIDEO, SiteConstantsPlacementType.GAME};

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "iron man";
      keyword.matchType = KeywordMatchType.EXACT;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {placementTypeSearchParam, relatedToKeywordSearchParam};
      selector.ideaType = IdeaType.PLACEMENT;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request related to keyword search parameter.
    /// </summary>
    [Test]
    public void TestGetRelatedToKeywordSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "flowers";
      keyword.matchType = KeywordMatchType.EXACT;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      selector.searchParameters =
          new SearchParameter[] {relatedToKeywordSearchParam};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request related to url search parameter.
    /// </summary>
    [Test]
    public void TestGetRelatedToUrlSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      RelatedToUrlSearchParameter relatedToUrlSearchParameter =
          new RelatedToUrlSearchParameter();
      relatedToUrlSearchParameter.urls = new string[] {"http://finance.google.com"};
      relatedToUrlSearchParameter.includeSubUrls = false;

      selector.searchParameters =
          new SearchParameter[] {relatedToUrlSearchParameter};
      selector.ideaType = IdeaType.KEYWORD;

      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request seed ad group id search parameter.
    /// </summary>
    [Test]
    public void TestGetSeedAdGroupIdSearchParameter() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      SeedAdGroupIdSearchParameter seedAdGroupIdSearchParameter =
          new SeedAdGroupIdSearchParameter();
      seedAdGroupIdSearchParameter.adGroupId = adGroupId;

      selector.searchParameters =
          new SearchParameter[] {seedAdGroupIdSearchParameter};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.get(selector) is TargetingIdeaPage);
    }

    /// <summary>
    /// Test whether we can request bulk keyword ideas.
    /// </summary>
    [Test]
    public void TestGetBulkKeywordIdeas() {
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      RelatedToKeywordSearchParameter relatedToKeywordSearchParam =
          new RelatedToKeywordSearchParameter();

      Keyword keyword = new Keyword();
      keyword.text = "presidential vote";
      keyword.matchType = KeywordMatchType.EXACT;

      relatedToKeywordSearchParam.keywords = new Keyword[] {keyword};

      RelatedToUrlSearchParameter relatedToUrlSearchParameter =
          new RelatedToUrlSearchParameter();
      relatedToUrlSearchParameter.urls = new string[] {"http://finance.google.com"};
      relatedToUrlSearchParameter.includeSubUrls = false;

      selector.searchParameters =
          new SearchParameter[] {relatedToKeywordSearchParam, relatedToUrlSearchParameter};
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestType = RequestType.IDEAS;

      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = 1;

      Assert.That(targetingIdeaService.getBulkKeywordIdeas(selector) is TargetingIdeaPage);
    }
  }
}

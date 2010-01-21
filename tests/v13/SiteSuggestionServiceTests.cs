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
  /// Unittest for SiteSuggestionService.
  /// </summary>
  [TestFixture]
  class SiteSuggestionServiceTests : BaseTests {
    /// <summary>
    /// SiteSuggestionService object to be used in this test.
    /// </summary>
    SiteSuggestionService siteSuggestionService;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public SiteSuggestionServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      AdWordsUser user = new AdWordsUser();
      siteSuggestionService =
          (SiteSuggestionService) user.GetService(AdWordsService.v13.SiteSuggestionService);
    }

    /// <summary>
    /// Test whether we can fetch sites by category name.
    /// </summary>
    [Test]
    public void TestGetSitesByCategoryName() {
      LanguageGeoTargeting targeting = new LanguageGeoTargeting();
      targeting.countries = new string[] {"US"};
      targeting.languages = new string[] {"en"};
      targeting.metros = new string[] {"501"};
      targeting.regions = new string[] {"US-NY"};
      Assert.That(siteSuggestionService.getSitesByCategoryName("Software", targeting)
          is SiteSuggestion[]);
    }

    /// <summary>
    /// Test whether we can fetch sites by demographics.
    /// </summary>
    [Test]
    public void TestGetSitesByDemographics() {
      DemographicsTarget demo = new DemographicsTarget();
      demo.childrenTargetSpecified = true;
      demo.childrenTarget = ChildrenTarget.HouseholdsWithChildrenOnly;
      demo.ethnicityTargetSpecified = true;
      demo.ethnicityTarget = EthnicityTarget.NoPreference;
      demo.genderTargetSpecified = true;
      demo.genderTarget = GenderTarget.NoPreference;
      demo.maxAgeRangeSpecified = true;
      demo.maxAgeRange = AgeRange.Range35To44;
      demo.maxHouseholdIncomeRangeSpecified = true;
      demo.maxHouseholdIncomeRange = HouseholdIncomeRange.Range40000To59999;
      demo.minAgeRangeSpecified = true;
      demo.minAgeRange = AgeRange.Range18To24;
      demo.minHouseholdIncomeRangeSpecified = true;
      demo.minHouseholdIncomeRange = HouseholdIncomeRange.Range15000To24999;

      LanguageGeoTargeting targeting = new LanguageGeoTargeting();
      targeting.countries = new string[] {"US"};
      targeting.languages = new string[] {"en"};
      targeting.metros = new string[] {"501"};
      targeting.regions = new string[] {"US-NY"};


      Assert.That(siteSuggestionService.getSitesByDemographics(demo, targeting)
          is SiteSuggestion[]);
    }

    /// <summary>
    /// Test whether we can fetch sites by topics.
    /// </summary>
    [Test]
    public void TestGetSitesByTopics() {
      LanguageGeoTargeting targeting = new LanguageGeoTargeting();
      targeting.countries = new string[] {"US"};
      targeting.languages = new string[] {"en"};
      targeting.metros = new string[] {"501"};
      targeting.regions = new string[] {"US-NY"};
      Assert.That(siteSuggestionService.getSitesByTopics(new string[] {"Flowers"}, targeting)
          is SiteSuggestion[]);
    }

    /// <summary>
    /// Test whether we can fetch sites by URLs.
    /// </summary>
    [Test]
    public void TestGetSitesByUrls() {
      LanguageGeoTargeting targeting = new LanguageGeoTargeting();
      targeting.countries = new string[] {"US"};
      targeting.languages = new string[] {"en"};
      targeting.metros = new string[] {"501"};
      targeting.regions = new string[] {"US-NY"};
      Assert.That(siteSuggestionService.getSitesByUrls(new string[] {"www.google.com"}, targeting)
          is SiteSuggestion[]);
    }
  }
}

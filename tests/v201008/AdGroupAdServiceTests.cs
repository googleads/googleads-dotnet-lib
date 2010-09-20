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
using com.google.api.adwords.lib.util;
using com.google.api.adwords.v201008;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tests.v201008 {
  /// <summary>
  /// UnitTests for <see cref="AdGroupAdService"/> class.
  /// </summary>
  [TestFixture]
  class AdGroupAdServiceTests : BaseTests {
    /// <summary>
    /// AdGroupAdService object to be used in this test.
    /// </summary>
    private AdGroupAdService adGroupAdService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    private long adGroupId = 0;

    /// <summary>
    /// Ad Id for which tests are run.
    /// </summary>
    private long adId = 0;
    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdGroupAdServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      adGroupAdService = (AdGroupAdService)user.GetService(AdWordsService.v201008.AdGroupAdService);
      campaignId = utils.CreateCampaign(user, true);
      adGroupId = utils.CreateAdGroup(user, campaignId);
      adId = utils.CreateTextAd(user, adGroupId, false);
    }

    /// <summary>
    /// Test whether we can add a text ad.
    /// </summary>
    [Test]
    public void TestAddTextAd() {
      // Create your text ad.
      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!";
      textAd.displayUrl = "www.example.com";
      textAd.url = "http://www.example.com";

      AdGroupAd textadGroupAd = new AdGroupAd();
      textadGroupAd.adGroupId = adGroupId;
      textadGroupAd.adGroupIdSpecified = true;
      textadGroupAd.ad = textAd;

      AdGroupAdOperation textAdOperation = new AdGroupAdOperation();
      textAdOperation.operatorSpecified = true;
      textAdOperation.@operator = Operator.ADD;
      textAdOperation.operand = textadGroupAd;

      AdGroupAdReturnValue result = adGroupAdService.mutate(
          new AdGroupAdOperation[] {textAdOperation});

      Assert.NotNull(result);
      Assert.NotNull(result.value);
      Assert.AreEqual(result.value.Length, 1);
      Assert.NotNull(result.value[0]);
      Assert.NotNull(result.value[0].ad);
      Assert.That(result.value[0].ad is TextAd);
    }

    /// <summary>
    /// Test whether we can add a text ad with exemption requests.
    /// </summary>
    [Test]
    public void TestAddTextAdWithExemptionRequests() {
      // Create your text ad.
      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!!!";
      textAd.displayUrl = "www.example.com";
      textAd.url = "http://www.example.com";

      AdGroupAd textadGroupAd = new AdGroupAd();
      textadGroupAd.adGroupId = adGroupId;
      textadGroupAd.adGroupIdSpecified = true;
      textadGroupAd.ad = textAd;

      AdGroupAdOperation textAdOperation = new AdGroupAdOperation();
      textAdOperation.operatorSpecified = true;
      textAdOperation.@operator = Operator.ADD;
      textAdOperation.operand = textadGroupAd;

      ExemptionRequest exemptionRequest1 = new ExemptionRequest();
      exemptionRequest1.key = new PolicyViolationKey();
      exemptionRequest1.key.policyName =  "nonstandard_punctuation";
      exemptionRequest1.key.violatingText =  "everyone!!!";

      ExemptionRequest exemptionRequest2 = new ExemptionRequest();
      exemptionRequest2.key = new PolicyViolationKey();
      exemptionRequest2.key.policyName =  "nonstandard_punctuation";
      exemptionRequest2.key.violatingText =  "everyone!!";

      textAdOperation.exemptionRequests =
          new ExemptionRequest[] {exemptionRequest1, exemptionRequest2};

      AdGroupAdReturnValue result = adGroupAdService.mutate(
          new AdGroupAdOperation[] {textAdOperation});

      Assert.NotNull(result);
      Assert.NotNull(result.value);
      Assert.AreEqual(result.value.Length, 1);
      Assert.NotNull(result.value[0]);
      Assert.NotNull(result.value[0].ad);
      Assert.That(result.value[0].ad is TextAd);
    }

    /// <summary>
    /// Test whether we can add an image ad.
    /// </summary>
    [Test]
    public void TestAddImageAd() {
      // Create your image ad.
      ImageAd imageAd = new ImageAd();
      imageAd.name = "My Image Ad";
      imageAd.displayUrl = "www.example.com";
      imageAd.url = "http://www.example.com";

      imageAd.image = new Image();
      imageAd.image.data = new TestUtils().GetSandboxImage();

      // Set the AdGroup Id.
      AdGroupAd imageAdGroupAd = new AdGroupAd();
      imageAdGroupAd.adGroupId = adGroupId;
      imageAdGroupAd.adGroupIdSpecified = true;
      imageAdGroupAd.ad = imageAd;

      // Create the ADD Operation.
      AdGroupAdOperation imageAdOperation = new AdGroupAdOperation();
      imageAdOperation.operatorSpecified = true;
      imageAdOperation.@operator = Operator.ADD;
      imageAdOperation.operand = imageAdGroupAd;

      AdGroupAdReturnValue result = adGroupAdService.mutate(
          new AdGroupAdOperation[] {imageAdOperation});

      Assert.NotNull(result);
      Assert.NotNull(result.value);
      Assert.AreEqual(result.value.Length, 1);
      Assert.NotNull(result.value[0]);
      Assert.NotNull(result.value[0].ad);
      Assert.That(result.value[0].ad is ImageAd);
    }

    /// <summary>
    /// Test whether we can add a mobile image ad.
    /// </summary>
    [Test]
    public void TestAddMobileImageAd() {
      // Create your mobile image ad.
      MobileImageAd mobileImageId = new MobileImageAd();
      mobileImageId.url = "http://www.example.com";

      // Maximum length of display url is 20 characters.
      mobileImageId.displayUrl = "www.example.com";

      // Ads should be displayed on carriers supporting HTML and XHTML browsers.
      mobileImageId.markupLanguages =
          new MarkupLanguageType[] {MarkupLanguageType.HTML, MarkupLanguageType.XHTML};

      // Use all the available carriers. For possible values, see
      // http://code.google.com/apis/adwords/docs/developer/MobileImageAd.html
      mobileImageId.mobileCarriers = new string[] {"ALLCARRIERS"};

      mobileImageId.image = new Image();
      mobileImageId.image.data = MediaUtilities.GetAssetDataFromUrl(
          "http://adwords.google.com/select/images/samples/mobile300-50.gif");

      // Set the AdGroup Id.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroupId;
      adGroupAd.adGroupIdSpecified = true;
      adGroupAd.ad = mobileImageId;

      // Create the ADD Operation.
      AdGroupAdOperation mobileAdOperation = new AdGroupAdOperation();
      mobileAdOperation.operatorSpecified = true;
      mobileAdOperation.@operator = Operator.ADD;
      mobileAdOperation.operand = adGroupAd;

      AdGroupAdReturnValue result = adGroupAdService.mutate(
          new AdGroupAdOperation[] {mobileAdOperation});

      Assert.NotNull(result);
      Assert.NotNull(result.value);
      Assert.AreEqual(result.value.Length, 1);
      Assert.NotNull(result.value[0]);
      Assert.NotNull(result.value[0].ad);
      Assert.That(result.value[0].ad is MobileImageAd);
    }

    /// <summary>
    /// Test whether we can fetch all ads from given campaign.
    /// </summary>
    [Test]
    public void TestGetAllAdsFromCampaign() {
      // Create at least one text ad.
      new TestUtils().CreateTextAd(user, adGroupId, false);
      // Create a selector and set the filters.
      AdGroupAdSelector selector = new AdGroupAdSelector();
      selector.campaignIds = new long[] {campaignId};

      AdGroupAdPage page = adGroupAdService.get(selector);
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
      foreach (AdGroupAd adGroupAd in page.entries) {
        Assert.NotNull(adGroupAd);
        Assert.NotNull(adGroupAd.ad);
        Assert.NotNull(adGroupAd);
      }
    }

    /// <summary>
    /// Test whether we can fetch an ad by id.
    /// </summary>
    [Test]
    public void TestGetAd() {
      // Create a selector and set the filters.
      AdGroupAdSelector selector = new AdGroupAdSelector();
      selector.adIds = new long[] {adId};
      selector.adGroupIds = new long [] {adGroupId};

      AdGroupAdPage page = adGroupAdService.get(selector);
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
      Assert.AreEqual(page.entries.Length, 1);
      Assert.NotNull(page.entries[0]);
      Assert.AreEqual(page.entries[0].adGroupId, adGroupId);
      Assert.NotNull(page.entries[0].ad);
      Assert.AreEqual(page.entries[0].ad.id, adId);
      Assert.That(page.entries[0].ad is TextAd);
    }

    /// <summary>
    /// Test whether we can update an ad.
    /// </summary>
    [Test]
    public void TestUpdateAd() {
      // Update your Ad.
      AdGroupAd adGroupAd = new AdGroupAd();

      adGroupAd.statusSpecified = true;
      adGroupAd.status = AdGroupAdStatus.DISABLED;

      adGroupAd.adGroupId = adGroupId;
      adGroupAd.adGroupIdSpecified = true;

      adGroupAd.ad = new Ad();
      adGroupAd.ad.id = adId;
      adGroupAd.ad.idSpecified = true;

      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.operatorSpecified = true;
      adGroupAdOperation.@operator = Operator.SET;
      adGroupAdOperation.operand = adGroupAd;

      AdGroupAdReturnValue result = adGroupAdService.mutate(
          new AdGroupAdOperation[]{adGroupAdOperation});

      Assert.NotNull(result);
      Assert.NotNull(result.value);
      Assert.AreEqual(result.value.Length, 1);
      Assert.NotNull(result.value[0]);
      Assert.AreEqual(result.value[0].adGroupId, adGroupId);
      Assert.NotNull(result.value[0].ad);
      Assert.AreEqual(result.value[0].ad.id, adId);
      Assert.That(result.value[0].ad is TextAd);
      Assert.AreEqual(result.value[0].status, AdGroupAdStatus.DISABLED);
    }

    /// <summary>
    /// Test if we can delete an ad.
    /// </summary>
    [Test]
    public void TestDeleteAd() {
      // Create base class ad to avoid setting type specific fields.
      Ad ad = new Ad();
      ad.id = adId;
      ad.idSpecified = true;

      // Create ad group ad.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroupId;
      adGroupAd.adGroupIdSpecified = true;

      adGroupAd.ad = ad;

      // Create operations.
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.operand = adGroupAd;
      operation.operatorSpecified = true;
      operation.@operator = Operator.REMOVE;

      AdGroupAdReturnValue result = null;
      Assert.DoesNotThrow(delegate() {
        result = adGroupAdService.mutate(new AdGroupAdOperation[] {operation});
      });
      Assert.NotNull(result);
      Assert.NotNull(result.value);
      Assert.AreEqual(result.value.Length, 1);
      Assert.NotNull(result.value[0]);
      Assert.AreEqual(result.value[0].adGroupId, adGroupId);
      Assert.NotNull(result.value[0].ad);
      Assert.AreEqual(result.value[0].ad.id, adId);
    }
  }
}

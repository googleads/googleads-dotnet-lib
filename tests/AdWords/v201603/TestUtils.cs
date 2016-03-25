// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;
using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201603 {

  /// <summary>
  /// A utility class to assist the testing of v201603 services.
  /// </summary>
  class TestUtils {

    /// <summary>
    /// The polling interval base to be used for exponential backoff.
    /// </summary>
    private const int POLL_INTERVAL_SECONDS_BASE = 30;

    /// <summary>
    /// The maximum number of retries.
    /// </summary>
    private const long MAX_RETRIES = 5;

    public long CreateBudget(AdWordsUser user) {
      BudgetService budgetService =
          (BudgetService) user.GetService(AdWordsService.v201603.BudgetService);

      // Create the campaign budget.
      Budget budget = new Budget();
      budget.name = "Interplanetary Cruise Budget #" + DateTime.Now.ToString(
          "yyyy-MM-dd HH:mm:ss.ffffff");
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget.amount = new Money();
      budget.amount.microAmount = 500000;

      BudgetOperation budgetOperation = new BudgetOperation();
      budgetOperation.@operator = Operator.ADD;
      budgetOperation.operand = budget;

      BudgetReturnValue budgetRetval = budgetService.mutate(new BudgetOperation[] { budgetOperation });
      return budgetRetval.value[0].budgetId;
    }

    /// <summary>
    /// Creates a test search campaign for running further tests.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="biddingStrategy">The bidding strategy to be used.</param>
    /// <returns>The campaign id.</returns>
    public long CreateMobileSearchCampaign(AdWordsUser user, BiddingStrategyType strategyType) {
      return CreateCampaign(user, AdvertisingChannelType.SEARCH, strategyType, true);
    }

    /// <summary>
    /// Creates a test search campaign for running further tests.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="biddingStrategy">The bidding strategy to be used.</param>
    /// <returns>The campaign id.</returns>
    public long CreateSearchCampaign(AdWordsUser user, BiddingStrategyType strategyType) {
      return CreateCampaign(user, AdvertisingChannelType.SEARCH, strategyType);
    }

    /// <summary>
    /// Creates a display campaign for running further tests.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="biddingStrategy">The bidding strategy to be used.</param>
    /// <returns>The campaign id.</returns>
    public long CreateDisplayCampaign(AdWordsUser user, BiddingStrategyType strategyType) {
      return CreateCampaign(user, AdvertisingChannelType.DISPLAY, strategyType);
    }

    /// <summary>
    /// Creates a test shopping campaign for running further tests.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="biddingStrategy">The bidding strategy to be used.</param>
    /// <returns>The campaign id.</returns>
    public long CreateShoppingCampaign(AdWordsUser user, BiddingStrategyType strategyType) {
      return CreateCampaign(user, AdvertisingChannelType.SHOPPING, strategyType);
    }

    /// <summary>
    /// Creates a test campaign for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="channelType">The advertising channel type for this
    /// campaign.</param>
    /// <param param name="strategyType">The bidding strategy to be used for
    /// this campaign.</param>
    /// <returns>The campaign id.</returns>
    public long CreateCampaign(AdWordsUser user, AdvertisingChannelType channelType,
        BiddingStrategyType strategyType) {
      return CreateCampaign(user, channelType, strategyType, false);
    }

    /// <summary>
    /// Creates a test campaign for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="channelType">The advertising channel type for this
    /// campaign.</param>
    /// <param param name="strategyType">The bidding strategy to be used for
    /// this campaign.</param>
    /// <param name="isMobile">True, if this campaign is mobile-only, false
    /// otherwise.</param>
    /// <returns>The campaign id.</returns>
    public long CreateCampaign(AdWordsUser user, AdvertisingChannelType channelType,
        BiddingStrategyType strategyType, bool isMobile) {
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201603.CampaignService);

      Campaign campaign = new Campaign() {
        name = string.Format("Campaign {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")),
        advertisingChannelType = channelType,
        status = CampaignStatus.PAUSED,
        biddingStrategyConfiguration = new BiddingStrategyConfiguration() {
          biddingStrategyType = strategyType
        },
        budget = new Budget() {
          budgetId = CreateBudget(user),
          amount = new Money() {
            microAmount = 100000000,
          },
          deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
        }
      };

      if (isMobile) {
        switch (campaign.advertisingChannelType) {
          case AdvertisingChannelType.SEARCH:
            campaign.advertisingChannelSubType = AdvertisingChannelSubType.SEARCH_MOBILE_APP;
            break;

          case AdvertisingChannelType.DISPLAY:
            campaign.advertisingChannelSubType = AdvertisingChannelSubType.DISPLAY_MOBILE_APP;
            break;
        }
      }

      List<Setting> settings = new List<Setting>();

      if (channelType == AdvertisingChannelType.SHOPPING) {
        // All Shopping campaigns need a ShoppingSetting.
        ShoppingSetting shoppingSetting = new ShoppingSetting() {
          salesCountry = "US",
          campaignPriority = 0,
          merchantId = (user.Config as AdWordsAppConfig).MerchantCenterId
        };
        settings.Add(shoppingSetting);
      }
      campaign.settings = settings.ToArray();

      CampaignOperation campaignOperation = new CampaignOperation() {
        @operator = Operator.ADD,
        operand = campaign
      };

      CampaignReturnValue retVal =
          campaignService.mutate(new CampaignOperation[] { campaignOperation });
      return retVal.value[0].id;
    }

    /// <summary>
    /// Creates a test adgroup for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
    /// <returns>The adgroup id.</returns>
    public long CreateAdGroup(AdWordsUser user, long campaignId) {
      return CreateAdGroup(user, campaignId, false);
    }

    /// <summary>
    /// Creates a test adgroup for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
    /// <param name="isCpmBid">True, if a ManualCPM bid is to be used.</param>
    /// <returns>The adgroup id.</returns>
    public long CreateAdGroup(AdWordsUser user, long campaignId, bool isCpmBid) {
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201603.AdGroupService);

      AdGroupOperation adGroupOperation = new AdGroupOperation();
      adGroupOperation.@operator = Operator.ADD;
      adGroupOperation.operand = new AdGroup();
      adGroupOperation.operand.campaignId = campaignId;
      adGroupOperation.operand.name =
          string.Format("AdGroup {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
      adGroupOperation.operand.status = AdGroupStatus.ENABLED;

      if (isCpmBid) {
        BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();
        CpmBid cpmBid = new CpmBid();
        cpmBid.bid = new Money();
        cpmBid.bid.microAmount = 10000000;
        biddingConfig.bids = new Bids[] { cpmBid };
        adGroupOperation.operand.biddingStrategyConfiguration = biddingConfig;
      } else {
        BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();
        CpcBid cpcBid = new CpcBid();
        cpcBid.bid = new Money();
        cpcBid.bid.microAmount = 10000000;
        biddingConfig.bids = new Bids[] { cpcBid };
        adGroupOperation.operand.biddingStrategyConfiguration = biddingConfig;
      }
      AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[] { adGroupOperation });
      return retVal.value[0].id;
    }

    /// <summary>
    /// Creates a test textad for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id for which the ad is created.
    /// </param>
    /// <param name="hasAdParam">True, if an ad param placeholder should be
    /// added.</param>
    /// <returns>The text ad id.</returns>
    public long CreateTextAd(AdWordsUser user, long adGroupId, bool hasAdParam) {
      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v201603.AdGroupAdService);
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = new AdGroupAd();
      adGroupAdOperation.operand.adGroupId = adGroupId;
      TextAd ad = new TextAd();

      ad.headline = "Luxury Cruise to Mars";
      ad.description1 = "Visit the Red Planet in style.";
      if (hasAdParam) {
        ad.description2 = "Low-gravity fun for {param1:cheap}!";
      } else {
        ad.description2 = "Low-gravity fun for everyone!";
      }
      ad.displayUrl = "example.com";
      ad.finalUrls = new string[] { "http://www.example.com" };

      adGroupAdOperation.operand.ad = ad;

      AdGroupAdReturnValue retVal =
          adGroupAdService.mutate(new AdGroupAdOperation[] { adGroupAdOperation });
      return retVal.value[0].ad.id;
    }

    /// <summary>
    /// Creates a test draft for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="baseCampaignId">The base campaign ID for the draft.</param>
    /// <returns>The draft ID.</returns>
    public long AddDraft(AdWordsUser user, long baseCampaignId) {
      // Get the DraftService.
      DraftService draftService = (DraftService) user.GetService(
        AdWordsService.v201603.DraftService);
      Draft draft = new Draft() {
        baseCampaignId = baseCampaignId,
        draftName = "Test Draft #" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")
      };

      DraftOperation draftOperation = new DraftOperation() {
        @operator = Operator.ADD,
        operand = draft
      };

      draft = draftService.mutate(new DraftOperation[] {draftOperation}).value[0];
      return draft.draftId;
    }

    /// <summary>
    /// Creates a test trial for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="baseCampaignId">The base campaign ID for the draft.</param>
    /// <param name="draftId">ID of the draft to use when creating trial.</param>
    /// <returns>The trial ID.</returns>
    public long CreateTrial(AdWordsUser user, long draftId, long baseCampaignId) {
      // Get the TrialService.
      TrialService trialService = (TrialService) user.GetService(
        AdWordsService.v201603.TrialService);

      Trial trial = new Trial() {
        draftId = draftId,
        baseCampaignId = baseCampaignId,
        name = "Test Trial #" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
        trafficSplitPercent = 50
      };

      TrialOperation trialOperation = new TrialOperation() {
        @operator = Operator.ADD,
        operand = trial
      };

      long trialId = trialService.mutate(new TrialOperation[] { trialOperation }).value[0].id;

      // Since creating a trial is asynchronous, we have to poll it to wait
      // for it to finish.
      Selector trialSelector = new Selector() {
        fields = new string[] {
          Trial.Fields.Id, Trial.Fields.Status, Trial.Fields.BaseCampaignId,
          Trial.Fields.TrialCampaignId
        },
        predicates = new Predicate[] {
          Predicate.Equals(Trial.Fields.Id, trialId)
        }
      };

      trial = null;
      bool isPending = true;
      int pollAttempts = 0;

      do {
        int sleepMillis = (int) Math.Pow(2, pollAttempts) *
          POLL_INTERVAL_SECONDS_BASE * 1000;
        Console.WriteLine("Sleeping {0} millis...", sleepMillis);
        Thread.Sleep(sleepMillis);

        trial = trialService.get(trialSelector).entries[0];

        Console.WriteLine("Trial ID {0} has status '{1}'.", trial.id, trial.status);
        pollAttempts++;
        isPending = (trial.status == TrialStatus.CREATING);
      } while (isPending && pollAttempts <= MAX_RETRIES);

      if (trial.status == TrialStatus.ACTIVE) {
        return trial.id;
      } else {
        throw new System.ApplicationException ("Failed to create an active trial for testing.");
      }
    }

    /// <summary>
    /// Creates a test ThirdPartyRedirectAd for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id for which the ad is created.
    /// </param>
    /// <param name="hasAdParam">True, if an ad param placeholder should be
    /// added.</param>
    /// <returns>The text ad id.</returns>
    public long CreateThirdPartyRedirectAd(AdWordsUser user, long adGroupId) {
      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v201603.AdGroupAdService);
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = new AdGroupAd();
      adGroupAdOperation.operand.adGroupId = adGroupId;

      // Create the third party redirect ad.
      ThirdPartyRedirectAd redirectAd = new ThirdPartyRedirectAd();
      redirectAd.name = string.Format("Example third party ad #{0}", this.GetTimeStamp());
      redirectAd.url = "http://www.example.com";

      redirectAd.dimensions = new Dimensions();
      redirectAd.dimensions.height = 250;
      redirectAd.dimensions.width = 300;

      // This field normally contains the javascript ad tag.
      redirectAd.snippet =
          "<img src=\"http://www.google.com/intl/en/adwords/select/images/samples/inline.jpg\"/>";
      redirectAd.impressionBeaconUrl = "http://www.examples.com/beacon";
      redirectAd.certifiedVendorFormatId = 119;
      redirectAd.isCookieTargeted = false;
      redirectAd.isUserInterestTargeted = false;
      redirectAd.isTagged = false;

      adGroupAdOperation.operand.ad = redirectAd;

      AdGroupAdReturnValue retVal =
          adGroupAdService.mutate(new AdGroupAdOperation[] { adGroupAdOperation });
      return retVal.value[0].ad.id;
    }

    /// <summary>
    /// Sets an adparam for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id to which criterionId belongs.
    /// </param>
    /// <param name="criterionId">The criterion id to which adparam is set.
    /// </param>
    public void SetAdParam(AdWordsUser user, long adGroupId, long criterionId) {
      AdParamService adParamService =
          (AdParamService) user.GetService(AdWordsService.v201603.AdParamService);

      // Prepare for setting ad parameters.
      AdParam adParam = new AdParam();
      adParam.adGroupId = adGroupId;
      adParam.criterionId = criterionId;
      adParam.paramIndex = 1;
      adParam.insertionText = "$100";

      AdParamOperation adParamOperation = new AdParamOperation();
      adParamOperation.@operator = Operator.SET;
      adParamOperation.operand = adParam;

      // Set ad parameters.
      AdParam[] newAdParams = adParamService.mutate(new AdParamOperation[] { adParamOperation });
      return;
    }

    /// <summary>
    /// Creates a keyword for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id for which the keyword is
    /// created.</param>
    /// <returns>The keyword id.</returns>
    public long CreateKeyword(AdWordsUser user, long adGroupId) {
      AdGroupCriterionService adGroupCriterionService =
         (AdGroupCriterionService) user.GetService(AdWordsService.v201603.AdGroupCriterionService);

      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.ADD;
      operation.operand = new BiddableAdGroupCriterion();
      operation.operand.adGroupId = adGroupId;

      Keyword keyword = new Keyword();
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.text = "mars cruise";

      operation.operand.criterion = keyword;
      AdGroupCriterionReturnValue retVal =
          adGroupCriterionService.mutate(new AdGroupCriterionOperation[] { operation });
      return retVal.value[0].criterion.id;
    }

    /// <summary>
    /// Creates the placement.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id for which the placement is
    /// created.</param>
    /// <returns>The placement id.</returns>
    public long CreatePlacement(AdWordsUser user, long adGroupId) {
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201603.AdGroupCriterionService);

      Placement placement = new Placement();
      placement.url = "http://mars.google.com";

      AdGroupCriterion placementCriterion = new BiddableAdGroupCriterion();
      placementCriterion.adGroupId = adGroupId;
      placementCriterion.criterion = placement;

      AdGroupCriterionOperation placementOperation = new AdGroupCriterionOperation();
      placementOperation.@operator = Operator.ADD;
      placementOperation.operand = placementCriterion;

      AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(
          new AdGroupCriterionOperation[] { placementOperation });

      return retVal.value[0].criterion.id;
    }

    /// <summary>
    /// Adds an experiment.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="adGroupId">The ad group id.</param>
    /// <param name="criterionId">The criterion id.</param>
    /// <returns>The experiment id.</returns>
    public long AddExperiment(AdWordsUser user, long campaignId, long adGroupId, long criterionId) {
      // Get the ExperimentService.
      ExperimentService experimentService =
          (ExperimentService) user.GetService(AdWordsService.v201603.ExperimentService);

      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201603.AdGroupService);

      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201603.AdGroupCriterionService);

      // Create the experiment.
      Experiment experiment = new Experiment();
      experiment.campaignId = campaignId;
      experiment.name = "Interplanetary Cruise #" + GetTimeStamp();
      experiment.queryPercentage = 10;
      experiment.startDateTime = DateTime.Now.AddDays(1).ToString("yyyyMMdd HHmmss");

      // Create the operation.
      ExperimentOperation experimentOperation = new ExperimentOperation();
      experimentOperation.@operator = Operator.ADD;
      experimentOperation.operand = experiment;

      // Add the experiment.
      ExperimentReturnValue experimentRetVal = experimentService.mutate(
          new ExperimentOperation[] { experimentOperation });

      return experimentRetVal.value[0].id;
    }

    /// <summary>
    /// Adds the campaign targeting criteria to a campaign.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="campaignId">The campaign id.</param>
    /// <returns>The campaign criteria id.</returns>
    public long AddCampaignTargetingCriteria(AdWordsUser user, long campaignId) {
      // Get the CampaignCriterionService.
      CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService) user.GetService(
              AdWordsService.v201603.CampaignCriterionService);

      // Create language criteria.
      // See http://code.google.com/apis/adwords/docs/appendix/languagecodes.html
      // for a detailed list of language codes.
      Language language1 = new Language();
      language1.id = 1002; // French
      CampaignCriterion languageCriterion1 = new CampaignCriterion();
      languageCriterion1.campaignId = campaignId;
      languageCriterion1.criterion = language1;

      CampaignCriterion[] criteria = new CampaignCriterion[] { languageCriterion1 };

      List<CampaignCriterionOperation> operations = new List<CampaignCriterionOperation>();

      foreach (CampaignCriterion criterion in criteria) {
        CampaignCriterionOperation operation = new CampaignCriterionOperation();
        operation.@operator = Operator.ADD;
        operation.operand = criterion;
        operations.Add(operation);
      }

      CampaignCriterionReturnValue retVal = campaignCriterionService.mutate(operations.ToArray());
      return retVal.value[0].criterion.id;
    }

    /// <summary>
    /// Returns an image which can be used for creating image ads.
    /// </summary>
    /// <returns>The image data, as an array of bytes.</returns>
    public byte[] GetTestImage() {
      return MediaUtilities.GetAssetDataFromUrl("http://goo.gl/HJM3L");
    }

    /// <summary>
    /// Creates the shared keyword set.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <returns>A shared keyword set.</returns>
    public long CreateSharedKeywordSet(AdWordsUser user) {
      // Get the SharedSetService.
      SharedSetService sharedSetService = (SharedSetService)
          user.GetService(AdWordsService.v201603.SharedSetService);

      SharedSetOperation operation = new SharedSetOperation();
      operation.@operator = Operator.ADD;
      SharedSet sharedSet = new SharedSet();
      sharedSet.name = "API Negative keyword list - " + GetTimeStampAlpha();
      sharedSet.type = SharedSetType.NEGATIVE_KEYWORDS;
      operation.operand = sharedSet;

      SharedSetReturnValue retval = sharedSetService.mutate(
          new SharedSetOperation[] { operation });
      return retval.value[0].sharedSetId;
    }

    /// <summary>
    /// Attaches a shared set to a campaign.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="sharedSetId">The shared set id.</param>
    /// <returns>A CampaignSharedSet object that represents a binding between
    /// the specified campaign and the shared set.</returns>
    public void AttachSharedSetToCampaign(AdWordsUser user, long campaignId, long sharedSetId) {
      // Get the CampaignSharedSetService.
      CampaignSharedSetService campaignSharedSetService = (CampaignSharedSetService)
          user.GetService(AdWordsService.v201603.CampaignSharedSetService);

      CampaignSharedSet campaignSharedSet = new CampaignSharedSet();
      campaignSharedSet.campaignId = campaignId;
      campaignSharedSet.sharedSetId = sharedSetId;

      CampaignSharedSetOperation operation = new CampaignSharedSetOperation();
      operation.@operator = Operator.ADD;
      operation.operand = campaignSharedSet;

      campaignSharedSetService.mutate(new CampaignSharedSetOperation[] { operation });
    }

    /// <summary>
    /// Detaches the shared set from campaign.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="sharedSetId">The shared set identifier.</param>
    public void DetachSharedSetFromCampaign(AdWordsUser user, long campaignId, long sharedSetId) {
      // Get the CampaignSharedSetService.
      CampaignSharedSetService campaignSharedSetService = (CampaignSharedSetService)
          user.GetService(AdWordsService.v201603.CampaignSharedSetService);

      CampaignSharedSet campaignSharedSet = new CampaignSharedSet();
      campaignSharedSet.campaignId = campaignId;
      campaignSharedSet.sharedSetId = sharedSetId;

      CampaignSharedSetOperation operation = new CampaignSharedSetOperation();
      operation.@operator = Operator.REMOVE;
      operation.operand = campaignSharedSet;

      campaignSharedSetService.mutate(new CampaignSharedSetOperation[] { operation });
    }

    /// <summary>
    /// Deletes the shared set.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="sharedSetId">The shared set ID.</param>
    public void DeleteSharedSet(AdWordsUser user, long sharedSetId) {
      // Get the SharedSetService.
      SharedSetService sharedSetService = (SharedSetService)
          user.GetService(AdWordsService.v201603.SharedSetService);

      SharedSetOperation operation = new SharedSetOperation();
      operation.@operator = Operator.REMOVE;
      SharedSet sharedSet = new SharedSet();
      sharedSet.sharedSetId = sharedSetId;
      operation.operand = sharedSet;

      SharedSetReturnValue retval = sharedSetService.mutate(
          new SharedSetOperation[] { operation });
    }

    /// <summary>
    /// Gets the current timestamp.
    /// </summary>
    /// <returns>The timestamp as a string.</returns>
    public string GetTimeStamp() {
      return (DateTime.UtcNow - new DateTime(1970, 1, 1)).
          TotalMilliseconds.ToString();
    }

    /// <summary>
    /// Gets the current timestamp as an alphabetic string.
    /// </summary>
    /// <returns>The timestamp as a string.</returns>
    public string GetTimeStampAlpha() {
      string timeStamp = GetTimeStamp();
      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < timeStamp.Length; i++) {
        if (timeStamp[i] == '.') {
          continue;
        }
        builder.Append(Convert.ToChar('a' + int.Parse(timeStamp[i].ToString())));
      }
      return builder.ToString();
    }
  }
}
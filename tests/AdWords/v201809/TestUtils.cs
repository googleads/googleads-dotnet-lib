// Copyright 2018 Google LLC
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
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.AdWords.Tests.v201809
{
    /// <summary>
    /// A utility class to assist the testing of v201809 services.
    /// </summary>
    internal class TestUtils
    {
        /// <summary>
        /// The polling interval base to be used for exponential backoff.
        /// </summary>
        private const int POLL_INTERVAL_SECONDS_BASE = 30;

        /// <summary>
        /// The placeholder ID for GMB location extension feeds.
        /// </summary>
        private const int PLACEHOLDER_LOCATION = 7;

        /// <summary>
        /// The maximum number of retries.
        /// </summary>
        private const long MAX_RETRIES = 5;

        /// <summary>
        /// The default budget to be used when creating campaigns.
        /// </summary>
        private const long DEFAULT_BUDGET = 500000;

        public long CreateBudget(AdWordsUser user)
        {
            BudgetService budgetService =
                (BudgetService) user.GetService(AdWordsService.v201809.BudgetService);

            // Create the campaign budget.
            Budget budget = new Budget();
            budget.name = "Interplanetary Cruise Budget #" +
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
            budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
            budget.amount = new Money();
            budget.amount.microAmount = DEFAULT_BUDGET;
            budget.isExplicitlyShared = false;

            BudgetOperation budgetOperation = new BudgetOperation();
            budgetOperation.@operator = Operator.ADD;
            budgetOperation.operand = budget;

            BudgetReturnValue budgetRetval = budgetService.mutate(new BudgetOperation[]
            {
                budgetOperation
            });
            return budgetRetval.value[0].budgetId;
        }

        /// <summary>
        /// Deletes the enabled GMB feeds.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void DeleteEnabledGmbFeeds(AdWordsUser user)
        {
            FeedService feedService =
                (FeedService) user.GetService(AdWordsService.v201809.FeedService);

            List<Feed> feedsToDelete = new List<Feed>();
            string query =
                "Select Id, SystemFeedGenerationData, FeedStatus where FeedStatus=ENABLED";

            FeedPage page = feedService.query(query);

            for (int i = 0; i < page.entries.Length; i++)
            {
                Feed f = page.entries[i];
                PlacesLocationFeedData systemData =
                    (f.systemFeedGenerationData as PlacesLocationFeedData);
                if (systemData != null)
                {
                    feedsToDelete.Add(f);
                }
            }

            if (feedsToDelete.Count > 0)
            {
                List<FeedOperation> operations = new List<FeedOperation>();

                for (int i = 0; i < feedsToDelete.Count; i++)
                {
                    FeedOperation operation = new FeedOperation()
                    {
                        @operator = Operator.REMOVE,
                        operand = new Feed()
                        {
                            id = feedsToDelete[i].id
                        }
                    };
                    operations.Add(operation);
                }

                feedService.mutate(operations.ToArray());
            }

            return;
        }

        /// <summary>
        /// Deletes the active GMB customer feeds.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void DeleteEnabledGmbCustomerFeeds(AdWordsUser user)
        {
            CustomerFeedService customerFeedService =
                (CustomerFeedService) user.GetService(AdWordsService.v201809.CustomerFeedService);

            string query = "Select FeedId, PlaceholderTypes where PlaceholderTypes CONTAINS_ANY[" +
                PLACEHOLDER_LOCATION + "] AND Status = ENABLED";

            List<CustomerFeedOperation> operations = new List<CustomerFeedOperation>();

            CustomerFeedPage page = customerFeedService.query(query);

            if (page != null && page.entries != null)
            {
                for (int i = 0; i < page.entries.Length; i++)
                {
                    CustomerFeed customerFeed = page.entries[i];
                    CustomerFeedOperation operation = new CustomerFeedOperation()
                    {
                        @operator = Operator.REMOVE,
                        operand = customerFeed
                    };
                    operations.Add(operation);
                }

                customerFeedService.mutate(operations.ToArray());
            }
        }

        /// <summary>
        /// Creates a label for test purposes.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <returns>ID of the newly created label.</returns>
        public long CreateLabel(AdWordsUser user)
        {
            LabelService labelService =
                (LabelService) user.GetService(AdWordsService.v201809.LabelService);

            // Create the campaign budget.
            TextLabel label = new TextLabel()
            {
                name = "Interplanetary Cruise Label #" +
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
            };

            LabelOperation labelOperation = new LabelOperation()
            {
                @operator = Operator.ADD,
                operand = label
            };

            LabelReturnValue labelRetval = labelService.mutate(new LabelOperation[]
            {
                labelOperation
            });
            return labelRetval.value[0].id;
        }

        /// <summary>
        /// Creates a test search campaign for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="strategyType">The bidding strategy to be used.</param>
        /// <returns>The campaign id.</returns>
        public long CreateMobileSearchCampaign(AdWordsUser user, BiddingStrategyType strategyType)
        {
            return CreateCampaign(user, AdvertisingChannelType.SEARCH, strategyType, true, false,
                false);
        }

        /// <summary>
        /// Creates a test search campaign for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="strategyType">The bidding strategy to be used.</param>
        /// <returns>The campaign id.</returns>
        public long CreateSearchCampaign(AdWordsUser user, BiddingStrategyType strategyType)
        {
            return CreateCampaign(user, AdvertisingChannelType.SEARCH, strategyType);
        }

        /// <summary>
        /// Creates a display campaign for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="strategyType">The bidding strategy to be used.</param>
        /// <returns>The campaign id.</returns>
        public long CreateDisplayCampaign(AdWordsUser user, BiddingStrategyType strategyType)
        {
            return CreateCampaign(user, AdvertisingChannelType.DISPLAY, strategyType);
        }

        /// <summary>
        /// Creates a test shopping campaign for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="strategyType">The bidding strategy to be used.</param>
        /// <returns>The campaign id.</returns>
        public long CreateShoppingCampaign(AdWordsUser user, BiddingStrategyType strategyType)
        {
            return CreateCampaign(user, AdvertisingChannelType.SHOPPING, strategyType);
        }

        /// <summary>
        /// Creates a test shopping campaign for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public long CreateDSACampaign(AdWordsUser user)
        {
            return CreateCampaign(user, AdvertisingChannelType.SEARCH,
                BiddingStrategyType.MANUAL_CPC, false, true, false);
        }

        /// <summary>
        /// Creates a test campaign for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="channelType">The advertising channel type for this
        /// campaign.</param>
        /// <param name="strategyType">The bidding strategy to be used for
        /// this campaign.</param>
        /// <returns>The campaign id.</returns>
        private long CreateCampaign(AdWordsUser user, AdvertisingChannelType channelType,
            BiddingStrategyType strategyType)
        {
            return CreateCampaign(user, channelType, strategyType, false, false, false);
        }

        public long CreateGmailCampaign(AdWordsUser user)
        {
            return CreateCampaign(user, AdvertisingChannelType.DISPLAY,
                BiddingStrategyType.MANUAL_CPC, false, false, true);
        }

        /// <summary>
        /// Creates a test campaign for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="channelType">The advertising channel type for this
        /// campaign.</param>
        /// <param name="strategyType">The bidding strategy to be used for
        /// this campaign.</param>
        /// <param name="isMobile">True, if this campaign is mobile-only, false
        /// otherwise.</param>
        /// <param name="isDsa">True, if this campaign is for DSA, false
        /// otherwise.</param>
        /// <param name="isGmail">True, if this campaign is for GMail Ads, false
        /// otherwise.</param>
        /// <returns>The campaign id.</returns>
        private long CreateCampaign(AdWordsUser user, AdvertisingChannelType channelType,
            BiddingStrategyType strategyType, bool isMobile, bool isDsa, bool isGmail)
        {
            CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201809.CampaignService);

            Campaign campaign = new Campaign()
            {
                name = string.Format("Campaign {0}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")),
                advertisingChannelType = channelType,

                // Set the test campaign to PAUSED when creating it to prevent the ads from serving.
                status = CampaignStatus.PAUSED,

                biddingStrategyConfiguration = new BiddingStrategyConfiguration()
                {
                    biddingStrategyType = strategyType
                },
                budget = new Budget()
                {
                    budgetId = CreateBudget(user),
                    amount = new Money()
                    {
                        microAmount = 100000000,
                    },
                    deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
                }
            };

            // Campaign setups that cannot be inferred just from AdvertisingChannelType uses flags.
            List<Setting> settings = new List<Setting>();

            // The following flags are all mutually exclusive for the purpose of testing.
            if (isMobile)
            {
                switch (campaign.advertisingChannelType)
                {
                    case AdvertisingChannelType.SEARCH:
                        campaign.advertisingChannelSubType =
                            AdvertisingChannelSubType.SEARCH_MOBILE_APP;
                        break;

                    case AdvertisingChannelType.DISPLAY:
                        campaign.advertisingChannelSubType =
                            AdvertisingChannelSubType.DISPLAY_MOBILE_APP;
                        break;
                }
            }
            else if (isDsa)
            {
                // Required: Set the campaign's Dynamic Search Ads settings.
                DynamicSearchAdsSetting dynamicSearchAdsSetting = new DynamicSearchAdsSetting();

                // Required: Set the domain name and language.
                dynamicSearchAdsSetting.domainName = "example.com";
                dynamicSearchAdsSetting.languageCode = "en";
                settings.Add(dynamicSearchAdsSetting);
            }
            else if (isGmail)
            {
                campaign.advertisingChannelSubType = AdvertisingChannelSubType.DISPLAY_GMAIL_AD;
            }
            else if (channelType == AdvertisingChannelType.SHOPPING)
            {
                // All Shopping campaigns need a ShoppingSetting.
                ShoppingSetting shoppingSetting = new ShoppingSetting()
                {
                    salesCountry = "US",
                    campaignPriority = 0,
                    merchantId = (user.Config as AdWordsAppConfig).MerchantCenterId
                };
                settings.Add(shoppingSetting);
            }

            campaign.settings = settings.ToArray();

            CampaignOperation campaignOperation = new CampaignOperation()
            {
                @operator = Operator.ADD,
                operand = campaign
            };

            CampaignReturnValue retVal = campaignService.mutate(new CampaignOperation[]
            {
                campaignOperation
            });
            return retVal.value[0].id;
        }

        /// <summary>
        /// Creates a test adgroup for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
        /// <returns>The adgroup id.</returns>
        public long CreateAdGroup(AdWordsUser user, long campaignId)
        {
            return CreateAdGroup(user, campaignId, false);
        }

        /// <summary>
        /// Creates a test ad group for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
        /// <param name="adGroupType">The ad group type.</param>
        /// <returns>The ad group ID.</returns>
        public long CreateAdGroup(AdWordsUser user, long campaignId, AdGroupType adGroupType)
        {
            return CreateAdGroup(user, campaignId, adGroupType, false);
        }

        /// <summary>
        /// Creates a test ad group for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
        /// <param name="isCpmBid">True, if a ManualCPM bid is to be used.</param>
        /// <returns>The ad group ID.</returns>
        public long CreateAdGroup(AdWordsUser user, long campaignId, bool isCpmBid)
        {
            return CreateAdGroup(user, campaignId, AdGroupType.UNKNOWN, isCpmBid);
        }

        /// <summary>
        /// Creates a test ad group for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
        /// <param name="adGroupType">The ad group type.</param>
        /// <param name="isCpmBid">True, if a ManualCPM bid is to be used.</param>
        /// <returns>The ad group ID.</returns>
        public long CreateAdGroup(AdWordsUser user, long campaignId, AdGroupType adGroupType,
            bool isCpmBid)
        {
            AdGroupService adGroupService =
                (AdGroupService) user.GetService(AdWordsService.v201809.AdGroupService);

            AdGroupOperation adGroupOperation = new AdGroupOperation();
            adGroupOperation.@operator = Operator.ADD;
            adGroupOperation.operand = new AdGroup();
            adGroupOperation.operand.campaignId = campaignId;
            adGroupOperation.operand.name = string.Format("AdGroup {0}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
            adGroupOperation.operand.status = AdGroupStatus.ENABLED;

            if (adGroupType != AdGroupType.UNKNOWN)
            {
                adGroupOperation.operand.adGroupType = adGroupType;
            }

            if (isCpmBid)
            {
                BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();
                CpmBid cpmBid = new CpmBid();
                cpmBid.bid = new Money();
                cpmBid.bid.microAmount = 10000000;
                biddingConfig.bids = new Bids[]
                {
                    cpmBid
                };
                adGroupOperation.operand.biddingStrategyConfiguration = biddingConfig;
            }
            else
            {
                BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();
                CpcBid cpcBid = new CpcBid();
                cpcBid.bid = new Money();
                cpcBid.bid.microAmount = 10000000;
                biddingConfig.bids = new Bids[]
                {
                    cpcBid
                };
                adGroupOperation.operand.biddingStrategyConfiguration = biddingConfig;
            }

            AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[]
            {
                adGroupOperation
            });
            return retVal.value[0].id;
        }

        /// <summary>
        /// Creates a test expanded text ad for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">The adgroup id for which the ad is created.
        /// </param>
        /// <param name="hasAdParam">True, if an ad param placeholder should be
        /// added.</param>
        /// <returns>The expanded text ad id.</returns>
        public long CreateExpandedTextAd(AdWordsUser user, long adGroupId, bool hasAdParam)
        {
            AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService);
            AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
            adGroupAdOperation.@operator = Operator.ADD;
            adGroupAdOperation.operand = new AdGroupAd();
            adGroupAdOperation.operand.adGroupId = adGroupId;

            ExpandedTextAd expandedTextAd = new ExpandedTextAd();
            expandedTextAd.headlinePart1 = "Luxury Cruise to Mars";
            expandedTextAd.headlinePart2 = "Best Space Cruise Line";
            expandedTextAd.description = "Buy your tickets now!";
            if (hasAdParam)
            {
                expandedTextAd.description = "Low-gravity fun for {param1:cheap}!";
            }
            else
            {
                expandedTextAd.description = "Low-gravity fun for everyone!";
            }

            expandedTextAd.finalUrls = new string[]
            {
                "http://www.example.com/"
            };

            adGroupAdOperation.operand.ad = expandedTextAd;

            AdGroupAdReturnValue retVal = adGroupAdService.mutate(new AdGroupAdOperation[]
            {
                adGroupAdOperation
            });
            return retVal.value[0].ad.id;
        }

        /// <summary>
        /// Creates a test draft for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="baseCampaignId">The base campaign ID for the draft.</param>
        /// <returns>The draft.</returns>
        /// <remarks>We are returning the Draft itself, since there's no way to get
        /// the draft campaign ID given a draft ID.</remarks>
        public Draft AddDraft(AdWordsUser user, long baseCampaignId)
        {
            // Get the DraftService.
            DraftService draftService =
                (DraftService) user.GetService(AdWordsService.v201809.DraftService);
            Draft draft = new Draft()
            {
                baseCampaignId = baseCampaignId,
                draftName = "Test Draft #" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")
            };

            DraftOperation draftOperation = new DraftOperation()
            {
                @operator = Operator.ADD,
                operand = draft
            };

            return draftService.mutate(new DraftOperation[]
            {
                draftOperation
            }).value[0];
        }

        /// <summary>
        /// Creates a test trial for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="baseCampaignId">The base campaign ID for the draft.</param>
        /// <param name="draftId">ID of the draft to use when creating trial.</param>
        /// <returns>The trial ID.</returns>
        public long CreateTrial(AdWordsUser user, long draftId, long baseCampaignId)
        {
            // Get the TrialService.
            TrialService trialService =
                (TrialService) user.GetService(AdWordsService.v201809.TrialService);

            Trial trial = new Trial()
            {
                draftId = draftId,
                baseCampaignId = baseCampaignId,
                name = "Test Trial #" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                trafficSplitPercent = 50
            };

            TrialOperation trialOperation = new TrialOperation()
            {
                @operator = Operator.ADD,
                operand = trial
            };

            long trialId = trialService.mutate(new TrialOperation[]
            {
                trialOperation
            }).value[0].id;

            // Since creating a trial is asynchronous, we have to poll it to wait
            // for it to finish.
            Selector trialSelector = new Selector()
            {
                fields = new string[]
                {
                    Trial.Fields.Id,
                    Trial.Fields.Status,
                    Trial.Fields.BaseCampaignId,
                    Trial.Fields.TrialCampaignId
                },
                predicates = new Predicate[]
                {
                    Predicate.Equals(Trial.Fields.Id, trialId)
                }
            };

            trial = null;
            bool isPending = true;
            int pollAttempts = 0;

            do
            {
                int sleepMillis = (int) Math.Pow(2, pollAttempts) * POLL_INTERVAL_SECONDS_BASE *
                    1000;
                Console.WriteLine("Sleeping {0} millis...", sleepMillis);
                Thread.Sleep(sleepMillis);

                trial = trialService.get(trialSelector).entries[0];

                Console.WriteLine("Trial ID {0} has status '{1}'.", trial.id, trial.status);
                pollAttempts++;
                isPending = (trial.status == TrialStatus.CREATING);
            } while (isPending && pollAttempts <= MAX_RETRIES);

            if (trial.status == TrialStatus.ACTIVE)
            {
                return trial.id;
            }
            else
            {
                throw new System.ApplicationException(
                    "Failed to create an active trial for testing.");
            }
        }

        /// <summary>
        /// Creates a test ThirdPartyRedirectAd for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">The adgroup id for which the ad is created.
        /// </param>
        /// <returns>The text ad id.</returns>
        public long CreateThirdPartyRedirectAd(AdWordsUser user, long adGroupId)
        {
            AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService);
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

            AdGroupAdReturnValue retVal = adGroupAdService.mutate(new AdGroupAdOperation[]
            {
                adGroupAdOperation
            });
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
        public void SetAdParam(AdWordsUser user, long adGroupId, long criterionId)
        {
            AdParamService adParamService =
                (AdParamService) user.GetService(AdWordsService.v201809.AdParamService);

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
            AdParam[] newAdParams = adParamService.mutate(new AdParamOperation[]
            {
                adParamOperation
            });
            return;
        }

        /// <summary>
        /// Creates a keyword for running further tests.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">The adgroup id for which the keyword is
        /// created.</param>
        /// <returns>The keyword id.</returns>
        public long CreateKeyword(AdWordsUser user, long adGroupId)
        {
            AdGroupCriterionService adGroupCriterionService =
                (AdGroupCriterionService) user.GetService(AdWordsService.v201809
                    .AdGroupCriterionService);

            AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
            operation.@operator = Operator.ADD;
            operation.operand = new BiddableAdGroupCriterion();
            operation.operand.adGroupId = adGroupId;

            Keyword keyword = new Keyword();
            keyword.matchType = KeywordMatchType.BROAD;
            keyword.text = "mars cruise";

            operation.operand.criterion = keyword;
            AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(
                new AdGroupCriterionOperation[]
                {
                    operation
                });
            return retVal.value[0].criterion.id;
        }

        /// <summary>
        /// Creates the placement.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">The adgroup id for which the placement is
        /// created.</param>
        /// <returns>The placement id.</returns>
        public long CreatePlacement(AdWordsUser user, long adGroupId)
        {
            AdGroupCriterionService adGroupCriterionService =
                (AdGroupCriterionService) user.GetService(AdWordsService.v201809
                    .AdGroupCriterionService);

            Placement placement = new Placement();
            placement.url = "http://mars.google.com";

            AdGroupCriterion placementCriterion = new BiddableAdGroupCriterion();
            placementCriterion.adGroupId = adGroupId;
            placementCriterion.criterion = placement;

            AdGroupCriterionOperation placementOperation = new AdGroupCriterionOperation();
            placementOperation.@operator = Operator.ADD;
            placementOperation.operand = placementCriterion;

            AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(
                new AdGroupCriterionOperation[]
                {
                    placementOperation
                });

            return retVal.value[0].criterion.id;
        }

        /// <summary>
        /// Adds the campaign targeting criteria to a campaign.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The campaign id.</param>
        /// <returns>The campaign criteria id.</returns>
        public long AddCampaignTargetingCriteria(AdWordsUser user, long campaignId)
        {
            // Get the CampaignCriterionService.
            CampaignCriterionService campaignCriterionService =
                (CampaignCriterionService) user.GetService(AdWordsService.v201809
                    .CampaignCriterionService);

            // Create language criteria.
            // See http://code.google.com/apis/adwords/docs/appendix/languagecodes.html
            // for a detailed list of language codes.
            Language language1 = new Language();
            language1.id = 1002; // French
            CampaignCriterion languageCriterion1 = new CampaignCriterion();
            languageCriterion1.campaignId = campaignId;
            languageCriterion1.criterion = language1;

            CampaignCriterion[] criteria = new CampaignCriterion[]
            {
                languageCriterion1
            };

            List<CampaignCriterionOperation> operations = new List<CampaignCriterionOperation>();

            foreach (CampaignCriterion criterion in criteria)
            {
                CampaignCriterionOperation operation = new CampaignCriterionOperation();
                operation.@operator = Operator.ADD;
                operation.operand = criterion;
                operations.Add(operation);
            }

            CampaignCriterionReturnValue retVal =
                campaignCriterionService.mutate(operations.ToArray());
            return retVal.value[0].criterion.id;
        }

        /// <summary>
        /// Returns an image which can be used for creating image ads.
        /// </summary>
        /// <returns>The image data, as an array of bytes.</returns>
        public byte[] GetTestImage(AdWordsAppConfig config)
        {
            return MediaUtilities.GetAssetDataFromUrl("http://goo.gl/HJM3L", config);
        }

        /// <summary>
        /// Creates the shared keyword set.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <returns>A shared keyword set.</returns>
        public long CreateSharedKeywordSet(AdWordsUser user)
        {
            // Get the SharedSetService.
            SharedSetService sharedSetService =
                (SharedSetService) user.GetService(AdWordsService.v201809.SharedSetService);

            SharedSetOperation operation = new SharedSetOperation();
            operation.@operator = Operator.ADD;
            SharedSet sharedSet = new SharedSet();
            sharedSet.name = "API Negative keyword list - " + GetTimeStampAlpha();
            sharedSet.type = SharedSetType.NEGATIVE_KEYWORDS;
            operation.operand = sharedSet;

            SharedSetReturnValue retval = sharedSetService.mutate(new SharedSetOperation[]
            {
                operation
            });
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
        public void AttachSharedSetToCampaign(AdWordsUser user, long campaignId, long sharedSetId)
        {
            // Get the CampaignSharedSetService.
            CampaignSharedSetService campaignSharedSetService =
                (CampaignSharedSetService) user.GetService(AdWordsService.v201809
                    .CampaignSharedSetService);

            CampaignSharedSet campaignSharedSet = new CampaignSharedSet();
            campaignSharedSet.campaignId = campaignId;
            campaignSharedSet.sharedSetId = sharedSetId;

            CampaignSharedSetOperation operation = new CampaignSharedSetOperation();
            operation.@operator = Operator.ADD;
            operation.operand = campaignSharedSet;

            campaignSharedSetService.mutate(new CampaignSharedSetOperation[]
            {
                operation
            });
        }

        /// <summary>
        /// Detaches the shared set from campaign.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The campaign identifier.</param>
        /// <param name="sharedSetId">The shared set identifier.</param>
        public void DetachSharedSetFromCampaign(AdWordsUser user, long campaignId, long sharedSetId)
        {
            // Get the CampaignSharedSetService.
            CampaignSharedSetService campaignSharedSetService =
                (CampaignSharedSetService) user.GetService(AdWordsService.v201809
                    .CampaignSharedSetService);

            CampaignSharedSet campaignSharedSet = new CampaignSharedSet();
            campaignSharedSet.campaignId = campaignId;
            campaignSharedSet.sharedSetId = sharedSetId;

            CampaignSharedSetOperation operation = new CampaignSharedSetOperation();
            operation.@operator = Operator.REMOVE;
            operation.operand = campaignSharedSet;

            campaignSharedSetService.mutate(new CampaignSharedSetOperation[]
            {
                operation
            });
        }

        /// <summary>
        /// Deletes the shared set.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="sharedSetId">The shared set ID.</param>
        public void DeleteSharedSet(AdWordsUser user, long sharedSetId)
        {
            // Get the SharedSetService.
            SharedSetService sharedSetService =
                (SharedSetService) user.GetService(AdWordsService.v201809.SharedSetService);

            SharedSetOperation operation = new SharedSetOperation();
            operation.@operator = Operator.REMOVE;
            SharedSet sharedSet = new SharedSet();
            sharedSet.sharedSetId = sharedSetId;
            operation.operand = sharedSet;

            SharedSetReturnValue retval = sharedSetService.mutate(new SharedSetOperation[]
            {
                operation
            });
        }

        /// <summary>
        /// Creates the user list.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <returns>The user list ID.</returns>
        public long CreateUserList(AdWordsUser user)
        {
            using (AdwordsUserListService userListService =
                (AdwordsUserListService) user.GetService(AdWordsService.v201809
                    .AdwordsUserListService))
            {
                BasicUserList userList = new BasicUserList();
                userList.name = "Mars cruise customers #" + GetTimeStamp();
                userList.description = "A list of mars cruise customers in the last year.";
                userList.status = UserListMembershipStatus.OPEN;
                userList.membershipLifeSpan = 365;

                UserListConversionType conversionType = new UserListConversionType();
                conversionType.name = userList.name;
                userList.conversionTypes = new UserListConversionType[]
                {
                    conversionType
                };

                // Optional: Set the user list status.
                userList.status = UserListMembershipStatus.OPEN;

                // Create the operation.
                UserListOperation operation = new UserListOperation();
                operation.operand = userList;
                operation.@operator = Operator.ADD;

                UserListReturnValue retval = userListService.mutate(new UserListOperation[]
                {
                    operation
                });

                return retval.value[0].id;
            }
        }

        /// <summary>
        /// Gets the current timestamp.
        /// </summary>
        /// <returns>The timestamp as a string.</returns>
        public string GetTimeStamp()
        {
            return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
        }

        /// <summary>
        /// Gets the current timestamp as an alphabetic string.
        /// </summary>
        /// <returns>The timestamp as a string.</returns>
        public string GetTimeStampAlpha()
        {
            string timeStamp = GetTimeStamp();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < timeStamp.Length; i++)
            {
                if (timeStamp[i] == '.')
                {
                    continue;
                }

                builder.Append(Convert.ToChar('a' + int.Parse(timeStamp[i].ToString())));
            }

            return builder.ToString();
        }
    }
}

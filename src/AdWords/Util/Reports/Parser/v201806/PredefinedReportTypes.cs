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

#pragma warning disable 1591

namespace Google.Api.Ads.AdWords.Util.Reports.v201806
{
    public class KeywordsPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum ApprovalStatus
        {
            APPROVED,
            DISAPPROVED,
            PENDING_REVIEW,
            UNDER_REVIEW
        }

        public enum BiddingStrategySource
        {
            ADGROUP,
            CAMPAIGN,
            CRITERION
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum ConversionLagBucket
        {
            CONVERSION_0_TO_1_DAY,
            CONVERSION_1_TO_2_DAYS,
            CONVERSION_10_TO_11_DAYS,
            CONVERSION_11_TO_12_DAYS,
            CONVERSION_12_TO_13_DAYS,
            CONVERSION_13_TO_14_DAYS,
            CONVERSION_14_TO_21_DAYS,
            CONVERSION_2_TO_3_DAYS,
            CONVERSION_21_TO_30_DAYS,
            CONVERSION_3_TO_4_DAYS,
            CONVERSION_30_TO_45_DAYS,
            CONVERSION_4_TO_5_DAYS,
            CONVERSION_45_TO_60_DAYS,
            CONVERSION_5_TO_6_DAYS,
            CONVERSION_6_TO_7_DAYS,
            CONVERSION_60_TO_90_DAYS,
            CONVERSION_7_TO_8_DAYS,
            CONVERSION_8_TO_9_DAYS,
            CONVERSION_9_TO_10_DAYS,
            UNKNOWN
        }

        public enum CpcBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CreativeQualityScore
        {
            ABOVE_AVERAGE,
            AVERAGE,
            BELOW_AVERAGE,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum HistoricalCreativeQualityScore
        {
            ABOVE_AVERAGE,
            AVERAGE,
            BELOW_AVERAGE,
            UNKNOWN
        }

        public enum HistoricalLandingPageQualityScore
        {
            ABOVE_AVERAGE,
            AVERAGE,
            BELOW_AVERAGE,
            UNKNOWN
        }

        public enum HistoricalSearchPredictedCtr
        {
            ABOVE_AVERAGE,
            AVERAGE,
            BELOW_AVERAGE,
            UNKNOWN
        }

        public enum KeywordMatchType
        {
            BROAD,
            EXACT,
            PHRASE
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum PostClickQualityScore
        {
            ABOVE_AVERAGE,
            AVERAGE,
            BELOW_AVERAGE,
            UNKNOWN
        }

        public enum SearchPredictedCtr
        {
            ABOVE_AVERAGE,
            AVERAGE,
            BELOW_AVERAGE,
            UNKNOWN
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        public enum Status
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        public enum SystemServingStatus
        {
            ELIGIBLE,
            RARELY_SERVED
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("approvalStatus")]
        public ApprovalStatus approvalStatus { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("pagesSession")]
        public double averagePageviews { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("avgSessionDurationSeconds")]
        public double averageTimeOnSite { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("biddingStrategySource")]
        public BiddingStrategySource biddingStrategySource { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("bounceRate")]
        public double bounceRate { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clickAssistedConv")]
        public long clickAssistedConversions { get; set; }

        [ReportColumn("clickAssistedConvLastClickConv")]
        public double clickAssistedConversionsOverLastClickConversions { get; set; }

        [ReportColumn("clickAssistedConvValue")]
        public double clickAssistedConversionValue { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("daysToConversion")]
        public ConversionLagBucket conversionLagBucket { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("costConvCurrentModel")]
        public double costPerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPCSource")]
        public CpcBidSource cpcBidSource { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("adRelevance")]
        public CreativeQualityScore creativeQualityScore { get; set; }

        [ReportColumn("keyword")]
        public string criteria { get; set; }

        [ReportColumn("destinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("conversionsCurrentModel")]
        public double currentModelAttributedConversions { get; set; }

        [ReportColumn("convValueCurrentModel")]
        public double currentModelAttributedConversionValue { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("enhancedCPCEnabled")]
        public bool enhancedCpcEnabled { get; set; }

        [ReportColumn("estAddClicksWkFirstPositionBid")]
        public long estimatedAddClicksAtFirstPositionCpc { get; set; }

        [ReportColumn("estAddCostWkFirstPositionBid")]
        public decimal estimatedAddCostAtFirstPositionCpc { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("appFinalURL")]
        public string finalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string finalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrls { get; set; }

        [ReportColumn("finalURLSuffix")]
        public string finalUrlSuffix { get; set; }

        [ReportColumn("firstPageCPC")]
        public string firstPageCpc { get; set; }

        [ReportColumn("firstPositionCPC")]
        public string firstPositionCpc { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("hasQualityScore")]
        public bool hasQualityScore { get; set; }

        [ReportColumn("adRelevanceHist")]
        public HistoricalCreativeQualityScore historicalCreativeQualityScore { get; set; }

        [ReportColumn("landingPageExperienceHist")]
        public HistoricalLandingPageQualityScore historicalLandingPageQualityScore { get; set; }

        [ReportColumn("qualScoreHist")]
        public long historicalQualityScore { get; set; }

        [ReportColumn("expectedClickthroughRateHist")]
        public HistoricalSearchPredictedCtr historicalSearchPredictedCtr { get; set; }

        [ReportColumn("keywordID")]
        public long id { get; set; }

        [ReportColumn("imprAssistedConv")]
        public long impressionAssistedConversions { get; set; }

        [ReportColumn("imprAssistedConvLastClickConv")]
        public double impressionAssistedConversionsOverLastClickConversions { get; set; }

        [ReportColumn("imprAssistedConvValue")]
        public double impressionAssistedConversionValue { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("matchType")]
        public KeywordMatchType keywordMatchType { get; set; }

        [ReportColumn("labelIDs")]
        public string labelIds { get; set; }

        [ReportColumn("labels")]
        public string labels { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("newSessions")]
        public double percentNewVisitors { get; set; }

        [ReportColumn("landingPageExperience")]
        public PostClickQualityScore postClickQualityScore { get; set; }

        [ReportColumn("qualityScore")]
        public long qualityScore { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("searchExactMatchIS")]
        public double searchExactMatchImpressionShare { get; set; }

        [ReportColumn("searchImprShare")]
        public double searchImpressionShare { get; set; }

        [ReportColumn("expectedClickthroughRate")]
        public SearchPredictedCtr searchPredictedCtr { get; set; }

        [ReportColumn("searchLostISRank")]
        public double searchRankLostImpressionShare { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("keywordState")]
        public Status status { get; set; }

        [ReportColumn("criterionServingStatus")]
        public SystemServingStatus systemServingStatus { get; set; }

        [ReportColumn("topOfPageCPC")]
        public string topOfPageCpc { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("valueConvCurrentModel")]
        public double valuePerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("verticalID")]
        public long verticalId { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class AdPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdType
        {
            CALL_ONLY_AD,
            DEPRECATED_AD,
            DYNAMIC_SEARCH_AD,
            EXPANDED_DYNAMIC_SEARCH_AD,
            EXPANDED_TEXT_AD,
            GMAIL_AD,
            GOAL_OPTIMIZED_SHOPPING_AD,
            IMAGE_AD,
            MULTI_ASSET_RESPONSIVE_DISPLAY_AD,
            PRODUCT_AD,
            RESPONSIVE_DISPLAY_AD,
            RESPONSIVE_SEARCH_AD,
            SHOWCASE_AD,
            TEMPLATE_AD,
            TEXT_AD,
            THIRD_PARTY_REDIRECT_AD,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum CombinedApprovalStatus
        {
            APPROVED,
            APPROVED_LIMITED,
            DISAPPROVED,
            ELIGIBLE,
            SITE_SUSPENDED,
            UNDER_REVIEW,
            UNKNOWN
        }

        public enum ConversionLagBucket
        {
            CONVERSION_0_TO_1_DAY,
            CONVERSION_1_TO_2_DAYS,
            CONVERSION_10_TO_11_DAYS,
            CONVERSION_11_TO_12_DAYS,
            CONVERSION_12_TO_13_DAYS,
            CONVERSION_13_TO_14_DAYS,
            CONVERSION_14_TO_21_DAYS,
            CONVERSION_2_TO_3_DAYS,
            CONVERSION_21_TO_30_DAYS,
            CONVERSION_3_TO_4_DAYS,
            CONVERSION_30_TO_45_DAYS,
            CONVERSION_4_TO_5_DAYS,
            CONVERSION_45_TO_60_DAYS,
            CONVERSION_5_TO_6_DAYS,
            CONVERSION_6_TO_7_DAYS,
            CONVERSION_60_TO_90_DAYS,
            CONVERSION_7_TO_8_DAYS,
            CONVERSION_8_TO_9_DAYS,
            CONVERSION_9_TO_10_DAYS,
            UNKNOWN
        }

        public enum CriterionType
        {
            AD_SCHEDULE,
            AGE_RANGE,
            APP_PAYMENT_MODEL,
            CARRIER,
            CONTENT_LABEL,
            CUSTOM_AFFINITY,
            CUSTOM_IN_MARKET,
            GENDER,
            IP_BLOCK,
            KEYWORD,
            LANGUAGE,
            LOCATION,
            LOCATION_GROUPS,
            MOBILE_APP_CATEGORY,
            MOBILE_APPLICATION,
            MOBILE_DEVICE,
            OPERATING_SYSTEM_VERSION,
            PARENT,
            PLACEMENT,
            PLATFORM,
            PREFERRED_CONTENT,
            PRODUCT_PARTITION,
            PRODUCT_SCOPE,
            PROXIMITY,
            UNKNOWN,
            USER_INTEREST,
            USER_LIST,
            VERTICAL,
            WEBPAGE,
            YOUTUBE_CHANNEL,
            YOUTUBE_VIDEO
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum FormatSetting
        {
            ALL_FORMATS,
            NATIVE,
            NON_NATIVE,
            UNKNOWN
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum MultiAssetResponsiveDisplayAdFormatSetting
        {
            ALL_FORMATS,
            NATIVE,
            NON_NATIVE,
            UNKNOWN
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        public enum Status
        {
            DISABLED,
            ENABLED,
            PAUSED
        }

        public enum SystemManagedEntitySource
        {
            AD_VARIATIONS,
            UNKNOWN
        }

        [ReportColumn("callOnlyAdPhoneNumber")]
        public string callOnlyPhoneNumber { get; set; }

        [ReportColumn("imageAdURL")]
        public string imageAdUrl { get; set; }

        [ReportColumn("descriptionsMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdDescriptions { get; set; }

        [ReportColumn("headlinesMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdHeadlines { get; set; }

        [ReportColumn("landscapeLogosMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdLandscapeLogoImages { get; set; }

        [ReportColumn("logosMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdLogoImages { get; set; }

        [ReportColumn("longHeadlineMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdLongHeadline { get; set; }

        [ReportColumn("imagesMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdMarketingImages { get; set; }

        [ReportColumn("squareImagesMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdSquareMarketingImages { get; set; }

        [ReportColumn("policy")]
        public string policySummary { get; set; }

        [ReportColumn("responsiveSearchAdDescriptions")]
        public string responsiveSearchAdDescriptions { get; set; }

        [ReportColumn("responsiveSearchAdHeadlines")]
        public string responsiveSearchAdHeadlines { get; set; }

        [ReportColumn("accentColorResponsive")]
        public string accentColor { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("adType")]
        public AdType adType { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("allowFlexibleColorResponsive")]
        public bool allowFlexibleColor { get; set; }

        [ReportColumn("autoAppliedAdSuggestion")]
        public bool automated { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("pagesSession")]
        public double averagePageviews { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("avgSessionDurationSeconds")]
        public double averageTimeOnSite { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bounceRate")]
        public double bounceRate { get; set; }

        [ReportColumn("businessName")]
        public string businessName { get; set; }

        [ReportColumn("callToActionTextResponsive")]
        public string callToActionText { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clickAssistedConv")]
        public long clickAssistedConversions { get; set; }

        [ReportColumn("clickAssistedConvLastClickConv")]
        public double clickAssistedConversionsOverLastClickConversions { get; set; }

        [ReportColumn("clickAssistedConvValue")]
        public double clickAssistedConversionValue { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("approvalStatus")]
        public CombinedApprovalStatus combinedApprovalStatus { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("daysToConversion")]
        public ConversionLagBucket conversionLagBucket { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("costConvCurrentModel")]
        public double costPerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("destinationURL")]
        public string creativeDestinationUrl { get; set; }

        [ReportColumn("appFinalURL")]
        public string creativeFinalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string creativeFinalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string creativeFinalUrls { get; set; }

        [ReportColumn("finalURLSuffix")]
        public string creativeFinalUrlSuffix { get; set; }

        [ReportColumn("trackingTemplate")]
        public string creativeTrackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string creativeUrlCustomParameters { get; set; }

        [ReportColumn("keywordID")]
        public long criterionId { get; set; }

        [ReportColumn("criteriaType")]
        public CriterionType criterionType { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("conversionsCurrentModel")]
        public double currentModelAttributedConversions { get; set; }

        [ReportColumn("convValueCurrentModel")]
        public double currentModelAttributedConversionValue { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("description")]
        public string description { get; set; }

        [ReportColumn("descriptionLine1")]
        public string description1 { get; set; }

        [ReportColumn("descriptionLine2")]
        public string description2 { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("devicePreference")]
        public long devicePreference { get; set; }

        [ReportColumn("displayURL")]
        public string displayUrl { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("landscapeLogoIDResponsive")]
        public long enhancedDisplayCreativeLandscapeLogoImageMediaId { get; set; }

        [ReportColumn("logoIDResponsive")]
        public long enhancedDisplayCreativeLogoImageMediaId { get; set; }

        [ReportColumn("imageIDResponsive")]
        public long enhancedDisplayCreativeMarketingImageMediaId { get; set; }

        [ReportColumn("squareImageIDResponsive")]
        public long enhancedDisplayCreativeMarketingImageSquareMediaId { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("adFormatPreferenceResponsive")]
        public FormatSetting formatSetting { get; set; }

        [ReportColumn("gmailAdHeaderImageMediaId")]
        public long gmailCreativeHeaderImageMediaId { get; set; }

        [ReportColumn("gmailAdLogoImageMediaId")]
        public long gmailCreativeLogoImageMediaId { get; set; }

        [ReportColumn("gmailAdMarketingImageMediaId")]
        public long gmailCreativeMarketingImageMediaId { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("gmailAdBusinessName")]
        public string gmailTeaserBusinessName { get; set; }

        [ReportColumn("gmailAdDescription")]
        public string gmailTeaserDescription { get; set; }

        [ReportColumn("gmailAdHeadline")]
        public string gmailTeaserHeadline { get; set; }

        [ReportColumn("ad")]
        public string headline { get; set; }

        [ReportColumn("headline1")]
        public string headlinePart1 { get; set; }

        [ReportColumn("headline2")]
        public string headlinePart2 { get; set; }

        [ReportColumn("adID")]
        public long id { get; set; }

        [ReportColumn("imageHeight")]
        public long imageCreativeImageHeight { get; set; }

        [ReportColumn("imageWidth")]
        public long imageCreativeImageWidth { get; set; }

        [ReportColumn("imageMimeType")]
        public long imageCreativeMimeType { get; set; }

        [ReportColumn("imageAdName")]
        public string imageCreativeName { get; set; }

        [ReportColumn("imprAssistedConv")]
        public long impressionAssistedConversions { get; set; }

        [ReportColumn("imprAssistedConvLastClickConv")]
        public double impressionAssistedConversionsOverLastClickConversions { get; set; }

        [ReportColumn("imprAssistedConvValue")]
        public double impressionAssistedConversionValue { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("labelIDs")]
        public string labelIds { get; set; }

        [ReportColumn("labels")]
        public string labels { get; set; }

        [ReportColumn("longHeadline")]
        public string longHeadline { get; set; }

        [ReportColumn("mainColorResponsive")]
        public string mainColor { get; set; }

        [ReportColumn("gmailAdMarketingImageCallToActionText")]
        public string marketingImageCallToActionText { get; set; }

        [ReportColumn("gmailAdMarketingImageCallToActionTextColor")]
        public string marketingImageCallToActionTextColor { get; set; }

        [ReportColumn("gmailAdMarketingImageDescription")]
        public string marketingImageDescription { get; set; }

        [ReportColumn("gmailAdMarketingImageHeadline")]
        public string marketingImageHeadline { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("accentColorMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdAccentColor { get; set; }

        [ReportColumn("allowFlexibleColorMultiAssetResponsiveDisplay")]
        public bool multiAssetResponsiveDisplayAdAllowFlexibleColor { get; set; }

        [ReportColumn("businessNameMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdBusinessName { get; set; }

        [ReportColumn("callToActionTextMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdCallToActionText { get; set; }

        [ReportColumn("pricePrefixMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdDynamicSettingsPricePrefix { get; set; }

        [ReportColumn("promotionTextMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdDynamicSettingsPromoText { get; set; }

        [ReportColumn("adFormatPreferenceMultiAssetResponsiveDisplay")]
        public MultiAssetResponsiveDisplayAdFormatSetting multiAssetResponsiveDisplayAdFormatSetting
        {
            get;
            set;
        }

        [ReportColumn("mainColorMultiAssetResponsiveDisplay")]
        public string multiAssetResponsiveDisplayAdMainColor { get; set; }

        [ReportColumn("path1")]
        public string path1 { get; set; }

        [ReportColumn("path2")]
        public string path2 { get; set; }

        [ReportColumn("newSessions")]
        public double percentNewVisitors { get; set; }

        [ReportColumn("pricePrefixResponsive")]
        public string pricePrefix { get; set; }

        [ReportColumn("promotionTextResponsive")]
        public string promoText { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("responsiveSearchAdPath1")]
        public string responsiveSearchAdPath1 { get; set; }

        [ReportColumn("responsiveSearchAdPath2")]
        public string responsiveSearchAdPath2 { get; set; }

        [ReportColumn("shortHeadline")]
        public string shortHeadline { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("adState")]
        public Status status { get; set; }

        [ReportColumn("systemManagedEntitySource")]
        public SystemManagedEntitySource systemManagedEntitySource { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("valueConvCurrentModel")]
        public double valuePerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class UrlPerformanceReportReportRow
    {
        public enum AdFormat
        {
            AUDIO,
            COMPOSITE,
            DYNAMIC_IMAGE,
            FLASH,
            HTML,
            IMAGE,
            PRINT,
            TEXT,
            UNKNOWN,
            VIDEO
        }

        public enum AdGroupCriterionStatus
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("criteriaDisplayName")]
        public string displayName { get; set; }

        [ReportColumn("added")]
        public string isBidOnPath { get; set; }

        [ReportColumn("excluded")]
        public string isPathExcluded { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adType")]
        public AdFormat adFormat { get; set; }

        [ReportColumn("keywordPlacementState")]
        public AdGroupCriterionStatus adGroupCriterionStatus { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("keywordPlacement")]
        public string criteriaParameters { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("domain")]
        public string domain { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("targetingMode")]
        public bool isAutoOptimized { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("url")]
        public string url { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class AdgroupPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdGroupType
        {
            DISPLAY_ENGAGEMENT,
            DISPLAY_STANDARD,
            HOTEL_ADS,
            SEARCH_DYNAMIC_ADS,
            SEARCH_STANDARD,
            SHOPPING_GOAL_OPTIMIZED_ADS,
            SHOPPING_PRODUCT_ADS,
            SHOPPING_SHOWCASE_ADS,
            UNKNOWN,
            VIDEO_BUMPER,
            VIDEO_NON_SKIPPABLE_IN_STREAM,
            VIDEO_TRUE_VIEW_IN_DISPLAY,
            VIDEO_TRUE_VIEW_IN_STREAM
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdRotationMode
        {
            OPTIMIZE,
            ROTATE_FOREVER,
            UNKNOWN
        }

        public enum BiddingStrategySource
        {
            ADGROUP,
            CAMPAIGN,
            CRITERION
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum ContentBidCriterionTypeGroup
        {
            AGE_RANGE,
            GENDER,
            INCOME_RANGE,
            KEYWORD,
            NONE,
            PARENT,
            PLACEMENT,
            UNKNOWN,
            USER_INTEREST_AND_LIST,
            VERTICAL
        }

        public enum ConversionLagBucket
        {
            CONVERSION_0_TO_1_DAY,
            CONVERSION_1_TO_2_DAYS,
            CONVERSION_10_TO_11_DAYS,
            CONVERSION_11_TO_12_DAYS,
            CONVERSION_12_TO_13_DAYS,
            CONVERSION_13_TO_14_DAYS,
            CONVERSION_14_TO_21_DAYS,
            CONVERSION_2_TO_3_DAYS,
            CONVERSION_21_TO_30_DAYS,
            CONVERSION_3_TO_4_DAYS,
            CONVERSION_30_TO_45_DAYS,
            CONVERSION_4_TO_5_DAYS,
            CONVERSION_45_TO_60_DAYS,
            CONVERSION_5_TO_6_DAYS,
            CONVERSION_6_TO_7_DAYS,
            CONVERSION_60_TO_90_DAYS,
            CONVERSION_7_TO_8_DAYS,
            CONVERSION_8_TO_9_DAYS,
            CONVERSION_9_TO_10_DAYS,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum EffectiveTargetRoasSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            ADGROUP_CRITERION,
            CAMPAIGN_BIDDING_STRATEGY,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        public enum TargetCpaBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("desktopBidAdj")]
        public double adGroupDesktopBidModifier { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("mobileBidAdj")]
        public double adGroupMobileBidModifier { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("tabletBidAdj")]
        public double adGroupTabletBidModifier { get; set; }

        [ReportColumn("adGroupType")]
        public AdGroupType adGroupType { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("adRotationMode")]
        public AdRotationMode adRotationMode { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("pagesSession")]
        public double averagePageviews { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("avgSessionDurationSeconds")]
        public double averageTimeOnSite { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("biddingStrategySource")]
        public BiddingStrategySource biddingStrategySource { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("bounceRate")]
        public double bounceRate { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clickAssistedConv")]
        public long clickAssistedConversions { get; set; }

        [ReportColumn("clickAssistedConvLastClickConv")]
        public double clickAssistedConversionsOverLastClickConversions { get; set; }

        [ReportColumn("clickAssistedConvValue")]
        public double clickAssistedConversionValue { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("contentNetworkBidDimension")]
        public ContentBidCriterionTypeGroup contentBidCriterionTypeGroup { get; set; }

        [ReportColumn("contentImprShare")]
        public double contentImpressionShare { get; set; }

        [ReportColumn("contentLostISRank")]
        public double contentRankLostImpressionShare { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("daysToConversion")]
        public ConversionLagBucket conversionLagBucket { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("costConvCurrentModel")]
        public double costPerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("defaultMaxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("maxCPV")]
        public decimal cpvBid { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("conversionsCurrentModel")]
        public double currentModelAttributedConversions { get; set; }

        [ReportColumn("convValueCurrentModel")]
        public double currentModelAttributedConversionValue { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("targetROAS")]
        public double effectiveTargetRoas { get; set; }

        [ReportColumn("targetROASSource")]
        public EffectiveTargetRoasSource effectiveTargetRoasSource { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("enhancedCPCEnabled")]
        public bool enhancedCpcEnabled { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("finalURLSuffix")]
        public string finalUrlSuffix { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("hourOfDay")]
        public long hourOfDay { get; set; }

        [ReportColumn("imprAssistedConv")]
        public long impressionAssistedConversions { get; set; }

        [ReportColumn("imprAssistedConvLastClickConv")]
        public double impressionAssistedConversionsOverLastClickConversions { get; set; }

        [ReportColumn("imprAssistedConvValue")]
        public double impressionAssistedConversionValue { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("labelIDs")]
        public string labelIds { get; set; }

        [ReportColumn("labels")]
        public string labels { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("phoneImpressions")]
        public long numOfflineImpressions { get; set; }

        [ReportColumn("phoneCalls")]
        public long numOfflineInteractions { get; set; }

        [ReportColumn("ptr")]
        public double offlineInteractionRate { get; set; }

        [ReportColumn("newSessions")]
        public double percentNewVisitors { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("relativeCTR")]
        public double relativeCtr { get; set; }

        [ReportColumn("searchAbsTopIS")]
        public double searchAbsoluteTopImpressionShare { get; set; }

        [ReportColumn("searchExactMatchIS")]
        public double searchExactMatchImpressionShare { get; set; }

        [ReportColumn("searchImprShare")]
        public double searchImpressionShare { get; set; }

        [ReportColumn("searchLostISRank")]
        public double searchRankLostImpressionShare { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("targetCPA")]
        public decimal targetCpa { get; set; }

        [ReportColumn("targetCPASource")]
        public TargetCpaBidSource targetCpaBidSource { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("valueConvCurrentModel")]
        public double valuePerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class CampaignPerformanceReportReportRow
    {
        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdvertisingChannelSubType
        {
            DISPLAY_EXPRESS,
            DISPLAY_GMAIL_AD,
            DISPLAY_MOBILE_APP,
            DISPLAY_SMART_CAMPAIGN,
            SEARCH_EXPRESS,
            SEARCH_MOBILE_APP,
            SHOPPING_GOAL_OPTIMIZED_ADS,
            UNIVERSAL_APP_CAMPAIGN,
            UNKNOWN
        }

        public enum AdvertisingChannelType
        {
            DISPLAY,
            EXPRESS,
            MULTI_CHANNEL,
            SEARCH,
            SHOPPING,
            UNKNOWN,
            VIDEO
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum CampaignTrialType
        {
            BASE,
            DRAFT,
            TRIAL,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum ConversionLagBucket
        {
            CONVERSION_0_TO_1_DAY,
            CONVERSION_1_TO_2_DAYS,
            CONVERSION_10_TO_11_DAYS,
            CONVERSION_11_TO_12_DAYS,
            CONVERSION_12_TO_13_DAYS,
            CONVERSION_13_TO_14_DAYS,
            CONVERSION_14_TO_21_DAYS,
            CONVERSION_2_TO_3_DAYS,
            CONVERSION_21_TO_30_DAYS,
            CONVERSION_3_TO_4_DAYS,
            CONVERSION_30_TO_45_DAYS,
            CONVERSION_4_TO_5_DAYS,
            CONVERSION_45_TO_60_DAYS,
            CONVERSION_5_TO_6_DAYS,
            CONVERSION_6_TO_7_DAYS,
            CONVERSION_60_TO_90_DAYS,
            CONVERSION_7_TO_8_DAYS,
            CONVERSION_8_TO_9_DAYS,
            CONVERSION_9_TO_10_DAYS,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum ServingStatus
        {
            ENDED,
            NONE,
            PENDING,
            SERVING,
            SUSPENDED
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        [ReportColumn("budgetPeriod")]
        public string period { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("advertisingSubChannel")]
        public AdvertisingChannelSubType advertisingChannelSubType { get; set; }

        [ReportColumn("advertisingChannel")]
        public AdvertisingChannelType advertisingChannelType { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("budget")]
        public decimal amount { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgImprFreqPerCookie")]
        public double averageFrequency { get; set; }

        [ReportColumn("pagesSession")]
        public double averagePageviews { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("avgSessionDurationSeconds")]
        public double averageTimeOnSite { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("bounceRate")]
        public double bounceRate { get; set; }

        [ReportColumn("budgetID")]
        public long budgetId { get; set; }

        [ReportColumn("desktopBidAdj")]
        public double campaignDesktopBidModifier { get; set; }

        [ReportColumn("campaignGroupID")]
        public long campaignGroupId { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("mobileBidAdj")]
        public double campaignMobileBidModifier { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("tabletBidAdj")]
        public double campaignTabletBidModifier { get; set; }

        [ReportColumn("campaignTrialType")]
        public CampaignTrialType campaignTrialType { get; set; }

        [ReportColumn("clickAssistedConv")]
        public long clickAssistedConversions { get; set; }

        [ReportColumn("clickAssistedConvLastClickConv")]
        public double clickAssistedConversionsOverLastClickConversions { get; set; }

        [ReportColumn("clickAssistedConvValue")]
        public double clickAssistedConversionValue { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("contentLostISBudget")]
        public double contentBudgetLostImpressionShare { get; set; }

        [ReportColumn("contentImprShare")]
        public double contentImpressionShare { get; set; }

        [ReportColumn("contentLostISRank")]
        public double contentRankLostImpressionShare { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("daysToConversion")]
        public ConversionLagBucket conversionLagBucket { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("costConvCurrentModel")]
        public double costPerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("conversionsCurrentModel")]
        public double currentModelAttributedConversions { get; set; }

        [ReportColumn("convValueCurrentModel")]
        public double currentModelAttributedConversionValue { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("endDate")]
        public string endDate { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("enhancedCPCEnabled")]
        public bool enhancedCpcEnabled { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("finalURLSuffix")]
        public string finalUrlSuffix { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("hourOfDay")]
        public long hourOfDay { get; set; }

        [ReportColumn("imprAssistedConv")]
        public long impressionAssistedConversions { get; set; }

        [ReportColumn("imprAssistedConvLastClickConv")]
        public double impressionAssistedConversionsOverLastClickConversions { get; set; }

        [ReportColumn("imprAssistedConvValue")]
        public double impressionAssistedConversionValue { get; set; }

        [ReportColumn("uniqueCookies")]
        public long impressionReach { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("invalidClickRate")]
        public double invalidClickRate { get; set; }

        [ReportColumn("invalidClicks")]
        public long invalidClicks { get; set; }

        [ReportColumn("budgetExplicitlyShared")]
        public bool isBudgetExplicitlyShared { get; set; }

        [ReportColumn("labelIDs")]
        public string labelIds { get; set; }

        [ReportColumn("labels")]
        public string labels { get; set; }

        [ReportColumn("targetROASMaximizeConversionValue")]
        public double maximizeConversionValueTargetRoas { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("phoneImpressions")]
        public long numOfflineImpressions { get; set; }

        [ReportColumn("phoneCalls")]
        public long numOfflineInteractions { get; set; }

        [ReportColumn("ptr")]
        public double offlineInteractionRate { get; set; }

        [ReportColumn("newSessions")]
        public double percentNewVisitors { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("relativeCTR")]
        public double relativeCtr { get; set; }

        [ReportColumn("searchAbsTopIS")]
        public double searchAbsoluteTopImpressionShare { get; set; }

        [ReportColumn("searchLostISBudget")]
        public double searchBudgetLostImpressionShare { get; set; }

        [ReportColumn("clickShare")]
        public double searchClickShare { get; set; }

        [ReportColumn("searchExactMatchIS")]
        public double searchExactMatchImpressionShare { get; set; }

        [ReportColumn("searchImprShare")]
        public double searchImpressionShare { get; set; }

        [ReportColumn("searchLostISRank")]
        public double searchRankLostImpressionShare { get; set; }

        [ReportColumn("campaignServingStatus")]
        public ServingStatus servingStatus { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("startDate")]
        public string startDate { get; set; }

        [ReportColumn("totalBudgetAmount")]
        public decimal totalAmount { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("valueConvCurrentModel")]
        public double valuePerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class AccountPerformanceReportReportRow
    {
        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum ConversionLagBucket
        {
            CONVERSION_0_TO_1_DAY,
            CONVERSION_1_TO_2_DAYS,
            CONVERSION_10_TO_11_DAYS,
            CONVERSION_11_TO_12_DAYS,
            CONVERSION_12_TO_13_DAYS,
            CONVERSION_13_TO_14_DAYS,
            CONVERSION_14_TO_21_DAYS,
            CONVERSION_2_TO_3_DAYS,
            CONVERSION_21_TO_30_DAYS,
            CONVERSION_3_TO_4_DAYS,
            CONVERSION_30_TO_45_DAYS,
            CONVERSION_4_TO_5_DAYS,
            CONVERSION_45_TO_60_DAYS,
            CONVERSION_5_TO_6_DAYS,
            CONVERSION_6_TO_7_DAYS,
            CONVERSION_60_TO_90_DAYS,
            CONVERSION_7_TO_8_DAYS,
            CONVERSION_8_TO_9_DAYS,
            CONVERSION_9_TO_10_DAYS,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("canManageClients")]
        public bool canManageClients { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("contentLostISBudget")]
        public double contentBudgetLostImpressionShare { get; set; }

        [ReportColumn("contentImprShare")]
        public double contentImpressionShare { get; set; }

        [ReportColumn("contentLostISRank")]
        public double contentRankLostImpressionShare { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("daysToConversion")]
        public ConversionLagBucket conversionLagBucket { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("hourOfDay")]
        public long hourOfDay { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("invalidClickRate")]
        public double invalidClickRate { get; set; }

        [ReportColumn("invalidClicks")]
        public long invalidClicks { get; set; }

        [ReportColumn("autoTaggingEnabled")]
        public bool isAutoTaggingEnabled { get; set; }

        [ReportColumn("testAccount")]
        public bool isTestAccount { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("searchLostISBudget")]
        public double searchBudgetLostImpressionShare { get; set; }

        [ReportColumn("searchExactMatchIS")]
        public double searchExactMatchImpressionShare { get; set; }

        [ReportColumn("searchImprShare")]
        public double searchImpressionShare { get; set; }

        [ReportColumn("searchLostISRank")]
        public double searchRankLostImpressionShare { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class GeoPerformanceReportReportRow
    {
        public enum AdFormat
        {
            AUDIO,
            COMPOSITE,
            DYNAMIC_IMAGE,
            FLASH,
            HTML,
            IMAGE,
            PRINT,
            TEXT,
            UNKNOWN,
            VIDEO
        }

        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum LocationType
        {
            AREA_OF_INTEREST,
            LOCATION_OF_PRESENCE,
            UNKNOWN
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("adType")]
        public AdFormat adFormat { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("city")]
        public long cityCriteriaId { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("countryTerritory")]
        public long countryCriteriaId { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isTargetable")]
        public bool isTargetingLocation { get; set; }

        [ReportColumn("locationType")]
        public LocationType locationType { get; set; }

        [ReportColumn("metroArea")]
        public long metroCriteriaId { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("mostSpecificLocation")]
        public long mostSpecificCriteriaId { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("region")]
        public long regionCriteriaId { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class SearchQueryPerformanceReportReportRow
    {
        public enum AdFormat
        {
            AUDIO,
            COMPOSITE,
            DYNAMIC_IMAGE,
            FLASH,
            HTML,
            IMAGE,
            PRINT,
            TEXT,
            UNKNOWN,
            VIDEO
        }

        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum QueryMatchTypeWithVariant
        {
            AUTO,
            BROAD,
            EXACT,
            EXPANDED,
            NEAR_EXACT,
            NEAR_PHRASE,
            PHRASE
        }

        public enum QueryTargetingStatus
        {
            ADDED,
            BOTH,
            EXCLUDED,
            NONE
        }

        [ReportColumn("keywordID")]
        public string keywordId { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("adType")]
        public AdFormat adFormat { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("adID")]
        public long creativeId { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("destinationURL")]
        public string destinationUrl { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrl { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("keyword")]
        public string keywordTextMatchingQuery { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("searchTerm")]
        public string query { get; set; }

        [ReportColumn("matchType")]
        public QueryMatchTypeWithVariant queryMatchTypeWithVariant { get; set; }

        [ReportColumn("addedExcluded")]
        public QueryTargetingStatus queryTargetingStatus { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class AutomaticPlacementsPerformanceReportReportRow
    {
        public enum AdFormat
        {
            AUDIO,
            COMPOSITE,
            DYNAMIC_IMAGE,
            FLASH,
            HTML,
            IMAGE,
            PRINT,
            TEXT,
            UNKNOWN,
            VIDEO
        }

        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("criteriaDisplayName")]
        public string displayName { get; set; }

        [ReportColumn("added")]
        public string isBidOnPath { get; set; }

        [ReportColumn("excluded")]
        public string isPathExcluded { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adType")]
        public AdFormat adFormat { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("url")]
        public string criteriaParameters { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("domain")]
        public string domain { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("targetingMode")]
        public bool isAutoOptimized { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class CampaignNegativeKeywordsPerformanceReportReportRow
    {
        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum KeywordMatchType
        {
            BROAD,
            EXACT,
            PHRASE
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("negativeKeyword")]
        public string criteria { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("keywordID")]
        public long id { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("matchType")]
        public KeywordMatchType keywordMatchType { get; set; }
    }

    public class CampaignNegativePlacementsPerformanceReportReportRow
    {
        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum KeywordMatchType
        {
            BROAD,
            EXACT,
            PHRASE
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("exclusion")]
        public string criteria { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("criteriaDisplayName")]
        public string displayName { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("keywordID")]
        public long id { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("matchType")]
        public KeywordMatchType keywordMatchType { get; set; }

        [ReportColumn("verticalID")]
        public long verticalId { get; set; }
    }

    public class DestinationUrlReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum ConversionLagBucket
        {
            CONVERSION_0_TO_1_DAY,
            CONVERSION_1_TO_2_DAYS,
            CONVERSION_10_TO_11_DAYS,
            CONVERSION_11_TO_12_DAYS,
            CONVERSION_12_TO_13_DAYS,
            CONVERSION_13_TO_14_DAYS,
            CONVERSION_14_TO_21_DAYS,
            CONVERSION_2_TO_3_DAYS,
            CONVERSION_21_TO_30_DAYS,
            CONVERSION_3_TO_4_DAYS,
            CONVERSION_30_TO_45_DAYS,
            CONVERSION_4_TO_5_DAYS,
            CONVERSION_45_TO_60_DAYS,
            CONVERSION_5_TO_6_DAYS,
            CONVERSION_6_TO_7_DAYS,
            CONVERSION_60_TO_90_DAYS,
            CONVERSION_7_TO_8_DAYS,
            CONVERSION_8_TO_9_DAYS,
            CONVERSION_9_TO_10_DAYS,
            UNKNOWN
        }

        public enum CriteriaStatus
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("daysToConversion")]
        public ConversionLagBucket conversionLagBucket { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("costConvCurrentModel")]
        public double costPerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("keywordPlacementDestinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("keywordPlacement")]
        public string criteriaParameters { get; set; }

        [ReportColumn("keywordPlacementState")]
        public CriteriaStatus criteriaStatus { get; set; }

        [ReportColumn("matchType")]
        public string criteriaTypeName { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("conversionsCurrentModel")]
        public double currentModelAttributedConversions { get; set; }

        [ReportColumn("convValueCurrentModel")]
        public double currentModelAttributedConversionValue { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("destinationURL")]
        public string effectiveDestinationUrl { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("valueConvCurrentModel")]
        public double valuePerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class SharedSetReportReportRow
    {
        public enum Status
        {
            ENABLED,
            REMOVED,
            UNKNOWN
        }

        public enum Type
        {
            NEGATIVE_KEYWORDS,
            NEGATIVE_PLACEMENTS,
            UNKNOWN
        }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("memberCount")]
        public string memberCount { get; set; }

        [ReportColumn("sharedSetName")]
        public string name { get; set; }

        [ReportColumn("referenceCount")]
        public string referenceCount { get; set; }

        [ReportColumn("sharedSetID")]
        public long sharedSetId { get; set; }

        [ReportColumn("state")]
        public Status status { get; set; }

        [ReportColumn("sharedSetType")]
        public Type type { get; set; }
    }

    public class CampaignSharedSetReportReportRow
    {
        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum SharedSetType
        {
            NEGATIVE_KEYWORDS,
            NEGATIVE_PLACEMENTS,
            UNKNOWN
        }

        public enum Status
        {
            ENABLED,
            REMOVED,
            UNKNOWN
        }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("sharedSetName")]
        public string sharedSetName { get; set; }

        [ReportColumn("sharedSetType")]
        public SharedSetType sharedSetType { get; set; }

        [ReportColumn("state")]
        public Status status { get; set; }
    }

    public class SharedSetCriteriaReportReportRow
    {
        public enum KeywordMatchType
        {
            BROAD,
            EXACT,
            PHRASE
        }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("negativeKeyword")]
        public string criteria { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("keywordID")]
        public long id { get; set; }

        [ReportColumn("matchType")]
        public KeywordMatchType keywordMatchType { get; set; }

        [ReportColumn("sharedSetID")]
        public long sharedSetId { get; set; }
    }

    public class CreativeConversionReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("freeClickType")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("freeClickRate")]
        public double creativeConversionRate { get; set; }

        [ReportColumn("freeClicks")]
        public long creativeConversions { get; set; }

        [ReportColumn("adID")]
        public long creativeId { get; set; }

        [ReportColumn("keywordPlacement")]
        public string criteriaParameters { get; set; }

        [ReportColumn("matchType")]
        public string criteriaTypeName { get; set; }

        [ReportColumn("keywordID")]
        public long criterionId { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class CallMetricsCallDetailsReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum CallStatus
        {
            MISSED,
            RECEIVED,
            UNKNOWN
        }

        public enum CallTrackingDisplayLocation
        {
            AD,
            LANDING_PAGE,
            UNKNOWN
        }

        public enum CallType
        {
            GOOGLE_SEARCH,
            HIGH_END_MOBILE_SEARCH,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("durationSeconds")]
        public long callDuration { get; set; }

        [ReportColumn("endTime")]
        public long callEndTime { get; set; }

        [ReportColumn("callerCountryCode")]
        public string callerCountryCallingCode { get; set; }

        [ReportColumn("callerAreaCode")]
        public string callerNationalDesignatedCode { get; set; }

        [ReportColumn("startTime")]
        public long callStartTime { get; set; }

        [ReportColumn("status")]
        public CallStatus callStatus { get; set; }

        [ReportColumn("callSource")]
        public CallTrackingDisplayLocation callTrackingDisplayLocation { get; set; }

        [ReportColumn("callType")]
        public CallType callType { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("hourOfDay")]
        public long hourOfDay { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class KeywordlessQueryReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum QueryTargetingStatus
        {
            ADDED,
            BOTH,
            EXCLUDED,
            NONE
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("categories")]
        public string categoryPaths { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("keywordID")]
        public long criterionId { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("domain")]
        public string domain { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("dynamicallyGeneratedHeadline")]
        public string line1 { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("searchTerm")]
        public string query { get; set; }

        [ReportColumn("addedExcluded")]
        public QueryTargetingStatus queryTargetingStatus { get; set; }

        [ReportColumn("landingPageTitle")]
        public string title { get; set; }

        [ReportColumn("url")]
        public string url { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class KeywordlessCategoryReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("topLevelCategories")]
        public string category0 { get; set; }

        [ReportColumn("firstLevelSubCategories")]
        public string category1 { get; set; }

        [ReportColumn("secondLevelSubCategories")]
        public string category2 { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("keywordID")]
        public long criterionId { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("domain")]
        public string domain { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class CriteriaPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum ApprovalStatus
        {
            APPROVED,
            DISAPPROVED,
            PENDING_REVIEW,
            UNDER_REVIEW
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum ConversionLagBucket
        {
            CONVERSION_0_TO_1_DAY,
            CONVERSION_1_TO_2_DAYS,
            CONVERSION_10_TO_11_DAYS,
            CONVERSION_11_TO_12_DAYS,
            CONVERSION_12_TO_13_DAYS,
            CONVERSION_13_TO_14_DAYS,
            CONVERSION_14_TO_21_DAYS,
            CONVERSION_2_TO_3_DAYS,
            CONVERSION_21_TO_30_DAYS,
            CONVERSION_3_TO_4_DAYS,
            CONVERSION_30_TO_45_DAYS,
            CONVERSION_4_TO_5_DAYS,
            CONVERSION_45_TO_60_DAYS,
            CONVERSION_5_TO_6_DAYS,
            CONVERSION_6_TO_7_DAYS,
            CONVERSION_60_TO_90_DAYS,
            CONVERSION_7_TO_8_DAYS,
            CONVERSION_8_TO_9_DAYS,
            CONVERSION_9_TO_10_DAYS,
            UNKNOWN
        }

        public enum CpcBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CpvBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CreativeQualityScore
        {
            ABOVE_AVERAGE,
            AVERAGE,
            BELOW_AVERAGE,
            UNKNOWN
        }

        public enum CriteriaType
        {
            AD_SCHEDULE,
            AGE_RANGE,
            APP_PAYMENT_MODEL,
            CARRIER,
            CONTENT_LABEL,
            CUSTOM_AFFINITY,
            CUSTOM_INTENT,
            GENDER,
            INCOME_RANGE,
            INTERACTION_TYPE,
            IP_BLOCK,
            KEYWORD,
            LANGUAGE,
            LOCATION,
            LOCATION_GROUPS,
            MOBILE_APP_CATEGORY,
            MOBILE_APPLICATION,
            MOBILE_DEVICE,
            OPERATING_SYSTEM_VERSION,
            PARENT,
            PLACEMENT,
            PLATFORM,
            PREFERRED_CONTENT,
            PRODUCT_PARTITION,
            PRODUCT_SCOPE,
            PROXIMITY,
            RUN_OF_NETWORK,
            UNKNOWN,
            USER_INTEREST,
            USER_LIST,
            VERTICAL,
            WEBPAGE,
            YOUTUBE_CHANNEL,
            YOUTUBE_VIDEO
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum PostClickQualityScore
        {
            ABOVE_AVERAGE,
            AVERAGE,
            BELOW_AVERAGE,
            UNKNOWN
        }

        public enum SearchPredictedCtr
        {
            ABOVE_AVERAGE,
            AVERAGE,
            BELOW_AVERAGE,
            UNKNOWN
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        public enum Status
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        public enum SystemServingStatus
        {
            ELIGIBLE,
            RARELY_SERVED
        }

        [ReportColumn("dynamicAdTarget")]
        public string parameter { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("approvalStatus")]
        public ApprovalStatus approvalStatus { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("daysToConversion")]
        public ConversionLagBucket conversionLagBucket { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("costConvCurrentModel")]
        public double costPerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPCSource")]
        public CpcBidSource cpcBidSource { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("maxCPV")]
        public decimal cpvBid { get; set; }

        [ReportColumn("maxCPVSource")]
        public CpvBidSource cpvBidSource { get; set; }

        [ReportColumn("adRelevance")]
        public CreativeQualityScore creativeQualityScore { get; set; }

        [ReportColumn("keywordPlacement")]
        public string criteria { get; set; }

        [ReportColumn("keywordPlacementDestinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("criteriaType")]
        public CriteriaType criteriaType { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("conversionsCurrentModel")]
        public double currentModelAttributedConversions { get; set; }

        [ReportColumn("convValueCurrentModel")]
        public double currentModelAttributedConversionValue { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("criteriaDisplayName")]
        public string displayName { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("enhancedCPCEnabled")]
        public bool enhancedCpcEnabled { get; set; }

        [ReportColumn("estAddClicksWkFirstPositionBid")]
        public long estimatedAddClicksAtFirstPositionCpc { get; set; }

        [ReportColumn("estAddCostWkFirstPositionBid")]
        public decimal estimatedAddCostAtFirstPositionCpc { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("appFinalURL")]
        public string finalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string finalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrls { get; set; }

        [ReportColumn("finalURLSuffix")]
        public string finalUrlSuffix { get; set; }

        [ReportColumn("firstPageCPC")]
        public string firstPageCpc { get; set; }

        [ReportColumn("firstPositionCPC")]
        public string firstPositionCpc { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("hasQualityScore")]
        public bool hasQualityScore { get; set; }

        [ReportColumn("keywordID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("labelIDs")]
        public string labelIds { get; set; }

        [ReportColumn("labels")]
        public string labels { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("landingPageExperience")]
        public PostClickQualityScore postClickQualityScore { get; set; }

        [ReportColumn("qualityScore")]
        public long qualityScore { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("expectedClickthroughRate")]
        public SearchPredictedCtr searchPredictedCtr { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("keywordPlacementState")]
        public Status status { get; set; }

        [ReportColumn("criterionServingStatus")]
        public SystemServingStatus systemServingStatus { get; set; }

        [ReportColumn("topOfPageCPC")]
        public string topOfPageCpc { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("valueConvCurrentModel")]
        public double valuePerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("verticalID")]
        public long verticalId { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class ClickPerformanceReportReportRow
    {
        public enum AdFormat
        {
            AUDIO,
            COMPOSITE,
            DYNAMIC_IMAGE,
            FLASH,
            HTML,
            IMAGE,
            PRINT,
            TEXT,
            UNKNOWN,
            VIDEO
        }

        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum KeywordMatchType
        {
            BROAD,
            EXACT,
            PHRASE,
            UNKNOWN
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        [ReportColumn("page")]
        public long page { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("adType")]
        public AdFormat adFormat { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("cityLocationOfInterest")]
        public long aoiCityCriteriaId { get; set; }

        [ReportColumn("countryTerritoryLocationOfInterest")]
        public long aoiCountryCriteriaId { get; set; }

        [ReportColumn("metroAreaLocationOfInterest")]
        public long aoiMetroCriteriaId { get; set; }

        [ReportColumn("mostSpecificLocationTargetLocationOfInterest")]
        public long aoiMostSpecificTargetId { get; set; }

        [ReportColumn("regionLocationOfInterest")]
        public long aoiRegionCriteriaId { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaignLocationTarget")]
        public long campaignLocationTargetId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("adID")]
        public long creativeId { get; set; }

        [ReportColumn("keywordID")]
        public long criteriaId { get; set; }

        [ReportColumn("keywordPlacement")]
        public string criteriaParameters { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("googleClickID")]
        public string gclId { get; set; }

        [ReportColumn("matchType")]
        public KeywordMatchType keywordMatchType { get; set; }

        [ReportColumn("cityPhysicalLocation")]
        public long lopCityCriteriaId { get; set; }

        [ReportColumn("countryTerritoryPhysicalLocation")]
        public long lopCountryCriteriaId { get; set; }

        [ReportColumn("metroAreaPhysicalLocation")]
        public long lopMetroCriteriaId { get; set; }

        [ReportColumn("mostSpecificLocationTargetPhysicalLocation")]
        public long lopMostSpecificTargetId { get; set; }

        [ReportColumn("regionPhysicalLocation")]
        public long lopRegionCriteriaId { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("userListID")]
        public long userListId { get; set; }
    }

    public class BudgetPerformanceReportReportRow
    {
        public enum AssociatedCampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum BudgetCampaignAssociationStatus
        {
            ENABLED,
            REMOVED,
            UNKNOWN
        }

        public enum BudgetStatus
        {
            ENABLED,
            REMOVED,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        [ReportColumn("deliveryMethod")]
        public string deliveryMethod { get; set; }

        [ReportColumn("budgetPeriod")]
        public string period { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("budget")]
        public decimal amount { get; set; }

        [ReportColumn("campaignID")]
        public long associatedCampaignId { get; set; }

        [ReportColumn("campaign")]
        public string associatedCampaignName { get; set; }

        [ReportColumn("campaignState")]
        public AssociatedCampaignStatus associatedCampaignStatus { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("budgetUsage")]
        public BudgetCampaignAssociationStatus budgetCampaignAssociationStatus { get; set; }

        [ReportColumn("budgetID")]
        public long budgetId { get; set; }

        [ReportColumn("budgetName")]
        public string budgetName { get; set; }

        [ReportColumn("campaigns")]
        public long budgetReferenceCount { get; set; }

        [ReportColumn("budgetState")]
        public BudgetStatus budgetStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("explicitlyShared")]
        public bool isBudgetExplicitlyShared { get; set; }

        [ReportColumn("totalBudgetAmount")]
        public decimal totalAmount { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }
    }

    public class BidGoalPerformanceReportReportRow
    {
        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Status
        {
            ENABLED,
            REMOVED,
            UNKNOWN
        }

        public enum Type
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        [ReportColumn("adGroups")]
        public long adGroupCount { get; set; }

        [ReportColumn("campaigns")]
        public long campaignCount { get; set; }

        [ReportColumn("nonRemovedAdGroups")]
        public long nonRemovedAdGroupCount { get; set; }

        [ReportColumn("nonRemovedKeywords")]
        public long nonRemovedAdGroupCriteriaCount { get; set; }

        [ReportColumn("nonRemovedCampaigns")]
        public long nonRemovedCampaignCount { get; set; }

        [ReportColumn("bidAutomationTargetSearchPageLocation")]
        public bool pageOnePromotedBidChangesForRaisesOnly { get; set; }

        [ReportColumn("bidAdjustmentTargetSearchPageLocation")]
        public double pageOnePromotedBidModifier { get; set; }

        [ReportColumn("locationTargetSearchPageLocation")]
        public string pageOnePromotedStrategyGoal { get; set; }

        [ReportColumn("targetCPA")]
        public decimal targetCpa { get; set; }

        [ReportColumn("targetROAS")]
        public double targetRoas { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("keywords")]
        public long adGroupCriteriaCount { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("hourOfDay")]
        public long hourOfDay { get; set; }

        [ReportColumn("bidStrategyID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("bidStrategyName")]
        public string name { get; set; }

        [ReportColumn("bidLimitTargetSearchPageLocation")]
        public decimal pageOnePromotedBidCeiling { get; set; }

        [ReportColumn("limitedBudgetsTargetSearchPageLocation")]
        public bool pageOnePromotedRaiseBidWhenBudgetConstrained { get; set; }

        [ReportColumn("lowQualityKeywordsTargetSearchPageLocation")]
        public bool pageOnePromotedRaiseBidWhenLowQualityScore { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("state")]
        public Status status { get; set; }

        [ReportColumn("upperBidLimitTargetCPA")]
        public decimal targetCpaMaxCpcBidCeiling { get; set; }

        [ReportColumn("lowerBidLimitTargetCPA")]
        public decimal targetCpaMaxCpcBidFloor { get; set; }

        [ReportColumn("targetOutrankingShare")]
        public double targetOutrankShare { get; set; }

        [ReportColumn("bidAutomationTargetOutrankingShare")]
        public bool targetOutrankShareBidChangesForRaisesOnly { get; set; }

        [ReportColumn("competitorDomainTargetOutrankingShare")]
        public string targetOutrankShareCompetitorDomain { get; set; }

        [ReportColumn("upperMaxCpcBidLimitTargetOutrankingShare")]
        public decimal targetOutrankShareMaxCpcBidCeiling { get; set; }

        [ReportColumn("lowQualityKeywordsTargetOutrankingShare")]
        public bool targetOutrankShareRaiseBidWhenLowQualityScore { get; set; }

        [ReportColumn("upperBidLimitTargetROAS")]
        public decimal targetRoasBidCeiling { get; set; }

        [ReportColumn("lowerBidLimitTargetROAS")]
        public decimal targetRoasBidFloor { get; set; }

        [ReportColumn("bidLimitMaximizeClicks")]
        public decimal targetSpendBidCeiling { get; set; }

        [ReportColumn("targetSpendMaximizeClicks")]
        public decimal targetSpendSpendTarget { get; set; }

        [ReportColumn("bidStrategyType")]
        public Type type { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class DisplayKeywordPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum CpcBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CpmBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CpvBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Status
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPCSource")]
        public CpcBidSource cpcBidSource { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("maxCPMSource")]
        public CpmBidSource cpmBidSource { get; set; }

        [ReportColumn("maxCPV")]
        public decimal cpvBid { get; set; }

        [ReportColumn("maxCPVSource")]
        public CpvBidSource cpvBidSource { get; set; }

        [ReportColumn("keyword")]
        public string criteria { get; set; }

        [ReportColumn("destinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("appFinalURL")]
        public string finalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string finalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrls { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("keywordID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("isRestricting")]
        public bool isRestrict { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("keywordState")]
        public Status status { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class PlaceholderFeedItemReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum GeoTargetingRestriction
        {
            LOCATION_OF_PRESENCE,
            UNKNOWN
        }

        public enum KeywordMatchType
        {
            BROAD,
            EXACT,
            PHRASE
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        public enum Status
        {
            ENABLED,
            REMOVED,
            UNKNOWN
        }

        [ReportColumn("scheduling")]
        public string scheduling { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("approvalStatus")]
        public string validationDetails { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("adID")]
        public long adId { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("attributeValues")]
        public string attributeValues { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("targetKeywordText")]
        public string criteria { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("devicePreference")]
        public string devicePreference { get; set; }

        [ReportColumn("disapprovalReasons")]
        public string disapprovalShortNames { get; set; }

        [ReportColumn("endDate")]
        public string endTime { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("feedID")]
        public long feedId { get; set; }

        [ReportColumn("itemID")]
        public long feedItemId { get; set; }

        [ReportColumn("targetLocation")]
        public long geoTargetingCriterionId { get; set; }

        [ReportColumn("targetLocationRestriction")]
        public GeoTargetingRestriction geoTargetingRestriction { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("thisExtensionVsOther")]
        public bool isSelfAction { get; set; }

        [ReportColumn("targetKeywordMatchType")]
        public KeywordMatchType keywordMatchType { get; set; }

        [ReportColumn("keywordID")]
        public long keywordTargetingId { get; set; }

        [ReportColumn("targetKeywordMatchType")]
        public string keywordTargetingMatchType { get; set; }

        [ReportColumn("targetKeywordText")]
        public string keywordTargetingText { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("feedPlaceholderType")]
        public long placeholderType { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("startDate")]
        public string startTime { get; set; }

        [ReportColumn("itemState")]
        public Status status { get; set; }

        [ReportColumn("targetAdGroupID")]
        public long targetingAdGroupId { get; set; }

        [ReportColumn("targetCampaignID")]
        public long targetingCampaignId { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class PlacementPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum CpcBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CpmBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Status
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPCSource")]
        public CpcBidSource cpcBidSource { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("maxCPMSource")]
        public CpmBidSource cpmBidSource { get; set; }

        [ReportColumn("placement")]
        public string criteria { get; set; }

        [ReportColumn("destinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("criteriaDisplayName")]
        public string displayName { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("appFinalURL")]
        public string finalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string finalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrls { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("criterionID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("isRestricting")]
        public bool isRestrict { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("placementState")]
        public Status status { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class CampaignNegativeLocationsReportReportRow
    {
        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("location")]
        public long id { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }
    }

    public class GenderPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum CpcBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CpmBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Status
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPCSource")]
        public CpcBidSource cpcBidSource { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("maxCPMSource")]
        public CpmBidSource cpmBidSource { get; set; }

        [ReportColumn("gender")]
        public string criteria { get; set; }

        [ReportColumn("destinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("appFinalURL")]
        public string finalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string finalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrls { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("criterionID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("isRestricting")]
        public bool isRestrict { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("genderState")]
        public Status status { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class AgeRangePerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum CpcBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CpmBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Status
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPCSource")]
        public CpcBidSource cpcBidSource { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("maxCPMSource")]
        public CpmBidSource cpmBidSource { get; set; }

        [ReportColumn("ageRange")]
        public string criteria { get; set; }

        [ReportColumn("destinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("appFinalURL")]
        public string finalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string finalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrls { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("criterionID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("isRestricting")]
        public bool isRestrict { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("ageRangeState")]
        public Status status { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class CampaignLocationTargetReportReportRow
    {
        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("location")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class CampaignAdScheduleTargetReportReportRow
    {
        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("adSchedule")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class PaidOrganicQueryReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum QueryMatchType
        {
            AUTO,
            BROAD,
            EXACT,
            EXPANDED,
            PHRASE
        }

        public enum SerpType
        {
            ADS_AND_ORGANIC,
            ADS_ONLY,
            ORGANIC_ONLY,
            UNKNOWN
        }

        [ReportColumn("keywordID")]
        public string keywordId { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("adAvgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("adAvgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("adClicks")]
        public long clicks { get; set; }

        [ReportColumn("combinedClicks")]
        public long combinedAdsOrganicClicks { get; set; }

        [ReportColumn("combinedClicksQuery")]
        public double combinedAdsOrganicClicksPerQuery { get; set; }

        [ReportColumn("combinedQueries")]
        public long combinedAdsOrganicQueries { get; set; }

        [ReportColumn("adCTR")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("adImpressions")]
        public long impressions { get; set; }

        [ReportColumn("keyword")]
        public string keywordTextMatchingQuery { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("organicAveragePosition")]
        public double organicAveragePosition { get; set; }

        [ReportColumn("organicClicks")]
        public long organicClicks { get; set; }

        [ReportColumn("organicClicksQuery")]
        public double organicClicksPerQuery { get; set; }

        [ReportColumn("organicListings")]
        public long organicImpressions { get; set; }

        [ReportColumn("organicListingsQuery")]
        public double organicImpressionsPerQuery { get; set; }

        [ReportColumn("organicQueries")]
        public long organicQueries { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("matchType")]
        public QueryMatchType queryMatchType { get; set; }

        [ReportColumn("query")]
        public string searchQuery { get; set; }

        [ReportColumn("searchResultType")]
        public SerpType serpType { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class AudiencePerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum CpcBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CpmBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CriterionAttachmentLevel
        {
            ADGROUP,
            CAMPAIGN,
            CUSTOMER,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        public enum Status
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPCSource")]
        public CpcBidSource cpcBidSource { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("maxCPMSource")]
        public CpmBidSource cpmBidSource { get; set; }

        [ReportColumn("audience")]
        public string criteria { get; set; }

        [ReportColumn("destinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("level")]
        public CriterionAttachmentLevel criterionAttachmentLevel { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("appFinalURL")]
        public string finalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string finalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrls { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("criterionID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isRestricting")]
        public bool isRestrict { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("audienceState")]
        public Status status { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("userListName")]
        public string userListName { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class DisplayTopicsPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum CpcBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CpmBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Status
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPCSource")]
        public CpcBidSource cpcBidSource { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("maxCPMSource")]
        public CpmBidSource cpmBidSource { get; set; }

        [ReportColumn("topic")]
        public string criteria { get; set; }

        [ReportColumn("destinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("appFinalURL")]
        public string finalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string finalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrls { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("criterionID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("isRestricting")]
        public bool isRestrict { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("topicState")]
        public Status status { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("verticalID")]
        public long verticalId { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class UserAdDistanceReportReportRow
    {
        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum DistanceBucket
        {
            DISTANCE_BUCKET_BEYOND_40MILES,
            DISTANCE_BUCKET_BEYOND_65KM,
            DISTANCE_BUCKET_WITHIN_0_2MILES,
            DISTANCE_BUCKET_WITHIN_0_5MILES,
            DISTANCE_BUCKET_WITHIN_0_7MILES,
            DISTANCE_BUCKET_WITHIN_10KM,
            DISTANCE_BUCKET_WITHIN_10MILES,
            DISTANCE_BUCKET_WITHIN_15KM,
            DISTANCE_BUCKET_WITHIN_15MILES,
            DISTANCE_BUCKET_WITHIN_1KM,
            DISTANCE_BUCKET_WITHIN_1MILE,
            DISTANCE_BUCKET_WITHIN_200M,
            DISTANCE_BUCKET_WITHIN_20KM,
            DISTANCE_BUCKET_WITHIN_20MILES,
            DISTANCE_BUCKET_WITHIN_25KM,
            DISTANCE_BUCKET_WITHIN_25MILES,
            DISTANCE_BUCKET_WITHIN_30KM,
            DISTANCE_BUCKET_WITHIN_30MILES,
            DISTANCE_BUCKET_WITHIN_35KM,
            DISTANCE_BUCKET_WITHIN_35MILES,
            DISTANCE_BUCKET_WITHIN_40KM,
            DISTANCE_BUCKET_WITHIN_40MILES,
            DISTANCE_BUCKET_WITHIN_45KM,
            DISTANCE_BUCKET_WITHIN_500M,
            DISTANCE_BUCKET_WITHIN_50KM,
            DISTANCE_BUCKET_WITHIN_55KM,
            DISTANCE_BUCKET_WITHIN_5KM,
            DISTANCE_BUCKET_WITHIN_5MILES,
            DISTANCE_BUCKET_WITHIN_60KM,
            DISTANCE_BUCKET_WITHIN_65KM,
            DISTANCE_BUCKET_WITHIN_700M,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("distanceFromLocationExtensions")]
        public DistanceBucket distanceBucket { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class ShoppingPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum Channel
        {
            LOCAL,
            ONLINE,
            UNKNOWN
        }

        public enum ChannelExclusivity
        {
            MULTI_CHANNEL,
            SINGLE_CHANNEL,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum ProductCondition
        {
            NEW,
            REFURBISHED,
            UNKNOWN,
            USED
        }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("mCAId")]
        public long aggregatorId { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("brand")]
        public string brand { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("category1stLevel")]
        public string categoryL1 { get; set; }

        [ReportColumn("category2ndLevel")]
        public string categoryL2 { get; set; }

        [ReportColumn("category3rdLevel")]
        public string categoryL3 { get; set; }

        [ReportColumn("category4thLevel")]
        public string categoryL4 { get; set; }

        [ReportColumn("category5thLevel")]
        public string categoryL5 { get; set; }

        [ReportColumn("channel")]
        public Channel channel { get; set; }

        [ReportColumn("channelExclusivity")]
        public ChannelExclusivity channelExclusivity { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("countryTerritory")]
        public long countryCriteriaId { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("customLabel0")]
        public string customAttribute0 { get; set; }

        [ReportColumn("customLabel1")]
        public string customAttribute1 { get; set; }

        [ReportColumn("customLabel2")]
        public string customAttribute2 { get; set; }

        [ReportColumn("customLabel3")]
        public string customAttribute3 { get; set; }

        [ReportColumn("customLabel4")]
        public string customAttribute4 { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("language")]
        public long languageCriteriaId { get; set; }

        [ReportColumn("mCId")]
        public long merchantId { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("itemId")]
        public string offerId { get; set; }

        [ReportColumn("condition")]
        public ProductCondition productCondition { get; set; }

        [ReportColumn("productTitle")]
        public string productTitle { get; set; }

        [ReportColumn("productType1stLevel")]
        public string productTypeL1 { get; set; }

        [ReportColumn("productType2ndLevel")]
        public string productTypeL2 { get; set; }

        [ReportColumn("productType3rdLevel")]
        public string productTypeL3 { get; set; }

        [ReportColumn("productType4thLevel")]
        public string productTypeL4 { get; set; }

        [ReportColumn("productType5thLevel")]
        public string productTypeL5 { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("searchAbsTopIS")]
        public double searchAbsoluteTopImpressionShare { get; set; }

        [ReportColumn("clickShare")]
        public double searchClickShare { get; set; }

        [ReportColumn("searchImprShare")]
        public double searchImpressionShare { get; set; }

        [ReportColumn("storeId")]
        public string storeId { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class ProductPartitionReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum PartitionType
        {
            SUBDIVISION,
            UNIT,
            UNKNOWN
        }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("benchmarkMaxCPC")]
        public decimal benchmarkAverageMaxCpc { get; set; }

        [ReportColumn("benchmarkCTR")]
        public double benchmarkCtr { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("keywordPlacementDestinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("finalURLSuffix")]
        public string finalUrlSuffix { get; set; }

        [ReportColumn("criterionID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("parentCriterionID")]
        public long parentCriterionId { get; set; }

        [ReportColumn("partitionType")]
        public PartitionType partitionType { get; set; }

        [ReportColumn("productGroup")]
        public string productGroup { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("searchAbsTopIS")]
        public double searchAbsoluteTopImpressionShare { get; set; }

        [ReportColumn("clickShare")]
        public double searchClickShare { get; set; }

        [ReportColumn("searchImprShare")]
        public double searchImpressionShare { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class ParentalStatusPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum BiddingStrategyType
        {
            MANUAL_CPC,
            MANUAL_CPM,
            MANUAL_CPV,
            MAXIMIZE_CONVERSION_VALUE,
            MAXIMIZE_CONVERSIONS,
            NONE,
            PAGE_ONE_PROMOTED,
            TARGET_CPA,
            TARGET_OUTRANK_SHARE,
            TARGET_ROAS,
            TARGET_SPEND,
            UNKNOWN
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum CpcBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum CpmBidSource
        {
            ADGROUP,
            ADGROUP_BIDDING_STRATEGY,
            CAMPAIGN_BIDDING_STRATEGY,
            CRITERION
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Status
        {
            ENABLED,
            PAUSED,
            REMOVED
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("baseAdGroupID")]
        public long baseAdGroupId { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("bidStrategyID")]
        public long biddingStrategyId { get; set; }

        [ReportColumn("bidStrategyName")]
        public string biddingStrategyName { get; set; }

        [ReportColumn("bidStrategyType")]
        public BiddingStrategyType biddingStrategyType { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("maxCPC")]
        public decimal cpcBid { get; set; }

        [ReportColumn("maxCPCSource")]
        public CpcBidSource cpcBidSource { get; set; }

        [ReportColumn("maxCPM")]
        public decimal cpmBid { get; set; }

        [ReportColumn("maxCPMSource")]
        public CpmBidSource cpmBidSource { get; set; }

        [ReportColumn("parentalStatus")]
        public string criteria { get; set; }

        [ReportColumn("destinationURL")]
        public string criteriaDestinationUrl { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("appFinalURL")]
        public string finalAppUrls { get; set; }

        [ReportColumn("mobileFinalURL")]
        public string finalMobileUrls { get; set; }

        [ReportColumn("finalURL")]
        public string finalUrls { get; set; }

        [ReportColumn("gmailForwards")]
        public long gmailForwards { get; set; }

        [ReportColumn("gmailSaves")]
        public long gmailSaves { get; set; }

        [ReportColumn("gmailClicksToWebsite")]
        public long gmailSecondaryClicks { get; set; }

        [ReportColumn("criterionID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }

        [ReportColumn("isRestricting")]
        public bool isRestrict { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("parentalStatusState")]
        public Status status { get; set; }

        [ReportColumn("trackingTemplate")]
        public string trackingUrlTemplate { get; set; }

        [ReportColumn("customParameter")]
        public string urlCustomParameters { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class PlaceholderReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("adID")]
        public long extensionPlaceholderCreativeId { get; set; }

        [ReportColumn("feedPlaceholderType")]
        public string extensionPlaceholderType { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class AdCustomizersFeedItemReportReportRow
    {
        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum FeedItemStatus
        {
            ENABLED,
            REMOVED,
            UNKNOWN
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adID")]
        public long adId { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("feedID")]
        public long feedId { get; set; }

        [ReportColumn("attributeValues")]
        public string feedItemAttributes { get; set; }

        [ReportColumn("endDate")]
        public long feedItemEndTime { get; set; }

        [ReportColumn("itemID")]
        public long feedItemId { get; set; }

        [ReportColumn("startDate")]
        public long feedItemStartTime { get; set; }

        [ReportColumn("itemState")]
        public FeedItemStatus feedItemStatus { get; set; }

        [ReportColumn("targetLocation")]
        public long geoTargetingCriterionId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("keywordID")]
        public long keywordTargetingId { get; set; }

        [ReportColumn("targetKeywordMatchType")]
        public string keywordTargetingMatchType { get; set; }

        [ReportColumn("targetKeywordText")]
        public string keywordTargetingText { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("targetAdGroupID")]
        public long targetingAdGroupId { get; set; }

        [ReportColumn("targetCampaignID")]
        public long targetingCampaignId { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class LabelReportReportRow
    {
        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("adGroupCreativesCount")]
        public long adGroupCreativesCount { get; set; }

        [ReportColumn("adGroupCriteriaCount")]
        public long adGroupCriteriaCount { get; set; }

        [ReportColumn("adGroupsCount")]
        public long adGroupsCount { get; set; }

        [ReportColumn("campaignsCount")]
        public long campaignsCount { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("labelID")]
        public long labelId { get; set; }

        [ReportColumn("labelName")]
        public string labelName { get; set; }

        [ReportColumn("userListsCount")]
        public long userListsCount { get; set; }
    }

    public class FinalUrlReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdvertisingChannelType
        {
            DISPLAY,
            EXPRESS,
            MULTI_CHANNEL,
            SEARCH,
            SHOPPING,
            UNKNOWN,
            VIDEO
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("advertisingChannel")]
        public AdvertisingChannelType advertisingChannelType { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("keywordPlacement")]
        public string criteriaParameters { get; set; }

        [ReportColumn("matchType")]
        public string criteriaTypeName { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("finalURL")]
        public string effectiveFinalUrl { get; set; }

        [ReportColumn("trackingTemplate")]
        public string effectiveTrackingUrlTemplate { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class VideoPerformanceReportReportRow
    {
        public enum AdGroupStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum ClickType
        {
            APP_DEEPLINK,
            BREADCRUMBS,
            BROADBAND_PLAN,
            CALL_TRACKING,
            CALLS,
            CLICK_ON_ENGAGEMENT_AD,
            GET_DIRECTIONS,
            LOCATION_EXPANSION,
            LOCATION_FORMAT_CALL,
            LOCATION_FORMAT_DIRECTIONS,
            LOCATION_FORMAT_IMAGE,
            LOCATION_FORMAT_LANDING_PAGE,
            LOCATION_FORMAT_MAP,
            LOCATION_FORMAT_STORE_INFO,
            LOCATION_FORMAT_TEXT,
            MOBILE_CALL_TRACKING,
            OFFER_PRINTS,
            OTHER,
            PRICE_EXTENSION,
            PRODUCT_EXTENSION_CLICKS,
            PRODUCT_LISTING_AD_CLICKS,
            PROMOTION_EXTENSION,
            SHOWCASE_AD_CATEGORY_LINK,
            SHOWCASE_AD_LOCAL_PRODUCT_LINK,
            SHOWCASE_AD_LOCAL_STOREFRONT_LINK,
            SHOWCASE_AD_ONLINE_PRODUCT_LINK,
            SITELINKS,
            STORE_LOCATOR,
            SWIPEABLE_GALLERY_AD_HEADLINE,
            SWIPEABLE_GALLERY_AD_SEE_MORE,
            SWIPEABLE_GALLERY_AD_SITELINK_FIVE,
            SWIPEABLE_GALLERY_AD_SITELINK_FOUR,
            SWIPEABLE_GALLERY_AD_SITELINK_ONE,
            SWIPEABLE_GALLERY_AD_SITELINK_THREE,
            SWIPEABLE_GALLERY_AD_SITELINK_TWO,
            SWIPEABLE_GALLERY_AD_SWIPES,
            UNKNOWN,
            URL_CLICKS,
            VIDEO_APP_STORE_CLICKS,
            VIDEO_CALL_TO_ACTION_CLICKS,
            VIDEO_CARD_ACTION_HEADLINE_CLICKS,
            VIDEO_END_CAP_CLICKS,
            VIDEO_WEBSITE_CLICKS,
            VISUAL_SITELINKS,
            WIRELESS_PLAN
        }

        public enum CreativeStatus
        {
            DISABLED,
            ENABLED,
            PAUSED
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("videoChannelId")]
        public string videoChannelId { get; set; }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("adGroupState")]
        public AdGroupStatus adGroupStatus { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCPM")]
        public double averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("clickType")]
        public ClickType clickType { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("adID")]
        public long creativeId { get; set; }

        [ReportColumn("adState")]
        public CreativeStatus creativeStatus { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("videoDuration")]
        public long videoDuration { get; set; }

        [ReportColumn("videoId")]
        public string videoId { get; set; }

        [ReportColumn("videoPlayedTo100")]
        public double videoQuartile100Rate { get; set; }

        [ReportColumn("videoPlayedTo25")]
        public double videoQuartile25Rate { get; set; }

        [ReportColumn("videoPlayedTo50")]
        public double videoQuartile50Rate { get; set; }

        [ReportColumn("videoPlayedTo75")]
        public double videoQuartile75Rate { get; set; }

        [ReportColumn("videoTitle")]
        public string videoTitle { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class TopContentPerformanceReportReportRow
    {
        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("bidAdj")]
        public double bidModifier { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("criterionID")]
        public long id { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }
    }

    public class CampaignCriteriaReportReportRow
    {
        public enum CampaignStatus
        {
            ENABLED,
            PAUSED,
            REMOVED,
            UNKNOWN
        }

        public enum CriteriaType
        {
            AD_SCHEDULE,
            AGE_RANGE,
            APP_PAYMENT_MODEL,
            CARRIER,
            CONTENT_LABEL,
            CUSTOM_AFFINITY,
            CUSTOM_INTENT,
            GENDER,
            INCOME_RANGE,
            INTERACTION_TYPE,
            IP_BLOCK,
            KEYWORD,
            LANGUAGE,
            LOCATION,
            LOCATION_GROUPS,
            MOBILE_APP_CATEGORY,
            MOBILE_APPLICATION,
            MOBILE_DEVICE,
            OPERATING_SYSTEM_VERSION,
            PARENT,
            PLACEMENT,
            PLATFORM,
            PREFERRED_CONTENT,
            PRODUCT_PARTITION,
            PRODUCT_SCOPE,
            PROXIMITY,
            RUN_OF_NETWORK,
            UNKNOWN,
            USER_INTEREST,
            USER_LIST,
            VERTICAL,
            WEBPAGE,
            YOUTUBE_CHANNEL,
            YOUTUBE_VIDEO
        }

        [ReportColumn("currency")]
        public string accountCurrencyCode { get; set; }

        [ReportColumn("account")]
        public string accountDescriptiveName { get; set; }

        [ReportColumn("timeZone")]
        public string accountTimeZone { get; set; }

        [ReportColumn("baseCampaignID")]
        public long baseCampaignId { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("campaignState")]
        public CampaignStatus campaignStatus { get; set; }

        [ReportColumn("criterion")]
        public string criteria { get; set; }

        [ReportColumn("criteriaType")]
        public CriteriaType criteriaType { get; set; }

        [ReportColumn("clientName")]
        public string customerDescriptiveName { get; set; }

        [ReportColumn("customerID")]
        public long externalCustomerId { get; set; }

        [ReportColumn("criterionID")]
        public long id { get; set; }

        [ReportColumn("isNegative")]
        public bool isNegative { get; set; }
    }

    public class CampaignGroupPerformanceReportReportRow
    {
        public enum ExternalConversionSource
        {
            AD_CALL_METRICS,
            ANALYTICS,
            ANDROID_DOWNLOAD,
            ANDROID_FIRST_OPEN,
            ANDROID_IN_APP,
            APP_UNSPECIFIED,
            CLICK_TO_CALL,
            ENGAGEMENT,
            FIREBASE,
            GOOGLE_ATTRIBUTION,
            GOOGLE_PLAY,
            IOS_FIRST_OPEN,
            IOS_IN_APP,
            OFFERS,
            SALESFORCE,
            STORE_SALES_CRM,
            STORE_SALES_DIRECT,
            STORE_SALES_PAYMENT_NETWORK,
            STORE_VISITS,
            THIRD_PARTY_APP_ANALYTICS,
            UNKNOWN,
            UPLOAD,
            UPLOAD_CALLS,
            WEBPAGE,
            WEBSITE_CALL_METRICS
        }

        public enum Status
        {
            ENABLED,
            REMOVED,
            UNKNOWN
        }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("allConvRate")]
        public double allConversionRate { get; set; }

        [ReportColumn("allConv")]
        public double allConversions { get; set; }

        [ReportColumn("allConvValue")]
        public double allConversionValue { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPE")]
        public double averageCpe { get; set; }

        [ReportColumn("avgCPV")]
        public double averageCpv { get; set; }

        [ReportColumn("conversionCategory")]
        public string conversionCategoryName { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("conversionTrackerId")]
        public long conversionTrackerId { get; set; }

        [ReportColumn("conversionName")]
        public string conversionTypeName { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("costAllConv")]
        public decimal costPerAllConversion { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("costConvCurrentModel")]
        public double costPerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("crossDeviceConv")]
        public double crossDeviceConversions { get; set; }

        [ReportColumn("conversionsCurrentModel")]
        public double currentModelAttributedConversions { get; set; }

        [ReportColumn("convValueCurrentModel")]
        public double currentModelAttributedConversionValue { get; set; }

        [ReportColumn("engagementRate")]
        public double engagementRate { get; set; }

        [ReportColumn("engagements")]
        public long engagements { get; set; }

        [ReportColumn("conversionSource")]
        public ExternalConversionSource externalConversionSource { get; set; }

        [ReportColumn("campaignGroupID")]
        public long id { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("campaignGroupName")]
        public string name { get; set; }

        [ReportColumn("campaignGroupStatus")]
        public Status status { get; set; }

        [ReportColumn("valueAllConv")]
        public double valuePerAllConversion { get; set; }

        [ReportColumn("valueConvCurrentModel")]
        public double valuePerCurrentModelAttributedConversion { get; set; }

        [ReportColumn("viewRate")]
        public double videoViewRate { get; set; }

        [ReportColumn("views")]
        public long videoViews { get; set; }

        [ReportColumn("viewThroughConv")]
        public long viewThroughConversions { get; set; }
    }

    public class LandingPageReportReportRow
    {
        public enum AdNetworkType1
        {
            CONTENT,
            MIXED,
            SEARCH,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum AdNetworkType2
        {
            CONTENT,
            MIXED,
            SEARCH,
            SEARCH_PARTNERS,
            UNKNOWN,
            YOUTUBE_SEARCH,
            YOUTUBE_WATCH
        }

        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum Device
        {
            DESKTOP,
            HIGH_END_MOBILE,
            TABLET,
            UNKNOWN
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        public enum Slot
        {
            AfsOther,
            AfsTop,
            Content,
            Mixed,
            SearchOther,
            SearchRhs,
            SearchTop,
            Unknown
        }

        [ReportColumn("activeViewAvgCPM")]
        public decimal activeViewCpm { get; set; }

        [ReportColumn("activeViewViewableCTR")]
        public double activeViewCtr { get; set; }

        [ReportColumn("activeViewViewableImpressions")]
        public long activeViewImpressions { get; set; }

        [ReportColumn("activeViewMeasurableImprImpr")]
        public double activeViewMeasurability { get; set; }

        [ReportColumn("activeViewMeasurableCost")]
        public decimal activeViewMeasurableCost { get; set; }

        [ReportColumn("activeViewMeasurableImpr")]
        public long activeViewMeasurableImpressions { get; set; }

        [ReportColumn("activeViewViewableImprMeasurableImpr")]
        public double activeViewViewability { get; set; }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("adGroup")]
        public string adGroupName { get; set; }

        [ReportColumn("network")]
        public AdNetworkType1 adNetworkType1 { get; set; }

        [ReportColumn("networkWithSearchPartners")]
        public AdNetworkType2 adNetworkType2 { get; set; }

        [ReportColumn("avgCost")]
        public decimal averageCost { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("avgPosition")]
        public double averagePosition { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("campaign")]
        public string campaignName { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("device")]
        public Device device { get; set; }

        [ReportColumn("expandedLandingPage")]
        public string expandedFinalUrlString { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("interactionRate")]
        public double interactionRate { get; set; }

        [ReportColumn("interactions")]
        public long interactions { get; set; }

        [ReportColumn("interactionTypes")]
        public string interactionTypes { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("topVsOther")]
        public Slot slot { get; set; }

        [ReportColumn("landingPage")]
        public string unexpandedFinalUrlString { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }

    public class MarketplacePerformanceReportReportRow
    {
        public enum DayOfWeek
        {
            FRIDAY,
            MONDAY,
            SATURDAY,
            SUNDAY,
            THURSDAY,
            TUESDAY,
            WEDNESDAY
        }

        public enum MonthOfYear
        {
            APRIL,
            AUGUST,
            DECEMBER,
            FEBRUARY,
            JANUARY,
            JULY,
            JUNE,
            MARCH,
            MAY,
            NOVEMBER,
            OCTOBER,
            SEPTEMBER
        }

        [ReportColumn("adGroupID")]
        public long adGroupId { get; set; }

        [ReportColumn("avgCPC")]
        public decimal averageCpc { get; set; }

        [ReportColumn("avgCPM")]
        public decimal averageCpm { get; set; }

        [ReportColumn("campaignID")]
        public long campaignId { get; set; }

        [ReportColumn("clicks")]
        public long clicks { get; set; }

        [ReportColumn("convRate")]
        public double conversionRate { get; set; }

        [ReportColumn("conversions")]
        public double conversions { get; set; }

        [ReportColumn("totalConvValue")]
        public double conversionValue { get; set; }

        [ReportColumn("cost")]
        public decimal cost { get; set; }

        [ReportColumn("costConv")]
        public decimal costPerConversion { get; set; }

        [ReportColumn("ctr")]
        public double ctr { get; set; }

        [ReportColumn("day")]
        public string date { get; set; }

        [ReportColumn("dayOfWeek")]
        public DayOfWeek dayOfWeek { get; set; }

        [ReportColumn("impressions")]
        public long impressions { get; set; }

        [ReportColumn("month")]
        public string month { get; set; }

        [ReportColumn("monthOfYear")]
        public MonthOfYear monthOfYear { get; set; }

        [ReportColumn("quarter")]
        public string quarter { get; set; }

        [ReportColumn("valueConv")]
        public double valuePerConversion { get; set; }

        [ReportColumn("week")]
        public string week { get; set; }

        [ReportColumn("year")]
        public long year { get; set; }
    }
}
#pragma warning restore 1591

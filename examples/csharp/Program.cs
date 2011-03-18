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

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using SamplePair = System.Collections.Generic.KeyValuePair<string,
    Google.Api.Ads.AdWords.Examples.CSharp.SampleBase>;
using System.Threading;

namespace Google.Api.Ads.AdWords.Examples.CSharp {
  /// <summary>
  /// The Main class for this application.
  /// </summary>
  class Program {
    /// <summary>
    /// A map to hold the code examples to be executed.
    /// </summary>
    static List<SamplePair> sampleMap = new List<SamplePair>();

    /// <summary>
    /// A flag to keep track of whether help message was shown earlier.
    /// </summary>
    private static bool helpShown = false;

    /// <summary>
    /// Registers the code example.
    /// </summary>
    /// <param name="key">The code example name.</param>
    /// <param name="value">The code example instance.</param>
    static void RegisterSample(string key, SampleBase value) {
      sampleMap.Add(new SamplePair(key, value));
    }

    /// <summary>
    /// Static constructor to initialize the sample map.
    /// </summary>
    static Program() {
      // Add v13 samples.
      RegisterSample("v13.AccountServiceDemo", new v13.AccountServiceDemo());
      RegisterSample("v13.AccountServiceNoConfigDemo", new v13.AccountServiceNoConfigDemo());
      RegisterSample("v13.KeywordEstimateDemo", new v13.KeywordEstimateDemo());
      RegisterSample("v13.CheckKeywordTrafficDemo", new v13.CheckKeywordTrafficDemo());
      RegisterSample("v13.ReportServiceKeywordDemo", new v13.ReportServiceKeywordDemo());
      RegisterSample("v13.ReportServiceStructureDemo", new v13.ReportServiceStructureDemo());
      RegisterSample("v13.DownloadReportAsCsvDemo", new v13.DownloadReportAsCsvDemo());
      RegisterSample("v13.DownloadReportAsXmlDemo", new v13.DownloadReportAsXmlDemo());

      // Add v200909 samples.
      RegisterSample("v200909.AddCampaign", new v200909.AddCampaign());
      RegisterSample("v200909.UpdateCampaign", new v200909.UpdateCampaign());
      RegisterSample("v200909.GetAllCampaigns", new v200909.GetAllCampaigns());
      RegisterSample("v200909.GetCampaign", new v200909.GetCampaign());
      RegisterSample("v200909.GetAllPausedCampaigns", new v200909.GetAllPausedCampaigns());
      RegisterSample("v200909.CheckCampaigns", new v200909.CheckCampaigns());
      RegisterSample("v200909.DeleteCampaign", new v200909.DeleteCampaign());

      RegisterSample("v200909.SetCampaignTargets", new v200909.SetCampaignTargets());
      RegisterSample("v200909.GetAllCampaignTargets", new v200909.GetAllCampaignTargets());

      RegisterSample("v200909.AddAdGroup", new v200909.AddAdGroup());
      RegisterSample("v200909.UpdateAdGroup", new v200909.UpdateAdGroup());
      RegisterSample("v200909.GetAllAdGroups", new v200909.GetAllAdGroups());
      RegisterSample("v200909.DeleteAdGroup", new v200909.DeleteAdGroup());

      RegisterSample("v200909.AddAds", new v200909.AddAds());
      RegisterSample("v200909.UpdateAd", new v200909.UpdateAd());
      RegisterSample("v200909.AddMobileImageAd", new v200909.AddMobileImageAd());
      RegisterSample("v200909.GetAllAds", new v200909.GetAllAds());
      RegisterSample("v200909.GetAllDisapprovedAds", new v200909.GetAllDisapprovedAds());
      RegisterSample("v200909.DeleteAd", new v200909.DeleteAd());

      RegisterSample("v200909.AddCampaignAdExtension", new v200909.AddCampaignAdExtension());
      RegisterSample("v200909.PerformBulkMutateJob", new v200909.PerformBulkMutateJob());
      RegisterSample("v200909.GetAllCampaignAdExtensions",
          new v200909.GetAllCampaignAdExtensions());

      RegisterSample("v200909.AddAdGroupCriteria", new v200909.AddAdGroupCriteria());
      RegisterSample("v200909.UpdateAdGroupCriterion", new v200909.UpdateAdGroupCriterion());
      RegisterSample("v200909.GetAllAdGroupCriteria", new v200909.GetAllAdGroupCriteria());
      RegisterSample("v200909.GetAllActiveAdGroupCriteria",
          new v200909.GetAllActiveAdGroupCriteria());
      RegisterSample("v200909.DeleteAdGroupCriterion", new v200909.DeleteAdGroupCriterion());

      RegisterSample("v200909.AddNegativeCampaignCriterion",
          new v200909.AddNegativeCampaignCriterion());

      RegisterSample("v200909.GetRelatedKeywords", new v200909.GetRelatedKeywords());
      RegisterSample("v200909.GetRelatedPlacements", new v200909.GetRelatedPlacements());

      RegisterSample("v200909.AddAdExtensionOverride", new v200909.AddAdExtensionOverride());
      RegisterSample("v200909.GetAllAdExtensionOverrides",
          new v200909.GetAllAdExtensionOverrides());
      RegisterSample("v200909.SetAdParams", new v200909.SetAdParams());
      RegisterSample("v200909.GetGeoLocationInfo", new v200909.GetGeoLocationInfo());
      RegisterSample("v200909.GetConversionOptimizerEligibility",
          new v200909.GetConversionOptimizerEligibility());

      RegisterSample("v200909.GetTotalUsageUnitsPerMonth",
          new v200909.GetTotalUsageUnitsPerMonth());
      RegisterSample("v200909.GetOperationCount", new v200909.GetOperationCount());
      RegisterSample("v200909.GetUnitCount", new v200909.GetUnitCount());
      RegisterSample("v200909.GetMethodCost", new v200909.GetMethodCost());

      RegisterSample("v200909.BackupSandboxDemo", new v200909.BackupSandboxDemo());
      RegisterSample("v200909.RestoreSandboxDemo", new v200909.RestoreSandboxDemo());
      RegisterSample("v200909.MethodApiUnitsUsageDemo", new v200909.MethodApiUnitsUsageDemo());

      // Add v201003 samples.
      RegisterSample("v201003.AddCampaign", new v201003.AddCampaign());
      RegisterSample("v201003.UpdateCampaign", new v201003.UpdateCampaign());
      RegisterSample("v201003.GetAllCampaigns", new v201003.GetAllCampaigns());
      RegisterSample("v201003.GetCampaign", new v201003.GetCampaign());
      RegisterSample("v201003.GetAllPausedCampaigns", new v201003.GetAllPausedCampaigns());
      RegisterSample("v201003.CheckCampaigns", new v201003.CheckCampaigns());
      RegisterSample("v201003.DeleteCampaign", new v201003.DeleteCampaign());

      RegisterSample("v201003.SetCampaignTargets", new v201003.SetCampaignTargets());
      RegisterSample("v201003.GetAllCampaignTargets", new v201003.GetAllCampaignTargets());

      RegisterSample("v201003.AddAdGroup", new v201003.AddAdGroup());
      RegisterSample("v201003.UpdateAdGroup", new v201003.UpdateAdGroup());
      RegisterSample("v201003.GetAllAdGroups", new v201003.GetAllAdGroups());
      RegisterSample("v201003.DeleteAdGroup", new v201003.DeleteAdGroup());

      RegisterSample("v201003.AddAds", new v201003.AddAds());
      RegisterSample("v201003.HandlePolicyViolationError",
          new v201003.HandlePolicyViolationError());
      RegisterSample("v201003.UpdateAd", new v201003.UpdateAd());
      RegisterSample("v201003.AddMobileImageAd", new v201003.AddMobileImageAd());
      RegisterSample("v201003.GetAllAds", new v201003.GetAllAds());
      RegisterSample("v201003.GetAllDisapprovedAds", new v201003.GetAllDisapprovedAds());
      RegisterSample("v201003.DeleteAd", new v201003.DeleteAd());

      RegisterSample("v201003.AddCampaignAdExtension", new v201003.AddCampaignAdExtension());
      RegisterSample("v201003.PerformBulkMutateJob", new v201003.PerformBulkMutateJob());
      RegisterSample("v201003.GetAllCampaignAdExtensions",
          new v201003.GetAllCampaignAdExtensions());

      RegisterSample("v201003.AddAdGroupCriteria", new v201003.AddAdGroupCriteria());
      RegisterSample("v201003.UpdateAdGroupCriterion", new v201003.UpdateAdGroupCriterion());
      RegisterSample("v201003.GetAllAdGroupCriteria", new v201003.GetAllAdGroupCriteria());
      RegisterSample("v201003.GetAllActiveAdGroupCriteria",
          new v201003.GetAllActiveAdGroupCriteria());
      RegisterSample("v201003.DeleteAdGroupCriterion", new v201003.DeleteAdGroupCriterion());

      RegisterSample("v201003.AddNegativeCampaignCriterion",
          new v201003.AddNegativeCampaignCriterion());
      RegisterSample("v201003.GetAllNegativeCampaignCriteria",
          new v201003.GetAllNegativeCampaignCriteria());

      RegisterSample("v201003.GetRelatedKeywords", new v201003.GetRelatedKeywords());
      RegisterSample("v201003.GetRelatedPlacements", new v201003.GetRelatedPlacements());

      RegisterSample("v201003.AddAdExtensionOverride", new v201003.AddAdExtensionOverride());
      RegisterSample("v201003.GetAllAdExtensionOverrides",
          new v201003.GetAllAdExtensionOverrides());
      RegisterSample("v201003.SetAdParams", new v201003.SetAdParams());
      RegisterSample("v201003.GetGeoLocationInfo", new v201003.GetGeoLocationInfo());
      RegisterSample("v201003.GetConversionOptimizerEligibility",
          new v201003.GetConversionOptimizerEligibility());

      RegisterSample("v201003.GetTotalUsageUnitsPerMonth",
          new v201003.GetTotalUsageUnitsPerMonth());
      RegisterSample("v201003.GetOperationCount", new v201003.GetOperationCount());
      RegisterSample("v201003.GetUnitCount", new v201003.GetUnitCount());
      RegisterSample("v201003.GetMethodCost", new v201003.GetMethodCost());

      RegisterSample("v201003.BackupSandboxDemo", new v201003.BackupSandboxDemo());
      RegisterSample("v201003.RestoreSandboxDemo", new v201003.RestoreSandboxDemo());
      RegisterSample("v201003.MethodApiUnitsUsageDemo", new v201003.MethodApiUnitsUsageDemo());

      RegisterSample("v201003.GetCriterionBidLandscape", new v201003.GetCriterionBidLandscape());
      RegisterSample("v201003.GetAllReportDefinitions", new v201003.GetAllReportDefinitions());
      RegisterSample("v201003.GetReportFields", new v201003.GetReportFields());
      RegisterSample("v201003.DownloadReport", new v201003.DownloadReport());
      RegisterSample("v201003.AddKeywordsPerformanceReportDefinition",
          new v201003.AddKeywordsPerformanceReportDefinition());

      RegisterSample("v201003.UploadImage", new v201003.UploadImage());
      RegisterSample("v201003.GetAllImages", new v201003.GetAllImages());
      RegisterSample("v201003.GetAllVideos", new v201003.GetAllVideos());

      RegisterSample("v201003.AddSiteLinks", new v201003.AddSiteLinks());
      RegisterSample("v201003.DeleteSitelinks", new v201003.DeleteSitelinks());

      // Add v201008 samples.
      RegisterSample("v201008.AddCampaign", new v201008.AddCampaign());
      RegisterSample("v201008.UpdateCampaign", new v201008.UpdateCampaign());
      RegisterSample("v201008.GetAllCampaigns", new v201008.GetAllCampaigns());
      RegisterSample("v201008.GetCampaign", new v201008.GetCampaign());
      RegisterSample("v201008.GetAllPausedCampaigns", new v201008.GetAllPausedCampaigns());
      RegisterSample("v201008.CheckCampaigns", new v201008.CheckCampaigns());
      RegisterSample("v201008.DeleteCampaign", new v201008.DeleteCampaign());

      RegisterSample("v201008.SetCampaignTargets", new v201008.SetCampaignTargets());
      RegisterSample("v201008.GetAllCampaignTargets", new v201008.GetAllCampaignTargets());

      RegisterSample("v201008.AddAdGroup", new v201008.AddAdGroup());
      RegisterSample("v201008.UpdateAdGroup", new v201008.UpdateAdGroup());
      RegisterSample("v201008.GetAllAdGroups", new v201008.GetAllAdGroups());
      RegisterSample("v201008.DeleteAdGroup", new v201008.DeleteAdGroup());

      RegisterSample("v201008.AddAds", new v201008.AddAds());
      RegisterSample("v201008.HandlePolicyViolationError",
          new v201008.HandlePolicyViolationError());
      RegisterSample("v201008.UpdateAd", new v201008.UpdateAd());
      RegisterSample("v201008.AddMobileImageAd", new v201008.AddMobileImageAd());
      RegisterSample("v201008.GetAllAds", new v201008.GetAllAds());
      RegisterSample("v201008.GetAllDisapprovedAds", new v201008.GetAllDisapprovedAds());
      RegisterSample("v201008.DeleteAd", new v201008.DeleteAd());

      RegisterSample("v201008.AddCampaignAdExtension", new v201008.AddCampaignAdExtension());
      RegisterSample("v201008.PerformBulkMutateJob", new v201008.PerformBulkMutateJob());
      RegisterSample("v201008.GetAllBulkMutateJobs", new v201008.GetAllBulkMutateJobs());
      RegisterSample("v201008.DeleteBulkMutateJob", new v201008.DeleteBulkMutateJob());

      RegisterSample("v201008.GetAllCampaignAdExtensions",
          new v201008.GetAllCampaignAdExtensions());

      RegisterSample("v201008.AddAdGroupCriteria", new v201008.AddAdGroupCriteria());
      RegisterSample("v201008.UpdateAdGroupCriterion", new v201008.UpdateAdGroupCriterion());
      RegisterSample("v201008.GetAllAdGroupCriteria", new v201008.GetAllAdGroupCriteria());
      RegisterSample("v201008.GetAllActiveAdGroupCriteria",
          new v201008.GetAllActiveAdGroupCriteria());
      RegisterSample("v201008.DeleteAdGroupCriterion", new v201008.DeleteAdGroupCriterion());

      RegisterSample("v201008.AddNegativeCampaignCriterion",
          new v201008.AddNegativeCampaignCriterion());
      RegisterSample("v201008.GetAllNegativeCampaignCriteria",
          new v201008.GetAllNegativeCampaignCriteria());

      RegisterSample("v201008.GetRelatedKeywords", new v201008.GetRelatedKeywords());
      RegisterSample("v201008.GetRelatedPlacements", new v201008.GetRelatedPlacements());

      RegisterSample("v201008.AddAdExtensionOverride", new v201008.AddAdExtensionOverride());
      RegisterSample("v201008.GetAllAdExtensionOverrides",
          new v201008.GetAllAdExtensionOverrides());
      RegisterSample("v201008.SetAdParams", new v201008.SetAdParams());
      RegisterSample("v201008.GetGeoLocationInfo", new v201008.GetGeoLocationInfo());
      RegisterSample("v201008.GetConversionOptimizerEligibility",
          new v201008.GetConversionOptimizerEligibility());

      RegisterSample("v201008.GetTotalUsageUnitsPerMonth",
          new v201008.GetTotalUsageUnitsPerMonth());
      RegisterSample("v201008.GetOperationCount", new v201008.GetOperationCount());
      RegisterSample("v201008.GetUnitCount", new v201008.GetUnitCount());
      RegisterSample("v201008.GetMethodCost", new v201008.GetMethodCost());

      RegisterSample("v201008.BackupSandboxDemo", new v201008.BackupSandboxDemo());
      RegisterSample("v201008.RestoreSandboxDemo", new v201008.RestoreSandboxDemo());
      RegisterSample("v201008.MethodApiUnitsUsageDemo", new v201008.MethodApiUnitsUsageDemo());

      RegisterSample("v201008.GetCriterionBidLandscape", new v201008.GetCriterionBidLandscape());
      RegisterSample("v201008.GetAllReportDefinitions", new v201008.GetAllReportDefinitions());
      RegisterSample("v201008.GetReportFields", new v201008.GetReportFields());
      RegisterSample("v201008.DownloadReport", new v201008.DownloadReport());
      RegisterSample("v201008.AddKeywordsPerformanceReportDefinition",
          new v201008.AddKeywordsPerformanceReportDefinition());

      RegisterSample("v201008.UploadImage", new v201008.UploadImage());
      RegisterSample("v201008.GetAllImages", new v201008.GetAllImages());
      RegisterSample("v201008.GetAllVideos", new v201008.GetAllVideos());

      RegisterSample("v201008.AddSiteLinks", new v201008.AddSiteLinks());
      RegisterSample("v201008.DeleteSitelinks", new v201008.DeleteSitelinks());

      RegisterSample("v201008.AddExperiment", new v201008.AddExperiment());
      RegisterSample("v201008.GetAllExperiments", new v201008.GetAllExperiments());
      RegisterSample("v201008.PromoteExperiment", new v201008.PromoteExperiment());
      RegisterSample("v201008.DeleteExperiment", new v201008.DeleteExperiment());

      RegisterSample("v201008.GetTrafficEstimates", new v201008.GetTrafficEstimates());
      RegisterSample("v201008.GetCampaignAlerts", new v201008.GetCampaignAlerts());
      RegisterSample("v201008.GetAccountHierarchy", new v201008.GetAccountHierarchy());

      RegisterSample("v201008.AddUserList", new v201008.AddUserList());
      RegisterSample("v201008.AddLogicalUserList", new v201008.AddLogicalUserList());
      RegisterSample("v201008.DeleteUserList", new v201008.DeleteUserList());
      RegisterSample("v201008.GetAllUserLists", new v201008.GetAllUserLists());
      RegisterSample("v201008.UpdateUserList", new v201008.UpdateUserList());

      RegisterSample("v201008.GetAllAccountChanges", new v201008.GetAllAccountChanges());
      RegisterSample("v201008.HandlePartialFailures", new v201008.HandlePartialFailures());

      // Add v201101 samples.
      RegisterSample("v201101.AddCampaign", new v201101.AddCampaign());
      RegisterSample("v201101.UpdateCampaign", new v201101.UpdateCampaign());
      RegisterSample("v201101.GetAllCampaigns", new v201101.GetAllCampaigns());
      RegisterSample("v201101.GetCampaign", new v201101.GetCampaign());
      RegisterSample("v201101.GetAllPausedCampaigns", new v201101.GetAllPausedCampaigns());
      RegisterSample("v201101.CheckCampaigns", new v201101.CheckCampaigns());
      RegisterSample("v201101.DeleteCampaign", new v201101.DeleteCampaign());

      RegisterSample("v201101.SetCampaignTargets", new v201101.SetCampaignTargets());
      RegisterSample("v201101.GetAllCampaignTargets", new v201101.GetAllCampaignTargets());

      RegisterSample("v201101.AddAdGroup", new v201101.AddAdGroup());
      RegisterSample("v201101.UpdateAdGroup", new v201101.UpdateAdGroup());
      RegisterSample("v201101.GetAllAdGroups", new v201101.GetAllAdGroups());
      RegisterSample("v201101.DeleteAdGroup", new v201101.DeleteAdGroup());

      RegisterSample("v201101.AddAds", new v201101.AddAds());
      RegisterSample("v201101.HandlePolicyViolationError",
          new v201101.HandlePolicyViolationError());
      RegisterSample("v201101.UpdateAd", new v201101.UpdateAd());
      RegisterSample("v201101.AddMobileImageAd", new v201101.AddMobileImageAd());
      RegisterSample("v201101.GetAllAds", new v201101.GetAllAds());
      RegisterSample("v201101.GetAllDisapprovedAds", new v201101.GetAllDisapprovedAds());
      RegisterSample("v201101.DeleteAd", new v201101.DeleteAd());

      RegisterSample("v201101.AddCampaignAdExtension", new v201101.AddCampaignAdExtension());
      RegisterSample("v201101.PerformBulkMutateJob", new v201101.PerformBulkMutateJob());
      RegisterSample("v201101.GetBulkMutateJob", new v201101.GetBulkMutateJob());
      RegisterSample("v201101.DeleteBulkMutateJob", new v201101.DeleteBulkMutateJob());

      RegisterSample("v201101.GetAllCampaignAdExtensions",
          new v201101.GetAllCampaignAdExtensions());

      RegisterSample("v201101.AddAdGroupCriteria", new v201101.AddAdGroupCriteria());
      RegisterSample("v201101.UpdateAdGroupCriterion", new v201101.UpdateAdGroupCriterion());
      RegisterSample("v201101.GetAllAdGroupCriteria", new v201101.GetAllAdGroupCriteria());
      RegisterSample("v201101.GetAllActiveAdGroupCriteria",
          new v201101.GetAllActiveAdGroupCriteria());
      RegisterSample("v201101.DeleteAdGroupCriterion", new v201101.DeleteAdGroupCriterion());

      RegisterSample("v201101.AddNegativeCampaignCriterion",
          new v201101.AddNegativeCampaignCriterion());
      RegisterSample("v201101.GetAllNegativeCampaignCriteria",
          new v201101.GetAllNegativeCampaignCriteria());

      RegisterSample("v201101.GetRelatedKeywords", new v201101.GetRelatedKeywords());
      RegisterSample("v201101.GetRelatedPlacements", new v201101.GetRelatedPlacements());

      RegisterSample("v201101.AddAdExtensionOverride", new v201101.AddAdExtensionOverride());
      RegisterSample("v201101.GetAllAdExtensionOverrides",
          new v201101.GetAllAdExtensionOverrides());
      RegisterSample("v201101.SetAdParams", new v201101.SetAdParams());
      RegisterSample("v201101.GetGeoLocationInfo", new v201101.GetGeoLocationInfo());
      RegisterSample("v201101.GetConversionOptimizerEligibility",
          new v201101.GetConversionOptimizerEligibility());

      RegisterSample("v201101.GetTotalUsageUnitsPerMonth",
          new v201101.GetTotalUsageUnitsPerMonth());
      RegisterSample("v201101.GetOperationCount", new v201101.GetOperationCount());
      RegisterSample("v201101.GetUnitCount", new v201101.GetUnitCount());
      RegisterSample("v201101.GetMethodCost", new v201101.GetMethodCost());

      RegisterSample("v201101.BackupSandboxDemo", new v201101.BackupSandboxDemo());
      RegisterSample("v201101.RestoreSandboxDemo", new v201101.RestoreSandboxDemo());
      RegisterSample("v201101.MethodApiUnitsUsageDemo", new v201101.MethodApiUnitsUsageDemo());

      RegisterSample("v201101.GetCriterionBidLandscape", new v201101.GetCriterionBidLandscape());
      RegisterSample("v201101.GetAdGroupBidLandscape", new v201101.GetAdGroupBidLandscape());
      RegisterSample("v201101.GetAllReportDefinitions", new v201101.GetAllReportDefinitions());
      RegisterSample("v201101.GetReportFields", new v201101.GetReportFields());
      RegisterSample("v201101.DownloadReport", new v201101.DownloadReport());
      RegisterSample("v201101.AddKeywordsPerformanceReportDefinition",
          new v201101.AddKeywordsPerformanceReportDefinition());

      RegisterSample("v201101.UploadImage", new v201101.UploadImage());
      RegisterSample("v201101.GetAllImages", new v201101.GetAllImages());
      RegisterSample("v201101.GetAllVideos", new v201101.GetAllVideos());

      RegisterSample("v201101.AddSiteLinks", new v201101.AddSiteLinks());
      RegisterSample("v201101.DeleteSitelinks", new v201101.DeleteSitelinks());

      RegisterSample("v201101.AddExperiment", new v201101.AddExperiment());
      RegisterSample("v201101.GetAllExperiments", new v201101.GetAllExperiments());
      RegisterSample("v201101.PromoteExperiment", new v201101.PromoteExperiment());
      RegisterSample("v201101.DeleteExperiment", new v201101.DeleteExperiment());

      RegisterSample("v201101.GetTrafficEstimates", new v201101.GetTrafficEstimates());
      RegisterSample("v201101.GetCampaignAlerts", new v201101.GetCampaignAlerts());
      RegisterSample("v201101.GetAccountHierarchy", new v201101.GetAccountHierarchy());

      RegisterSample("v201101.AddUserList", new v201101.AddUserList());
      RegisterSample("v201101.AddLogicalUserList", new v201101.AddLogicalUserList());
      RegisterSample("v201101.DeleteUserList", new v201101.DeleteUserList());
      RegisterSample("v201101.GetAllUserLists", new v201101.GetAllUserLists());
      RegisterSample("v201101.UpdateUserList", new v201101.UpdateUserList());

      RegisterSample("v201101.GetAllAccountChanges", new v201101.GetAllAccountChanges());
      RegisterSample("v201101.HandlePartialFailures", new v201101.HandlePartialFailures());

      // Add combined examples.
      RegisterSample("Both.UsingTrafficEstimatorDemo", new Both.UsingTrafficEstimatorDemo());
    }

    /// <summary>
    /// The main method.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      if (args.Length == 0) {
        ShowUsage();
        return;
      }

      AdWordsUser user = new AdWordsUser();

      if (string.Compare(args[0], "--all", true) == 0) {
        foreach(SamplePair pair in sampleMap) {
          RunASample(user, pair.Value);
        }
      } else if (string.Compare(args[0], "--v13all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
            return pair.Key.StartsWith("v13");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          RunASample(user, matchingItem.Value);
        }
      } else if (string.Compare(args[0], "--v200909all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v200909");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          RunASample(user, matchingItem.Value);
        }
      } else if (string.Compare(args[0], "--v201003all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v201003");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          RunASample(user, matchingItem.Value);
        }
      } else if (string.Compare(args[0], "--v201008all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v201008");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          RunASample(user, matchingItem.Value);
        }
      } else {
        foreach (string cmdArgs in args) {
          SamplePair matchingPair = sampleMap.Find(delegate(SamplePair pair) {
            return string.Compare(pair.Key, cmdArgs, true) == 0;
          });

          if (matchingPair.Key != null) {
            RunASample(user, matchingPair.Value);
          } else {
            ShowUsage();
          }
        }
      }
    }

    /// <summary>
    /// Runs a code sample.
    /// </summary>
    /// <param name="user">The user whose credentials should be used for
    /// running the sample.</param>
    /// <param name="sample">The code sample to run.</param>
    private static void RunASample(AdWordsUser user, SampleBase sample) {
      try {
        Console.WriteLine(sample.Description);
        sample.Run(user);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code sample.\n{0} at\n{1}",
            ex.Message, ex.StackTrace);
      } finally {
        Console.WriteLine("Press [Enter] to continue");
        Console.ReadLine();
      }
    }

    /// <summary>
    /// Prints program usage message.
    /// </summary>
    private static void ShowUsage() {
      if (helpShown) {
        return;
      } else {
        helpShown = true;
      }
      string exeName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
      Console.WriteLine("Runs AdWords API code examples");
      Console.WriteLine("Usage : {0} [flags]\n", exeName);
      Console.WriteLine("Available flags\n");
      Console.WriteLine("--help\t\t : Prints this help message.", exeName);
      Console.WriteLine("--all\t\t : Run all code examples.", exeName);
      Console.WriteLine("examplename1 [examplename1 ...] : " +
          "Run specific code examples. Example name can be one of the following:\n", exeName);
      foreach (SamplePair pair in sampleMap) {
        Console.WriteLine("{0} : {1}", pair.Key, pair.Value.Description);
      }
      Console.WriteLine("Press [Enter] to continue");
      Console.ReadLine();
    }
  }
}

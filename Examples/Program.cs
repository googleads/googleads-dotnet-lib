// Copyright 2009, Google Inc. All Rights Reserved.
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
using com.google.api.adwords.samples.both;
using com.google.api.adwords.samples.v13;
using com.google.api.adwords.samples.v200909;
using com.google.api.adwords.v13;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace com.google.api.adwords.samples {
  /// <summary>
  /// The Main class for this application.
  /// </summary>
  class Program {
    /// <summary>
    /// A map to hold the code samples to be executed.
    /// </summary>
    static SortedDictionary<string, SampleBase> sampleMap =
        new SortedDictionary<string, SampleBase>(StringComparer.InvariantCultureIgnoreCase);

    /// <summary>
    /// A flag to keep track of whether help message was shown earlier.
    /// </summary>
    private static bool helpShown = false;

    /// <summary>
    /// Static constructor to initialize the sample map.
    /// </summary>
    static Program() {
      // Add v13 samples.
      sampleMap.Add("v13.AccountServiceDemo", new AccountServiceDemo());
      sampleMap.Add("v13.AccountServiceNoConfigDemo", new AccountServiceNoConfigDemo());
      sampleMap.Add("v13.CampaignServiceDemo", new CampaignServiceDemo());
      sampleMap.Add("v13.CampaignServiceWebsiteDemo", new CampaignServiceWebsiteDemo());
      sampleMap.Add("v13.GetCampaignIdsDemo", new GetCampaignIdsDemo());
      sampleMap.Add("v13.GetAdGroupIdsDemo", new GetAdGroupIdsDemo());
      sampleMap.Add("v13.AdServiceDemo", new AdServiceDemo());
      sampleMap.Add("v13.GetAdIdsDemo", new GetAdIdsDemo());
      sampleMap.Add("v13.KeywordToolDemo", new KeywordToolDemo());
      sampleMap.Add("v13.KeywordEstimateDemo", new KeywordEstimateDemo());
      sampleMap.Add("v13.CheckKeywordTrafficDemo", new CheckKeywordTrafficDemo());
      sampleMap.Add("v13.SiteSuggestionServiceDemo", new SiteSuggestionServiceDemo());
      sampleMap.Add("v13.InfoServiceDemo", new InfoServiceDemo());
      sampleMap.Add("v13.ReportServiceKeywordDemo", new ReportServiceKeywordDemo());
      sampleMap.Add("v13.ReportServiceStructureDemo", new ReportServiceStructureDemo());
      sampleMap.Add("v13.DownloadReportAsCsvDemo", new DownloadReportAsCsvDemo());
      sampleMap.Add("v13.DownloadReportAsXmlDemo", new DownloadReportAsXmlDemo());
      sampleMap.Add("v13.BackupSandboxDemo", new BackupSandboxDemo());
      sampleMap.Add("v13.RestoreSandboxDemo", new RestoreSandboxDemo());
      sampleMap.Add("v13.MethodQuotaUsageDemo", new MethodQuotaUsageDemo());
      sampleMap.Add("v13.ClientQuotaUsageDemo", new ClientQuotaUsageDemo());

      // Add v200909 samples.
      sampleMap.Add("v200909.AddAds", new AddAds());
      sampleMap.Add("v200909.AddMobileImageAd", new AddMobileImageAd());
      sampleMap.Add("v200909.AddCampaignAdExtension", new AddCampaignAdExtension());
      sampleMap.Add("v200909.PerformBulkMutateJob", new PerformBulkMutateJob());
      sampleMap.Add("v200909.GetAllAds", new GetAllAds());
      sampleMap.Add("v200909.GetAllCampaignAdExtensions",
          new GetAllCampaignAdExtensions());
      sampleMap.Add("v200909.GetAllDisapprovedAds", new GetAllDisapprovedAds());
      sampleMap.Add("v200909.UpdateAd", new UpdateAd());
      sampleMap.Add("v200909.GetAllCampaigns", new GetAllCampaigns());
      sampleMap.Add("v200909.GetAllPausedCampaigns", new GetAllPausedCampaigns());
      sampleMap.Add("v200909.AddCampaign", new AddCampaign());
      sampleMap.Add("v200909.UpdateCampaign", new UpdateCampaign());
      sampleMap.Add("v200909.SetCampaignTargets", new SetCampaignTargets());
      sampleMap.Add("v200909.GetAllCampaignTargets", new GetAllCampaignTargets());
      sampleMap.Add("v200909.GetAllAdGroups", new GetAllAdGroups());
      sampleMap.Add("v200909.AddAdGroup", new AddAdGroup());
      sampleMap.Add("v200909.UpdateAdGroup", new UpdateAdGroup());
      sampleMap.Add("v200909.GetAllAdGroupCriteria", new GetAllAdGroupCriteria());
      sampleMap.Add("v200909.GetAllActiveAdGroupCriteria", new GetAllActiveAdGroupCriteria());
      sampleMap.Add("v200909.AddAdGroupCriteria", new AddAdGroupCriteria());
      sampleMap.Add("v200909.UpdateAdGroupCriterion", new UpdateAdGroupCriterion());
      sampleMap.Add("v200909.AddNegativeCampaignCriterion", new AddNegativeCampaignCriterion());
      sampleMap.Add("v200909.GetTotalUsageUnitsPerMonth", new GetTotalUsageUnitsPerMonth());
      sampleMap.Add("v200909.GetOperationCount", new GetOperationCount());
      sampleMap.Add("v200909.GetUnitCount", new GetUnitCount());
      sampleMap.Add("v200909.GetMethodCost", new GetMethodCost());
      sampleMap.Add("v200909.GetRelatedKeywords", new GetRelatedKeywords());
      sampleMap.Add("v200909.GetRelatedPlacements", new GetRelatedPlacements());
      sampleMap.Add("v200909.CheckCampaigns", new CheckCampaigns());
      sampleMap.Add("v200909.GetAllAdExtensionOverrides", new GetAllAdExtensionOverrides());
      sampleMap.Add("v200909.AddAdExtensionOverride", new AddAdExtensionOverride());
      sampleMap.Add("v200909.GetGeoLocationInfo", new GetGeoLocationInfo());
      sampleMap.Add("v200909.SetAdParams", new SetAdParams());
      sampleMap.Add("v200909.GetConversionOptimizerEligibility",
          new GetConversionOptimizerEligibility());

      // Add combined examples.
      sampleMap.Add("both.UsingKeywordSuggestionDemo", new UsingKeywordSuggestionDemo());
    }

    /// <summary>
    /// The main method.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args) {
      if (args.Length == 0) {
        ShowUsage();
        return;
      }

      AdWordsUser user = new AdWordsUser();

      if (string.Compare(args[0], "--all", true) == 0) {
        foreach (string key in sampleMap.Keys) {
          SampleBase sample = sampleMap[key];
          Console.WriteLine(sample.Description);
          sample.Run(user);
          Console.WriteLine("Press [Enter] to continue");
          Console.ReadLine();
        }
      } else if (string.Compare(args[0], "--v13all", true) == 0) {
        foreach (string key in sampleMap.Keys) {
          if (key.StartsWith("v13")) {
            SampleBase sample = sampleMap[key];
            Console.WriteLine(sample.Description);
            sample.Run(user);
            Console.WriteLine("Press [Enter] to continue");
            Console.ReadLine();
          }
        }
      } else if (string.Compare(args[0], "--v2009all", true) == 0) {
        foreach (string key in sampleMap.Keys) {
          if (key.StartsWith("v2009")) {
            SampleBase sample = sampleMap[key];
            Console.WriteLine(sample.Description);
            sample.Run(user);
            Console.WriteLine("Press [Enter] to continue");
            Console.ReadLine();
          }
        }
      } else {
        foreach (string cmdArgs in args) {
          if (sampleMap.ContainsKey(cmdArgs)) {
            SampleBase sample = sampleMap[cmdArgs];
            Console.WriteLine(sample.Description);
            sample.Run(user);
            Console.WriteLine("Press [Enter] to continue");
            Console.ReadLine();
          } else {
            ShowUsage();
          }
        }
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
      Console.WriteLine("Runs AdWords API code samples");
      Console.WriteLine("Usage : {0} [flags]\n", exeName);
      Console.WriteLine("Available flags\n");
      Console.WriteLine("--help\t\t : Prints this help message.", exeName);
      Console.WriteLine("--all\t\t : Run all code samples.", exeName);
      Console.WriteLine("samplename1 [samplename2 ...] : " +
          "Run specific code samples. Samplename can be one of the following:\n", exeName);
      foreach (string key in sampleMap.Keys) {
        Console.WriteLine("{0} : {1}", key, sampleMap[key].Description);
      }
      Console.WriteLine("Press [Enter] to continue");
      Console.ReadLine();
    }
  }
}

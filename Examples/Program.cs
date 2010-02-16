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

using SamplePair = System.Collections.Generic.KeyValuePair<string,
    com.google.api.adwords.samples.SampleBase>;

namespace com.google.api.adwords.samples {
  /// <summary>
  /// The Main class for this application.
  /// </summary>
  class Program {
    /// <summary>
    /// A map to hold the code samples to be executed.
    /// </summary>
    static List<SamplePair> sampleMap = new List<SamplePair>();

    /// <summary>
    /// A flag to keep track of whether help message was shown earlier.
    /// </summary>
    private static bool helpShown = false;

    static void RegisterSample(string key, SampleBase value) {
      sampleMap.Add(new SamplePair(key, value));
    }
    /// <summary>
    /// Static constructor to initialize the sample map.
    /// </summary>
    static Program() {
      // Add v13 samples.
      RegisterSample("v13.AccountServiceDemo", new AccountServiceDemo());
      RegisterSample("v13.AccountServiceNoConfigDemo", new AccountServiceNoConfigDemo());
      RegisterSample("v13.CampaignServiceDemo", new CampaignServiceDemo());
      RegisterSample("v13.CampaignServiceWebsiteDemo", new CampaignServiceWebsiteDemo());
      RegisterSample("v13.GetCampaignIdsDemo", new GetCampaignIdsDemo());
      RegisterSample("v13.GetAdGroupIdsDemo", new GetAdGroupIdsDemo());
      RegisterSample("v13.AdServiceDemo", new AdServiceDemo());
      RegisterSample("v13.GetAdIdsDemo", new GetAdIdsDemo());
      RegisterSample("v13.KeywordToolDemo", new KeywordToolDemo());
      RegisterSample("v13.KeywordEstimateDemo", new KeywordEstimateDemo());
      RegisterSample("v13.CheckKeywordTrafficDemo", new CheckKeywordTrafficDemo());
      RegisterSample("v13.SiteSuggestionServiceDemo", new SiteSuggestionServiceDemo());
      RegisterSample("v13.InfoServiceDemo", new InfoServiceDemo());
      RegisterSample("v13.ReportServiceKeywordDemo", new ReportServiceKeywordDemo());
      RegisterSample("v13.ReportServiceStructureDemo", new ReportServiceStructureDemo());
      RegisterSample("v13.DownloadReportAsCsvDemo", new DownloadReportAsCsvDemo());
      RegisterSample("v13.DownloadReportAsXmlDemo", new DownloadReportAsXmlDemo());
      RegisterSample("v13.BackupSandboxDemo", new BackupSandboxDemo());
      RegisterSample("v13.RestoreSandboxDemo", new RestoreSandboxDemo());
      RegisterSample("v13.MethodQuotaUsageDemo", new MethodQuotaUsageDemo());
      RegisterSample("v13.ClientQuotaUsageDemo", new ClientQuotaUsageDemo());

      // Add v200909 samples.
      RegisterSample("v200909.AddCampaign", new AddCampaign());
      RegisterSample("v200909.UpdateCampaign", new UpdateCampaign());
      RegisterSample("v200909.GetAllCampaigns", new GetAllCampaigns());
      RegisterSample("v200909.GetAllPausedCampaigns", new GetAllPausedCampaigns());
      RegisterSample("v200909.CheckCampaigns", new CheckCampaigns());

      RegisterSample("v200909.SetCampaignTargets", new SetCampaignTargets());
      RegisterSample("v200909.GetAllCampaignTargets", new GetAllCampaignTargets());

      RegisterSample("v200909.AddAdGroup", new AddAdGroup());
      RegisterSample("v200909.UpdateAdGroup", new UpdateAdGroup());
      RegisterSample("v200909.GetAllAdGroups", new GetAllAdGroups());

      RegisterSample("v200909.AddAds", new AddAds());
      RegisterSample("v200909.UpdateAd", new UpdateAd());
      RegisterSample("v200909.AddMobileImageAd", new AddMobileImageAd());
      RegisterSample("v200909.GetAllAds", new GetAllAds());
      RegisterSample("v200909.GetAllDisapprovedAds", new GetAllDisapprovedAds());

      RegisterSample("v200909.AddCampaignAdExtension", new AddCampaignAdExtension());
      RegisterSample("v200909.PerformBulkMutateJob", new PerformBulkMutateJob());
      RegisterSample("v200909.GetAllCampaignAdExtensions",
          new GetAllCampaignAdExtensions());

      RegisterSample("v200909.AddAdGroupCriteria", new AddAdGroupCriteria());
      RegisterSample("v200909.UpdateAdGroupCriterion", new UpdateAdGroupCriterion());
      RegisterSample("v200909.GetAllAdGroupCriteria", new GetAllAdGroupCriteria());
      RegisterSample("v200909.GetAllActiveAdGroupCriteria", new GetAllActiveAdGroupCriteria());

      RegisterSample("v200909.AddNegativeCampaignCriterion", new AddNegativeCampaignCriterion());

      RegisterSample("v200909.GetRelatedKeywords", new GetRelatedKeywords());
      RegisterSample("v200909.GetRelatedPlacements", new GetRelatedPlacements());

      RegisterSample("v200909.AddAdExtensionOverride", new AddAdExtensionOverride());
      RegisterSample("v200909.GetAllAdExtensionOverrides", new GetAllAdExtensionOverrides());
      RegisterSample("v200909.SetAdParams", new SetAdParams());
      RegisterSample("v200909.GetGeoLocationInfo", new GetGeoLocationInfo());
      RegisterSample("v200909.GetConversionOptimizerEligibility",
          new GetConversionOptimizerEligibility());

      RegisterSample("v200909.GetTotalUsageUnitsPerMonth", new GetTotalUsageUnitsPerMonth());
      RegisterSample("v200909.GetOperationCount", new GetOperationCount());
      RegisterSample("v200909.GetUnitCount", new GetUnitCount());
      RegisterSample("v200909.GetMethodCost", new GetMethodCost());

      // Add combined examples.
      RegisterSample("both.UsingKeywordSuggestionDemo", new UsingKeywordSuggestionDemo());
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
        foreach(SamplePair pair in sampleMap) {
          SampleBase sample = pair.Value;
          Console.WriteLine(sample.Description);
          sample.Run(user);
          Console.WriteLine("Press [Enter] to continue");
          Console.ReadLine();
        }
      } else if (string.Compare(args[0], "--v13all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
            return pair.Key.StartsWith("v13");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          SampleBase sample = matchingItem.Value;
          Console.WriteLine(sample.Description);
          sample.Run(user);
          Console.WriteLine("Press [Enter] to continue");
          Console.ReadLine();
        }
      } else if (string.Compare(args[0], "--v2009all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v2009");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          SampleBase sample = matchingItem.Value;
          Console.WriteLine(sample.Description);
          sample.Run(user);
          Console.WriteLine("Press [Enter] to continue");
          Console.ReadLine();
        }
      } else {
        foreach (string cmdArgs in args) {
          SamplePair matchingPair = sampleMap.Find(delegate(SamplePair pair) {
            return string.Compare(pair.Key, cmdArgs, true) == 0;
          });

          if (matchingPair.Key != null) {
            SampleBase sample = matchingPair.Value;
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
      foreach (SamplePair pair in sampleMap) {
        Console.WriteLine("{0} : {1}", pair.Key, pair.Value.Description);
      }
      Console.WriteLine("Press [Enter] to continue");
      Console.ReadLine();
    }
  }
}

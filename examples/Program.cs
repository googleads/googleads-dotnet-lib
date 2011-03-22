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

using Google.Api.Ads.Dfa.Lib;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using SamplePair = System.Collections.Generic.KeyValuePair<string,
    Google.Api.Ads.Dfa.Examples.SampleBase>;

namespace Google.Api.Ads.Dfa.Examples {
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

    static void RegisterSample(string key, SampleBase value) {
      sampleMap.Add(new SamplePair(key, value));
    }
    /// <summary>
    /// Static constructor to initialize the sample map.
    /// </summary>
    static Program() {
      // Add v1.11 examples.
      RegisterSample("v1_11.AddAdvertiserUserFilter", new v1_11.AddAdvertiserUserFilter());
      RegisterSample("v1_11.AssignAdvertisersToAdvertiserGroup",
          new v1_11.AssignAdvertisersToAdvertiserGroup());
      RegisterSample("v1_11.AssignCreativesToPlacements", new v1_11.AssignCreativesToPlacements());
      RegisterSample("v1_11.Authenticate", new v1_11.Authenticate());
      RegisterSample("v1_11.CreateAdvertiser", new v1_11.CreateAdvertiser());
      RegisterSample("v1_11.CreateAdvertiserGroup", new v1_11.CreateAdvertiserGroup());
      RegisterSample("v1_11.CreateCampaign", new v1_11.CreateCampaign());
      RegisterSample("v1_11.CreateContentCategory", new v1_11.CreateContentCategory());
      RegisterSample("v1_11.CreateCreativeField", new v1_11.CreateCreativeField());
      RegisterSample("v1_11.CreateCreativeFieldValue", new v1_11.CreateCreativeFieldValue());
      RegisterSample("v1_11.CreateCreativeGroup", new v1_11.CreateCreativeGroup());
      RegisterSample("v1_11.CreateFlashInpageCreative", new v1_11.CreateFlashInpageCreative());
      RegisterSample("v1_11.CreateHTMLAsset", new v1_11.CreateHTMLAsset());
      RegisterSample("v1_11.CreateImageAsset", new v1_11.CreateImageAsset());
      RegisterSample("v1_11.CreateMobileAsset", new v1_11.CreateMobileAsset());
      RegisterSample("v1_11.CreateMobileCreative", new v1_11.CreateMobileCreative());
      RegisterSample("v1_11.CreatePlacement", new v1_11.CreatePlacement());
      RegisterSample("v1_11.CreatePlacementStrategy", new v1_11.CreatePlacementStrategy());
      RegisterSample("v1_11.CreateRotationGroup", new v1_11.CreateRotationGroup());
      RegisterSample("v1_11.CreateSpotlightActivity", new v1_11.CreateSpotlightActivity());
      RegisterSample("v1_11.CreateSpotlightActivityGroup",
          new v1_11.CreateSpotlightActivityGroup());
      RegisterSample("v1_11.CreateSubnetwork", new v1_11.CreateSubnetwork());
      RegisterSample("v1_11.CreateUserRole", new v1_11.CreateUserRole());
      RegisterSample("v1_11.DownloadTags", new v1_11.DownloadTags());
      RegisterSample("v1_11.GetActivityGroups", new v1_11.GetActivityGroups());
      RegisterSample("v1_11.GetActivityTypes", new v1_11.GetActivityTypes());
      RegisterSample("v1_11.GetAdTypes", new v1_11.GetAdTypes());
      RegisterSample("v1_11.GetAdTypesNoConfig", new v1_11.GetAdTypesNoConfig());
      RegisterSample("v1_11.GetAdvertiserGroups", new v1_11.GetAdvertiserGroups());
      RegisterSample("v1_11.GetAdvertisers", new v1_11.GetAdvertisers());
      RegisterSample("v1_11.GetAvailablePermissions", new v1_11.GetAvailablePermissions());
      RegisterSample("v1_11.GetCampaigns", new v1_11.GetCampaigns());
      RegisterSample("v1_11.GetChangeLogForAdvertiser", new v1_11.GetChangeLogForAdvertiser());
      RegisterSample("v1_11.GetChangeLogObjectTypes", new v1_11.GetChangeLogObjectTypes());
      RegisterSample("v1_11.GetContentCategories", new v1_11.GetContentCategories());
      RegisterSample("v1_11.GetCountries", new v1_11.GetCountries());
      RegisterSample("v1_11.GetCreativeField", new v1_11.GetCreativeField());
      RegisterSample("v1_11.GetCreativeFieldValues", new v1_11.GetCreativeFieldValues());
      RegisterSample("v1_11.GetCreativeGroups", new v1_11.GetCreativeGroups());
      RegisterSample("v1_11.GetCreatives", new v1_11.GetCreatives());
      RegisterSample("v1_11.GetCreativeTypes", new v1_11.GetCreativeTypes());
      RegisterSample("v1_11.GetDFASite", new v1_11.GetDFASite());
      RegisterSample("v1_11.GetPlacements", new v1_11.GetPlacements());
      RegisterSample("v1_11.GetPlacementStrategies", new v1_11.GetPlacementStrategies());
      RegisterSample("v1_11.GetPlacementTypes", new v1_11.GetPlacementTypes());
      RegisterSample("v1_11.GetPricingTypes", new v1_11.GetPricingTypes());
      RegisterSample("v1_11.GetSize", new v1_11.GetSize());
      RegisterSample("v1_11.GetSubnetworks", new v1_11.GetSubnetworks());
      RegisterSample("v1_11.GetTagMethodTypes", new v1_11.GetTagMethodTypes());
      RegisterSample("v1_11.GetUserFilterTypes", new v1_11.GetUserFilterTypes());
      RegisterSample("v1_11.GetUserRoles", new v1_11.GetUserRoles());
      RegisterSample("v1_11.GetUsers", new v1_11.GetUsers());

      // Add v1.12 examples.
      RegisterSample("v1_12.AddAdvertiserUserFilter", new v1_12.AddAdvertiserUserFilter());
      RegisterSample("v1_12.AssignAdvertisersToAdvertiserGroup",
          new v1_12.AssignAdvertisersToAdvertiserGroup());
      RegisterSample("v1_12.AssignCreativesToPlacements", new v1_12.AssignCreativesToPlacements());
      RegisterSample("v1_12.Authenticate", new v1_12.Authenticate());
      RegisterSample("v1_12.CreateAdvertiser", new v1_12.CreateAdvertiser());
      RegisterSample("v1_12.CreateAdvertiserGroup", new v1_12.CreateAdvertiserGroup());
      RegisterSample("v1_12.CreateCampaign", new v1_12.CreateCampaign());
      RegisterSample("v1_12.CreateContentCategory", new v1_12.CreateContentCategory());
      RegisterSample("v1_12.CreateCreativeField", new v1_12.CreateCreativeField());
      RegisterSample("v1_12.CreateCreativeFieldValue", new v1_12.CreateCreativeFieldValue());
      RegisterSample("v1_12.CreateCreativeGroup", new v1_12.CreateCreativeGroup());
      RegisterSample("v1_12.CreateFlashInpageCreative", new v1_12.CreateFlashInpageCreative());
      RegisterSample("v1_12.CreateHTMLAsset", new v1_12.CreateHTMLAsset());
      RegisterSample("v1_12.CreateImageAsset", new v1_12.CreateImageAsset());
      RegisterSample("v1_12.CreateMobileAsset", new v1_12.CreateMobileAsset());
      RegisterSample("v1_12.CreateMobileCreative", new v1_12.CreateMobileCreative());
      RegisterSample("v1_12.CreatePlacement", new v1_12.CreatePlacement());
      RegisterSample("v1_12.CreatePlacementStrategy", new v1_12.CreatePlacementStrategy());
      RegisterSample("v1_12.CreateRotationGroup", new v1_12.CreateRotationGroup());
      RegisterSample("v1_12.CreateSpotlightActivity", new v1_12.CreateSpotlightActivity());
      RegisterSample("v1_12.CreateSpotlightActivityGroup",
          new v1_12.CreateSpotlightActivityGroup());
      RegisterSample("v1_12.CreateSubnetwork", new v1_12.CreateSubnetwork());
      RegisterSample("v1_12.CreateUserRole", new v1_12.CreateUserRole());
      RegisterSample("v1_12.DownloadTags", new v1_12.DownloadTags());
      RegisterSample("v1_12.GetActivityGroups", new v1_12.GetActivityGroups());
      RegisterSample("v1_12.GetActivityTypes", new v1_12.GetActivityTypes());
      RegisterSample("v1_12.GetAdTypes", new v1_12.GetAdTypes());
      RegisterSample("v1_12.GetAdTypesNoConfig", new v1_12.GetAdTypesNoConfig());
      RegisterSample("v1_12.GetAdvertiserGroups", new v1_12.GetAdvertiserGroups());
      RegisterSample("v1_12.GetAdvertisers", new v1_12.GetAdvertisers());
      RegisterSample("v1_12.GetAvailablePermissions", new v1_12.GetAvailablePermissions());
      RegisterSample("v1_12.GetCampaigns", new v1_12.GetCampaigns());
      RegisterSample("v1_12.GetChangeLogForAdvertiser", new v1_12.GetChangeLogForAdvertiser());
      RegisterSample("v1_12.GetChangeLogObjectTypes", new v1_12.GetChangeLogObjectTypes());
      RegisterSample("v1_12.GetContentCategories", new v1_12.GetContentCategories());
      RegisterSample("v1_12.GetCountries", new v1_12.GetCountries());
      RegisterSample("v1_12.GetCreativeField", new v1_12.GetCreativeField());
      RegisterSample("v1_12.GetCreativeFieldValues", new v1_12.GetCreativeFieldValues());
      RegisterSample("v1_12.GetCreativeGroups", new v1_12.GetCreativeGroups());
      RegisterSample("v1_12.GetCreatives", new v1_12.GetCreatives());
      RegisterSample("v1_12.GetCreativeTypes", new v1_12.GetCreativeTypes());
      RegisterSample("v1_12.GetDFASite", new v1_12.GetDFASite());
      RegisterSample("v1_12.GetPlacements", new v1_12.GetPlacements());
      RegisterSample("v1_12.GetPlacementStrategies", new v1_12.GetPlacementStrategies());
      RegisterSample("v1_12.GetPlacementTypes", new v1_12.GetPlacementTypes());
      RegisterSample("v1_12.GetPricingTypes", new v1_12.GetPricingTypes());
      RegisterSample("v1_12.GetSize", new v1_12.GetSize());
      RegisterSample("v1_12.GetSubnetworks", new v1_12.GetSubnetworks());
      RegisterSample("v1_12.GetTagMethodTypes", new v1_12.GetTagMethodTypes());
      RegisterSample("v1_12.GetUserFilterTypes", new v1_12.GetUserFilterTypes());
      RegisterSample("v1_12.GetUserRoles", new v1_12.GetUserRoles());
      RegisterSample("v1_12.GetUsers", new v1_12.GetUsers());

      // Add v1.13 examples.
      RegisterSample("v1_13.AddAdvertiserUserFilter", new v1_13.AddAdvertiserUserFilter());
      RegisterSample("v1_13.AssignAdvertisersToAdvertiserGroup",
          new v1_13.AssignAdvertisersToAdvertiserGroup());
      RegisterSample("v1_13.AssignCreativesToPlacements", new v1_13.AssignCreativesToPlacements());
      RegisterSample("v1_13.Authenticate", new v1_13.Authenticate());
      RegisterSample("v1_13.CreateAdvertiser", new v1_13.CreateAdvertiser());
      RegisterSample("v1_13.CreateAdvertiserGroup", new v1_13.CreateAdvertiserGroup());
      RegisterSample("v1_13.CreateCampaign", new v1_13.CreateCampaign());
      RegisterSample("v1_13.CreateContentCategory", new v1_13.CreateContentCategory());
      RegisterSample("v1_13.CreateCreativeField", new v1_13.CreateCreativeField());
      RegisterSample("v1_13.CreateCreativeFieldValue", new v1_13.CreateCreativeFieldValue());
      RegisterSample("v1_13.CreateCreativeGroup", new v1_13.CreateCreativeGroup());
      RegisterSample("v1_13.CreateFlashInpageCreative", new v1_13.CreateFlashInpageCreative());
      RegisterSample("v1_13.CreateHTMLAsset", new v1_13.CreateHTMLAsset());
      RegisterSample("v1_13.CreateImageAsset", new v1_13.CreateImageAsset());
      RegisterSample("v1_13.CreateMobileAsset", new v1_13.CreateMobileAsset());
      RegisterSample("v1_13.CreateMobileCreative", new v1_13.CreateMobileCreative());
      RegisterSample("v1_13.CreatePlacement", new v1_13.CreatePlacement());
      RegisterSample("v1_13.CreatePlacementStrategy", new v1_13.CreatePlacementStrategy());
      RegisterSample("v1_13.CreateRotationGroup", new v1_13.CreateRotationGroup());
      RegisterSample("v1_13.CreateSpotlightActivity", new v1_13.CreateSpotlightActivity());
      RegisterSample("v1_13.CreateSpotlightActivityGroup",
          new v1_13.CreateSpotlightActivityGroup());
      RegisterSample("v1_13.CreateSubnetwork", new v1_13.CreateSubnetwork());
      RegisterSample("v1_13.CreateUserRole", new v1_13.CreateUserRole());
      RegisterSample("v1_13.DownloadTags", new v1_13.DownloadTags());
      RegisterSample("v1_13.GetActivityGroups", new v1_13.GetActivityGroups());
      RegisterSample("v1_13.GetActivityTypes", new v1_13.GetActivityTypes());
      RegisterSample("v1_13.GetAdTypes", new v1_13.GetAdTypes());
      RegisterSample("v1_13.GetAdTypesNoConfig", new v1_13.GetAdTypesNoConfig());
      RegisterSample("v1_13.GetAdvertiserGroups", new v1_13.GetAdvertiserGroups());
      RegisterSample("v1_13.GetAdvertisers", new v1_13.GetAdvertisers());
      RegisterSample("v1_13.GetAvailablePermissions", new v1_13.GetAvailablePermissions());
      RegisterSample("v1_13.GetCampaigns", new v1_13.GetCampaigns());
      RegisterSample("v1_13.GetChangeLogForAdvertiser", new v1_13.GetChangeLogForAdvertiser());
      RegisterSample("v1_13.GetChangeLogObjectTypes", new v1_13.GetChangeLogObjectTypes());
      RegisterSample("v1_13.GetContentCategories", new v1_13.GetContentCategories());
      RegisterSample("v1_13.GetCountries", new v1_13.GetCountries());
      RegisterSample("v1_13.GetCreativeField", new v1_13.GetCreativeField());
      RegisterSample("v1_13.GetCreativeFieldValues", new v1_13.GetCreativeFieldValues());
      RegisterSample("v1_13.GetCreativeGroups", new v1_13.GetCreativeGroups());
      RegisterSample("v1_13.GetCreatives", new v1_13.GetCreatives());
      RegisterSample("v1_13.GetCreativeTypes", new v1_13.GetCreativeTypes());
      RegisterSample("v1_13.GetDFASite", new v1_13.GetDFASite());
      RegisterSample("v1_13.GetPlacements", new v1_13.GetPlacements());
      RegisterSample("v1_13.GetPlacementStrategies", new v1_13.GetPlacementStrategies());
      RegisterSample("v1_13.GetPlacementTypes", new v1_13.GetPlacementTypes());
      RegisterSample("v1_13.GetPricingTypes", new v1_13.GetPricingTypes());
      RegisterSample("v1_13.GetSize", new v1_13.GetSize());
      RegisterSample("v1_13.GetSubnetworks", new v1_13.GetSubnetworks());
      RegisterSample("v1_13.GetTagMethodTypes", new v1_13.GetTagMethodTypes());
      RegisterSample("v1_13.GetUserFilterTypes", new v1_13.GetUserFilterTypes());
      RegisterSample("v1_13.GetUserRoles", new v1_13.GetUserRoles());
      RegisterSample("v1_13.GetUsers", new v1_13.GetUsers());
      RegisterSample("v1_13.GetReport", new v1_13.GetReport());
      RegisterSample("v1_13.GetReports", new v1_13.GetReports());
      RegisterSample("v1_13.RunDeferredReport", new v1_13.RunDeferredReport());
      RegisterSample("v1_13.RCReport", new v1_13.RCReport());
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

      DfaUser user = new DfaUser();

      if (string.Compare(args[0], "--all", true) == 0) {
        foreach(SamplePair pair in sampleMap) {
          RunASample(user, pair.Value);
        }
      } else if (string.Compare(args[0], "--v1_11all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v1_11");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          RunASample(user, matchingItem.Value);
        }
      } else if (string.Compare(args[0], "--v1_12all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v1_12");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          RunASample(user, matchingItem.Value);
        }
      } else if (string.Compare(args[0], "--v1_13all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v1_13");
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
    private static void RunASample(DfaUser user, SampleBase sample) {
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
      Console.WriteLine("Runs DFA API code examples");
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

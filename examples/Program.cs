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
      RegisterSample("v111.AddAdvertiserUserFilter", new v111.AddAdvertiserUserFilter());
      RegisterSample("v111.AssignAdvertisersToAdvertiserGroup",
          new v111.AssignAdvertisersToAdvertiserGroup());
      RegisterSample("v111.AssignCreativesToPlacements", new v111.AssignCreativesToPlacements());
      RegisterSample("v111.Authenticate", new v111.Authenticate());
      RegisterSample("v111.CreateAdvertiser", new v111.CreateAdvertiser());
      RegisterSample("v111.CreateAdvertiserGroup", new v111.CreateAdvertiserGroup());
      RegisterSample("v111.CreateCampaign", new v111.CreateCampaign());
      RegisterSample("v111.CreateContentCategory", new v111.CreateContentCategory());
      RegisterSample("v111.CreateCreativeField", new v111.CreateCreativeField());
      RegisterSample("v111.CreateCreativeFieldValue", new v111.CreateCreativeFieldValue());
      RegisterSample("v111.CreateCreativeGroup", new v111.CreateCreativeGroup());
      RegisterSample("v111.CreateFlashInpageCreative", new v111.CreateFlashInpageCreative());
      RegisterSample("v111.CreateHTMLAsset", new v111.CreateHTMLAsset());
      RegisterSample("v111.CreateImageAsset", new v111.CreateImageAsset());
      RegisterSample("v111.CreateMobileAsset", new v111.CreateMobileAsset());
      RegisterSample("v111.CreateMobileCreative", new v111.CreateMobileCreative());
      RegisterSample("v111.CreatePlacement", new v111.CreatePlacement());
      RegisterSample("v111.CreatePlacementStrategy", new v111.CreatePlacementStrategy());
      RegisterSample("v111.CreateRotationGroup", new v111.CreateRotationGroup());
      RegisterSample("v111.CreateSpotlightActivity", new v111.CreateSpotlightActivity());
      RegisterSample("v111.CreateSpotlightActivityGroup", new v111.CreateSpotlightActivityGroup());
      RegisterSample("v111.CreateSubnetwork", new v111.CreateSubnetwork());
      RegisterSample("v111.CreateUserRole", new v111.CreateUserRole());
      RegisterSample("v111.DownloadTags", new v111.DownloadTags());
      RegisterSample("v111.GetActivityGroups", new v111.GetActivityGroups());
      RegisterSample("v111.GetActivityTypes", new v111.GetActivityTypes());
      RegisterSample("v111.GetAdTypes", new v111.GetAdTypes());
      RegisterSample("v111.GetAdTypesNoConfig", new v111.GetAdTypesNoConfig());
      RegisterSample("v111.GetAdvertiserGroups", new v111.GetAdvertiserGroups());
      RegisterSample("v111.GetAdvertisers", new v111.GetAdvertisers());
      RegisterSample("v111.GetAvailablePermissions", new v111.GetAvailablePermissions());
      RegisterSample("v111.GetCampaigns", new v111.GetCampaigns());
      RegisterSample("v111.GetChangeLogForAdvertiser", new v111.GetChangeLogForAdvertiser());
      RegisterSample("v111.GetChangeLogObjectTypes", new v111.GetChangeLogObjectTypes());
      RegisterSample("v111.GetContentCategories", new v111.GetContentCategories());
      RegisterSample("v111.GetCountries", new v111.GetCountries());
      RegisterSample("v111.GetCreativeField", new v111.GetCreativeField());
      RegisterSample("v111.GetCreativeFieldValues", new v111.GetCreativeFieldValues());
      RegisterSample("v111.GetCreativeGroups", new v111.GetCreativeGroups());
      RegisterSample("v111.GetCreatives", new v111.GetCreatives());
      RegisterSample("v111.GetCreativeTypes", new v111.GetCreativeTypes());
      RegisterSample("v111.GetDFASite", new v111.GetDFASite());
      RegisterSample("v111.GetPlacements", new v111.GetPlacements());
      RegisterSample("v111.GetPlacementStrategies", new v111.GetPlacementStrategies());
      RegisterSample("v111.GetPlacementTypes", new v111.GetPlacementTypes());
      RegisterSample("v111.GetPricingTypes", new v111.GetPricingTypes());
      RegisterSample("v111.GetSize", new v111.GetSize());
      RegisterSample("v111.GetSubnetworks", new v111.GetSubnetworks());
      RegisterSample("v111.GetTagMethodTypes", new v111.GetTagMethodTypes());
      RegisterSample("v111.GetUserFilterTypes", new v111.GetUserFilterTypes());
      RegisterSample("v111.GetUserRoles", new v111.GetUserRoles());
      RegisterSample("v111.GetUsers", new v111.GetUsers());

      // Add v1.12 examples.
      RegisterSample("v112.AddAdvertiserUserFilter", new v112.AddAdvertiserUserFilter());
      RegisterSample("v112.AssignAdvertisersToAdvertiserGroup",
          new v112.AssignAdvertisersToAdvertiserGroup());
      RegisterSample("v112.AssignCreativesToPlacements", new v112.AssignCreativesToPlacements());
      RegisterSample("v112.Authenticate", new v112.Authenticate());
      RegisterSample("v112.CreateAdvertiser", new v112.CreateAdvertiser());
      RegisterSample("v112.CreateAdvertiserGroup", new v112.CreateAdvertiserGroup());
      RegisterSample("v112.CreateCampaign", new v112.CreateCampaign());
      RegisterSample("v112.CreateContentCategory", new v112.CreateContentCategory());
      RegisterSample("v112.CreateCreativeField", new v112.CreateCreativeField());
      RegisterSample("v112.CreateCreativeFieldValue", new v112.CreateCreativeFieldValue());
      RegisterSample("v112.CreateCreativeGroup", new v112.CreateCreativeGroup());
      RegisterSample("v112.CreateFlashInpageCreative", new v112.CreateFlashInpageCreative());
      RegisterSample("v112.CreateHTMLAsset", new v112.CreateHTMLAsset());
      RegisterSample("v112.CreateImageAsset", new v112.CreateImageAsset());
      RegisterSample("v112.CreateMobileAsset", new v112.CreateMobileAsset());
      RegisterSample("v112.CreateMobileCreative", new v112.CreateMobileCreative());
      RegisterSample("v112.CreatePlacement", new v112.CreatePlacement());
      RegisterSample("v112.CreatePlacementStrategy", new v112.CreatePlacementStrategy());
      RegisterSample("v112.CreateRotationGroup", new v112.CreateRotationGroup());
      RegisterSample("v112.CreateSpotlightActivity", new v112.CreateSpotlightActivity());
      RegisterSample("v112.CreateSpotlightActivityGroup", new v112.CreateSpotlightActivityGroup());
      RegisterSample("v112.CreateSubnetwork", new v112.CreateSubnetwork());
      RegisterSample("v112.CreateUserRole", new v112.CreateUserRole());
      RegisterSample("v112.DownloadTags", new v112.DownloadTags());
      RegisterSample("v112.GetActivityGroups", new v112.GetActivityGroups());
      RegisterSample("v112.GetActivityTypes", new v112.GetActivityTypes());
      RegisterSample("v112.GetAdTypes", new v112.GetAdTypes());
      RegisterSample("v112.GetAdTypesNoConfig", new v112.GetAdTypesNoConfig());
      RegisterSample("v112.GetAdvertiserGroups", new v112.GetAdvertiserGroups());
      RegisterSample("v112.GetAdvertisers", new v112.GetAdvertisers());
      RegisterSample("v112.GetAvailablePermissions", new v112.GetAvailablePermissions());
      RegisterSample("v112.GetCampaigns", new v112.GetCampaigns());
      RegisterSample("v112.GetChangeLogForAdvertiser", new v112.GetChangeLogForAdvertiser());
      RegisterSample("v112.GetChangeLogObjectTypes", new v112.GetChangeLogObjectTypes());
      RegisterSample("v112.GetContentCategories", new v112.GetContentCategories());
      RegisterSample("v112.GetCountries", new v112.GetCountries());
      RegisterSample("v112.GetCreativeField", new v112.GetCreativeField());
      RegisterSample("v112.GetCreativeFieldValues", new v112.GetCreativeFieldValues());
      RegisterSample("v112.GetCreativeGroups", new v112.GetCreativeGroups());
      RegisterSample("v112.GetCreatives", new v112.GetCreatives());
      RegisterSample("v112.GetCreativeTypes", new v112.GetCreativeTypes());
      RegisterSample("v112.GetDFASite", new v112.GetDFASite());
      RegisterSample("v112.GetPlacements", new v112.GetPlacements());
      RegisterSample("v112.GetPlacementStrategies", new v112.GetPlacementStrategies());
      RegisterSample("v112.GetPlacementTypes", new v112.GetPlacementTypes());
      RegisterSample("v112.GetPricingTypes", new v112.GetPricingTypes());
      RegisterSample("v112.GetSize", new v112.GetSize());
      RegisterSample("v112.GetSubnetworks", new v112.GetSubnetworks());
      RegisterSample("v112.GetTagMethodTypes", new v112.GetTagMethodTypes());
      RegisterSample("v112.GetUserFilterTypes", new v112.GetUserFilterTypes());
      RegisterSample("v112.GetUserRoles", new v112.GetUserRoles());
      RegisterSample("v112.GetUsers", new v112.GetUsers());
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
      } else if (string.Compare(args[0], "--v111all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v111");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          RunASample(user, matchingItem.Value);
        }
      } else if (string.Compare(args[0], "--v112all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v112");
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

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
using Google.Api.Ads.Dfa.v111;

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
      RegisterSample("v111.AssignCreativesToPlacements", new v111.AssignCreativesToPlacements());
      RegisterSample("v111.Authenticate", new v111.Authenticate());
      RegisterSample("v111.CreateAdvertiser", new v111.CreateAdvertiser());
      RegisterSample("v111.CreateCampaign", new v111.CreateCampaign());
      RegisterSample("v111.CreateFlashInpageCreative", new v111.CreateFlashInpageCreative());
      RegisterSample("v111.CreateImageAsset", new v111.CreateImageAsset());
      RegisterSample("v111.CreateHTMLAsset", new v111.CreateHTMLAsset());
      RegisterSample("v111.CreatePlacement", new v111.CreatePlacement());
      RegisterSample("v111.CreateRotationGroup", new v111.CreateRotationGroup());
      RegisterSample("v111.CreateSpotlightActivity", new v111.CreateSpotlightActivity());
      RegisterSample("v111.CreateSpotlightActivityGroup", new v111.CreateSpotlightActivityGroup());
      RegisterSample("v111.DownloadTags", new v111.DownloadTags());
      RegisterSample("v111.GetActivityGroups", new v111.GetActivityGroups());
      RegisterSample("v111.GetActivityTypes", new v111.GetActivityTypes());
      RegisterSample("v111.GetAdTypes", new v111.GetAdTypes());
      RegisterSample("v111.GetAdTypesNoConfig", new v111.GetAdTypesNoConfig());
      RegisterSample("v111.GetAdvertisers", new v111.GetAdvertisers());
      RegisterSample("v111.GetCampaigns", new v111.GetCampaigns());
      RegisterSample("v111.GetCountries", new v111.GetCountries());
      RegisterSample("v111.GetCreatives", new v111.GetCreatives());
      RegisterSample("v111.GetCreativeTypes", new v111.GetCreativeTypes());
      RegisterSample("v111.GetPlacements", new v111.GetPlacements());
      RegisterSample("v111.GetPlacementTypes", new v111.GetPlacementTypes());
      RegisterSample("v111.GetPricingTypes", new v111.GetPricingTypes());
      RegisterSample("v111.GetSize", new v111.GetSize());
      RegisterSample("v111.GetTagMethodTypes", new v111.GetTagMethodTypes());
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

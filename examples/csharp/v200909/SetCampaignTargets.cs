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
using Google.Api.Ads.AdWords.v200909;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v200909 {
  /// <summary>
  /// This code example adds geo, language, and network targeting to an
  /// existing campaign. To get a campaign, run GetAllCampaigns.cs.
  ///
  /// Tags: CampaignTargetService.mutate
  /// </summary>
  class SetCampaignTargets : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds geo, language, and network targeting to an existing" +
            " campaign. To get a campaign, run GetAllCampaigns.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new SetCampaignTargets();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignTargetService.
      CampaignTargetService campaignTargetService =
          (CampaignTargetService)user.GetService(AdWordsService.v200909.CampaignTargetService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      // Create language targets.
      LanguageTargetList langTargetList = new LanguageTargetList();
      langTargetList.campaignId = campaignId;

      LanguageTarget langTarget1 = new LanguageTarget();
      langTarget1.languageCode = "fr";

      LanguageTarget langTarget2 = new LanguageTarget();
      langTarget2.languageCode = "ja";

      langTargetList.targets = new LanguageTarget[] {langTarget1,langTarget2};

      // Create language target set operation.
      CampaignTargetOperation langTargetOperation = new CampaignTargetOperation();
      langTargetOperation.@operator = Operator.SET;
      langTargetOperation.operand = langTargetList;

      // Create geo targets.
      GeoTargetList geoTargetList = new GeoTargetList();
      geoTargetList.campaignId = campaignId;

      CountryTarget geoTarget1 = new CountryTarget();
      geoTarget1.countryCode = "US";

      CountryTarget geoTarget2 = new CountryTarget();
      geoTarget2.countryCode = "JP";

      // Create geo target set operation.
      CampaignTargetOperation geoTargetOperation = new CampaignTargetOperation();
      geoTargetOperation.@operator = Operator.SET;
      geoTargetOperation.operand = geoTargetList;

      // Create network targets.
      NetworkTargetList networkTargetList = new NetworkTargetList();
      networkTargetList.campaignId = campaignId;

      // Specifying GOOGLE_SEARCH is necessary if you want to target SEARCH_NETWORK.
      NetworkTarget networkTarget1 = new NetworkTarget();
      networkTarget1.networkCoverageType = NetworkCoverageType.GOOGLE_SEARCH;

      NetworkTarget networkTarget2 = new NetworkTarget();
      networkTarget2.networkCoverageType = NetworkCoverageType.SEARCH_NETWORK;

      networkTargetList.targets = new NetworkTarget[] {networkTarget1, networkTarget2};

      // Create network target set operation.
      CampaignTargetOperation networkTargetOperation = new CampaignTargetOperation();
      networkTargetOperation.@operator = Operator.SET;
      networkTargetOperation.operand = networkTargetList;

      try {
        // Set campaign targets.
        CampaignTargetReturnValue retVal =
            campaignTargetService.mutate(new CampaignTargetOperation[] {
                geoTargetOperation, langTargetOperation,networkTargetOperation});

        if (retVal != null && retVal.value != null) {
          // Display campaign targets.
          foreach (TargetList targetList in retVal.value) {
            Console.WriteLine("Campaign target of type '{0}' was set to Campaign with" +
                " id = '{1}'.", targetList.TargetListType, targetList.campaignId);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to set Campaign target(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

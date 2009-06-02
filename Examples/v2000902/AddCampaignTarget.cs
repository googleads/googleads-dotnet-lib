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

using System;
using System.Collections;

using com.google.api.adwords.lib;
using com.google.api.adwords.v200902.CampaignTargetService;

namespace com.google.api.adwords.samples.v200902 {
  /// <summary>
  /// This code sample adds geo, language, and network targeting to an
  /// existing campaign. To create a campaign, you can run AddCampaign.cs.
  /// </summary>
  class AddCampaignTarget : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {return "Create Campaign target (geo, language, network)";}
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      CampaignTargetService service =
          (CampaignTargetService) user.GetService(ApiServices.v200902.CampaignTargetService);

      CampaignId campaignId = new CampaignId();
      campaignId.idSpecified = true;
      campaignId.id = long.Parse("INSERT_CAMPAIGN_ID_HERE");

      // Create a language target - for English language.
      LanguageTarget languageTarget = new LanguageTarget();
      languageTarget.languageCode = "en";
      LanguageTargetList languageTargetList = new LanguageTargetList();
      languageTargetList.targets = new LanguageTarget[] {languageTarget};
      languageTargetList.campaignId = campaignId;

      // Create a country target - include US, exclude metrocode 743.
      CountryTarget countryTarget = new CountryTarget();
      countryTarget.countryCode = "US";
      countryTarget.excludedSpecified = true;
      countryTarget.excluded = false;
      MetroTarget metroTarget = new MetroTarget();
      metroTarget.excludedSpecified = true;
      metroTarget.excluded = true;
      metroTarget.metroCode = "743";

      GeoTargetList geoTargetList = new GeoTargetList();
      geoTargetList.targets = new GeoTarget[] {countryTarget, metroTarget};
      geoTargetList.campaignId = campaignId;

      // Create a network target - Google Search.
      NetworkTarget networkTarget1 = new NetworkTarget();
      networkTarget1.networkCoverageTypeSpecified = true;
      networkTarget1.networkCoverageType = NetworkCoverageType.GOOGLE_SEARCH;
      NetworkTarget networkTarget2 = new NetworkTarget();
      networkTarget2.networkCoverageTypeSpecified = true;
      networkTarget2.networkCoverageType = NetworkCoverageType.SEARCH_NETWORK;

      NetworkTargetList networkTargetList = new NetworkTargetList();
      networkTargetList.targets = new NetworkTarget[] {networkTarget1, networkTarget2};
      networkTargetList.campaignId = campaignId;

      TargetList[] targets =
          new TargetList[] {languageTargetList, geoTargetList, networkTargetList};

      ArrayList campaignTargetOperations = new ArrayList();

      foreach (TargetList target in targets) {
        CampaignTargetOperation ops = new CampaignTargetOperation();
        ops.operatorSpecified = true;
        ops.@operator = Operator.SET;
        ops.operand = target;
        campaignTargetOperations.Add(ops);
      }

      try {
        service.mutate((CampaignTargetOperation[])
          campaignTargetOperations.ToArray(typeof(CampaignTargetOperation)));
          Console.WriteLine("Geo, language, and network targeting were " +
              "successfully added to campaign id = \"{0}\".", campaignId.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to create campaign targeting. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

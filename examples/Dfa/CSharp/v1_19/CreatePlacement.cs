// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfa.v1_19;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_19 {
  /// <summary>
  /// This code example creates a placement in a given campaign. This code
  /// example requires the DFA site id and campaign id in which the placement
  /// will be created into. To create a campaign, run CreateCampaign.cs.
  /// To get DFA site id, run GetDfaSite.cs. To get a size id, run
  /// GetSize.cs. To get placement types, run GetPlacementTypes.cs. To get
  /// pricing types, run GetPricingTypes.cs.
  ///
  /// Tags: placement.savePlacement
  /// </summary>
  class CreatePlacement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a placement in a given campaign. This code example " +
            "requires the DFA site id and campaign id in which the placement will be created " +
            "into. To create a campaign, run CreateCampaign.cs. To get DFA site id, run " +
            "GetDfaSite.cs. To get a size id, run GetSize.cs. To get placement types, run " +
            "GetPlacementTypes.cs. To get pricing types, run GetPricingTypes.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreatePlacement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create PlacementRemoteService instance.
      PlacementRemoteService service = (PlacementRemoteService) user.GetService(
          DfaService.v1_19.PlacementRemoteService);
      string placementName = _T("INSERT_PLACEMENT_NAME_HERE");
      long dfaSiteId = long.Parse(_T("INSERT_DFA_SITE_ID_HERE"));
      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));
      int pricingType = int.Parse(_T("INSERT_PRICING_TYPE_HERE"));
      int placementType = int.Parse(_T("INSERT_PLACEMENT_TYPE_HERE"));
      long sizeId = long.Parse(_T("INSERT_SIZE_ID_HERE"));

      // Create placement structure.
      Placement placement = new Placement();
      placement.id = 0;
      placement.name = placementName;
      placement.campaignId = campaignId;
      placement.dfaSiteId = dfaSiteId;
      placement.sizeId = sizeId;

      // Set pricing schedule for placement.
      PricingSchedule pricingSchedule = new PricingSchedule();
      pricingSchedule.startDate = DateTime.Now;
      pricingSchedule.endDate = DateTime.Now.AddMonths(1);
      pricingSchedule.pricingType = pricingType;
      placement.pricingSchedule = pricingSchedule;

      // Set placement type.
      placement.placementType = placementType;

      try {
        // Set placement tag settings.
        TagSettings tagSettings = new TagSettings();
        PlacementTagOption[] placementTagOptions = service.getRegularPlacementTagOptions();
        int[] tagTypes = new int[placementTagOptions.Length];

        for (int i = 0; i < placementTagOptions.Length; i++) {
          tagTypes[i] = (int) placementTagOptions[i].id;
        }

        tagSettings.tagTypes = tagTypes;
        placement.tagSettings = tagSettings;

        // Create the placement.
        PlacementSaveResult placementSaveResult = service.savePlacement(placement);

        // Display new placment id.
        Console.WriteLine("Placment with id \"{0}\" was created.", placementSaveResult.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to create placement. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

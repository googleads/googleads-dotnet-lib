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
using Google.Api.Ads.Dfa.v1_11;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_11 {
  /// <summary>
  /// This code example downloads HTML Tags for a given campaign and placement
  /// id. To create campaigns, run CreateCampaigns.cs. To create placements, run
  /// GetPlacements.cs.
  /// </summary>
  class DownloadTags : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example downloads HTML Tags for a given campaign and placement id. To " +
            "create campaigns, run CreateCampaigns.cs. To create placements, run GetPlacements.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DownloadTags();
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
          DfaService.v1_11.PlacementRemoteService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));
      long placementId = long.Parse(_T("INSERT_PLACEMENT_ID_HERE"));

      // Set placement tag search criteria.
      PlacementTagCriteria placementTagCriteria = new PlacementTagCriteria();
      placementTagCriteria.id = placementId;

      try {
        // Get placement tag options.
        PlacementTagOption[] placementTagOptions = service.getRegularPlacementTagOptions();

        long[] tagOptionIds = new long[placementTagOptions.Length];

        // Add all types of tags to the tag option structure.
        for (int i = 0; i < placementTagOptions.Length; i++) {
          tagOptionIds[i] = placementTagOptions[i].id;
        }

        placementTagCriteria.tagOptionIds = tagOptionIds;

        // Get HTML tags for the placements.
        PlacementTagData placementTagData = service.getPlacementTagData(campaignId,
            new PlacementTagCriteria[] {placementTagCriteria});

        // Display tags for the placement id used as criteria.
        PlacementTagInfo temp = placementTagData.placementTagInfos[0];
        Console.WriteLine("Placement name : {0}\nIframe/JavaScript tag : {1}\nStandard tag : {2}" +
            "\nInternal Redirect tag : {3}", temp.placement.name, temp.iframeJavaScriptTag,
            temp.javaScriptTag, temp.internalRedirectTag);
      } catch (Exception ex) {
        Console.WriteLine("Failed to download tags. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

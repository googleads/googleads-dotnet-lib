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
using Google.Api.Ads.Dfa.v1_14;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_14 {
  /// <summary>
  /// This code example creates a rotation group ad in a given campaign. To get
  /// ad types run GetAdTypes.cs. Start and end date for the ad must be within
  /// campaign start and end dates. To create creatives, run
  /// CreateCreatives.cs. To get available placements, run GetPlacement.cs.
  /// To get a size id, run GetSize.cs.
  ///
  /// Tags: ad.saveAd
  /// </summary>
  class CreateRotationGroup : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a rotation group ad in a given campaign. To get ad " +
            "types run GetAdTypes.cs. Start and end date for the ad must be within campaign " +
            "start and end dates. To create creatives, run CreateCreatives.cs. To get " +
            "available placements, run GetPlacement.cs. To get a size id, run GetSize.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateRotationGroup();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create AdRemoteService instance.
      AdRemoteService service = (AdRemoteService) user.GetService(
          DfaService.v1_14.AdRemoteService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));
      long sizeId = long.Parse(_T("INSERT_SIZE_ID_HERE"));
      long creativeId = long.Parse(_T("INSERT_CREATIVE_ID_HERE"));
      long placementId = long.Parse(_T("INSERT_PLACEMENT_ID_HERE"));
      String adName = _T("INSERT_AD_NAME_HERE");
      long typeId = long.Parse(_T("INSERT_AD_TYPEID_HERE"));

      // Create rotation group structure.
      RotationGroup rotationGroup = new RotationGroup();
      rotationGroup.id = 0;
      rotationGroup.name = adName;
      rotationGroup.active = true;
      rotationGroup.archived = false;
      rotationGroup.campaignId = campaignId;
      rotationGroup.sizeId = sizeId;
      rotationGroup.typeId = typeId;
      rotationGroup.priority = 12;
      rotationGroup.ratio = 1;

      // Set ad start and end dates.
      rotationGroup.startTime = DateTime.Today.AddDays(1);
      rotationGroup.endTime = DateTime.Today.AddMonths(1);

      // Add creatives to the ad.
      CreativeAssignment creativeAssignment = new CreativeAssignment();
      creativeAssignment.active = true;
      creativeAssignment.creativeId = creativeId;

      // Create click through url.
      ClickThroughUrl clickThroughUrl = new ClickThroughUrl();
      clickThroughUrl.defaultLandingPageUsed = true;
      clickThroughUrl.landingPageId = 0;
      creativeAssignment.clickThroughUrl = clickThroughUrl;

      // Create creative assigments.
      rotationGroup.creativeAssignments = new CreativeAssignment[] {creativeAssignment};
      rotationGroup.rotationType = 1;

      // Assign ad to placement.
      PlacementAssignment placementAssignment = new PlacementAssignment();
      placementAssignment.active = true;
      placementAssignment.placementId = placementId;
      rotationGroup.placementAssignments = new PlacementAssignment[] {placementAssignment};

      try {
        // Create rotation group.
        AdSaveResult result = service.saveAd(rotationGroup);

        // Display new ad id.
        Console.WriteLine("Ad with id \"{0}\" was created.", result.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to create rotation group. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

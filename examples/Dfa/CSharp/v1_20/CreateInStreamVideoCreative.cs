// Copyright 2013, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfa.v1_20;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_20 {
  /// <summary>
  /// This code example creates an In-Stream video creative associated with a
  /// given advertiser. If a campaign is specified, the creative is also
  /// associated with that campaign. To associate In-Stream assets with an
  /// In-Stream video creative, first create the creative and then run
  /// UploadInStreamAsset.cs.
  ///
  /// Tags: creative.saveCreative
  /// </summary>
  class CreateInStreamVideoCreative : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates an In-Stream video creative associated with a given " +
            "advertiser. If a campaign is specified, the creative is also associated with that " +
            "campaign. To associate In-Stream assets with an In-Stream video creative, first " +
            "create the creative and then run UploadInStreamAsset.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateInStreamVideoCreative();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Request the creative service from the service client factory.
      CreativeRemoteService creativeService = (CreativeRemoteService) user.GetService(
          DfaService.v1_20.CreativeRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));
      long campaignId = 0;
      float videoDuration = float.Parse(_T("INSERT_VIDEO_DURATION_HERE"));
      string adId = _T("INSERT_VAST_AD_ID_HERE");
      string surveyUrl = _T("INSERT_VAST_SURVEY_URL_HERE");
      string clickThroughUrl = _T("INSERT_VAST_CLICK_THROUGH_URL_HERE");

      // Create the In-Stream video creative.
      InStreamVideoCreative inStreamVideoCreative = new InStreamVideoCreative();
      inStreamVideoCreative.advertiserId = advertiserId;
      inStreamVideoCreative.name = "In-Stream Video Creative #" + GetTimeStamp();
      inStreamVideoCreative.videoDuration = videoDuration;
      // In-Stream video creatives have to be created inactive. One can only be
      // set active after at least one media file has been added to it or the API
      // will return an error message.
      inStreamVideoCreative.active = false;

      // Set the video details based on the Video Ad Serving Template (VAST)
      // specification.
      inStreamVideoCreative.adId = adId;
      inStreamVideoCreative.description = "You are viewing an In-Stream Video Creative";
      inStreamVideoCreative.surveyUrl = surveyUrl;
      inStreamVideoCreative.clickThroughUrl = clickThroughUrl;

      try {
        // Save the In-Stream video creative.
        CreativeSaveResult creativeSaveResult = creativeService.saveCreative(inStreamVideoCreative,
            campaignId);

        // Display the new creative ID.
        Console.WriteLine("In-Stream video creative with ID \"{0}\" was created.",
            creativeSaveResult.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to create in-stream video creative. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

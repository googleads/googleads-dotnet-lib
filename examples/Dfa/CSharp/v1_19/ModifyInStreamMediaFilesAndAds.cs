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
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_19 {
  /// <summary>
  /// This code example retrieves an In-Stream video creative and modifies its
  /// media files, companion ads, and non-linear ads. You are not given the
  /// opportunity to set the fields on these components when they are created by
  /// uploading creative assets. Therefore, you must upload the assets first and
  /// then set any additional fields in a second request.
  ///
  /// To create an In-Stream video creative, run CreateInStreamVideoCreative.cs.
  /// To add an In-Stream asset to an In-Stream video creative, run
  /// UploadInStreamAsset.cs.
  ///
  /// Tags: creative.saveCreative
  /// </summary>
  class ModifyInStreamMediaFilesAndAds : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves an In-Stream video creative and modifies its media " +
            "files, companion ads, and non-linear ads. You are not given the opportunity to " +
            "set the fields on these components when they are created by uploading creative " +
            "assets. Therefore, you must upload the assets first and then set any additional " +
            "fields in a second request.\nTo create an In-Stream video creative, run " +
            "CreateInStreamVideoCreative.cs. To add an In-Stream asset to an In-Stream video " +
            "creative, run UploadInStreamAsset.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ModifyInStreamMediaFilesAndAds();
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
          DfaService.v1_19.CreativeRemoteService);

      long creativeId = long.Parse(_T("INSERT_IN_STREAM_VIDEO_CREATIVE_ID_HERE"));

      try {
        // Fetch the In-Stream video creative which contains the asset to modify.
        CreativeBase rawCreative = creativeService.getCreative(creativeId);

        if (!(rawCreative is InStreamVideoCreative)) {
          Console.WriteLine("Unable to update creative with ID \"{0}\": not an In-Stream video "
              + "creative.", creativeId);
        } else {
          InStreamVideoCreative inStreamVideoCreative = (InStreamVideoCreative) rawCreative;

          // Modify the media files, companion ads, and/or non-linear ads.
          if (inStreamVideoCreative.mediaFiles != null) {
            foreach (InStreamMediaFile mediaFile in inStreamVideoCreative.mediaFiles) {
              mediaFile.pickedToServe = !mediaFile.pickedToServe;
            }
          }

          if (inStreamVideoCreative.companionAds != null) {
            foreach (InStreamCompanionAd companionAd in inStreamVideoCreative.companionAds) {
              companionAd.altText = companionAd.altText + " Updated.";
            }
          }

          if (inStreamVideoCreative.nonLinearAds != null) {
            foreach (InStreamNonLinearAd nonLinearAd in inStreamVideoCreative.nonLinearAds) {
              nonLinearAd.scalable = !nonLinearAd.scalable;
            }
          }

          CreativeSaveResult creativeSaveResult =
              creativeService.saveCreative(inStreamVideoCreative, 0);

          Console.WriteLine("Updated the In-Stream assets of In-Stream video creative with ID "
              + "\"{0}\".", creativeSaveResult.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update in-stream assets of in-stream video creative. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

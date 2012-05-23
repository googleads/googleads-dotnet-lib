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
using Google.Api.Ads.Dfa.v1_18;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_18 {
  /// <summary>
  /// This code example replaces an In-Stream asset in an existing In-Stream
  /// video creative with a new In-Stream asset. To create an In-Stream video
  /// creative, run CreateInStreamVideoCreative.cs. To add an In-Stream asset to
  /// an In-Stream video creative, run UploadInStreamAsset.cs.
  ///
  /// This code example replaces a companion ad asset in the target creative
  /// because the <code>companion<code> flag on the
  /// <code>InStreamAssetUploadRequest</code> was set to <code>true</code>.
  /// You can use the same workflow to replace a non-linear ad by setting the
  /// <code>nonLinear</code> flag instead. You may not use this method to swap
  /// out media files (a.k.a. video assets).
  ///
  /// Tags: creative.replaceInStreamAsset
  /// </summary>
  class ReplaceInStreamAsset : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example replaces an In-Stream asset in an existing In-Stream video " +
            "creative with a new In-Stream asset. To create an In-Stream video creative, " +
            "run CreateInStreamVideoCreative.cs. To add an In-Stream asset to an In-Stream " +
            "video creative, run UploadInStreamAsset.cs.\nThis code example replaces a " +
            "companion ad asset in the target creative because the companion flag on the " +
            "InStreamAssetUploadRequest was set to true. You can use the same workflow to " +
            "replace a non-linear ad by setting the nonLinear flag instead. You may not use " +
            "this method to swap out media files (a.k.a. video assets).";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ReplaceInStreamAsset();
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
          DfaService.v1_18.CreativeRemoteService);

      string assetName = _T("INSERT_ASSET_NAME_HERE");
      string pathToFile = _T("INSERT_PATH_TO_FILE_HERE");
      long creativeId = long.Parse(_T("INSERT_IN_STREAM_VIDEO_CREATIVE_ID_HERE"));
      string assetToReplace = _T("INSERT_ASSET_TO_REPLACE_HERE");

      // Create the In-Stream creative asset.
      CreativeAsset inStreamVideoAsset = new CreativeAsset();
      inStreamVideoAsset.name = assetName;
      inStreamVideoAsset.content = MediaUtilities.GetAssetDataFromUrl(
          new Uri(pathToFile).AbsoluteUri);

      // Create an upload request to make this asset a companion ad file for an
      // existing In-Stream video creative.
      InStreamAssetUploadRequest inStreamAssetUploadRequest = new InStreamAssetUploadRequest();
      inStreamAssetUploadRequest.companion = true;
      inStreamAssetUploadRequest.inStreamAsset = inStreamVideoAsset;
      inStreamAssetUploadRequest.creativeId = creativeId;

      try {
        // Replace the existing asset with a newly uploaded asset.
        InStreamVideoCreative inStreamVideoCreative =
            creativeService.replaceInStreamAsset(assetToReplace, inStreamAssetUploadRequest);

        // Display a success message.
        Console.WriteLine("Replaced companion ad asset \"{0}\" in In-Stream video creative "
            + "with ID \"{1}\".%n", assetToReplace, inStreamVideoCreative.id);

      } catch (Exception ex) {
        Console.WriteLine("Failed to replace companion ad asset in in-stream video creative. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

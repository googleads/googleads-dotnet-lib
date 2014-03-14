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
  /// This code example uploads an In-Stream video asset into an existing
  /// In-Stream video creative. To create an In-Stream video creative, run
  /// CreateInStreamVideoCreative.cs.
  ///
  /// This code example creates a media file in the target creative because the
  /// <code>mediaFile<code> flag on the <code>InStreamAssetUploadRequest</code>
  /// was set to <code>true</code>. You can use the same workflow to upload
  /// companion ads or non-linear ads to your creative by setting the
  /// <code>companion</code> or <code>nonLinear</code> flags instead,
  /// respectively. Only one flag may be set per upload request.
  ///
  /// Tags: creative.uploadInStreamAsset
  /// </summary>
  class UploadInStreamAsset : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example uploads an In-Stream video asset into an existing In-Stream " +
            "video creative. To create an In-Stream video creative, run " +
            "CreateInStreamVideoCreative.cs.\nThis code example creates a media file in the " +
            "target creative because the mediaFile flag on the InStreamAssetUploadRequest was " +
            "set to true. You can use the same workflow to upload companion ads or non-linear " +
            "ads to your creative by setting the companion or nonLinear flags instead, " +
            "respectively. Only one flag may be set per upload request.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UploadInStreamAsset();
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

      string assetName = _T("INSERT_ASSET_NAME_HERE");
      string pathToFile = _T("INSERT_PATH_TO_FILE_HERE");
      long creativeId = long.Parse(_T("INSERT_IN_STREAM_VIDEO_CREATIVE_ID_HERE"));

      // Create the In-Stream video creative asset.
      CreativeAsset inStreamVideoAsset = new CreativeAsset();
      inStreamVideoAsset.name = assetName;
      inStreamVideoAsset.content = MediaUtilities.GetAssetDataFromUrl(
          new Uri(pathToFile).AbsolutePath);

      // Create an upload request to make this asset a media file for an existing
      // In-Stream creative.
      InStreamAssetUploadRequest inStreamAssetUploadRequest = new InStreamAssetUploadRequest();
      inStreamAssetUploadRequest.mediaFile = true;
      inStreamAssetUploadRequest.inStreamAsset = inStreamVideoAsset;
      inStreamAssetUploadRequest.creativeId = creativeId;

      try {
        // Save the media file.
        InStreamVideoCreative inStreamVideoCreative =
            creativeService.uploadInStreamAsset(inStreamAssetUploadRequest);

        // Display a success message.
        Console.WriteLine("Added a media file to In-Stream video creative with ID \"{0}\".",
            inStreamVideoCreative.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to add a media file to in-stream video creative. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

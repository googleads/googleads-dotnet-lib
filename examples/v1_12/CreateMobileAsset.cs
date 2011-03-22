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
using Google.Api.Ads.Dfa.v1_12;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.v1_12 {
  /// <summary>
  /// This code example creates a mobile creative asset in a given advertiser.
  /// Currently only gif, jpg, jpeg, png and wbmp files are supported as mobile
  /// assets. To create an advertiser, run CreateAdvertiser.cs.
  /// </summary>
  class CreateMobileAsset : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a mobile creative asset in a given advertiser. " +
            "Currently only gif, jpg, jpeg, png and wbmp files are supported as mobile assets. " +
            "To create an advertiser, run CreateAdvertiser.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateMobileAsset();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CreateRemoteService instance.
      CreativeRemoteService service = (CreativeRemoteService) user.GetService(
          DfaService.v1_12.CreativeRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));
      string assetName = _T("INSERT_MOBILE_ASSET_NAME_HERE");
      string pathToFile = _T("INSERT_PATH_TO_MOBILE_ASSET_HERE");

      // Set mobile asset structure.
      CreativeAsset mobileAsset = new CreativeAsset();
      mobileAsset.forHTMLCreatives = true;
      mobileAsset.advertiserId = advertiserId;
      mobileAsset.content = MediaUtilities.GetAssetDataFromUrl(new Uri(pathToFile));
      mobileAsset.name = assetName;

      try {
        // Create mobile asset.
        CreativeAssetSaveResult result = service.saveCreativeAsset(mobileAsset);

        // Display asset file name.
        Console.WriteLine("Asset was saved with file name of \"{0}\".", result.savedFilename);
      } catch (Exception ex) {
        Console.WriteLine("Failed to create mobile asset. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

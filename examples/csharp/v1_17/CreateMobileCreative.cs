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
using Google.Api.Ads.Dfa.v1_17;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_17 {
  /// <summary>
  /// This code example creates a mobile display creative in a given advertiser
  /// or campaign. If no campaign is specified then the creative is created in
  /// the advertiser provided. To get assets file names, run CreateHTMLAsset.cs
  /// and CreateImageAsset.cs. To get a size id, run GetSize.cs. To get a
  /// creative type id, run GetCreativeType.cs.
  ///
  /// Tags: creative.saveCreative
  /// </summary>
  class CreateMobileCreative : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a mobile display creative in a given advertiser or " +
            "campaign. If no campaign is specified then the creative is created in the " +
            "advertiser provided. To get assets file names, run CreateHTMLAsset.cs and " +
            "CreateImageAsset.cs. To get a size id, run GetSize.cs. To get a creative type " +
            "id, run GetCreativeType.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateMobileCreative();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CreativeRemoteService instance.
      CreativeRemoteService service = (CreativeRemoteService) user.GetService(
          DfaService.v1_17.CreativeRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));
      String creativeName = _T("INSERT_CREATIVE_NAME_HERE");
      String mobileAssetFileName = _T("INSERT_MOBILE_ASSET_FILE_NAME_HERE");
      long typeId = long.Parse(_T("INSERT_TYPE_ID_HERE"));

      // Set mobile display creative structure.
      MobileDisplayCreative mobileDisplayCreative = new MobileDisplayCreative();
      mobileDisplayCreative.advertiserId = advertiserId;
      mobileDisplayCreative.name = creativeName;
      mobileDisplayCreative.typeId = typeId;
      mobileDisplayCreative.archived = false;

      // Set mobile creative asset.
      HTMLCreativeAsset htmlCreativeAsset = new MobileDisplayCreativeAsset();
      htmlCreativeAsset.assetFilename = mobileAssetFileName;
      mobileDisplayCreative.creativeAssets = new HTMLCreativeAsset[] {htmlCreativeAsset};

      try {
        // Create mobile display creative.
        CreativeSaveResult creativeSaveResult = service.saveCreative(mobileDisplayCreative, 0);

        // Display new creative id.
        Console.WriteLine("Mobile display creative with id \"{0}\" was created.",
            creativeSaveResult.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to create display creative. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

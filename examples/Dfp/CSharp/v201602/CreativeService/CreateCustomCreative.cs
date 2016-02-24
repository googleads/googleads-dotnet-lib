// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates a custom creative for a given advertiser. To
  /// determine which companies are advertisers, run GetCompaniesByStatement.cs.
  /// To determine which creatives already exist, run GetAllCreatives.cs.
  /// </summary>
  class CreateCustomCreative : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a custom creative for a given advertiser. To determine " +
            "which companies are advertisers, run GetCompaniesByStatement.cs. To determine which " +
            "creatives already exist, run GetAllCreatives.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateCustomCreative();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CreativeService.
      CreativeService creativeService =
          (CreativeService) user.GetService(DfpService.v201602.CreativeService);

      // Set the ID of the advertiser (company) that all creatives will be
      // assigned to.
      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));

      // Create the local custom creative object.
      CustomCreative customCreative = new CustomCreative();
      customCreative.name = "Custom creative " + GetTimeStamp();
      customCreative.advertiserId = advertiserId;
      customCreative.destinationUrl = "http://google.com";

      // Set the custom creative image asset.
      CustomCreativeAsset customCreativeAsset = new CustomCreativeAsset();
      customCreativeAsset.macroName = "IMAGE_ASSET";
      CreativeAsset asset = new CreativeAsset();
      asset.fileName = string.Format("inline{0}.jpg", GetTimeStamp());
      asset.assetByteArray = MediaUtilities.GetAssetDataFromUrl(
          "http://www.google.com/intl/en/adwords/select/images/samples/inline.jpg");
      customCreativeAsset.asset = asset;

      customCreative.customCreativeAssets = new CustomCreativeAsset[] {customCreativeAsset};

      // Set the HTML snippet using the custom creative asset macro.
      customCreative.htmlSnippet = "<a href='%%CLICK_URL_UNESC%%%%DEST_URL%%'>" +
          "<img src='%%FILE:" + customCreativeAsset.macroName + "%%'/>" +
          "</a><br>Click above for great deals!";

      // Set the creative size.
      Size size = new Size();
      size.width = 300;
      size.height = 250;
      size.isAspectRatio = false;

      customCreative.size = size;

      try {
        // Create the custom creative on the server.
        Creative[] createdCreatives = creativeService.createCreatives(
            new Creative[] {customCreative});

        foreach (Creative createdCreative in createdCreatives) {
          Console.WriteLine("A custom creative with ID \"{0}\", name \"{1}\", and size ({2}, " +
              "{3}) was created and can be previewed at {4}", createdCreative.id,
              createdCreative.name, createdCreative.size.width, createdCreative.size.height,
              createdCreative.previewUrl);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create custom creatives. Exception says \"{0}\"", e.Message);
      }
    }
  }
}

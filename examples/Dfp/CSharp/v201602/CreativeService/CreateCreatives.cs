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
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates new image creatives for a given advertiser. To
  /// determine which companies are advertisers, run GetCompaniesByStatement.cs.
  /// To determine which creatives already exist, run GetAllCreatives.cs.
  /// </summary>
  class CreateCreatives : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new image creatives for a given advertiser. To " +
            "determine which companies are advertisers, run GetCompaniesByStatement.cs. " +
            "To determine which creatives already exist, run GetAllCreatives.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateCreatives();
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
      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_COMPANY_ID_HERE"));

      // Create an array to store local image creative objects.
      Creative[] imageCreatives = new ImageCreative[5];

      for (int i = 0; i < 5; i++) {
        // Create creative size.
        Size size = new Size();
        size.width = 300;
        size.height = 250;

        // Create an image creative.
        ImageCreative imageCreative = new ImageCreative();
        imageCreative.name = string.Format("Image creative #{0}", i);
        imageCreative.advertiserId = advertiserId;
        imageCreative.destinationUrl = "http://www.google.com";
        imageCreative.size = size;

        // Create image asset.
        CreativeAsset creativeAsset = new CreativeAsset();
        creativeAsset.fileName = "image.jpg";
        creativeAsset.assetByteArray = MediaUtilities.GetAssetDataFromUrl(
            "http://www.google.com/intl/en/adwords/select/images/samples/inline.jpg");
        creativeAsset.size = size;
        imageCreative.primaryImageAsset = creativeAsset;

        imageCreatives[i] = imageCreative;
      }

      try {
        // Create the image creatives on the server.
        imageCreatives = creativeService.createCreatives(imageCreatives);

        if (imageCreatives != null) {
          foreach (Creative creative in imageCreatives) {
            // Use "is" operator to determine what type of creative was
            // returned.
            if (creative is ImageCreative) {
              ImageCreative imageCreative = (ImageCreative) creative;
              Console.WriteLine("An image creative with ID ='{0}', name ='{1}' and size = " +
                  "({2},{3}) was created and can be previewed at: {4}", imageCreative.id,
                  imageCreative.name, imageCreative.size.width, imageCreative.size.height,
                  imageCreative.previewUrl);
            } else {
              Console.WriteLine("A creative with ID ='{0}', name='{1}' and type='{2}' was created.",
                  creative.id, creative.name, creative.GetType().Name);
            }
          }
        } else {
          Console.WriteLine("No creatives created.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create creatives. Exception says \"{0}\"", e.Message);
      }
    }
  }
}

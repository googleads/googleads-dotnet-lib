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
  /// This code example creates a copy of an image creative. This would
  /// typically be done to reuse creatives in a small business network. To
  /// determine which creatives exist, run GetAllCreatives.cs.
  /// </summary>
  class CopyImageCreatives : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a copy of an image creative. This would typically be" +
            " done to reuse creatives in a small business network. To determine which creatives " +
            "exist, run GetAllCreatives.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CopyImageCreatives();
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

      long creativeId = long.Parse(_T("INSERT_IMAGE_CREATIVE_ID_HERE"));

      // Create a statement to get the image creative.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("id", creativeId);

      try {
        // Get the creative.
        CreativePage page = creativeService.getCreativesByStatement(statementBuilder.ToStatement());

        if (page.results != null) {
          ImageCreative imageCreative = (ImageCreative) page.results[0];
          // Since we cannot set id to null, we mark it as not specified.
          imageCreative.idSpecified = false;

          imageCreative.advertiserId = imageCreative.advertiserId;
          imageCreative.name = imageCreative.name + " (Copy #" + GetTimeStamp() + ")";

          // Create image asset.
          CreativeAsset creativeAsset = new CreativeAsset();
          creativeAsset.fileName = "image.jpg";
          creativeAsset.assetByteArray = MediaUtilities.GetAssetDataFromUrl(
              imageCreative.primaryImageAsset.assetUrl);

          creativeAsset.size = imageCreative.primaryImageAsset.size;
          imageCreative.primaryImageAsset = creativeAsset;

          // Create the copied creative.
          Creative[] creatives = creativeService.createCreatives(new Creative[] {imageCreative});

          // Display copied creatives.
          foreach (Creative copiedCreative in creatives) {
            Console.WriteLine("Image creative with ID \"{0}\", name \"{1}\", and type \"{2}\" " +
                "was created and can be previewed at {3}", copiedCreative.id, copiedCreative.name,
                 copiedCreative.GetType().Name, copiedCreative.previewUrl);
          }
        } else {
          Console.WriteLine("No creatives were copied.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to copy creatives. Exception says \"{0}\"", e.Message);
      }
    }
  }
}

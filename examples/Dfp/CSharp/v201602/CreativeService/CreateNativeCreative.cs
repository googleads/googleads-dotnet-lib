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
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates a new native creative. To determine which creatives
  /// already exist, run GetAllCreatives.cs.
  /// </summary>
  class CreateNativeCreative : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a new native creative. To determine which creatives " +
            "already exist, run GetAllCreatives.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateNativeCreative();
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

      // Set the ID of the advertiser (company) that the creative will be
      // assigned to.
      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_COMPANY_ID_HERE"));

      // Use the system defined native app install creative template.
      long creativeTemplateId = 10004400L;

      // Create a native app install creative for the Pie Noon app.
      TemplateCreative nativeAppInstallCreative = new TemplateCreative();
      nativeAppInstallCreative.name =
          String.Format("Native creative #{0}", new Random().Next(int.MaxValue));
      nativeAppInstallCreative.advertiserId = advertiserId;
      nativeAppInstallCreative.creativeTemplateId = creativeTemplateId;
      nativeAppInstallCreative.destinationUrl =
          "https://play.google.com/store/apps/details?id=com.google.fpl.pie_noon";

      // Use 1x1 as the size for native creatives.
      Size size = new Size();
      size.width = 1;
      size.height = 1;
      size.isAspectRatio = false;
      nativeAppInstallCreative.size = size;

      List<BaseCreativeTemplateVariableValue> templateVariables =
          new List<BaseCreativeTemplateVariableValue>();

      // Set the headline.
      templateVariables.Add(new StringCreativeTemplateVariableValue() {
        uniqueName = "Headline",
        value = "Pie Noon"
      });

      // Set the body text.
      templateVariables.Add(new StringCreativeTemplateVariableValue() {
        uniqueName = "Body",
        value = "Try multi-screen mode!"
      });

      // Set the image asset.
      templateVariables.Add(new AssetCreativeTemplateVariableValue() {
        uniqueName = "Image",
        asset = new CreativeAsset() {
          fileName = String.Format("image{0}.png", this.GetTimeStamp()),
          assetByteArray = MediaUtilities.GetAssetDataFromUrl("https://lh4.ggpht.com/"
              + "GIGNKdGHMEHFDw6TM2bgAUDKPQQRIReKZPqEpMeEhZOPYnTdOQGaSpGSEZflIFs0iw=h300")
        }
      });

      // Set the price.
      templateVariables.Add(new StringCreativeTemplateVariableValue() {
        uniqueName = "Price",
        value = "Free"
      });

      // Set app icon image asset.
      templateVariables.Add(new AssetCreativeTemplateVariableValue() {
        uniqueName = "Appicon",
        asset = new CreativeAsset() {
          fileName = String.Format("icon{0}.png", this.GetTimeStamp()),
          assetByteArray = MediaUtilities.GetAssetDataFromUrl("https://lh6.ggpht.com/"
              + "Jzvjne5CLs6fJ1MHF-XeuUfpABzl0YNMlp4RpHnvPRCIj4--eTDwtyouwUDzVVekXw=w300")
        }
      });

      // Set the call to action text.
      templateVariables.Add(new StringCreativeTemplateVariableValue() {
        uniqueName = "Calltoaction",
        value = "Install"
      });

      // Set the star rating.
      templateVariables.Add(new StringCreativeTemplateVariableValue() {
        uniqueName = "Starrating",
        value = "4"
      });

      // Set the store type.
      templateVariables.Add(new StringCreativeTemplateVariableValue() {
        uniqueName = "Store",
        value = "Google Play"
      });

      // Set the deep link URL.
      templateVariables.Add(new UrlCreativeTemplateVariableValue() {
        uniqueName = "DeeplinkclickactionURL",
        value = "market://details?id=com.google.fpl.pie_noon"
      });

      nativeAppInstallCreative.creativeTemplateVariableValues = templateVariables.ToArray();

      try {
        // Create the native creative on the server.
        Creative[] createdNativeCreatives = creativeService.createCreatives(
            new Creative[] { nativeAppInstallCreative });

        foreach (Creative createdNativeCreative in createdNativeCreatives) {
          Console.WriteLine("A native creative with ID \"{0}\" and name \"{1}\" " +
              "was created and can be previewed at {2}", createdNativeCreative.id,
              createdNativeCreative.name, createdNativeCreative.previewUrl);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create creatives. Exception says \"{0}\"", e.Message);
      }
    }
  }
}

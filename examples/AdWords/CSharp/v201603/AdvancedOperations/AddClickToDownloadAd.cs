// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example creates a click-to-download ad, also known as an
  /// app promotion ad to a given ad group. To list ad groups, run
  /// GetAdGroups.cs.
  /// </summary>
  public class AddClickToDownloadAd : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddClickToDownloadAd codeExample = new AddClickToDownloadAd();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a click-to-download ad, also known as an app " +
            "promotion ad to a given ad group. To list ad groups, run GetAdGroups.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to which ads are added.
    /// </param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupAdService.
      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v201603.AdGroupAdService);

      // Create the template ad.
      TemplateAd clickToDownloadAppAd = new TemplateAd();

      clickToDownloadAppAd.name = "Ad for demo game";
      clickToDownloadAppAd.templateId = 353;
      clickToDownloadAppAd.finalUrls = new string[] {
          "http://play.google.com/store/apps/details?id=com.example.demogame"
      };
      clickToDownloadAppAd.displayUrl = "play.google.com";

      // Create the template elements for the ad. You can refer to
      // https://developers.google.com/adwords/api/docs/appendix/templateads
      // for the list of avaliable template fields.
      TemplateElementField headline = new TemplateElementField();
      headline.name = "headline";
      headline.fieldText = "Enjoy your drive in Mars";
      headline.type = TemplateElementFieldType.TEXT;

      TemplateElementField description1 = new TemplateElementField();
      description1.name = "description1";
      description1.fieldText = "Realistic physics simulation";
      description1.type = TemplateElementFieldType.TEXT;

      TemplateElementField description2 = new TemplateElementField();
      description2.name = "description2";
      description2.fieldText = "Race against players online";
      description2.type = TemplateElementFieldType.TEXT;

      TemplateElementField appId = new TemplateElementField();
      appId.name = "appId";
      appId.fieldText = "com.example.demogame";
      appId.type = TemplateElementFieldType.TEXT;

      TemplateElementField appStore = new TemplateElementField();
      appStore.name = "appStore";
      appStore.fieldText = "2";
      appStore.type = TemplateElementFieldType.ENUM;

      TemplateElement adData = new TemplateElement();
      adData.uniqueName = "adData";
      adData.fields = new TemplateElementField[] {headline, description1, description2, appId,
          appStore};

      clickToDownloadAppAd.templateElements = new TemplateElement[] {adData};

      // Create the adgroupad.
      AdGroupAd clickToDownloadAppAdGroupAd = new AdGroupAd();
      clickToDownloadAppAdGroupAd.adGroupId = adGroupId;
      clickToDownloadAppAdGroupAd.ad = clickToDownloadAppAd;

      // Optional: Set the status.
      clickToDownloadAppAdGroupAd.status = AdGroupAdStatus.PAUSED;

      // Create the operation.
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.@operator = Operator.ADD;
      operation.operand = clickToDownloadAppAdGroupAd;

      try {
        // Create the ads.
        AdGroupAdReturnValue retval = adGroupAdService.mutate(new AdGroupAdOperation[] {operation});

        // Display the results.
        if (retval != null && retval.value != null) {
          foreach (AdGroupAd adGroupAd in retval.value) {
            Console.WriteLine("New click-to-download ad with id = \"{0}\" and url = \"{1}\" " +
                "was created.", adGroupAd.ad.id, adGroupAd.ad.finalUrls[0]);
          }
        } else {
          Console.WriteLine("No click-to-download ads were created.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create click-to-download ad.", e);
      }
    }
  }
}

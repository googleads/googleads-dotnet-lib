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

using System;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example adds an HTML5 ad to a given ad group. To get ad groups,
  /// run GetAdGroups.cs.
  /// </summary>
  public class AddHtml5Ad : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddHtml5Ad codeExample = new AddHtml5Ad();
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
        return "This code example adds an HTML5 ad to a given ad group. To get ad groups, run " +
            "GetAdGroups.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the first adgroup to which ad is added.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupAdService.
      AdGroupAdService adGroupAdService = (AdGroupAdService) user.GetService(
          AdWordsService.v201603.AdGroupAdService);

      // Create the HTML5 template ad. See
      // https://developers.google.com/adwords/api/docs/guides/template-ads#html5_ads
      // for more details.
      TemplateAd html5Ad = new TemplateAd() {
        name = "Ad for HTML5",
        templateId = 419,
        finalUrls = new string[] { "http://example.com/html5" },
        displayUrl = "www.example.com/html5",
        dimensions = new Dimensions() {
          width = 300,
          height = 250
        }
      };

      // The HTML5 zip file contains all the HTML, CSS, and images needed for the
      // HTML5 ad. For help on creating an HTML5 zip file, check out Google Web
      // Designer (https://www.google.com/webdesigner/).
      byte[] html5Zip = MediaUtilities.GetAssetDataFromUrl("https://goo.gl/9Y7qI2");

      // Create a media bundle containing the zip file with all the HTML5 components.
      MediaBundle mediaBundle = new MediaBundle() {
        // You may also upload an HTML5 zip using MediaService.upload() method
        // set the mediaId field. See UploadMediaBundle.cs for an example on
        // how to upload HTML5 zip files.
        data = html5Zip,
        entryPoint = "carousel/index.html",
        type = MediaMediaType.MEDIA_BUNDLE
      };

      // Create the template elements for the ad. You can refer to
      // https://developers.google.com/adwords/api/docs/appendix/templateads
      // for the list of available template fields.
      html5Ad.templateElements = new TemplateElement[] {
        new TemplateElement() {
          uniqueName = "adData",
          fields = new TemplateElementField[] {
            new TemplateElementField() {
              name = "Custom_layout",
              fieldMedia = mediaBundle,
              type = TemplateElementFieldType.MEDIA_BUNDLE
            },
            new TemplateElementField() {
              name = "layout",
              fieldText = "Custom",
              type = TemplateElementFieldType.ENUM
            },
          },
        }
      };

      // Create the AdGroupAd.
      AdGroupAd html5AdGroupAd = new AdGroupAd() {
        adGroupId = adGroupId,
        ad = html5Ad,
        // Additional properties (non-required).
        status = AdGroupAdStatus.PAUSED
      };
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation() {
        @operator = Operator.ADD,
        operand = html5AdGroupAd
      };

      try {
        // Add HTML5 ad.
        AdGroupAdReturnValue result =
          adGroupAdService.mutate(new AdGroupAdOperation[] { adGroupAdOperation });

        // Display results.
        if (result != null && result.value != null && result.value.Length > 0) {
          foreach (AdGroupAd adGroupAd in result.value) {
            Console.WriteLine("New HTML5 ad with id \"{0}\" and display url \"{1}\" was added.",
              adGroupAd.ad.id, adGroupAd.ad.displayUrl);
          }
        } else {
          Console.WriteLine("No HTML5 ads were added.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create HTML5 ad.", e);
      }
    }
  }
}

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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201101;
using Google.Api.Ads.Common.Util;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example adds a text, image ad, and template (Click to Play
  /// Video) ad to a given ad group. To get ad group, run GetAllAdGroups.cs.
  /// To get all videos, run GetAllVideos.cs. To upload video, see
  /// http://adwords.google.com/support/aw/bin/answer.py?hl=en&amp;answer=39454.
  ///
  /// Tags: AdGroupAdService.mutate
  /// </summary>
  class AddAds : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a text, image ad, and template (Click to Play Video) " +
            "ad to a given ad group. To get adgroup, run GetAllAdGroups.cs. To get " +
            "all videos, run GetAllVideos.cs. To upload video, see " +
            "http://adwords.google.com/support/aw/bin/answer.py?hl=en&answer=39454.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddAds();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201101.AdGroupAdService);

      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));
      long videoMediaId = long.Parse(_T("INSERT_VIDEO_MEDIA_ID_HERE"));

      // Create your text ad.
      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!";
      textAd.displayUrl = "www.example.com";
      textAd.url = "http://www.example.com";

      AdGroupAd textadGroupAd = new AdGroupAd();
      textadGroupAd.adGroupId = adGroupId;
      textadGroupAd.ad = textAd;

      AdGroupAdOperation textAdOperation = new AdGroupAdOperation();
      textAdOperation.@operator = Operator.ADD;
      textAdOperation.operand = textadGroupAd;

      // Create your image ad.
      ImageAd imageAd = new ImageAd();
      imageAd.name = "My Image Ad";
      imageAd.displayUrl = "www.example.com";
      imageAd.url = "http://www.example.com";

      imageAd.image = new Image();
      imageAd.image.data = MediaUtilities.GetAssetDataFromUrl("http://goo.gl/HJM3L");

      // Set the AdGroup Id.
      AdGroupAd imageAdGroupAd = new AdGroupAd();
      imageAdGroupAd.adGroupId = adGroupId;
      imageAdGroupAd.ad = imageAd;

      // Create the ADD Operation.
      AdGroupAdOperation imageAdOperation = new AdGroupAdOperation();
      imageAdOperation.@operator = Operator.ADD;
      imageAdOperation.operand = imageAdGroupAd;

      // Create your video ad.
      TemplateAd templateAd = new TemplateAd();
      templateAd.templateId = 9;

      TemplateElement templateElement = new TemplateElement();
      templateElement.uniqueName = "adData";
      templateAd.templateElements = new TemplateElement[] {templateElement};

      // Create the template field "startImage".
      TemplateElementField imageField = new TemplateElementField();
      imageField.type = TemplateElementFieldType.IMAGE;
      imageField.name = "startImage";

      Image image = new Image();

      image.type = MediaMediaType.IMAGE;
      image.name = "Starting Image";
      image.data = MediaUtilities.GetAssetDataFromUrl("http://goo.gl/HJM3L");
      imageField.fieldMedia = image;

      // Create the template field "displayUrlColor".
      TemplateElementField displayUrlColorField = new TemplateElementField();
      displayUrlColorField.type = TemplateElementFieldType.ENUM;
      displayUrlColorField.fieldText = "#ffffff";
      displayUrlColorField.name = "displayUrlColor";

      // Create the template field "video".
      TemplateElementField videoField = new TemplateElementField();
      videoField.type = TemplateElementFieldType.VIDEO;
      videoField.name = "video";

      Video video = new Video();
      video.mediaId = videoMediaId;
      video.type = MediaMediaType.VIDEO;

      videoField.fieldMedia = video;

      templateElement.fields = new TemplateElementField[] {imageField, displayUrlColorField,
          videoField};

      // Set the dimension, name, url and displayurl for video ad.
      templateAd.dimensions = new Dimensions();
      templateAd.dimensions.width = 300;
      templateAd.dimensions.height = 250;

      templateAd.name = "VideoAdTemplateExample";
      templateAd.url = "http://www.example.com";
      templateAd.displayUrl = "www.example.com";

      // Set the AdGroup Id.
      AdGroupAd videoAdGroupAd = new AdGroupAd();
      videoAdGroupAd.adGroupId = adGroupId;
      videoAdGroupAd.ad = templateAd;

      // Create the ADD Operation.
      AdGroupAdOperation videoAdOperation = new AdGroupAdOperation();
      videoAdOperation.@operator = Operator.ADD;
      videoAdOperation.operand = videoAdGroupAd;

      AdGroupAdReturnValue retVal = null;
      try {
        retVal = service.mutate(new AdGroupAdOperation[] {textAdOperation, imageAdOperation,
            videoAdOperation});

        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          foreach (AdGroupAd tempAdGroupAd in retVal.value) {
            Console.WriteLine("New ad with id = \"{0}\" and displayUrl = \"{1}\" was created.",
                tempAdGroupAd.ad.id, tempAdGroupAd.ad.displayUrl);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create Ad(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

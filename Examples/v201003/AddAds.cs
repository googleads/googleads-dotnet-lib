// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v201003;

using System;
using System.IO;
using System.Net;

namespace com.google.api.adwords.examples.v201003 {
  /// <summary>
  /// This example adds a text, image ad, and template (Click to Play Video)
  /// ad to a given ad group. To get ad_group, run GetAllAdGroups.cs. To get
  /// all videos, run GetAllVideos.cs. To upload video, see
  /// http://adwords.google.com/support/aw/bin/answer.py?hl=en&answer=39454.
  /// </summary>
  class AddAds : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example adds a text, image ad, and template (Click to Play Video) " +
            "ad to a given ad group. To get ad_group, run GetAllAdGroups.cs. To get " +
            "all videos, run GetAllVideos.cs. To upload video, see " +
            "http://adwords.google.com/support/aw/bin/answer.py?hl=en&answer=39454.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201003.AdGroupAdService);

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
      textadGroupAd.adGroupIdSpecified = true;
      textadGroupAd.ad = textAd;

      AdGroupAdOperation textAdOperation = new AdGroupAdOperation();
      textAdOperation.operatorSpecified = true;
      textAdOperation.@operator = Operator.ADD;
      textAdOperation.operand = textadGroupAd;

      // Create your image ad.
      ImageAd imageAd = new ImageAd();
      imageAd.name = "My Image Ad";
      imageAd.displayUrl = "www.example.com";
      imageAd.url = "http://www.example.com";

      // Load your image into data field.
      string imageUrl = "https://sandbox.google.com/sandboximages/image.jpg";

      WebRequest request = HttpWebRequest.Create(imageUrl);
      WebResponse response = request.GetResponse();

      MemoryStream memStream = new MemoryStream();
      using (Stream responseStream = response.GetResponseStream()) {
        byte[] strmBuffer = new byte[4096];

        int bytesRead = responseStream.Read(strmBuffer, 0, 4096);
        while (bytesRead != 0) {
          memStream.Write(strmBuffer, 0, bytesRead);
          bytesRead = responseStream.Read(strmBuffer, 0, 4096);
        }
      }

      imageAd.image = new Image();
      imageAd.image.data = memStream.ToArray();

      // Set the AdGroup Id.
      AdGroupAd imageAdGroupAd = new AdGroupAd();
      imageAdGroupAd.adGroupId = adGroupId;
      imageAdGroupAd.adGroupIdSpecified = true;
      imageAdGroupAd.ad = imageAd;

      // Create the ADD Operation.
      AdGroupAdOperation imageAdOperation = new AdGroupAdOperation();
      imageAdOperation.operatorSpecified = true;
      imageAdOperation.@operator = Operator.ADD;
      imageAdOperation.operand = imageAdGroupAd;

      // Create your video ad.
      TemplateAd templateAd = new TemplateAd();
      templateAd.templateIdSpecified = true;
      templateAd.templateId = 9;

      TemplateElement templateElement = new TemplateElement();
      templateElement.uniqueName = "adData";
      templateAd.templateElements = new TemplateElement[] {templateElement};

      // Create the template field "startImage".
      TemplateElementField imageField = new TemplateElementField();
      imageField.typeSpecified = true;
      imageField.type = TemplateElementFieldType.IMAGE;
      imageField.name = "startImage";

      Image image = new Image();

      image.mediaTypeDbSpecified = true;
      image.mediaTypeDb = MediaMediaType.IMAGE;
      image.name = "Starting Image";
      image.data = memStream.ToArray();
      imageField.fieldMedia = image;

      // Create the template field "displayUrlColor".
      TemplateElementField displayUrlColorField = new TemplateElementField();
      displayUrlColorField.typeSpecified = true;
      displayUrlColorField.type = TemplateElementFieldType.ENUM;
      displayUrlColorField.fieldText = "#ffffff";
      displayUrlColorField.name = "displayUrlColor";

      // Create the template field "video".
      TemplateElementField videoField = new TemplateElementField();
      videoField.typeSpecified = true;
      videoField.type = TemplateElementFieldType.VIDEO;
      videoField.name = "video";

      Video video = new Video();
      video.mediaIdSpecified = true;
      video.mediaId = videoMediaId;
      video.mediaTypeDbSpecified = true;
      video.mediaTypeDb = MediaMediaType.VIDEO;

      videoField.fieldMedia = video;

      templateElement.fields = new TemplateElementField[] {imageField, displayUrlColorField,
          videoField};

      // Set the dimension, name, url and displayurl for video ad.
      templateAd.dimensions = new Dimensions();
      templateAd.dimensions.widthSpecified = true;
      templateAd.dimensions.width = 300;
      templateAd.dimensions.heightSpecified = true;
      templateAd.dimensions.height = 250;

      templateAd.name = "VideoAdTemplateExample";
      templateAd.url = "http://www.example.com";
      templateAd.displayUrl = "www.example.com";

      // Set the AdGroup Id.
      AdGroupAd videoAdGroupAd = new AdGroupAd();
      videoAdGroupAd.adGroupId = adGroupId;
      videoAdGroupAd.adGroupIdSpecified = true;
      videoAdGroupAd.ad = templateAd;

      // Create the ADD Operation.
      AdGroupAdOperation videoAdOperation = new AdGroupAdOperation();
      videoAdOperation.operatorSpecified = true;
      videoAdOperation.@operator = Operator.ADD;
      videoAdOperation.operand = videoAdGroupAd;

      AdGroupAdReturnValue result = null;
      try {
        result = service.mutate(new AdGroupAdOperation[] {textAdOperation, imageAdOperation,
            videoAdOperation});

        if (result != null && result.value != null && result.value.Length > 0) {
          foreach (AdGroupAd tempAdGroupAd in result.value) {
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

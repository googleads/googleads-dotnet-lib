// Copyright 2009, Google Inc. All Rights Reserved.
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
using com.google.api.adwords.v200909;

using System;
using System.IO;
using System.Net;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This code sample shows how to add an image Ad and a text Ad to a given
  /// AdGroup. To create an AdGroup, see AddAdGroup sample.
  /// </summary>
  class AddAds : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This code sample shows how to add an image Ad and a text Ad to a given AdGroup.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v200909.AdGroupAdService);

      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));

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

      AdGroupAdReturnValue result = null;
      try {
        result = service.mutate(new AdGroupAdOperation[] {textAdOperation, imageAdOperation});

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

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

using System;
using System.IO;
using System.Net;

using com.google.api.adwords.lib;
using com.google.api.adwords.v200906.AdGroupAdService;

namespace com.google.api.adwords.samples.v200906 {
  /// <summary>
  /// This code sample creates a new image ad given an existing ad group.
  /// To create an ad group, you can run AddAdGroup.cs.
  /// </summary>
  class AddImageAd : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This sample shows how to add an image ad to an existing ad group.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(ApiServices.v200906.AdGroupAdService);

      // Create your image ad.
      ImageAd imageAd = new ImageAd();
      imageAd.name = "My Image Ad";
      imageAd.displayUrl = "http://www.example.com";
      imageAd.url = "http://www.example.com";

      // Load your image into data field.
      string imageUrl = "https://sandbox.google.com/sandboximages/image.jpg";

      WebRequest request = HttpWebRequest.Create(imageUrl);
      WebResponse response = request.GetResponse();

      Stream responseStream = response.GetResponseStream();

      MemoryStream memStream = new MemoryStream();
      byte[] strmBuffer = new byte[4096];

      int bytesRead = responseStream.Read(strmBuffer, 0, 4096);
      while (bytesRead != 0) {
        memStream.Write(strmBuffer, 0, bytesRead);
        bytesRead = responseStream.Read(strmBuffer, 0, 4096);
      }
      responseStream.Close();

      imageAd.image = new Image();
      imageAd.image.data = memStream.ToArray();

      // Set the AdGroup Id.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));
      adGroupAd.adGroupIdSpecified = true;
      adGroupAd.ad = imageAd;

      // Create the ADD Operation.
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.operatorSpecified = true;
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = adGroupAd;

      try {
        AdGroupAdReturnValue result =
            service.mutate(new AdGroupAdOperation[] {adGroupAdOperation});
        if (result.value != null && result.value.Length > 0) {
          foreach (AdGroupAd tempAdGroupAd in result.value) {
            Console.WriteLine("New image ad with displayUrl = \"{0}\" and id = {1} was created.",
                ((ImageAd) tempAdGroupAd.ad).displayUrl, tempAdGroupAd.ad.id);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create Ad(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

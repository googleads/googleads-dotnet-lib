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

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example shows how to create a Mobile Image Ad.
  /// </summary>
  class AddMobileImageAd : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to create a Mobile Image Ad.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddMobileImageAd();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201101.AdGroupAdService);

      MobileImageAd mobileImageId = new MobileImageAd();
      mobileImageId.url = "http://www.example.com";

      // Maximum length of display url is 20 characters.
      mobileImageId.displayUrl = "www.example.com";

      // Ads should be displayed on carriers supporting HTML and XHTML browsers.
      mobileImageId.markupLanguages =
          new MarkupLanguageType[] {MarkupLanguageType.HTML, MarkupLanguageType.XHTML};

      // Use all the available carriers. For possible values, see
      // http://code.google.com/apis/adwords/docs/developer/MobileImageAd.html
      mobileImageId.mobileCarriers = new string[] {"ALLCARRIERS"};

      mobileImageId.image = new Image();
      mobileImageId.image.data = LoadImage();

      // Set the AdGroup Id.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));
      adGroupAd.ad = mobileImageId;

      // Create the ADD Operation.
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = adGroupAd;

      try {
        AdGroupAdReturnValue retVal =
            service.mutate(new AdGroupAdOperation[] {adGroupAdOperation});
        if (retVal.value != null && retVal.value.Length > 0) {
          foreach (AdGroupAd tempAdGroupAd in retVal.value) {
            Console.WriteLine("New mobile image ad with displayUrl = \"{0}\" and id = {1}" +
                " was created.", ((MobileImageAd) tempAdGroupAd.ad).displayUrl,
                tempAdGroupAd.ad.id);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create Ad(s). Exception says \"{0}\"", ex.Message);
      }
    }

    /// <summary>
    /// Load an external image for creating the Mobile Image Ad.
    /// </summary>
    /// <returns>Image bytes as an array.</returns>
    private static byte[] LoadImage() {
      // Load your image into data field.
      string imageUrl = "http://adwords.google.com/select/images/samples/mobile300-50.gif";

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
      return memStream.ToArray();
    }
  }
}

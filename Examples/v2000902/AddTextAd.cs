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
using System.Collections;

using com.google.api.adwords.lib;
using com.google.api.adwords.v200902.AdGroupAdService;

namespace com.google.api.adwords.samples.v200902 {
  /// <summary>
  /// This code sample creates new text ads given an existing ad group. To
  /// create an ad group, you can run AddAdGroup.cs.
  /// </summary>
  class AddTextAd : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This code sample creates a new text ad given an existing ad group.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(ApiServices.v200902.AdGroupAdService);

      long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");

      // Create your good text ad.
      TextAd goodTextAd = new TextAd();
      goodTextAd.headline = "Luxury Cruise to Mars";
      goodTextAd.description1 = "Visit the Red Planet in style.";
      goodTextAd.description2 = "Low-gravity fun for everyone!";
      goodTextAd.displayUrl = "www.example.com";
      goodTextAd.url = "http://www.example.com";

      AdGroupAd goodAdGroupAd = new AdGroupAd();
      goodAdGroupAd.adGroupId = new AdGroupId();
      goodAdGroupAd.adGroupId.idSpecified = true;
      goodAdGroupAd.adGroupId.id = adGroupId;
      goodAdGroupAd.ad = goodTextAd;

      // Create your bad text ad.
      TextAd badTextAd = new TextAd();
      badTextAd.headline = "Luxury Cruise to MARS";
      badTextAd.description1 = "Visit the Red Planet in style.";
      badTextAd.description2 = "Low-gravity fun for everyone!!!";
      badTextAd.displayUrl = "www.example.com";
      badTextAd.url = "http://www.example.com";

      AdGroupAd badAdGroupAd = new AdGroupAd();
      badAdGroupAd.adGroupId = new AdGroupId();
      badAdGroupAd.adGroupId.idSpecified = true;
      badAdGroupAd.adGroupId.id = adGroupId;
      badAdGroupAd.ad = badTextAd;

      // Create the ADD operation.
      AdGroupAd[] adGroupAds = {goodAdGroupAd, badAdGroupAd};
      ArrayList operations = new ArrayList();

      foreach (AdGroupAd adGroupAd in adGroupAds) {
        AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
        adGroupAdOperation.operatorSpecified = true;
        adGroupAdOperation.@operator = Operator.ADD;
        adGroupAdOperation.operand = adGroupAd;
        operations.Add(adGroupAdOperation);
      }

      AdGroupAdReturnValue result = null;
      try {
        result = service.mutate((AdGroupAdOperation[])
            operations.ToArray(typeof(AdGroupAdOperation)));

        if (result.value != null && result.value.Length > 0) {
          foreach (AdGroupAd tempAdGroupAd in result.value) {
            Console.WriteLine("New text ad with headline = \"{0}\" and id = \"{1}\" was created.",
                ((TextAd)tempAdGroupAd.ad).headline, tempAdGroupAd.ad.id.id);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create Ad(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

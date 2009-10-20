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
using com.google.api.adwords.v200906;

using System;
using System.Collections;

namespace com.google.api.adwords.samples.v200906 {
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
          (AdGroupAdService) user.GetService(AdWordsService.v200906.AdGroupAdService);

      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));

      // Create your good text ad.
      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!";
      textAd.displayUrl = "www.example.com";
      textAd.url = "http://www.example.com";

      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroupId;
      adGroupAd.adGroupIdSpecified = true;
      adGroupAd.ad = textAd;

      // Create the ADD operation.
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;
      operation.operand = adGroupAd;

      AdGroupAdReturnValue result = null;
      try {
        result = service.mutate(new AdGroupAdOperation[] {operation});

        if (result.value != null && result.value.Length > 0) {
          foreach (AdGroupAd tempAdGroupAd in result.value) {
            Console.WriteLine("New text ad with headline = \"{0}\" and id = \"{1}\" was created.",
                ((TextAd)tempAdGroupAd.ad).headline, tempAdGroupAd.ad.id);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create Ad(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

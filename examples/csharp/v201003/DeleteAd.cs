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
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v201003 {
  /// <summary>
  /// This code example deletes an ad using the 'REMOVE' operator. To get ads,
  /// run GetAllAds.cs.
  ///
  /// Tags: AdGroupAdService.mutate
  /// </summary>
  class DeleteAd : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes an ad using the 'REMOVE' operator. To get ads, " +
            "run GetAllAds.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupAdService.
      AdGroupAdService adGroupAdService = (AdGroupAdService)user.GetService(
          AdWordsService.v201003.AdGroupAdService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));
      long adId = long.Parse(_T("INSERT_AD_ID_HERE"));

      // Create base class ad to avoid setting type specific fields.
      Ad ad = new Ad();
      ad.id = adId;
      ad.idSpecified = true;

      // Create ad group ad.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroupId;
      adGroupAd.adGroupIdSpecified = true;

      adGroupAd.ad = ad;

      // Create operations.
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.operand = adGroupAd;
      operation.operatorSpecified = true;
      operation.@operator = Operator.REMOVE;

      try {
        // Delete ad.
        AdGroupAdReturnValue result = adGroupAdService.mutate(
            new AdGroupAdOperation[] { operation });

        if (result != null && result.value != null && result.value.Length > 0) {
          foreach (AdGroupAd temp in result.value) {
            Console.WriteLine("Ad with id = \"{0}\" and type = \"{1}\" was deleted.",
                temp.ad.id, temp.ad.AdType);
          }
        } else {
          Console.WriteLine("No ads were deleted.");
        }

      } catch (Exception ex) {
        Console.WriteLine("Failed to delete ad. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

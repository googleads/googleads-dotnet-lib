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
using Google.Api.Ads.AdWords.v201008;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
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
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeleteAd();
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
      AdGroupAdService adGroupAdService = (AdGroupAdService) user.GetService(
          AdWordsService.v201008.AdGroupAdService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));
      long adId = long.Parse(_T("INSERT_AD_ID_HERE"));

      // Create base class ad to avoid setting type specific fields.
      Ad ad = new Ad();
      ad.id = adId;

      // Create ad group ad.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroupId;

      adGroupAd.ad = ad;

      // Create operations.
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.operand = adGroupAd;
      operation.@operator = Operator.REMOVE;

      try {
        // Delete ad.
        AdGroupAdReturnValue retVal = adGroupAdService.mutate(
            new AdGroupAdOperation[] {operation});

        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          foreach (AdGroupAd temp in retVal.value) {
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

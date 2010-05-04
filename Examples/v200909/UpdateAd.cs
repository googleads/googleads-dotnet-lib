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
using com.google.api.adwords.v200909;

using System;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This code example updates an ad's status given an existing ad group
  /// and ad.
  /// </summary>
  class UpdateAd : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates an ad's status given an existing ad group and ad.";
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
          (AdGroupAdService) user.GetService(AdWordsService.v200909.AdGroupAdService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));
      long adId = long.Parse(_T("INSERT_AD_ID_HERE"));
      AdGroupAdStatus status = (AdGroupAdStatus) Enum.Parse(typeof(AdGroupAdStatus),
          _T("INSERT_AD_GROUP_AD_STATUS_HERE"));

      // Update your Ad.
      AdGroupAd adGroupAd = new AdGroupAd();

      adGroupAd.statusSpecified = true;
      adGroupAd.status = status;

      adGroupAd.adGroupId = adGroupId;
      adGroupAd.adGroupIdSpecified = true;

      adGroupAd.ad = new Ad();
      adGroupAd.ad.id = adId;
      adGroupAd.ad.idSpecified = true;

      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.operatorSpecified = true;
      adGroupAdOperation.@operator = Operator.SET;
      adGroupAdOperation.operand = adGroupAd;
      try {
        AdGroupAdReturnValue result = service.mutate(new AdGroupAdOperation[]{adGroupAdOperation});
        if (result.value != null && result.value.Length > 0) {
          AdGroupAd tempAdGroupAd = result.value[0];
          Console.WriteLine("Status of ad with id \"{0}\" was set to \"{1}\"",
              tempAdGroupAd.ad.id, tempAdGroupAd.status);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update Ad. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example deletes an ad using the 'REMOVE' operator. To list ads,
  /// run GetTextAds.cs.
  ///
  /// Tags: AdGroupAdService.mutate
  /// </summary>
  public class DeleteAd : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new DeleteAd();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes an ad using the 'REMOVE' operator. To list ads, " +
            "run GetTextAds.cs.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID", "AD_ID"};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the AdGroupAdService.
      AdGroupAdService adGroupAdService = (AdGroupAdService) user.GetService(
          AdWordsService.v201109_1.AdGroupAdService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);
      long adId = long.Parse(parameters["AD_ID"]);

      // Since we do not need to update any ad-specific fields, it is enough to
      // create the base type.
      Ad ad = new Ad();
      ad.id = adId;

      // Create the ad group ad.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroupId;

      adGroupAd.ad = ad;

      // Create the operation.
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.operand = adGroupAd;
      operation.@operator = Operator.REMOVE;

      try {
        // Delete the ad.
        AdGroupAdReturnValue retVal = adGroupAdService.mutate(
            new AdGroupAdOperation[] {operation});

        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroupAd deletedAdGroupAd = retVal.value[0];
          writer.WriteLine("Ad with id = \"{0}\" and type = \"{1}\" was deleted.",
              deletedAdGroupAd.ad.id, deletedAdGroupAd.ad.AdType);
        } else {
          writer.WriteLine("No ads were deleted.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to delete ad.", ex);
      }
    }
  }
}

// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example shows how to use the validateOnly header to validate
  /// a text ad. No objects will be created, but exceptions will still be
  /// thrown.
  /// </summary>
  public class ValidateTextAd : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ValidateTextAd codeExample = new ValidateTextAd();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to use the validateOnly header to validate a " +
            "text ad. No objects will be created, but exceptions will still be thrown.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to which text ads are
    /// added.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupAdService.
      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v201603.AdGroupAdService);

      // Set the validateOnly headers.
      adGroupAdService.RequestHeader.validateOnly = true;

      // Create your text ad.
      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!!";
      textAd.displayUrl = "www.example.com";
      textAd.finalUrls = new string[] { "http://www.example.com" };

      AdGroupAd textAdGroupAd = new AdGroupAd();
      textAdGroupAd.adGroupId = adGroupId;
      textAdGroupAd.ad = textAd;

      AdGroupAdOperation textAdOperation = new AdGroupAdOperation();
      textAdOperation.@operator = Operator.ADD;
      textAdOperation.operand = textAdGroupAd;

      try {
        AdGroupAdReturnValue retVal = adGroupAdService.mutate(
            (new AdGroupAdOperation[] {textAdOperation}));
        // Since validation is ON, result will be null.
        Console.WriteLine("text ad validated successfully.");
      } catch (AdWordsApiException e) {
        // This block will be hit if there is a validation error from the server.
        Console.WriteLine("There were validation error(s) while adding text ad.");

        if (e.ApiException != null) {
          foreach (ApiError error in ((ApiException) e.ApiException).errors) {
            Console.WriteLine("  Error type is '{0}' and fieldPath is '{1}'.",
                error.ApiErrorType, error.fieldPath);
          }
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to validate text ad.", e);
      }
    }
  }
}

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
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example shows how to use the validateOnly header to validate
  /// a text ad. No objects will be created, but exceptions will still be
  /// thrown.
  ///
  /// Tags: AdGroupAdService.mutate
  /// </summary>
  class ValidateTextAd : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new ValidateTextAd();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID"};
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
      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v201109.AdGroupAdService);

      // Set the validateOnly headers.
      adGroupAdService.RequestHeader.validateOnly = true;

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);

      // Create your text ad.
      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!!";
      textAd.displayUrl = "www.example.com";
      textAd.url = "http://www.example.com";

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
        writer.WriteLine("text ad validated successfully.");
      } catch (AdWordsApiException ex) {
        // This block will be hit if there is a validation error from the server.
        writer.WriteLine("There were validation error(s) while adding text ad.");

        if (ex.ApiException != null) {
          foreach (ApiError error in ((ApiException) ex.ApiException).errors) {
            writer.WriteLine("  Error type is '{0}' and fieldPath is '{1}'.",
                error.ApiErrorType, error.fieldPath);
          }
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to validate text ad. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

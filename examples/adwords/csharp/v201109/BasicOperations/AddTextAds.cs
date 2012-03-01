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
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example adds text ads to a given ad group. To list ad groups,
  /// run GetAdGroups.cs.
  ///
  /// Tags: AdGroupAdService.mutate
  /// </summary>
  class AddTextAds : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new AddTextAds();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds text ads to a given ad group. To list ad groups, run " +
            "GetAdGroups.cs.";
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
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201109.AdGroupAdService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);

      // Create the text ad.
      TextAd textAd1 = new TextAd();
      textAd1.headline = "Luxury Cruise to Mars";
      textAd1.description1 = "Visit the Red Planet in style.";
      textAd1.description2 = "Low-gravity fun for everyone!";
      textAd1.displayUrl = "www.example.com";
      textAd1.url = "http://www.example.com";

      AdGroupAd textadGroupAd1 = new AdGroupAd();
      textadGroupAd1.adGroupId = adGroupId;
      textadGroupAd1.ad = textAd1;

      // Create the text ad.
      TextAd textAd2 = new TextAd();
      textAd2.headline = "Luxury Hotels in Mars";
      textAd2.description1 = "Enjoy your stay at Red Planet.";
      textAd2.description2 = "Low-gravity fun for everyone!";
      textAd2.displayUrl = "www.example.com";
      textAd2.url = "http://www.example.com";

      AdGroupAd textadGroupAd2 = new AdGroupAd();
      textadGroupAd2.adGroupId = adGroupId;
      textadGroupAd2.ad = textAd2;

      // Create the operations.
      AdGroupAdOperation textAdOperation1 = new AdGroupAdOperation();
      textAdOperation1.@operator = Operator.ADD;
      textAdOperation1.operand = textadGroupAd1;

      AdGroupAdOperation textAdOperation2 = new AdGroupAdOperation();
      textAdOperation2.@operator = Operator.ADD;
      textAdOperation2.operand = textadGroupAd2;

      AdGroupAdReturnValue retVal = null;

      try {
        // Create the ads.
        retVal = service.mutate(new AdGroupAdOperation[] {textAdOperation1, textAdOperation2});

        // Display the results.
        if (retVal != null && retVal.value != null) {
          // If you are adding multiple type of Ads, then you may need to check
          // for
          //
          // if (adGroupAd.ad is TextAd) { ... }
          //
          // to identify the ad type.
          foreach (AdGroupAd adGroupAd in retVal.value) {
            writer.WriteLine("New text ad with id = \"{0}\" and displayUrl = \"{1}\" was created.",
                adGroupAd.ad.id, adGroupAd.ad.displayUrl);
          }
        } else {
          writer.WriteLine("No text ads were created.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to create text ad(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

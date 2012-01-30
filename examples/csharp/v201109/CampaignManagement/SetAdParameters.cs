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
  /// This code example illustrates how to create a text ad with ad parameters.
  /// To add an ad group, run AddAdGroup.cs. To add a keyword, run
  /// run AddKeyword.cs.
  ///
  /// Tags: AdGroupAdService.mutate, AdParamService.mutate
  /// </summary>
  class SetAdParameters : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new SetAdParameters();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create a text ad with ad parameters." +
            " To add an ad group, run AddAdGroup.cs. To add a keyword, run AddKeyword.vb.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID", "CRITERION_ID"};
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
          AdWordsService.v201109.AdGroupAdService);

      // Get the AdParamService.
      AdParamService adParamService = (AdParamService) user.GetService(
          AdWordsService.v201109.AdParamService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);
      long criterionId = long.Parse(parameters["CRITERION_ID"]);

      // Create the text ad.
      TextAd textAd = new TextAd();
      textAd.url = "http://www.example.com";
      textAd.displayUrl = "example.com";
      textAd.headline = " Mars Cruises";
      textAd.description1 = "Low-gravity fun for {param1:cheap}.";
      textAd.description2 = "Only {param2:a few} seats left!";

      AdGroupAd adOperand = new AdGroupAd();
      adOperand.adGroupId = adGroupId;
      adOperand.status = AdGroupAdStatus.ENABLED;
      adOperand.ad = textAd;

      // Create the operation.
      AdGroupAdOperation adOperation = new AdGroupAdOperation();
      adOperation.operand = adOperand;
      adOperation.@operator = Operator.ADD;

      try {
        // Create the text ad.
        AdGroupAdReturnValue retVal = adGroupAdService.mutate(
            new AdGroupAdOperation[] {adOperation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          writer.WriteLine("Text ad with id ='{0}' was successfully added.",
              retVal.value[0].ad.id);
        } else {
          writer.WriteLine("No text ads were created.");
          return;
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to create text ads. Exception says \"{0}\"", ex.Message);
        return;
      }

      // Create the ad param for price.
      AdParam priceParam = new AdParam();
      priceParam.adGroupId = adGroupId;
      priceParam.criterionId = criterionId;
      priceParam.paramIndex = 1;
      priceParam.insertionText = "$100";

      // Create the ad param for seats.
      AdParam seatParam = new AdParam();
      seatParam.adGroupId = adGroupId;
      seatParam.criterionId = criterionId;
      seatParam.paramIndex = 2;
      seatParam.insertionText = "50";

      // Create the operations.
      AdParamOperation priceOperation = new AdParamOperation();
      priceOperation.@operator = Operator.SET;
      priceOperation.operand = priceParam;

      AdParamOperation seatOperation = new AdParamOperation();
      seatOperation.@operator = Operator.SET;
      seatOperation.operand = seatParam;

      try {
        // Set the ad parameters.
        AdParam [] newAdParams = adParamService.mutate(new AdParamOperation[]
            {priceOperation, seatOperation});

        // Display the results.
        if (newAdParams != null) {
          writer.WriteLine("Ad parameters were successfully updated.");
        } else {
          writer.WriteLine("No ad parameters were set.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to set ad parameter(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

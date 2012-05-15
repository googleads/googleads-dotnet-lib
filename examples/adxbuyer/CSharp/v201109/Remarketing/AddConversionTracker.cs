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
  /// This code example adds an AdWords conversion tracker.
  ///
  /// Tags: ConversionTrackerService.mutate
  /// </summary>
  public class AddConversionTracker : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new AddConversionTracker();
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
        return "This code example adds an AdWords conversion tracker.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {};
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
      // Get the ConversionTrackerService.
      ConversionTrackerService conversionTrackerService =
          (ConversionTrackerService)user.GetService(AdWordsService.v201109.
              ConversionTrackerService);

      // Create Adwords conversion tracker.
      AdWordsConversionTracker conversionTracker = new AdWordsConversionTracker();
      conversionTracker.name = "Earth to Mars Cruises Conversion #" +
          ExampleUtilities.GetTimeStamp();
      conversionTracker.category = ConversionTrackerCategory.DEFAULT;
      conversionTracker.markupLanguage = AdWordsConversionTrackerMarkupLanguage.HTML;
      conversionTracker.httpProtocol = AdWordsConversionTrackerHttpProtocol.HTTP;
      conversionTracker.textFormat = AdWordsConversionTrackerTextFormat.HIDDEN;

      // Set optional fields.
      conversionTracker.status = ConversionTrackerStatus.ENABLED;
      conversionTracker.viewthroughLookbackWindow = 15;
      conversionTracker.viewthroughConversionDeDupSearch = true;
      conversionTracker.isProductAdsChargeable = true;
      conversionTracker.productAdsChargeableConversionWindow = 15;
      conversionTracker.conversionPageLanguage = "en";
      conversionTracker.backgroundColor = "#0000FF";
      conversionTracker.userRevenueValue = "someJavascriptVariable";

      // Create the operation.
      ConversionTrackerOperation operation = new ConversionTrackerOperation();
      operation.@operator = Operator.ADD;
      operation.operand = conversionTracker;

      try {
        // Add conversion tracker.
        ConversionTrackerReturnValue retval = conversionTrackerService.mutate(
            new ConversionTrackerOperation[] {operation});

        // Display the results.
        if (retval != null && retval.value != null && retval.value.Length > 0) {
          ConversionTracker newConversionTracker = retval.value[0];
          writer.WriteLine("Conversion tracker with id '{0}', name '{1}', status '{2}', " +
              "category '{3}' was added.", newConversionTracker.id, newConversionTracker.name,
              newConversionTracker.status, newConversionTracker.category);
        } else {
          writer.WriteLine("No conversion trackers were added.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add conversion tracker.", ex);
      }
    }
  }
}

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
  /// This code example adds an AdWords conversion tracker.
  /// </summary>
  public class AddConversionTracker : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddConversionTracker codeExample = new AddConversionTracker();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
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
        return "This code example adds an AdWords conversion tracker.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the ConversionTrackerService.
      ConversionTrackerService conversionTrackerService =
          (ConversionTrackerService)user.GetService(AdWordsService.v201603.
              ConversionTrackerService);

      // Create Adwords conversion tracker.
      AdWordsConversionTracker conversionTracker = new AdWordsConversionTracker();
      conversionTracker.name = "Earth to Mars Cruises Conversion #" +
          ExampleUtilities.GetRandomString();
      conversionTracker.category = ConversionTrackerCategory.DEFAULT;
      conversionTracker.textFormat = AdWordsConversionTrackerTextFormat.HIDDEN;

      // Set optional fields.
      conversionTracker.status = ConversionTrackerStatus.ENABLED;
      conversionTracker.viewthroughLookbackWindow = 15;
      conversionTracker.conversionPageLanguage = "en";
      conversionTracker.backgroundColor = "#0000FF";
      conversionTracker.defaultRevenueValue = 23.41;
      conversionTracker.alwaysUseDefaultRevenueValue = true;

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
          Console.WriteLine("Conversion tracker with id '{0}', name '{1}', status '{2}', " +
              "category '{3}' was added.", newConversionTracker.id, newConversionTracker.name,
              newConversionTracker.status, newConversionTracker.category);
        } else {
          Console.WriteLine("No conversion trackers were added.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to add conversion tracker.", e);
      }
    }
  }
}

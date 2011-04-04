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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example adds an AdWords conversion tracker.
  ///
  /// Tags: ConversionTrackerService.mutate
  /// </summary>
  class AddConversionTracker : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds an AdWords conversion tracker.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddConversionTracker();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the ConversionTrackerService.
      ConversionTrackerService conversionTrackerService =
          (ConversionTrackerService)user.GetService(AdWordsService.v201101.
              ConversionTrackerService);

      // Create adwords conversion tracker.
      AdWordsConversionTracker conversionTracker = new AdWordsConversionTracker();
      conversionTracker.name = "Earth to Mars Cruises Conversion #" + GetTimeStamp();
      conversionTracker.category = ConversionTrackerCategory.DEFAULT;
      conversionTracker.markupLanguage = AdWordsConversionTrackerMarkupLanguage.HTML;
      conversionTracker.httpProtocol = AdWordsConversionTrackerHttpProtocol.HTTP;
      conversionTracker.textFormat = AdWordsConversionTrackerTextFormat.HIDDEN;

      // Create operation.
      ConversionTrackerOperation operation = new ConversionTrackerOperation();
      operation.@operator = Operator.ADD;
      operation.operand = conversionTracker;

      try {
        // Add conversion tracker.
        ConversionTrackerReturnValue retval = conversionTrackerService.mutate(
            new ConversionTrackerOperation[] {operation});

        // Display conversion tracker.
        if (retval != null && retval.value != null && retval.value.Length > 0) {
          foreach (ConversionTracker tempConversionTracker in retval.value) {
            Console.WriteLine("Conversion tracker with id '{0}', name '{1}', status '{2}', " +
                "category '{3}' was added.", tempConversionTracker.id, tempConversionTracker.name,
                tempConversionTracker.status, tempConversionTracker.category);
          }
        } else {
          Console.WriteLine("No conversion trackers were added.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add conversion tracker. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

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
  /// This code example updates a conversion tracker by setting its status to
  /// 'DISABLED'. To get conversion trackers, run GetAllConversionTrackers.cs.
  ///
  /// Tags: ConversionTrackerService.mutate
  /// </summary>
  class UpdateConversionTracker : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a conversion tracker by setting its status to " +
            "'DISABLED'. To get conversion trackers, run GetAllConversionTrackers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateConversionTracker();
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

      long conversionTrackerId = long.Parse(_T("INSERT_CONVERSION_TRACKER_ID_HERE"));

      // Create conversion tracker with updated status.
      AdWordsConversionTracker conversionTracker = new AdWordsConversionTracker();
      conversionTracker.id = conversionTrackerId;
      conversionTracker.status = ConversionTrackerStatus.DISABLED;

      // Create operations.
      ConversionTrackerOperation operation = new ConversionTrackerOperation();
      operation.operand = conversionTracker;
      operation.@operator = Operator.SET;

      try {
        // Update conversion.
        ConversionTrackerReturnValue retval = conversionTrackerService.mutate(
            new ConversionTrackerOperation[] {operation});

        // Display conversions.
        if (retval != null && retval.value != null && retval.value.Length > 0) {
          foreach (ConversionTracker tempConversionTracker in retval.value) {
            Console.WriteLine("Conversion tracker with id '{0}' , name '{1}', status '{2}', " +
                "category '{3}' was disabled.", tempConversionTracker.id,
                tempConversionTracker.name, tempConversionTracker.status,
                tempConversionTracker.category);
          }
        } else {
          Console.WriteLine("No conversion trackers were disabled.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update conversion tracker. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

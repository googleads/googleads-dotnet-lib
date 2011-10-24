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
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example gets all conversion trackers in the account. To add
  /// conversion trackers, run AddConversionTracker.cs.
  ///
  /// Tags: ConversionTrackerService.get
  /// </summary>
  class GetAllConversionTrackers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all conversion trackers in the account. To add " +
            "conversion trackers, run AddConversionTracker.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllConversionTrackers();
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
          (ConversionTrackerService)user.GetService(AdWordsService.v201109.
              ConversionTrackerService);

      // Create selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name", "Status", "Category"};

      OrderBy orderBy = new OrderBy();
      orderBy.field = "Name";
      orderBy.sortOrder = SortOrder.ASCENDING;
      selector.ordering = new OrderBy[] {orderBy};

      try {
        // Get all conversions.
        ConversionTrackerPage page = conversionTrackerService.get(selector);

        // Display conversions.
        if (page != null && page.entries != null && page.entries.Length > 0) {
          foreach (AdWordsConversionTracker conversionTracker in page.entries) {
            Console.WriteLine("Conversion tracker with id '{0}', name '{1}', status '{2}', " +
                "category '{3}' was found. With code snippet: \n{4}\n", conversionTracker.id,
                conversionTracker.name, conversionTracker.status, conversionTracker.category,
                conversionTracker.snippet);
          }
        } else {
          Console.WriteLine("No conversion trackers were found.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get all conversion trackers. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

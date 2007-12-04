//
// Copyright (C) 2007 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using com.google.api.adwords.lib;
using com.google.api.adwords.v10;

using System;
using System.Text;

namespace com.google.api.adwords.examples
{
  // Estimates traffic for a given keyword.
  class KeywordEstimateDemo
  {
    public static void run()
    {
      // Create a user (reads headers from App.config file).
      AdWordsUser user = new AdWordsUser();
      user.useSandbox();  // use sandbox

      // Get the service.
      TrafficEstimatorService service =
          (TrafficEstimatorService) user.getService("TrafficEstimatorService");

      // Set the attributes of the keywords to be estimated.
      KeywordRequest myKeyword = new KeywordRequest();
      myKeyword.text = "flowers";
      myKeyword.maxCpc = 50000;
      myKeyword.maxCpcSpecified = true;
      myKeyword.type = KeywordType.Broad;
      myKeyword.typeSpecified = true;

      // To estimate more keywords, create more KeywordRequest objects
      // and add them to the list of keyword to estimate.

      // Estimate traffic for given keywords.
      KeywordEstimate[] estimates =
          service.estimateKeywordList(new KeywordRequest[] {myKeyword});

      for (int i = 0; i < estimates.Length; i ++)
      {
        KeywordEstimate estimate = estimates[i];

        Console.WriteLine(
            "Clicks per day between {0} and {1}"
            + "\nCost per click between {0} and {1}"
            + "\nAverage position between {0} and {1}",
            estimate.lowerClicksPerDay, estimate.upperClicksPerDay,
            estimate.lowerCpc, estimate.upperCpc, estimate.lowerAvgPosition,
            estimate.upperAvgPosition);
      }

      Console.ReadLine();
    }
  }
}

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

using Google.Api.Ads.AdWords.Examples;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v13;

using System;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v13 {
  /// <summary>
  /// This code example estimates traffic for a given keyword.
  /// </summary>
  class KeywordEstimateDemo : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example estimates traffic for a given keyword.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      TrafficEstimatorService service =
          (TrafficEstimatorService) user.GetService(AdWordsService.v13.TrafficEstimatorService);

      // Set the attributes of the keywords to be estimated.
      KeywordRequest myKeyword = new KeywordRequest();
      myKeyword.text = "flowers";
      myKeyword.maxCpc = 50000;
      myKeyword.type = KeywordType.Broad;

      // To estimate more keywords, create more KeywordRequest objects
      // and add them to the list of keyword to estimate.
      KeywordRequest[] keyReqs = new KeywordRequest[] {myKeyword};

      // Estimate traffic for given keywords.
      KeywordEstimate[] estimates = service.estimateKeywordList(keyReqs);

      if (estimates != null) {
        for (int i = 0; i < estimates.Length; i++) {
          KeywordEstimate estimate = estimates[i];

          Console.WriteLine(
              "Keyword estimates for: " + keyReqs[i].text +
              "\nClicks per day between {0} and {1}" +
              "\nCost per click between {0} and {1}" +
              "\nAverage position between {0} and {1}",
              estimate.lowerClicksPerDay, estimate.upperClicksPerDay,
              estimate.lowerCpc, estimate.upperCpc, estimate.lowerAvgPosition,
              estimate.upperAvgPosition);
        }
      } else {
        Console.WriteLine("No traffic estimates are available for given keyword(s)");
      }
    }
  }
}

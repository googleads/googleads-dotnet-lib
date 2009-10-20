// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

using System;
using System.Text;

namespace com.google.api.adwords.samples.v13 {
  /// <summary>
  /// Checks a batch of keywords to see whether they will get any traffic.
  /// </summary>
  class CheckKeywordTrafficDemo : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Checks a batch of keywords to see whether they will get any traffic.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      TrafficEstimatorService service =
          (TrafficEstimatorService) user.GetService(AdWordsService.v13.TrafficEstimatorService);

      KeywordTrafficRequest trafficRequest1 = new KeywordTrafficRequest();
      trafficRequest1.keywordText = "AdWords API";
      trafficRequest1.keywordType = KeywordType.Phrase;
      trafficRequest1.language = "en_US";

      KeywordTrafficRequest trafficRequest2 = new KeywordTrafficRequest();
      trafficRequest2.keywordText = "Google Desktop";
      trafficRequest2.keywordType = KeywordType.Broad;
      trafficRequest2.language = "fr";

      KeywordTrafficRequest[] trafficRequests = {trafficRequest1, trafficRequest2};

      // Check keyword traffic estimates.
      KeywordTraffic[] estimates = service.checkKeywordTraffic(trafficRequests);

      if (estimates != null) {
        Console.WriteLine("{0, -20}{1, -10}", "Keyword", "Traffic");
        Console.WriteLine("---------------------------------");

        for (int i = 0; i < estimates.Length; i++) {
          Console.WriteLine("{0, -20}{1, -10}", trafficRequests[i].keywordText, estimates[i]);
        }
      } else {
        Console.WriteLine("Given keyword(s) not expected to get any traffic.");
      }
    }
  }
}

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
using Google.Api.Ads.AdWords.v201008;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
  /// <summary>
  /// This code example gets keyword traffic estimates.
  ///
  /// Tags: TrafficEstimatorService.get
  /// </summary>
  class GetTrafficEstimates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets keyword traffic estimates.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetTrafficEstimates();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the TrafficEstimatorService.
      TrafficEstimatorService trafficEstimatorService = (TrafficEstimatorService) user.GetService(
          AdWordsService.v201008.TrafficEstimatorService);

      // Create keywords. Up to 2000 keywords can be passed in a single request.
      Keyword keyword1 = new Keyword();
      keyword1.text = "mars cruise";
      keyword1.matchType = KeywordMatchType.BROAD;

      Keyword keyword2 = new Keyword();
      keyword2.text = "cheap cruise";
      keyword2.matchType = KeywordMatchType.PHRASE;

      Keyword keyword3 = new Keyword();
      keyword3.text = "cruise";
      keyword3.matchType = KeywordMatchType.EXACT;

      Keyword[] keywords = new Keyword[] {keyword1, keyword2, keyword3};

      // Create a keyword estimate request for each keyword.
      List<KeywordEstimateRequest> keywordEstimateRequests = new List<KeywordEstimateRequest>();

      foreach (Keyword keyword in keywords) {
        KeywordEstimateRequest keywordEstimateRequest = new KeywordEstimateRequest();
        keywordEstimateRequest.keyword = keyword;
        keywordEstimateRequests.Add(keywordEstimateRequest);
      }

      // Create ad group estimate requests.
      AdGroupEstimateRequest adGroupEstimateRequest = new AdGroupEstimateRequest();
      adGroupEstimateRequest.keywordEstimateRequests = keywordEstimateRequests.ToArray();
      adGroupEstimateRequest.maxCpc = new Money();
      adGroupEstimateRequest.maxCpc.microAmount = 1000000;

      // Create campaign estimate requests.
      CampaignEstimateRequest campaignEstimateRequest = new CampaignEstimateRequest();
      campaignEstimateRequest.adGroupEstimateRequests = new AdGroupEstimateRequest[] {
          adGroupEstimateRequest};

      CountryTarget countryTarget = new CountryTarget();
      countryTarget.countryCode = "US";

      LanguageTarget languageTarget = new LanguageTarget();
      languageTarget.languageCode = "en";

      campaignEstimateRequest.targets = new Target[] {countryTarget, languageTarget};

      // Create selector.
      TrafficEstimatorSelector selector = new TrafficEstimatorSelector();
      selector.campaignEstimateRequests = new CampaignEstimateRequest[] {campaignEstimateRequest};

      try {
        // Get traffic estimates.
        TrafficEstimatorResult result = trafficEstimatorService.get(selector);

        // Display traffic estimates.
        if (result != null && result.campaignEstimates != null &&
            result.campaignEstimates.Length > 0) {
          CampaignEstimate campaignEstimate = result.campaignEstimates[0];
          if (campaignEstimate.adGroupEstimates != null &&
              campaignEstimate.adGroupEstimates.Length > 0) {
            AdGroupEstimate adGroupEstimate = campaignEstimate.adGroupEstimates[0];
            if (adGroupEstimate.keywordEstimates != null) {
              for (int i = 0; i < adGroupEstimate.keywordEstimates.Length; i++) {
                Keyword keyword = keywordEstimateRequests[i].keyword;
                KeywordEstimate keywordEstimate = adGroupEstimate.keywordEstimates[i];

                // Find the mean of the min and max values.
                long meanAverageCpc = (keywordEstimate.min.averageCpc.microAmount
                    + keywordEstimate.max.averageCpc.microAmount) / 2;
                double meanAveragePosition = (keywordEstimate.min.averagePosition
                    + keywordEstimate.max.averagePosition) / 2;
                long meanClicks = (keywordEstimate.min.clicks
                   + keywordEstimate.max.clicks) / 2;
                long meanTotalCost = (keywordEstimate.min.totalCost.microAmount
                   + keywordEstimate.max.totalCost.microAmount) / 2;

               Console.WriteLine("Results for the keyword with text = '{0}' and match type = " +
                    "'{1}':", keyword.text, keyword.matchType);
               Console.WriteLine("  Estimated average CPC: {0}", meanAverageCpc);
               Console.WriteLine("  Estimated ad position: {0:0.00}", meanAveragePosition);
               Console.WriteLine("  Estimated daily clicks: {0}", meanClicks);
               Console.WriteLine("  Estimated daily cost: {0}\n", meanTotalCost);
              }
            }
          }
        } else {
          Console.WriteLine("No traffic estimates were returned.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve traffic estimates. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

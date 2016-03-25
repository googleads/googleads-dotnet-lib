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
  /// This code example gets keyword traffic estimates.
  /// </summary>
  public class EstimateKeywordTraffic : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      EstimateKeywordTraffic codeExample = new EstimateKeywordTraffic();
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
        return "This code example gets keyword traffic estimates.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the TrafficEstimatorService.
      TrafficEstimatorService trafficEstimatorService = (TrafficEstimatorService) user.GetService(
          AdWordsService.v201603.TrafficEstimatorService);

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

      Keyword[] keywords = new Keyword[] { keyword1, keyword2, keyword3 };

      // Create a keyword estimate request for each keyword.
      List<KeywordEstimateRequest> keywordEstimateRequests = new List<KeywordEstimateRequest>();

      foreach (Keyword keyword in keywords) {
        KeywordEstimateRequest keywordEstimateRequest = new KeywordEstimateRequest();
        keywordEstimateRequest.keyword = keyword;
        keywordEstimateRequests.Add(keywordEstimateRequest);
      }

      // Create negative keywords.
      Keyword negativeKeyword1 = new Keyword();
      negativeKeyword1.text = "moon walk";
      negativeKeyword1.matchType = KeywordMatchType.BROAD;

      KeywordEstimateRequest negativeKeywordEstimateRequest = new KeywordEstimateRequest();
      negativeKeywordEstimateRequest.keyword = negativeKeyword1;
      negativeKeywordEstimateRequest.isNegative = true;
      keywordEstimateRequests.Add(negativeKeywordEstimateRequest);

      // Create ad group estimate requests.
      AdGroupEstimateRequest adGroupEstimateRequest = new AdGroupEstimateRequest();
      adGroupEstimateRequest.keywordEstimateRequests = keywordEstimateRequests.ToArray();
      adGroupEstimateRequest.maxCpc = new Money();
      adGroupEstimateRequest.maxCpc.microAmount = 1000000;

      // Create campaign estimate requests.
      CampaignEstimateRequest campaignEstimateRequest = new CampaignEstimateRequest();
      campaignEstimateRequest.adGroupEstimateRequests = new AdGroupEstimateRequest[] {
          adGroupEstimateRequest};

      // See http://code.google.com/apis/adwords/docs/appendix/countrycodes.html
      // for a detailed list of country codes.
      Location countryCriterion = new Location();
      countryCriterion.id = 2840; //US

      // See http://code.google.com/apis/adwords/docs/appendix/languagecodes.html
      // for a detailed list of language codes.
      Language languageCriterion = new Language();
      languageCriterion.id = 1000; //en

      campaignEstimateRequest.criteria = new Criterion[] { countryCriterion, languageCriterion };

      // Create the selector.
      TrafficEstimatorSelector selector = new TrafficEstimatorSelector();
      selector.campaignEstimateRequests = new CampaignEstimateRequest[] { campaignEstimateRequest };

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

                if (keywordEstimateRequests[i].isNegative) {
                  continue;
                }

                // Find the mean of the min and max values.
                long meanAverageCpc = 0;
                double meanAveragePosition = 0;
                float meanClicks = 0;
                long meanTotalCost = 0;

                if (keywordEstimate.min != null && keywordEstimate.max != null) {
                  if (keywordEstimate.min.averageCpc != null &&
                      keywordEstimate.max.averageCpc != null) {
                    meanAverageCpc = (keywordEstimate.min.averageCpc.microAmount +
                        keywordEstimate.max.averageCpc.microAmount) / 2;
                  }

                  meanAveragePosition = (keywordEstimate.min.averagePosition +
                      keywordEstimate.max.averagePosition) / 2;
                  meanClicks = (keywordEstimate.min.clicksPerDay +
                      keywordEstimate.max.clicksPerDay) / 2;
                  if (keywordEstimate.min.totalCost != null &&
                      keywordEstimate.max.totalCost != null) {
                    meanTotalCost = (keywordEstimate.min.totalCost.microAmount +
                        keywordEstimate.max.totalCost.microAmount) / 2;
                  }
                }

                Console.WriteLine("Results for the keyword with text = '{0}' and match type = " +
                     "'{1}':", keyword.text, keyword.matchType);
                Console.WriteLine("  Estimated average CPC: {0}", meanAverageCpc);
                Console.WriteLine("  Estimated ad position: {0:0.00}", meanAveragePosition);
                Console.WriteLine("  Estimated daily clicks: {0}", meanClicks);
                Console.WriteLine("  Estimated daily cost: {0}", meanTotalCost);
              }
            }
          }
        } else {
          Console.WriteLine("No traffic estimates were returned.\n");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve traffic estimates.", e);
      }
    }
  }
}

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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example retrieves all text ads given an existing ad group.
  /// To add text ads to an existing ad group, run AddTextAds.cs.
  /// </summary>
  public class GetTextAds : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetTextAds codeExample = new GetTextAds();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
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
        return "This code example retrieves all text ads given an existing ad group. To add " +
            "text ads to an existing ad group, run AddTextAds.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group from which text ads are
    /// retrieved.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201603.AdGroupAdService);

      // Create a selector.
      Selector selector = new Selector() {
        fields = new string[] {
          TextAd.Fields.Id, AdGroupAd.Fields.Status, TextAd.Fields.Headline,
          TextAd.Fields.Description1, TextAd.Fields.Description2, TextAd.Fields.DisplayUrl
        },
        ordering = new OrderBy[] { OrderBy.Asc(TextAd.Fields.Id) },
        predicates = new Predicate[] {
          // Restrict the fetch to only the selected ad group id.
          Predicate.Equals(AdGroupAd.Fields.AdGroupId, adGroupId),

          // Retrieve only text ads.
          Predicate.Equals("AdType", "TEXT_AD"),

          // By default disabled ads aren't returned by the selector. To return
          // them include the DISABLED status in the statuses field.
          Predicate.In(AdGroupAd.Fields.Status, new string[] {
            AdGroupAdStatus.ENABLED.ToString(),
            AdGroupAdStatus.PAUSED.ToString(),
            AdGroupAdStatus.DISABLED.ToString()
          })
        },
        paging = Paging.Default
      };

      AdGroupAdPage page = new AdGroupAdPage();

      try {
        do {
          // Get the text ads.
          page = service.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;

            foreach (AdGroupAd adGroupAd in page.entries) {
              TextAd textAd = (TextAd) adGroupAd.ad;
              Console.WriteLine("{0}) Ad id is {1} and status is {2}", i + 1, textAd.id,
                  adGroupAd.status);
              Console.WriteLine("  {0}\n  {1}\n  {2}\n  {3}", textAd.headline,
                  textAd.description1, textAd.description2, textAd.displayUrl);
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of text ads found: {0}", page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to get text ads", e);
      }
    }
  }
}

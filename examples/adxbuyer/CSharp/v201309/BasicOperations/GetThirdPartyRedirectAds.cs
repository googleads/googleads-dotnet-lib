// Copyright 2013, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201309;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201309 {
  /// <summary>
  /// This code example retrieves all third party redirect ads given an existing
  /// ad group. To add third party redirect ads to an existing ad group, run
  /// AddThirdPartyRedirectAd.cs.
  ///
  /// Tags: AdGroupAdService.get
  /// </summary>
  public class GetThirdPartyRedirectAds : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetThirdPartyRedirectAds codeExample = new GetThirdPartyRedirectAds();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves all third party redirect ads given an existing ad " +
            "group. To add third party redirect ads to an existing ad group, run " +
            "AddThirdPartyRedirectAd.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group from which third party
    /// redirect ads are retrieved.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201309.AdGroupAdService);

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Status", "Url", "DisplayUrl", "RichMediaAdSnippet"};

      // Set the sort order.
      OrderBy orderBy = new OrderBy();
      orderBy.field = "Id";
      orderBy.sortOrder = SortOrder.ASCENDING;
      selector.ordering = new OrderBy[] {orderBy};

      // Restrict the fetch to only the selected ad group id.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.EQUALS;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      // Retrieve only third party redirect ads.
      Predicate typePredicate = new Predicate();
      typePredicate.field = "AdType";
      typePredicate.@operator = PredicateOperator.EQUALS;
      typePredicate.values = new string[] {"THIRD_PARTY_REDIRECT_AD"};

      // By default disabled ads aren't returned by the selector. To return
      // them include the DISABLED status in the statuses field.
      Predicate statusPredicate = new Predicate();
      statusPredicate.field = "Status";
      statusPredicate.@operator = PredicateOperator.IN;

      statusPredicate.values = new string[] {AdGroupAdStatus.ENABLED.ToString(),
          AdGroupAdStatus.PAUSED.ToString(), AdGroupAdStatus.DISABLED.ToString()};

      selector.predicates = new Predicate[] {adGroupPredicate, statusPredicate, typePredicate};

      // Select the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      AdGroupAdPage page = new AdGroupAdPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the third party redirect ads.
          page = service.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;

            foreach (AdGroupAd adGroupAd in page.entries) {
              ThirdPartyRedirectAd thirdPartyRedirectAd = (ThirdPartyRedirectAd) adGroupAd.ad;
              Console.WriteLine("{0}) Ad id is {1} and status is {2}", i, thirdPartyRedirectAd.id,
                  adGroupAd.status);
              Console.WriteLine("  Url: {0}\n  Display Url: {1}\n  Snippet:{2}",
                  thirdPartyRedirectAd.url, thirdPartyRedirectAd.displayUrl,
                  thirdPartyRedirectAd.snippet);
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of third party redirect ads found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get third party redirect ad(s).", ex);
      }
    }
  }
}

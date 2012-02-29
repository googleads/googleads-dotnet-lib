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
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example retrieves all third party redirect ads given an existing
  /// ad group. To add third party redirect ads to an existing ad group, run
  /// AddThirdPartyRedirectAd.cs.
  ///
  /// Tags: AdGroupAdService.get
  /// </summary>
  class GetThirdPartyRedirectAds : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetThirdPartyRedirectAds();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID"};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201109.AdGroupAdService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);

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
              writer.WriteLine("{0}) Ad id is {1} and status is {2}", i, thirdPartyRedirectAd.id,
                  adGroupAd.status);
              writer.WriteLine("  Url: {0}\n  Display Url: {1}\n  Snippet:{2}",
                  thirdPartyRedirectAd.url, thirdPartyRedirectAd.displayUrl,
                  thirdPartyRedirectAd.snippet);
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        writer.WriteLine("Number of third party redirect ads found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        writer.WriteLine("Failed to get third party redirect ad(s). Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

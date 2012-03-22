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
  /// This code example illustrates how to retrieve all the ad groups for a
  /// campaign. To create an ad group, run AddAdGroup.cs.
  ///
  /// Tags: AdGroupService.get
  /// </summary>
  public class GetAdGroups : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetAdGroups();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
        return "This code example illustrates how to retrieve all the ad groups for a " +
            "campaign. To create an ad group, run AddAdGroup.cs.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"CAMPAIGN_ID"};
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
      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201109.AdGroupService);

      long campaignId = long.Parse(parameters["CAMPAIGN_ID"]);

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name"};

      // Create the filters.
      Predicate predicate = new Predicate();
      predicate.field = "CampaignId";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] {campaignId.ToString()};
      selector.predicates = new Predicate[] {predicate};

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      AdGroupPage page = new AdGroupPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the ad groups.
          page = adGroupService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (AdGroup adGroup in page.entries) {
              writer.WriteLine("{0}) Ad group name is '{1}' and id is {2}.", i + 1, adGroup.name,
                  adGroup.id);
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        writer.WriteLine("Number of ad groups found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve ad groups.", ex);
      }
    }
  }
}

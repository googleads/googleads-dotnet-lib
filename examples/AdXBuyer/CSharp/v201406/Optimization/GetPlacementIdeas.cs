// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201406;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {
  /// <summary>
  /// This code example retrieves urls that have content keywords related to a
  /// given website.
  ///
  /// Tags: TargetingIdeaService.get
  /// </summary>
  public class GetPlacementIdeas : ExampleBase {
     /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetPlacementIdeas codeExample = new GetPlacementIdeas();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
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
        return "This code example retrieves urls that have content keywords related to a" +
            " given website.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the TargetingIdeaService.
      TargetingIdeaService targetingIdeaService =
          (TargetingIdeaService) user.GetService(AdWordsService.v201406.TargetingIdeaService);

      // Create seed url.
      string url = "mars.google.com";

      // Create the selector.
      TargetingIdeaSelector selector = new TargetingIdeaSelector();
      selector.requestType = RequestType.IDEAS;
      selector.ideaType = IdeaType.PLACEMENT;
      selector.requestedAttributeTypes = new AttributeType[] {AttributeType.CRITERION,
          AttributeType.PLACEMENT_TYPE};

      // Create related to url search parameter.
      RelatedToUrlSearchParameter relatedToUrlSearchParameter = new RelatedToUrlSearchParameter();
      relatedToUrlSearchParameter.urls = new string[] {url};
      relatedToUrlSearchParameter.includeSubUrls = false;
      selector.searchParameters = new SearchParameter[] {relatedToUrlSearchParameter};

      // Set selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      TargetingIdeaPage page = new TargetingIdeaPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get placement ideas.
          page = targetingIdeaService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;

            foreach (TargetingIdea idea in page.entries) {
              foreach (Type_AttributeMapEntry entry in idea.data) {
                if (entry.key == AttributeType.CRITERION) {
                  CriterionAttribute placementAttribute = entry.value as CriterionAttribute;
                  Placement placement = (Placement) placementAttribute.value;
                  Console.WriteLine("Related placement urls were found at '{0}'.",
                      (placementAttribute.value as Placement).url);
                }
              }
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of related placements found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve related placements.", ex);
      }
    }
  }
}

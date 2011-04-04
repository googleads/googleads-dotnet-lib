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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example retrieves keywords that are related to a given keyword.
  ///
  /// Tags: TargetingIdeaService.get
  /// </summary>
  class GetRelatedKeywords : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves keywords that are related to a given keyword.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetRelatedKeywords();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the TargetingIdeaService.
      TargetingIdeaService targetingIdeaService =
          (TargetingIdeaService) user.GetService(AdWordsService.v201101.TargetingIdeaService);

      string keywordText = "mars cruise";

      // Create seed keyword.
      Keyword keyword = new Keyword();
      keyword.text = keywordText;
      keyword.matchType = KeywordMatchType.BROAD;

      // Create selector.
      TargetingIdeaSelector selector = new TargetingIdeaSelector();
      selector.requestType = RequestType.IDEAS;
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestedAttributeTypes = new AttributeType[] {AttributeType.CRITERION,
          AttributeType.AVERAGE_TARGETED_MONTHLY_SEARCHES};

      // Set selector paging (required for targeting idea service).
      Paging paging = new Paging();
      paging.startIndex = 0;
      paging.numberResults = 10;
      selector.paging = paging;

      // Create related to keyword search parameter.
      RelatedToKeywordSearchParameter relatedToKeywordSearchParameter =
          new RelatedToKeywordSearchParameter();
      relatedToKeywordSearchParameter.keywords = new Keyword[] {keyword};

      // Create keyword match type search parameter to ensure unique results.
      KeywordMatchTypeSearchParameter keywordMatchTypeSearchParameter =
          new KeywordMatchTypeSearchParameter();
      keywordMatchTypeSearchParameter.keywordMatchTypes = new KeywordMatchType[] {
          KeywordMatchType.BROAD};

      selector.searchParameters =
          new SearchParameter[] {relatedToKeywordSearchParameter, keywordMatchTypeSearchParameter};

      try {
        TargetingIdeaPage page = targetingIdeaService.get(selector);

        if (page != null && page.entries != null) {
          Console.WriteLine("There are a total of {0} keywords related to '{1}'. The first {2}" +
            " entries are displayed below: \n", page.totalNumEntries, keywordText,
            page.entries.Length);

          foreach(TargetingIdea idea in page.entries) {
            foreach (Type_AttributeMapEntry entry in idea.data) {
              if (entry.key == AttributeType.CRITERION) {
                CriterionAttribute kwdAttribute = entry.value as CriterionAttribute;
                Console.WriteLine("Related keyword with text = '{0}' and match type = '{1}'" +
                  " was found.", (kwdAttribute.value as Keyword).text,
                  (kwdAttribute.value as Keyword).matchType);
              }
            }
          }
        } else {
          Console.WriteLine("No related keywords were found for your keyword.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve related keywords. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

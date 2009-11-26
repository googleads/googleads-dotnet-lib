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
using com.google.api.adwords.v200909;

using System;
using System.IO;
using System.Net;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This demo fetches targeting ideas for excluded keyword search parameters.
  /// </summary>
  class GetTargetingIdeas : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This demo fetches targeting ideas for excluded keyword search parameters.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      TargetingIdeaService service =
          (TargetingIdeaService) user.GetService(AdWordsService.v200909.TargetingIdeaService);
      TargetingIdeaSelector selector = new TargetingIdeaSelector();

      // Get keyword suggestion on broad exclusion on "sony player".
      ExcludedKeywordSearchParameter excludeKeyword = new ExcludedKeywordSearchParameter();
      Keyword broadKeyword = new Keyword();
      broadKeyword.matchType = KeywordMatchType.BROAD;
      broadKeyword.matchTypeSpecified = true;
      broadKeyword.text = "sony player";
      excludeKeyword.keywords = new Keyword[] {broadKeyword};

      // Get keyword suggestion on broad exclusion on "sony player".
      KeywordMatchTypeSearchParameter matchKeyword = new KeywordMatchTypeSearchParameter();
      matchKeyword.keywordMatchTypes =
          new KeywordMatchType[] {KeywordMatchType.BROAD, KeywordMatchType.EXACT};

      // Get keyword suggestions for exact match on "dvd player".
      RelatedToKeywordSearchParameter relatedKeyword = new RelatedToKeywordSearchParameter();
      Keyword exactKeyword = new Keyword();
      exactKeyword.matchType = KeywordMatchType.EXACT;
      exactKeyword.matchTypeSpecified = true;
      exactKeyword.text = "dvd player";
      relatedKeyword.keywords = new Keyword[] {exactKeyword};

      // Set the idea type and request type.
      selector.ideaType = IdeaType.KEYWORD;
      selector.ideaTypeSpecified = true;

      selector.requestType = RequestType.IDEAS;
      selector.requestTypeSpecified = true;

      // Restrict the results to first 10 items.
      selector.paging = new Paging();
      selector.paging.numberResultsSpecified = true;
      selector.paging.numberResults = 10;

      // Get suggestions.
      selector.searchParameters =
          new SearchParameter[] {excludeKeyword, matchKeyword, relatedKeyword};

      try {
        TargetingIdeaPage results = service.get(selector);

        if (results != null && results.entries != null) {
          Console.WriteLine("There are a total of {0} suggestions. The first {1} results " +
              "are displayed below.", results.totalNumEntries, results.entries.Length);
          foreach (TargetingIdea idea in results.entries) {
            foreach (Type_AttributeMapEntry entry in idea.data) {
              if (entry.value is KeywordAttribute) {
                KeywordAttribute keywordIdea = (KeywordAttribute) entry.value;
                Console.WriteLine("Keyword Suggestion - matchtype is {0} and value is {1}",
                    keywordIdea.value.matchType, keywordIdea.value.text);
              } else {
                // There can be more type of suggestions like NGRAMS, Placements,
                // etc. They can be casted into their right forms and detailed can
                // obtained as above.
                Console.WriteLine("Suggestion type is {0}", entry.key);
              }
            }
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve targeting ideas. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

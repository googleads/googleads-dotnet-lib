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
  /// This example retrieves keywords that are related to a given keyword.
  /// </summary>
  class GetRelatedKeywords : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This example retrieves keywords that are related to a given keyword.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the TargetingIdeaService.
      TargetingIdeaService targetingIdeaService =
          (TargetingIdeaService) user.GetService(AdWordsService.v200909.TargetingIdeaService);

      string keywordText = "space cruise";

      Keyword keyword = new Keyword();
      keyword.text = keywordText;
      keyword.matchTypeSpecified = true;
      keyword.matchType = KeywordMatchType.EXACT;

      RelatedToKeywordSearchParameter searchParameter = new RelatedToKeywordSearchParameter();
      searchParameter.keywords = new Keyword[] {keyword};

      TargetingIdeaSelector selector = new TargetingIdeaSelector();
      selector.searchParameters = new SearchParameter[] {searchParameter};
      selector.ideaTypeSpecified = true;
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestTypeSpecified = true;
      selector.requestType = RequestType.IDEAS;

      Paging paging = new Paging();
      paging.startIndex = 0;
      paging.startIndexSpecified = true;
      paging.numberResults = 10;
      paging.numberResultsSpecified = true;

      selector.paging = paging;

      try {
        TargetingIdeaPage page = targetingIdeaService.get(selector);

        if (page != null && page.entries != null) {
          Console.WriteLine("There are a total of {0} keywords related to '{1}'. The first {2}" +
            " entries are displayed below: \n", page.totalNumEntries, keywordText,
            page.entries.Length);

          foreach(TargetingIdea idea in page.entries) {
            foreach (Type_AttributeMapEntry entry in idea.data) {
              if (entry.key == AttributeType.KEYWORD) {
                KeywordAttribute kwdAttribute = entry.value as KeywordAttribute;
                Console.WriteLine("Related keyword with text = '{0}' and match type = '{1}'" +
                  " was found.", kwdAttribute.value.text, kwdAttribute.value.matchType);
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

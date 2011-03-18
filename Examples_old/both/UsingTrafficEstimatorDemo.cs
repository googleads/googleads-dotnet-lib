// Copyright 2010, Google Inc. All Rights Reserved.
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
using com.google.api.adwords.v13;
using com.google.api.adwords.v200909;

using System;
using System.Collections.Generic;

using Keywordv200909 = com.google.api.adwords.v200909.Keyword;

namespace com.google.api.adwords.examples.both {
  /// <summary>
  /// This code example shows how to use both v13 and v200909 APIs in a
  /// single method.
  /// </summary>
  class UsingTrafficEstimatorDemo : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "Shows how to use both v13 and v200909 APIs in a single method.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
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
      searchParameter.keywords = new Keyword[] { keyword };

      TargetingIdeaSelector selector = new TargetingIdeaSelector();
      selector.searchParameters = new SearchParameter[] { searchParameter };
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

      List<Keyword> suggestions = new List<Keyword>();
      try {
        TargetingIdeaPage page = targetingIdeaService.get(selector);

        if (page != null && page.entries != null) {
          Console.WriteLine("There are a total of {0} keywords related to '{1}'. The first {2}" +
            " entries are displayed below: \n", page.totalNumEntries, keywordText,
            page.entries.Length);

          foreach (TargetingIdea idea in page.entries) {
            foreach (Type_AttributeMapEntry entry in idea.data) {
              if (entry.key == AttributeType.KEYWORD) {
                KeywordAttribute kwdAttribute = entry.value as KeywordAttribute;
                suggestions.Add(kwdAttribute.value);
              }
            }
          }
        } else {
          Console.WriteLine("No related keywords were found for your keyword.");
        }
        TrafficEstimatorService service = (TrafficEstimatorService) user.GetService(
            AdWordsService.v13.TrafficEstimatorService);
        List<KeywordTrafficRequest> keywordTrafficRequests = new List<KeywordTrafficRequest>();

        foreach (Keyword suggestion in suggestions) {
          KeywordTrafficRequest trafficRequest = new KeywordTrafficRequest();
          trafficRequest.keywordText = suggestion.text;

          switch (suggestion.matchType) {
            case KeywordMatchType.BROAD:
              trafficRequest.keywordType = KeywordType.Broad;
              break;

            case KeywordMatchType.EXACT:
              trafficRequest.keywordType = KeywordType.Exact;
              break;

            case KeywordMatchType.PHRASE:
              trafficRequest.keywordType = KeywordType.Phrase;
              break;

          }
          trafficRequest.language = "en_US";
          keywordTrafficRequests.Add(trafficRequest);
        }

        KeywordTraffic[] traffics = service.checkKeywordTraffic(keywordTrafficRequests.ToArray());

        if (traffics != null) {
          for (int i = 0; i < traffics.Length; i++) {
            Console.WriteLine("Keyword is '{0}' and traffic estimate is '{1}'",
              suggestions[i].text, traffics[i]);
          }
        } else {
          Console.WriteLine("Could not estimate traffic for keywords.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve traffic estimates for related keywords. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

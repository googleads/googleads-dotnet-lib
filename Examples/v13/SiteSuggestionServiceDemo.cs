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
using com.google.api.adwords.v13;

using System;
using System.Text;

namespace com.google.api.adwords.samples.v13 {
  /// <summary>
  /// Gets web site suggestions by topics.
  /// </summary>
  class SiteSuggestionServiceDemo : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Gets web site suggestions by topics.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      SiteSuggestionService service =
          (SiteSuggestionService) user.GetService(AdWordsService.v13.SiteSuggestionService);

      String[] topics = {"soap", "xml", "virtual machine"};

      LanguageGeoTargeting targeting = new LanguageGeoTargeting();
      targeting.languages = new String[] {"en_US", "es"};
      targeting.countries = new String[] {"US", "ES"};

      // Get site suggestions.
      SiteSuggestion[] sites = service.getSitesByTopics(topics, targeting);

      if (sites != null) {
        Console.WriteLine("{0, -16}{1, -15}{2, -15}{3, -12}{4, -14}", "acceptsImageAds",
            "acceptsTextAds", "acceptsVideoAds", "pageViews", "url");

        for (int i = 0; i < sites.Length; i++) {
          SiteSuggestion site = sites[i];
          Console.WriteLine("{0, -16}{1, -15}{2, -15}{3, -12}{4, -14}", site.acceptsImageAds,
              site.acceptsTextAds, site.acceptsVideoAds, site.pageViews, site.url);
        }
      } else {
        Console.WriteLine("No site suggestions available for given keyword(s)");
      }
    }
  }
}

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
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example gets all ad parameters in an ad group. To set ad params,
  /// run SetAdParams.cs.
  ///
  /// Tags: AdParamService.get
  /// </summary>
  class GetAllAdParams : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all ad parameters in an ad group. To set ad params, " +
            "run SetAdParams.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllAdParams();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdParamService.
      AdParamService adParamService =
          (AdParamService) user.GetService(AdWordsService.v201109.AdParamService);

      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"CriterionId", "InsertionText", "ParamIndex"};

      // Set a filter condition.
      Predicate predicate = new Predicate();
      predicate.field = "AdGroupId";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] {adGroupId.ToString()};

      selector.predicates = new Predicate[] {predicate};

      try {
        AdParamPage page = adParamService.get(selector);
        if (page != null && page.entries != null) {
          foreach (AdParam adParam in page.entries) {
            Console.WriteLine("Ad param with text '{0}' was found for criterion with id '{1}' " +
                "and ad group id '{2}'.", adParam.insertionText, adParam.criterionId, adGroupId);
          }
        } else {
          Console.WriteLine("No ad parameters found for adgroup #{0}.", adGroupId);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve ad parameters(s). Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201306;
using Google.Api.Ads.Dfp.v201306;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201306 {
  /// <summary>
  /// This code example gets up to 500 system defined creative templates.
  ///
  /// Tags: CreativeTemplateService.getCreativeTemplatesByStatement
  /// </summary>
  class GetCreativeTemplatesByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets up to 500 system defined creative templates.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCreativeTemplatesByStatement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CreativeTemplateService.
      CreativeTemplateService creativeTemplateService =
          (CreativeTemplateService) user.GetService(DfpService.v201306.CreativeTemplateService);

      // Create a statement to only select system defined creative templates.
      Statement filterStatement =
          new StatementBuilder("WHERE type = :creativeTemplateType LIMIT 500").AddValue(
              "creativeTemplateType", CreativeTemplateType.SYSTEM_DEFINED.ToString()).ToStatement();

      try {
        // Get creative templates by statement.
        CreativeTemplatePage page = creativeTemplateService.getCreativeTemplatesByStatement(
            filterStatement);

        if (page.results != null) {
          int i = page.startIndex;
          foreach (CreativeTemplate creativeTemplate in page.results) {
            Console.WriteLine("{0}) Creative template with ID \"{1}\", name \"{2}\", and type " +
                "\"{3}\" was found.", i, creativeTemplate.id, creativeTemplate.name,
                creativeTemplate.type);
            i++;
          }
        }
        Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get creative templates. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

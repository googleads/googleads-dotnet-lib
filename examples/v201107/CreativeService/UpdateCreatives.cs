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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201107;
using Google.Api.Ads.Dfp.v201107;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201107 {
  /// <summary>
  /// This code example gets all image creatives. The Statement retrieves up to
  /// the maximum page size limit of 500. To create an image creative, run
  /// CreateCreatives.cs.
  ///
  /// Tags: CreativeService.getCreativesByStatement
  /// Tags: CreativeService.updateCreatives
  /// </summary>
  class UpdateCreatives : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all image creatives. The Statement retrieves up to the " +
            "maximum page size limit of 500. To create an image creative, run " +
            "CreateCreatives.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateCreatives();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CreativeService.
      CreativeService creativeService =
          (CreativeService) user.GetService(DfpService.v201107.CreativeService);

      // Create a Statement to get all image creatives.
      Statement statement = new StatementBuilder("WHERE creativeType = :creativeType LIMIT 500").
          AddValue("creativeType", "ImageCreative").ToStatement();

      try {
        // Get creatives by Statement.
        CreativePage page = creativeService.getCreativesByStatement(statement);

        if (page.results != null && page.results.Length > 0) {
          Creative[] creatives = page.results;

          // Update each local creative object by changing its destination URL.
          foreach (Creative creative in creatives) {
            if (creative is ImageCreative) {
              ImageCreative imageCreative = (ImageCreative) creative;
              imageCreative.destinationUrl = "http://news.google.com";
            }
          }

          // Update the creatives on the server.
          creatives = creativeService.updateCreatives(creatives);

          if (creatives != null) {
            foreach (Creative creative in creatives) {
              if (creative is ImageCreative) {
                ImageCreative imageCreative = (ImageCreative) creative;
                Console.WriteLine("An image creative with ID = '{0}' and destination URL ='{1}' " +
                    "was updated.", imageCreative.id, imageCreative.destinationUrl);
              }
            }
          } else {
            Console.WriteLine("No creatives updated.");
          }
        } else {
          Console.WriteLine("No creatives found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update creatives. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201103;
using Google.Api.Ads.Dfp.v201103;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201103 {
  /// <summary>
  /// This code example copies a given set of image creatives. This would
  /// typically be done to reuse creatives in a small business network. To
  /// determine which creatives exist, run GetAllCreatives.cs.
  /// </summary>
  class CopyImageCreatives : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example copies a given set of image creatives. This would typically be" +
            " done to reuse creatives in a small business network. To determine which creatives " +
            "exist, run GetAllCreatives.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CopyImageCreatives();
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
          (CreativeService) user.GetService(DfpService.v201103.CreativeService);

      long[] creativeIds = new long[] {long.Parse(_T("INSERT_IMAGE_CREATIVE_ID_HERE"))};

      // Build a comma separated list of creativeIds. Note that if you are using
      // .NET 4.0 or above, you could use the newly introduced
      // String.Join<T>(string separator, IEnumerable<T> values) method to
      // perform this task.
      string[] creativeIdTexts = new string[creativeIds.Length];
      for (int i = 0; i < creativeIdTexts.Length; i++) {
        creativeIdTexts[i] = creativeIds[i].ToString();
      }

      string commaSeparatedCreativeIds = string.Join(",", creativeIdTexts);

      // Create the statement to filter image creatives by id.
      Statement statement = new StatementBuilder(
          string.Format("WHERE id IN ({0}) and creativeType = :creativeType LIMIT 500",
              commaSeparatedCreativeIds)).AddValue("creativeType", "ImageCreative").
              ToStatement();

      try {
        // Retrieve all creatives which match.
        CreativePage page = creativeService.getCreativesByStatement(statement);

        if (page.results != null) {
          Creative[] creatives = page.results;
          long[] oldIds = new long[creatives.Length];
          for (int i = 0; i < creatives.Length; i++) {
            ImageCreative imageCreative = (ImageCreative) creatives[i];
            oldIds[i] = imageCreative.id;
            // Since we cannot set id to null, we mark it as not specified.
            imageCreative.idSpecified = false;

            imageCreative.advertiserId = imageCreative.advertiserId;
            imageCreative.name = imageCreative.name + " (Copy #" + GetTimeStamp() + ")";
            imageCreative.imageByteArray = MediaUtilities.GetAssetDataFromUrl(
                imageCreative.imageUrl);
            creatives[i] = imageCreative;
          }

          // Create the copied creative.
          creatives = creativeService.createCreatives(creatives);

          // Display copied creatives.
          for (int i = 0; i < creatives.Length; i++) {
            Console.WriteLine("Image creative with ID \"{0}\" copied to ID \"{1}\".", oldIds[i],
                creatives[i].id);
          }
        } else {
          Console.WriteLine("No creatives were copied.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to copy creatives. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

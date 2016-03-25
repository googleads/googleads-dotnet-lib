// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example gets all express businesses. To add an express
  /// business, run AddExpressBusinesses.cs.
  /// </summary>
  public class GetExpressBusinesses : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all express businesses. To add an express business, " +
            "run AddExpressBusinesses.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetExpressBusinesses codeExample = new GetExpressBusinesses();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the ExpressBusinessService.
      ExpressBusinessService businessService = (ExpressBusinessService)
          user.GetService(AdWordsService.v201603.ExpressBusinessService);

      Selector selector = new Selector() {
        fields = new String[] { ExpressBusiness.Fields.Id, ExpressBusiness.Fields.Name,
          ExpressBusiness.Fields.Website, ExpressBusiness.Fields.Status
        },
        predicates = new Predicate[] {
          // To get all express businesses owned by the current customer,
          // simply skip the call to selector.setPredicates below.
          Predicate.Equals(ExpressBusiness.Fields.Status, ExpressBusinessStatus.ENABLED.ToString())
        },
        paging = Paging.Default
      };
      ExpressBusinessPage page = null;
      try {
        do {
          // Get all businesses.
          page = businessService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;
            foreach (ExpressBusiness business in page.entries) {
              Console.WriteLine("{0}) Express business found with name '{1}', id = {2}, " +
                  "website = {3} and status = {4}.\n", i + 1, business.name, business.id,
                  business.website, business.status);
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of businesses found: {0}", page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve express business.", e);
      }
    }

    /// <summary>
    /// Formats the geopoint as a printable text.
    /// </summary>
    /// <param name="geoPoint">The geo point.</param>
    /// <returns>The formatted text.</returns>
    private string FormatGeopoint(GeoPoint geoPoint) {
      if (geoPoint == null) {
        return "(Unknown, Unknown)";
      }
      return string.Format("({0}, {1})", (decimal) geoPoint.latitudeInMicroDegrees / 1000000,
          (decimal) geoPoint.longitudeInMicroDegrees / 1000000);
    }
  }
}
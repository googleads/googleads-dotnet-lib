// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201409;

using System;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201409 {

  /// <summary>
  /// This code example gets all express businesses. To add an express
  /// business, run AddExpressBusinesses.cs.
  ///
  /// Tags: ExpressBusinessService.get
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
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the ExpressBusinessService.
      ExpressBusinessService businessService = (ExpressBusinessService)
          user.GetService(AdWordsService.v201409.ExpressBusinessService);

      Selector selector = new Selector();
      selector.fields = new String[] { "Id", "Name", "Website", "Address", "GeoPoint", "Status" };

      // To get all express businesses owned by the current customer,
      // simply skip the call to selector.setPredicates below.
      Predicate predicate = new Predicate();
      predicate.field = "Status";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] { "ACTIVE" };

      selector.predicates = new Predicate[] { predicate };

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      ExpressBusinessPage page = null;
      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get all businesses.
          page = businessService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (ExpressBusiness business in page.entries) {
              Console.WriteLine("{0}) Express business found with name '{1}', id = {2}, " +
                  "website = {3} and status = {4}.\n", i + 1, business.name, business.id,
                  business.website, business.status);
              Console.WriteLine("Address");
              Console.WriteLine("=======");
              Console.WriteLine(FormatAddress(business.address));
              Console.WriteLine("Co-ordinates: {0}\n", FormatGeopoint(business.geoPoint));
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of businesses found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve express business.", ex);
      }
    }

    /// <summary>
    /// Formats the address as a printable text.
    /// </summary>
    /// <param name="address">The address object.</param>
    /// <returns>The formatted text.</returns>
    private static string FormatAddress(Address address) {
      if (address == null) {
        return "Not available.";
      }
      StringBuilder addressBuilder = new StringBuilder();

      addressBuilder.AppendFormat("Line 1: {0}\n", address.streetAddress ?? "");
      addressBuilder.AppendFormat("Line 2: {0}\n", address.streetAddress2 ?? "");
      addressBuilder.AppendFormat("Province Name: {0}\n", address.provinceName ?? "");
      addressBuilder.AppendFormat("Province Code: {0}\n", address.provinceCode ?? "");
      addressBuilder.AppendFormat("City name: {0}\n", address.cityName ?? "");
      addressBuilder.AppendFormat("Postal code: {0}\n", address.postalCode ?? "");
      addressBuilder.AppendFormat("Country name: {0}\n", address.countryCode ?? "");

      return addressBuilder.ToString();
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
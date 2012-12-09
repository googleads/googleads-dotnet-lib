// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201211;
using Google.Api.Ads.Dfp.v201211;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201211 {
  /// <summary>
  /// This code example updates the names of all companies that are advertisers
  /// by appending "LLC." up to the first 500. To determine which companies
  /// exist, run GetAllCompanies.cs.
  ///
  /// Tags: CompanyService.getCompaniesByStatement, CompanyService.updateCompanies
  /// </summary>
  class UpdateCompanies : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the names of all companies that are advertisers by " +
            "appending 'LLC.' up to the first 500. To determine which companies exist, run " +
            "GetAllCompanies.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateCompanies();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CompanyService.
      CompanyService companyService =
          (CompanyService) user.GetService(DfpService.v201211.CompanyService);

      // Create a Statement to only select companies that are advertisers.
      Statement statement = new StatementBuilder("WHERE type = :advertiser LIMIT 500").AddValue(
          "advertiser", CompanyType.ADVERTISER.ToString()).ToStatement();

      try {
        // Get the companies by Statement.
        CompanyPage page = companyService.getCompaniesByStatement(statement);

        if (page.results != null && page.results.Length > 0) {
          Company[] companies = page.results;

          // Update each local company object by appending LLC. to its name.
          foreach (Company company in companies) {
            company.name = company.name + " LLC.";
          }

          // Update the companies on the server.
          companies = companyService.updateCompanies(companies);

          if (companies != null && companies.Length > 0) {
            int i = 0;
            foreach (Company company in companies) {
              Console.WriteLine("{0}) Company with ID = {1}, name = {2} was updated",
                  i, company.id, company.name, company.type);
              i++;
            }
          } else {
            Console.WriteLine("No companies updated.");
          }
        } else {
          Console.WriteLine("No companies found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update companies. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

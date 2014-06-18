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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201311;
using Google.Api.Ads.Dfp.v201311;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201311 {
  /// <summary>
  /// This code example gets all companies that are advertisers. The Statement
  /// retrieves up to the maximum page size limit of 500. To create companies,
  /// run CreateCompanies.cs.
  ///
  /// Tags: CompanyService.getCompaniesByStatement
  /// </summary>
  class GetCompaniesByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all companies that are advertisers. The Statement " +
            "retrieves up to the maximum page size limit of 500. To create companies, run " +
            "CreateCompanies.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCompaniesByStatement();
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
          (CompanyService) user.GetService(DfpService.v201311.CompanyService);

      // Create a Statement to only select companies that are advertisers sorted
      // by name.
      Statement statement = new StatementBuilder("WHERE type = :advertiser ORDER BY name " +
          "LIMIT 500").AddValue("advertiser", CompanyType.ADVERTISER.ToString()).ToStatement();

      try {
        // Get companies by Statement.
        CompanyPage page = companyService.getCompaniesByStatement(statement);

        if (page.results != null) {
          int i = page.startIndex;
          foreach (Company company in page.results) {
            Console.WriteLine("{0}) Company with ID = {1}, name = {2} and type = {3} was found",
                i, company.id, company.name, company.type);
            i++;
          }
        }
        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get companies. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
